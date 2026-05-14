# Store the Application Insights Connection String in Key Vault
resource "azurerm_key_vault_secret" "appinsights_connectionstring" {
  name         = "ApplicationInsights--ConnectionString"
  value        = azurerm_application_insights.main.connection_string
  key_vault_id = azurerm_key_vault.app.id
  depends_on   = [azurerm_application_insights.main, azurerm_key_vault.app]
}
# Application Insights
resource "azurerm_application_insights" "main" {
  name                = substr(replace(lower("${var.app_name}-${var.env}-appi"), "_", "-"), 0, 24)
  location            = azurerm_resource_group.main.location
  resource_group_name = azurerm_resource_group.main.name
  application_type    = "web"
  retention_in_days   = 30
  # workspace_id can be added for Log Analytics integration
}
terraform {
  backend "azurerm" {
    resource_group_name  = "HireMattResources"
    storage_account_name = "hirematttfstatestorage"
    container_name       = "tfstate"
      key                  = "offermanager.tfstate"
  }
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = ">= 3.0.0"
    }
  }
  required_version = ">= 1.0.0"
}

provider "azurerm" {
  features {}
  subscription_id = var.subscription_id
}

resource "azurerm_resource_group" "main" {
  # Only allow alphanumeric, dash, underscore, parentheses, and periods
  name = substr(replace(lower("${var.app_name}-${var.env}-rg"), "_", "-"), 0, 24)
  location = var.location
}

# Azure Key Vault for the app
resource "azurerm_key_vault" "app" {
  name                        = substr(replace(lower("${var.app_name}-${var.env}-kv"), "_", "-"), 0, 24)
  location                    = azurerm_resource_group.main.location
  resource_group_name         = azurerm_resource_group.main.name
  tenant_id                   = data.azurerm_client_config.current.tenant_id
  sku_name                    = "standard"
  access_policy               = []
  rbac_authorization_enabled   = true
}

# SQL admin credentials (for demo, use Key Vault or secure method in production)
resource "random_password" "sql_admin" {
  length  = 16
  special = true
}

resource "azurerm_mssql_server" "main" {
  name = substr(replace(lower("${var.app_name}-${var.env}-sqlsrv"), "_", "-"), 0, 24)
  resource_group_name          = azurerm_resource_group.main.name
  location                     = azurerm_resource_group.main.location
  version                      = "12.0"
  administrator_login          = "sqladminuser"
  administrator_login_password = random_password.sql_admin.result
}

resource "azurerm_mssql_database" "main" {
  name = substr(replace(lower("${var.app_name}-${var.env}-sqldb"), "_", "-"), 0, 24)
  server_id           = azurerm_mssql_server.main.id
  sku_name            = "S0"
  collation           = "SQL_Latin1_General_CP1_CI_AS"
  max_size_gb         = 5
}

data "azurerm_client_config" "current" {}

resource "azurerm_role_assignment" "current_kv_secrets_officer" {
  scope                = azurerm_key_vault.app.id
  role_definition_name = "Key Vault Secrets Officer"
  principal_id         = data.azurerm_client_config.current.object_id
  depends_on   = [azurerm_key_vault.app]
}

# SQL Server Firewall Rules
resource "azurerm_mssql_firewall_rule" "allow_azure_services" {
  name             = "AllowAzureServices"
  server_id        = azurerm_mssql_server.main.id
  start_ip_address = "0.0.0.0"
  end_ip_address   = "0.0.0.0"
}

# Store the connection string in Key Vault
resource "azurerm_key_vault_secret" "db_connection" {
  name         = "DbConnectionString"
  value        = "Server=tcp:${azurerm_mssql_server.main.fully_qualified_domain_name},1433;Initial Catalog=${azurerm_mssql_database.main.name};Persist Security Info=False;User ID=sqladminuser;Password=${random_password.sql_admin.result};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
  key_vault_id = azurerm_key_vault.app.id
  depends_on = [
    azurerm_role_assignment.current_kv_secrets_officer,
    azurerm_mssql_firewall_rule.allow_azure_services
  ]
}

# Azure Container Registry
resource "azurerm_container_registry" "main" {
  name                = substr(replace(lower("${var.app_name}${var.env}acr"), "_", ""), 0, 50)
  resource_group_name = azurerm_resource_group.main.name
  location            = azurerm_resource_group.main.location
  sku                 = "Basic"
  admin_enabled       = false
}

# Azure Kubernetes Service (AKS)
resource "azurerm_kubernetes_cluster" "main" {
  name                = substr(replace(lower("${var.app_name}-${var.env}-aks"), "_", "-"), 0, 24)
  location            = azurerm_resource_group.main.location
  resource_group_name = azurerm_resource_group.main.name
  dns_prefix          = substr(replace(lower("${var.app_name}-${var.env}-aks"), "_", "-"), 0, 24)

  default_node_pool {
    name       = "default"
    node_count = 2
    vm_size    = "Standard_D2s_v3"
  }

  identity {
    type = "SystemAssigned"
  }

  network_profile {
    network_plugin = "azure"
    load_balancer_sku = "standard"
  }

  depends_on = [azurerm_resource_group.main, azurerm_key_vault.app, azurerm_mssql_server.main, azurerm_mssql_database.main, azurerm_container_registry.main]
}

# Grant AKS access to ACR
resource "azurerm_role_assignment" "aks_acr_pull" {
  principal_id         = azurerm_kubernetes_cluster.main.kubelet_identity[0].object_id
  role_definition_name = "AcrPull"
  scope                = azurerm_container_registry.main.id
  depends_on           = [azurerm_kubernetes_cluster.main, azurerm_container_registry.main]
}

# Grant AKS managed identity access to Key Vault secrets
resource "azurerm_role_assignment" "aks_keyvault_secrets_user" {
  scope                = azurerm_key_vault.app.id
  role_definition_name = "Key Vault Secrets User"
  principal_id         = azurerm_kubernetes_cluster.main.identity[0].principal_id
  depends_on           = [azurerm_kubernetes_cluster.main, azurerm_key_vault.app]
}

# Grant AKS node pool (kubelet identity) access to Key Vault secrets
resource "azurerm_role_assignment" "aks_kubelet_keyvault_secrets_user" {
  scope                = azurerm_key_vault.app.id
  role_definition_name = "Key Vault Secrets User"
  principal_id         = azurerm_kubernetes_cluster.main.kubelet_identity[0].object_id
  depends_on           = [azurerm_kubernetes_cluster.main, azurerm_key_vault.app]
}

# App Service Plan for React Frontend
resource "azurerm_service_plan" "frontend" {
  name                = substr(replace(lower("${var.app_name}-${var.env}-frontend-asp"), "_", "-"), 0, 40)
  location            = azurerm_resource_group.main.location
  resource_group_name = azurerm_resource_group.main.name
  os_type             = "Linux"
  sku_name            = "B1"
}

# One Static Web App per environment, named `<app>-<env>-frontend`. The env=dev
# state previously had this resource as frontend[0]; the moved block migrates
# that state in place without destroying the existing SWA.
moved {
  from = azurerm_static_web_app.frontend[0]
  to   = azurerm_static_web_app.frontend
}

resource "azurerm_static_web_app" "frontend" {
  name                = substr(replace(lower("${var.app_name}-${var.env}-frontend"), "_", "-"), 0, 60)
  location            = azurerm_resource_group.main.location
  resource_group_name = azurerm_resource_group.main.name
  sku_tier            = "Free"
  sku_size            = "Free"
}

resource "azurerm_static_web_app_custom_domain" "frontend" {
  count               = var.custom_domain != "" ? 1 : 0
  static_web_app_id   = azurerm_static_web_app.frontend.id
  domain_name         = var.custom_domain
  validation_type     = "cname-delegation"

  depends_on = [azurerm_service_plan.frontend]
}

# API Management for AKS backend
resource "azurerm_api_management" "main" {
  name                = substr(replace(lower("${var.app_name}-${var.env}-apim"), "_", "-"), 0, 50)
  location            = azurerm_resource_group.main.location
  resource_group_name = azurerm_resource_group.main.name
  publisher_name      = "OfferManager"
  publisher_email     = "admin@offermanager.com"
  sku_name            = "Consumption_0"

  depends_on = [azurerm_resource_group.main]
}

# API Management Backend (points to AKS)
resource "azurerm_api_management_backend" "aks_backend" {
  name                = "aks-backend"
  api_management_name = azurerm_api_management.main.name
  resource_group_name = azurerm_resource_group.main.name
  protocol            = "http"
  url                 = "http://135.233.56.119"

  depends_on = [azurerm_api_management.main]
}

# API Management API
resource "azurerm_api_management_api" "offermanager_api" {
  name                = "offermanager-api"
  resource_group_name = azurerm_resource_group.main.name
  api_management_name = azurerm_api_management.main.name
  revision            = "1"
  display_name        = "OfferManager API"
  path                = "api"
  protocols           = ["https"]

  depends_on = [azurerm_api_management.main]
}

locals {
  # Each env's APIM only needs to allow its own SWA: dev APIM serves the dev SPA,
  # prd APIM serves the prd SPA. The frontend deploy workflow points each branch
  # at the matching APIM, so cross-env CORS isn't needed.
  cors_allowed_origins = [
    "https://${azurerm_static_web_app.frontend.default_host_name}",
  ]
}

# CORS at API scope: browser preflight hits APIM first; without this, preflight never reaches ASP.NET.
resource "azurerm_api_management_api_policy" "offermanager_cors" {
  api_name            = azurerm_api_management_api.offermanager_api.name
  api_management_name = azurerm_api_management.main.name
  resource_group_name = azurerm_resource_group.main.name

  xml_content = <<XML
<policies>
  <inbound>
    <cors allow-credentials="false">
      <allowed-origins>
${join("\n", [for o in local.cors_allowed_origins : "        <origin>${o}</origin>"])}
      </allowed-origins>
      <allowed-methods>
        <method>GET</method>
        <method>POST</method>
        <method>PUT</method>
        <method>PATCH</method>
        <method>DELETE</method>
        <method>OPTIONS</method>
      </allowed-methods>
      <allowed-headers>
        <header>*</header>
      </allowed-headers>
      <expose-headers>
        <header>*</header>
      </expose-headers>
    </cors>
    <base />
  </inbound>
  <backend>
    <base />
  </backend>
  <outbound>
    <base />
  </outbound>
  <on-error>
    <base />
  </on-error>
</policies>
XML

  depends_on = [azurerm_api_management_api.offermanager_api]
}

# Catch-all forward per HTTP verb (single GET-only operation blocked POST/PATCH/DELETE to the API).
locals {
  apim_forward_methods = ["GET", "POST", "PUT", "PATCH", "DELETE"]
}

resource "azurerm_api_management_api_operation" "forward" {
  for_each            = toset(local.apim_forward_methods)
  operation_id        = "forward-${each.key}"
  api_name            = azurerm_api_management_api.offermanager_api.name
  api_management_name = azurerm_api_management.main.name
  resource_group_name = azurerm_resource_group.main.name
  display_name        = "Forward ${each.key} /*"
  method              = each.key
  # Use a named wildcard so policies can reliably reference the captured path.
  url_template        = "/{*path}"
  template_parameter {
    name        = "path"
    required    = true
    type        = "string"
    description = "Wildcard path segment forwarded to backend."
  }

  depends_on = [
    azurerm_api_management_api.offermanager_api,
    azurerm_api_management_api_policy.offermanager_cors,
  ]
}

resource "azurerm_api_management_api_operation_policy" "forward_policy" {
  for_each            = azurerm_api_management_api_operation.forward
  api_name            = azurerm_api_management_api.offermanager_api.name
  api_management_name = azurerm_api_management.main.name
  resource_group_name = azurerm_resource_group.main.name
  operation_id        = each.value.operation_id

  xml_content = <<XML
<policies>
  <inbound>
    <base />
    <set-backend-service base-url="http://135.233.56.119" />
    <!-- Forward the exact path the client used. Using /api/{path} can drop segments after the first
         for some wildcard matches, so GET /api/Offer/1 became /api/Offer and never hit GetById. -->
    <rewrite-uri template="@(context.Request.OriginalUrl.Path)" copy-unmatched-params="true" />
  </inbound>
  <backend>
    <forward-request />
  </backend>
  <outbound />
  <on-error />
</policies>
XML

  depends_on = [azurerm_api_management_api_operation.forward]
}
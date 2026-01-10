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
  enable_rbac_authorization   = true
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
terraform {
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
  resource_provider_registrations = "none"
}

resource "azurerm_resource_group" "main" {
  name     = "${var.resource_group_name}-${terraform.workspace}"
  location = var.location
}

# Azure Key Vault for the app
resource "azurerm_key_vault" "app" {
  name                        = "${var.resource_group_name}-${terraform.workspace}-kv"
  location                    = azurerm_resource_group.main.location
  resource_group_name         = azurerm_resource_group.main.name
  tenant_id                   = data.azurerm_client_config.current.tenant_id
  sku_name                    = "standard"
}

# SQL admin credentials (for demo, use Key Vault or secure method in production)
resource "random_password" "sql_admin" {
  length  = 16
  special = true
}

resource "azurerm_mssql_server" "main" {
  name                         = "${var.resource_group_name}-${terraform.workspace}-sqlsrv"
  resource_group_name          = azurerm_resource_group.main.name
  location                     = azurerm_resource_group.main.location
  version                      = "12.0"
  administrator_login          = "sqladminuser"
  administrator_login_password = random_password.sql_admin.result
}

resource "azurerm_mssql_database" "main" {
  name                = "${var.resource_group_name}-${terraform.workspace}-sqldb"
  server_id           = azurerm_mssql_server.main.id
  sku_name            = "S0"
  collation           = "SQL_Latin1_General_CP1_CI_AS"
  max_size_gb         = 5
}

# Store the connection string in Key Vault
resource "azurerm_key_vault_secret" "db_connection" {
  name         = "DbConnectionString"
  value        = "Server=tcp:${azurerm_mssql_server.main.fully_qualified_domain_name},1433;Initial Catalog=${azurerm_mssql_database.main.name};Persist Security Info=False;User ID=sqladminuser;Password=${random_password.sql_admin.result};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
  key_vault_id = azurerm_key_vault.app.id
  depends_on   = [azurerm_key_vault.app]
}

data "azurerm_client_config" "current" {}

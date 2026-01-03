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
  resource_provider_registrations = "none"
}

resource "azurerm_resource_group" "main" {
  name     = var.resource_group_name
  location = var.location
}

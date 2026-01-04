# Azure service principal authentication variables
variable "client_id" {
  description = "Azure service principal client ID"
  type        = string
}

variable "client_secret" {
  description = "Azure service principal client secret"
  type        = string
  sensitive   = true
}

variable "subscription_id" {
  description = "Azure subscription ID"
  type        = string
}
    condition     = can(regex("^[a-zA-Z0-9_.()-]+$", var.env))
    error_message = "The env variable may only contain alphanumeric characters, dashes, underscores, parentheses, and periods."
  }
}
# Application name for resource naming
variable "app_name" {
  description = "Application name for resource naming"
  type        = string
  default     = "offermanager"
  validation {
    condition     = can(regex("^[a-zA-Z0-9_.()-]+$", var.app_name))
    error_message = "The app_name variable may only contain alphanumeric characters, dashes, underscores, parentheses, and periods."
  }
}
# Define your Terraform variables here

variable "resource_group_name" {
  description = "Azure resource group name"
  type        = string
  default     = "OfferManagerResources"
}

variable "location" {
  description = "Azure region to deploy resources"
  type        = string
  default     = "centralus"
}


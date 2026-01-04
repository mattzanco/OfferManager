# Environment variable for deployment
variable "env" {
  description = "Deployment environment (e.g., dev, prod)"
  type        = string
  validation {
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


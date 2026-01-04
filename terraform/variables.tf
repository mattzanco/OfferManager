# Environment name (dev, prd, etc.)
variable "env" {
  default     = "dev"
  description = "Environment name for resource naming (e.g., dev, prd)"
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

variable "subscription_id" {
  description = "Azure subscription ID"
  type        = string
  default     = "caeb9f7e-fb54-4eb0-901c-5eeaac1e68d0"
}
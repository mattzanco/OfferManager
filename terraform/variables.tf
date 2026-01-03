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
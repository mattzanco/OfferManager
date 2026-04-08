# Application Insights Instrumentation Key
output "app_insights_instrumentation_key" {
	value       = azurerm_application_insights.main.instrumentation_key
	description = "Instrumentation Key for Application Insights"
	sensitive   = true
}

output "app_insights_connection_string" {
	value       = azurerm_application_insights.main.connection_string
	description = "Connection string for Application Insights"
	sensitive   = true
}

output "frontend_url" {
	value       = "https://${azurerm_static_web_app.frontend.default_host_name}"
	description = "URL of the React frontend Static Web App"
}

output "api_management_url" {
	value       = "https://${azurerm_api_management.main.name}.azure-api.net"
	description = "URL of the API Management service"
}

output "api_management_gateway_url" {
	value       = "https://${azurerm_api_management.main.name}.azure-api.net/api"
	description = "Gateway URL for the OfferManager API"
}
# Define your Terraform outputs here

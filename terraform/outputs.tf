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
	value       = "https://${azurerm_linux_web_app.frontend.default_hostname}"
	description = "URL of the React frontend App Service"
}

output "backend_url" {
	value       = "https://${azurerm_linux_web_app.backend.default_hostname}"
	description = "URL of the backend App Service"
}
# Define your Terraform outputs here

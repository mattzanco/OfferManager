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
# Define your Terraform outputs here

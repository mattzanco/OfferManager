# Application Insights Instrumentation Key
output "app_insights_instrumentation_key" {
	value = azurerm_application_insights.main.instrumentation_key
	description = "Instrumentation Key for Application Insights"
}

output "app_insights_connection_string" {
	value = azurerm_application_insights.main.connection_string
	description = "Connection string for Application Insights"
}
# Define your Terraform outputs here

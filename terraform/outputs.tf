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
	description = "React frontend URL for this environment's Static Web App."
}

# Sensitive: use for GitHub secret AZURE_STATIC_WEB_APPS_API_TOKEN_STAGING / _PRODUCTION (per env workspace).
output "static_web_app_deployment_token" {
	value       = azurerm_static_web_app.frontend.api_key
	description = "Deployment token for Azure/static-web-apps-deploy (GitHub Actions)."
	sensitive   = true
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

# GitHub Actions CI/CD Workflows

## Required GitHub Secrets

Configure these secrets in your GitHub repository settings (Settings → Secrets and variables → Actions):

### For Frontend Deployment (deploy-frontend.yml)
- **AZURE_PUBLISH_PROFILE_FRONTEND**: The publish profile XML from Azure App Service
  - Go to Azure Portal → App Service (offermanager-dev-frontend) → Download publish profile
  - Copy the entire XML content as the secret value

### For Backend Deployment (deploy-webapi.yml)
- **AZURE_REGISTRY_NAME**: Your Azure Container Registry name (e.g., `offermanageracr`)
- **AZURE_REGISTRY_USERNAME**: ACR username (get from ACR Access Keys)
- **AZURE_REGISTRY_PASSWORD**: ACR password (get from ACR Access Keys)

## Workflows

### deploy-frontend.yml
Triggers when:
- Changes pushed to `dev` branch
- Changes in `frontend/` directory

Actions:
1. Install npm dependencies
2. Build React app with `npm run build`
3. Deploy `frontend/dist` to Azure App Service

### deploy-webapi.yml
Triggers when:
- Changes pushed to `dev` branch
- Changes in backend directories (WebApi, Domain, Storage)

Actions:
1. Build Docker image from Dockerfile
2. Push to Azure Container Registry
3. Update AKS deployment with new image

## Setup Instructions

1. **Create Azure App Service credentials:**
   ```bash
   az webapp deployment list-publishing-profiles --name offermanager-dev-frontend \
     --resource-group offermanager-dev-rg --xml > profile.xml
   ```
   Copy the content to GitHub secret `AZURE_PUBLISH_PROFILE_FRONTEND`

2. **Get ACR credentials:**
   ```bash
   az acr credential show --name <your-registry-name> --resource-group offermanager-dev-rg
   ```
   Copy username and password to GitHub secrets

3. **Verify App Service names match** in the workflows and Terraform outputs

4. **Test by pushing to dev branch** - workflows will trigger automatically

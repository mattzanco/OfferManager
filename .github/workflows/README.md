# GitHub Actions CI/CD Workflows

## Required GitHub Secrets

Configure these in **Settings → Secrets and variables → Actions** (and use **Environments** if you add protection rules).

### Frontend (`deploy-frontend.yml`)

| Secret | Used on branch | How to obtain |
|--------|----------------|----------------|
| `AZURE_STATIC_WEB_APPS_API_TOKEN_STAGING` | `dev` | Azure Portal → **staging** Static Web App (e.g. dev) → **Overview** → **Manage deployment token** → copy value |
| `AZURE_STATIC_WEB_APPS_API_TOKEN_PRODUCTION` | `main` | Same for the **production** Static Web App (prd). If the SWA was recreated or moved, generate a **new** token; old tokens show *No matching Static Web App was found or the api key was invalid*. |
| `APIM_SUBSCRIPTION_KEY_STAGING` | `dev` | APIM subscription primary/secondary key for the dev gateway |
| `APIM_SUBSCRIPTION_KEY_PRODUCTION` | `main` | APIM subscription key for the production gateway |

After Terraform creates or replaces a Static Web App, refresh the matching GitHub secret from **Manage deployment token** on that resource.

### Backend (`deploy-webapi.yml`)

- `AZURE_REGISTRY_NAME`, `AZURE_REGISTRY_USERNAME`, `AZURE_REGISTRY_PASSWORD` — ACR push credentials for the dev pipeline

## Workflows

### `deploy-frontend.yml`

**Triggers:** push to `dev` or `main` when `frontend/**` or this workflow file changes.

**Steps:** `npm ci` → `npm run build` (with branch-specific APIM URL + key) → deploy `frontend/dist` to the matching Static Web App (`Azure/static-web-apps-deploy`).

### `deploy-webapi.yml`

**Triggers:** push to `dev` when WebApi / Domain / Storage (or this workflow) changes.

**Steps:** build and push Docker image to ACR → `kubectl set image` on dev AKS.

## Terraform

For each environment, from that environment’s Terraform workspace (correct `key` in `backend "azurerm"`), run:

`terraform output -raw static_web_app_deployment_token`

Paste the value into the matching GitHub secret (`…_STAGING` or `…_PRODUCTION`). The output is sensitive and stored in state; protect state accordingly.

# OfferManager

## Overview
OfferManager is the **Offer Management microservice** within a larger 3PL (Third-Party Logistics) platform. It provides RESTful APIs for managing quotes, offers, revisions, and related business entities. Built with ASP.NET Core and SQL Server, it is designed as a containerized, independently deployable service within a Kubernetes ecosystem.

This microservice is part of a distributed 3PL system and would integrate with other services such as Customer Management, Pricing Engine, Fulfillment, and Order Management through well-defined API contracts.

## Features
- RESTful API for managing Offers, Users, Customers, Documents, and more
- Modular architecture with Domain, Storage, Services, and WebApi layers
- Database migrations managed with DbUp and SQL scripts
- Unit tests for controllers and repositories (xUnit, Moq)
- Docker and Kubernetes deployment ready
- Azure DevOps pipeline integration

## Architecture
### Microservice Design
OfferManager follows microservice principles:
- **Independent deployment**: Containerized and deployable via Kubernetes
- **Data ownership**: Maintains its own SQL Server database
- **API-driven**: RESTful API designed for inter-service communication
- **Scalability**: Horizontal scaling through Kubernetes replicas

### Service Components
- **OfferManager.Domain**: Domain models and repository interfaces
- **OfferManager.Storage**: Data access layer and repository implementations
- **OfferManager.Services**: Business logic (extensible for future features)
- **OfferManager.WebApi**: ASP.NET Core REST API
- **OfferManager.DbUp**: Database schema migrations and initialization
- **OfferManager.Tests**: Unit tests (xUnit, Moq)

### Integration Points
This microservice integrates with:
- **Customer Service**: Retrieves customer information and contact details
- **Pricing Service**: Accesses pricing rules and rates
- **RFQ (Request for Quote) Service**: Receives RFQ data and generates offers
- **Document Service**: Manages offer documents and attachments
- **Notification Service**: Sends offer notifications and updates

## Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (local or Azure SQL)
- [Docker](https://www.docker.com/products/docker-desktop) (for containerization)
- [Azure CLI](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli) (for Azure deployment)
- [kubectl](https://kubernetes.io/docs/tasks/tools/) (for Kubernetes management)

## Local Development
1. **Clone the repository**
	```sh
	git clone https://github.com/your-org/OfferManager.git
	cd OfferManager
	```
2. **Set up the database**
	- Update connection strings in `OfferManager.WebApi/appsettings.Development.json`.
	- Run migrations:
	  ```sh
	  dotnet run --project OfferManager.DbUp/OfferManager.DbUp.csproj
	  ```
3. **Run the API**
	```sh
	dotnet run --project OfferManager.WebApi/OfferManager.WebApi.csproj
	```
4. **Run tests**
	```sh
	dotnet test
	```

## Docker & Kubernetes
1. **Build Docker images**
	```sh
	docker build -t offermanager-api -f OfferManager.WebApi/Dockerfile .
	```
2. **Push to Azure Container Registry (ACR)**
	- Create ACR: `az acr create --resource-group <rg> --name <acrName> --sku Basic`
	- Login: `az acr login --name <acrName>`
	- Tag & push:
	  ```sh
	  docker tag offermanager-api <acrName>.azurecr.io/offermanager-api:latest
	  docker push <acrName>.azurecr.io/offermanager-api:latest
	  ```
3. **Deploy to AKS**
	- Create AKS: `az aks create --resource-group <rg> --name <aksName> --node-count 1 --enable-addons monitoring --generate-ssh-keys`
	- Get credentials: `az aks get-credentials --resource-group <rg> --name <aksName>`
	- Apply manifests:
	  ```sh
	  kubectl apply -f k8s/
	  ```

## Azure Resources Needed
- Resource Group
- Azure Kubernetes Service (AKS) Cluster
- Azure Container Registry (ACR)
- Azure SQL Database (or SQL Server VM)
- Azure Storage (if persistent storage is needed)
- Azure Key Vault (for secrets, optional)

## CI/CD
- Azure DevOps pipeline defined in `azure-pipelines.yml`

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

## License
[MIT](LICENSE)
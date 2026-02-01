
# OfferManager

## 🚀 Overview
OfferManager is a modern, cloud-native platform for managing commercial offers, users, customers, and business workflows. Built with ASP.NET Core and TypeScript, it leverages best practices in modular architecture, cloud deployment, and developer experience. Designed for scalability, security, and rapid iteration, OfferManager is ready for enterprise use and open-source collaboration.

## ✨ Key Features
- **Robust RESTful API**: Manage Offers, Users, Customers, Documents, and more with clean, versioned endpoints.
- **Modular Architecture**: Separation of concerns across Domain, Storage, Services, and WebApi layers for maintainability and extensibility.
- **Automated Database Migrations**: Reliable schema evolution using DbUp and SQL scripts.
- **Comprehensive Testing**: Unit and integration tests for controllers and repositories (xUnit, Moq).
- **Cloud-Ready Deployment**: Docker and Kubernetes manifests for seamless Azure AKS rollout.
- **Continuous Integration & Delivery**: Azure DevOps pipelines for automated build, test, and deploy.
- **Frontend Powered by Vite + React**: Fast, modern UI with TypeScript and component-driven development.
- **Enterprise Security**: Azure Key Vault integration for secret management.
- **Observability**: Integrated Application Insights for full-stack monitoring and diagnostics.

## 🏗️ Architecture
- **Domain Layer**: Business models and repository interfaces.
- **Storage Layer**: Data access and persistence logic.
- **Services Layer**: Business rules and orchestration (extensible).
- **WebApi Layer**: ASP.NET Core API, CORS, Swagger, and middleware.
- **DbUp**: Automated database migrations.
- **Frontend**: React + Vite, TypeScript, modular UI.
- **Tests**: xUnit, Moq, high coverage.

## 🛠️ Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Node.js & npm](https://nodejs.org/) (for frontend)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (local or Azure SQL)
- [Docker](https://www.docker.com/products/docker-desktop)
- [Azure CLI](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli)
- [kubectl](https://kubernetes.io/docs/tasks/tools/)

## 🏃‍♂️ Getting Started (Local Development)
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
4. **Run the Frontend**
	```sh
	cd frontend
	npm install
	npm run dev
	```
5. **Run tests**
	```sh
	dotnet test
	```

## ☁️ Cloud Deployment (Docker & Kubernetes)
1. **Build Docker images**
	```sh
	docker build -t offermanager-api -f OfferManager.WebApi/Dockerfile .
	```
2. **Push to Azure Container Registry (ACR)**
	```sh
	az acr create --resource-group <rg> --name <acrName> --sku Basic
	az acr login --name <acrName>
	docker tag offermanager-api <acrName>.azurecr.io/offermanager-api:latest
	docker push <acrName>.azurecr.io/offermanager-api:latest
	```
3. **Deploy to AKS**
	```sh
	az aks create --resource-group <rg> --name <aksName> --node-count 1 --enable-addons monitoring --generate-ssh-keys
	az aks get-credentials --resource-group <rg> --name <aksName>
	kubectl apply -f k8s/
	```

## 🔒 Azure Resources
- Resource Group
- AKS Cluster
- ACR
- Azure SQL Database
- Azure Key Vault
- Azure Storage (optional)

## 🔄 CI/CD
- Automated pipeline in `azure-pipelines.yml` for build, test, and deploy.

## 🤝 Contributing
Open to pull requests and collaboration! For major changes, please open an issue to discuss your ideas.

## 📄 License
[MIT](LICENSE)
# SX400 — Full-Stack Application

> **Stack:** React 18 + Vite (Frontend) · ASP.NET Core 8 (Backend API) · Azure SQL Server (Database)
> **CI/CD:** Azure DevOps Pipelines → Azure App Services

## 📁 Project Structure

```
fe-be_sx400/
├── backend/              # ASP.NET Core 8 Web API
│   ├── SX400.Api/        # API project
│   └── SX400.Api.sln     # Solution file
├── frontend/             # React 18 + Vite
│   ├── src/
│   └── package.json
├── pipelines/            # Azure DevOps YAML pipelines
│   ├── azure-pipelines-build.yml
│   └── azure-pipelines-deploy.yml
└── README.md
```

---

## 🚀 Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js 20+](https://nodejs.org/)
- Azure SQL Server (or SQL Server locally)

### Backend

```bash
cd backend
dotnet restore
dotnet run --project SX400.Api
```

The API will start at `https://localhost:5001`. Open **Swagger UI**: `https://localhost:5001/swagger`

### Frontend

```bash
cd frontend
npm install
npm run dev
```

The app opens at `http://localhost:5173`. API calls are proxied to the backend via Vite config.

---

## 🔗 API Endpoints

| Method | Endpoint              | Description                |
|--------|-----------------------|----------------------------|
| GET    | `/api/weather`        | List all forecasts         |
| GET    | `/api/weather/{id}`   | Get forecast by ID         |
| POST   | `/api/weather`        | Create a new forecast      |
| PUT    | `/api/weather/{id}`   | Update an existing forecast|
| DELETE | `/api/weather/{id}`   | Delete a forecast          |

---

## ⚙️ Configuration

### Azure SQL Connection String

Set in `backend/SX400.Api/appsettings.json` or via Azure App Service **Application Settings**:

```
ConnectionStrings__DefaultConnection=Server=tcp:<server>.database.windows.net,1433;...
```

### Frontend API URL

Set the `VITE_API_URL` environment variable (in `.env` or as a pipeline variable):

```
VITE_API_URL=https://sx400-api.azurewebsites.net/api
```

---

## 🔄 CI/CD Pipelines

### Build Pipeline (`pipelines/azure-pipelines-build.yml`)

- Triggers on push to `main`
- **Parallel stages:** builds backend (.NET) and frontend (React) simultaneously
- Publishes artifacts: `backend-drop` and `frontend-drop`

### Deploy Pipeline (`pipelines/azure-pipelines-deploy.yml`)

- Triggers after the build pipeline completes
- **Parallel jobs:** deploys to two Azure App Services simultaneously
  - Backend → `<YOUR_BACKEND_APP_SERVICE_NAME>`
  - Frontend → `<YOUR_FRONTEND_APP_SERVICE_NAME>`

### Pipeline Variables to Configure

| Variable | Description |
|----------|-------------|
| `azureSubscription` | Azure service connection name |
| `backendAppName` | Backend App Service name |
| `frontendAppName` | Frontend App Service name |
| `VITE_API_URL` | Backend API URL for the frontend build |

---

## 📦 Azure App Service Setup

1. **Create two App Services** in the Azure Portal:
   - Backend: Linux, .NET 8, e.g., `sx400-api`
   - Frontend: Linux, Node 20 LTS, e.g., `sx400-frontend`
2. **Configure the backend** App Service setting: `ConnectionStrings__DefaultConnection`
3. **Configure CORS** on the backend to allow the frontend origin
4. **Import pipelines** in Azure DevOps pointing to the YAML files in `pipelines/`

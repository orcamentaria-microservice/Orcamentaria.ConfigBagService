# Orcamentaria • Orcamentaria.ConfigBagService.sln (.NET 8)

Parte do ecossistema **Orcamentaria**, responsável por processamento de mensagens, integração com Service Registry e controle de fluxo de eventos entre serviços.

---

## 🧱 Stack & Arquitetura
- **.NET 8** (ASP.NET Core Web API)
- RabbitMQ para mensageria
- Integração com **Service Registry**
- Versionamento de API: `api/v1/...`

Estrutura do repositório:
```
Orcamentaria.ConfigBagService.sln/
 ├── Orcamentaria.ConfigBagService.sln.API/
 ├── Orcamentaria.ConfigBagService.sln.Application/
 ├── Orcamentaria.ConfigBagService.sln.Domain/
 ├── Orcamentaria.ConfigBagService.sln.Infrastructure/
 └── Orcamentaria.ConfigBagService.sln.sln
```

---

## ⚙️ Configuração (appsettings.json)

- **ConnectionStrings.DefaultConnection** :
  - `mongodb+srv://<usuario>:<senha>@<cluster>/<...>`
- **ApiGetawayConfiguration**:
  - `BaseUrl`
---

- `ConnectionStrings.DefaultConnection`: url da conexão com o base de dados mongo.
- `ApiGetawayConfiguration`: url do getaway.
- Use `appsettings.Development.json` para overrides locais. **Nunca** commitar segredos reais.

---

## ▶️ Executando localmente

```bash
dotnet restore
dotnet build
dotnet run --project Orcamentaria.ConfigBagService.sln.API
```

---

## 🧭 Endpoints (v1)

- **GET** `/api/v1/ConfigurationBag/GetByServiceName/{serviceName}`
- **GET** `/api/v1/ConfigurationBag/GetConfigurationBagServiceName`
- **POST** `/api/v1/ConfigurationBag/Configurations`
- **POST** `/api/v1/ConfigurationBag/ConnectionStrings`
- **POST** `/api/v1/ConfigurationBag/RealTimeConfigurations`

---

## ✨ Autor

**Marcelo Fernando**  
Desenvolvedor Fullstack | Arquitetura de Microserviços


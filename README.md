
- **Domain** → core business entities (Workflow, WorkflowStep, Process, ProcessStep)  
- **Application** → business logic and service interfaces  
- **Infrastructure** → database access via EF Core (SQL Server)  
- **Presentation** → API controllers and middleware  

---

## ⚙️ Technologies Used

| Layer | Technology |
|-------|-------------|
| Backend Framework | .NET 8 Web API |
| ORM | Entity Framework Core |
| Database | SQL Server |
| JSON Serialization | Newtonsoft.Json / System.Text.Json |
| Validation | Custom Middleware |
| Architecture | Clean Architecture + Dependency Injection |

---

## 🧪 API Endpoints

### **1️⃣ Workflows Management**

#### ➕ Create a New Workflow  
**POST** `/v1/workflows`  
**Body:**
```json
{
  "name": "Approval Process",
  "description": "A workflow to approve purchase requests",
  "steps": [
    { "stepName": "Submit Request", "assignedTo": "employee", "actionType": "input", "nextStep": "Manager Approval" },
    { "stepName": "Manager Approval", "assignedTo": "manager", "actionType": "approve_reject", "nextStep": "Finance Approval" },
    { "stepName": "Finance Approval", "assignedTo": "finance", "actionType": "approve_reject", "nextStep": "Completed" }
  ]
}

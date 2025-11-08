using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Domain.Entities
{
    public class ValidationLog
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ProcessId { get; set; }
        public Guid WorkflowStepId { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

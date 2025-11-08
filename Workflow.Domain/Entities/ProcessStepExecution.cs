using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Domain.Entities
{
    public class ProcessStepExecution
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ProcessId { get; set; }
        public Guid WorkflowStepId { get; set; }
        public string StepName { get; set; } = null!;
        public string PerformedBy { get; set; } = null!;
        public string Action { get; set; } = null!; // "approve", "reject", "input"
        public DateTime PerformedAt { get; set; } = DateTime.UtcNow;
        public bool ValidationPassed { get; set; } = true;
        public string? ValidationMessage { get; set; }
        public Process Process { get; set; } = null!;
        public WorkflowStep WorkflowStep { get; set; } = null!;
    }
}

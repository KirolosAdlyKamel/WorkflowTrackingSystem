using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Enums;

namespace Workflow.Domain.Entities
{
    public class Process
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid WorkflowId { get; set; }
        public string Initiator { get; set; } = null!;
        public ProcessStatus Status { get; set; } = ProcessStatus.Active;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public List<ProcessStepExecution> Executions { get; set; } = new();
        public Workflow Workflow { get; set; } = null!;
    }
}

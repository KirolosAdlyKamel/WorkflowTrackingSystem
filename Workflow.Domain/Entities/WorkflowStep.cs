using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Domain.Entities
{
    public class WorkflowStep
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid WorkflowId { get; set; }
        public string StepName { get; set; } = null!;
        public string AssignedTo { get; set; } = null!;
        public string ActionType { get; set; } = null!;
        public string NextStep { get; set; } = null!;
        public bool RequiresValidation { get; set; } = false;

        public Workflow Workflow { get; set; } = null!;
    }
}

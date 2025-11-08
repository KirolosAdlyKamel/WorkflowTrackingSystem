using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities;

namespace Workflow.Application.Interfaces
{
    public interface IValidationService
    {
        Task<bool> ValidateStepAsync(WorkflowStep step);
    }
}

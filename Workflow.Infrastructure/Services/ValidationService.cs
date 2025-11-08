using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Application.Interfaces;
using Workflow.Domain.Entities;

namespace Workflow.Infrastructure.Services
{
    public class ValidationService : IValidationService
    {
        public async Task<bool> ValidateStepAsync(WorkflowStep step)
        {
            // simulate external API validation
            if (step.StepName == "Finance Approval")
            {
                await Task.Delay(300); // simulate call
                return false; // simulate validation failure
            }
            return true;
        }
    }
}

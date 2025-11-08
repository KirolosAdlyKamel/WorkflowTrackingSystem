using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Application.Interfaces;

namespace Workflow.Application.Services
{
    public class WorkflowService
    {
        private readonly IWorkflowRepository _workflowRepo;
        private readonly IValidationService _validationService;

        public WorkflowService(IWorkflowRepository workflowRepo, IValidationService validationService)
        {
            _workflowRepo = workflowRepo;
            _validationService = validationService;
        }

        public async Task<Domain.Entities.Workflow> CreateAsync(Domain.Entities.Workflow workflow)
        {
            await _workflowRepo.AddAsync(workflow);
            await _workflowRepo.SaveChangesAsync();
            return workflow;
        }

        public async Task<Domain.Entities.Workflow?> GetByIdAsync(Guid id)
        {
            return await _workflowRepo.GetByIdAsync(id);
        }

        public async Task<bool> ExecuteStepAsync(Guid workflowId, string stepName)
        {
            var workflow = await _workflowRepo.GetByIdAsync(workflowId);
            if (workflow == null) return false;

            var step = workflow.Steps.FirstOrDefault(s => s.StepName == stepName);
            if (step == null) return false;

            var isValid = await _validationService.ValidateStepAsync(step);
            return isValid;
        }
    }
}

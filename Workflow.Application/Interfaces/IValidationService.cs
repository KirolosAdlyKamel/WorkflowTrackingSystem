using Workflow.Domain.Entities;

namespace Workflow.Application.Interfaces;

public interface IValidationService
{
    Task<(bool success, string message)> ValidateAsync(Process process, WorkflowStep step);
}

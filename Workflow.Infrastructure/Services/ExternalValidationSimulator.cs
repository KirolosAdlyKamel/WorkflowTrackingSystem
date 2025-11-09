using Microsoft.Extensions.Logging;
using Workflow.Application.Interfaces;
using Workflow.Domain.Entities;

namespace Workflow.Infrastructure.Services;

public class ExternalValidationSimulator : IValidationService
{
    private readonly ILogger<ExternalValidationSimulator> _logger;
    public ExternalValidationSimulator(ILogger<ExternalValidationSimulator> logger) => _logger = logger;

    public Task<(bool success, string message)> ValidateAsync(Process process, WorkflowStep step)
    {
        _logger.LogInformation("Simulating external validation for Process {ProcessId} and Step {Step}", process.Id, step.StepName);

        // Simple rule: if step name contains "Finance" and initiator contains "fail" => fail
        if (step.StepName.Contains("Finance", StringComparison.OrdinalIgnoreCase) && process.Initiator.EndsWith("fail", StringComparison.OrdinalIgnoreCase))
        {
            return Task.FromResult((false, "External financial validation failed: insufficient budget."));
        }

        // else pass
        return Task.FromResult((true, "Validation passed"));
    }
}

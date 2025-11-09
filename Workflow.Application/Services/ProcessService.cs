using Microsoft.EntityFrameworkCore;
using Workflow.Application.Interfaces;
using Workflow.Domain.Entities;

namespace Workflow.Application.Services;

public class ProcessService
{
    private readonly IProcessRepository _processRepo;
    private readonly IWorkflowRepository _workflowRepo;
    private readonly IValidationService _validation;
    public ProcessService(IProcessRepository processRepo, IWorkflowRepository workflowRepo, IValidationService validation)
    {
        _processRepo = processRepo;
        _workflowRepo = workflowRepo;
        _validation = validation;
    }

    public async Task<Process> StartProcessAsync(int workflowId, string initiator)
    {
        var workflow = await _workflowRepo.GetByIdAsync(workflowId) ?? throw new KeyNotFoundException("Workflow not found");
        var process = new Process { WorkflowId = workflowId, Initiator = initiator, Status = ProcessStatus.Active, CreatedAt = DateTime.UtcNow };
        await _processRepo.AddAsync(process);
        await _processRepo.SaveChangesAsync();
        return process;
    }

    public async Task ExecuteStepAsync(int processId, string stepName, string performedBy, string action)
    {
        var process = await _processRepo.Query()
            .Include(p => p.Workflow).ThenInclude(w => w.Steps)
            .Include(p => p.Executions)
            .FirstOrDefaultAsync(p => p.Id == processId)
            ?? throw new KeyNotFoundException("Process not found");

        if (process.Status == ProcessStatus.Completed || process.Status == ProcessStatus.Rejected)
            throw new InvalidOperationException("Process is already finished");

        var step = process.Workflow.Steps.FirstOrDefault(s => s.StepName == stepName)
            ?? throw new KeyNotFoundException("Step not found in workflow");

        // Validation if required
        if (step.RequiresValidation)
        {
            var (success, message) = await _validation.ValidateAsync(process, step);
            // log validation via ProcessStepExecution + ValidationLog should be handled by repository/DbContext layer
            var logExec = new ProcessStepExecution
            {
                ProcessId = process.Id,
                WorkflowStepId = step.Id,
                StepName = step.StepName,
                PerformedBy = performedBy,
                Action = action,
                ValidationPassed = success,
                ValidationMessage = message,
                PerformedAt = DateTime.UtcNow
            };

            process.Executions.Add(logExec);

            if (!success)
            {
                // Save attempted execution
                await _processRepo.SaveChangesAsync();
                throw new InvalidOperationException($"Validation failed: {message}");
            }
        }

        // record success execution
        var exec = new ProcessStepExecution
        {
            ProcessId = process.Id,
            WorkflowStepId = step.Id,
            StepName = step.StepName,
            PerformedBy = performedBy,
            Action = action,
            ValidationPassed = true,
            PerformedAt = DateTime.UtcNow
        };
        process.Executions.Add(exec);

        // Advance status if next is Completed
        if (string.Equals(step.NextStep, "Completed", StringComparison.OrdinalIgnoreCase))
            process.Status = ProcessStatus.Completed;
        else
            process.Status = ProcessStatus.Active;

        await _processRepo.SaveChangesAsync();
    }

    public async Task<List<Process>> QueryProcessesAsync(int? workflowId, string? status, string? assignedTo)
    {
        var q = _processRepo.Query().Include(p => p.Workflow).Include(p => p.Executions).AsQueryable();
        if (workflowId.HasValue) q = q.Where(p => p.WorkflowId == workflowId.Value);
        if (!string.IsNullOrWhiteSpace(status) && Enum.TryParse<ProcessStatus>(status, true, out var st)) q = q.Where(p => p.Status == st);
        if (!string.IsNullOrWhiteSpace(assignedTo))
            q = q.Where(p => p.Workflow.Steps.Any(s => s.AssignedTo == assignedTo));

        return await q.OrderByDescending(p => p.CreatedAt).ToListAsync();
    }
}

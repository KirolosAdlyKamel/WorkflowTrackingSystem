namespace Workflow.Domain.Entities;

public class ProcessStepExecution
{
    public int Id { get; set; }
    public int ProcessId { get; set; }
    public Process Process { get; set; } = null!;

    public int WorkflowStepId { get; set; }
    public WorkflowStep WorkflowStep { get; set; } = null!;

    public string StepName { get; set; } = null!;
    public string PerformedBy { get; set; } = null!;
    public string Action { get; set; } = null!; // "approve", "reject", "input"
    public DateTime PerformedAt { get; set; } = DateTime.UtcNow;

    public bool ValidationPassed { get; set; } = true;
    public string? ValidationMessage { get; set; }
}

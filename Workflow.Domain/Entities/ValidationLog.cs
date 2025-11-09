namespace Workflow.Domain.Entities;

public class ValidationLog
{
    public int Id { get; set; }
    public int ProcessId { get; set; }
    public Process Process { get; set; } = null!;
    public int WorkflowStepId { get; set; }
    public WorkflowStep WorkflowStep { get; set; } = null!;
    public bool Success { get; set; }
    public string Message { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

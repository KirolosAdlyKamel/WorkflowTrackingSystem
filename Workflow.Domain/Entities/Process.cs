namespace Workflow.Domain.Entities;

public enum ProcessStatus { Pending, Active, Completed, Rejected }

public class Process
{
    public int Id { get; set; }
    public int WorkflowId { get; set; }
    public Workflow Workflow { get; set; } = null!;
    public string Initiator { get; set; } = null!;
    public ProcessStatus Status { get; set; } = ProcessStatus.Active;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<ProcessStepExecution> Executions { get; set; } = new List<ProcessStepExecution>();
}

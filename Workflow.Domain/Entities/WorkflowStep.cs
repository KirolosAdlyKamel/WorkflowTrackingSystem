namespace Workflow.Domain.Entities;

public class WorkflowStep
{
    public int Id { get; set; }
    public int WorkflowId { get; set; }
    public Workflow Workflow { get; set; } = null!;

    public string StepName { get; set; } = null!;
    public string AssignedTo { get; set; } = null!; // role or user id
    public string ActionType { get; set; } = null!; // "input", "approve_reject", etc.
    public string NextStep { get; set; } = null!; // next step name or "Completed"
    public bool RequiresValidation { get; set; } = false;
}

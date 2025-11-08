namespace Workflow.Domain.Entities
{
    public class Workflow
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public ICollection<WorkflowStep> Steps { get; set; } = new List<WorkflowStep>();
    }
}

namespace Workflow.Application.Interfaces;

public interface IWorkflowRepository
{
    Task<Domain.Entities.Workflow?> GetByIdAsync(int id);
    Task AddAsync(Domain.Entities.Workflow workflow);
    Task SaveChangesAsync();
}

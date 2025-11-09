using Workflow.Application.Interfaces;

namespace Workflow.Application.Services;

public class WorkflowService
{
    private readonly IWorkflowRepository _repo;
    public WorkflowService(IWorkflowRepository repo) => _repo = repo;

    public async Task<Domain.Entities.Workflow> CreateAsync(Domain.Entities.Workflow workflow)
    {
        await _repo.AddAsync(workflow);
        await _repo.SaveChangesAsync();
        return workflow;
    }

    public async Task<Domain.Entities.Workflow?> GetByIdAsync(int id) => await _repo.GetByIdAsync(id);
}

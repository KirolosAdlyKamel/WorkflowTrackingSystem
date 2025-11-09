using Microsoft.EntityFrameworkCore;
using Workflow.Application.Interfaces;
using Workflow.Infrastructure.Persistence;

namespace Workflow.Infrastructure.Repositories;

public class WorkflowRepository : IWorkflowRepository
{
    private readonly AppDbContext _db;
    public WorkflowRepository(AppDbContext db) => _db = db;

    public async Task AddAsync(Domain.Entities.Workflow workflow) => await _db.Workflows.AddAsync(workflow);

    public async Task<Domain.Entities.Workflow?> GetByIdAsync(int id) => await _db.Workflows.Include(w => w.Steps).FirstOrDefaultAsync(w => w.Id == id);

    public async Task SaveChangesAsync() => await _db.SaveChangesAsync();
}

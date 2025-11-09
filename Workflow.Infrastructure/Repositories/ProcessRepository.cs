using Microsoft.EntityFrameworkCore;
using Workflow.Application.Interfaces;
using Workflow.Domain.Entities;
using Workflow.Infrastructure.Persistence;

namespace Workflow.Infrastructure.Repositories;

public class ProcessRepository : IProcessRepository
{
    private readonly AppDbContext _db;
    public ProcessRepository(AppDbContext db) => _db = db;

    public async Task AddAsync(Process process) => await _db.Processes.AddAsync(process);

    public IQueryable<Process> Query() => _db.Processes.AsQueryable();

    public async Task<Process?> GetByIdAsync(int id) => await _db.Processes.Include(p => p.Executions).Include(p => p.Workflow).FirstOrDefaultAsync(p => p.Id == id);

    public async Task SaveChangesAsync() => await _db.SaveChangesAsync();
}

using Workflow.Domain.Entities;

namespace Workflow.Application.Interfaces;

public interface IProcessRepository
{
    Task<Process?> GetByIdAsync(int id);
    Task AddAsync(Process process);
    Task SaveChangesAsync();
    IQueryable<Process> Query();
}

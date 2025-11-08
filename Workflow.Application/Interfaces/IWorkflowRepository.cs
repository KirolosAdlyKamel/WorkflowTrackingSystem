using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workflow.Application.Interfaces
{
    public interface IWorkflowRepository
    {
        Task<Domain.Entities.Workflow?> GetByIdAsync(Guid id);
        Task AddAsync(Domain.Entities.Workflow workflow);
        Task SaveChangesAsync();
    }
}

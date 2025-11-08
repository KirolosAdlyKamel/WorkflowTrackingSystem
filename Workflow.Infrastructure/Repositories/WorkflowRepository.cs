using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Application.Interfaces;
using Workflow.Infrastructure.Persistence;

namespace Workflow.Infrastructure.Repositories
{

    public class WorkflowRepository : IWorkflowRepository
    {
        private readonly AppDbContext _context;
        public WorkflowRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Domain.Entities.Workflow workflow)
        {
            await _context.Workflows.AddAsync(workflow);
        }

        public async Task<Domain.Entities.Workflow?> GetByIdAsync(Guid id)
        {
            return await _context.Workflows.Include(w => w.Steps)
                                           .FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Workflow.Domain.Entities;

namespace Workflow.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Domain.Entities.Workflow> Workflows => Set<Domain.Entities.Workflow>();
        public DbSet<WorkflowStep> WorkflowSteps => Set<WorkflowStep>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Domain.Entities.Workflow>()
                .HasMany(w => w.Steps)
                .WithOne(s => s.Workflow)
                .HasForeignKey(s => s.WorkflowId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}

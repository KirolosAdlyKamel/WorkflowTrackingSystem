using Microsoft.EntityFrameworkCore;
using Workflow.Domain.Entities;

namespace Workflow.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Domain.Entities.Workflow> Workflows => Set<Domain.Entities.Workflow>();
    public DbSet<WorkflowStep> WorkflowSteps => Set<WorkflowStep>();
    public DbSet<Process> Processes => Set<Process>();
    public DbSet<ProcessStepExecution> ProcessExecutions => Set<ProcessStepExecution>();
    public DbSet<ValidationLog> ValidationLogs => Set<ValidationLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Domain.Entities.Workflow>(b =>
        {
            b.HasKey(w => w.Id);
            b.Property(w => w.Name).IsRequired();
            b.HasMany(w => w.Steps)
             .WithOne(s => s.Workflow)
             .HasForeignKey(s => s.WorkflowId)
             .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<WorkflowStep>(b =>
        {
            b.HasKey(s => s.Id);
            b.Property(s => s.StepName).IsRequired();
        });

        modelBuilder.Entity<Process>(b =>
        {
            b.HasKey(p => p.Id);
            b.HasMany(p => p.Executions)
             .WithOne(e => e.Process)
             .HasForeignKey(e => e.ProcessId)
             .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(p => p.Workflow)
             .WithMany()
             .HasForeignKey(p => p.WorkflowId)
             .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<ProcessStepExecution>(b =>
        {
            b.HasKey(e => e.Id);
            b.HasOne(e => e.WorkflowStep).WithMany().HasForeignKey(e => e.WorkflowStepId).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<ValidationLog>(b =>
        {
            b.HasKey(v => v.Id);
        });
    }
}

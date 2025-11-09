using Microsoft.EntityFrameworkCore;
using Workflow.Application.Interfaces;
using Workflow.Application.Services;
using Workflow.Infrastructure.Persistence;
using Workflow.Infrastructure.Repositories;
using Workflow.Infrastructure.Services;
using Workflow.Presentation.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext
var conn = builder.Configuration.GetConnectionString("DefaultConnection")
           ?? "Server=(localdb)\\MSSQLLocalDB;Database=WorkflowDb;Trusted_Connection=True;";
builder.Services.AddDbContext<AppDbContext>(opts => opts.UseSqlServer(conn));

// Repositories & services
builder.Services.AddScoped<IWorkflowRepository, WorkflowRepository>();
builder.Services.AddScoped<IProcessRepository, ProcessRepository>();
builder.Services.AddScoped<IValidationService, ExternalValidationSimulator>();

builder.Services.AddScoped<WorkflowService>();
builder.Services.AddScoped<ProcessService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseGlobalErrorHandler(); // global error handler
app.UseHttpsRedirection();
app.UseJsonMiddleware(); // custom JSON parser middleware if needed
app.UseAuthorization();
app.MapControllers();
app.Run();

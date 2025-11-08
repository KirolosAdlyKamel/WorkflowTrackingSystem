using Microsoft.EntityFrameworkCore;
using Workflow.Application.Interfaces;
using Workflow.Application.Services;
using Workflow.Infrastructure.Persistence;
using Workflow.Infrastructure.Repositories;
using Workflow.Infrastructure.Services;
using Workflow.Presentation.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Register DbContext
builder.Services.AddDbContext<AppDbContext>(opts => opts.UseSqlServer(connectionString));

// Register Services
builder.Services.AddScoped<IWorkflowRepository, WorkflowRepository>();
builder.Services.AddScoped<IValidationService, ValidationService>();
builder.Services.AddScoped<WorkflowService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseGlobalErrorHandler();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseJsonMiddleware();
app.MapControllers();
app.Run();

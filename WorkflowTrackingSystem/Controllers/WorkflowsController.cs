using Microsoft.AspNetCore.Mvc;
using Workflow.Application.DTOs;
using Workflow.Application.Services;
using Workflow.Domain.Entities;

namespace Workflow.Presentation.Controllers;

[ApiController]
[Route("v1/workflows")]
public class WorkflowsController : ControllerBase
{
    private readonly WorkflowService _workflowService;
    public WorkflowsController(WorkflowService workflowService) => _workflowService = workflowService;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateWorkflowDto dto)
    {
        var wf = new Domain.Entities.Workflow { Name = dto.Name, Description = dto.Description };
        foreach (var s in dto.Steps)
        {
            wf.Steps.Add(new WorkflowStep
            {
                StepName = s.StepName,
                AssignedTo = s.AssignedTo,
                ActionType = s.ActionType,
                NextStep = s.NextStep,
                RequiresValidation = s.RequiresValidation
            });
        }

        var created = await _workflowService.CreateAsync(wf);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var wf = await _workflowService.GetByIdAsync(id);
        if (wf == null) return NotFound();
        return Ok(wf);
    }
}

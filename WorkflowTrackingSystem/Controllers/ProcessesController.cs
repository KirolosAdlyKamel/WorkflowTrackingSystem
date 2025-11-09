using Microsoft.AspNetCore.Mvc;
using Workflow.Application.DTOs;
using Workflow.Application.Services;

namespace Workflow.Presentation.Controllers;

[ApiController]
[Route("v1/processes")]
public class ProcessesController : ControllerBase
{
    private readonly ProcessService _service;
    public ProcessesController(ProcessService service) => _service = service;

    [HttpPost("start")]
    public async Task<IActionResult> Start([FromBody] StartProcessDto dto)
    {
        var p = await _service.StartProcessAsync(dto.WorkflowId, dto.Initiator);
        return CreatedAtAction(nameof(Get), new { id = p.Id }, p);
    }

    [HttpPost("execute")]
    public async Task<IActionResult> Execute([FromBody] ExecuteStepDto dto)
    {
        try
        {
            await _service.ExecuteStepAsync(dto.ProcessId, dto.StepName, dto.PerformedBy, dto.Action);
            return Ok(new { message = "Step executed" });
        }
        catch (KeyNotFoundException knf) { return NotFound(new { error = knf.Message }); }
        catch (InvalidOperationException ioe) { return BadRequest(new { error = ioe.Message }); }
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int? workflow_id, [FromQuery] string? status, [FromQuery] string? assigned_to)
    {
        var list = await _service.QueryProcessesAsync(workflow_id, status, assigned_to);
        return Ok(list);
    }
}

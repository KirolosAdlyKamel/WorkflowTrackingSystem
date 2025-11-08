using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Workflow.Application.Services;

namespace Workflow.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Route("v1/workflows")]
    public class WorkflowsController : ControllerBase
    {
        private readonly WorkflowService _workflowService;
        public WorkflowsController(WorkflowService workflowService)
        {
            _workflowService = workflowService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Domain.Entities.Workflow workflow)
        {
            var result = await _workflowService.CreateAsync(workflow);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _workflowService.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost("{id:guid}/execute/{stepName}")]
        public async Task<IActionResult> Execute(Guid id, string stepName)
        {
            var result = await _workflowService.ExecuteStepAsync(id, stepName);
            if (!result) return BadRequest("Validation failed or step not found.");
            return Ok("Step executed successfully.");
        }
    }
}

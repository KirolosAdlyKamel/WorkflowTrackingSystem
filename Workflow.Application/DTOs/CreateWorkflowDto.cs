namespace Workflow.Application.DTOs;

public record CreateStepDto(string StepName, string AssignedTo, string ActionType, string NextStep, bool RequiresValidation = false);
public record CreateWorkflowDto(string Name, string? Description, List<CreateStepDto> Steps);

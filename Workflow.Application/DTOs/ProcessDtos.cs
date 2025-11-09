namespace Workflow.Application.DTOs;

public record StartProcessDto(int WorkflowId, string Initiator);
public record ExecuteStepDto(int ProcessId, string StepName, string PerformedBy, string Action);

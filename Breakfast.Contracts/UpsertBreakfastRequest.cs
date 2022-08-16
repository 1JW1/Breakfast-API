namespace Breakfast.Contracts.Breakfast;

public record UpsertBreakfastRequest(
    Guid Id,
    string Name,
    string Description,
    DateTime StartDateTime,
    DateTime LastDateTime,
    DateTime LastModifiedDateTime,
    List<string> Savoury,
    List<string> Sweet
);
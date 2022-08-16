namespace Breakfast.Contracts.Breakfast;

public record CreateBreakfastRequest(
    string Name,
    string Description,
    DateTime StartDateTime,
    DateTime LastDateTime,
    List<string> Savoury,
    List<string> Sweet
);
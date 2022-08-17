using Breakfast.Models;
using Breakfast.ServiceErrors;
using ErrorOr;

namespace Breakfast.Services.Breakfasts;

public class BreakfastService : IBreakfastService
{
    private static readonly Dictionary<Guid, ABreakfast> _breakfasts = new();
    
    public void CreateBreakfast(ABreakfast aBreakfast)
    {
        _breakfasts.Add(aBreakfast.Id, aBreakfast);
    }

    public void DeleteBreakfast(Guid id)
    {
        _breakfasts.Remove(id);
    }

    public ErrorOr<ABreakfast> GetBreakfast(Guid id)
    {
        if(_breakfasts.TryGetValue(id, out var breakfast))
        {
            return breakfast;
        }

        return Errors.Breakfast.NotFound;
    }

    public void UpsertBreakfast(ABreakfast breakfast)
    {
        // adding breakfast to dictionary
        _breakfasts[breakfast.Id] = breakfast;
    }
}
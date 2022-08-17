using Breakfast.Models;
using Breakfast.ServiceErrors;
using ErrorOr;

namespace Breakfast.Services.Breakfasts;

public class BreakfastService : IBreakfastService
{
    private static readonly Dictionary<Guid, ABreakfast> _breakfasts = new();
    
    public ErrorOr<Created> CreateBreakfast(ABreakfast aBreakfast)
    {
        _breakfasts.Add(aBreakfast.Id, aBreakfast);

        return Result.Created;
    }

    public ErrorOr<Deleted> DeleteBreakfast(Guid id)
    {
        _breakfasts.Remove(id);

        return Result.Deleted;
    }

    public ErrorOr<ABreakfast> GetBreakfast(Guid id)
    {
        if(_breakfasts.TryGetValue(id, out var breakfast))
        {
            return breakfast;
        }

        return Errors.Breakfast.NotFound;
    }

    public ErrorOr<UpsertedBreakfast> UpsertBreakfast(ABreakfast breakfast)
    {   
        var isNewlyCreated = !_breakfasts.ContainsKey(breakfast.Id);
        // adding breakfast to dictionary
        _breakfasts[breakfast.Id] = breakfast;

        return new UpsertedBreakfast(isNewlyCreated);
    }
}
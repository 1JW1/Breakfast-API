using Breakfast.Models;

namespace Breakfast.Services.Breakfasts;

public class BreakfastService : IBreakfastService
{
    private static readonly Dictionary<Guid, ABreakfast> _breakfasts = new();
    
    public void CreateBreakfast(ABreakfast aBreakfast)
    {
        _breakfasts.Add(aBreakfast.Id, aBreakfast);
    }

    public ABreakfast GetBreakfast(Guid id)
    {
        return _breakfasts[id];
    }
}
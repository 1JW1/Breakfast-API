using Breakfast.Models;

namespace Breakfast.Services.Breakfasts;

public interface IBreakfastService
{
    void CreateBreakfast(ABreakfast aBreakfast);
    ABreakfast GetBreakfast(Guid id);
    void UpsertBreakfast(ABreakfast breakfast);
    void DeleteBreakfast(Guid id);
}
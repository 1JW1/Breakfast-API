using Breakfast.Models;
using ErrorOr;

namespace Breakfast.Services.Breakfasts;

public interface IBreakfastService
{
    void CreateBreakfast(ABreakfast aBreakfast);
    ErrorOr<ABreakfast> GetBreakfast(Guid id);
    void UpsertBreakfast(ABreakfast breakfast);
    void DeleteBreakfast(Guid id);
}
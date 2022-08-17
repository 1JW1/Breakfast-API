using Breakfast.Models;
using ErrorOr;

namespace Breakfast.Services.Breakfasts;

public interface IBreakfastService
{
    ErrorOr<Created> CreateBreakfast(ABreakfast aBreakfast);
    ErrorOr<ABreakfast> GetBreakfast(Guid id);
    ErrorOr<UpsertedBreakfast> UpsertBreakfast(ABreakfast breakfast);
    ErrorOr<Deleted> DeleteBreakfast(Guid id);
}
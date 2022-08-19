using ErrorOr;

namespace Breakfast.ServiceErrors;

// all errors expected in our system defined here with unique error codes 
public static class Errors
{
    public static class Breakfast
    {   
        public static Error InvalidDescription => Error.Validation(
            code: "Breakfast.InvalidName",
            description: $"Breakfast description must be at least {Models.ABreakfast.MinDescriptionLength} characters long and at most {Models.ABreakfast.MaxDescriptionLength} characters long."
        );
        public static Error InvalidName => Error.Validation(
            code: "Breakfast.InvalidName",
            description: $"Breakfast name must be at least {Models.ABreakfast.MinNameLength} characters long and at most {Models.ABreakfast.MaxNameLength} characters long."
        ); 
        public static Error NotFound => Error.NotFound(
            code: "Breakfast.NotFound",
            description: "Breakfast not found."
        ); 
    };
}
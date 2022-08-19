using ErrorOr;

namespace Breakfast.ServiceErrors;

// all errors expected in our system defined here with unique error codes 
public static class Errors
{
    public static class Breakfast
    {
        public static Error NotFound => Error.NotFound(
            code: "Breakfast.NotFound",
            description: "Breakfast not found."
        ); 
    };
}
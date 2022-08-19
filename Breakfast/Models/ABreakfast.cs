using Breakfast.ServiceErrors;
using ErrorOr;

namespace Breakfast.Models;

public class ABreakfast
{   
    public const int MinNameLength = 3;
    public const int MaxNameLength = 50;

    public const int MinDescriptionLength = 50;
    public const int MaxDescriptionLength = 150;

    public Guid Id { get;  }
    public string Name {get; }
    public string Description {get; }
    public DateTime StartDateTime {get; }
    public DateTime EndDateTime {get; }
    public DateTime LastModifiedDateTime {get; }
    public List<string> Savoury {get; }
    public List<string> Sweet {get; }

    // constructor
    private ABreakfast(
        Guid id, 
        string name, 
        string description, 
        DateTime startDateTime, 
        DateTime endDateTime, 
        DateTime lastModifiedTime,
        List<string> savoury, 
        List<string> sweet)
{       
        Id = id;
        Name = name;
        Description = description;
        StartDateTime = startDateTime;
        EndDateTime = endDateTime;
        LastModifiedDateTime = lastModifiedTime;
        Savoury = savoury;
        Sweet = sweet;
}
        // ensures theres no other way to create a breakfast apart from using this static method
        public static ErrorOr<ABreakfast> Create(
            string name, 
            string description, 
            DateTime startDateTime, 
            DateTime endDateTime, 
            DateTime lastModifiedTime,
            List<string> savoury, 
            List<string> sweet,
            Guid? id = null
        )
        {  
          
          List<Error> errors = new();

          // enforcing business rules related to breakfast object

          if (name.Length is < MinNameLength or > MaxNameLength)
          {
            errors.Add(Errors.Breakfast.InvalidName);
          }

          if (description.Length is < MinDescriptionLength or > MaxDescriptionLength)
          {
            errors.Add(Errors.Breakfast.InvalidDescription);
          }

          if (errors.Count > 0)
          {
            // implicit converter that takes errors and returns an errorOr object
            return errors;
          }
          
           //enforce variants 
           return new ABreakfast(
           id ?? Guid.NewGuid(), // generates new id if its not specified
           name,
           description,
           startDateTime,
           endDateTime,
           DateTime.UtcNow,
           savoury,
           sweet
           );
        }

}


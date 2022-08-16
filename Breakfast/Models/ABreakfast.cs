namespace Breakfast.Models;

public class ABreakfast
{
    public Guid Id { get;  }
    public string Name {get; }
    public string Description {get; }
    public DateTime StartDateTime {get; }
    public DateTime EndDateTime {get; }
    public DateTime LastModifiedDateTime {get; }
    public List<string> Savoury {get; }
    public List<string> Sweet {get; }

    // constructor
    public ABreakfast(
        Guid id, 
        string name, 
        string description, 
        DateTime startDateTime, 
        DateTime endDateTime, 
        DateTime lastModifiedTime,
        List<string> savoury, 
        List<string> sweet)
{       
        // enforce invariants
        Id = id;
        Name = name;
        Description = description;
        StartDateTime = startDateTime;
        EndDateTime = endDateTime;
        LastModifiedDateTime = lastModifiedTime;
        Savoury = savoury;
        Sweet = sweet;
}

}


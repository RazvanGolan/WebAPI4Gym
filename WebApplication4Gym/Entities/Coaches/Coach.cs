using WebApplication4Gym.Entities.Members;

namespace WebApplication4Gym.Entities.Coaches;

public class Coach : Entity
{
    public string FirstName { get; private set; }

    public string LastName { get; private set; }
    
    public DateTime Created { get; private set; }

    public List<Member> MemberList { get; set; } = new ();

    public int Limit { get; private set; }

    private Coach()
    {
        
    }

    public static async Task<Coach> CreateAsync(string firstname, string lastname, string date, int limit)
    {
        if (string.IsNullOrWhiteSpace(firstname))
            throw new Exception("First name can't be empty.");

        if (string.IsNullOrWhiteSpace(lastname))
            throw new Exception("last name can't be empty.");
        
        if (string.IsNullOrWhiteSpace(date))
            throw new Exception("date can't be empty.");
        
        DateTime parsed;
        string[] formats = { "dd/MM/yyyy", "dd/M/yyyy", "d/M/yyyy", "d/MM/yyyy",
            "dd/MM/yy", "dd/M/yy", "d/M/yy", "d/MM/yy", "dd-MM-yyyy", "dd-M-yyyy", "d-M-yyyy", "d-MM-yyyy",
            "dd-MM-yy", "dd-M-yy", "d-M-yy", "d-MM-yy"};
        
        if(!DateTime.TryParseExact(date, formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out parsed))
            throw new Exception("incorrect date format");

        if (limit <= 0)
            throw new Exception("Incorect limit format");
            
        return new Coach
        {
            FirstName = firstname,
            LastName = lastname,
            Created = parsed,
            Limit = limit
        };
    }
    
    public void SetFirstName(string firstName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new Exception("First Name can't be empty");

        firstName = firstName.Replace(" ", "");

        FirstName = firstName;
    }

    public void SetLastName(string lastName)
    {
        if (string.IsNullOrWhiteSpace(lastName))
            throw new Exception("Last Name can't be empty");

        LastName = lastName;
    }
    
    public void SetCreated(string date)
    {
        if (string.IsNullOrWhiteSpace(date))
            throw new Exception("Date can't be empty");

        DateTime parse;
        string[] formats = { "dd/MM/yyyy", "dd/M/yyyy", "d/M/yyyy", "d/MM/yyyy",
            "dd/MM/yy", "dd/M/yy", "d/M/yy", "d/MM/yy", "dd-MM-yyyy", "dd-M-yyyy", "d-M-yyyy", "d-MM-yyyy",
            "dd-MM-yy", "dd-M-yy", "d-M-yy", "d-MM-yy"};
        if(!DateTime.TryParseExact(date, formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out parse))
            throw new Exception("incorrect date format");
        
        Created = parse;
    }

    public int GetLimit()
    {
        return Limit;
    }

    public void AddMember(Member member)
    {
        if (Limit == MemberList.Count)
            throw new Exception($"Coach with id: {Id} has too many members");
        
        MemberList.Add(member);
        
    }
    
}
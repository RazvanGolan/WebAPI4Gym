using System.Text.RegularExpressions;
using WebApplication4Gym.Entities.Coaches;

namespace WebApplication4Gym.Entities.Members;

public class Member : Entity
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public DateTime Created { get; private set; }
    public bool GoldenState { get; private set; }
    public string Email { get; private set; }
    public Coach Coach { get;  set; }
    
    private Member()
    {
        
    }

    public static async Task<Member> CreateAsync(IMemberRepository _repository, string firstname, string lastname, string date, Coach coach, string email)
    {
        
        if (string.IsNullOrWhiteSpace(firstname))
            throw new Exception("First name can't be empty.");

        if (string.IsNullOrWhiteSpace(lastname))
            throw new Exception("last name can't be empty.");
        
        if (string.IsNullOrWhiteSpace(date))
            throw new Exception("date can't be empty.");
        
        DateTime parsed;
        string[] formats = { "dd/MM/yyyy", "dd/M/yyyy", "d/M/yyyy", "d/MM/yyyy",
            "dd/MM/yy", "dd/M/yy", "d/M/yy", "d/MM/yy", "dd-MM-yyyy"};
        
        if(!DateTime.TryParseExact(date, formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out parsed))
            throw new Exception("incorrect date format");
        
        if (string.IsNullOrWhiteSpace(email))
            throw new Exception("date can't be empty.");
        else
        {
            string pattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";
            Regex regex = new Regex(pattern);
            if (!regex.IsMatch(email))
                throw new Exception("Email is not valid");
        }

        if (!await _repository.IsEmailUnique(email))
            throw new Exception("this email is already used by another member");
        
        Member x = new Member
        {
            FirstName = firstname,
            LastName = lastname,
            Created = parsed,
            GoldenState = false,
            Coach = coach, 
            Email = email
        };
        //getting total number of days
        var totalDays = (DateTime.Now - x.Created).Days;

        if (totalDays >= 90)
            x.GoldenState = true;
        
        return x;
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

    public void SetEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new Exception("date can't be empty.");
        else
        {
            string pattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";
            Regex regex = new Regex(pattern);
            if (!regex.IsMatch(email))
                throw new Exception("Email is not valid");
        }

        Email = email;
    }
    public void SetCreated(string date)
    {
        if (string.IsNullOrWhiteSpace(date))
            throw new Exception("Last Name can't be empty");

        DateTime parse;
        string[] formats = { "dd/MM/yyyy", "dd/M/yyyy", "d/M/yyyy", "d/MM/yyyy",
            "dd/MM/yy", "dd/M/yy", "d/M/yy", "d/MM/yy", "dd-MM-yyyy", "dd-M-yyyy", "d-M-yyyy", "d-MM-yyyy",
            "dd-MM-yy", "dd-M-yy", "d-M-yy", "d-MM-yy"};
        if(!DateTime.TryParseExact(date, formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out parse))
            throw new Exception("incorrect date format");
        
        Created = parse;
    }

    public void setCoach(Coach coach)
    {
        Coach = coach;
    }
    

}
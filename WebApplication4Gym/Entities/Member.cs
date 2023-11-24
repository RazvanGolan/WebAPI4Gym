using System.Globalization;
using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Http.HttpResults;

namespace WebApplication4Gym.Entities;

public class Member : Entity
{
    //public string Id { get; private set; }
    
    public string FirstName { get; private set; }

    public string LastName { get; private set; }
    public DateTime Created { get; private set; }
    public bool GoldenState { get; private set; }
    private Member()
    {
        
    }

    public static Member Create(string firstname, string lastname, string date)
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
        
        Member x = new Member
        {
            FirstName = firstname,
            LastName = lastname,
            Created = parsed,
            GoldenState = false
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
    
}
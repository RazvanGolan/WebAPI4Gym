using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Http.HttpResults;

namespace WebApplication4Gym.Entities;

public class Member : Entity
{
    //public string Id { get; private set; }
    
    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public int Days { get; private set; }
    
    public bool GoldenState { get; private set; }
    private Member()
    {
        
    }

    public static Member Create(string firstname, string lastname)
    {
        if (string.IsNullOrWhiteSpace(firstname))
            throw new Exception("First name can't be empty.");

        if (string.IsNullOrWhiteSpace(lastname))
            throw new Exception("last name can't be empty.");

        Member x = new Member
        {
            FirstName = firstname,
            LastName = lastname,
            Days = (int)(DateTime.Now.AddMonths(Random.Shared.Next(0,12)) - DateTime.Now).TotalDays,
            GoldenState = false
        };
        
        x.GoldenMember();
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

    public void GoldenMember()
    {
        if (Days >= 90)
        {
            GoldenState = true;
        }
    }
}
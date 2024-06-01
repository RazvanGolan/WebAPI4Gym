namespace WebApplication4Gym.Controllers.Members;

public class MemberResponse
{
    public string Id { get; set; }
    public string FirstName { get;  set; }
    public string LastName { get;  set; }
    public DateTime Created { get;  set; }
    public bool GoldenState { get;  set; }
    public string Email { get;  set; }
    public string? CoachId { get;  set; } // nu e obligatoriu
}
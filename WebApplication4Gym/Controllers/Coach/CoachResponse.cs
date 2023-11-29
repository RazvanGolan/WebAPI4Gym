using WebApplication4Gym.Controllers.Member;

namespace WebApplication4Gym.Controllers.Coach;

public class CoachResponse
{
    public string Id { get; set; }
    public string FirstName { get;  set; }

    public string LastName { get;  set; }
    
    public DateTime Created { get;  set; }

    public List<MemberResponse> Members { get; set; } 
}
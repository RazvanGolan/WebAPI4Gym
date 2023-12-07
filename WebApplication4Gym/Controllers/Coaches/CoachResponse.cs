using WebApplication4Gym.Controllers.Members;

namespace WebApplication4Gym.Controllers.Coaches;

public class CoachResponse
{
    public string Id { get; set; }
    public string FirstName { get;  set; }

    public string LastName { get;  set; }
    
    public DateTime Created { get;  set; }
    public int Limit { get; set; }
    public List<MemberResponse> Members { get; set; }
}
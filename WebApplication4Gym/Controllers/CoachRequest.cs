using WebApplication4Gym.Entities;

namespace WebApplication4Gym.Controllers;

public record CoachRequest(string FirstName, string LastName, string Date, List<Member> Members);
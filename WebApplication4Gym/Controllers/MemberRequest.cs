using WebApplication4Gym.Entities;

namespace WebApplication4Gym.Controllers;

public record MemberRequest(string FirstName, string LastName, string Date, string email); 
//DTO din frontend
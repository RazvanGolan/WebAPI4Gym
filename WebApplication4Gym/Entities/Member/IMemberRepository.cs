namespace WebApplication4Gym.Entities.Member;

public interface IMemberRepository
{
    Task<bool> IsEmailUnique(string email);
    
}
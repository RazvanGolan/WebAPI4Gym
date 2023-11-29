namespace WebApplication4Gym.Entities.Members;

public interface IMemberRepository
{
    Task<bool> IsEmailUnique(string email);
    
}
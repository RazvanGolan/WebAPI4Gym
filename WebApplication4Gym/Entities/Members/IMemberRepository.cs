using Microsoft.AspNetCore.Mvc;

namespace WebApplication4Gym.Entities.Members;

public interface IMemberRepository
{
    Task<bool> IsEmailUnique(string email); 
    Task<List<Member>> GetAllAsync();
    Task<Member> GetByIdAsync(string id);
    Task DeleteMember(string id);
    Task SaveAsync();
    Task Create(Member member);
    
}
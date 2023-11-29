using Microsoft.EntityFrameworkCore;

namespace WebApplication4Gym.Repository;

public class MemberRepository : IMemberRepository
{

    private readonly AppDBContext _dbcontext;

    public MemberRepository(AppDBContext context)
    {
        _dbcontext = context;
    }
    
    public async Task<bool> IsEmailUnique(string email)
    {
        return await _dbcontext.Members.AnyAsync(m => m.Email == email);
    }
}
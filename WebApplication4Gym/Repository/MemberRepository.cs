using Microsoft.EntityFrameworkCore;
using WebApplication4Gym.Entities;
using WebApplication4Gym.Entities.Member;

namespace WebApplication4Gym.Repository;

public class MemberRepository : IMemberRepository
{
    private readonly AppDBContext _context;

    public MemberRepository(AppDBContext context)
    {
        _context = context;
    }

    public async Task<bool> IsEmailUnique(string email)
    {
        return !await _context.Members.AnyAsync(m => m.Email == email);
    }
}
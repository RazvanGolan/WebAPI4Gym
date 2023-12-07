using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication4Gym.Entities;
using WebApplication4Gym.Entities.Members;

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

    public async Task<List<Member>> GetAllAsync()
    {
        var members = await _context.Set<Member>()
            .Include(m=>m.Coach)
            .ToListAsync();

        return members;
    }

    public async Task<Member> GetByIdAsync(string id)
    {
        var member = await _context.Members.Where(members => members.Id == id)
            .Include(member => member.Coach)
            .FirstOrDefaultAsync();

        if (member is null)
        {
            throw new Exception($"Member with Id: {id} doesnt exist bro");
        }
        
        return member;
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task Create(Member member)
    {
        _context.Add(member); // nu da si save dupa ce adauga unul
    }

    public async Task DeleteMember(string id)
    {
        var member = await _context.Members.Where(members => members.Id == id)
            .Include(member => member.Coach)
            .FirstOrDefaultAsync();
        
        if (member is null)
        {
            throw new Exception($"Member with Id: {id} doesn't exist bro");
        }
        
        _context.Remove(member);
        await SaveAsync();
    }

}
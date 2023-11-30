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

    public async Task<Member> Create(string firstname, string lastname, string date, string email)
    {
        if (string.IsNullOrWhiteSpace(firstname))
            throw new Exception("First name can't be empty.");

        if (string.IsNullOrWhiteSpace(lastname))
            throw new Exception("last name can't be empty.");
        
        if (string.IsNullOrWhiteSpace(date))
            throw new Exception("date can't be empty.");
        
        DateTime parsed;
        string[] formats = { "dd/MM/yyyy", "dd/M/yyyy", "d/M/yyyy", "d/MM/yyyy",
            "dd/MM/yy", "dd/M/yy", "d/M/yy", "d/MM/yy", "dd-MM-yyyy"};
        
        if(!DateTime.TryParseExact(date, formats, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out parsed))
            throw new Exception("incorrect date format");
        
        if (string.IsNullOrWhiteSpace(email))
            throw new Exception("date can't be empty.");
        else
        {
            string pattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";
            Regex regex = new Regex(pattern);
            if (!regex.IsMatch(email))
                throw new Exception("Email is not valid");
        }

        if (!await IsEmailUnique(email))
            throw new Exception("this email is already used by another member");

        Member x = Member.CreateAsync(firstname, lastname, parsed, email);
        
        //getting total number of days
        var totalDays = (DateTime.Now - x.Created).Days;

        if (totalDays >= 90)
            x.SetGoldenState();

        _context.Add(x);
        await SaveAsync();
        
        return x;
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
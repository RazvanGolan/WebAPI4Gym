using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication4Gym.Entities.Members;
using WebApplication4Gym.Repository;

namespace WebApplication4Gym.Controllers.Members;

[Route("api/[controller]")]
[ApiController]

public class MemberController : ControllerBase
{

    private readonly AppDBContext _dbContext; //pentru readonly pune _
    private readonly IMemberRepository _repository;
    public MemberController(AppDBContext dbContext, IMemberRepository repository)
    {
        _dbContext = dbContext;
        _repository = repository;
    }

    [HttpGet(Name = "GetAllMembers")]
    public async Task<ActionResult> GetMembers()
    {
        var members = await _dbContext.Set<Member>()
            .Include(m=>m.Coach)
            .ToListAsync();
        
        return Ok(members.Select(m=>new MemberResponse
        {
            FirstName = m.FirstName,
            LastName = m.LastName,
            Id = m.Id,
            Email = m.Email,
            GoldenState = m.GoldenState,
            Created = m.Created,
            CoachId = m.Coach?.Id
        }));
    }

    [HttpGet(template: "{Id}")]
    public async Task<ActionResult> GetMembers(string Id)
    {
        var member = await _dbContext.Members.Where(members => members.Id == Id)
            .Include(member => member.Coach)
            .FirstOrDefaultAsync();

        if (member is null)
        {
            return NotFound($"Member with Id: {Id} doesnt exist bro");
        }
        
        return Ok(new MemberResponse
        {
            Id = member.Id,
            FirstName = member.FirstName,
            LastName = member.LastName,
            Created = member.Created,
            GoldenState = member.GoldenState,
            Email = member.Email,
            CoachId = member.Coach?.Id
        });
    }

    [HttpPost]
    public async Task<ActionResult> CreateMember([FromBody] MemberRequest memberRequest)
    {
        Member member = null;

        //var coach = await _dbContext.Coaches.FirstOrDefaultAsync(c => c.Id == memberRequest.CoachId);
        
        try
        {
            member = await Member.CreateAsync(_repository, memberRequest.FirstName, memberRequest.LastName, memberRequest.Date, memberRequest.Email);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        _dbContext.Add(member);
        await _dbContext.SaveChangesAsync();
        return Ok(new MemberResponse
        {
            Id = member.Id,
            FirstName = member.FirstName,
            LastName = member.LastName,
            Created = member.Created,
            GoldenState = member.GoldenState,
            Email = member.Email,
            CoachId = null
        });
    }
    
    
    [HttpDelete(template: "{Id}")]
    public async Task<ActionResult> DeleteMember(string Id)
    {
        var member = await _dbContext.Members
            .Include(member => member.Coach)
            .FirstOrDefaultAsync(m => m.Id == Id);
            

        if (member is null)
            return NotFound($"Member with Id: {Id} doesnt exis broteher");

        _dbContext.Remove(member);
        await _dbContext.SaveChangesAsync();
        return Ok($"All good, {Id} is no longer along us");
    }
    
    
    [HttpDelete(template: "DeleteCoach/{Id}")]
    public async Task<ActionResult> DeleteMemberCoach(string Id)
    {
        var member = _dbContext.Members
            .Include(member => member.Coach)
            .FirstOrDefault(m => m.Id == Id);

        if (member is null)
            return NotFound($"Member with Id: {Id} doesnt exis broteher");

        if (member.Coach is null)
            return BadRequest($"Member with Id: {Id} doesn't have a coach");

        member.Coach = null;
        
        await _dbContext.SaveChangesAsync();
        return Ok($"All good, {member.FirstName}'s coach is no longer along us");
    }
    
    [HttpPatch(template: "PatchFirstName/{Id}")]
    public async Task<ActionResult> UpdateMemberFirstName(string Id, [FromBody] string firstName)
    {
        var member = await _dbContext.Members
            .Include(member => member.Coach)
            .FirstOrDefaultAsync(m => m.Id == Id);

        if (member is null)
            return NotFound($"Member with Id: {Id} doesnt exis broteher");
        
        try
        {
            member.SetFirstName(firstName);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        await _dbContext.SaveChangesAsync();
        return Ok(new MemberResponse
        {
            Id = member.Id,
            FirstName = member.FirstName,
            LastName = member.LastName,
            Created = member.Created,
            GoldenState = member.GoldenState,
            Email = member.Email,
            CoachId = member.Coach?.Id
        });
    } 
    
    [HttpPatch(template: "PatchCoach/{Id}")]
    public async Task<ActionResult> UpdateMemberCoach(string Id, [FromBody] string coachId)
    {
        var member = await _dbContext.Members.FirstOrDefaultAsync(m => m.Id == Id);
        var coach = await _dbContext.Coaches.FirstOrDefaultAsync(c => c.Id == coachId);
        
        if (member is null)
            return NotFound($"Member with Id: {Id} doesnt exist brother");
        
        if (coach is null)
            return NotFound($"Coach with Id: {coachId} doesnt exist brother");
        
        try
        {
            member.Coach = coach;
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        await _dbContext.SaveChangesAsync();
        return Ok(new MemberResponse
        {
            Id = member.Id, 
            FirstName = member.FirstName, 
            LastName = member.LastName,
            Created = member.Created,
            GoldenState = member.GoldenState,
            Email = member.Email,
            CoachId = coach.Id
        });

    }
    
    [HttpPut(template: "{Id}")]
    public async Task<ActionResult> UpdateMember(string Id, [FromBody] MemberRequest memberRequest)
    {        
        var member = await _dbContext.Members
            .Include(member => member.Coach)
            .FirstOrDefaultAsync(m => m.Id == Id);

        if (member is null)
            return NotFound($"Member with Id: {Id} doesnt exist brother");
        
        try
        {
            member.SetFirstName(memberRequest.FirstName);
            member.SetLastName(memberRequest.LastName);
            member.SetCreated(memberRequest.Date);
            member.SetEmail(memberRequest.Email);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        await _dbContext.SaveChangesAsync();
        return Ok(new MemberResponse
        {
            Id = member.Id, 
            FirstName = member.FirstName, 
            LastName = member.LastName,
            Created = member.Created,
            GoldenState = member.GoldenState,
            Email = member.Email,
            CoachId = member.Coach?.Id
        });
    }
}
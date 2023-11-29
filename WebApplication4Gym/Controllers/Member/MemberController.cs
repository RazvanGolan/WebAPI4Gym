using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication4Gym.Entities;
using WebApplication4Gym.Entities.Member;
using WebApplication4Gym.Repository;

namespace WebApplication4Gym.Controllers.Member;

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
        var members = await _dbContext.Set<Entities.Member.Member>().ToListAsync();
        
        return Ok(members);
    }

    [HttpGet(template: "{Id}")]
    public async Task<ActionResult> GetMembers(string Id)
    {
        var member = await _dbContext.Members.Where(members => members.Id == Id).FirstOrDefaultAsync();

        if (member is null)
        {
            return NotFound($"Member with Id: {Id} doesnt exist bro");
        }
        
        return Ok(member);
    }

    [HttpPost]
    public async Task<ActionResult> CreateMember([FromBody] MemberRequest memberRequest)
    {
        Entities.Member.Member member = null;

        var coach = await _dbContext.Coaches.FirstOrDefaultAsync(c => c.Id == memberRequest.CoachId);
        
        try
        {
            member = await Entities.Member.Member.CreateAsync(_repository, memberRequest.FirstName, memberRequest.LastName, memberRequest.Date, coach, memberRequest.Email);
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
            CoachId = coach.Id
        });
    }
    
    
    [HttpDelete(template: "{Id}")]
    public async Task<ActionResult> DeleteMember(string Id)
    {
        var member = await _dbContext.Members.FirstOrDefaultAsync(m => m.Id == Id);

        if (member is null)
            return NotFound($"Member with Id: {Id} doesnt exis broteher");

        _dbContext.Remove(member);
        await _dbContext.SaveChangesAsync();
        return Ok($"All good, {Id} is no longer along us");
    }
    
    /*
    [HttpDelete(template: "{Id}")]
    public ActionResult DeleteMemberCoach(string Id)
    {
        var member = _dbContext.Members.FirstOrDefault(m => m.Id == Id);

        if (member is null)
            return NotFound($"Member with Id: {Id} doesnt exis broteher");

        if (member.Coach is null)
            return BadRequest($"Member with Id: {Id} doesn't have a coach");

        member.Coach = null;

        _dbContext.Remove(member);
        _dbContext.SaveChanges();
        return Ok($"All good, {member.FirstName}'s member is no longer along us");
    }
    */
    [HttpPatch(template: "{Id}")]
    public async Task<ActionResult> UpdateMemberFirstName(string Id, [FromBody] string firstName)
    {
        var member = await _dbContext.Members.FirstOrDefaultAsync(m => m.Id == Id);

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
        return Ok(member);
    } 
    /*
    [HttpPatch(template: "{Id}")]
    public ActionResult UpdateMemberCoach(string Id, [FromBody] Coach coach)
    {
        var member = _dbContext.Members.FirstOrDefault(m => m.Id == Id);

        if (member is null)
            return NotFound($"Member with Id: {Id} doesnt exis broteher");
        
        try
        {
            member.setCoach(coach);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        _dbContext.SaveChanges();
        return Ok(member);
    }
    */
    [HttpPut(template: "{Id}")]
    public async Task<ActionResult> UpdateMember(string Id, [FromBody] MemberRequest memberRequest)
    {        
        var member = await _dbContext.Members.FirstOrDefaultAsync(m => m.Id == Id);

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
        return Ok(member);
    }
}
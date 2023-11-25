using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication4Gym.Entities;
using WebApplication4Gym.Repository;

namespace WebApplication4Gym.Controllers;

[Route("api/[controller]")]
[ApiController]

public class MemberController : ControllerBase
{

    private readonly AppDBContext _dbContext; //pentru readonly pune _

    public MemberController(AppDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet(Name = "GetAllMembers")]
    public ActionResult GetMembers()
    {
        var members = _dbContext.Set<Member>().ToList();
        
        return Ok(members);
    }

    [HttpGet(template: "{Id}")]
    public ActionResult GetMembers(string Id)
    {
        var member = _dbContext.Members.Where(members => members.Id == Id).FirstOrDefault();

        if (member is null)
        {
            return NotFound($"Member with Id: {Id} doesnt exist bro");
        }

        return Ok(member);
    }

    [HttpPost]
    public ActionResult CreateMember([FromBody] MemberRequest memberRequest)
    {
        Member member = null;
        
        try
        {
            member = Member.Create(memberRequest.FirstName, memberRequest.LastName, memberRequest.Date, memberRequest.Coach);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        _dbContext.Add(member);
        _dbContext.SaveChanges();
        return Ok(member);
    }
    
    /*
    [HttpDelete(template: "{Id}")]
    public ActionResult DeleteMember(string Id)
    {
        var member = _dbContext.Members.FirstOrDefault(m => m.Id == Id);

        if (member is null)
            return NotFound($"Member with Id: {Id} doesnt exis broteher");

        _dbContext.Remove(member);
        _dbContext.SaveChanges();
        return Ok($"All good, {Id} is no longer along us");
    }
    */

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
    
    /*
    [HttpPatch(template: "{Id}")]
    public ActionResult UpdateMemberFirstName(string Id, [FromBody] string firstName)
    {
        var member = _dbContext.Members.FirstOrDefault(m => m.Id == Id);

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

        _dbContext.SaveChanges();
        return Ok(member);
    } */
    
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

    [HttpPut(template: "{Id}")]
    public ActionResult UpdateMember(string Id, [FromBody] MemberRequest memberRequest)
    {        
        var member = _dbContext.Members.FirstOrDefault(m => m.Id == Id);

        if (member is null)
            return NotFound($"Member with Id: {Id} doesnt exist brother");
        
        try
        {
            member.SetFirstName(memberRequest.FirstName);
            member.SetLastName(memberRequest.LastName);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        _dbContext.SaveChanges();
        return Ok(member);
    }
}
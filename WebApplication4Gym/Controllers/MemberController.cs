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
    private readonly IMemberRepository _repo;
    
    /*
    private static readonly List<Member> _list = new()
    {
        Member.Create("Razvan", "Golan"),
        Member.Create("denis", "Beleaua")
    }; */

    public MemberController(AppDBContext dbContext, IMemberRepository repo)
    {
        _dbContext = dbContext;
        _repo = repo;
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
    public async Task<ActionResult> CreateMember([FromBody] MemberRequest memberRequest)
    {
        Member member = null;

        try
        {
            member = await Member.CreateAsync(_repo, memberRequest.FirstName, memberRequest.LastName, memberRequest.Date, memberRequest.email);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        _dbContext.Add(member);
        _dbContext.SaveChangesAsync();
        return Ok(member);
    }

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
            member.SetEmail(memberRequest.email);
            
            if(memberRequest.Date is not "string")
                member.SetCreated(memberRequest.Date);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        _dbContext.SaveChanges();
        return Ok(member);
    }
}
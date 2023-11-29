using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication4Gym.Controllers.Member;
using WebApplication4Gym.Repository;

namespace WebApplication4Gym.Controllers.Coach;

[Route("api/[controller]")]
[ApiController]

public class CoachController : ControllerBase
{
    private readonly AppDBContext _dbContext;
    
    public CoachController(AppDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet(Name = "GetAllCoaches")]
    public async Task<ActionResult<IEnumerable<CoachResponse>>> GetCoaches()
    {
        var coaches = await _dbContext.Coaches
            .Include(c => c.MemberList)
            .ToListAsync();

        return Ok(coaches.Select(c=>new CoachResponse
        {
            Id = c.Id,
            FirstName = c.FirstName,
            LastName = c.LastName,
            Created = c.Created,
            Members = c.MemberList.Select(m=> new MemberResponse
            {
                Id = m.Id,
                FirstName = m.FirstName,
                LastName = m.LastName,
                Created = m.Created,
                GoldenState = m.GoldenState,
                Email = m.Email,
                CoachId = c.Id
            }).ToList()
        }));

    }

    /*
    [HttpGet(template: "{Id}")]
    public ActionResult GetCoach(string Id)
    {
        var coach = _dbContext.Coaches.FirstOrDefault(c => c.Id == Id);

        if (coach is null)
            return NotFound($"Coach with {Id} doesn't exist");

        return Ok(coach);
    }
    */
    [HttpPost]
    public async Task<ActionResult> AddCoach([FromBody] CoachRequest coachRequest)
    {
        Entities.Coach.Coach coach = null;
        try
        {
            coach = await Entities.Coach.Coach.CreateAsync(coachRequest.FirstName, coachRequest.LastName, coachRequest.Date);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        _dbContext.Add(coach);
        await _dbContext.SaveChangesAsync();

        return Ok(coach);
    }

    [HttpDelete(template: "{Id}")]
    public ActionResult DeleteCoach(string Id)
    {
        var coach = _dbContext.Coaches.FirstOrDefault(c => c.Id == Id);
        
        if (coach is null)
            return NotFound($"Coach with {Id} doesn't exist");

        _dbContext.Remove(coach);
        _dbContext.SaveChanges();
        
        return Ok($"All good, {Id} is no longer along us");
    }
    /*
    [HttpPatch(template: "{Id}")]
    public ActionResult UpdateCoachFirstName(string Id, [FromBody] string firstName)
    {
        var coach = _dbContext.Coaches.FirstOrDefault(c => c.Id == Id);
        
        if (coach is null)
            return NotFound($"Coach with {Id} doesn't exist");
        
        try
        {
            coach.SetFirstName(firstName);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        _dbContext.SaveChanges();
        return Ok(coach);
    }
    */
    
    [HttpPatch(template: "{Id}")]
    public ActionResult UpdateCoachMembers(string Id, [FromBody] string memberId)
    {
        var coach = _dbContext.Coaches.FirstOrDefault(c => c.Id == Id);
        
        if (coach is null)
            return NotFound($"Coach with {Id} doesn't exist");

        var member = _dbContext.Members.FirstOrDefault(m => m.Id == memberId);

        if (member is null)
            return NotFound($"Member with {Id} doesn't exist");
        
        try
        {
            coach.addMember(member);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        _dbContext.SaveChanges();
        return Ok(coach);
    }

    [HttpPut(template: "{Id}")]
    public ActionResult UpdateCoach(string Id, CoachRequest coachRequest)
    {
        var coach = _dbContext.Coaches.FirstOrDefault(c => c.Id == Id);
        
        if (coach is null)
            return NotFound($"Coach with {Id} doesn't exist");
        
        try
        {
            coach.SetFirstName(coachRequest.FirstName);
            coach.SetLastName(coachRequest.LastName);
            
            if(coachRequest.Date is not "string")
                coach.SetCreated(coachRequest.Date);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        _dbContext.SaveChanges();
        return Ok(coach);
    }
    
}
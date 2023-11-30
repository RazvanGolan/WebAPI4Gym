using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication4Gym.Controllers.Members;
using WebApplication4Gym.Entities.Coaches;
using WebApplication4Gym.Repository;

namespace WebApplication4Gym.Controllers.Coaches;

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

    
    [HttpGet(template: "{Id}")]
    public async Task<ActionResult> GetCoach(string Id)
    {
        var coach = await _dbContext.Coaches
            .Include(coach => coach.MemberList)
            .FirstOrDefaultAsync(c => c.Id == Id);

        if (coach is null)
            return NotFound($"Coach with {Id} doesn't exist");

        return Ok(new CoachResponse
        {
            Id = coach.Id,
            FirstName = coach.FirstName,
            LastName = coach.LastName,
            Created = coach.Created,
            Members = coach.MemberList.Select(m=> new MemberResponse
            {
                Id = m.Id,
                FirstName = m.FirstName,
                LastName = m.LastName,
                Created = m.Created,
                GoldenState = m.GoldenState,
                Email = m.Email,
                CoachId = coach.Id
            }).ToList()
        });
    }
    
    [HttpPost]
    public async Task<ActionResult> AddCoach([FromBody] CoachRequest coachRequest)
    {
        Coach coach;
        try
        {
            coach = await Entities.Coaches.Coach.CreateAsync(coachRequest.FirstName, coachRequest.LastName, coachRequest.Date);
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
        var coach = _dbContext.Coaches
            .Include(coach => coach.MemberList)
            .FirstOrDefault(c => c.Id == Id);
        
        if (coach is null)
            return NotFound($"Coach with {Id} doesn't exist");

        foreach (var member in coach.MemberList)
        {
            member.Coach = null;
        }

        _dbContext.Remove(coach);
        _dbContext.SaveChanges();
        
        return Ok($"All good, {Id} is no longer along us");
    }
    
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
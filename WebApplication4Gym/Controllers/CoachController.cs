using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication4Gym.Entities;
using WebApplication4Gym.Repository;

namespace WebApplication4Gym.Controllers;

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
    public ActionResult GetCoaches()
    {
        var members = _dbContext.Set<Coach>().ToList();
        
        return Ok(members);
    }

    [HttpGet(template: "{Id}")]
    public async Task<ActionResult> GetCoach(string Id)
    {
        var coach = await _dbContext.Coaches.FirstOrDefaultAsync(c => c.Id == Id);

        if (coach is null)
            return NotFound($"Coach with {Id} doesn't exist");

        return Ok(coach);
    }

    [HttpPost]
    public ActionResult AddCoach([FromBody] CoachRequest coachRequest)
    {
        Coach coach = null;

        try
        {
            coach = Coach.Create(coachRequest.FirstName, coachRequest.LastName, coachRequest.Date);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        _dbContext.Add(coach);
        _dbContext.SaveChanges();

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
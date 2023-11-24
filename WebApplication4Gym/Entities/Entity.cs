namespace WebApplication4Gym.Entities;

public class Entity : IDentity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
}
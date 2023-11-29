namespace WebApplication4Gym.Repository;

public interface IMemberRepository
{
    Task<bool> IsEmailUnique(string email);
    //implement next methods:
    //GetAllAsync
    //GetByIdAsync
    //Create(Member member)
    //Delete(Id)
    //SaveAsync
}
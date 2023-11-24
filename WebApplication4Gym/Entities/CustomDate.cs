namespace WebApplication4Gym.Entities;

[AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
sealed class CustomDate : Attribute
{
    public string Format { get; }

    public CustomDate(string format)
    {
        Format = format;
    }
}
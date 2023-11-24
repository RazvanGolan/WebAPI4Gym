using System.Text.Json.Serialization;

namespace WebApplication4Gym.Entities;

public class DateConverter<T> : JsonConverter
{
    public override bool CanConvert(Type typeToConvert)
    {
        throw new NotImplementedException();
    }
}
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BuoyancyApi.Infrastructure.Persistence.ValueConverters;

internal class DateTimeConverter : ValueConverter<DateTime, DateTime>
{
    public DateTimeConverter() : base(
            dateTime => dateTime.ToUniversalTime(),
            dateTime => DateTime.SpecifyKind(dateTime, DateTimeKind.Utc))
    {
    }
}

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BuoyancyApi.Infrastructure.Persistence.ValueConverters;

public class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
{
    public DateOnlyConverter() : base(
            dateOnly => dateOnly.ToDateTime(TimeOnly.MinValue),
            dateTime => DateOnly.FromDateTime(dateTime))
    {
    }
}

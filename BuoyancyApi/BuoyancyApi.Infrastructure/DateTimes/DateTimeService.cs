using BuoyancyApi.Application.Common.Interfaces;

namespace BuoyancyApi.Infrastructure.DateTimes;

public class DateTimeService : IDateTimeService
{
    public DateTime Now => DateTime.Now;
}
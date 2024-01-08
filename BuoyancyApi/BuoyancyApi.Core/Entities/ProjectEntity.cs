namespace BuoyancyApi.Core.Entities;

public class ProjectEntity
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public List<PlannedTimeEntity> PlannedTime { get; set; } = null!;

    public List<RequiredTimeEntity> RequiredTime { get; set; } = null!;
}

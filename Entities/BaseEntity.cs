namespace SchedulingManagement.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; protected set; } = Guid.NewGuid();

    public DateOnly CreatedAtUtc { get; protected set; } = DateOnly.FromDateTime(DateTime.UtcNow);

    public DateOnly UpdatedAtUtc { get; protected set; } = DateOnly.FromDateTime(DateTime.UtcNow);

    public void Touch()
    {
        UpdatedAtUtc = DateOnly.FromDateTime(DateTime.UtcNow);
    }
}


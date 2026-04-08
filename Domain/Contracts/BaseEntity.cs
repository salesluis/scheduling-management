using System;

namespace scheduling_management.Domain.Contracts;

public abstract class BaseEntity
{
    public Guid Id { get; protected set; } = Guid.NewGuid();
    public DateTime CreatedAtUtc { get; init; } = DateTime.UtcNow;
    public DateTime UpdatedAtUtc { get; private set; } = DateTime.UtcNow;
    public void Touch() 
        => UpdatedAtUtc = DateTime.UtcNow;
}


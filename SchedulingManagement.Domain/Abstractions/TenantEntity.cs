namespace scheduling_management.Domain.Abstractions;

public abstract class TenantEntity : BaseEntity
{
    public Guid EstablishmentId { get; set; }
}


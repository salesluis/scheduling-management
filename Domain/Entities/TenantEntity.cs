namespace scheduling_management.Domain.Entities;
public abstract class TenantEntity : BaseEntity
{
    public Guid EstablishmentId { get; protected set; }
}


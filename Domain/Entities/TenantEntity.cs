using scheduling_management.Entities;

namespace SchedulingManagement.Entities;
public abstract class TenantEntity : BaseEntity
{
    public Guid EstablishmentId { get; protected set; }
}


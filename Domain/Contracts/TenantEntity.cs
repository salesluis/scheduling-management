using System;
using scheduling_management.Domain.Contracts;
using scheduling_management.Domain.Entities;

namespace scheduling_management.Domain.Contracts;

public abstract class TenantEntity : BaseEntity
{
    public Guid EstablishmentId { get; set; }
    public Establishment Establishment { get; set; } = null!;

    public void SetEstablishmentId(Guid establishment)
    {
        EstablishmentId = establishment;
    }
}


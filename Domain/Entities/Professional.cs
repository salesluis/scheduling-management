using System;
using System.Collections.Generic;
using scheduling_management.Domain.Contracts;

namespace scheduling_management.Domain.Entities;

public class Professional : TenantEntity
{
    public Guid UserId { get; private set; }
    public string DisplayName { get; private set; } = null!;
    public bool IsActive { get; private set; } = true;
    public int TotalClients { get; private set; } = 0;

    public Establishment Establishment { get; private set; } = null!;
    
    public User User { get; private set; } = null!;
    public List<ProfessionalService> ProfessionalServices { get; private set; } = new();
    public List<Appointment> Appointments { get; private set; } = new();

    public Professional(Guid establishmentId, Guid userId, string displayName)
    {
        EstablishmentId = establishmentId;
        UserId = userId;
        DisplayName = displayName;
    }

    public void SetDisplayName(string displayName)
    {
        DisplayName = displayName;
        Touch();
    }

    public void Activate()
    {
        IsActive = true;
        Touch();
    }

    public void Deactivate()
    {
        IsActive = false;
        Touch();
    }
}


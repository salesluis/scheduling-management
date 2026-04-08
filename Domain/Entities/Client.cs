using System;
using System.Collections.Generic;
using scheduling_management.Domain.Contracts;

namespace scheduling_management.Domain.Entities;
public class Client : TenantEntity
{
    public Guid? UserId { get; private set; }
    public string Name { get; private set; } = null!;
    public string? PhoneNumber { get; private set; } = null!;
    
    public List<Appointment> Appointments { get; private set; } = new();
    public Client(Guid establishmentId, string name, string? phoneNumber = null)
    {
        EstablishmentId = establishmentId;
        Name = name;
        PhoneNumber = phoneNumber;
    }

    public void Update(string name, string? phoneNumber)
    {
        Name = name;
        PhoneNumber = phoneNumber;
        Touch();
    }
}


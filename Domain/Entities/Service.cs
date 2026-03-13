using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using scheduling_management.Domain.Abstractions;

namespace scheduling_management.Domain.Entities;
public class Service : TenantEntity
{
    public string Name { get; private set; }
    public int DurationInMinutes { get; private set; }
    public int PriceInReal { get; private set; }
    public bool IsActive { get; private set; } = true; 
    // todo: definir cor padrão
    public string Color { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;

    public Establishment Establishment { get; private set; } = null!;
    public List<ProfessionalService> ProfessionalServices { get; private set; } = new();
    public List<Appointment> Appointments { get; private set; } = new();

    public Service() { }    
    public Service(Guid establishmentId, string name, int durationInMinutes, int price)
    {
        EstablishmentId = establishmentId;
        Name = name;
        DurationInMinutes = durationInMinutes;
        PriceInReal = price;
    }
}


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

    public Service(Guid establishmentId, string name, int durationInMinutes, int price, string? color = null, string? description = null)
    {
        EstablishmentId = establishmentId;
        Name = name;
        DurationInMinutes = durationInMinutes;
        PriceInReal = price;
        Color = color ?? string.Empty;
        Description = description ?? string.Empty;
    }

    public void Update(string name, int durationInMinutes, int priceInReal, string? color = null, string? description = null)
    {
        Name = name;
        DurationInMinutes = durationInMinutes;
        PriceInReal = priceInReal;
        Color = color ?? string.Empty;
        Description = description ?? string.Empty;
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


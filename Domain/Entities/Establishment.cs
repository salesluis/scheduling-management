using System.Collections.Generic;
using scheduling_management.Domain.Contracts;

namespace scheduling_management.Domain.Entities;

public class Establishment(string name) : BaseEntity
{
    public string Name { get; private set; } = name;
    public string Slug { get; private set; } = name
        .Replace(" ", "-")
        .ToLower();

    public bool IsActive { get; private set; } = true;

    public List<Service> Services { get; private set; } = new();
    public List<Professional> Professionals { get; private set; } = new();
    public List<Client> Clients { get; private set; } = new();
    public List<Appointment> Appointments { get; private set; } = new();

    public void UpdateName(string name)
    {
        Name = name;
        Slug = name.Replace(" ", "-").ToLower();
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


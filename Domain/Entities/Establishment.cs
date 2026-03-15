using System.Collections.Generic;
using scheduling_management.Domain.Abstractions;

namespace scheduling_management.Domain.Entities;

public class Establishment : BaseEntity
{
    public string Name { get; private set; } 
    public string Slug { get; private set; }
    public bool IsActive { get; private set; } = true;

    public ICollection<Service> Services { get; private set; } = new List<Service>();
    public ICollection<Professional> Professionals { get; private set; } = new List<Professional>();
    public ICollection<Client> Clients { get; private set; } = new List<Client>();
    public ICollection<Appointment> Appointments { get; private set; } = new List<Appointment>();
    

    public Establishment(string name)
    {
        Name = name;
        Slug = name
            .Replace(" ", "-")
            .ToLower();
    }

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


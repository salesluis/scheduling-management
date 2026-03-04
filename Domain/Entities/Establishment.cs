using scheduling_management.Domain.Abstractions;

namespace scheduling_management.Domain.Entities;

public class Establishment : BaseEntity
{
    public string Name { get; private set; } 

    public string Slug { get; private set; }

    public string TimeZoneId { get; private set; } = "UTC";

    public bool IsActive { get; private set; } = true;

    public ICollection<Service> Services { get; private set; } = new List<Service>();

    public ICollection<Professional> Professionals { get; private set; } = new List<Professional>();

    public ICollection<Client> Clients { get; private set; } = new List<Client>();

    public ICollection<Appointment> Appointments { get; private set; } = new List<Appointment>();
    

    public Establishment(string name, string slug, string timeZoneId)
    {
        Name = name;
        Slug = slug;
        TimeZoneId = timeZoneId;
    }
}


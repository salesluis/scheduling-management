using scheduling_management.Domain.Abstractions;

namespace scheduling_management.Domain.Entities;
public class Service : TenantEntity
{
    public string Name { get; private set; } = null!;

    public int DurationInMinutes { get; private set; }

    public int PriceInCentavos { get; private set; }

    public bool IsActive { get; private set; } = true;

    public Establishment Establishment { get; private set; } = null!;

    public ICollection<ProfessionalService> ProfessionalServices { get; private set; } = new List<ProfessionalService>();

    public ICollection<Appointment> Appointments { get; private set; } = new List<Appointment>();

    public Service(Guid establishmentId, string name, int durationInMinutes, int price)
    {
        EstablishmentId = establishmentId;
        Name = name;
        DurationInMinutes = durationInMinutes;
        PriceInCentavos = price;
    }
}


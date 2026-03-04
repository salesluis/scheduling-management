namespace scheduling_management.Domain.Entities;
public class Service : TenantEntity
{
    public string Name { get; private set; } = null!;

    public int DurationInMinutes { get; private set; }

    public decimal? Price { get; private set; }

    public bool IsActive { get; private set; } = true;

    public Establishment Establishment { get; private set; } = null!;

    public ICollection<ProfessionalService> ProfessionalServices { get; private set; } = new List<ProfessionalService>();

    public ICollection<Appointment> Appointments { get; private set; } = new List<Appointment>();

    private Service()
    {
    }

    public Service(Guid establishmentId, string name, int durationInMinutes, decimal? price = null)
    {
        EstablishmentId = establishmentId;
        Name = name;
        DurationInMinutes = durationInMinutes;
        Price = price;
    }
}


namespace scheduling_management.Domain.Entities;
public class Professional : TenantEntity
{
    public Guid UserId { get; private set; }

    public string DisplayName { get; private set; } = null!;

    public bool IsActive { get; private set; } = true;

    public Establishment Establishment { get; private set; } = null!;

    public User User { get; private set; } = null!;

    public ICollection<ProfessionalService> ProfessionalServices { get; private set; } = new List<ProfessionalService>();

    public ICollection<Appointment> Appointments { get; private set; } = new List<Appointment>();

    private Professional()
    {
    }

    public Professional(Guid establishmentId, Guid userId, string displayName)
    {
        EstablishmentId = establishmentId;
        UserId = userId;
        DisplayName = displayName;
    }
}


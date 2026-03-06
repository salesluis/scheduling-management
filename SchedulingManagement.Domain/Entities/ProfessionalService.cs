using scheduling_management.Domain.Abstractions;

namespace scheduling_management.Domain.Entities;
public class ProfessionalService : TenantEntity
{
    public Guid ProfessionalId { get; private set; }

    public Guid ServiceId { get; private set; }

    public Professional Professional { get; private set; } = null!;

    public Service Service { get; private set; } = null!;


    public ProfessionalService(Guid establishmentId, Guid professionalId, Guid serviceId)
    {
        EstablishmentId = establishmentId;
        ProfessionalId = professionalId;
        ServiceId = serviceId;
    }
}


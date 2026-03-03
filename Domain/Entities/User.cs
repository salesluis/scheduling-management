using System.ComponentModel.DataAnnotations;
using scheduling_management.Entities;
using SchedulingManagement.Entities;

namespace scheduling_management.Domain.Entities;
public class User : BaseEntity
{
    public string Name { get; private set; }
    public string Email { get; private set; } = null!;
    public string? PhoneNumber { get; private set; }
    
    public ICollection<Professional> ProfessionalProfiles { get; private set; } = new List<Professional>();

    public ICollection<Client> ClientProfiles { get; private set; } = new List<Client>();

    private User()
    {
    }

    public User(string name, string email, string? phoneNumber = null)
    {
        Name = name;
        Email = email;
        PhoneNumber = phoneNumber;
    }
}


using scheduling_management.Domain.Abstractions;

namespace scheduling_management.Domain.Entities;

public class User : BaseEntity
{
    public string Name { get; private set; }
    public string Email { get; private set; } = null!;
    public string? PhoneNumber { get; private set; }

    public User(string name, string email, string? phoneNumber = null)
    {
        Name = name;
        Email = email;
        PhoneNumber = phoneNumber;
    }

    public void Update(string name, string email, string? phoneNumber = null)
    {
        Name = name;
        Email = email;
        PhoneNumber = phoneNumber;
        Touch();
    }
}


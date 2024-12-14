using Microsoft.AspNetCore.Identity;

namespace User.Domain;

public sealed class User : IdentityUser<long>
{
    public string FirstName { get; set; } = null!;

    public string LastName { get;  set; } = null!;

    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    
    public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;
}
using Q2.Web_Service.API.UserManagement.Domain.Model.ValueObjects;

namespace Q2.Web_Service.API.UserManagement.Domain.Model.Entities;

public class User
{
    public Guid Id { get; }
    public string Email { get; }
    public string Token { get; }
    public Role Rol { get; }
    
    public Profile Profile { get; }

    public User(Guid id, string email, string token, Role rol, Profile profile)
    {
        Id = id;
        Email = email;
        Token = token;
        Rol = rol;
        Profile = profile;
    }
}

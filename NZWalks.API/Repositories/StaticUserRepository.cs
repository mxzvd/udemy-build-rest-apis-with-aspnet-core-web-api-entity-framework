using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories;

public class StaticUserRepository : IUserRepository
{
    private List<User> Users = new List<User>() {
        new() {
            FirstName = "Read Only",
            LastName = "User",
            EmailAddress = "readonly@user.com",
            Id = new(),
            Username = "readonly@user.com",
            Password = "Readonly@user.com",
            Roles = new() { "reader" }
        },
        new() {
            FirstName = "Read Write",
            LastName = "User",
            EmailAddress = "readwrite@user.com",
            Id = new(),
            Username = "readwrite@user.com",
            Password = "Readwrite@user.com",
            Roles = new() { "reader", "writer" }
        },
    };

    public async Task<User> AuthenticateAsync(string username, string password)
    {
        var user = Users.Find(e => e.Username.Equals(username, StringComparison.InvariantCultureIgnoreCase) && e.Password == password);

        return user;
    }
}

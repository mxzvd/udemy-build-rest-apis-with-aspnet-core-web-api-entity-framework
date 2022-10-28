using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories;

public class UserRepository : IUserRepository
{
    private readonly NZWalksDbContext dbContext;
    
    public UserRepository(NZWalksDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<User> AuthenticateAsync(string username, string password)
    {
        var user = dbContext.Users.FirstOrDefault(x => x.Username.ToLower() == username.ToLower() && x.Password == password);

        if (user == null)
        {
            return null;
        }

        var userRoles = await dbContext.Users_Roles.Where(e => e.UserId == user.Id).ToListAsync();

        if (userRoles.Any())
        {
            user.Roles = new List<string>();
            foreach (var userRole in userRoles)
            {
                var role = await dbContext.Roles.FirstOrDefaultAsync(e => e.Id == userRole.RoleId);
                if (role != null)
                {
                    user.Roles.Add(role.Name);
                }
            }
        }

        user.Password = null;
        return user;
    }
}

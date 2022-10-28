using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories;

public interface ITokenHandler
{
    string CreateToken(User user);
}

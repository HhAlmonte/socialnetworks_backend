using Core.Entities;

namespace Core.Interface
{
    public interface ITokenService
    {
        string CreateToken(UserEntities user);
    }
}

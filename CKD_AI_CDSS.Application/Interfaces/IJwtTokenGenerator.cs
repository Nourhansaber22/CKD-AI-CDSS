using CKD_AI_CDSS.Domain.Entites;
namespace CKD_AI_CDSS.Application.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}

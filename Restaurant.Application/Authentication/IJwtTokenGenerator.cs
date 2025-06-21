
using Restaurant.Domain.Entities;

namespace Restaurant.Application.Authentication;

public interface IJwtTokenGenerator
{
    public Task<string> GenerateToken(User user);
}

using WSVenta.Models.Request;
using WSVenta.Models.Response;

namespace WSVenta.Services.User
{
    public interface IUserService
    {
        Task<UserResponse> LoginAuth(AuthRequest user, string jwtSecret, string dbConnectionString);
    }
}

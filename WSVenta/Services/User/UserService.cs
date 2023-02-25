using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WSVenta.Models;
using WSVenta.Models.Request;
using WSVenta.Models.Response;
using WSVenta.Tools;

namespace WSVenta.Services.User
{
    public class UserService : IUserService
    {
        public async Task<UserResponse> LoginAuth(AuthRequest user, string jwtSecret, string dbConnectionString)
        {
            UserResponse response = new UserResponse();
            try
            {
                using (VentaRealContext db = new VentaRealContext())
                {
                    Usuario usuarioDB = await db.Usuarios.Where(d => d.Email == user.Email).FirstOrDefaultAsync();
                    /* DAPPER
                    using SqlConnection connection = new SqlConnection(dbConnectionString);
                    Usuario usuarioDB = await connection.QueryFirstOrDefaultAsync<Usuario>(
                        "SELECT * FROM usuario WHERE email = @Email;", user);
                    */

                    if (usuarioDB == null) return null;
                    if (Encrypt.VerifyPasswordHash(usuarioDB, user.Password))
                    {
                        response.Email = user.Email;
                        response.Token = Encrypt.GenerateJWTToken(usuarioDB, jwtSecret);
                    }
                }
            }
            catch(Exception ex)
            {
                return null;
            }
            return response;
        }
    }
}

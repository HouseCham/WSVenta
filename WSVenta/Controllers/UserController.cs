using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WSVenta.Models;
using WSVenta.Models.Request;
using WSVenta.Models.Response;
using WSVenta.Services.User;
using WSVenta.Tools;

namespace WSVenta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost("Registrar")]
        public async Task<IActionResult> RegistrarNuevoCliente([FromBody] AuthRequest user)
        {
            /* ========== Registro de usuario ========== */
            Encrypt.CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordKey);
            Usuario usuario = new Usuario{
                Nombre= user.Nombre,
                Email= user.Email,
                PasswordHash= passwordHash,
                PasswordKey= passwordKey
            };
            try
            {
                using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("SqlServerDB"));
                await connection
                    .ExecuteAsync(
                    "INSERT INTO usuario(nombre, email, passwordHash, passwordKey) " +
                    "VALUES(@nombre, @email, @passwordHash, @passwordKey);", usuario);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Autentificar([FromBody] AuthRequest user)
        {
            Respuesta response = new Respuesta();
            var authResponse = await _userService.LoginAuth(
                user, _configuration.GetSection("JwtConfig:Secret").Value, _configuration.GetConnectionString("SqlServerDB"));
            if (authResponse.Token == null && authResponse.Email == null)
            {
                response.Mensaje = "Usuario o clave incorrectos";
                return Ok(response);
            }

            response.Exito = 1;
            response.Data = authResponse;
            return Ok(response);
        }
    }
}
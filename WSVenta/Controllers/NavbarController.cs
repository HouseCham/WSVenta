using Dapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WSVenta.Models;
using WSVenta.Models.Response;

namespace WSVenta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class NavbarController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public NavbarController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<NavbarSection>>> getNavbarSections(int id)
        {
            Respuesta response = new Respuesta();
            List<NavbarSection> navbar = new List<NavbarSection>();
            try
            {
                using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("SqlServerDB"));
                navbar = (List<NavbarSection>) await connection.QueryAsync<NavbarSection>(
                    $"SELECT id, sectionName, sectionUrl FROM navbar_section WHERE isActive = 1 AND navbarId = {id};", new {});
                response.Exito = 1;
                response.Data = navbar;
            }
            catch (Exception ex) 
            {
                response.Mensaje = ex.Message;
            }

            return Ok(response);
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WSVenta.Models;
using WSVenta.Models.Response;
using WSVenta.Models.Request;
using Microsoft.Data.SqlClient;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WSVenta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ClienteController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ClienteController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (VentaRealContext db = new VentaRealContext())
                {
                    var lst = await db.Clientes.OrderByDescending(d => d.Id).ToListAsync(); // => mayor o igual que
                    oRespuesta.Exito = 1;
                    oRespuesta.Data = lst;
                }
            }
            catch(Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ClienteRequest oModel)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                // ENTITY FRAMEWORK VERSION
                using(VentaRealContext db = new VentaRealContext())
                {
                    Cliente oCliente = new Cliente()
                    {
                        Nombre = oModel.Nombre,
                        ApellidoP = oModel.ApellidoP,
                        ApellidoM = oModel.ApellidoM,
                        Email = oModel.Email,
                        Telefono = oModel.Telefono
                    };

                    db.Clientes.Add(oCliente);
                    await db.SaveChangesAsync();
                }
                
                /* DAPPER VERSION
                using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("SqlServerDB"));
                await connection.ExecuteAsync(
                    "INSERT INTO cliente (nombre, apellido_p, apellido_m, email, telefono)" +
                    "VALUES (@Nombre, @ApellidoP, @ApellidoM, @Email, @Telefono)", oModel);
                */
                oRespuesta.Exito = 1;
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);
        }

        [HttpPut]
        public async Task<IActionResult> Update(ClienteRequest oModel)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (VentaRealContext db = new VentaRealContext())
                {
                    Cliente oCliente = db.Clientes.Find(oModel.Id);

                    oCliente.Nombre = oModel.Nombre;
                    oCliente.ApellidoP = oModel.ApellidoP;
                    oCliente.ApellidoM = oModel.ApellidoM;
                    oCliente.Email = oModel.Email;
                    oCliente.Telefono = oModel.Telefono;

                    db.Entry(oCliente).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    await db.SaveChangesAsync();
                    oRespuesta.Exito = 1;
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (VentaRealContext db = new VentaRealContext())
                {
                    Cliente oCliente = db.Clientes.Find(Id);
                    db.Remove(oCliente);
                    await db.SaveChangesAsync();
                    oRespuesta.Exito = 1;
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);
        }
        
    }
}
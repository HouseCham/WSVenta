using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WSVenta.Models;
using WSVenta.Models.Request;
using WSVenta.Models.Response;

namespace WSVenta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class VentaController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Add(VentaRequest ventaRequest)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                using (VentaRealContext db = new VentaRealContext())
                {
                    Ventum venta = new Ventum();
                    venta.Total = ventaRequest.Total;
                    venta.Fecha = DateTime.Now;
                    venta.IdCliente = ventaRequest.IdCliente;

                    db.Venta.Add(venta);
                    await db.SaveChangesAsync();

                    foreach (var conceptoRequest in venta.Conceptos)
                    {
                        Models.Concepto concepto = new Models.Concepto();

                        concepto.Cantidad = conceptoRequest.Cantidad;
                        concepto.IdProducto = conceptoRequest.IdProducto;
                        concepto.PrecioUnitario = conceptoRequest.PrecioUnitario;
                        concepto.IdVenta = conceptoRequest.IdVenta;

                        db.Conceptos.Add(concepto);
                        await db.SaveChangesAsync();
                    }
                }

                respuesta.Exito = 1;

            }
            catch (Exception ex)
            {
                respuesta.Mensaje = ex.Message;
            }
            return Ok(respuesta);
        }
    }
}

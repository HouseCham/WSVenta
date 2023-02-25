namespace WSVenta.Models.Request
{
    public class VentaRequest
    {
        public int IdCliente { get; set; }
        public decimal Total { get; set; }
        List<Concepto> Conceptos { get; set; }
    }

    public class Concepto
    {
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Importe { get; set; }
        public int IdProducto { get; set; }
    }
}

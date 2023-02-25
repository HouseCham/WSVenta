namespace WSVenta.Models.Request
{
    public class ClienteRequest
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string ApellidoP { get; set; }
        public string ApellidoM { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
    }
}
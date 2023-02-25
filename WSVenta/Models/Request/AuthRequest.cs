using System.ComponentModel.DataAnnotations;

namespace WSVenta.Models.Request
{
    public class AuthRequest
    {
        public string Nombre { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        //[Required]
        //public byte[]? PasswordHash { get; set; }   // Length 64 bytes
        //[Required]
        //public byte[]? PasswordSalt { get; set; }   // Length 128 bytes
    }
}

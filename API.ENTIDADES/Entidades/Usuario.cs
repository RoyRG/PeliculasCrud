using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace API.ENTIDADES.Entidades
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        public string UsuarioAcceso { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public bool Activo { get; set; }
    }
}

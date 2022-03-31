﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace API.ENTIDADES.Modelos
{
    public class UsuarioModelo
    {
        [Key]
        public int Id { get; set; }
        //[Required(ErrorMessage = "El usuario es obligatorio")]
        public string UsuarioAcceso { get; set; }
        [Required(ErrorMessage = "El usuario es obligatorio")]
        [StringLength(10, MinimumLength = 4, ErrorMessage = "La contraseña debe estar entre 4 y 10 caracteres")]
        public string Password { get; set; }
        public byte[] PasswordHash { get; set; }
    }
}
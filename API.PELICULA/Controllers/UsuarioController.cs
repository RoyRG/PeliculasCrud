using API.ENTIDADES;
using API.ENTIDADES.Modelos;
using API.SERVICIOS.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.PELICULA.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "ApiPeliculasUsuarios")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class UsuarioController : ControllerBase
    {
        private readonly IServicioUsuario servicioUsuario;
        private readonly IConfiguration configuration;
        public UsuarioController(IServicioUsuario servicioUsuario, IConfiguration configuration)
        {
            this.servicioUsuario = servicioUsuario;
            this.configuration = configuration;
        }
        /// <summary>
        /// Obtener los usuarios 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<ResultadoOperacion<IEnumerable<UsuarioModelo>>>> Get()
        {
            return await servicioUsuario.ObtenerTodos();
        }
        /// <summary>
        /// Obtener a un usuario 
        /// </summary>
        /// <param name="clave">Clave para distinguir el usuario deseado</param>
        /// <returns></returns>
        [HttpGet("{clave}")]
        public async Task<ActionResult<ResultadoOperacion>> Get(string clave)
        {
            return await servicioUsuario.Obtener(clave);
        }
        /// <summary>
        /// Registro de un nuevo usuario 
        /// </summary>
        /// <param name="usuarioModelo">Datos para el registro del nuevo usuario</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Registro(UsuarioModelo usuarioModelo)
        {
              servicioUsuario.Registro(usuarioModelo, usuarioModelo.Password);
            return Ok();
        }
        /// <summary>
        /// Autenticación de los datos del usuario
        /// </summary>
        /// <param name="usuarioModelo">Nombre y contraseña</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("{Login}")]
        public IActionResult Login(UsuarioModelo usuarioModelo)
        {
            //throw new Exception("Error generado");

            var usuarioDesdeRepo = servicioUsuario.Login(usuarioModelo.UsuarioAcceso, usuarioModelo.Password);

            if (usuarioDesdeRepo == null)
            {
                return Unauthorized();
            }

            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, usuarioDesdeRepo.Id.ToString()),
            new Claim(ClaimTypes.Name, usuarioDesdeRepo.UsuarioAcceso.ToString())
        };

            //Generación de token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:Token").Value));
            var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credenciales
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token)
            });
        }
    }
}

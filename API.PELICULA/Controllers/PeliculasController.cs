using API.ENTIDADES;
using API.ENTIDADES.Modelos;
using API.SERVICIOS.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace API.PELICULA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeliculasController : ControllerBase
    {
        private readonly IServicioPelicula servicioPelicula;
        private readonly IWebHostEnvironment hostingEnvironment;
        public PeliculasController(IServicioPelicula servicioPelicula, IWebHostEnvironment hostingEnvironment)
        {
            this.servicioPelicula = servicioPelicula;
            this.hostingEnvironment = hostingEnvironment;

        }
        /// <summary>
        /// Obtener todas las peliculas 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<ResultadoOperacion<IEnumerable<PeliculaModelo>>>> Get()
        {
            return await servicioPelicula.ObtenerTodos();
        }
        /// <summary>
        /// Obtener una película por su clave 
        /// </summary>
        /// <param name="clave">Clave para identificar la película deseada</param>
        /// <returns></returns>
        [HttpGet("{clave}")]
        public async Task<ActionResult<ResultadoOperacion>> Get(string clave)
        {
            return await servicioPelicula.Obtener(clave);
        }
        /// <summary>
        /// Obtenemos las películas por su categoría
        /// </summary>
        /// <param name="clave">La categoría que se desea</param>
        /// <returns></returns>
        [HttpGet("Categoria/{clave}")]
        public async Task<ActionResult<ResultadoOperacion>> GetC(string clave)
        {
            return await servicioPelicula.ObtenerCategoria(clave);
        }
        /// <summary>
        /// Eliminar una película de manera lógica
        /// </summary>
        /// <param name="clave">Clave para identificar la película que se desea eliminar</param>
        /// <returns></returns>
        [HttpDelete("{clave}")]
        public async Task<ActionResult<ResultadoOperacion>> Delete(string clave)
        {
            return await servicioPelicula.Eliminar(clave);
        }
        /// <summary>
        /// Modificar una plícula
        /// </summary>
        /// <param name="peliculaModelo">Id y datos a modificar de una película</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<ResultadoOperacion>> Put(PeliculaModelo peliculaModelo)
        {
            return await servicioPelicula.Actualizar(peliculaModelo);
        }
        /// <summary>
        /// Crear una nueva película
        /// </summary>
        /// <param name="peliculaModelo">Datos para registrar la película</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ResultadoOperacion>> Post([FromForm] PeliculaModelo peliculaModelo)
        {
            var archivo = peliculaModelo.Foto;
            string rutaPrincipal = hostingEnvironment.WebRootPath;
            var archivos = HttpContext.Request.Form.Files;
           if (archivo.Length > 0)
            {
                var nombreFoto = Guid.NewGuid().ToString();
                var subidas = Path.Combine(rutaPrincipal, @"fotos");
                var extension = Path.GetExtension(archivos[0].FileName);

                using (var fileStreams = new FileStream(Path.Combine(subidas, nombreFoto + extension), FileMode.Create))
                {
                    archivos[0].CopyTo(fileStreams);
                }
                peliculaModelo.RutaImagen = @"\fotos\" + nombreFoto + extension;
            }
            return await servicioPelicula.Insertar(peliculaModelo);
        }
        /// <summary>
        /// Busqueda de una película por su nombre
        /// </summary>
        /// <param name="nombre">Nombre de la película que se quiere búscar</param>
        /// <returns></returns>
        [HttpGet("Buscar/{clave}")]
        public IEnumerable<PeliculaModelo> BusquedaPelicula(string nombre)
        {
            return servicioPelicula.BuscarPelicula(nombre);
        }
    }
}


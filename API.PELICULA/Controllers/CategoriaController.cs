using API.ENTIDADES;
using API.ENTIDADES.Modelos;
using API.SERVICIOS.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.PELICULA.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "ApiPeliculasCategorias")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class CategoriaController : ControllerBase
    {
        private readonly IServicioCategoria servicioCategoria;
       
        public CategoriaController(IServicioCategoria servicioCategoria)
        {
            this.servicioCategoria = servicioCategoria;
        }
        /// <summary>
        /// Obtener todas las categorías
        /// </summary>
        /// <returns></returns>
        /// 
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<CategoriaModelo>))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ResultadoOperacion<IEnumerable<CategoriaModelo>>>> Get()
        {
            return await servicioCategoria.ObtenerTodos();
        }
        /// <summary>
        /// Obtener una categoría individual
        /// </summary>
        /// <param name="clave"> Clave es el parametro por el cual obtendremos un solo elemento</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("{clave}")]
        [ProducesResponseType(200, Type = typeof(CategoriaModelo))]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<ResultadoOperacion>> Get(string clave)
        {
            return await servicioCategoria.Obtener(clave);
        }
        /// <summary>
        /// Eliminar de manera lógica un elemento
        /// </summary>
        /// <param name="clave"></param>
        /// <returns></returns>
        [HttpDelete("{clave}")]
        public async Task<ActionResult<ResultadoOperacion>> Delete(string clave)
        {
            return await servicioCategoria.Eliminar(clave);
        }
        /// <summary>
        /// Modificar una Categoría 
        /// </summary>
        /// <param name="categoriaModelo">Clave es el parametro por el cual obtendremos un solo elemento</param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResultadoOperacion>> Put(CategoriaModelo categoriaModelo)
        {
            return await servicioCategoria.Actualizar(categoriaModelo);
        }
        /// <summary>
        /// Crear una Categoría 
        /// </summary>
        /// <param name="categoriaModelo">Elementos para la creacion de una Categoría</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(CategoriaModelo))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResultadoOperacion>> Post(CategoriaModelo categoriaModelo)
        {

            return await servicioCategoria.Insertar(categoriaModelo);
        }
    }
}

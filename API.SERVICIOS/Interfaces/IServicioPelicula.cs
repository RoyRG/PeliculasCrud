using API.ENTIDADES;
using API.ENTIDADES.Modelos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace API.SERVICIOS.Interfaces
{
    public interface IServicioPelicula : IServicioBase<PeliculaModelo>
    {
        IEnumerable<PeliculaModelo> BuscarPelicula(string nombre);
        Task<ResultadoOperacion<IEnumerable<PeliculaModelo>>> ObtenerCategoria(object llave);
    }
}

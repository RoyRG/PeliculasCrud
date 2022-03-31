
using API.ENTIDADES;
using API.ENTIDADES.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace API.NEGOCIOS.Interfaces
{
    public interface INegocioPelicula : INegocioBase<Pelicula>
    {
        IEnumerable<Pelicula> BuscarPelicula(string nombre);
        Task<ResultadoOperacion<IEnumerable<Pelicula>>> ObtenerCategoria(object llave);
    }
}

using API.ENTIDADES;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace API.NEGOCIOS.Interfaces
{
    public interface INegocioConsulta<T>
    {
        Task<ResultadoOperacion<IEnumerable<T>>> Obtener(object llave);
        Task<ResultadoOperacion<IEnumerable<T>>> ObtenerTodos();
    }
}

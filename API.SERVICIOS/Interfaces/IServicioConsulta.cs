using API.ENTIDADES;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace API.SERVICIOS.Interfaces
{
    public interface IServicioConsulta<T>
    {
        Task<ResultadoOperacion<IEnumerable<T>>> ObtenerTodos();
        Task<ResultadoOperacion<IEnumerable<T>>> Obtener(object llave);
        Task<ResultadoOperacion<IEnumerable<T>>> ObtenerCategoria(object llave);
    }
}

using API.ENTIDADES;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace API.SERVICIOS.Interfaces
{
    public interface IServicioBase<T> : IServicioConsulta<T>
    {
        Task<ResultadoOperacion<T>> Insertar(T modelo);
        Task<ResultadoOperacion<T>> Actualizar(T modelo);
        Task<ResultadoOperacion> Eliminar(string id);
    }
}

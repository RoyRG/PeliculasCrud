using API.ENTIDADES;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace API.NEGOCIOS.Interfaces
{
    public interface INegocioActualizacion<T> : INegocioConsulta<T>
    {
       Task<ResultadoOperacion<T>> Insertar(T modelo);
       Task<ResultadoOperacion<T>> Actualizar(T modelo);
    }
}

using API.ENTIDADES;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace API.NEGOCIOS.Interfaces
{
    public interface INegocioBase<T> : INegocioActualizacion<T>
    {
        Task<ResultadoOperacion> Eliminar(object llave);

    }
}

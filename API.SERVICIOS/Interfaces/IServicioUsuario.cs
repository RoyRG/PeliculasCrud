using API.ENTIDADES;
using API.ENTIDADES.Modelos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace API.SERVICIOS.Interfaces
{
    public interface IServicioUsuario
    {
        Task<ResultadoOperacion<IEnumerable<UsuarioModelo>>> ObtenerTodos();
        Task<ResultadoOperacion<IEnumerable<UsuarioModelo>>> Obtener(object llave);
        UsuarioModelo Registro(UsuarioModelo usuario, string password);
        UsuarioModelo Login(string usuario, string password);
    }
}

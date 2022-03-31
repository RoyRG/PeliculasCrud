using API.ENTIDADES;
using API.ENTIDADES.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace API.NEGOCIOS.Interfaces
{
    public interface INegocioUsuario
    {
        Task<ResultadoOperacion<IEnumerable<Usuario>>> ObtenerTodos();
        Task<ResultadoOperacion<IEnumerable<Usuario>>> Obtener(object llave);
        Usuario Registro(Usuario usuario, string password);
        Usuario Login(string usuario, string password);
    }
}

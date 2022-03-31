using API.DATA;
using API.ENTIDADES;
using API.ENTIDADES.Entidades;
using API.ENTIDADES.Enums;
using API.NEGOCIOS.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.NEGOCIOS.Negocios
{
    public class NegocioUsuario : INegocioUsuario
    {
        private readonly Contexto db;
        public NegocioUsuario(Contexto db)
        {
            this.db = db;
        }

        public Usuario Login(string usuario, string password)
        {
            var user = db.Usuario.FirstOrDefault(x => x.UsuarioAcceso == usuario);

            if (user == null)
            {
                return null;
            }

            if (!VerificaPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }

            return user;
        }

        public async Task<ResultadoOperacion<IEnumerable<Usuario>>> Obtener(object llave)
        {
            var resultado = new ResultadoOperacion<IEnumerable<Usuario>>();
            var sucursalesConsulta = db.Usuario.Where(c => c.Activo == true && c.Id.ToString() == llave.ToString());
            if (sucursalesConsulta == null)
            {
                return resultado.Error(TipoError.AccesoDatos, CodigoError.ReglaNegocio, "No se encontraron registros");
            }

            resultado.Datos = sucursalesConsulta;
            return resultado.Exito();
        }

        public async Task<ResultadoOperacion<IEnumerable<Usuario>>> ObtenerTodos()
        {
            var resultado = new ResultadoOperacion<IEnumerable<Usuario>>();
            var sucursalesConsulta = db.Usuario.Where(c => c.Activo == true)/*.ToList()/*.FirstOrDefault()*/;
            if (sucursalesConsulta == null)
            {
                return resultado.Error(TipoError.AccesoDatos, CodigoError.ReglaNegocio, "No se encontraron registros");
            }

            resultado.Datos = sucursalesConsulta;
            return resultado.Exito();

        }

        public Usuario Registro(Usuario usuario, string password)
        {
            byte[] passwordHash, passwordSalt;

            CrearPasswordHash(password, out passwordHash, out passwordSalt);

            usuario.PasswordHash = passwordHash;
            usuario.PasswordSalt = passwordSalt;
            usuario.UsuarioAcceso = usuario.UsuarioAcceso;
            usuario.Activo = true;
            db.Usuario.Add(usuario);
            db.SaveChanges();
            return usuario;
        }
        private bool VerificaPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var hashComputado = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < hashComputado.Length; i++)
                {
                    if (hashComputado[i] != passwordHash[i]) return false;
                }
            }
            return true;
        }
        private void CrearPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
    }
}

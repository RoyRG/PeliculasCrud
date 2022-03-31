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
    public class NegocioCategoria : INegocioCategoria
    {
        string ObtenerMensaje = "No se pudo eliminar";
        private readonly Contexto db;
        //public readonly IUnidad<Contexto> db;
        //public NegocioCategoria(IUnidad<Contexto> db)
        //{
        //    this.db = db;
        //}        public readonly IUnidad<Contexto> db;
        public NegocioCategoria(Contexto db)
        {
            this.db = db;
        }
        public async Task<ResultadoOperacion<Categoria>> Actualizar(Categoria modelo)
        {
            var resultado = new ResultadoOperacion<Categoria>();

            var elemento =  db.Categoria.Where(c => c.Activo == true && c.categoriaId == modelo.categoriaId).ToList().FirstOrDefault();
            elemento.Nombre = modelo.Nombre;

            if (modelo != null)
            {
                db.Update(elemento);
                db.SaveChanges();
                return resultado.Exito();
            }
            return resultado.Error(TipoError.Negocio, CodigoError.NoActualizado, ObtenerMensaje);
        }

        public async Task<ResultadoOperacion> Eliminar(object llave)
        {
            
            var resultado = new ResultadoOperacion<Categoria>();
            var elemento =  db.Categoria.Where(c => c.categoriaId.ToString() == llave.ToString()).FirstOrDefault();
            if (elemento != null)
            {
                elemento.Activo = false;
                db.SaveChanges();
                return resultado.Exito(elemento);
            }
            return resultado.Error(TipoError.Negocio, CodigoError.IdentificadorInvalido, ObtenerMensaje);
        }

        public async Task<ResultadoOperacion<Categoria>> Insertar(Categoria modelo)
        {
            var resultado = new ResultadoOperacion<Categoria>();
            var existe =
                db.Categoria.Any(c => c.Nombre.ToLower().Trim() == modelo.Nombre.ToLower().Trim());
            if (!existe)
            {
                modelo.Activo = true;
                db.Add(modelo);
                db.SaveChanges();
                return resultado.Exito(modelo);
            }
            return resultado.Error(TipoError.Negocio, CodigoError.RegistroDuplicado, string.Format(ObtenerMensaje));
        }
        public async Task<ResultadoOperacion<IEnumerable<Categoria>>> Obtener(object llave)
        {
            var resultado = new ResultadoOperacion<IEnumerable<Categoria>>();
            var sucursalesConsulta = db.Categoria.Where(c => c.Activo == true && c.categoriaId.ToString() == llave.ToString());
            if (sucursalesConsulta == null)
            {
                return resultado.Error(TipoError.AccesoDatos, CodigoError.ReglaNegocio, "No se encontraron registros");
            }

            resultado.Datos = sucursalesConsulta;
            return resultado.Exito();
        }

        public async Task<ResultadoOperacion<IEnumerable<Categoria>>> ObtenerTodos()
        {
            var resultado = new ResultadoOperacion<IEnumerable<Categoria>>();
            var sucursalesConsulta =  db.Categoria.Where(c => c.Activo == true).ToList();
            if (sucursalesConsulta.Count == 0)
            {
                return resultado.Error(TipoError.AccesoDatos, CodigoError.ReglaNegocio, "No se encontraron registros");
            }

            resultado.Datos = sucursalesConsulta;
            return resultado.Exito();
            //throw new NotImplementedException();
        }

       
    }
}

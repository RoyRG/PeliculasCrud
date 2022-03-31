using API.DATA;

using API.ENTIDADES;
using API.ENTIDADES.Entidades;
using API.ENTIDADES.Enums;
using API.NEGOCIOS.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.NEGOCIOS.Negocios
{
    public class NegocioPelicula : INegocioPelicula
    {
        private readonly Contexto db;
        string ObtenerMensaje = "";
        public NegocioPelicula(Contexto db)
        {
            this.db = db;
        }

        public async Task<ResultadoOperacion<Pelicula>> Actualizar(Pelicula modelo)
        {
            var resultado = new ResultadoOperacion<Pelicula>();

            var elemento = db.Pelicula.Where(c => c.Activo == true && c.Id == modelo.Id ).ToList().FirstOrDefault();
            elemento.Nombre = modelo.Nombre;

            if (modelo != null)
            {
                db.Update(elemento);
                db.SaveChanges();
                return resultado.Exito();
            }
            return resultado.Error(TipoError.Negocio, CodigoError.NoActualizado, ObtenerMensaje);
        }

        public IEnumerable<Pelicula> BuscarPelicula(string nombre)
        {
            IQueryable<Pelicula> query = db.Pelicula;
            if (!string.IsNullOrEmpty(nombre))
            {
                query = query.Where(e => e.Nombre.Contains(nombre.ToLower().Trim()) || e.Descripcion.Contains(nombre.ToLower().Trim()));
            }

            return query.ToList();
        }

        public async Task<ResultadoOperacion> Eliminar(object llave)
        {
            var resultado = new ResultadoOperacion<Pelicula>();
            var elemento = db.Pelicula.Where(c => c.Id.ToString() == llave.ToString()).FirstOrDefault();
            if (elemento != null)
            {
                elemento.Activo = false;
                db.SaveChanges();
                return resultado.Exito(elemento);
            }
            return resultado.Error(TipoError.Negocio, CodigoError.IdentificadorInvalido, ObtenerMensaje);
        }

        

        public async Task<ResultadoOperacion<Pelicula>> Insertar(Pelicula modelo)
        {
            var resultado = new ResultadoOperacion<Pelicula>();
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

        public async Task<ResultadoOperacion<IEnumerable<Pelicula>>> Obtener(object llave)
        {
            var resultado = new ResultadoOperacion<IEnumerable<Pelicula>>();
            var sucursalesConsulta = db.Pelicula.Where(c => c.Activo == true && c.Id.ToString() == llave.ToString());
            if (sucursalesConsulta == null)
            {
                return resultado.Error(TipoError.AccesoDatos, CodigoError.ReglaNegocio, "No se encontraron registros");
            }

            resultado.Datos = sucursalesConsulta;
            return resultado.Exito();
        }

        public async Task<ResultadoOperacion<IEnumerable<Pelicula>>> ObtenerCategoria(object llave)
        {
            var resultado = new ResultadoOperacion<IEnumerable<Pelicula>>();
            var sucursalesConsulta = db.Pelicula.Where(c => c.Activo == true && c.categoriaId.ToString() == llave.ToString()).Include(c => c.Categoria).ToList();
            if (sucursalesConsulta == null)
            {
                return resultado.Error(TipoError.AccesoDatos, CodigoError.ReglaNegocio, "No se encontraron registros");
            }

            resultado.Datos = sucursalesConsulta;
            return resultado.Exito();
        }

        public async Task<ResultadoOperacion<IEnumerable<Pelicula>>> ObtenerTodos()
        {
            var resultado = new ResultadoOperacion<IEnumerable<Pelicula>>();
            var sucursalesConsulta = db.Pelicula.Where(c => c.Activo == true).ToList();
            if (sucursalesConsulta.Count == 0)
            {
                return resultado.Error(TipoError.AccesoDatos, CodigoError.ReglaNegocio, "No se encontraron registros");
            }

            resultado.Datos = sucursalesConsulta;
            return resultado.Exito();
        }
    }
}

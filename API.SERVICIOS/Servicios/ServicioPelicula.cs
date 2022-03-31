using API.ENTIDADES;
using API.ENTIDADES.Entidades;
using API.ENTIDADES.Modelos;
using API.NEGOCIOS.Interfaces;
using API.SERVICIOS.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace API.SERVICIOS.Servicios
{
    public class ServicioPelicula : IServicioPelicula
    {
        
        private readonly INegocioPelicula negocioPelicula;
        private readonly IMapper mapper;
        
        public ServicioPelicula(INegocioPelicula negocioPelicula, IMapper mapper)
        {
            this.negocioPelicula = negocioPelicula;
            this.mapper = mapper;
           
        }

        public async Task<ResultadoOperacion<PeliculaModelo>> Actualizar(PeliculaModelo modelo)
        {
            var resultado = new ResultadoOperacion<PeliculaModelo>();
            try
            {
                var operacion = await negocioPelicula.Actualizar(mapper.Map<Pelicula>(modelo)).ConfigureAwait(false);
                resultado.Mensaje = operacion.Mensaje;
                if (operacion.EsExitosa)
                {
                    return resultado.Exito(mapper.Map<PeliculaModelo>(operacion.Datos));
                }

                return resultado.Error(operacion.Errores);
            }
            catch (Exception ex)
            {
                //var resultadoErrores = await elog.Error(ex);
                //return resultado.Errores();
                return null;
            }
        }

        public  IEnumerable<PeliculaModelo> BuscarPelicula(string nombre)
        {
            var respuesta = negocioPelicula.BuscarPelicula(mapper.Map<Pelicula>(nombre).ToString());
           return mapper.Map<IEnumerable<PeliculaModelo>>(respuesta);
        }

        public async Task<ResultadoOperacion> Eliminar(string id)
        {
            var resultado = new ResultadoOperacion<PeliculaModelo>();
            try
            {
                var operacion = await negocioPelicula.Eliminar(id).ConfigureAwait(false);
                resultado.Mensaje = operacion.Mensaje;

                if (operacion.EsExitosa)
                {
                    return resultado.Exito();
                }

                return resultado.Error(operacion.Errores);
            }
            catch (Exception ex)
            {
                //var resultadoErrores = await elog.Error(ex);
                //return resultado.Errores();
                return null;
            }
        }

        public async Task<ResultadoOperacion<PeliculaModelo>> Insertar(PeliculaModelo modelo)
        {
            var resultado = new ResultadoOperacion<PeliculaModelo>();
            try
            {
               
                var operacion = await negocioPelicula.Insertar(mapper.Map<Pelicula>(modelo)).ConfigureAwait(false);
                resultado.Mensaje = operacion.Mensaje;
                operacion.Datos.Activo = true;
                if (operacion.EsExitosa)
                {
                    return resultado.Exito(mapper.Map<PeliculaModelo>(operacion.Datos));
                }

                return resultado.Error(operacion.Errores);
            }
            catch (Exception ex)
            {
                //var resultadoErrores = await elog.Error(ex);
                //return resultado.Errores();
                return null;
            }
        }

        public async Task<ResultadoOperacion<IEnumerable<PeliculaModelo>>> Obtener(object llave)
        {
            var resultado = new ResultadoOperacion<IEnumerable<PeliculaModelo>>();
            try
            {
                var rSucursales = await negocioPelicula.Obtener(llave);
                var prueba = rSucursales.Datos;
                if (!rSucursales.EsExitosa)
                {
                    return resultado.Error(rSucursales.Errores);
                }
                var rModelo = mapper.Map<List<PeliculaModelo>>(rSucursales.Datos);
                resultado.Datos = rModelo;
                return resultado.Exito();
            }
            catch (Exception ex)
            {

                return resultado.Error(ex);
            }
        }

        public async Task<ResultadoOperacion<IEnumerable<PeliculaModelo>>> ObtenerCategoria(object llave)
        {

            var resultado = new ResultadoOperacion<IEnumerable<PeliculaModelo>>();
            try
            {
                var rSucursales = await negocioPelicula.ObtenerCategoria(llave);
                var prueba = rSucursales.Datos;
                if (!rSucursales.EsExitosa)
                {
                    return resultado.Error(rSucursales.Errores);
                }
                var rModelo = mapper.Map<List<PeliculaModelo>>(rSucursales.Datos);
                resultado.Datos = rModelo;
                return resultado.Exito();
            }
            catch (Exception ex)
            {

                return resultado.Error(ex);
            }
        }

        public async Task<ResultadoOperacion<IEnumerable<PeliculaModelo>>> ObtenerTodos()
        {
            var resultado = new ResultadoOperacion<IEnumerable<PeliculaModelo>>();
            try
            {
                var rSucursales = await negocioPelicula.ObtenerTodos();
                var prueba = rSucursales.Datos;
                if (!rSucursales.EsExitosa)
                {
                    return resultado.Error(rSucursales.Errores);
                }
                var rModelo = mapper.Map<List<PeliculaModelo>>(rSucursales.Datos);
                resultado.Datos = rModelo;
                return resultado.Exito();
            }
            catch (Exception ex)
            {

                return resultado.Error(ex);
            }
        }
    }
}

using API.ENTIDADES;
using API.ENTIDADES.Entidades;
using API.ENTIDADES.Modelos;
using API.NEGOCIOS.Interfaces;
using API.SERVICIOS.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace API.SERVICIOS.Servicios
{
    public class ServicioCategoria :  IServicioCategoria
    {
        private readonly INegocioCategoria negocioCategoria;
        private readonly IMapper mapper;
        private object elog;

        public ServicioCategoria(INegocioCategoria negocioCategoria, IMapper mapper) 
        {
            this.negocioCategoria = negocioCategoria;
            this.mapper = mapper;
            
        }

        public async Task<ResultadoOperacion<CategoriaModelo>> Actualizar(CategoriaModelo modelo)
        {
            var resultado = new ResultadoOperacion<CategoriaModelo>();
            try
            {
                var operacion = await negocioCategoria.Actualizar(mapper.Map<Categoria>(modelo)).ConfigureAwait(false);
                resultado.Mensaje = operacion.Mensaje;
                if (operacion.EsExitosa)
                {
                    return resultado.Exito(mapper.Map<CategoriaModelo>(operacion.Datos));
                }

                return resultado.Error(operacion.Errores);
            }
            catch (Exception ex)
            {
                //var resultadoErrores = await elog.Error(ex);
                //return resultado.Errores();
                return null;
            }

            //var result = new ResultadoOperacion<CategoriaModelo>();
            //var isOk = await negocioCategoria.Actualizar(mapper.Map<Categoria>(modelo));
            //return isOk.EsExitosa ? result.Exito(modelo) : result.Error(isOk.Errores);
        }
         
        public async Task<ResultadoOperacion> Eliminar(string id)
        {
            var resultado = new ResultadoOperacion<CategoriaModelo>();
            try
            {
                var operacion = await negocioCategoria.Eliminar(id).ConfigureAwait(false);
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
            //throw new ArgumentNullException();
            //var result = new ResultadoOperacion<CategoriaModelo>();
            //var isOk = await negocioCategoria.Eliminar(id);
            //return isOk.EsExitosa ? result.Exito() : result.Error(isOk.Errores);
        }

        public async Task<ResultadoOperacion<CategoriaModelo>> Insertar(CategoriaModelo modelo)
        {

            var resultado = new ResultadoOperacion<CategoriaModelo>();
            try
            {
                var operacion = await negocioCategoria.Insertar(mapper.Map<Categoria>(modelo)).ConfigureAwait(false);
                resultado.Mensaje = operacion.Mensaje;
                operacion.Datos.Activo = true;
                if (operacion.EsExitosa)
                {
                    return resultado.Exito(mapper.Map<CategoriaModelo>(operacion.Datos));
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



        public async Task<ResultadoOperacion<IEnumerable<CategoriaModelo>>> Obtener(object llave)
        {
            var resultado = new ResultadoOperacion<IEnumerable<CategoriaModelo>>();
            try
            {
                var rSucursales = await negocioCategoria.Obtener(llave);
                var prueba = rSucursales.Datos;
                if (!rSucursales.EsExitosa)
                {
                    return resultado.Error(rSucursales.Errores);
                }
                var rModelo = mapper.Map<List<CategoriaModelo>>(rSucursales.Datos);
                resultado.Datos = rModelo;
                return resultado.Exito();
            }
            catch (Exception ex)
            {

                return resultado.Error(ex);
            }
        }

        public async Task<ResultadoOperacion<IEnumerable<CategoriaModelo>>> ObtenerTodos()
        {
            var resultado = new ResultadoOperacion<IEnumerable<CategoriaModelo>>();
            try
            {
                var rSucursales = await negocioCategoria.ObtenerTodos();
                var prueba = rSucursales.Datos;
                if (!rSucursales.EsExitosa)
                {
                    return resultado.Error(rSucursales.Errores);
                }
                var rModelo = mapper.Map<List<CategoriaModelo>>(rSucursales.Datos);
                resultado.Datos = rModelo;
                return resultado.Exito();
            }
            catch (Exception ex)
            {

                return resultado.Error(ex);
            }
        }

        public async Task<ResultadoOperacion<IEnumerable<CategoriaModelo>>> ObtenerCategoria(object llave)
        {
            var resultado = new ResultadoOperacion<IEnumerable<CategoriaModelo>>();
            try
            {
                var rSucursales = await negocioCategoria.Obtener(llave);
                var prueba = rSucursales.Datos;
                if (!rSucursales.EsExitosa)
                {
                    return resultado.Error(rSucursales.Errores);
                }
                var rModelo = mapper.Map<List<CategoriaModelo>>(rSucursales.Datos);
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

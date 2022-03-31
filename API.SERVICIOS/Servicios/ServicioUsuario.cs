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
    public class ServicioUsuario : IServicioUsuario
    {
        private readonly INegocioUsuario negocioUsuario;
        private readonly IMapper mapper;
        public ServicioUsuario(INegocioUsuario negocioUsuario, IMapper mapper)
        {
            this.negocioUsuario = negocioUsuario;
            this.mapper = mapper;
        }

        public UsuarioModelo Login(string usuario, string password)
        {
           
            var rUsuario = negocioUsuario.Login(usuario, password);
            return mapper.Map<UsuarioModelo>(rUsuario);
        }

        public async Task<ResultadoOperacion<IEnumerable<UsuarioModelo>>> Obtener(object llave)
        {
            var resultado = new ResultadoOperacion<IEnumerable<UsuarioModelo>>();
            try
            {
                var rSucursales = await negocioUsuario.Obtener(llave);
                var prueba = rSucursales.Datos;
                if (!rSucursales.EsExitosa)
                {
                    return resultado.Error(rSucursales.Errores);
                }
                var rModelo = mapper.Map<List<UsuarioModelo>>(rSucursales.Datos);
                resultado.Datos = rModelo;
                return resultado.Exito();
            }
            catch (Exception ex)
            {

                return resultado.Error(ex);
            }
        }

        public async Task<ResultadoOperacion<IEnumerable<UsuarioModelo>>> ObtenerTodos()
        {
            var resultado = new ResultadoOperacion<IEnumerable<UsuarioModelo>>();
            try
            {
                var rSucursales = await negocioUsuario.ObtenerTodos();
                var prueba = rSucursales.Datos;
                if (!rSucursales.EsExitosa)
                {
                    return resultado.Error(rSucursales.Errores);
                }
                var rModelo = mapper.Map<List<UsuarioModelo>>(rSucursales.Datos);
                resultado.Datos = rModelo;
                return resultado.Exito();
            }
            catch (Exception ex)
            {

                return resultado.Error(ex);
            }
        }

        public UsuarioModelo Registro(UsuarioModelo usuario, string password)
        {
            var rUsuario = negocioUsuario.Registro(mapper.Map<Usuario>(usuario), password);
            return mapper.Map<UsuarioModelo>(rUsuario);
        }
    }
}

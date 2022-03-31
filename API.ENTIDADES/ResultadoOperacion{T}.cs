using API.ENTIDADES.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.ENTIDADES
{
    public class ResultadoOperacion<T> : ResultadoOperacion
    {
        public T Datos { get; set; }
        public int Registros { get; set; }
        public ResultadoOperacion<T> Exito(T datos, int registros = 0)
        {
            Datos = datos;
            this.Mensaje = Mensaje;
            this.Registros = registros;
            return Exito();
        }
        public new ResultadoOperacion<T> Exito()
        {
            base.Exito();
            this.Mensaje = Mensaje;
            return this;
        }
        public new ResultadoOperacion<T> Error(TipoError tipo, CodigoError codigo, string mensaje, int registros = 0)
        {
            base.Error(tipo, codigo, mensaje);
            return (ResultadoOperacion<T>)Error();
        }

        public new ResultadoOperacion<T> Error(List<ResultadoError> errores)
        {
            base.Error(errores);
            return (ResultadoOperacion<T>)Error();
        }

        public new ResultadoOperacion<T> Error(Exception ex)
        {
            base.Error(ex);
            return (ResultadoOperacion<T>)Error();
        }

        public new ResultadoOperacion<T> Error(TipoError negocio)
        {
            base.Error();
            return this;
        }
    }
}

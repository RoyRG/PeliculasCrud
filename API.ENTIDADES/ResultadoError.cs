using API.ENTIDADES.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.ENTIDADES
{
    public class ResultadoError
    {
        public ResultadoError()
        {
        }

        public ResultadoError(TipoError tipo, CodigoError codigo, string mensaje)
        {
            if (!string.IsNullOrWhiteSpace(mensaje))
            {
                Codigo = codigo;
                Tipo = tipo;
                Mensaje = mensaje;
            }
            else
            {
                throw new ArgumentException("mensaje");
            }
        }

        public ResultadoError(Exception ex)
        {
            if (ex != null)
            {
                Codigo = CodigoError.NoControlado;
                Tipo = TipoError.Excepcion;
                Mensaje = ex.Message;
#if DEBUG
                Exception = new Exception(ex.Message, ex.InnerException != null ? new Exception(ex.InnerException.Message) : null);
#endif
            }
            else
            {
                throw new ArgumentException("ex");
            }
        }

        public TipoError Tipo { get; set; }

        public CodigoError Codigo { get; set; }

        public string Mensaje { get; set; }

        public Exception Exception { get; private set; }
    }
}

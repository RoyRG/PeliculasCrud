using API.ENTIDADES.Enums;
using System;
using System.Collections.Generic;

namespace API.ENTIDADES
{
    public class ResultadoOperacion 
    {
        public bool EsExitosa { get; set; }
        public string Mensaje { get; set; }
        public List<ResultadoError> Errores { get; set; }
        public virtual ResultadoOperacion Exito()
        {
            EsExitosa = true;
            return this;
        }
        public virtual ResultadoOperacion Error(TipoError tipo, CodigoError codigo, string mensaje)
        {
            Errores = new List<ResultadoError>()
            {
                new ResultadoError(tipo, codigo, mensaje)
            };

            return Error();
        }

        public virtual ResultadoOperacion Error(List<ResultadoError> errores)
        {
            Errores = errores;
            return Error();
        }

        public virtual ResultadoOperacion Error(Exception ex)
        {
            Errores = new List<ResultadoError>() { new ResultadoError(ex) };
            return Error();
        }

        public virtual ResultadoOperacion Error()
        {
            EsExitosa = false;
            return this;
        }
    }
}

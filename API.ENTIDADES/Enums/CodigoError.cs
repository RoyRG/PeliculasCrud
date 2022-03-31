using System;
using System.Collections.Generic;
using System.Text;

namespace API.ENTIDADES.Enums
{
    public enum CodigoError
    {
        // De Aplicacion
        NoControlado = 0,
        ValidacionEntidad = 1,
        AccesoDatos = 2,
        ReglaNegocio = 3,
        WCF = 4,
        SMTP = 5,
        // Datos
        ReferenciaNula = 100,
        CampoRequerido = 101,
        ExcedeLongitud = 102,
        IdentificadorInvalido = 103,
        NoInsertado = 104,
        NoActualizado = 105,
        NoEliminado = 106,
        ParametroRequerido = 107,
        RegistroDuplicado = 108,
        RegistroNoEncontrado = 109,
        RangoNoValido = 110,
        PeticionBorrado = 111,
        // Servicio
        ErrorToken = 110,

        // Negocio
        RegistroBloqueado = 2000
    }
}

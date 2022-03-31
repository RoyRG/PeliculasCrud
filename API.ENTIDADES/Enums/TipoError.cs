using System;
using System.Collections.Generic;
using System.Text;

namespace API.ENTIDADES.Enums
{
    public enum TipoError
    {
        Excepcion = 0,
        AccesoDatos = 1,
        Negocio = 2,
        Validacion = 3,
        Servicio = 4,
        Autorizacion = 5,
        IntegracionSap = 6,
        CambioPassword = 99,
        Cancel201 = 201,
        Cancel202 = 202,
    }
}

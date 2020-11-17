using DAO.Context;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebCovid19.App_Start
{
    public class ValidacionesGenerales
    {
        public static void VerificarFechaFinDeNecesidades()
        {
            TpDBContext context = new TpDBContext();
            ServicioNecesidad servicioNecesidad = new ServicioNecesidad(context);
            servicioNecesidad.VerificarFechaFinDeNecesidades();
        }
    }
}
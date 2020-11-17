using DAO.Context;
using Entidades;
using Servicios;
using System.Collections.Generic;
using System.Web.Http;
using WebApi.Models;
using WebApi.Servicios;

namespace WebApi.Controllers
{
    public class NecesidadesController : ApiController
    {
        ServicioNecesidad necesidadServicio;
        ServiciosNecesidadApi serviciosNecesidadApi;
        public NecesidadesController()
        {
            TpDBContext ctx = new TpDBContext();
            necesidadServicio = new ServicioNecesidad(ctx);
            serviciosNecesidadApi = new ServiciosNecesidadApi(ctx);
        }

        public List<VMNecesidades> Get(int id)
        {
            int idUserLogueado = id;
            List<Necesidades> listaNecesidadesEF = necesidadServicio.TraerNecesidadesConDonacionesDelUserLogueado(idUserLogueado);
            List<NecesidadesDTO> listadoNecesidadesDTO = NecesidadesDTO.MapearListaEF(listaNecesidadesEF);
            List<VMNecesidades> ListavMNecesidades = serviciosNecesidadApi.AsignarDatosANecesidadesVM(listadoNecesidadesDTO, idUserLogueado);
            return ListavMNecesidades;

        }
    }
}

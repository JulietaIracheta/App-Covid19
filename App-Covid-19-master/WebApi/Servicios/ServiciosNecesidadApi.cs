using DAO.Context;
using Entidades.Enum;
using System.Collections.Generic;
using System.Linq;
using WebApi.Models;

namespace WebApi.Servicios
{
    public class ServiciosNecesidadApi
    {
        public ServiciosNecesidadApi(TpDBContext ctx)
        {
        }

        public List<VMNecesidades> AsignarDatosANecesidadesVM(List<NecesidadesDTO> listadoNecesidadesDTO, int idUserLogueado)
        {
            List<VMNecesidades> ListavMNecesidades = new List<VMNecesidades>();
            foreach (var necesidadDTO in listadoNecesidadesDTO)
            {
                VMNecesidades vmNececidad = new VMNecesidades();

                vmNececidad.IdNecesidad = necesidadDTO.IdNecesidad;
                vmNececidad.Nombre = necesidadDTO.Nombre;
                vmNececidad.TipoDonacion = (TipoDonacion)necesidadDTO.TipoDonacion; ;
                vmNececidad.Estado = (TipoEstadoNecesidad)necesidadDTO.Estado;

                if (necesidadDTO.TipoDonacion == (int)TipoDonacion.Monetaria)
                {
                    CalcularDineroRecaudadoYSepararDonacionesDelUserLogueado(necesidadDTO.NecesidadesDonacionesMonetarias, vmNececidad, idUserLogueado);
                }
                else
                {
                    CalcularCantidadTotalInsumosYSepararDonacionesInsumosDelUserLogueado(necesidadDTO.NecesidadesDonacionesInsumos, vmNececidad, idUserLogueado);
                }

                ListavMNecesidades.Add(vmNececidad);
            }

            OrdenarPorFechaDonacionActualAlaMasAntigua(ListavMNecesidades);
            return ListavMNecesidades;
        }

        private void CalcularDineroRecaudadoYSepararDonacionesDelUserLogueado(List<NecesidadesDonacionesMonetariasDTO> necesidadesDonacionesMonetariasDTO, VMNecesidades vmNecesidad, int idUserLogueado)
        {
            foreach (var ndm in necesidadesDonacionesMonetariasDTO)
            {
                foreach (var dm in ndm.DonacionesMonetarias)
                {
                    vmNecesidad.TotalDineroRecaudado += dm.Dinero;

                    // agrego la donacion del user logueado otra lista 
                    if (dm.IdUsuario == idUserLogueado)
                    {
                        vmNecesidad.MisDonacionesMonetarias.Add(dm);
                    }

                }
            }
        }
        private void CalcularCantidadTotalInsumosYSepararDonacionesInsumosDelUserLogueado(List<NecesidadesDonacionesInsumosDTO> necesidadesDonacionesInsumosDTO, VMNecesidades vmNececidad, int idUserLogueado)
        {
            foreach (var ndi in necesidadesDonacionesInsumosDTO)
            {
                // lista de donaciones insumos que no son del usuario logueado
                DonacionesInsumosVM donacionesInsumosVM = new DonacionesInsumosVM();
                donacionesInsumosVM.NombreNecesidadInsumos = ndi.Nombre;
                int totalDonado = 0;
                foreach (var di in ndi.DonacionesInsumos)
                {
                    totalDonado += di.Cantidad;

                    if (di.IdUsuario == idUserLogueado)
                    {
                        vmNececidad.MisDonacionesInsumos.Add(di);
                    }
                }
                donacionesInsumosVM.TotalRecaudado = totalDonado;
                vmNececidad.DonacionesInsumos.Add(donacionesInsumosVM);
            }
        }
        private void OrdenarPorFechaDonacionActualAlaMasAntigua(List<VMNecesidades> ListavMNecesidades)
        {
            foreach (var item in ListavMNecesidades)
            {
                item.MisDonacionesInsumos = item.MisDonacionesInsumos.OrderByDescending(o => o.FechaCreacion).ToList();
                item.MisDonacionesMonetarias = item.MisDonacionesMonetarias.OrderByDescending(o => o.FechaCreacion).ToList();
            }
        }
    }
}



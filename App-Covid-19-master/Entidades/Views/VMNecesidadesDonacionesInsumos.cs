using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entidades.Views
{
    public class VMNecesidadesDonacionesInsumos
    {
        /*[Required (ErrorMessage = "Debe ingresar una cantidad a donar")]
        [Range(0, 3500, ErrorMessage = "Puede donar hasta 3500 insumos")]*/
        public int Cantidad { get; set; }

        public int IdNecesidadDonacionInsumo { get; set; }

        public List<NecesidadesDonacionesInsumos> listaNombre { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class DonacionesInsumosMetadata
    {
        [Required(ErrorMessage = "Ingrese un insumo")]
        [Range (1,int.MaxValue,ErrorMessage = "Debe ingresar al menos 1 insumo..")]
        public int Cantidad { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.ValidationCustom
{
    class CheckDateGTNow : ValidationAttribute
    {
        public CheckDateGTNow()
        {
            ErrorMessage = "La fecha debe ser mayor al día de hoy.";
        }
        //Verifica que la fecha ingresada sea mayor a la actual, sino devuelve false
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }

            DateTime fecha = (DateTime)value;
            DateTime now = DateTime.Now;

            if (fecha > now)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

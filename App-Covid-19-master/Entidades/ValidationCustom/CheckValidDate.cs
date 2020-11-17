using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.ValidationCustom
{
    class CheckValidDate : ValidationAttribute
    {
        public CheckValidDate()
        {
            ErrorMessage = "Debe ser mayor de 18 años para registrarse.";
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false;
            }

            DateTime fecha = (DateTime)value;
            DateTime now = DateTime.Now;
            int year = now.Year - fecha.Year;

            if (year < 18)
            {
                return false;
            }
            else if (year > 18)
            {
                return true;
            }
            else if ((fecha.Month < now.Month) || (fecha.Day <= now.Day && fecha.Month <= now.Month))
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


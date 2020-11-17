using DAO;
using DAO.Context;
using Entidades;
using Entidades.Views;
using System;

namespace Servicios
{
    public class ServicioDonacionMonetaria
    {

        DonacionMonetariaDao DonacionMonetariaDao;

        public ServicioDonacionMonetaria(TpDBContext context)
        {
            DonacionMonetariaDao = new DonacionMonetariaDao(context);
        }

        public DonacionesMonetarias GuardarDonacionM(VMDonacionMonetaria donacionesMonetarias, int idUsuario)
        {
            DonacionesMonetarias donacionM = new DonacionesMonetarias()
            {
                Dinero = donacionesMonetarias.Dinero,
                IdNecesidadDonacionMonetaria = donacionesMonetarias.IdNecesidadDonacionMonetaria,
                IdUsuario = idUsuario,
                FechaCreacion = DateTime.Now,
                ArchivoTransferencia = ""
            };

            return DonacionMonetariaDao.Guardar(donacionM);
        }

        //ACA SE GUARDA EL NOMBRE DEL COMPROBANTE DE PAGO EN LA BD.
        public DonacionesMonetarias Actualizar(VMComprobantePago donaM)
        {
            return DonacionMonetariaDao.ActualizarComprobante(donaM);
        }

   
    }
}
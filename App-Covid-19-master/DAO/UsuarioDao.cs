using DAO.Context;
using DAO.Repository;
using Entidades;
using System.Collections.Generic;
using System.Linq;

namespace DAO
{
    public class UsuarioDao : BaseRepository<Usuarios>  //Uso de Generics
    {
        TpDBContext context;
        public UsuarioDao(TpDBContext contexto) : base(contexto)
        {
            context = contexto;
        }
        public Usuarios obtenerUsuarioPorEmail(string email)
        {
            Usuarios usuario = context.Usuarios.Where(k => k.Email == email).FirstOrDefault();
            return usuario;
        }

        public Usuarios obtenerUsuarioPorCodigoDeActivacion(string token)
        {
            Usuarios usuario = context.Usuarios.Where(k => k.Token == token).FirstOrDefault();
            return usuario;
        }

        public List<Usuarios> listadoUsuariosActivos()
        {
            List<Usuarios> listadoUsuarios = context.Usuarios.Where(a => a.Activo == true).ToList();
            return listadoUsuarios;
        }

        public Usuarios obtenerUsuarioPorUsername(string userName)
        {
            Usuarios usuarioObtenido = context.Usuarios.Where(o => o.UserName == userName).FirstOrDefault();
            return usuarioObtenido;
        }
    }
}

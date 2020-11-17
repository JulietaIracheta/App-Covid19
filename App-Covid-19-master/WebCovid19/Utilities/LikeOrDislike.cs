using Servicios;

namespace WebCovid19.Utilities
{
    public class LikeOrDislike
    {
        public bool AgregaLikeOrDislike(int idSession, string boton, int idNecesidad, ServicioNecesidadValoraciones servicioValoraciones)
        {
            bool likeOrDislike = servicioValoraciones.guardarValoracion(idSession, idNecesidad, boton);
            return likeOrDislike;
        }
    }
}
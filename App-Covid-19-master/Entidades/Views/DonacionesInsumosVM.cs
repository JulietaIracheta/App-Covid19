namespace Entidades.Views
{
    public class DonacionesInsumosVM
    {
        public string NombreNecesidadInsumos { get; set; }
        public int TotalRecaudado { get; set; }
        public DonacionesInsumosVM()
        {
            this.TotalRecaudado = 0;
        }
    }
}

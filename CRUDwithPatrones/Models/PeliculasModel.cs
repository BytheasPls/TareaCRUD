using System.ComponentModel.DataAnnotations;

namespace CRUDwithPatrones.Models
{
    public class PeliculasModel
    {
        public int IdPelicula { get; set; }

        [Required(ErrorMessage = "El campo nombre es olbigatorio")]
        public string? NombrePelicula { get; set; }
        [Required(ErrorMessage = "El campo sinopsis es olbigatorio")]
        public string? Sinopsis { get; set; }
        [Required(ErrorMessage = "El campo fecha es olbigatorio")]
        public DateOnly FechaLanzamiento { get; set; }


    }
}

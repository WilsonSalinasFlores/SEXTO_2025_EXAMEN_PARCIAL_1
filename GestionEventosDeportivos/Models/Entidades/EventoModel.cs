using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionEventosDeportivos.Models.Entidades
{

    [Table("Eventos")]
    public class EventoModel
    {
        [Required]
        public int EventoId { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        [Required]
        public string Ubicacion { get; set; }
        [Required]
        public string Descripcion { get; set; }

        public ICollection<InscripcionModel> Inscripciones { get; set; }
    }

}

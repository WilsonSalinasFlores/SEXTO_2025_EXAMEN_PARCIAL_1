using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionEventosDeportivos.Models.Entidades
{

    [Table("Eventos")]

    public class EventoModel
    {
        [Key]
        public int EventoId { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        public string Ubicacion { get; set; }

        public string Descripcion { get; set; }

        public ICollection<InscripcionModel> Inscripciones { get; set; }
    }


}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace GestionEventosDeportivos.Models.Entidades
{


    [Table("Eventos")]
    public class EventoModel
    {
        public EventoModel()
        {
            Inscripciones = new HashSet<InscripcionModel>();
            Eliminado = false;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EventoId { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public DateTime Fecha { get; set; }
        [Required]
        public string Ubicacion { get; set; }
        
        [Required]
        public string Descripcion { get; set; }

        [AllowNull]
        public bool ? Eliminado { get; set; } = false;
        public ICollection<InscripcionModel> Inscripciones { get; set; }
    }


}

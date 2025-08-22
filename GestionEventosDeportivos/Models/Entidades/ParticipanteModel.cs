using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace GestionEventosDeportivos.Models.Entidades
{

    [Table("Paticipantes")]
    public class ParticipanteModel

    {
        public ParticipanteModel()
        {
            Inscripciones = new HashSet<InscripcionModel>();
        }

        [Key]
        public int ParticipanteId { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Apellido { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Telefono { get; set; }

        [AllowNull] 
        public bool? Eliminado { get; set; } = false;

        public ICollection<InscripcionModel> Inscripciones { get; set; }

        
    }


public class ParticipantePorEventoDto
{
    public int InscripcionId { get; set; }
    public int ParticipanteId { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string Email { get; set; }
    public DateTime FechaInscripcion { get; set; }

    public string Telefono { get; set; }
}

}

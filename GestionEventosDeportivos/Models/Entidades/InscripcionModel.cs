using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionEventosDeportivos.Models.Entidades
{
    [Table("Inscripciones")]
    public class InscripcionModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InscripcionId { get; set; }

        [ForeignKey("EventoModel")]
        public int EventoId { get; set; }
        public EventoModel Evento { get; set; }

        [ForeignKey("ParticipanteModel")]
        public int ParticipanteId { get; set; }
        public ParticipanteModel Participante { get; set; }

        public DateTime FechaInscripcion { get; set; }
    }

}

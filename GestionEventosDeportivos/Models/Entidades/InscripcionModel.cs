using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

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
        [JsonIgnore]
        public EventoModel Evento { get; set; }

        [ForeignKey("ParticipanteModel")]
        public int ParticipanteId { get; set; }
        [JsonIgnore]
        public ParticipanteModel Participante { get; set; }

        public DateTime FechaInscripcion { get; set; }
    }

    public class InscripcionDto
    {
        public int EventoId { get; set; }
        public int ParticipanteId { get; set; }
    }


}

namespace GestionEventosDeportivos.Models.Entidades
{
    // Models/InscripcionModel.cs
    public class InscripcionModel
    {
        public int InscripcionId { get; set; }

        public int EventoId { get; set; }
        public EventoModel Evento { get; set; }

        public int ParticipanteId { get; set; }
        public ParticipanteModel Participante { get; set; }

        public DateTime FechaInscripcion { get; set; }
    }

}

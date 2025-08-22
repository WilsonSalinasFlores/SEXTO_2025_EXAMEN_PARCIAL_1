namespace GestionEventosDeportivos.Models.Entidades
{

    public class ParticipanteModel
    {
        public int ParticipanteId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }

        public ICollection<InscripcionModel> Inscripciones { get; set; }
    }

}

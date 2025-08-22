using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public ICollection<InscripcionModel> Inscripciones { get; set; }
    }


}

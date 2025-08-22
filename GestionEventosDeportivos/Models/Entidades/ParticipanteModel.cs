using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionEventosDeportivos.Models.Entidades
{

    [Table("Eventos")]
    public class ParticipanteModel
    {
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

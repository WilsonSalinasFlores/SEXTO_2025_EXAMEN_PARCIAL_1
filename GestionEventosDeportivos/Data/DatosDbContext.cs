using GestionEventosDeportivos.Models.Entidades;
using Microsoft.EntityFrameworkCore;

namespace GestionEventosDeportivos.Data
{
    public class DatosDbContext:DbContext
    {
        public DatosDbContext(DbContextOptions<DatosDbContext> opciones) : base(opciones)
        {
        }

        public DbSet<EventoModel> Eventos { get; set; }
        public DbSet<ParticipanteModel> Participantes { get; set; }
        public DbSet<InscripcionModel> Inscripciones { get; set; }

    }
}

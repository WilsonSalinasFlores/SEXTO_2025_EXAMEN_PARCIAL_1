using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionEventosDeportivos.Data;
using GestionEventosDeportivos.Models.Entidades;

namespace GestionEventosDeportivos.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantesApiController : ControllerBase
    {
        private readonly DatosDbContext _context;

        public ParticipantesApiController(DatosDbContext context)
        {
            _context = context;
        }

        // GET: api/ParticipantesApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParticipanteModel>>> GetParticipantes()
        {
            return await _context.Participantes.ToListAsync();
        }

        // GET: api/ParticipantesApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ParticipanteModel>> GetParticipanteModel(int id)
        {
            var participanteModel = await _context.Participantes.FindAsync(id);

            if (participanteModel == null)
            {
                return NotFound();
            }

            return participanteModel;
        }

        // GET: api/ParticipantesApi/Evento/5
        [HttpGet("ParticipantesPorEvento/{EventoId}")]
        public async Task<ActionResult<List<ParticipantePorEventoDto>>> GetParticipanteByEventModel(int EventoId)
        {
            var participantes = await _context.Inscripciones
                .Include(i => i.Participante)
                .Where(i => i.EventoId == EventoId)
                .Select(i => new ParticipantePorEventoDto
                {
                    InscripcionId = i.InscripcionId,
                    ParticipanteId = i.ParticipanteId,
                    Nombre = i.Participante.Nombre,
                    Apellido = i.Participante.Apellido,
                    Email = i.Participante.Email,
                    Telefono = i.Participante.Telefono,
                    FechaInscripcion = i.FechaInscripcion
                })
                .ToListAsync();

            if (participantes == null || participantes.Count == 0)
            {
                return NotFound();
            }

            return participantes;
        }



        // PUT: api/ParticipantesApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParticipanteModel(int id, ParticipanteModel participanteModel)
        {
            if (id != participanteModel.ParticipanteId)
            {
                return BadRequest();
            }

            _context.Entry(participanteModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParticipanteModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ParticipantesApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ParticipanteModel>> PostParticipanteModel(ParticipanteModel participanteModel)
        {
            _context.Participantes.Add(participanteModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetParticipanteModel", new { id = participanteModel.ParticipanteId }, participanteModel);
        }

        // DELETE: api/ParticipantesApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParticipanteModel(int id)
        {
            var participanteModel = await _context.Participantes.FindAsync(id);
            if (participanteModel == null)
            {
                return NotFound();
            }

            _context.Participantes.Remove(participanteModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ParticipanteModelExists(int id)
        {
            return _context.Participantes.Any(e => e.ParticipanteId == id);
        }
    }
}

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
    public class InscripcionesApiController : ControllerBase
    {
        private readonly DatosDbContext _context;

        public InscripcionesApiController(DatosDbContext context)
        {
            _context = context;
        }

        // GET: api/InscripcionesApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InscripcionModel>>> GetInscripciones()
        {
            return await _context.Inscripciones.ToListAsync();
        }

        // GET: api/InscripcionesApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InscripcionModel>> GetInscripcionModel(int id)
        {
            var inscripcionModel = await _context.Inscripciones.FindAsync(id);

            if (inscripcionModel == null)
            {
                return NotFound();
            }

            return inscripcionModel;
        }

        [HttpGet("ValidarParticipante/{eventoId}/{participanteId}")]
        public async Task<ActionResult<bool>> ValidarParticipante(int eventoId, int participanteId)
        {
            bool existe = await _context.Inscripciones
                .AnyAsync(i => i.EventoId == eventoId && i.ParticipanteId == participanteId);

            return Ok(existe);
        }


        // POST: api/InscripcionesApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<InscripcionModel>> PostInscripcionModel(InscripcionDto dto)
        {
            var inscripcion = new InscripcionModel
            {
                EventoId = dto.EventoId,
                ParticipanteId = dto.ParticipanteId,
                FechaInscripcion = DateTime.Now
            };

            _context.Inscripciones.Add(inscripcion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInscripcionModel", new { id = inscripcion.InscripcionId }, inscripcion);
        }


        // DELETE: api/InscripcionesApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInscripcionModel(int id)
        {
            var inscripcionModel = await _context.Inscripciones.FindAsync(id);
            if (inscripcionModel == null)
            {
                return NotFound();
            }

            _context.Inscripciones.Remove(inscripcionModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InscripcionModelExists(int id)
        {
            return _context.Inscripciones.Any(e => e.InscripcionId == id);
        }
    }
}

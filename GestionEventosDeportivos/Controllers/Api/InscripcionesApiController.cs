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

        // PUT: api/InscripcionesApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInscripcionModel(int id, InscripcionModel inscripcionModel)
        {
            if (id != inscripcionModel.InscripcionId)
            {
                return BadRequest();
            }

            _context.Entry(inscripcionModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InscripcionModelExists(id))
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

        // POST: api/InscripcionesApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<InscripcionModel>> PostInscripcionModel(InscripcionModel inscripcionModel)
        {
            _context.Inscripciones.Add(inscripcionModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInscripcionModel", new { id = inscripcionModel.InscripcionId }, inscripcionModel);
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

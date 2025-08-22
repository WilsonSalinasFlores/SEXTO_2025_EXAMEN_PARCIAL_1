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
    public class EventosApiController : ControllerBase
    {
        private readonly DatosDbContext _context;

        public EventosApiController(DatosDbContext context)
        {
            _context = context;
        }

        // GET: api/EventosApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventoModel>>> GetEventos()
        {
            return await _context.Eventos.ToListAsync();
        }

        // GET: api/EventosApi/activos
        [HttpGet("activos")]
        public async Task<ActionResult<IEnumerable<EventoModel>>> GetEventosActivos()
        {
            var eventos = _context.Eventos.Where(e => e.Fecha > DateTime.Now).ToListAsync();
            return await (eventos);

        }

        // GET: api/EventosApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EventoModel>> GetEventoModel(int id)
        {
            var eventoModel = await _context.Eventos.FindAsync(id);

            if (eventoModel == null)
            {
                return NotFound();
            }

            return eventoModel;
        }

        // PUT: api/EventosApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEventoModel(int id, EventoModel eventoModel)
        {
            if (id != eventoModel.EventoId)
            {
                return BadRequest();
            }

            _context.Entry(eventoModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventoModelExists(id))
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

        // POST: api/EventosApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        [HttpPost]
        public async Task<ActionResult<EventoModel>> PostEventoModel(EventoModel eventoModel)
        {
            _context.Eventos.Add(eventoModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEventoModel", new { id = eventoModel.EventoId }, eventoModel);
        }

        // DELETE: api/EventosApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEventoModel(int id)
        {
            var eventoModel = await _context.Eventos.FindAsync(id);
            if (eventoModel == null)
            {
                return NotFound();
            }

            _context.Eventos.Remove(eventoModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EventoModelExists(int id)
        {
            return _context.Eventos.Any(e => e.EventoId == id);
        }
    }
}

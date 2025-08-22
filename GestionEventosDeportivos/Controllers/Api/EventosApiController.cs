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
            return await _context.Eventos.Where(e => e.Eliminado == false).ToListAsync();
        }

        // GET: api/EventosApi/activos
        [HttpGet("activos")]
        public async Task<ActionResult<IEnumerable<EventoModel>>> GetEventosActivos()
        {
            var eventos = _context.Eventos.Where(e => e.Fecha > DateTime.Now && e.Eliminado==false ).ToListAsync();
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


        private bool EventoModelExists(int id)
        {
            return _context.Eventos.Any(e => e.EventoId == id);
        }
    }
}

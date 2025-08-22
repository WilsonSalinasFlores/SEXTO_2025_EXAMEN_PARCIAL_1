using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestionEventosDeportivos.Data;
using GestionEventosDeportivos.Models.Entidades;

namespace GestionEventosDeportivos.Controllers
{
    public class InscripcionController : Controller
    {
        private readonly DatosDbContext _context;

        public InscripcionController(DatosDbContext context)
        {
            _context = context;
        }

        // GET: Inscripcion
        public async Task<IActionResult> Index()
        {
            var datosDbContext = _context.Inscripciones.Include(i => i.Evento).Include(i => i.Participante);
            return View(await datosDbContext.ToListAsync());
        }

        // GET: Inscripcion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inscripcionModel = await _context.Inscripciones
                .Include(i => i.Evento)
                .Include(i => i.Participante)
                .FirstOrDefaultAsync(m => m.InscripcionId == id);
            if (inscripcionModel == null)
            {
                return NotFound();
            }

            return View(inscripcionModel);
        }

        // GET: Inscripcion/Create
        public IActionResult Create()
        {
            ViewData["EventoId"] = new SelectList(_context.Eventos, "EventoId", "Nombre");
            ViewData["ParticipanteId"] = new SelectList(_context.Participantes, "ParticipanteId", "Apellido");
            return View();
        }

        // POST: Inscripcion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InscripcionId,EventoId,ParticipanteId,FechaInscripcion")] InscripcionModel inscripcionModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inscripcionModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventoId"] = new SelectList(_context.Eventos, "EventoId", "Nombre", inscripcionModel.EventoId);
            ViewData["ParticipanteId"] = new SelectList(_context.Participantes, "ParticipanteId", "Apellido", inscripcionModel.ParticipanteId);
            return View(inscripcionModel);
        }

        // GET: Inscripcion/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inscripcionModel = await _context.Inscripciones.FindAsync(id);
            if (inscripcionModel == null)
            {
                return NotFound();
            }
            ViewData["EventoId"] = new SelectList(_context.Eventos, "EventoId", "Nombre", inscripcionModel.EventoId);
            ViewData["ParticipanteId"] = new SelectList(_context.Participantes, "ParticipanteId", "Apellido", inscripcionModel.ParticipanteId);
            return View(inscripcionModel);
        }

        // POST: Inscripcion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InscripcionId,EventoId,ParticipanteId,FechaInscripcion")] InscripcionModel inscripcionModel)
        {
            if (id != inscripcionModel.InscripcionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inscripcionModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InscripcionModelExists(inscripcionModel.InscripcionId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventoId"] = new SelectList(_context.Eventos, "EventoId", "Nombre", inscripcionModel.EventoId);
            ViewData["ParticipanteId"] = new SelectList(_context.Participantes, "ParticipanteId", "Apellido", inscripcionModel.ParticipanteId);
            return View(inscripcionModel);
        }

        // GET: Inscripcion/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inscripcionModel = await _context.Inscripciones
                .Include(i => i.Evento)
                .Include(i => i.Participante)
                .FirstOrDefaultAsync(m => m.InscripcionId == id);
            if (inscripcionModel == null)
            {
                return NotFound();
            }

            return View(inscripcionModel);
        }

        // POST: Inscripcion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inscripcionModel = await _context.Inscripciones.FindAsync(id);
            if (inscripcionModel != null)
            {
                _context.Inscripciones.Remove(inscripcionModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InscripcionModelExists(int id)
        {
            return _context.Inscripciones.Any(e => e.InscripcionId == id);
        }
    }
}

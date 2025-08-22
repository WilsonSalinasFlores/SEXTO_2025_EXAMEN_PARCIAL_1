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
    public class ParticipanteController : Controller
    {
        private readonly DatosDbContext _context;

        public ParticipanteController(DatosDbContext context)
        {
            _context = context;
        }

        // GET: Participante
        public async Task<IActionResult> Index()
        {
            return View(await _context.Participantes.Where(p => p.Eliminado == false ).ToListAsync());
        }

        // GET: Participante/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var participanteModel = await _context.Participantes
                .FirstOrDefaultAsync(m => m.ParticipanteId == id);
            if (participanteModel == null)
            {
                return NotFound();
            }

            return View(participanteModel);
        }

        // GET: Participante/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Participante/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ParticipanteId,Nombre,Apellido,Email,Telefono")] ParticipanteModel participanteModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(participanteModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(participanteModel);
        }

        // GET: Participante/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var participanteModel = await _context.Participantes.FindAsync(id);
            if (participanteModel == null)
            {
                return NotFound();
            }
            return View(participanteModel);
        }

        // POST: Participante/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ParticipanteId,Nombre,Apellido,Email,Telefono")] ParticipanteModel participanteModel)
        {
            if (id != participanteModel.ParticipanteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(participanteModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParticipanteModelExists(participanteModel.ParticipanteId))
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
            return View(participanteModel);
        }

        // GET: Participante/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var participanteModel = await _context.Participantes
                .FirstOrDefaultAsync(m => m.ParticipanteId == id);
            if (participanteModel == null)
            {
                return NotFound();
            }

            return View(participanteModel);
        }

        // POST: Participante/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var participanteModel = await _context.Participantes.FindAsync(id);
            if (participanteModel != null)
            {
                participanteModel.Eliminado = true;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParticipanteModelExists(int id)
        {
            return _context.Participantes.Any(e => e.ParticipanteId == id);
        }
    }
}

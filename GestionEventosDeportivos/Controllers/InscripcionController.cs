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
            
            return View();
        }

    }
}

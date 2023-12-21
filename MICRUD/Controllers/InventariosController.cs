using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MICRUD.Models;

namespace MICRUD.Controllers
{
    public class InventariosController : Controller
    {
        private readonly Base1Context _context;

        public InventariosController(Base1Context context)
        {
            _context = context;
        }

        // GET: Inventarios
        public async Task<IActionResult> Index()
        {
              return _context.Inventarios != null ? 
                          View(await _context.Inventarios.ToListAsync()) :
                          Problem("Entity set 'Base1Context.Inventarios'  is null.");
        }

        private bool InventarioExists(int id)
        {
          return (_context.Inventarios?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

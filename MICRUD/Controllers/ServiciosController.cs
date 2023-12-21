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
    public class ServiciosController : Controller
    {
        private readonly Base1Context _context;

        public ServiciosController(Base1Context context)
        {
            _context = context;
        }

        // GET: Servicios
        public async Task<IActionResult> Index()
        {
              return _context.Servicio != null ? 
                          View(await _context.Servicio.ToListAsync()) :
                          Problem("Entity set 'Base1Context.Servicio'  is null.");
        }

        // GET: Servicios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Servicio == null)
            {
                return NotFound();
            }

            var servicios = await _context.Servicio
                .FirstOrDefaultAsync(m => m.Id == id);
            if (servicios == null)
            {
                return NotFound();
            }

            return View(servicios);
        }

        // GET: Servicios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Servicios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Descripcion,PrecioUnidad,FechaEntrada")] Servicios servicios)
        {
            if (ModelState.IsValid)
            {
                _context.Add(servicios);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(servicios);
        }

        // GET: Servicios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Servicio == null)
            {
                return NotFound();
            }

            var servicios = await _context.Servicio.FindAsync(id);
            if (servicios == null)
            {
                return NotFound();
            }
            return View(servicios);
        }

        // POST: Servicios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Descripcion,PrecioUnidad,FechaEntrada")] Servicios servicios)
        {
            if (id != servicios.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(servicios);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiciosExists(servicios.Id))
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
            return View(servicios);
        }

        // GET: Servicios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Servicio == null)
            {
                return NotFound();
            }

            var servicios = await _context.Servicio
                .FirstOrDefaultAsync(m => m.Id == id);
            if (servicios == null)
            {
                return NotFound();
            }

            return View(servicios);
        }

        // POST: Servicios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Servicio == null)
            {
                return Problem("Entity set 'Base1Context.Servicio'  is null.");
            }
            var servicios = await _context.Servicio.FindAsync(id);
            if (servicios != null)
            {
                _context.Servicio.Remove(servicios);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiciosExists(int id)
        {
          return (_context.Servicio?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

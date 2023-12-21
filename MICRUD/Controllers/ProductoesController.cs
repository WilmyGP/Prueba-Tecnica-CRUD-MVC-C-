using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Debugger.Contracts.HotReload;
using MICRUD.Models;

namespace MICRUD.Controllers
{
    public class ProductoesController : Controller
    {
        private readonly Base1Context _context;
        private readonly Base1Context _context2;

        public ProductoesController(Base1Context context, Base1Context context2)
        {
            _context = context;
            _context2 = context2;
        }

        // GET: Productoes
        public async Task<IActionResult> Index()
        {
            return _context.Productos != null ? 
                          View(await _context.Productos.ToListAsync()) :
                          Problem("Entity set 'Base1Context.Productos'  is null.");
        }

        // GET: Productoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: Productoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Productoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Precio,FechaExpiracion,Cantidad")] Producto producto)
        {
            Inventario inventario = new Inventario();

            inventario.Nombre = producto.Nombre;
            inventario.Cantidad = producto.Cantidad;

            if (ModelState.IsValid)
            {
                _context.Add(producto);
                _context2.Add(inventario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(producto);
        }

        // GET: Productoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Productoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Precio,FechaExpiracion,Cantidad")] Producto producto)
        {
            var inventario = await _context.Inventarios.FindAsync(id);

            inventario.Cantidad = producto.Cantidad;
            inventario.Nombre = producto.Nombre;
            
            if (id != producto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producto);
                    _context2.Update(inventario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.Id))
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
            return View(producto);
        }

        // GET: Productoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Productoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Productos == null)
            {
                return Problem("Entity set 'Base1Context.Productos'  is null.");
            }

            var producto = await _context.Productos.FindAsync(id);
            var inventario = await _context.Inventarios.FindAsync(id);

            inventario.FechaSalida = DateTime.Now;
            
            _context2.Update(inventario);

            if (producto != null)
            {
                _context.Productos.Remove(producto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Sales(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Sales(int id, [Bind("Id,Nombre,Precio,FechaExpiracion,Cantidad")] Producto producto, int cantidad)
        {
            var inventario = await _context.Inventarios.FindAsync(id);
            producto.Cantidad = inventario.Cantidad;
            producto.Cantidad -= cantidad;
            inventario.Cantidad = producto.Cantidad;


            inventario.cantidadVendidos = cantidad;


            if (id != producto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producto);
                    _context2.Update(inventario);
                    await _context.SaveChangesAsync();
                    await _context2.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.Id))
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
            return View(producto);
        }

        private bool ProductoExists(int id)
        {
          return (_context.Productos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

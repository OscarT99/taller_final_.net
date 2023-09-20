using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using taller_final_cruds.Models;
using X.PagedList;

namespace taller_final_cruds.Controllers
{
    public class VentasController : Controller
    {
        private readonly CrudCountechNetContext _context;



        public VentasController(CrudCountechNetContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index(string buscar, string filtrar, int? page)
        {

            var ventas = from Venta in _context.Ventas select Venta;
            if (!String.IsNullOrEmpty(buscar))
            {
                if (int.TryParse(buscar, out int numeroBuscado))
                {
                    ventas = ventas.Where(venta => venta.Pedido == numeroBuscado);
                }
                
            }
            ViewData["FiltroFecha"] = filtrar == "FechaAscendente" ? "FechaDescendente" : "FechaAscendente";
            switch(filtrar)
            {

                case "FechaDescendente":
                    ventas = ventas.OrderByDescending(ventas => ventas.FechaVenta);
                    break;
                case "FechaAscendente":
                    ventas = ventas.OrderBy(ventas => ventas.FechaVenta);
                    break;
                default:
                    ventas = ventas.OrderByDescending(venta => venta.FechaVenta);
                    break;
            }


            int pageSize = 5;
            int pageNumber = (page ?? 1);
            
            var pageVentas = await ventas.ToPagedListAsync(pageNumber, pageSize);

            ViewData["Buscar"] = buscar;
            ViewData["Page"] = pageNumber;

            return View(pageVentas);
            //return View(await ventas.ToListAsync());
        }



        /*
        // GET: Ventas
        public async Task<IActionResult> Index()
        {
            return _context.Ventas != null ?
                        View(await _context.Ventas.ToListAsync()) :
                        Problem("Entity set 'CrudCountechNetContext.Ventas'  is null.");
        }
        */


        // GET: Ventas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ventas == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas
                .FirstOrDefaultAsync(m => m.IdVenta == id);
            if (venta == null)
            {
                return NotFound();
            }

            return View(venta);
        }

        // GET: Ventas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ventas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdVenta,Pedido,FechaVenta,FechaRegistro,ValorTotal,FormaDePago,Observaciones,Estado")] Venta venta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(venta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(venta);
        }

        // GET: Ventas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ventas == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas.FindAsync(id);
            if (venta == null)
            {
                return NotFound();
            }
            return View(venta);
        }

        // POST: Ventas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdVenta,Pedido,FechaVenta,FechaRegistro,ValorTotal,FormaDePago,Observaciones,Estado")] Venta venta)
        {
            if (id != venta.IdVenta)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(venta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VentaExists(venta.IdVenta))
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
            return View(venta);
        }

        // GET: Ventas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ventas == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas
                .FirstOrDefaultAsync(m => m.IdVenta == id);
            if (venta == null)
            {
                return NotFound();
            }

            return View(venta);
        }

        // POST: Ventas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ventas == null)
            {
                return Problem("Entity set 'CrudCountechNetContext.Ventas'  is null.");
            }
            var venta = await _context.Ventas.FindAsync(id);
            if (venta != null)
            {
                _context.Ventas.Remove(venta);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VentaExists(int id)
        {
            return (_context.Ventas?.Any(e => e.IdVenta == id)).GetValueOrDefault();
        }
    }
}
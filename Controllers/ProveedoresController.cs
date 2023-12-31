﻿using System;
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
    public class ProveedoresController : Controller
    {
        private readonly CrudCountechNetContext _context;

        public ProveedoresController(CrudCountechNetContext context)
        {
            _context = context;
        }

        // GET: Proveedores
        public async Task<IActionResult> Index(String buscar, string filtrar, int? page)
        {
            var proveedores = from Proveedore in _context.Proveedores select Proveedore;

            if (!String.IsNullOrEmpty(buscar))
            {
                proveedores = proveedores.Where(s => s.NumeroIdentificacion!.Contains(buscar));
            }

            ViewData["FiltroNombre"] = filtrar == "NombreAscendente" ? "NombreDescendente" : "NombreAscendente";
            switch (filtrar)
            {

                case "NombreDescendente":
                    proveedores = proveedores.OrderByDescending(proveedores => proveedores.NombreComercial);
                    break;
                case "NombreAscendente":
                    proveedores = proveedores.OrderBy(proveedores => proveedores.NombreComercial);
                    break;
                default:
                    proveedores = proveedores.OrderByDescending(proveedores => proveedores.NombreComercial);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            var pageProveedores = await proveedores.ToPagedListAsync(pageNumber, pageSize);

            ViewData["Buscar"] = buscar;
            ViewData["Page"] = pageNumber;


            return View(pageProveedores);
        }

        // GET: Proveedores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Proveedores == null)
            {
                return NotFound();
            }

            var proveedore = await _context.Proveedores
                .FirstOrDefaultAsync(m => m.IdProveedor == id);
            if (proveedore == null)
            {
                return NotFound();
            }

            return View(proveedore);
        }

        // GET: Proveedores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Proveedores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProveedor,TipoProveedor,TipoIdentificacion,NumeroIdentificacion,RazonSocial,NombreComercial,Ciudad,Direccion,Contacto,Telefono,Email,Estado")] Proveedore proveedore)
        {
            if (ModelState.IsValid)
            {
                _context.Add(proveedore);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(proveedore);
        }

        // GET: Proveedores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Proveedores == null)
            {
                return NotFound();
            }

            var proveedore = await _context.Proveedores.FindAsync(id);
            if (proveedore == null)
            {
                return NotFound();
            }
            return View(proveedore);
        }

        // POST: Proveedores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProveedor,TipoProveedor,TipoIdentificacion,NumeroIdentificacion,RazonSocial,NombreComercial,Ciudad,Direccion,Contacto,Telefono,Email,Estado")] Proveedore proveedore)
        {
            if (id != proveedore.IdProveedor)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(proveedore);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProveedoreExists(proveedore.IdProveedor))
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
            return View(proveedore);
        }

        // GET: Proveedores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Proveedores == null)
            {
                return NotFound();
            }

            var proveedore = await _context.Proveedores
                .FirstOrDefaultAsync(m => m.IdProveedor == id);
            if (proveedore == null)
            {
                return NotFound();
            }

            return View(proveedore);
        }

        // POST: Proveedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Proveedores == null)
            {
                return Problem("Entity set 'CrudCountechNetContext.Proveedores'  is null.");
            }
            var proveedore = await _context.Proveedores.FindAsync(id);
            if (proveedore != null)
            {
                _context.Proveedores.Remove(proveedore);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProveedoreExists(int id)
        {
          return (_context.Proveedores?.Any(e => e.IdProveedor == id)).GetValueOrDefault();
        }
    }
}

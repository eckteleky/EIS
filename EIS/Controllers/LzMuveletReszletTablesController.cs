using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EIS.EISModels;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;

namespace EIS.Controllers
{
    public class LzMuveletReszletTablesController : Controller
    {
        private readonly EISDBContext _context;

        public LzMuveletReszletTablesController(EISDBContext context)
        {
            _context = context;
        }

        // GET: LzMuveletReszletTables
        public async Task<IActionResult> Index()
        {
            HttpContext.Session.SetInt32("LZGroup", 0);
            HttpContext.Session.SetInt32("DayGroup", 1);
            HttpContext.Session.SetInt32("AddGroup", 1);
            HttpContext.Session.SetInt32("HeaderGroup", 1);
            return View(await _context.LzMuveletReszletTable.ToListAsync());
        }

        // GET: LzMuveletReszletTables/Create
        public IActionResult Create()
        {
            LzMuveletReszletTable lzMuveletReszletTable = new LzMuveletReszletTable()
            {
                Id = 0
            };
            return PartialView("_CreateModalPartion", lzMuveletReszletTable);
        }

        // POST: LzMuveletReszletTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Muveletszam,Muveletreszlet,Description,Search,Interval,Munkanap,Unnep,Pihenonap,AllCode,Empty")] LzMuveletReszletTable lzMuveletReszletTable)
        {
            if (ModelState.IsValid)
            {
                if (lzMuveletReszletTable.Muveletreszlet.IsNullOrEmpty())
                {
                    lzMuveletReszletTable.Muveletreszlet = "";
                }
                _context.Add(lzMuveletReszletTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return PartialView("_CreateModalPartion", lzMuveletReszletTable);
        }

        // GET: LzMuveletReszletTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.LzMuveletReszletTable == null)
            {
                return NotFound();
            }

            var lzMuveletReszletTable = await _context.LzMuveletReszletTable.FindAsync(id);
            if (lzMuveletReszletTable == null)
            {
                return NotFound();
            }
            return PartialView("_EditModalPartion", lzMuveletReszletTable);
        }

        // POST: LzMuveletReszletTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Muveletszam,Muveletreszlet,Description,Search,Interval,Munkanap,Unnep,Pihenonap,AllCode,Empty")] LzMuveletReszletTable lzMuveletReszletTable)
        {
            if (id != lzMuveletReszletTable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (lzMuveletReszletTable.Muveletreszlet.IsNullOrEmpty())
                    {
                        lzMuveletReszletTable.Muveletreszlet = "";
                    }
                    _context.Update(lzMuveletReszletTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LzMuveletReszletTableExists(lzMuveletReszletTable.Id))
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
            return PartialView("_EditModalPartion", lzMuveletReszletTable);
        }

        private bool LzMuveletReszletTableExists(int id)
        {
          return _context.LzMuveletReszletTable.Any(e => e.Id == id);
        }
    }
}

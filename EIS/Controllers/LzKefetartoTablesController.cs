using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EIS.EISModels;
using Microsoft.AspNetCore.Http;

namespace EIS.Controllers
{
    public class LzKefetartoTablesController : Controller
    {
        private readonly EISDBContext _context;

        public LzKefetartoTablesController(EISDBContext context)
        {
            _context = context;
        }

        // GET: LzKefetartoTables
        public async Task<IActionResult> Index()
        {
            HttpContext.Session.SetInt32("LZGroup", 0);
            HttpContext.Session.SetInt32("DayGroup", 1);
            HttpContext.Session.SetInt32("AddGroup", 1);
            HttpContext.Session.SetInt32("HeaderGroup", 1);
            var eISDBContext = _context.ViewLzKefetartoTable.Where(w => w.Interval == false);
            return View(await eISDBContext.ToListAsync());
        }

        // GET: LzKefetartoTables/Create
        public IActionResult Create()
        {
            ViewData["KefetartoId"] = new SelectList(_context.LzKefetartotipusTable.OrderBy(a => a.Kefetartoszam), "Id", "Kefetartoszam");
            ViewData["MuveletId"] = new SelectList(_context.LzMuveletReszletTable.Where(w => w.Interval == false).OrderBy(a => a.Search), "Id", "Search");
            LzKefetartoTable lzKefetartoTable = new LzKefetartoTable()
            {
                Id = 0
            };
            return PartialView("_CreateModalPartion", lzKefetartoTable);
        }

        // POST: LzKefetartoTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,KefetartoId,MuveletId")] LzKefetartoTable lzKefetartoTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lzKefetartoTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KefetartoId"] = new SelectList(_context.LzKefetartotipusTable.OrderBy(a => a.Kefetartoszam), "Id", "Kefetartoszam", lzKefetartoTable.KefetartoId);
            ViewData["MuveletId"] = new SelectList(_context.LzMuveletReszletTable.Where(w => w.Interval == false).OrderBy(a=>a.Search), "Id", "Search", lzKefetartoTable.MuveletId);
            return PartialView("_CreateModalPartion", lzKefetartoTable);
        }

        // GET: LzKefetartoTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.LzKefetartoTable == null)
            {
                return NotFound();
            }

            var lzKefetartoTable = await _context.LzKefetartoTable.FindAsync(id);
            if (lzKefetartoTable == null)
            {
                return NotFound();
            }
            ViewData["KefetartoId"] = new SelectList(_context.LzKefetartotipusTable.OrderBy(a => a.Kefetartoszam), "Id", "Kefetartoszam", lzKefetartoTable.KefetartoId);
            ViewData["MuveletId"] = new SelectList(_context.LzMuveletReszletTable.Where(w => w.Interval == false).OrderBy(a => a.Search), "Id", "Search", lzKefetartoTable.MuveletId);
            return PartialView("_EditModalPartion", lzKefetartoTable);
        }

        // POST: LzKefetartoTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,KefetartoId,MuveletId")] LzKefetartoTable lzKefetartoTable)
        {
            if (id != lzKefetartoTable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lzKefetartoTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LzKefetartoTableExists(lzKefetartoTable.Id))
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
            ViewData["KefetartoId"] = new SelectList(_context.LzKefetartotipusTable.OrderBy(a => a.Kefetartoszam), "Id", "Kefetartoszam", lzKefetartoTable.KefetartoId);
            ViewData["MuveletId"] = new SelectList(_context.LzMuveletReszletTable.Where(w => w.Interval == false).OrderBy(a => a.Search), "Id", "Search", lzKefetartoTable.MuveletId);
            return PartialView("_EditModalPartion", lzKefetartoTable);
        }

        private bool LzKefetartoTableExists(int id)
        {
          return _context.LzKefetartoTable.Any(e => e.Id == id);
        }
    }
}

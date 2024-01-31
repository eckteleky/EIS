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
    public class LzKefetartotipusTablesController : Controller
    {
        private readonly EISDBContext _context;

        public LzKefetartotipusTablesController(EISDBContext context)
        {
            _context = context;
        }

        // GET: LzKefetartotipusTables
        public async Task<IActionResult> Index()
        {
            HttpContext.Session.SetInt32("LZGroup", 0);
            HttpContext.Session.SetInt32("DayGroup", 1);
            HttpContext.Session.SetInt32("AddGroup", 1);
            HttpContext.Session.SetInt32("HeaderGroup", 1);
            var eISDBContext = _context.ViewLzKefetartotipusTable;
            return View(await eISDBContext.ToListAsync());
        }

        // GET: LzKefetartotipusTables/Create
        public IActionResult Create()
        {
            LzKefetartotipusTable lzKefetartotipusTable = new LzKefetartotipusTable()
            {
                Id = 0
            };
            ViewData["TipusId"] = new SelectList(_context.LzKefetartotipuslistaTable, "Id", "Tipus");
            return PartialView("_CreateModalPartion", lzKefetartotipusTable);
        }

        // POST: LzKefetartotipusTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Kefetartoszam,TipusId")] LzKefetartotipusTable lzKefetartotipusTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lzKefetartotipusTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TipusId"] = new SelectList(_context.LzKefetartotipuslistaTable, "Id", "Tipus", lzKefetartotipusTable.TipusId);
            return PartialView("_CreateModalPartion", lzKefetartotipusTable);
        }

        // GET: LzKefetartotipusTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.LzKefetartotipusTable == null)
            {
                return NotFound();
            }

            var lzKefetartotipusTable = await _context.LzKefetartotipusTable.FindAsync(id);
            if (lzKefetartotipusTable == null)
            {
                return NotFound();
            }
            ViewData["TipusId"] = new SelectList(_context.LzKefetartotipuslistaTable, "Id", "Tipus", lzKefetartotipusTable.TipusId);
            return PartialView("_EditModalPartion", lzKefetartotipusTable);
        }

        // POST: LzKefetartotipusTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Kefetartoszam,TipusId")] LzKefetartotipusTable lzKefetartotipusTable)
        {
            if (id != lzKefetartotipusTable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lzKefetartotipusTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LzKefetartotipusTableExists(lzKefetartotipusTable.Id))
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
            ViewData["TipusId"] = new SelectList(_context.LzKefetartotipuslistaTable, "Id", "Tipus", lzKefetartotipusTable.TipusId);
            return PartialView("_EditModalPartion", lzKefetartotipusTable);
        }

        private bool LzKefetartotipusTableExists(int id)
        {
          return _context.LzKefetartotipusTable.Any(e => e.Id == id);
        }
    }
}

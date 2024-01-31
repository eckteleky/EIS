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
    public class LzMuszakTablesController : Controller
    {
        private readonly EISDBContext _context;

        public LzMuszakTablesController(EISDBContext context)
        {
            _context = context;
        }

        // GET: LzMuszakTables
        public async Task<IActionResult> Index()
        {
            HttpContext.Session.SetInt32("LZGroup", 0);
            HttpContext.Session.SetInt32("DayGroup", 1);
            HttpContext.Session.SetInt32("AddGroup", 1);
            HttpContext.Session.SetInt32("HeaderGroup", 1);
            return View(await _context.LzMuszakTable.ToListAsync());
        }

        // GET: LzMuszakTables/Create
        public IActionResult Create()
        {
            LzMuszakTable lzMuszakTable = new LzMuszakTable()
            {
                Id = 0,
                Muszak = 4,
                Lzmuszak = 14
            };
            return PartialView("_CreateModalPartion", lzMuszakTable);
        }

        // POST: LzMuszakTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Lzmuszak,Muszak,Description")] LzMuszakTable lzMuszakTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lzMuszakTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return PartialView("_CreateModalPartion", lzMuszakTable);
        }

        // GET: LzMuszakTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.LzMuszakTable == null)
            {
                return NotFound();
            }

            var lzMuszakTable = await _context.LzMuszakTable.FindAsync(id);
            if (lzMuszakTable == null)
            {
                return NotFound();
            }
            return PartialView("_EditModalPartion", lzMuszakTable);
        }

        // POST: LzMuszakTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Lzmuszak,Muszak,Description")] LzMuszakTable lzMuszakTable)
        {
            if (id != lzMuszakTable.Muszak)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lzMuszakTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LzMuszakTableExists(lzMuszakTable.Lzmuszak))
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
            return PartialView("_EditModalPartion", lzMuszakTable);
        }

        private bool LzMuszakTableExists(int id)
        {
          return _context.LzMuszakTable.Any(e => e.Id == id);
        }
    }
}

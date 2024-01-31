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
    public class LzKefetartotipuslistaTablesController : Controller
    {
        private readonly EISDBContext _context;

        public LzKefetartotipuslistaTablesController(EISDBContext context)
        {
            _context = context;
        }

        // GET: LzKefetartotipuslistaTables
        public async Task<IActionResult> Index()
        {
            HttpContext.Session.SetInt32("LZGroup", 0);
            HttpContext.Session.SetInt32("DayGroup", 1);
            HttpContext.Session.SetInt32("AddGroup", 1);
            HttpContext.Session.SetInt32("HeaderGroup", 1);
            return View(await _context.LzKefetartotipuslistaTable.ToListAsync());
        }

        // GET: LzKefetartotipuslistaTables/Create
        public IActionResult Create()
        {
            LzKefetartotipuslistaTable lzKefetartotipuslistaTable = new LzKefetartotipuslistaTable()
            {
                Id = 0
            };
            return PartialView("_CreateModalPartion", lzKefetartotipuslistaTable);
        }

        // POST: LzKefetartotipuslistaTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Tipus")] LzKefetartotipuslistaTable lzKefetartotipuslistaTable)
        {
            if (ModelState.IsValid)
            {
                if (lzKefetartotipuslistaTable.Tipus.Count()==0)
                {
                    lzKefetartotipuslistaTable.Tipus = "";
                }
                _context.Add(lzKefetartotipuslistaTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return PartialView("_CreateModalPartion", lzKefetartotipuslistaTable);
        }

        // GET: LzKefetartotipuslistaTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.LzKefetartotipuslistaTable == null)
            {
                return NotFound();
            }

            var lzKefetartotipuslistaTable = await _context.LzKefetartotipuslistaTable.FindAsync(id);
            if (lzKefetartotipuslistaTable == null)
            {
                return NotFound();
            }
            return PartialView("_EditModalPartion", lzKefetartotipuslistaTable);
        }

        // POST: LzKefetartotipuslistaTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Tipus")] LzKefetartotipuslistaTable lzKefetartotipuslistaTable)
        {
            if (id != lzKefetartotipuslistaTable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                if (lzKefetartotipuslistaTable.Tipus.Count()==0)
                {
                    lzKefetartotipuslistaTable.Tipus = "";
                }                    _context.Update(lzKefetartotipuslistaTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LzKefetartotipuslistaTableExists(lzKefetartotipuslistaTable.Id))
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
            return PartialView("_EditModalPartion", lzKefetartotipuslistaTable);
        }

        private bool LzKefetartotipuslistaTableExists(int id)
        {
          return _context.LzKefetartotipuslistaTable.Any(e => e.Id == id);
        }
    }
}

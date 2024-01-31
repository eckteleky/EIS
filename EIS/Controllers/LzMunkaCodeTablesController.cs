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
    public class LzMunkaCodeTablesController : Controller
    {
        private readonly EISDBContext _context;

        public LzMunkaCodeTablesController(EISDBContext context)
        {
            _context = context;
        }

        // GET: LzMunkaCodeTables
        public async Task<IActionResult> Index()
        {
            HttpContext.Session.SetInt32("LZGroup", 0);
            HttpContext.Session.SetInt32("DayGroup", 1);
            HttpContext.Session.SetInt32("AddGroup", 1);
            HttpContext.Session.SetInt32("HeaderGroup", 1);
            return View(await _context.LzMunkaCodeTable.ToListAsync());
        }

        // GET: LzMunkaCodeTables/Create
        public IActionResult Create()
        {
            LzMunkaCodeTable lzMunkaCodeTable = new LzMunkaCodeTable()
            {
                Id = 0
            };
            return PartialView("_CreateModalPartion", lzMunkaCodeTable);
        }

        // POST: LzMunkaCodeTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Short,Description,Munkanap,Unnep,Pihenonap")] LzMunkaCodeTable lzMunkaCodeTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lzMunkaCodeTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return PartialView("_CreateModalPartion", lzMunkaCodeTable);
        }

        // GET: LzMunkaCodeTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.LzMunkaCodeTable == null)
            {
                return NotFound();
            }

            var lzMunkaCodeTable = await _context.LzMunkaCodeTable.FindAsync(id);
            if (lzMunkaCodeTable == null)
            {
                return NotFound();
            }
            return PartialView("_EditModalPartion", lzMunkaCodeTable);
        }

        // POST: LzMunkaCodeTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Short,Description,Munkanap,Unnep,Pihenonap")] LzMunkaCodeTable lzMunkaCodeTable)
        {
            if (id != lzMunkaCodeTable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lzMunkaCodeTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LzMunkaCodeTableExists(lzMunkaCodeTable.Id))
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
            return PartialView("_EditModalPartion", lzMunkaCodeTable);
        }

        private bool LzMunkaCodeTableExists(int id)
        {
          return _context.LzMunkaCodeTable.Any(e => e.Id == id);
        }
    }
}

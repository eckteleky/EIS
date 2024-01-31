using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EIS.EISModels
{
    public class ErrorTablesController : Controller
    {
        private readonly EISDBContext _context;

        public ErrorTablesController(EISDBContext context)
        {
            _context = context;
        }

        // GET: ErrorTables
        public async Task<IActionResult> Index()
        {
            return View(await _context.ErrorTable.ToListAsync());
        }

        // GET: ErrorTables/Details/5
        public async Task<IActionResult> Details(DateTime? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var errorTable = await _context.ErrorTable
                .FirstOrDefaultAsync(m => m.TimeStamp == id);
            if (errorTable == null)
            {
                return NotFound();
            }

            return View(errorTable);
        }

        // GET: ErrorTables/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ErrorTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TimeStamp,LineId,StationId,ErrorCode,DownTime,EndTime")] ErrorTable errorTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(errorTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(errorTable);
        }

        // GET: ErrorTables/Edit/5
        public async Task<IActionResult> Edit(DateTime? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var errorTable = await _context.ErrorTable.FindAsync(id);
            if (errorTable == null)
            {
                return NotFound();
            }
            return View(errorTable);
        }

        // POST: ErrorTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DateTime id, [Bind("TimeStamp,LineId,StationId,ErrorCode,DownTime,EndTime")] ErrorTable errorTable)
        {
            if (id != errorTable.TimeStamp)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(errorTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ErrorTableExists(errorTable.TimeStamp))
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
            return View(errorTable);
        }

        // GET: ErrorTables/Delete/5
        public async Task<IActionResult> Delete(DateTime? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var errorTable = await _context.ErrorTable
                .FirstOrDefaultAsync(m => m.TimeStamp == id);
            if (errorTable == null)
            {
                return NotFound();
            }

            return View(errorTable);
        }

        // POST: ErrorTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(DateTime id)
        {
            var errorTable = await _context.ErrorTable.FindAsync(id);
            if (errorTable != null)
            {
                _context.ErrorTable.Remove(errorTable);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ErrorTableExists(DateTime id)
        {
            return _context.ErrorTable.Any(e => e.TimeStamp == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EIS.EISModels;

namespace EIS.Controllers
{
    public class CttablesController : Controller
    {
        private readonly EISDBContext _context;

        public CttablesController(EISDBContext context)
        {
            _context = context;
        }

        // GET: Cttables
        public async Task<IActionResult> Index()
        {
            var eISDBContext = _context.Cttable.Include(c => c.Dttypes);
            return View(await eISDBContext.ToListAsync());
        }

        // GET: Cttables/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cttable == null)
            {
                return NotFound();
            }

            var cttable = await _context.Cttable
                .Include(c => c.Dttypes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cttable == null)
            {
                return NotFound();
            }

            return View(cttable);
        }

        // GET: Cttables/Create
        public IActionResult Create()
        {
            ViewData["LineId"] = new SelectList(_context.Dttypes, "LineId", "CodeId");
            return View();
        }

        // POST: Cttables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TimeStamp,LineId,StationId,Ct,Status,Pt,Wt,StartTime,Muszak,LongCt,LongStep,TesterId,TesterSubId,Xct,Xpt,Xwt,Nok,Wtnr,Type,Cutting,Dummies,Leerfahren,Active,Modified,Sent2Cloud,EndTime,TypeId,AlertLevel,LastAlertLevel,Dtstatus,Id,Dt,Downtime")] Cttable cttable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cttable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LineId"] = new SelectList(_context.Dttypes, "LineId", "CodeId", cttable.LineId);
            return View(cttable);
        }

        // GET: Cttables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cttable == null)
            {
                return NotFound();
            }

            var cttable = await _context.Cttable.FindAsync(id);
            if (cttable == null)
            {
                return NotFound();
            }
            ViewData["LineId"] = new SelectList(_context.Dttypes, "LineId", "CodeId", cttable.LineId);
            return View(cttable);
        }

        // POST: Cttables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TimeStamp,LineId,StationId,Ct,Status,Pt,Wt,StartTime,Muszak,LongCt,LongStep,TesterId,TesterSubId,Xct,Xpt,Xwt,Nok,Wtnr,Type,Cutting,Dummies,Leerfahren,Active,Modified,Sent2Cloud,EndTime,TypeId,AlertLevel,LastAlertLevel,Dtstatus,Id,Dt,Downtime")] Cttable cttable)
        {
            if (id != cttable.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cttable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CttableExists(cttable.Id))
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
            ViewData["LineId"] = new SelectList(_context.Dttypes, "LineId", "CodeId", cttable.LineId);
            return View(cttable);
        }

        // GET: Cttables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cttable == null)
            {
                return NotFound();
            }

            var cttable = await _context.Cttable
                .Include(c => c.Dttypes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cttable == null)
            {
                return NotFound();
            }

            return View(cttable);
        }

        // POST: Cttables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cttable == null)
            {
                return Problem("Entity set 'EISDBContext.Cttable'  is null.");
            }
            var cttable = await _context.Cttable.FindAsync(id);
            if (cttable != null)
            {
                _context.Cttable.Remove(cttable);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CttableExists(int id)
        {
          return _context.Cttable.Any(e => e.Id == id);
        }
    }
}

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
    public class MesproductPlanWeekTempsController : Controller
    {
        private readonly EISDBContext _context;

        public MesproductPlanWeekTempsController(EISDBContext context)
        {
            _context = context;
        }

        // GET: MesproductPlanWeekTemps
        public async Task<IActionResult> Index()
        {
            var eISDBContext = _context.MesproductPlanWeekTemp;
            return View(await eISDBContext.ToListAsync());
        }

        // GET: MesproductPlanWeekTemps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MesproductPlanWeekTemp == null)
            {
                return NotFound();
            }

            var mesproductPlanWeekTemp = await _context.MesproductPlanWeekTemp
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mesproductPlanWeekTemp == null)
            {
                return NotFound();
            }

            return View(mesproductPlanWeekTemp);
        }

        // GET: MesproductPlanWeekTemps/Create
        public IActionResult Create()
        {
            ViewData["WorkerId11"] = new SelectList(_context.Mesworker, "Id", "WorkerName");
            ViewData["WorkerId12"] = new SelectList(_context.Mesworker, "Id", "WorkerName");
            ViewData["WorkerId21"] = new SelectList(_context.Mesworker, "Id", "WorkerName");
            ViewData["WorkerId22"] = new SelectList(_context.Mesworker, "Id", "WorkerName");
            ViewData["WorkerId31"] = new SelectList(_context.Mesworker, "Id", "WorkerName");
            ViewData["WorkerId32"] = new SelectList(_context.Mesworker, "Id", "WorkerName");
            return View();
        }

        // POST: MesproductPlanWeekTemps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LineId,Year,Week,Active01,Active11,Active21,Active31,Active41,Active51,Active61,WorkerId11,WorkerId12,Active02,Active12,Active22,Active32,Active42,Active52,Active62,WorkerId21,WorkerId22,Active03,Active13,Active23,Active33,Active43,Active53,Active63,WorkerId31,WorkerId32,StartTime,Newflag")] MesproductPlanWeekTemp mesproductPlanWeekTemp)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mesproductPlanWeekTemp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["WorkerId11"] = new SelectList(_context.Mesworker, "Id", "WorkerName", mesproductPlanWeekTemp.WorkerId11);
            ViewData["WorkerId12"] = new SelectList(_context.Mesworker, "Id", "WorkerName", mesproductPlanWeekTemp.WorkerId12);
            ViewData["WorkerId21"] = new SelectList(_context.Mesworker, "Id", "WorkerName", mesproductPlanWeekTemp.WorkerId21);
            ViewData["WorkerId22"] = new SelectList(_context.Mesworker, "Id", "WorkerName", mesproductPlanWeekTemp.WorkerId22);
            ViewData["WorkerId31"] = new SelectList(_context.Mesworker, "Id", "WorkerName", mesproductPlanWeekTemp.WorkerId31);
            ViewData["WorkerId32"] = new SelectList(_context.Mesworker, "Id", "WorkerName", mesproductPlanWeekTemp.WorkerId32);
            return View(mesproductPlanWeekTemp);
        }

        // GET: MesproductPlanWeekTemps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MesproductPlanWeekTemp == null)
            {
                return NotFound();
            }

            var mesproductPlanWeekTemp = await _context.MesproductPlanWeekTemp.FindAsync(id);
            if (mesproductPlanWeekTemp == null)
            {
                return NotFound();
            }
            ViewData["WorkerId11"] = new SelectList(_context.Mesworker, "Id", "WorkerName", mesproductPlanWeekTemp.WorkerId11);
            ViewData["WorkerId12"] = new SelectList(_context.Mesworker, "Id", "WorkerName", mesproductPlanWeekTemp.WorkerId12);
            ViewData["WorkerId21"] = new SelectList(_context.Mesworker, "Id", "WorkerName", mesproductPlanWeekTemp.WorkerId21);
            ViewData["WorkerId22"] = new SelectList(_context.Mesworker, "Id", "WorkerName", mesproductPlanWeekTemp.WorkerId22);
            ViewData["WorkerId31"] = new SelectList(_context.Mesworker, "Id", "WorkerName", mesproductPlanWeekTemp.WorkerId31);
            ViewData["WorkerId32"] = new SelectList(_context.Mesworker, "Id", "WorkerName", mesproductPlanWeekTemp.WorkerId32);
            return View(mesproductPlanWeekTemp);
        }

        // POST: MesproductPlanWeekTemps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LineId,Year,Week,Active01,Active11,Active21,Active31,Active41,Active51,Active61,WorkerId11,WorkerId12,Active02,Active12,Active22,Active32,Active42,Active52,Active62,WorkerId21,WorkerId22,Active03,Active13,Active23,Active33,Active43,Active53,Active63,WorkerId31,WorkerId32,StartTime,Newflag")] MesproductPlanWeekTemp mesproductPlanWeekTemp)
        {
            if (id != mesproductPlanWeekTemp.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mesproductPlanWeekTemp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MesproductPlanWeekTempExists(mesproductPlanWeekTemp.Id))
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
            ViewData["WorkerId11"] = new SelectList(_context.Mesworker, "Id", "WorkerName", mesproductPlanWeekTemp.WorkerId11);
            ViewData["WorkerId12"] = new SelectList(_context.Mesworker, "Id", "WorkerName", mesproductPlanWeekTemp.WorkerId12);
            ViewData["WorkerId21"] = new SelectList(_context.Mesworker, "Id", "WorkerName", mesproductPlanWeekTemp.WorkerId21);
            ViewData["WorkerId22"] = new SelectList(_context.Mesworker, "Id", "WorkerName", mesproductPlanWeekTemp.WorkerId22);
            ViewData["WorkerId31"] = new SelectList(_context.Mesworker, "Id", "WorkerName", mesproductPlanWeekTemp.WorkerId31);
            ViewData["WorkerId32"] = new SelectList(_context.Mesworker, "Id", "WorkerName", mesproductPlanWeekTemp.WorkerId32);
            return View(mesproductPlanWeekTemp);
        }

        // GET: MesproductPlanWeekTemps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MesproductPlanWeekTemp == null)
            {
                return NotFound();
            }

            var mesproductPlanWeekTemp = await _context.MesproductPlanWeekTemp
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mesproductPlanWeekTemp == null)
            {
                return NotFound();
            }

            return View(mesproductPlanWeekTemp);
        }

        // POST: MesproductPlanWeekTemps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MesproductPlanWeekTemp == null)
            {
                return Problem("Entity set 'EISDBContext.MesproductPlanWeekTemp'  is null.");
            }
            var mesproductPlanWeekTemp = await _context.MesproductPlanWeekTemp.FindAsync(id);
            if (mesproductPlanWeekTemp != null)
            {
                _context.MesproductPlanWeekTemp.Remove(mesproductPlanWeekTemp);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MesproductPlanWeekTempExists(int id)
        {
          return _context.MesproductPlanWeekTemp.Any(e => e.Id == id);
        }
    }
}

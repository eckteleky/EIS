using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EIS.EISModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using EIS.Models;
using EIS.Helpers;

namespace EIS.Controllers
{
    public class KpiheadersController : Controller
    {
        private readonly EISDBContext _context;

        public KpiheadersController(EISDBContext context)
        {
            _context = context;
        }

        // GET: Kpiheaders
        public async Task<IActionResult> Index()
        {
            HttpContext.Session.SetInt32("LZGroup", 0);
            HttpContext.Session.SetInt32("LineIDGroup", 0);
            HttpContext.Session.SetInt32("StationGroup", 0);
            HttpContext.Session.SetInt32("ShiftGroup", 0);
            HttpContext.Session.SetInt32("DayGroup", 0);
            HttpContext.Session.SetInt32("AddGroup", 1);
            HttpContext.Session.SetInt32("HeaderGroup", 1);
            #region GetServerDatas v1.3b
            string LineID = "GBM Line1";
            if (HttpContext.Session.GetString("LineID") == null)
            {
                LineID = "GBM Line1";// VALEO RSW2";
            }
            else
            {
                LineID = HttpContext.Session.GetString("LineID");
            }
            int Year = DateTime.Now.Year;
            if (!HttpContext.Session.GetInt32("Year").HasValue)
            {
                Year = DateTime.Now.Year;
            }
            else
            {
                Year = HttpContext.Session.GetInt32("Year").Value;
            }
            int year = (int)Year;
            int Month = DateTime.Now.Month;
            if (!HttpContext.Session.GetInt32("Month").HasValue)
            {
                Month = DateTime.Now.Month;
            }
            else
            {
                Month = HttpContext.Session.GetInt32("Month").Value;
            }
            int month = (int)Month;
            int Day = DateTime.Now.Day;
            if (!HttpContext.Session.GetInt32("Day").HasValue)
            {
                Day = DateTime.Now.Day;
            }
            else
            {
                Day = HttpContext.Session.GetInt32("Day").Value;
            }
            int day = (int)Day;
            int YearFrom = DateTime.Now.Year;
            if (!HttpContext.Session.GetInt32("YearFrom").HasValue)
            {
                YearFrom = DateTime.Now.Year;
            }
            else
            {
                YearFrom = HttpContext.Session.GetInt32("YearFrom").Value;
            }
            int yearfrom = (int)YearFrom;
            int MonthFrom = DateTime.Now.Month;
            if (!HttpContext.Session.GetInt32("MonthFrom").HasValue)
            {
                MonthFrom = DateTime.Now.Month;
            }
            else
            {
                MonthFrom = HttpContext.Session.GetInt32("MonthFrom").Value;
            }
            int monthfrom = (int)MonthFrom;
            int DayFrom = DateTime.Now.Day;
            if (!HttpContext.Session.GetInt32("DayFrom").HasValue)
            {
                DayFrom = DateTime.Now.Day;
            }
            else
            {
                DayFrom = HttpContext.Session.GetInt32("DayFrom").Value;
            }
            int dayfrom = (int)DayFrom;
            int YearTo = DateTime.Now.Year;
            if (!HttpContext.Session.GetInt32("YearTo").HasValue)
            {
                YearTo = DateTime.Now.Year;
            }
            else
            {
                YearTo = HttpContext.Session.GetInt32("YearTo").Value;
            }
            int yearto = (int)YearTo;
            int MonthTo = DateTime.Now.Month;
            if (!HttpContext.Session.GetInt32("MonthTo").HasValue)
            {
                MonthTo = DateTime.Now.Month;
            }
            else
            {
                MonthTo = HttpContext.Session.GetInt32("MonthTo").Value;
            }
            int monthto = (int)MonthTo;
            int DayTo = DateTime.Now.Day;
            if (!HttpContext.Session.GetInt32("DayTo").HasValue)
            {
                DayTo = DateTime.Now.Day;
            }
            else
            {
                DayTo = HttpContext.Session.GetInt32("DayTo").Value;
            }
            int dayto = (int)DayTo;
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            string Language = @requestCulture.RequestCulture.UICulture.Name;

            int Company = 1;
            if (!HttpContext.Session.GetInt32("Company").HasValue)
            {
                Company = 1;
            }
            else
            {
                Company = HttpContext.Session.GetInt32("Company").Value;
            }
            int company = (int)Company;

            int DolgozoId = 0;
            if (!HttpContext.Session.GetInt32("DolgozoId").HasValue)
            {
                DolgozoId = 0;
            }
            else
            {
                DolgozoId = HttpContext.Session.GetInt32("DolgozoId").Value;
            }
            int dolgozoid = (int)DolgozoId;

            if (HttpContext.Session.GetString("LineID") == null |
                HttpContext.Session.GetInt32("Year") == null |
                HttpContext.Session.GetInt32("Month") == null |
                HttpContext.Session.GetInt32("Day") == null |
                HttpContext.Session.GetInt32("Shift") == null |
                HttpContext.Session.GetInt32("Company") == null |
                HttpContext.Session.GetInt32("DolgozoId") == null)
            {
                Headers header = new()
                {
                    MyFields = false,
                    SearchString = ""
                };
                SessionHelper.SetObjectAsJson(HttpContext.Session, "header", header);
            }
            HttpContext.Session.SetString("LineID", LineID);

            HttpContext.Session.SetInt32("Year", year);
            HttpContext.Session.SetInt32("Month", month);
            HttpContext.Session.SetInt32("Day", day);

            HttpContext.Session.SetInt32("YearFrom", yearfrom);
            HttpContext.Session.SetInt32("MonthFrom", monthfrom);
            HttpContext.Session.SetInt32("DayFrom", dayfrom);

            HttpContext.Session.SetInt32("YearTo", yearto);
            HttpContext.Session.SetInt32("MonthTo", monthto);
            HttpContext.Session.SetInt32("DayTo", dayto);

            HttpContext.Session.SetInt32("Company", company);

            HttpContext.Session.SetInt32("DolgozoId", dolgozoid);
            #endregion
            var eISDBContext = _context.Kpiheader.Include(k => k.DataSource).Include(k => k.Expectation).Include(k => k.Process).Include(k => k.Responsible).Include(k => k.Summantion).Include(k => k.Unit);
            return View(await eISDBContext.ToListAsync());
        }

        // GET: Kpiheaders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Kpiheader == null)
            {
                return NotFound();
            }

            var kpiheader = await _context.Kpiheader
                .Include(k => k.DataSource)
                .Include(k => k.Expectation)
                .Include(k => k.Process)
                .Include(k => k.Responsible)
                .Include(k => k.Summantion)
                .Include(k => k.Unit)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kpiheader == null)
            {
                return NotFound();
            }

            return View(kpiheader);
        }

        // GET: Kpiheaders/Create
        public IActionResult Create()
        {
            Kpiheader kpiheader = new Kpiheader()
            {
                Id = 0
            };
            ViewData["DataSourceId"] = new SelectList(_context.KpidataSource, "Id", "Id");
            ViewData["ExpectationId"] = new SelectList(_context.Kpiexpectation, "Id", "Id");
            ViewData["ProcessId"] = new SelectList(_context.Kpiprocess, "Id", "Id");
            ViewData["ResponsibleId"] = new SelectList(_context.Kpiresponsible, "Id", "Id");
            ViewData["SummantionId"] = new SelectList(_context.Kpisummantion, "Id", "Id");
            ViewData["UnitId"] = new SelectList(_context.Kpiunit, "Id", "Id");
            return PartialView("_CreateModalPartion", kpiheader);
        }

        // POST: Kpiheaders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProcessId,DescriptionHu,DescriptionEn,DescriptionDe,ResponsibleId,DataSourceId,UnitId,ExpectationId,SummantionId")] Kpiheader kpiheader)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kpiheader);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DataSourceId"] = new SelectList(_context.KpidataSource, "Id", "Id", kpiheader.DataSourceId);
            ViewData["ExpectationId"] = new SelectList(_context.Kpiexpectation, "Id", "Id", kpiheader.ExpectationId);
            ViewData["ProcessId"] = new SelectList(_context.Kpiprocess, "Id", "Id", kpiheader.ProcessId);
            ViewData["ResponsibleId"] = new SelectList(_context.Kpiresponsible, "Id", "Id", kpiheader.ResponsibleId);
            ViewData["SummantionId"] = new SelectList(_context.Kpisummantion, "Id", "Id", kpiheader.SummantionId);
            ViewData["UnitId"] = new SelectList(_context.Kpiunit, "Id", "Id", kpiheader.UnitId);
            return PartialView("_CreateModalPartion", kpiheader);
        }

        // GET: Kpiheaders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Kpiheader == null)
            {
                return NotFound();
            }

            var kpiheader = await _context.Kpiheader.FindAsync(id);
            if (kpiheader == null)
            {
                return NotFound();
            }
            ViewData["DataSourceId"] = new SelectList(_context.KpidataSource, "Id", "Id", kpiheader.DataSourceId);
            ViewData["ExpectationId"] = new SelectList(_context.Kpiexpectation, "Id", "Id", kpiheader.ExpectationId);
            ViewData["ProcessId"] = new SelectList(_context.Kpiprocess, "Id", "Id", kpiheader.ProcessId);
            ViewData["ResponsibleId"] = new SelectList(_context.Kpiresponsible, "Id", "Id", kpiheader.ResponsibleId);
            ViewData["SummantionId"] = new SelectList(_context.Kpisummantion, "Id", "Id", kpiheader.SummantionId);
            ViewData["UnitId"] = new SelectList(_context.Kpiunit, "Id", "Id", kpiheader.UnitId);
            return PartialView("_EditModalPartion", kpiheader);
        }

        // POST: Kpiheaders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProcessId,DescriptionHu,DescriptionEn,DescriptionDe,ResponsibleId,DataSourceId,UnitId,ExpectationId,SummantionId")] Kpiheader kpiheader)
        {
            if (id != kpiheader.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kpiheader);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KpiheaderExists(kpiheader.Id))
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
            ViewData["DataSourceId"] = new SelectList(_context.KpidataSource, "Id", "Id", kpiheader.DataSourceId);
            ViewData["ExpectationId"] = new SelectList(_context.Kpiexpectation, "Id", "Id", kpiheader.ExpectationId);
            ViewData["ProcessId"] = new SelectList(_context.Kpiprocess, "Id", "Id", kpiheader.ProcessId);
            ViewData["ResponsibleId"] = new SelectList(_context.Kpiresponsible, "Id", "Id", kpiheader.ResponsibleId);
            ViewData["SummantionId"] = new SelectList(_context.Kpisummantion, "Id", "Id", kpiheader.SummantionId);
            ViewData["UnitId"] = new SelectList(_context.Kpiunit, "Id", "Id", kpiheader.UnitId);
            return PartialView("_EditModalPartion", kpiheader);
        }

        // GET: Kpiheaders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Kpiheader == null)
            {
                return NotFound();
            }

            var kpiheader = await _context.Kpiheader
                .Include(k => k.DataSource)
                .Include(k => k.Expectation)
                .Include(k => k.Process)
                .Include(k => k.Responsible)
                .Include(k => k.Summantion)
                .Include(k => k.Unit)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kpiheader == null)
            {
                return NotFound();
            }

            return View(kpiheader);
        }

        // POST: Kpiheaders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Kpiheader == null)
            {
                return Problem("Entity set 'EISDBContext.Kpiheader'  is null.");
            }
            var kpiheader = await _context.Kpiheader.FindAsync(id);
            if (kpiheader != null)
            {
                _context.Kpiheader.Remove(kpiheader);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KpiheaderExists(int id)
        {
          return _context.Kpiheader.Any(e => e.Id == id);
        }
    }
}

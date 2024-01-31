using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EIS.EISModels;
using EIS.Helpers;
using EIS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;

namespace EIS.Controllers
{
    public class MesproductPlansController : Controller
    {
        private readonly EISDBContext _context;

        public MesproductPlansController(EISDBContext context)
        {
            _context = context;
        }

        // GET: MesproductPlans
        public async Task<IActionResult> Index()
        {
            HttpContext.Session.SetInt32("LZGroup", 0);
            HttpContext.Session.SetInt32("LineIDGroup", 1);
            HttpContext.Session.SetInt32("StationGroup", 0);
            HttpContext.Session.SetInt32("ShiftGroup", 1);
            HttpContext.Session.SetInt32("DayGroup", 1);
            HttpContext.Session.SetInt32("AddGroup", 1);
            HttpContext.Session.SetInt32("HeaderGroup", 1);
            #region GetServerDatas v1.3
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
            int Shift = 4;
            if (!HttpContext.Session.GetInt32("Shift").HasValue)
            {
                Shift = 4;
            }
            else
            {
                Shift = HttpContext.Session.GetInt32("Shift").Value;
            }
            int shift = 4;
            string shifttxt = "All day";
            switch (Shift)
            {
                case 4: shifttxt = "All day"; shift = 4; break;
                case 1: shifttxt = "Morning"; shift = 1; break;
                case 2: shifttxt = "Afternoon"; shift = 2; break;
                case 3: shifttxt = "Overnight"; shift = 3; break;
                case 5: shifttxt = "Night+Morning"; shift = 5; break; // 1 + 3
                case 6: shifttxt = "Night+Afternoon"; shift = 6; break; // 2 + 3
                case 7: shifttxt = "Morning+Afternoon"; shift = 7; break; // 1 + 2
            }
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            if (@requestCulture.RequestCulture.UICulture.Name == "hu-HU")
            {
                shifttxt = "Egész nap";
                switch (Shift)
                {
                    case 4: shifttxt = "Egész nap"; shift = 4; break;
                    case 1: shifttxt = "Délelőtt"; shift = 1; break;
                    case 2: shifttxt = "Délután"; shift = 2; break;
                    case 3: shifttxt = "Éjszaka"; shift = 3; break;
                    case 5: shifttxt = "Éjszaka+Nappal"; shift = 5; break; // 1 + 3
                    case 6: shifttxt = "Éjszaka+Délután"; shift = 6; break; // 2 + 3
                    case 7: shifttxt = "Nappal+Délután"; shift = 7; break; // 1 + 2
                }
            }
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

            HttpContext.Session.SetInt32("Shift", shift);
            HttpContext.Session.SetString("ShiftTxt", shifttxt);

            HttpContext.Session.SetInt32("Company", company);

            HttpContext.Session.SetInt32("DolgozoId", dolgozoid);
            #endregion
            DateTime StartTimeFrom = new DateTime(YearFrom, MonthFrom, DayFrom, 6, 0, 0);
            DateTime StartTimeTo = new DateTime(YearTo, MonthTo, DayTo, 6, 0, 0);
            var eISDBContext = _context.MesproductPlan.Include(m => m.WorkerId1Navigation).Include(m => m.WorkerId2Navigation).Where(a => (a.LineId == LineID | LineID == "ALL Line") & (a.StartTime >= StartTimeFrom & a.StartTime <= StartTimeTo) & (a.Muszak == Shift | (Shift == 4)));
            return View(await eISDBContext.ToListAsync());
        }

        // GET: MesproductPlans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MesproductPlan == null)
            {
                return NotFound();
            }

            var mesproductPlan = await _context.MesproductPlan
                .Include(m => m.WorkerId1Navigation)
                .Include(m => m.WorkerId2Navigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mesproductPlan == null)
            {
                return NotFound();
            }

            return View(mesproductPlan);
        }

        // GET: MesproductPlans/Create
        public IActionResult Create()
        {
            MesproductPlan mesproductPlan = new MesproductPlan()
            {
                Id = 0
            };
            ViewData["WorkerId1"] = new SelectList(_context.Mesworker, "Id", "WorkerName");
            ViewData["WorkerId2"] = new SelectList(_context.Mesworker, "Id", "WorkerName");
            return PartialView("_CreateModalPartion", mesproductPlan);
        }

        // POST: MesproductPlans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LineId,StartTime,Muszak,Active,Maintenance,WorkerId1,WorkerId2")] MesproductPlan mesproductPlan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mesproductPlan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["WorkerId1"] = new SelectList(_context.Mesworker, "Id", "WorkerName", mesproductPlan.WorkerId1);
            ViewData["WorkerId2"] = new SelectList(_context.Mesworker, "Id", "WorkerName", mesproductPlan.WorkerId2);
            return PartialView("_CreateModalPartion", mesproductPlan);
        }

        // GET: MesproductPlans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MesproductPlan == null)
            {
                return NotFound();
            }

            var mesproductPlan = await _context.MesproductPlan.FindAsync(id);
            if (mesproductPlan == null)
            {
                return NotFound();
            }
            ViewData["WorkerId1"] = new SelectList(_context.Mesworker, "Id", "WorkerName", mesproductPlan.WorkerId1);
            ViewData["WorkerId2"] = new SelectList(_context.Mesworker, "Id", "WorkerName", mesproductPlan.WorkerId2);
            return PartialView("_EditModalPartion", mesproductPlan);
        }

        // POST: MesproductPlans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LineId,StartTime,Muszak,Active,Maintenance,WorkerId1,WorkerId2")] MesproductPlan mesproductPlan)
        {
            if (id != mesproductPlan.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mesproductPlan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MesproductPlanExists(mesproductPlan.Id))
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
            ViewData["WorkerId1"] = new SelectList(_context.Mesworker, "Id", "WorkerName", mesproductPlan.WorkerId1);
            ViewData["WorkerId2"] = new SelectList(_context.Mesworker, "Id", "WorkerName", mesproductPlan.WorkerId2);
            return PartialView("_EditModalPartion", mesproductPlan);
        }

        // GET: MesproductPlans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MesproductPlan == null)
            {
                return NotFound();
            }

            var mesproductPlan = await _context.MesproductPlan
                .Include(m => m.WorkerId1Navigation)
                .Include(m => m.WorkerId2Navigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mesproductPlan == null)
            {
                return NotFound();
            }

            return View(mesproductPlan);
        }

        // POST: MesproductPlans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MesproductPlan == null)
            {
                return Problem("Entity set 'EISDBContext.MesproductPlan'  is null.");
            }
            var mesproductPlan = await _context.MesproductPlan.FindAsync(id);
            if (mesproductPlan != null)
            {
                _context.MesproductPlan.Remove(mesproductPlan);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MesproductPlanExists(int id)
        {
          return _context.MesproductPlan.Any(e => e.Id == id);
        }
    }
}

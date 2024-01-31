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
using Microsoft.IdentityModel.Tokens;

namespace EIS.Controllers
{
    public class LzTeljesitmenyTablesController : Controller
    {
        private readonly EISDBContext _context;

        public LzTeljesitmenyTablesController(EISDBContext context)
        {
            _context = context;
        }

        // GET: LzTeljesitmenyTables
        public async Task<IActionResult> Index()
        {
            HttpContext.Session.SetString("Submenu", "2");
            HttpContext.Session.SetInt32("ShiftGroup", 1);
            HttpContext.Session.SetInt32("LZGroup", 1);
            HttpContext.Session.SetInt32("DayGroup", 3);
            HttpContext.Session.SetInt32("AddGroup", 1);
            HttpContext.Session.SetInt32("LZOPGroup", 0);
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
            DateTime firstday = new DateTime(yearfrom, monthfrom, 1, 6, 0, 0);
            DateTime firstdayto = new DateTime(yearto, monthto, 1, 6, 0, 0);
            DateTime lastday = firstday.AddMonths(1).AddDays(-1);
            DateTime lastdayto = firstdayto.AddMonths(1).AddDays(-1);
            if (lastdayto > lastday)
            {
                lastday = lastdayto;
            }
            DateTime dateTimeFrom = new DateTime(yearfrom, monthfrom, dayfrom, 6, 0, 0);
            DateTime dateTimeTo = new DateTime(yearto, monthto, dayto, 6, 0, 0);
            var eISDBContext = await _context.ViewLzTeljesitmenyTable.Where(a => (shift == 4 | a.MuszakId == shift) & (dolgozoid == 0 | a.DolgozoId == dolgozoid) & (a.Datum >= dateTimeFrom.Date & a.Datum <= dateTimeTo.Date)).ToListAsync();
            return View(eISDBContext);
        }

        public JsonResult GetKefetartoId(int? MuveletId)
        {
            if (MuveletId is null)
            {
                MuveletId = 0;
            }
            var lzkefetartoid = _context.GetLZKefetartoId(MuveletId).OrderBy(a => a.Kefetartoszam);
            int kefetartoid = 0;
            if (lzkefetartoid.Count()>0)
            {
                kefetartoid = lzkefetartoid.FirstOrDefault().KefetartoId;
            }
            var lista = new SelectList(lzkefetartoid, "KefetartoId", "Kefetartoszam",kefetartoid);
            //ViewData["KefetartoId"] = lista;
            return Json(lista);
        }

        // GET: LzTeljesitmenyTables/Create
        public IActionResult Create()
        {
            #region GetServerDatas v1.2
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

            int DolgozoId = 11005;
            if (!HttpContext.Session.GetInt32("DolgozoId").HasValue)
            {
                DolgozoId = 11005;
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

            HttpContext.Session.SetInt32("Shift", shift);
            HttpContext.Session.SetString("ShiftTxt", shifttxt);

            HttpContext.Session.SetInt32("Company", company);

            HttpContext.Session.SetInt32("DolgozoId", dolgozoid);
            #endregion
            DateTime date = new DateTime(year, month, day);
            LzTeljesitmenyTable lzTeljesitmenyTable = new LzTeljesitmenyTable()
            {
                Id = 0,
                DolgozoId = dolgozoid,
                MuveletId = 0,
                KefetartoId = 0,
                Datum = date,
                MuszakId = shift>3 ? 1 : shift,
                Darab = 0,
                HibasDarab = 0,
                Ora = 8
            };
            ViewData["DolgozoId"] = new SelectList(_context.LzDolgozoTable.Where(a => a.Active), "DolgozoId", "Search", lzTeljesitmenyTable.DolgozoId);
            ViewData["MuszakId"] = new SelectList(_context.LzMuszakTable, "Id", "Description",lzTeljesitmenyTable.MuszakId);
            var lzmuveletid = _context.GetLZMuveletId(lzTeljesitmenyTable.MuveletId).OrderBy(a=>a.Search);
            int muveletid = lzTeljesitmenyTable.MuveletId;
            if (lzmuveletid.Count() > 0)
            {
                muveletid = lzmuveletid.FirstOrDefault().Id;
                ViewData["MuveletId"] = new SelectList(lzmuveletid, "Id", "Search", muveletid);
            }
            else
            {
                muveletid = _context.LzMuveletReszletTable.Where(w => w.Interval == false).OrderBy(a => a.Search).FirstOrDefault().Id;
                ViewData["MuveletId"] = new SelectList(_context.LzMuveletReszletTable.Where(w => w.Interval == false).OrderBy(a=>a.Search), "Id", "Search", muveletid);
            }
            lzTeljesitmenyTable.MuveletId = muveletid;
            var lzkefetartoid = _context.GetLZKefetartoId(lzTeljesitmenyTable.MuveletId).OrderBy(a=>a.Kefetartoszam);
            int kefetartoid = lzTeljesitmenyTable.KefetartoId;
            if (lzkefetartoid.Count() > 0)
            {
                kefetartoid = lzkefetartoid.FirstOrDefault().KefetartoId;
                ViewData["KefetartoId"] = new SelectList(lzkefetartoid, "KefetartoId", "Kefetartoszam", kefetartoid);
            }
            else
            {
                kefetartoid = _context.LzKefetartotipusTable.OrderBy(a => a.Kefetartoszam).FirstOrDefault().Id;
                ViewData["KefetartoId"] = new SelectList(_context.LzKefetartotipusTable.OrderBy(a=>a.Kefetartoszam), "Id", "Kefetartoszam", kefetartoid);
            }
            lzTeljesitmenyTable.KefetartoId = kefetartoid;
            return PartialView("_CreateModalPartion", lzTeljesitmenyTable);
        }

        // POST: LzTeljesitmenyTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DolgozoId,Datum,Darab,HibasDarab,Megjegyzes,Ora,MuszakId,MuveletId,KefetartoId")] LzTeljesitmenyTable lzTeljesitmenyTable)
        {
            ModelState.ClearValidationState("Ora");
            ModelState.MarkFieldValid("Ora");
            if (ModelState.IsValid)
            {
                if (lzTeljesitmenyTable.KefetartoId==0)
                {
                    lzTeljesitmenyTable.KefetartoId = 1615;
                }
                if (lzTeljesitmenyTable.Megjegyzes.IsNullOrEmpty())
                {
                    lzTeljesitmenyTable.Megjegyzes = "";
                }
                _context.Add(lzTeljesitmenyTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DolgozoId"] = new SelectList(_context.LzDolgozoTable.Where(a => a.Active), "DolgozoId", "Search", lzTeljesitmenyTable.DolgozoId);
            ViewData["MuszakId"] = new SelectList(_context.LzMuszakTable, "Id", "Description", lzTeljesitmenyTable.MuszakId);
            var lzmuveletid = _context.GetLZMuveletId(lzTeljesitmenyTable.MuveletId).OrderBy(a => a.Search);
            if (lzmuveletid.Count() > 0)
            {
                ViewData["MuveletId"] = new SelectList(lzmuveletid, "Id", "Search", lzTeljesitmenyTable.MuveletId);
            }
            else
            {
                ViewData["MuveletId"] = new SelectList(_context.LzMuveletReszletTable.Where(w => w.Interval == false).OrderBy(a => a.Search), "Id", "Search", lzTeljesitmenyTable.MuveletId);
            }
            var lzkefetartoid = _context.GetLZKefetartoId(lzTeljesitmenyTable.MuveletId).OrderBy(a => a.Kefetartoszam);
            if (lzkefetartoid.Count() > 0)
            {
                ViewData["KefetartoId"] = new SelectList(lzkefetartoid, "KefetartoId", "Kefetartoszam", lzTeljesitmenyTable.KefetartoId);
            }
            else
            {
                ViewData["KefetartoId"] = new SelectList(_context.LzKefetartotipusTable.OrderBy(a => a.Kefetartoszam), "Id", "Kefetartoszam", lzTeljesitmenyTable.KefetartoId);
            }
            return PartialView("_CreateModalPartion", lzTeljesitmenyTable);
        }

        // GET: LzTeljesitmenyTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.LzTeljesitmenyTable == null)
            {
                return NotFound();
            }

            var lzTeljesitmenyTable = await _context.LzTeljesitmenyTable.FindAsync(id);
            if (lzTeljesitmenyTable == null)
            {
                return NotFound();
            }
            if (lzTeljesitmenyTable.Ora <= 0)
            {
                lzTeljesitmenyTable.Ora = 0;
            }
            else
            {
                if (lzTeljesitmenyTable.Ora >= 12)
                {
                    lzTeljesitmenyTable.Ora = 12;
                }
            }
            ViewData["DolgozoId"] = new SelectList(_context.LzDolgozoTable.Where(a => a.Active), "DolgozoId", "Search", lzTeljesitmenyTable.DolgozoId);
            ViewData["MuszakId"] = new SelectList(_context.LzMuszakTable, "Id", "Description", lzTeljesitmenyTable.MuszakId);
            var lzmuveletid = _context.GetLZMuveletId(lzTeljesitmenyTable.MuveletId).OrderBy(a => a.Search);
            if (lzmuveletid.Count() > 0)
            {
                ViewData["MuveletId"] = new SelectList(lzmuveletid, "Id", "Search", lzTeljesitmenyTable.MuveletId);
            }
            else
            {
                ViewData["MuveletId"] = new SelectList(_context.LzMuveletReszletTable.OrderBy(a => a.Search), "Id", "Search", lzTeljesitmenyTable.MuveletId);
            }
            var lzkefetartoid = _context.GetLZKefetartoId(lzTeljesitmenyTable.MuveletId).OrderBy(a => a.Kefetartoszam);
            if (lzkefetartoid.Count() > 0)
            {
                ViewData["KefetartoId"] = new SelectList(lzkefetartoid, "KefetartoId", "Kefetartoszam", lzTeljesitmenyTable.KefetartoId);
            }
            else
            {
                ViewData["KefetartoId"] = new SelectList(_context.LzKefetartotipusTable.OrderBy(a => a.Kefetartoszam), "Id", "Kefetartoszam", lzTeljesitmenyTable.KefetartoId);
            }
            return PartialView("_EditModalPartion", lzTeljesitmenyTable);
        }

        // POST: LzTeljesitmenyTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DolgozoId,Datum,Darab,HibasDarab,Megjegyzes,Ora,MuszakId,MuveletId,KefetartoId")] LzTeljesitmenyTable lzTeljesitmenyTable)
        {
            if (id != lzTeljesitmenyTable.Id)
            {
                return NotFound();
            }

            ModelState.ClearValidationState("Ora");
            ModelState.MarkFieldValid("Ora");
            if (ModelState.IsValid)
            {
                try
                {
                    if (lzTeljesitmenyTable.Ora <= 0)
                    {
                        lzTeljesitmenyTable.Ora = 0;
                    }
                    else
                    {
                        if (lzTeljesitmenyTable.Ora >= 12)
                        {
                            lzTeljesitmenyTable.Ora = 12;
                        }
                    }
                    _context.Update(lzTeljesitmenyTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LzTeljesitmenyTableExists(lzTeljesitmenyTable.Id))
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
            ViewData["DolgozoId"] = new SelectList(_context.LzDolgozoTable.Where(a => a.Active), "DolgozoId", "DolgozoNeve", lzTeljesitmenyTable.DolgozoId);
            ViewData["MuszakId"] = new SelectList(_context.LzMuszakTable, "Id", "Description", lzTeljesitmenyTable.MuszakId);
            var lzmuveletid = _context.GetLZMuveletId(lzTeljesitmenyTable.MuveletId).OrderBy(a => a.Search);
            if (lzmuveletid.Count() > 0)
            {
                ViewData["MuveletId"] = new SelectList(lzmuveletid, "Id", "Search", lzTeljesitmenyTable.MuveletId);
            }
            else
            {
                ViewData["MuveletId"] = new SelectList(_context.LzMuveletReszletTable.OrderBy(a => a.Search), "Id", "Search", lzTeljesitmenyTable.MuveletId);
            }
            var lzkefetartoid = _context.GetLZKefetartoId(lzTeljesitmenyTable.MuveletId).OrderBy(a => a.Kefetartoszam);
            if (lzkefetartoid.Count() > 0)
            {
                ViewData["KefetartoId"] = new SelectList(lzkefetartoid, "KefetartoId", "Kefetartoszam", lzTeljesitmenyTable.KefetartoId);
            }
            else
            {
                ViewData["KefetartoId"] = new SelectList(_context.LzKefetartotipusTable.OrderBy(a => a.Kefetartoszam), "Id", "Kefetartoszam", lzTeljesitmenyTable.KefetartoId);
            }
            return PartialView("_EditModalPartion", lzTeljesitmenyTable);
        }

        // GET: LzTeljesitmenyTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.LzTeljesitmenyTable == null)
            {
                return NotFound();
            }

            var lzTeljesitmenyTable = await _context.ViewLzTeljesitmenyTable
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lzTeljesitmenyTable == null)
            {
                return NotFound();
            }

            return PartialView("_DeleteModalPartion", lzTeljesitmenyTable);
        }

        // POST: LzTeljesitmenyTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.LzTeljesitmenyTable == null)
            {
                return Problem("Entity set 'EISDBContext.LzTeljesitmenyTable'  is null.");
            }
            var lzTeljesitmenyTable = await _context.LzTeljesitmenyTable.FindAsync(id);
            if (lzTeljesitmenyTable != null)
            {
                _context.LzTeljesitmenyTable.Remove(lzTeljesitmenyTable);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LzTeljesitmenyTableExists(int id)
        {
          return _context.LzTeljesitmenyTable.Any(e => e.Id == id);
        }
    }
}

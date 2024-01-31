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
using Microsoft.VisualBasic;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Localization;

namespace EIS.Controllers
{
    public class LzIntervalTablesController : Controller
    {
        private readonly EISDBContext _context;

        public LzIntervalTablesController(EISDBContext context)
        {
            _context = context;
        }

        // GET: LzIntervalTables
        public async Task<IActionResult> Index()
        {
            HttpContext.Session.SetString("Submenu", "2");
            HttpContext.Session.SetInt32("ShiftGroup", 0);
            HttpContext.Session.SetInt32("LineIDGroup", 0);
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
            var eISDBContext = _context.ViewLzIntervalTable.Where(a => (dolgozoid==0|a.DolgozoId==dolgozoid) & ((a.DatumTol >= dateTimeFrom.Date & a.DatumTol <= dateTimeTo.Date) | (a.DatumIg >= dateTimeFrom.Date & a.DatumIg <= dateTimeTo.Date)));
            return View(await eISDBContext.ToListAsync());
        }

        // GET: LzIntervalTables/Create
        public async Task<IActionResult> CreateAsync()
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
            var lzDolgozoTable = await _context.LzDolgozoTable.Where(a=>a.DolgozoId==dolgozoid).FirstOrDefaultAsync();
            if (lzDolgozoTable == null)
            {
                lzDolgozoTable = await _context.LzDolgozoTable.FirstAsync();
                dolgozoid = lzDolgozoTable.DolgozoId;
                //return NotFound();
            }
            LzIntervalTable lzIntervalTable = new LzIntervalTable()
            {
                Id = 0,
                DolgozoId = dolgozoid,
                DatumTol = date,
                DatumIg = date.AddDays(7),
                Ora = lzDolgozoTable.Munkaido,
            };
            ViewData["DolgozoId"] = new SelectList(_context.LzDolgozoTable.OrderBy(a => a.DolgozoId), "DolgozoId", "Search");
            ViewData["MuveletId"] = new SelectList(_context.LzMuveletReszletTable.Where(w=>w.Interval), "Id", "Search");
            return PartialView("_CreateModalPartion", lzIntervalTable);
        }

        // POST: LzIntervalTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DolgozoId,DatumTol,DatumIg,Megjegyzes,Ora,MuveletId")] LzIntervalTable lzIntervalTable)
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
            var lzDolgozoTable = await _context.LzDolgozoTable.Where(a => a.DolgozoId == lzIntervalTable.DolgozoId).FirstOrDefaultAsync();
            if (lzDolgozoTable == null)
            {
                lzDolgozoTable = await _context.LzDolgozoTable.FirstAsync();
                dolgozoid = lzDolgozoTable.DolgozoId;
                //return NotFound();
            }
            else
            {
                if (lzIntervalTable.Ora!= lzDolgozoTable.Munkaido)
                {
                    lzIntervalTable.Ora = lzDolgozoTable.Munkaido;
                }
            }
            ModelState.ClearValidationState("Ora");
            ModelState.MarkFieldValid("Ora");
            if (ModelState.IsValid)
            {
                _context.Add(lzIntervalTable);
                await _context.SaveChangesAsync();
                DateTime date = lzIntervalTable.DatumTol.Date;
                DateTime datetol = lzIntervalTable.DatumTol.Date;
                DateTime dateig = lzIntervalTable.DatumIg.Date;
                if (datetol > dateig)
                {
                    datetol = lzIntervalTable.DatumIg.Date;
                    dateig = lzIntervalTable.DatumTol.Date;
                }
                int days = (dateig-datetol).Days;
                int darab = 0;
                for (int i = 0; i <= days; i++)
                {
                    var lzMunkanapokTable = await _context.ViewLzMunkanapTable.FirstOrDefaultAsync(m => m.Datum == date.AddDays(i).Date);
                    if (lzMunkanapokTable != null)
                    {
                        if (lzMunkanapokTable.Datum == date.AddDays(i).Date)
                        {
                            var lzMuveletReszletTable = await _context.LzMuveletReszletTable.FirstOrDefaultAsync(m => m.Id == lzIntervalTable.MuveletId);
                            if (lzMuveletReszletTable != null)
                            {
                                if (lzMuveletReszletTable.Id == lzIntervalTable.MuveletId)
                                {
                                    if ((lzMunkanapokTable.Munkanap & lzMunkanapokTable.Munkanap == lzMuveletReszletTable.Munkanap) //|
                                        //(lzMunkanapokTable.Unnep & lzMunkanapokTable.Unnep == lzMuveletReszletTable.Unnep) |
                                        //(lzMunkanapokTable.Pihenonap & lzMunkanapokTable.Pihenonap == lzMuveletReszletTable.Pihenonap)
                                        )
                                    {
                                        var getLZKefetartoId = _context.GetLZKefetartoId(lzIntervalTable.MuveletId).First();
                                        if (getLZKefetartoId != null)
                                        {
                                            LzTeljesitmenyTable lzTeljesitmenyTable = new LzTeljesitmenyTable()
                                            {
                                                Id = 0,
                                                DolgozoId = lzIntervalTable.DolgozoId,
                                                Datum = date.AddDays(i).Date,
                                                Ora = lzIntervalTable.Ora,
                                                Megjegyzes = lzIntervalTable.Megjegyzes.IsNullOrEmpty() ? "-" : lzIntervalTable.Megjegyzes,
                                                MuveletId = lzIntervalTable.MuveletId,
                                                KefetartoId = getLZKefetartoId.KefetartoId,
                                                MuszakId = 1,
                                                Darab = 0,
                                                HibasDarab = 0,
                                            };
                                            var viewLzTeljesitmenyTable = _context.ViewLzTeljesitmenyTable.
                                                Where(a => ( 
                                                    a.DolgozoId == lzTeljesitmenyTable.DolgozoId &
                                                    a.Datum == lzTeljesitmenyTable.Datum &
                                                    a.Ora == lzTeljesitmenyTable.Ora &
                                                    a.MuveletId == lzTeljesitmenyTable.MuveletId &
                                                    a.KefetartoId == lzTeljesitmenyTable.KefetartoId &
                                                    a.MuszakId == lzTeljesitmenyTable.MuszakId &
                                                    a.Darab == lzTeljesitmenyTable.Darab & 
                                                    a.HibasDarab == lzTeljesitmenyTable.HibasDarab
                                                    ));
                                            if (viewLzTeljesitmenyTable.Count() == 0)
                                            {
                                                try
                                                {
                                                    _context.Add(lzTeljesitmenyTable);
                                                    await _context.SaveChangesAsync();
                                                    darab++;
                                                }
                                                catch
                                                {

                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                ViewData["Darab"] = darab;
                return RedirectToAction(nameof(Index));
            }
            ViewData["DolgozoId"] = new SelectList(_context.LzDolgozoTable.OrderBy(a=>a.DolgozoId), "DolgozoId", "Search", lzIntervalTable.DolgozoId);
            ViewData["MuveletId"] = new SelectList(_context.LzMuveletReszletTable.Where(w => w.Interval), "Id", "Search", lzIntervalTable.MuveletId);
            return PartialView("_CreateModalPartion", lzIntervalTable);
        }

        // GET: LzIntervalTables/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.LzIntervalTable == null)
            {
                return NotFound();
            }

            var lzIntervalTable = await _context.LzIntervalTable.FindAsync(id);
            if (lzIntervalTable == null)
            {
                return NotFound();
            }
            var lzDolgozoTable = await _context.LzDolgozoTable.Where(a => a.DolgozoId == lzIntervalTable.DolgozoId).FirstOrDefaultAsync();
            if (lzDolgozoTable == null)
            {
                return NotFound();
            }
            else
            {
                if (lzIntervalTable.Ora != lzDolgozoTable.Munkaido)
                {
                    lzIntervalTable.Ora = lzDolgozoTable.Munkaido;
                }
            }
            ViewData["DolgozoId"] = new SelectList(_context.LzDolgozoTable.OrderBy(a => a.DolgozoId), "DolgozoId", "Search", lzIntervalTable.DolgozoId);
            ViewData["MuveletId"] = new SelectList(_context.LzMuveletReszletTable.Where(w => w.Interval), "Id", "Search", lzIntervalTable.MuveletId);
            return PartialView("_EditModalPartion", lzIntervalTable);
        }

        // POST: LzIntervalTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DolgozoId,DatumTol,DatumIg,Megjegyzes,Ora,MuveletId")] LzIntervalTable lzIntervalTable)
        {
            if (id != lzIntervalTable.Id)
            {
                return NotFound();
            }

            var lzDolgozoTable = await _context.LzDolgozoTable.Where(a => a.DolgozoId == lzIntervalTable.DolgozoId).FirstOrDefaultAsync();
            if (lzDolgozoTable == null)
            {
                return NotFound();
            }
            else
            {
                if (lzIntervalTable.Ora != lzDolgozoTable.Munkaido)
                {
                    lzIntervalTable.Ora = lzDolgozoTable.Munkaido;
                }
            }
            ModelState.ClearValidationState("Ora");
            ModelState.MarkFieldValid("Ora");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lzIntervalTable);
                    await _context.SaveChangesAsync();
                    DateTime date = lzIntervalTable.DatumTol.Date;
                    DateTime datetol = lzIntervalTable.DatumTol.Date;
                    DateTime dateig = lzIntervalTable.DatumIg.Date;
                    if (datetol > dateig)
                    {
                        datetol = lzIntervalTable.DatumIg.Date;
                        dateig = lzIntervalTable.DatumTol.Date;
                    }
                    int days = (dateig - datetol).Days;
                    int darab = 0;
                    for (int i = 0; i <= days; i++)
                    {
                        var lzMunkanapokTable = await _context.ViewLzMunkanapTable.FirstOrDefaultAsync(m => m.Datum == date.AddDays(i).Date);
                        if (lzMunkanapokTable != null)
                        {
                            if (lzMunkanapokTable.Datum == date.AddDays(i).Date)
                            {
                                var lzMuveletReszletTable = await _context.LzMuveletReszletTable.FirstOrDefaultAsync(m => m.Id == lzIntervalTable.MuveletId);
                                if (lzMuveletReszletTable != null)
                                {
                                    if (lzMuveletReszletTable.Id == lzIntervalTable.MuveletId)
                                    {
                                        if ((lzMunkanapokTable.Munkanap & lzMunkanapokTable.Munkanap == lzMuveletReszletTable.Munkanap) |
                                            (lzMunkanapokTable.Unnep & lzMunkanapokTable.Unnep == lzMuveletReszletTable.Unnep) |
                                            (lzMunkanapokTable.Pihenonap & lzMunkanapokTable.Pihenonap == lzMuveletReszletTable.Pihenonap))
                                        {
                                            var getLZKefetartoId = _context.GetLZKefetartoId(lzIntervalTable.MuveletId).First();
                                            if (getLZKefetartoId != null)
                                            {
                                                LzTeljesitmenyTable lzTeljesitmenyTable = new LzTeljesitmenyTable()
                                                {
                                                    Id = 0,
                                                    DolgozoId = lzIntervalTable.DolgozoId,
                                                    Datum = date.AddDays(i).Date,
                                                    Ora = lzIntervalTable.Ora,
                                                    Megjegyzes = lzIntervalTable.Megjegyzes.IsNullOrEmpty() ? "-" : lzIntervalTable.Megjegyzes,
                                                    MuveletId = lzIntervalTable.MuveletId,
                                                    KefetartoId = getLZKefetartoId.KefetartoId,
                                                    MuszakId = 1,
                                                    Darab = 0,
                                                    HibasDarab = 0,
                                                };
                                                var viewLzTeljesitmenyTable = _context.ViewLzTeljesitmenyTable.
                                                    Where(a => (
                                                        a.DolgozoId == lzTeljesitmenyTable.DolgozoId &
                                                        a.Datum == lzTeljesitmenyTable.Datum &
                                                        a.Ora == lzTeljesitmenyTable.Ora &
                                                        a.MuveletId == lzTeljesitmenyTable.MuveletId &
                                                        a.KefetartoId == lzTeljesitmenyTable.KefetartoId &
                                                        a.MuszakId == lzTeljesitmenyTable.MuszakId &
                                                        a.Darab == lzTeljesitmenyTable.Darab &
                                                        a.HibasDarab == lzTeljesitmenyTable.HibasDarab
                                                        ));
                                                if (viewLzTeljesitmenyTable.Count() == 0)
                                                {
                                                    try
                                                    {
                                                        _context.Add(lzTeljesitmenyTable);
                                                        await _context.SaveChangesAsync();
                                                        darab++;
                                                    }
                                                    catch
                                                    {

                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    ViewData["Darab"] = darab;
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LzIntervalTableExists(lzIntervalTable.Id))
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
            ViewData["DolgozoId"] = new SelectList(_context.LzDolgozoTable.OrderBy(a => a.DolgozoId), "DolgozoId", "Search", lzIntervalTable.DolgozoId);
            ViewData["MuveletId"] = new SelectList(_context.LzMuveletReszletTable.Where(w => w.Interval), "Id", "Search", lzIntervalTable.MuveletId);
            return PartialView("_EditModalPartion", lzIntervalTable);
        }

        // GET: LzIntervalTables/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.LzIntervalTable == null)
            {
                return NotFound();
            }

            var lzIntervalTable = await _context.ViewLzIntervalTable
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lzIntervalTable == null)
            {
                return NotFound();
            }

            return PartialView("_DeleteModalPartion", lzIntervalTable);
        }

        // POST: LzIntervalTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.LzIntervalTable == null)
            {
                return Problem("Entity set 'EISDBContext.LzIntervalTable'  is null.");
            }
            var lzIntervalTable = await _context.LzIntervalTable.FindAsync(id);
            if (lzIntervalTable != null)
            {
                _context.LzIntervalTable.Remove(lzIntervalTable);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LzIntervalTableExists(int id)
        {
          return _context.LzIntervalTable.Any(e => e.Id == id);
        }
    }
}

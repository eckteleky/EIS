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
using Humanizer;

namespace EIS.Controllers
{
    public class EasyMesjobsController : Controller
    {
        private readonly EISDBContext _context;

        public EasyMesjobsController(EISDBContext context)
        {
            _context = context;
        }

        // GET: EasyMesjobs
        public async Task<IActionResult> Index()
        {
            HttpContext.Session.SetString("Submenu", "1");
            HttpContext.Session.SetInt32("LZGroup", 0);
            HttpContext.Session.SetInt32("LineIDGroup", 1);
            HttpContext.Session.SetInt32("StationGroup", 0);
            HttpContext.Session.SetInt32("ShiftGroup", 0);
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

            DateTime dateTimeFrom = new DateTime(yearfrom, monthfrom, dayfrom, 6, 0, 0);
            DateTime dateTimeTo = new DateTime(yearto, monthto, dayto, 6, 0, 0);
            var thissunday = dateTimeFrom.AddDays(-(int)DateTime.Today.DayOfWeek);
            var nextsunday = thissunday.AddDays(7);
            if (yearfrom==yearto & monthfrom==monthto & dayfrom == dayto)
            {
                yearfrom = thissunday.Year;
                monthfrom = thissunday.Month;
                dayfrom = thissunday.Day;
                yearto = nextsunday.Year;
                monthto = nextsunday.Month;
                dayto = nextsunday.Day;
            }
            dateTimeFrom = new DateTime(yearfrom, monthfrom, dayfrom, 22, 40, 0);
            dateTimeTo = new DateTime(yearto, monthto, dayto, 22, 40, 0);

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
            //var eISDBContext = await _context.EasyMesjob.Where(a => (a.SystemParam.LineId == LineID | LineID == "ALL Line") & a.CalcStart >= dateTimeFrom & a.CalcStart < dateTimeTo.AddDays(1)).Include(e => e.Status).Include(e => e.SystemParam).Include(e => e.Ttstypes).OrderBy(x => x.RowNo).ToListAsync();
            var eISDBContext = await _context.EasyMesjob.Where(a => (a.SystemParam.LineId == LineID | LineID == "ALL Line")).Include(e => e.Status).Include(e => e.SystemParam).Include(e => e.Ttstypes).OrderBy(x => x.RowNo).ToListAsync();
            int count = 1;           
            DateTime df = dateTimeFrom;
            DateTime dcs = dateTimeFrom;
            DateTime dce = dateTimeTo;
            DateTime de = dateTimeTo;
            if (eISDBContext.Count() > 0)
            {
                foreach (var itemId in eISDBContext)
                {
                    try
                    {
                        EasyMesjob item = await _context.EasyMesjob.Where(x => x.Id == itemId.Id).FirstOrDefaultAsync();
                        if (count > 1)
                        {
                            dcs = dcs.AddMinutes(item.Ttstypes.ChangeTime.TotalMinutes);
                        }
                        if (item.TargetStart.HasValue)
                        {
                            if (item.CalcTargetStart & item.TargetStart.Value > dcs)
                            {
                                dcs = item.TargetStart.Value;
                            }
                        }
                        if(count==1 & dcs>df)
                        {
                            df = dcs;
                        }
                        int delta = (int)Math.Round(Math.Max(5, Math.Round(0.5 + item.PlanQty * item.Ttstypes.CycleTime / 300, 0) * 5));
                        dce = dcs.AddMinutes(delta);
                        item.RowNo = count;
                        item.CalcStart = dcs;
                        item.CalcEnd = dce;
                        _context.EasyMesjob.Update(item);
                        await _context.SaveChangesAsync();
                        dcs = dce;
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                    count++;
                }
            }
            ViewBag.lineid = LineID;
            ViewBag.Scalefrom = df;
            ViewBag.Scaleto = dce;//>de ? dce: de;
            return View(eISDBContext);
        }

        public async Task<ActionResult> UpdateItemAsync(string itemIds)
        {
            int count = 1;
            List<int> itemIdList = new List<int>();
            itemIdList = itemIds.Split(",".ToArray(), StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            foreach (var itemId in itemIdList)
            {
                try
                {
                    EasyMesjob item = await _context.EasyMesjob.Where(x => x.Id == itemId).FirstOrDefaultAsync();
                    item.RowNo = count;
                    _context.EasyMesjob.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    continue;
                }
                count++;
            }
            return Json(true);
        }

        // GET: EasyMesjobs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.EasyMesjob == null)
            {
                return NotFound();
            }

            var easyMesjob = await _context.EasyMesjob
                .Include(e => e.Status)
                .Include(e => e.SystemParam)
                .Include(e => e.Ttstypes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (easyMesjob == null)
            {
                return NotFound();
            }

            return View(easyMesjob);
        }

        // GET: EasyMesjobs/Create
        public async Task<IActionResult> Create()
        {
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

            DateTime dateTimeFrom = new DateTime(yearfrom, monthfrom, dayfrom, 6, 0, 0);
            DateTime dateTimeTo = new DateTime(yearto, monthto, dayto, 6, 0, 0);
            //var thissunday = dateTimeFrom.AddDays(-(int)DateTime.Today.DayOfWeek);
            //var nextsunday = dateTimeTo.AddDays(-(int)DateTime.Today.DayOfWeek + 6);
            //if (yearfrom==yearto & monthfrom==monthto & dayfrom == dayto)
            //{
            //    yearfrom = thissunday.Year;
            //    monthfrom = thissunday.Month;
            //    dayfrom = thissunday.Day;
            //    yearto = nextsunday.Year;
            //    monthto = nextsunday.Month;
            //    dayto = nextsunday.Day;
            //}
            //dateTimeFrom = new DateTime(yearfrom, monthfrom, dayfrom, 6, 0, 0);
            //dateTimeTo = new DateTime(yearto, monthto, dayto, 6, 0, 0);

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
            int RowNo = 0;
            var easyMesjobs = await _context.EasyMesjob.Where(a => (a.SystemParam.LineId == LineID | LineID == "ALL Line") & a.CalcStart >= dateTimeFrom & a.CalcStart < dateTimeTo.AddDays(1)).Include(e => e.Status).Include(e => e.SystemParam).Include(e => e.Ttstypes).ToListAsync();
            if (easyMesjobs != null)
            {
                RowNo = easyMesjobs.Count;
            }
            var systemParamTable = await _context.SystemParamTable.FindAsync(LineID);
            if (systemParamTable == null)
            {
                return NotFound();
            }
            var easyMesstatus = await _context.EasyMesstatus.Where(a => a.SystemParamId == systemParamTable.Id).FirstOrDefaultAsync();
            if (easyMesstatus == null)
            {
                return NotFound();
            }
            var easyMesttstypes = await _context.EasyMesttstypes.Where(a => a.SystemParamId == systemParamTable.Id).FirstOrDefaultAsync();
            if (easyMesttstypes == null)
            {
                return NotFound();
            }
            EasyMesjob easyMesjob = new EasyMesjob()
            {
                Id=0,
                SystemParamId = systemParamTable.Id,
                CalcAuto = true,
                CalcTargetEnd = false,
                CalcTargetStart = false,
                TtstypesId = easyMesttstypes.Id,
                Started = false,
                Stopped = false,
                Running = false,
                Closed = false,
                StatusId = easyMesstatus.Id,
                CalcStart = DateTime.Now,
                Percentage = 0,
                ProducedQty = 0,
                RowNo = RowNo+1
            };
            ViewData["StatusId"] = new SelectList(_context.EasyMesstatus.Where(a=>a.SystemParamId==easyMesjob.SystemParamId), "Id", "Status", easyMesjob.StatusId);
            ViewData["SystemParamId"] = new SelectList(_context.SystemParamTable.Where(a => a.Id == easyMesjob.SystemParamId), "Id", "LineId", easyMesjob.SystemParamId);
            ViewData["TtstypesId"] = new SelectList(_context.EasyMesttstypes.Where(a => a.SystemParamId == easyMesjob.SystemParamId), "Id", "TtstypeName", easyMesjob.TtstypesId);
            return PartialView("_CreateModalPartion", easyMesjob);
        }

        // POST: EasyMesjobs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TtstypesId,PlanQty,ProducedQty,Eol,BadParts,CalcStart,CalcEnd,JobStart,JobTimeStamp,JobFinish,SystemParamId,Started,Closed,Running,Stopped,StatusId,TargetEnd,TargetStart,Percentage,CalcAuto,CalcTargetEnd,CalcTargetStart,RowNo")] EasyMesjob easyMesjob)
        {
            if (ModelState.IsValid)
            {
                _context.Add(easyMesjob);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch { }
                return RedirectToAction(nameof(Index));
            }
            ViewData["StatusId"] = new SelectList(_context.EasyMesstatus.Where(a => a.SystemParamId == easyMesjob.SystemParamId), "Id", "Status", easyMesjob.StatusId);
            ViewData["SystemParamId"] = new SelectList(_context.SystemParamTable.Where(a => a.Id == easyMesjob.SystemParamId), "Id", "LineId", easyMesjob.SystemParamId);
            ViewData["TtstypesId"] = new SelectList(_context.EasyMesttstypes.Where(a => a.SystemParamId == easyMesjob.SystemParamId), "Id", "TtstypeName", easyMesjob.TtstypesId);
            return PartialView("_CreateModalPartion", easyMesjob);
        }

        // GET: EasyMesjobs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.EasyMesjob == null)
            {
                return NotFound();
            }
            var easyMesjob = await _context.EasyMesjob.FindAsync(id);
            if (easyMesjob == null)
            {
                return NotFound();
            }
            ViewData["StatusId"] = new SelectList(_context.EasyMesstatus.Where(a => a.SystemParamId == easyMesjob.SystemParamId), "Id", "Status", easyMesjob.StatusId);
            ViewData["SystemParamId"] = new SelectList(_context.SystemParamTable.Where(a => a.Id == easyMesjob.SystemParamId), "Id", "LineId", easyMesjob.SystemParamId);
            ViewData["TtstypesId"] = new SelectList(_context.EasyMesttstypes.Where(a => a.SystemParamId == easyMesjob.SystemParamId), "Id", "TtstypeName", easyMesjob.TtstypesId);
            return PartialView("_EditModalPartion", easyMesjob);
        }

        // POST: EasyMesjobs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TtstypesId,PlanQty,ProducedQty,Eol,BadParts,CalcStart,CalcEnd,JobStart,JobTimeStamp,JobFinish,SystemParamId,Started,Closed,Running,Stopped,StatusId,TargetStart,TargetEnd,Percentage,CalcAuto,CalcTargetStart,CalcTargetEnd,RowNo")] EasyMesjob easyMesjob)
        {
            if (id != easyMesjob.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (easyMesjob.TargetStart.HasValue)
                    {
                        if (easyMesjob.TargetStart.Value.Hour == 0 & easyMesjob.TargetStart.Value.Minute == 0)
                        {
                            DateTime st = easyMesjob.TargetStart.Value.AddHours(22).AddMinutes(40);
                            easyMesjob.TargetStart = st;
                        }
                    }
                    _context.Update(easyMesjob);
                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch { }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EasyMesjobExists(easyMesjob.Id))
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
            ViewData["StatusId"] = new SelectList(_context.EasyMesstatus.Where(a => a.SystemParamId == easyMesjob.SystemParamId), "Id", "Status", easyMesjob.StatusId);
            ViewData["SystemParamId"] = new SelectList(_context.SystemParamTable.Where(a => a.Id == easyMesjob.SystemParamId), "Id", "LineId", easyMesjob.SystemParamId);
            ViewData["TtstypesId"] = new SelectList(_context.EasyMesttstypes.Where(a => a.SystemParamId == easyMesjob.SystemParamId), "Id", "TtstypeName", easyMesjob.TtstypesId);
            return PartialView("_EditModalPartion", easyMesjob);
        }

        // GET: EasyMesjobs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.EasyMesjob == null)
            {
                return NotFound();
            }

            var easyMesjob = await _context.EasyMesjob
                .Include(e => e.Status)
                .Include(e => e.SystemParam)
                .Include(e => e.Ttstypes)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (easyMesjob == null)
            {
                return NotFound();
            }

            return View(easyMesjob);
        }

        // POST: EasyMesjobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.EasyMesjob == null)
            {
                return Problem("Entity set 'EISDBContext.EasyMesjob'  is null.");
            }
            var easyMesjob = await _context.EasyMesjob.FindAsync(id);
            if (easyMesjob != null)
            {
                _context.EasyMesjob.Remove(easyMesjob);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EasyMesjobExists(int id)
        {
          return _context.EasyMesjob.Any(e => e.Id == id);
        }
    }
}

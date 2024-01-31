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
using NuGet.Packaging.Signing;

namespace EIS.Controllers
{
    public class DtreportsController : Controller
    {
        private readonly EISDBContext _context;

        public DtreportsController(EISDBContext context)
        {
            _context = context;
        }

        //public IActionResult Select()
        //{
        //    HttpContext.Session.SetString("Submenu", "1");
        //    HttpContext.Session.SetInt32("LZGroup", 0);
        //    HttpContext.Session.SetInt32("LineIDGroup", 1);
        //    HttpContext.Session.SetInt32("StationGroup", 0);
        //    HttpContext.Session.SetInt32("ShiftGroup", 1);
        //    HttpContext.Session.SetInt32("DayGroup", 3);
        //    HttpContext.Session.SetInt32("AddGroup", 0);
        //    HttpContext.Session.SetInt32("HeaderGroup", 1);
        //    #region GetServerDatas v1.3
        //    string LineID = "GBM Line1";
        //    if (HttpContext.Session.GetString("LineID") == null)
        //    {
        //        LineID = "GBM Line1";// VALEO RSW2";
        //    }
        //    else
        //    {
        //        LineID = HttpContext.Session.GetString("LineID");
        //    }
        //    int Year = DateTime.Now.Year;
        //    if (!HttpContext.Session.GetInt32("Year").HasValue)
        //    {
        //        Year = DateTime.Now.Year;
        //    }
        //    else
        //    {
        //        Year = HttpContext.Session.GetInt32("Year").Value;
        //    }
        //    int year = (int)Year;
        //    int Month = DateTime.Now.Month;
        //    if (!HttpContext.Session.GetInt32("Month").HasValue)
        //    {
        //        Month = DateTime.Now.Month;
        //    }
        //    else
        //    {
        //        Month = HttpContext.Session.GetInt32("Month").Value;
        //    }
        //    int month = (int)Month;
        //    int Day = DateTime.Now.Day;
        //    if (!HttpContext.Session.GetInt32("Day").HasValue)
        //    {
        //        Day = DateTime.Now.Day;
        //    }
        //    else
        //    {
        //        Day = HttpContext.Session.GetInt32("Day").Value;
        //    }
        //    int day = (int)Day;
        //    int YearFrom = DateTime.Now.Year;
        //    if (!HttpContext.Session.GetInt32("YearFrom").HasValue)
        //    {
        //        YearFrom = DateTime.Now.Year;
        //    }
        //    else
        //    {
        //        YearFrom = HttpContext.Session.GetInt32("YearFrom").Value;
        //    }
        //    int yearfrom = (int)YearFrom;
        //    int MonthFrom = DateTime.Now.Month;
        //    if (!HttpContext.Session.GetInt32("MonthFrom").HasValue)
        //    {
        //        MonthFrom = DateTime.Now.Month;
        //    }
        //    else
        //    {
        //        MonthFrom = HttpContext.Session.GetInt32("MonthFrom").Value;
        //    }
        //    int monthfrom = (int)MonthFrom;
        //    int DayFrom = DateTime.Now.Day;
        //    if (!HttpContext.Session.GetInt32("DayFrom").HasValue)
        //    {
        //        DayFrom = DateTime.Now.Day;
        //    }
        //    else
        //    {
        //        DayFrom = HttpContext.Session.GetInt32("DayFrom").Value;
        //    }
        //    int dayfrom = (int)DayFrom;
        //    int YearTo = DateTime.Now.Year;
        //    if (!HttpContext.Session.GetInt32("YearTo").HasValue)
        //    {
        //        YearTo = DateTime.Now.Year;
        //    }
        //    else
        //    {
        //        YearTo = HttpContext.Session.GetInt32("YearTo").Value;
        //    }
        //    int yearto = (int)YearTo;
        //    int MonthTo = DateTime.Now.Month;
        //    if (!HttpContext.Session.GetInt32("MonthTo").HasValue)
        //    {
        //        MonthTo = DateTime.Now.Month;
        //    }
        //    else
        //    {
        //        MonthTo = HttpContext.Session.GetInt32("MonthTo").Value;
        //    }
        //    int monthto = (int)MonthTo;
        //    int DayTo = DateTime.Now.Day;
        //    if (!HttpContext.Session.GetInt32("DayTo").HasValue)
        //    {
        //        DayTo = DateTime.Now.Day;
        //    }
        //    else
        //    {
        //        DayTo = HttpContext.Session.GetInt32("DayTo").Value;
        //    }
        //    int dayto = (int)DayTo;
        //    int Shift = 4;
        //    if (!HttpContext.Session.GetInt32("Shift").HasValue)
        //    {
        //        Shift = 4;
        //    }
        //    else
        //    {
        //        Shift = HttpContext.Session.GetInt32("Shift").Value;
        //    }
        //    int shift = 4;
        //    string shifttxt = "All day";
        //    switch (Shift)
        //    {
        //        case 4: shifttxt = "All day"; shift = 4; break;
        //        case 1: shifttxt = "Morning"; shift = 1; break;
        //        case 2: shifttxt = "Afternoon"; shift = 2; break;
        //        case 3: shifttxt = "Overnight"; shift = 3; break;
        //        case 5: shifttxt = "Night+Morning"; shift = 5; break; // 1 + 3
        //        case 6: shifttxt = "Night+Afternoon"; shift = 6; break; // 2 + 3
        //        case 7: shifttxt = "Morning+Afternoon"; shift = 7; break; // 1 + 2
        //    }
        //    var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
        //    if (@requestCulture.RequestCulture.UICulture.Name == "hu-HU")
        //    {
        //        shifttxt = "Egész nap";
        //        switch (Shift)
        //        {
        //            case 4: shifttxt = "Egész nap"; shift = 4; break;
        //            case 1: shifttxt = "Délelőtt"; shift = 1; break;
        //            case 2: shifttxt = "Délután"; shift = 2; break;
        //            case 3: shifttxt = "Éjszaka"; shift = 3; break;
        //            case 5: shifttxt = "Éjszaka+Nappal"; shift = 5; break; // 1 + 3
        //            case 6: shifttxt = "Éjszaka+Délután"; shift = 6; break; // 2 + 3
        //            case 7: shifttxt = "Nappal+Délután"; shift = 7; break; // 1 + 2
        //        }
        //    }
        //    string Language = @requestCulture.RequestCulture.UICulture.Name;

        //    int Company = 1;
        //    if (!HttpContext.Session.GetInt32("Company").HasValue)
        //    {
        //        Company = 1;
        //    }
        //    else
        //    {
        //        Company = HttpContext.Session.GetInt32("Company").Value;
        //    }
        //    int company = (int)Company;

        //    int DolgozoId = 0;
        //    if (!HttpContext.Session.GetInt32("DolgozoId").HasValue)
        //    {
        //        DolgozoId = 0;
        //    }
        //    else
        //    {
        //        DolgozoId = HttpContext.Session.GetInt32("DolgozoId").Value;
        //    }
        //    int dolgozoid = (int)DolgozoId;

        //    if (HttpContext.Session.GetString("LineID") == null |
        //        HttpContext.Session.GetInt32("Year") == null |
        //        HttpContext.Session.GetInt32("Month") == null |
        //        HttpContext.Session.GetInt32("Day") == null |
        //        HttpContext.Session.GetInt32("Shift") == null |
        //        HttpContext.Session.GetInt32("Company") == null |
        //        HttpContext.Session.GetInt32("DolgozoId") == null)
        //    {
        //        Headers header = new()
        //        {
        //            MyFields = false,
        //            SearchString = ""
        //        };
        //        SessionHelper.SetObjectAsJson(HttpContext.Session, "header", header);
        //    }
        //    HttpContext.Session.SetString("LineID", LineID);

        //    HttpContext.Session.SetInt32("Year", year);
        //    HttpContext.Session.SetInt32("Month", month);
        //    HttpContext.Session.SetInt32("Day", day);

        //    HttpContext.Session.SetInt32("YearFrom", yearfrom);
        //    HttpContext.Session.SetInt32("MonthFrom", monthfrom);
        //    HttpContext.Session.SetInt32("DayFrom", dayfrom);

        //    HttpContext.Session.SetInt32("YearTo", yearto);
        //    HttpContext.Session.SetInt32("MonthTo", monthto);
        //    HttpContext.Session.SetInt32("DayTo", dayto);

        //    HttpContext.Session.SetInt32("Shift", shift);
        //    HttpContext.Session.SetString("ShiftTxt", shifttxt);

        //    HttpContext.Session.SetInt32("Company", company);

        //    HttpContext.Session.SetInt32("DolgozoId", dolgozoid);
        //    #endregion
        //    DateTime StartTimeFrom = new DateTime(YearFrom, MonthFrom, DayFrom, 6, 0, 0);
        //    DateTime StartTimeTo = new DateTime(YearTo, MonthTo, DayTo, 6, 0, 0);
        //    ViewData["LineID"] = LineID;
        //    ViewData["StartTime"] = StartTimeFrom.ToShortDateString() + "-" + StartTimeTo.ToShortDateString();
        //    ViewData["Shift"] = shifttxt;
        //    return View("Select");
        //}

        // GET: Dtreports
        public async Task<IActionResult> Index(int? Shift)
        {
            HttpContext.Session.SetString("Submenu", "1");
            HttpContext.Session.SetInt32("LZGroup", 0);
            HttpContext.Session.SetInt32("LineIDGroup", 1);
            HttpContext.Session.SetInt32("StationGroup", 0);
            HttpContext.Session.SetInt32("ShiftGroup", 1);
            HttpContext.Session.SetInt32("DayGroup", 3);
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
            if (Shift.HasValue)
            {
                HttpContext.Session.SetInt32("Shift", Shift.Value);
            }
            else
            {
                if (!HttpContext.Session.GetInt32("Shift").HasValue)
                {
                    Shift = 4;
                }
                else
                {
                    Shift = HttpContext.Session.GetInt32("Shift").Value;
                }
            }
            int shift = 4;
            string shifttxt = "All day";
            switch (Shift.Value)
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
                switch (Shift.Value)
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
            var alertsDBContext = await _context.ShiftsTable.Where(a => ((a.LineId == LineID) & (a.Starttime == StartTimeFrom) & (shift < 4 & a.Muszak == shift))).ToListAsync();
            HttpContext.Session.SetInt32("AddGroup", (StartTimeFrom != StartTimeTo | (HttpContext.Session.GetString("LineID") == "ALL Line") | (Shift.Value > 3) | alertsDBContext.Count() == 0) ? 2 : 1);
            ViewData["LineID"] = LineID;
            ViewData["StartTime"] = StartTimeFrom.ToShortDateString() + "-" + StartTimeTo.ToShortDateString();
            ViewData["Shift"] = shifttxt;
            return await Task.Run(async () => View(await _context.Dtreport.Where(a => (a.LineId == LineID | LineID == "ALL Line") & (a.StartTime >= StartTimeFrom & a.StartTime <= StartTimeTo) & (a.Shift == Shift | (Shift == 4))).Include(d => d.ErrorIda10Navigation).Include(d => d.ErrorIda11Navigation).Include(d => d.ErrorIda12Navigation).Include(d => d.ErrorIda13Navigation).Include(d => d.ErrorIda14Navigation).Include(d => d.ErrorIda15Navigation).Include(d => d.ErrorIda1Navigation).Include(d => d.ErrorIda2Navigation).Include(d => d.ErrorIda3Navigation).Include(d => d.ErrorIda4Navigation).Include(d => d.ErrorIda5Navigation).Include(d => d.ErrorIda6Navigation).Include(d => d.ErrorIda7Navigation).Include(d => d.ErrorIda8Navigation).Include(d => d.ErrorIda9Navigation).Include(d => d.ErrorIdb10Navigation).Include(d => d.ErrorIdb11Navigation).Include(d => d.ErrorIdb12Navigation).Include(d => d.ErrorIdb13Navigation).Include(d => d.ErrorIdb14Navigation).Include(d => d.ErrorIdb15Navigation).Include(d => d.ErrorIdb1Navigation).Include(d => d.ErrorIdb2Navigation).Include(d => d.ErrorIdb3Navigation).Include(d => d.ErrorIdb4Navigation).Include(d => d.ErrorIdb5Navigation).Include(d => d.ErrorIdb6Navigation).Include(d => d.ErrorIdb7Navigation).Include(d => d.ErrorIdb8Navigation).Include(d => d.ErrorIdb9Navigation).Include(d => d.ErrorIdc10Navigation).Include(d => d.ErrorIdc11Navigation).Include(d => d.ErrorIdc12Navigation).Include(d => d.ErrorIdc13Navigation).Include(d => d.ErrorIdc14Navigation).Include(d => d.ErrorIdc15Navigation).Include(d => d.ErrorIdc1Navigation).Include(d => d.ErrorIdc2Navigation).Include(d => d.ErrorIdc3Navigation).Include(d => d.ErrorIdc4Navigation).Include(d => d.ErrorIdc5Navigation).Include(d => d.ErrorIdc6Navigation).Include(d => d.ErrorIdc7Navigation).Include(d => d.ErrorIdc8Navigation).Include(d => d.ErrorIdc9Navigation).Include(d => d.ErrorIdd10Navigation).Include(d => d.ErrorIdd11Navigation).Include(d => d.ErrorIdd12Navigation).Include(d => d.ErrorIdd13Navigation).Include(d => d.ErrorIdd14Navigation).Include(d => d.ErrorIdd15Navigation).Include(d => d.ErrorIdd1Navigation).Include(d => d.ErrorIdd2Navigation).Include(d => d.ErrorIdd3Navigation).Include(d => d.ErrorIdd4Navigation).Include(d => d.ErrorIdd5Navigation).Include(d => d.ErrorIdd6Navigation).Include(d => d.ErrorIdd7Navigation).Include(d => d.ErrorIdd8Navigation).Include(d => d.ErrorIdd9Navigation).Include(d => d.ErrorIde10Navigation).Include(d => d.ErrorIde11Navigation).Include(d => d.ErrorIde12Navigation).Include(d => d.ErrorIde13Navigation).Include(d => d.ErrorIde14Navigation).Include(d => d.ErrorIde15Navigation).Include(d => d.ErrorIde1Navigation).Include(d => d.ErrorIde2Navigation).Include(d => d.ErrorIde3Navigation).Include(d => d.ErrorIde4Navigation).Include(d => d.ErrorIde5Navigation).Include(d => d.ErrorIde6Navigation).Include(d => d.ErrorIde7Navigation).Include(d => d.ErrorIde8Navigation).Include(d => d.ErrorIde9Navigation).OrderBy(e => e.StartTime).ThenBy(f => f.Shift).ToListAsync()));
        }

        public async Task<IActionResult> Viewer(int? Shift)
        {
            HttpContext.Session.SetString("Submenu", "3");
            HttpContext.Session.SetInt32("LZGroup", 0);
            HttpContext.Session.SetInt32("LineIDGroup", 1);
            HttpContext.Session.SetInt32("StationGroup", 0);
            HttpContext.Session.SetInt32("ShiftGroup", 1);
            HttpContext.Session.SetInt32("DayGroup", 3);
            HttpContext.Session.SetInt32("HeaderGroup", 1);
            HttpContext.Session.SetInt32("AddGroup", 0);
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
            if (Shift.HasValue)
            {
                HttpContext.Session.SetInt32("Shift", Shift.Value);
            }
            else
            {
                if (!HttpContext.Session.GetInt32("Shift").HasValue)
                {
                    Shift = 4;
                }
                else
                {
                    Shift = HttpContext.Session.GetInt32("Shift").Value;
                }
            }
            int shift = 4;
            string shifttxt = "All day";
            switch (Shift.Value)
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
                switch (Shift.Value)
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
            ViewData["LineID"] = LineID;
            ViewData["StartTime"] = StartTimeFrom.ToShortDateString() + "-" + StartTimeTo.ToShortDateString();
            ViewData["Shift"] = shifttxt;
            return await Task.Run(async () => View(await _context.Dtreport.Where(a => (a.LineId == LineID | LineID == "ALL Line") & (a.StartTime >= StartTimeFrom & a.StartTime <= StartTimeTo) & (a.Shift == Shift | (Shift == 4))).Include(d => d.ErrorIda10Navigation).Include(d => d.ErrorIda11Navigation).Include(d => d.ErrorIda12Navigation).Include(d => d.ErrorIda13Navigation).Include(d => d.ErrorIda14Navigation).Include(d => d.ErrorIda15Navigation).Include(d => d.ErrorIda1Navigation).Include(d => d.ErrorIda2Navigation).Include(d => d.ErrorIda3Navigation).Include(d => d.ErrorIda4Navigation).Include(d => d.ErrorIda5Navigation).Include(d => d.ErrorIda6Navigation).Include(d => d.ErrorIda7Navigation).Include(d => d.ErrorIda8Navigation).Include(d => d.ErrorIda9Navigation).Include(d => d.ErrorIdb10Navigation).Include(d => d.ErrorIdb11Navigation).Include(d => d.ErrorIdb12Navigation).Include(d => d.ErrorIdb13Navigation).Include(d => d.ErrorIdb14Navigation).Include(d => d.ErrorIdb15Navigation).Include(d => d.ErrorIdb1Navigation).Include(d => d.ErrorIdb2Navigation).Include(d => d.ErrorIdb3Navigation).Include(d => d.ErrorIdb4Navigation).Include(d => d.ErrorIdb5Navigation).Include(d => d.ErrorIdb6Navigation).Include(d => d.ErrorIdb7Navigation).Include(d => d.ErrorIdb8Navigation).Include(d => d.ErrorIdb9Navigation).Include(d => d.ErrorIdc10Navigation).Include(d => d.ErrorIdc11Navigation).Include(d => d.ErrorIdc12Navigation).Include(d => d.ErrorIdc13Navigation).Include(d => d.ErrorIdc14Navigation).Include(d => d.ErrorIdc15Navigation).Include(d => d.ErrorIdc1Navigation).Include(d => d.ErrorIdc2Navigation).Include(d => d.ErrorIdc3Navigation).Include(d => d.ErrorIdc4Navigation).Include(d => d.ErrorIdc5Navigation).Include(d => d.ErrorIdc6Navigation).Include(d => d.ErrorIdc7Navigation).Include(d => d.ErrorIdc8Navigation).Include(d => d.ErrorIdc9Navigation).Include(d => d.ErrorIdd10Navigation).Include(d => d.ErrorIdd11Navigation).Include(d => d.ErrorIdd12Navigation).Include(d => d.ErrorIdd13Navigation).Include(d => d.ErrorIdd14Navigation).Include(d => d.ErrorIdd15Navigation).Include(d => d.ErrorIdd1Navigation).Include(d => d.ErrorIdd2Navigation).Include(d => d.ErrorIdd3Navigation).Include(d => d.ErrorIdd4Navigation).Include(d => d.ErrorIdd5Navigation).Include(d => d.ErrorIdd6Navigation).Include(d => d.ErrorIdd7Navigation).Include(d => d.ErrorIdd8Navigation).Include(d => d.ErrorIdd9Navigation).Include(d => d.ErrorIde10Navigation).Include(d => d.ErrorIde11Navigation).Include(d => d.ErrorIde12Navigation).Include(d => d.ErrorIde13Navigation).Include(d => d.ErrorIde14Navigation).Include(d => d.ErrorIde15Navigation).Include(d => d.ErrorIde1Navigation).Include(d => d.ErrorIde2Navigation).Include(d => d.ErrorIde3Navigation).Include(d => d.ErrorIde4Navigation).Include(d => d.ErrorIde5Navigation).Include(d => d.ErrorIde6Navigation).Include(d => d.ErrorIde7Navigation).Include(d => d.ErrorIde8Navigation).Include(d => d.ErrorIde9Navigation).OrderBy(e => e.StartTime).ThenBy(f => f.Shift).ToListAsync()));
        }
        // GET: Dtreports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Dtreport == null)
            {
                return NotFound();
            }

            var dtreport = await _context.Dtreport
                .Include(d => d.ErrorIda10Navigation)
                .Include(d => d.ErrorIda11Navigation)
                .Include(d => d.ErrorIda12Navigation)
                .Include(d => d.ErrorIda13Navigation)
                .Include(d => d.ErrorIda14Navigation)
                .Include(d => d.ErrorIda15Navigation)
                .Include(d => d.ErrorIda1Navigation)
                .Include(d => d.ErrorIda2Navigation)
                .Include(d => d.ErrorIda3Navigation)
                .Include(d => d.ErrorIda4Navigation)
                .Include(d => d.ErrorIda5Navigation)
                .Include(d => d.ErrorIda6Navigation)
                .Include(d => d.ErrorIda7Navigation)
                .Include(d => d.ErrorIda8Navigation)
                .Include(d => d.ErrorIda9Navigation)
                .Include(d => d.ErrorIdb10Navigation)
                .Include(d => d.ErrorIdb11Navigation)
                .Include(d => d.ErrorIdb12Navigation)
                .Include(d => d.ErrorIdb13Navigation)
                .Include(d => d.ErrorIdb14Navigation)
                .Include(d => d.ErrorIdb15Navigation)
                .Include(d => d.ErrorIdb1Navigation)
                .Include(d => d.ErrorIdb2Navigation)
                .Include(d => d.ErrorIdb3Navigation)
                .Include(d => d.ErrorIdb4Navigation)
                .Include(d => d.ErrorIdb5Navigation)
                .Include(d => d.ErrorIdb6Navigation)
                .Include(d => d.ErrorIdb7Navigation)
                .Include(d => d.ErrorIdb8Navigation)
                .Include(d => d.ErrorIdb9Navigation)
                .Include(d => d.ErrorIdc10Navigation)
                .Include(d => d.ErrorIdc11Navigation)
                .Include(d => d.ErrorIdc12Navigation)
                .Include(d => d.ErrorIdc13Navigation)
                .Include(d => d.ErrorIdc14Navigation)
                .Include(d => d.ErrorIdc15Navigation)
                .Include(d => d.ErrorIdc1Navigation)
                .Include(d => d.ErrorIdc2Navigation)
                .Include(d => d.ErrorIdc3Navigation)
                .Include(d => d.ErrorIdc4Navigation)
                .Include(d => d.ErrorIdc5Navigation)
                .Include(d => d.ErrorIdc6Navigation)
                .Include(d => d.ErrorIdc7Navigation)
                .Include(d => d.ErrorIdc8Navigation)
                .Include(d => d.ErrorIdc9Navigation)
                .Include(d => d.ErrorIdd10Navigation)
                .Include(d => d.ErrorIdd11Navigation)
                .Include(d => d.ErrorIdd12Navigation)
                .Include(d => d.ErrorIdd13Navigation)
                .Include(d => d.ErrorIdd14Navigation)
                .Include(d => d.ErrorIdd15Navigation)
                .Include(d => d.ErrorIdd1Navigation)
                .Include(d => d.ErrorIdd2Navigation)
                .Include(d => d.ErrorIdd3Navigation)
                .Include(d => d.ErrorIdd4Navigation)
                .Include(d => d.ErrorIdd5Navigation)
                .Include(d => d.ErrorIdd6Navigation)
                .Include(d => d.ErrorIdd7Navigation)
                .Include(d => d.ErrorIdd8Navigation)
                .Include(d => d.ErrorIdd9Navigation)
                .Include(d => d.ErrorIde10Navigation)
                .Include(d => d.ErrorIde11Navigation)
                .Include(d => d.ErrorIde12Navigation)
                .Include(d => d.ErrorIde13Navigation)
                .Include(d => d.ErrorIde14Navigation)
                .Include(d => d.ErrorIde15Navigation)
                .Include(d => d.ErrorIde1Navigation)
                .Include(d => d.ErrorIde2Navigation)
                .Include(d => d.ErrorIde3Navigation)
                .Include(d => d.ErrorIde4Navigation)
                .Include(d => d.ErrorIde5Navigation)
                .Include(d => d.ErrorIde6Navigation)
                .Include(d => d.ErrorIde7Navigation)
                .Include(d => d.ErrorIde8Navigation)
                .Include(d => d.ErrorIde9Navigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dtreport == null)
            {
                return NotFound();
            }
            string LineID = dtreport.LineId;
            dtreport.Date = dtreport.StartTime.Date.ToString("yyyy-MM-dd");
            dtreport.Time = dtreport.StartTime.Date.AddHours(dtreport.Shift == 3 ? 22 : dtreport.Shift == 2 ? 14 : 6).AddMinutes(dtreport.Shift == 3 ? 40 : dtreport.Shift == 2 ? 20 : 0).TimeOfDay.ToString();
            #region Search Values
            ViewData["ErrorIda1"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIda1);
            ViewData["ErrorIda2"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIda2);
            ViewData["ErrorIda3"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIda3);
            ViewData["ErrorIda4"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIda4);
            ViewData["ErrorIda5"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIda5);
            ViewData["ErrorIda6"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIda6);
            ViewData["ErrorIda7"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIda7);
            ViewData["ErrorIda8"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIda8);
            ViewData["ErrorIda9"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIda9);
            ViewData["ErrorIda10"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIda10);
            ViewData["ErrorIda11"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIda11);
            ViewData["ErrorIda12"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIda12);
            ViewData["ErrorIda13"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIda13);
            ViewData["ErrorIda14"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIda14);
            ViewData["ErrorIda15"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIda15);
            ViewData["ErrorIdb1"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdb1);
            ViewData["ErrorIdb2"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdb2);
            ViewData["ErrorIdb3"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdb3);
            ViewData["ErrorIdb4"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdb4);
            ViewData["ErrorIdb5"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdb5);
            ViewData["ErrorIdb6"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdb6);
            ViewData["ErrorIdb7"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdb7);
            ViewData["ErrorIdb8"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdb8);
            ViewData["ErrorIdb9"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdb9);
            ViewData["ErrorIdb10"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdb10);
            ViewData["ErrorIdb11"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdb11);
            ViewData["ErrorIdb12"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdb12);
            ViewData["ErrorIdb13"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdb13);
            ViewData["ErrorIdb14"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdb14);
            ViewData["ErrorIdb15"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdb15);
            ViewData["ErrorIdc1"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdc1);
            ViewData["ErrorIdc2"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdc2);
            ViewData["ErrorIdc3"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdc3);
            ViewData["ErrorIdc4"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdc4);
            ViewData["ErrorIdc5"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdc5);
            ViewData["ErrorIdc6"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdc6);
            ViewData["ErrorIdc7"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdc7);
            ViewData["ErrorIdc8"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdc8);
            ViewData["ErrorIdc9"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdc9);
            ViewData["ErrorIdc10"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdc10);
            ViewData["ErrorIdc11"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdc11);
            ViewData["ErrorIdc12"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdc12);
            ViewData["ErrorIdc13"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdc13);
            ViewData["ErrorIdc14"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdc14);
            ViewData["ErrorIdc15"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdc15);
            ViewData["ErrorIdd1"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdd1);
            ViewData["ErrorIdd2"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdd2);
            ViewData["ErrorIdd3"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdd3);
            ViewData["ErrorIdd4"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdd4);
            ViewData["ErrorIdd5"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdd5);
            ViewData["ErrorIdd6"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdd6);
            ViewData["ErrorIdd7"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdd7);
            ViewData["ErrorIdd8"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdd8);
            ViewData["ErrorIdd9"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdd9);
            ViewData["ErrorIdd10"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdd10);
            ViewData["ErrorIdd11"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdd11);
            ViewData["ErrorIdd12"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdd12);
            ViewData["ErrorIdd13"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdd13);
            ViewData["ErrorIdd14"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdd14);
            ViewData["ErrorIdd15"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdd15);
            ViewData["ErrorIde1"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIde1);
            ViewData["ErrorIde2"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIde2);
            ViewData["ErrorIde3"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIde3);
            ViewData["ErrorIde4"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIde4);
            ViewData["ErrorIde5"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIde5);
            ViewData["ErrorIde6"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIde6);
            ViewData["ErrorIde7"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIde7);
            ViewData["ErrorIde8"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIde8);
            ViewData["ErrorIde9"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIde9);
            ViewData["ErrorIde10"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIde10);
            ViewData["ErrorIde11"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIde11);
            ViewData["ErrorIde12"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIde12);
            ViewData["ErrorIde13"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIde13);
            ViewData["ErrorIde14"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIde14);
            ViewData["ErrorIde15"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIde15);
            #endregion
            ViewData["TypeNumbers"] = 0 < dtreport.TypeNumbers ? dtreport.TypeNumbers : null;
            return PartialView("_DetailsModalPartion", dtreport);
        }


        //public IActionResult CreateModal(int? Shift)
        //{
        //    #region GetServerDatas v1.3
        //    string LineID = "GBM Line1";
        //    if (HttpContext.Session.GetString("LineID") == null)
        //    {
        //        LineID = "GBM Line1";// VALEO RSW2";
        //    }
        //    else
        //    {
        //        LineID = HttpContext.Session.GetString("LineID");
        //    }
        //    int Year = DateTime.Now.Year;
        //    if (!HttpContext.Session.GetInt32("Year").HasValue)
        //    {
        //        Year = DateTime.Now.Year;
        //    }
        //    else
        //    {
        //        Year = HttpContext.Session.GetInt32("Year").Value;
        //    }
        //    int year = (int)Year;
        //    int Month = DateTime.Now.Month;
        //    if (!HttpContext.Session.GetInt32("Month").HasValue)
        //    {
        //        Month = DateTime.Now.Month;
        //    }
        //    else
        //    {
        //        Month = HttpContext.Session.GetInt32("Month").Value;
        //    }
        //    int month = (int)Month;
        //    int Day = DateTime.Now.Day;
        //    if (!HttpContext.Session.GetInt32("Day").HasValue)
        //    {
        //        Day = DateTime.Now.Day;
        //    }
        //    else
        //    {
        //        Day = HttpContext.Session.GetInt32("Day").Value;
        //    }
        //    int day = (int)Day;
        //    int YearFrom = DateTime.Now.Year;
        //    if (!HttpContext.Session.GetInt32("YearFrom").HasValue)
        //    {
        //        YearFrom = DateTime.Now.Year;
        //    }
        //    else
        //    {
        //        YearFrom = HttpContext.Session.GetInt32("YearFrom").Value;
        //    }
        //    int yearfrom = (int)YearFrom;
        //    int MonthFrom = DateTime.Now.Month;
        //    if (!HttpContext.Session.GetInt32("MonthFrom").HasValue)
        //    {
        //        MonthFrom = DateTime.Now.Month;
        //    }
        //    else
        //    {
        //        MonthFrom = HttpContext.Session.GetInt32("MonthFrom").Value;
        //    }
        //    int monthfrom = (int)MonthFrom;
        //    int DayFrom = DateTime.Now.Day;
        //    if (!HttpContext.Session.GetInt32("DayFrom").HasValue)
        //    {
        //        DayFrom = DateTime.Now.Day;
        //    }
        //    else
        //    {
        //        DayFrom = HttpContext.Session.GetInt32("DayFrom").Value;
        //    }
        //    int dayfrom = (int)DayFrom;
        //    int YearTo = DateTime.Now.Year;
        //    if (!HttpContext.Session.GetInt32("YearTo").HasValue)
        //    {
        //        YearTo = DateTime.Now.Year;
        //    }
        //    else
        //    {
        //        YearTo = HttpContext.Session.GetInt32("YearTo").Value;
        //    }
        //    int yearto = (int)YearTo;
        //    int MonthTo = DateTime.Now.Month;
        //    if (!HttpContext.Session.GetInt32("MonthTo").HasValue)
        //    {
        //        MonthTo = DateTime.Now.Month;
        //    }
        //    else
        //    {
        //        MonthTo = HttpContext.Session.GetInt32("MonthTo").Value;
        //    }
        //    int monthto = (int)MonthTo;
        //    int DayTo = DateTime.Now.Day;
        //    if (!HttpContext.Session.GetInt32("DayTo").HasValue)
        //    {
        //        DayTo = DateTime.Now.Day;
        //    }
        //    else
        //    {
        //        DayTo = HttpContext.Session.GetInt32("DayTo").Value;
        //    }
        //    int dayto = (int)DayTo;
        //    //int Shift = 4;
        //    if (!Shift.HasValue)
        //    {
        //        if (!HttpContext.Session.GetInt32("Shift").HasValue)
        //        {
        //            Shift = 4;
        //        }
        //        else
        //        {
        //            Shift = HttpContext.Session.GetInt32("Shift").Value;
        //        }
        //    }
        //    else
        //    {
        //        HttpContext.Session.SetInt32("Shift",Shift.Value);
        //    }
        //    int shift = 4;
        //    string shifttxt = "All day";
        //    switch (Shift)
        //    {
        //        case 4: shifttxt = "All day"; shift = 4; break;
        //        case 1: shifttxt = "Morning"; shift = 1; break;
        //        case 2: shifttxt = "Afternoon"; shift = 2; break;
        //        case 3: shifttxt = "Overnight"; shift = 3; break;
        //        case 5: shifttxt = "Night+Morning"; shift = 5; break; // 1 + 3
        //        case 6: shifttxt = "Night+Afternoon"; shift = 6; break; // 2 + 3
        //        case 7: shifttxt = "Morning+Afternoon"; shift = 7; break; // 1 + 2
        //    }
        //    var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
        //    if (@requestCulture.RequestCulture.UICulture.Name == "hu-HU")
        //    {
        //        shifttxt = "Egész nap";
        //        switch (Shift)
        //        {
        //            case 4: shifttxt = "Egész nap"; shift = 4; break;
        //            case 1: shifttxt = "Délelőtt"; shift = 1; break;
        //            case 2: shifttxt = "Délután"; shift = 2; break;
        //            case 3: shifttxt = "Éjszaka"; shift = 3; break;
        //            case 5: shifttxt = "Éjszaka+Nappal"; shift = 5; break; // 1 + 3
        //            case 6: shifttxt = "Éjszaka+Délután"; shift = 6; break; // 2 + 3
        //            case 7: shifttxt = "Nappal+Délután"; shift = 7; break; // 1 + 2
        //        }
        //    }
        //    string Language = @requestCulture.RequestCulture.UICulture.Name;

        //    int Company = 1;
        //    if (!HttpContext.Session.GetInt32("Company").HasValue)
        //    {
        //        Company = 1;
        //    }
        //    else
        //    {
        //        Company = HttpContext.Session.GetInt32("Company").Value;
        //    }
        //    int company = (int)Company;

        //    int DolgozoId = 0;
        //    if (!HttpContext.Session.GetInt32("DolgozoId").HasValue)
        //    {
        //        DolgozoId = 0;
        //    }
        //    else
        //    {
        //        DolgozoId = HttpContext.Session.GetInt32("DolgozoId").Value;
        //    }
        //    int dolgozoid = (int)DolgozoId;

        //    if (HttpContext.Session.GetString("LineID") == null |
        //        HttpContext.Session.GetInt32("Year") == null |
        //        HttpContext.Session.GetInt32("Month") == null |
        //        HttpContext.Session.GetInt32("Day") == null |
        //        HttpContext.Session.GetInt32("Shift") == null |
        //        HttpContext.Session.GetInt32("Company") == null |
        //        HttpContext.Session.GetInt32("DolgozoId") == null)
        //    {
        //        Headers header = new()
        //        {
        //            MyFields = false,
        //            SearchString = ""
        //        };
        //        SessionHelper.SetObjectAsJson(HttpContext.Session, "header", header);
        //    }
        //    HttpContext.Session.SetString("LineID", LineID);

        //    HttpContext.Session.SetInt32("Year", year);
        //    HttpContext.Session.SetInt32("Month", month);
        //    HttpContext.Session.SetInt32("Day", day);

        //    HttpContext.Session.SetInt32("YearFrom", yearfrom);
        //    HttpContext.Session.SetInt32("MonthFrom", monthfrom);
        //    HttpContext.Session.SetInt32("DayFrom", dayfrom);

        //    HttpContext.Session.SetInt32("YearTo", yearto);
        //    HttpContext.Session.SetInt32("MonthTo", monthto);
        //    HttpContext.Session.SetInt32("DayTo", dayto);

        //    HttpContext.Session.SetInt32("Shift", shift);
        //    HttpContext.Session.SetString("ShiftTxt", shifttxt);

        //    HttpContext.Session.SetInt32("Company", company);

        //    HttpContext.Session.SetInt32("DolgozoId", dolgozoid);
        //    #endregion
        //    return RedirectToAction(nameof(Index),Shift.Value);
        //}

        //GET: Dtreports/Create
        public async Task<IActionResult> CreateAsync()
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
            if (Shift == 4)
            {
                return await Task.Run(() => PartialView("Error")); //PartialView("Select"));
            }
            else
            {
                DateTime StartTime = new DateTime(Year, Month, Day, 6, 0, 0);
                DateTime ShiftStartTime = new DateTime(Year, Month, Day, 6, 0, 0);
                if (shift == 2)
                {
                    ShiftStartTime = new DateTime(Year, Month, Day, 14, 20, 0);
                }
                if (shift == 3)
                {
                    ShiftStartTime = new DateTime(Year, Month, Day, 22, 40, 0);
                }
                Dtreport dtreport = await _context.Dtreport.Where(a => a.LineId == LineID & a.StartTime == StartTime & (a.Shift == Shift | (Shift == 4 & a.Shift == 1))).FirstOrDefaultAsync();
                var alertsDBContext = await _context.ShiftsTable.Where(a => a.LineId == LineID & a.Starttime == StartTime & (shift < 4 & a.Muszak == shift)).ToListAsync();
                int cnt = 0;
                bool newflag = false;
                var alertstypesDBContext = await _context.ViewShiftTypes.Where(a => a.LineId == LineID & a.StartTime == StartTime & (shift < 4 & a.Muszak == shift)).OrderBy(b => b.ChangeTime).ToListAsync();
                if (alertstypesDBContext.Count > 0)
                {
                    cnt = alertstypesDBContext.Count;
                }
                if (alertsDBContext.Count > 0)
                {
                    if (dtreport == null)
                    {
                        newflag = true;
                        dtreport = new Dtreport()
                        {
                            Id = 0,
                            LineId = LineID,
                            UserCode = "00000",
                            UserName = "System",
                            Shift = shift>3 ? 1 : shift
                        };
                    }
                    if (!dtreport.SentEmail)
                    {
                        string username = HttpContext.User.Identity.Name.ToLower();
                        var name = User.Identity.Name.Split('.');
                        if (name.Count() > 1)
                        {
                            username = @name[0].Substring(0, 1).ToUpper() + @name[0].Substring(1) + " " + @name[1].Substring(0, 1).ToUpper() + @name[1].Substring(1);
                            if (@requestCulture.RequestCulture.UICulture.Name == "hu-HU")
                            {
                                username = @name[1].Substring(0, 1).ToUpper() + @name[1].Substring(1) + " " + @name[0].Substring(0, 1).ToUpper() + @name[0].Substring(1);
                            }
                        }
                        dtreport.UserCode = "00000";
                        dtreport.UserName = username;
                        dtreport.TypeNumbers = cnt;
                        dtreport.TimeStamp = DateTime.Now;
                        dtreport.StartTime = StartTime;
                        dtreport.Shift = shift > 3 ? 1 : shift;
                        dtreport.RequestfromSql = alertsDBContext[0].Elvart.HasValue ? alertsDBContext[0].Elvart.Value : null;
                        dtreport.CurrentfromSql = alertsDBContext[0].Gyartott.HasValue & !alertsDBContext[0].PGyartott.HasValue ? alertsDBContext[0].Gyartott.Value : alertsDBContext[0].Gyartott.HasValue & alertsDBContext[0].PGyartott.HasValue ? alertsDBContext[0].Gyartott.Value + alertsDBContext[0].PGyartott.Value : null;
                        dtreport.GoodfromSql = alertsDBContext[0].Gyartott.HasValue ? alertsDBContext[0].Gyartott.Value : null;
                        dtreport.EolfromSql = alertsDBContext[0].PGyartott.HasValue ? alertsDBContext[0].PGyartott.Value : null;
                        dtreport.DownTimefromSql = alertsDBContext[0].Allasido.HasValue ? alertsDBContext[0].Allasido.Value : null;
                        dtreport.TypeNumberA = 0 < cnt ? alertstypesDBContext[0].TypeId : null;
                        dtreport.ChangeTimeA = 0 < cnt ? alertstypesDBContext[0].ChangeTime : null;
                        dtreport.TypeNameA = 0 < cnt ? alertstypesDBContext[0].TypeName : "";
                        dtreport.RequestfromSqla = 0 < cnt ? alertstypesDBContext[0].TargetParts : null;
                        dtreport.CurrentfromSqla = 0 < cnt ? alertstypesDBContext[0].CurrentParts : null;
                        dtreport.GoodfromSqla = 0 < cnt ? alertstypesDBContext[0].GoodParts : null;
                        dtreport.EolfromSqla = 0 < cnt ? alertstypesDBContext[0].BadParts : null;
                        dtreport.DownTimefromSqla = 0 < cnt ? alertstypesDBContext[0].DownTime : null;
                        dtreport.WtnrA = 0 < cnt ? alertstypesDBContext[0].ActiveWts : null;
                        dtreport.TypeNumberB = 1 < cnt ? alertstypesDBContext[1].TypeId : null;
                        dtreport.ChangeTimeB = 1 < cnt ? alertstypesDBContext[1].ChangeTime : null;
                        dtreport.TypeNameB = 1 < cnt ? alertstypesDBContext[1].TypeName : "";
                        dtreport.RequestfromSqlb = 1 < cnt ? alertstypesDBContext[1].TargetParts : null;
                        dtreport.CurrentfromSqlb = 1 < cnt ? alertstypesDBContext[1].CurrentParts : null;
                        dtreport.GoodfromSqlb = 1 < cnt ? alertstypesDBContext[1].GoodParts : null;
                        dtreport.EolfromSqlb = 1 < cnt ? alertstypesDBContext[1].BadParts : null;
                        dtreport.DownTimefromSqlb = 1 < cnt ? alertstypesDBContext[1].DownTime : null;
                        dtreport.WtnrB = 1 < cnt ? alertstypesDBContext[1].ActiveWts : null;
                        dtreport.TypeNumberC = 2 < cnt ? alertstypesDBContext[2].TypeId : null;
                        dtreport.ChangeTimeC = 2 < cnt ? alertstypesDBContext[2].ChangeTime : null;
                        dtreport.TypeNameC = 2 < cnt ? alertstypesDBContext[2].TypeName : "";
                        dtreport.RequestfromSqlc = 2 < cnt ? alertstypesDBContext[2].TargetParts : null;
                        dtreport.CurrentfromSqlc = 2 < cnt ? alertstypesDBContext[2].CurrentParts : null;
                        dtreport.GoodfromSqlc = 2 < cnt ? alertstypesDBContext[2].GoodParts : null;
                        dtreport.EolfromSqlc = 2 < cnt ? alertstypesDBContext[2].BadParts : null;
                        dtreport.DownTimefromSqlc = 2 < cnt ? alertstypesDBContext[2].DownTime : null;
                        dtreport.WtnrC = 2 < cnt ? alertstypesDBContext[2].ActiveWts : null;
                        dtreport.TypeNumberD = 3 < cnt ? alertstypesDBContext[3].TypeId : null;
                        dtreport.ChangeTimeD = 3 < cnt ? alertstypesDBContext[3].ChangeTime : null;
                        dtreport.TypeNameD = 3 < cnt ? alertstypesDBContext[3].TypeName : "";
                        dtreport.RequestfromSqld = 3 < cnt ? alertstypesDBContext[3].TargetParts : null;
                        dtreport.CurrentfromSqld = 3 < cnt ? alertstypesDBContext[3].CurrentParts : null;
                        dtreport.GoodfromSqld = 3 < cnt ? alertstypesDBContext[3].GoodParts : null;
                        dtreport.EolfromSqld = 3 < cnt ? alertstypesDBContext[3].BadParts : null;
                        dtreport.DownTimefromSqld = 3 < cnt ? alertstypesDBContext[3].DownTime : null;
                        dtreport.WtnrD = 3 < cnt ? alertstypesDBContext[3].ActiveWts : null;
                        dtreport.TypeNumberE = 4 < cnt ? alertstypesDBContext[4].TypeId : null;
                        dtreport.ChangeTimeE = 4 < cnt ? alertstypesDBContext[4].ChangeTime : null;
                        dtreport.TypeNameE = 4 < cnt ? alertstypesDBContext[4].TypeName : "";
                        dtreport.RequestfromSqle = 4 < cnt ? alertstypesDBContext[4].TargetParts : null;
                        dtreport.CurrentfromSqle = 4 < cnt ? alertstypesDBContext[4].CurrentParts : null;
                        dtreport.GoodfromSqle = 4 < cnt ? alertstypesDBContext[4].GoodParts : null;
                        dtreport.EolfromSqle = 4 < cnt ? alertstypesDBContext[4].BadParts : null;
                        dtreport.DownTimefromSqle = 4 < cnt ? alertstypesDBContext[4].DownTime : null;
                        dtreport.WtnrE = 4 < cnt ? alertstypesDBContext[4].ActiveWts : null;
                    }
                    dtreport.Date = ShiftStartTime.Date.ToString("yyyy-MM-dd");
                    dtreport.Time = ShiftStartTime.TimeOfDay.ToString();
                    #region Search Values
                    ViewData["ErrorIda1"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIda2"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIda3"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIda4"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIda5"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIda6"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIda7"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIda8"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIda9"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIda10"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIda11"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIda12"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIda13"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIda14"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIda15"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIdb1"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIdb2"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIdb3"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIdb4"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIdb5"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIdb6"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIdb7"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIdb8"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIdb9"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIdb10"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIdb11"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIdb12"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIdb13"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIdb14"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIdb15"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIdc1"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIdc2"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIdc3"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIdc4"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIdc5"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIdc6"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIdc7"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIdc8"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIdc9"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIdc10"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIdc11"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIdc12"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIdc13"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIdc14"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIdc15"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIdd1"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIdd2"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIdd3"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIdd4"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIdd5"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIdd6"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIdd7"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIdd8"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIdd9"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIdd10"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIdd11"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIdd12"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIdd13"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIdd14"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIdd15"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIde1"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIde2"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIde3"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIde4"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIde5"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIde6"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIde7"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIde8"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIde9"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIde10"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIde11"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIde12"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIde13"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIde14"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["ErrorIde15"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search");
                    ViewData["TypeNumbers"] = 0 < dtreport.TypeNumbers ? dtreport.TypeNumbers : null;
                    #endregion
                    if (newflag)
                    {
                        return PartialView("_CreateModalPartion", dtreport);
                    }
                    else
                    {
                        ViewData["SentEmail"] = dtreport.SentEmail;
                        return PartialView("_EditModalPartion", dtreport);
                    }
                }
                else
                {
                    return PartialView("Error");
                }
            }
        }

        // POST: Dtreports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TimeStamp,UserCode,UserName,StartTime,Shift,LineId,TypeNumbers,RequestfromSql,CurrentfromSql,GoodfromSql,EolfromSql,DownTimefromSql,Wtnr,Message,TypeNumberA,ChangeTimeA,TypeNameA,RequestfromSqla,CurrentfromSqla,CurrentKorrA,CurrentMessageA,GoodfromSqla,EolfromSqla,EolkorrA,DownTimefromSqla,WtnrA,MessageA,TypeNumberB,ChangeTimeB,TypeNameB,RequestfromSqlb,CurrentfromSqlb,CurrentKorrB,CurrentMessageB,GoodfromSqlb,EolfromSqlb,EolkorrB,DownTimefromSqlb,WtnrB,MessageB,TypeNumberC,ChangeTimeC,TypeNameC,RequestfromSqlc,CurrentfromSqlc,CurrentKorrC,CurrentMessageC,GoodfromSqlc,EolfromSqlc,EolkorrC,DownTimefromSqlc,WtnrC,MessageC,TypeNumberD,ChangeTimeD,TypeNameD,RequestfromSqld,CurrentfromSqld,CurrentKorrD,CurrentMessageD,GoodfromSqld,EolfromSqld,EolkorrD,DownTimefromSqld,WtnrD,MessageD,TypeNumberE,ChangeTimeE,TypeNameE,RequestfromSqle,CurrentfromSqle,CurrentKorrE,CurrentMessageE,GoodfromSqle,EolfromSqle,EolkorrE,DownTimefromSqle,WtnrE,MessageE,SentEmail,Date,Time,ErrorIda1,ErrorIda2,ErrorIda3,ErrorIda4,ErrorIda5,ErrorIda6,ErrorIda7,ErrorIda8,ErrorIda9,ErrorIda10,ErrorIda11,ErrorIda12,ErrorIda13,ErrorIda14,ErrorIda15,ErrorNra1,ErrorNra2,ErrorNra3,ErrorNra4,ErrorNra5,ErrorNra6,ErrorNra7,ErrorNra8,ErrorNra9,ErrorNra10,ErrorNra11,ErrorNra12,ErrorNra13,ErrorNra14,ErrorNra15,ErrorIdb1,ErrorIdb2,ErrorIdb3,ErrorIdb4,ErrorIdb5,ErrorIdb6,ErrorIdb7,ErrorIdb8,ErrorIdb9,ErrorIdb10,ErrorIdb11,ErrorIdb12,ErrorIdb13,ErrorIdb14,ErrorIdb15,ErrorNrb1,ErrorNrb2,ErrorNrb3,ErrorNrb4,ErrorNrb5,ErrorNrb6,ErrorNrb7,ErrorNrb8,ErrorNrb9,ErrorNrb10,ErrorNrb11,ErrorNrb12,ErrorNrb13,ErrorNrb14,ErrorNrb15,ErrorIdc1,ErrorIdc2,ErrorIdc3,ErrorIdc4,ErrorIdc5,ErrorIdc6,ErrorIdc7,ErrorIdc8,ErrorIdc9,ErrorIdc10,ErrorIdc11,ErrorIdc12,ErrorIdc13,ErrorIdc14,ErrorIdc15,ErrorNrc1,ErrorNrc2,ErrorNrc3,ErrorNrc4,ErrorNrc5,ErrorNrc6,ErrorNrc7,ErrorNrc8,ErrorNrc9,ErrorNrc10,ErrorNrc11,ErrorNrc12,ErrorNrc13,ErrorNrc14,ErrorNrc15,ErrorIdd1,ErrorIdd2,ErrorIdd3,ErrorIdd4,ErrorIdd5,ErrorIdd6,ErrorIdd7,ErrorIdd8,ErrorIdd9,ErrorIdd10,ErrorIdd11,ErrorIdd12,ErrorIdd13,ErrorIdd14,ErrorIdd15,ErrorNrd1,ErrorNrd2,ErrorNrd3,ErrorNrd4,ErrorNrd5,ErrorNrd6,ErrorNrd7,ErrorNrd8,ErrorNrd9,ErrorNrd10,ErrorNrd11,ErrorNrd12,ErrorNrd13,ErrorNrd14,ErrorNrd15,ErrorIde1,ErrorIde2,ErrorIde3,ErrorIde4,ErrorIde5,ErrorIde6,ErrorIde7,ErrorIde8,ErrorIde9,ErrorIde10,ErrorIde11,ErrorIde12,ErrorIde13,ErrorIde14,ErrorIde15,ErrorNre1,ErrorNre2,ErrorNre3,ErrorNre4,ErrorNre5,ErrorNre6,ErrorNre7,ErrorNre8,ErrorNre9,ErrorNre10,ErrorNre11,ErrorNre12,ErrorNre13,ErrorNre14,ErrorNre15")] Dtreport dtreport)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dtreport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            string LineID = dtreport.LineId;
            #region Search Values
            ViewData["ErrorIda1"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIda1);
            ViewData["ErrorIda2"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIda2);
            ViewData["ErrorIda3"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIda3);
            ViewData["ErrorIda4"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIda4);
            ViewData["ErrorIda5"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIda5);
            ViewData["ErrorIda6"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIda6);
            ViewData["ErrorIda7"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIda7);
            ViewData["ErrorIda8"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIda8);
            ViewData["ErrorIda9"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIda9);
            ViewData["ErrorIda10"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIda10);
            ViewData["ErrorIda11"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIda11);
            ViewData["ErrorIda12"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIda12);
            ViewData["ErrorIda13"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIda13);
            ViewData["ErrorIda14"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIda14);
            ViewData["ErrorIda15"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIda15);
            ViewData["ErrorIdb1"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdb1);
            ViewData["ErrorIdb2"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdb2);
            ViewData["ErrorIdb3"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdb3);
            ViewData["ErrorIdb4"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdb4);
            ViewData["ErrorIdb5"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdb5);
            ViewData["ErrorIdb6"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdb6);
            ViewData["ErrorIdb7"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdb7);
            ViewData["ErrorIdb8"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdb8);
            ViewData["ErrorIdb9"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdb9);
            ViewData["ErrorIdb10"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdb10);
            ViewData["ErrorIdb11"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdb11);
            ViewData["ErrorIdb12"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdb12);
            ViewData["ErrorIdb13"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdb13);
            ViewData["ErrorIdb14"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdb14);
            ViewData["ErrorIdb15"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdb15);
            ViewData["ErrorIdc1"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdc1);
            ViewData["ErrorIdc2"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdc2);
            ViewData["ErrorIdc3"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdc3);
            ViewData["ErrorIdc4"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdc4);
            ViewData["ErrorIdc5"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdc5);
            ViewData["ErrorIdc6"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdc6);
            ViewData["ErrorIdc7"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdc7);
            ViewData["ErrorIdc8"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdc8);
            ViewData["ErrorIdc9"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdc9);
            ViewData["ErrorIdc10"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdc10);
            ViewData["ErrorIdc11"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdc11);
            ViewData["ErrorIdc12"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdc12);
            ViewData["ErrorIdc13"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdc13);
            ViewData["ErrorIdc14"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdc14);
            ViewData["ErrorIdc15"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdc15);
            ViewData["ErrorIdd1"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdd1);
            ViewData["ErrorIdd2"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdd2);
            ViewData["ErrorIdd3"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdd3);
            ViewData["ErrorIdd4"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdd4);
            ViewData["ErrorIdd5"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdd5);
            ViewData["ErrorIdd6"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdd6);
            ViewData["ErrorIdd7"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdd7);
            ViewData["ErrorIdd8"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdd8);
            ViewData["ErrorIdd9"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdd9);
            ViewData["ErrorIdd10"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdd10);
            ViewData["ErrorIdd11"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdd11);
            ViewData["ErrorIdd12"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdd12);
            ViewData["ErrorIdd13"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdd13);
            ViewData["ErrorIdd14"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdd14);
            ViewData["ErrorIdd15"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdd15);
            ViewData["ErrorIde1"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIde1);
            ViewData["ErrorIde2"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIde2);
            ViewData["ErrorIde3"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIde3);
            ViewData["ErrorIde4"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIde4);
            ViewData["ErrorIde5"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIde5);
            ViewData["ErrorIde6"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIde6);
            ViewData["ErrorIde7"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIde7);
            ViewData["ErrorIde8"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIde8);
            ViewData["ErrorIde9"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIde9);
            ViewData["ErrorIde10"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIde10);
            ViewData["ErrorIde11"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIde11);
            ViewData["ErrorIde12"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIde12);
            ViewData["ErrorIde13"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIde13);
            ViewData["ErrorIde14"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIde14);
            ViewData["ErrorIde15"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIde15);
            #endregion
            return PartialView("_CreateModalPartion", dtreport);
        }

        // GET: Dtreports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            if (id == null || _context.Dtreport == null)
            {
                return NotFound();
            }

            var dtreport = await _context.Dtreport.FindAsync(id);
            if (dtreport == null)
            {
                return NotFound();
            }
            //DateTime StartTime = new DateTime(Year, Month, Day, 6, 0, 0);
            //DateTime ShiftStartTime = new DateTime(Year, Month, Day, 6, 0, 0);
            //if (dtreport.Shift == 2)
            //{
            //    ShiftStartTime = new DateTime(Year, Month, Day, 14, 20, 0);
            //}
            //if (dtreport.Shift == 3)
            //{
            //    ShiftStartTime = new DateTime(Year, Month, Day, 22, 40, 0);
            //}
            //Dtreport dtreport = await _context.Dtreport.Where(a => a.LineId == dtreport.LineId & a.StartTime == StartTime & (a.Shift == Shift | (Shift == 4 & a.Shift == 1))).FirstOrDefaultAsync();
            var alertsDBContext = await _context.ShiftsTable.Where(a => a.LineId == dtreport.LineId & a.Starttime == dtreport.StartTime & (dtreport.Shift < 4 & a.Muszak == dtreport.Shift)).ToListAsync();
            int cnt = 0;
            var alertstypesDBContext = await _context.ViewShiftTypes.Where(a => a.LineId == dtreport.LineId & a.StartTime == dtreport.StartTime & (dtreport.Shift < 4 & a.Muszak == dtreport.Shift)).OrderBy(b => b.ChangeTime).ToListAsync();
            if (alertstypesDBContext.Count > 0)
            {
                cnt = alertstypesDBContext.Count;
            }
            if (alertsDBContext.Count > 0)
            {
                if (!dtreport.SentEmail)
                {
                    string username = HttpContext.User.Identity.Name.ToLower();
                    var name = User.Identity.Name.Split('.');
                    if (name.Count() > 1)
                    {
                        username = @name[0].Substring(0, 1).ToUpper() + @name[0].Substring(1) + " " + @name[1].Substring(0, 1).ToUpper() + @name[1].Substring(1);
                        if (@requestCulture.RequestCulture.UICulture.Name == "hu-HU")
                        {
                            username = @name[1].Substring(0, 1).ToUpper() + @name[1].Substring(1) + " " + @name[0].Substring(0, 1).ToUpper() + @name[0].Substring(1);
                        }
                    }
                    dtreport.UserCode = "00000";
                    dtreport.UserName = username;
                    dtreport.TypeNumbers = cnt;
                    dtreport.TimeStamp = DateTime.Now;
                    //dtreport.StartTime = StartTime;
                    dtreport.RequestfromSql = alertsDBContext[0].Elvart.HasValue ? alertsDBContext[0].Elvart.Value : null;
                    dtreport.CurrentfromSql = alertsDBContext[0].Gyartott.HasValue & !alertsDBContext[0].PGyartott.HasValue ? alertsDBContext[0].Gyartott.Value : alertsDBContext[0].Gyartott.HasValue & alertsDBContext[0].PGyartott.HasValue ? alertsDBContext[0].Gyartott.Value + alertsDBContext[0].PGyartott.Value : null;
                    dtreport.GoodfromSql = alertsDBContext[0].Gyartott.HasValue ? alertsDBContext[0].Gyartott.Value : null;
                    dtreport.EolfromSql = alertsDBContext[0].PGyartott.HasValue ? alertsDBContext[0].PGyartott.Value : null;
                    dtreport.DownTimefromSql = alertsDBContext[0].Allasido.HasValue ? alertsDBContext[0].Allasido.Value : null;
                    dtreport.TypeNumberA = 0 < cnt ? alertstypesDBContext[0].TypeId : null;
                    dtreport.ChangeTimeA = 0 < cnt ? alertstypesDBContext[0].ChangeTime : null;
                    dtreport.TypeNameA = 0 < cnt ? alertstypesDBContext[0].TypeName : "";
                    dtreport.RequestfromSqla = 0 < cnt ? alertstypesDBContext[0].TargetParts : null;
                    dtreport.CurrentfromSqla = 0 < cnt ? alertstypesDBContext[0].CurrentParts : null;
                    dtreport.GoodfromSqla = 0 < cnt ? alertstypesDBContext[0].GoodParts : null;
                    dtreport.EolfromSqla = 0 < cnt ? alertstypesDBContext[0].BadParts : null;
                    dtreport.DownTimefromSqla = 0 < cnt ? alertstypesDBContext[0].DownTime : null;
                    dtreport.WtnrA = 0 < cnt ? alertstypesDBContext[0].ActiveWts : null;
                    dtreport.TypeNumberB = 1 < cnt ? alertstypesDBContext[1].TypeId : null;
                    dtreport.ChangeTimeB = 1 < cnt ? alertstypesDBContext[1].ChangeTime : null;
                    dtreport.TypeNameB = 1 < cnt ? alertstypesDBContext[1].TypeName : "";
                    dtreport.RequestfromSqlb = 1 < cnt ? alertstypesDBContext[1].TargetParts : null;
                    dtreport.CurrentfromSqlb = 1 < cnt ? alertstypesDBContext[1].CurrentParts : null;
                    dtreport.GoodfromSqlb = 1 < cnt ? alertstypesDBContext[1].GoodParts : null;
                    dtreport.EolfromSqlb = 1 < cnt ? alertstypesDBContext[1].BadParts : null;
                    dtreport.DownTimefromSqlb = 1 < cnt ? alertstypesDBContext[1].DownTime : null;
                    dtreport.WtnrB = 1 < cnt ? alertstypesDBContext[1].ActiveWts : null;
                    dtreport.TypeNumberC = 2 < cnt ? alertstypesDBContext[2].TypeId : null;
                    dtreport.ChangeTimeC = 2 < cnt ? alertstypesDBContext[2].ChangeTime : null;
                    dtreport.TypeNameC = 2 < cnt ? alertstypesDBContext[2].TypeName : "";
                    dtreport.RequestfromSqlc = 2 < cnt ? alertstypesDBContext[2].TargetParts : null;
                    dtreport.CurrentfromSqlc = 2 < cnt ? alertstypesDBContext[2].CurrentParts : null;
                    dtreport.GoodfromSqlc = 2 < cnt ? alertstypesDBContext[2].GoodParts : null;
                    dtreport.EolfromSqlc = 2 < cnt ? alertstypesDBContext[2].BadParts : null;
                    dtreport.DownTimefromSqlc = 2 < cnt ? alertstypesDBContext[2].DownTime : null;
                    dtreport.WtnrC = 2 < cnt ? alertstypesDBContext[2].ActiveWts : null;
                    dtreport.TypeNumberD = 3 < cnt ? alertstypesDBContext[3].TypeId : null;
                    dtreport.ChangeTimeD = 3 < cnt ? alertstypesDBContext[3].ChangeTime : null;
                    dtreport.TypeNameD = 3 < cnt ? alertstypesDBContext[3].TypeName : "";
                    dtreport.RequestfromSqld = 3 < cnt ? alertstypesDBContext[3].TargetParts : null;
                    dtreport.CurrentfromSqld = 3 < cnt ? alertstypesDBContext[3].CurrentParts : null;
                    dtreport.GoodfromSqld = 3 < cnt ? alertstypesDBContext[3].GoodParts : null;
                    dtreport.EolfromSqld = 3 < cnt ? alertstypesDBContext[3].BadParts : null;
                    dtreport.DownTimefromSqld = 3 < cnt ? alertstypesDBContext[3].DownTime : null;
                    dtreport.WtnrD = 3 < cnt ? alertstypesDBContext[3].ActiveWts : null;
                    dtreport.TypeNumberE = 4 < cnt ? alertstypesDBContext[4].TypeId : null;
                    dtreport.ChangeTimeE = 4 < cnt ? alertstypesDBContext[4].ChangeTime : null;
                    dtreport.TypeNameE = 4 < cnt ? alertstypesDBContext[4].TypeName : "";
                    dtreport.RequestfromSqle = 4 < cnt ? alertstypesDBContext[4].TargetParts : null;
                    dtreport.CurrentfromSqle = 4 < cnt ? alertstypesDBContext[4].CurrentParts : null;
                    dtreport.GoodfromSqle = 4 < cnt ? alertstypesDBContext[4].GoodParts : null;
                    dtreport.EolfromSqle = 4 < cnt ? alertstypesDBContext[4].BadParts : null;
                    dtreport.DownTimefromSqle = 4 < cnt ? alertstypesDBContext[4].DownTime : null;
                    dtreport.WtnrE = 4 < cnt ? alertstypesDBContext[4].ActiveWts : null;
                }
                dtreport.Date = dtreport.StartTime.Date.ToString("yyyy-MM-dd");
                dtreport.Time = dtreport.StartTime.TimeOfDay.ToString();
            }
            else
            {
                return PartialView("Error");
            }
            //string LineID = dtreport.LineId;
            dtreport.Date = dtreport.StartTime.Date.ToString("yyyy-MM-dd");
            dtreport.Time = dtreport.StartTime.Date.AddHours(dtreport.Shift == 3 ? 22 : dtreport.Shift == 2 ? 14 : 6).AddMinutes(dtreport.Shift == 3 ? 40 : dtreport.Shift == 2 ? 20 : 0).TimeOfDay.ToString();
            #region Search Values
            ViewData["ErrorIda1"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIda1);
            ViewData["ErrorIda2"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIda2);
            ViewData["ErrorIda3"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIda3);
            ViewData["ErrorIda4"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIda4);
            ViewData["ErrorIda5"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIda5);
            ViewData["ErrorIda6"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIda6);
            ViewData["ErrorIda7"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIda7);
            ViewData["ErrorIda8"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIda8);
            ViewData["ErrorIda9"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIda9);
            ViewData["ErrorIda10"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIda10);
            ViewData["ErrorIda11"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIda11);
            ViewData["ErrorIda12"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIda12);
            ViewData["ErrorIda13"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIda13);
            ViewData["ErrorIda14"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIda14);
            ViewData["ErrorIda15"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIda15);
            ViewData["ErrorIdb1"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdb1);
            ViewData["ErrorIdb2"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdb2);
            ViewData["ErrorIdb3"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdb3);
            ViewData["ErrorIdb4"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdb4);
            ViewData["ErrorIdb5"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdb5);
            ViewData["ErrorIdb6"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdb6);
            ViewData["ErrorIdb7"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdb7);
            ViewData["ErrorIdb8"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdb8);
            ViewData["ErrorIdb9"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdb9);
            ViewData["ErrorIdb10"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdb10);
            ViewData["ErrorIdb11"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdb11);
            ViewData["ErrorIdb12"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdb12);
            ViewData["ErrorIdb13"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdb13);
            ViewData["ErrorIdb14"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdb14);
            ViewData["ErrorIdb15"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdb15);
            ViewData["ErrorIdc1"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdc1);
            ViewData["ErrorIdc2"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdc2);
            ViewData["ErrorIdc3"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdc3);
            ViewData["ErrorIdc4"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdc4);
            ViewData["ErrorIdc5"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdc5);
            ViewData["ErrorIdc6"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdc6);
            ViewData["ErrorIdc7"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdc7);
            ViewData["ErrorIdc8"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdc8);
            ViewData["ErrorIdc9"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdc9);
            ViewData["ErrorIdc10"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdc10);
            ViewData["ErrorIdc11"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdc11);
            ViewData["ErrorIdc12"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdc12);
            ViewData["ErrorIdc13"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdc13);
            ViewData["ErrorIdc14"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdc14);
            ViewData["ErrorIdc15"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdc15);
            ViewData["ErrorIdd1"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdd1);
            ViewData["ErrorIdd2"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdd2);
            ViewData["ErrorIdd3"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdd3);
            ViewData["ErrorIdd4"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdd4);
            ViewData["ErrorIdd5"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdd5);
            ViewData["ErrorIdd6"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdd6);
            ViewData["ErrorIdd7"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdd7);
            ViewData["ErrorIdd8"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdd8);
            ViewData["ErrorIdd9"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdd9);
            ViewData["ErrorIdd10"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdd10);
            ViewData["ErrorIdd11"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdd11);
            ViewData["ErrorIdd12"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdd12);
            ViewData["ErrorIdd13"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdd13);
            ViewData["ErrorIdd14"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdd14);
            ViewData["ErrorIdd15"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdd15);
            ViewData["ErrorIde1"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIde1);
            ViewData["ErrorIde2"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIde2);
            ViewData["ErrorIde3"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIde3);
            ViewData["ErrorIde4"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIde4);
            ViewData["ErrorIde5"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIde5);
            ViewData["ErrorIde6"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIde6);
            ViewData["ErrorIde7"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIde7);
            ViewData["ErrorIde8"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIde8);
            ViewData["ErrorIde9"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIde9);
            ViewData["ErrorIde10"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIde10);
            ViewData["ErrorIde11"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIde11);
            ViewData["ErrorIde12"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIde12);
            ViewData["ErrorIde13"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIde13);
            ViewData["ErrorIde14"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIde14);
            ViewData["ErrorIde15"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIde15);
            #endregion
            ViewData["TypeNumbers"] = 0 < dtreport.TypeNumbers ? dtreport.TypeNumbers : null;
            ViewData["SentEmail"] = dtreport.SentEmail;
            return PartialView("_EditModalPartion", dtreport);
        }

        // POST: Dtreports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TimeStamp,UserCode,UserName,StartTime,Shift,LineId,TypeNumbers,RequestfromSql,CurrentfromSql,GoodfromSql,EolfromSql,DownTimefromSql,Wtnr,Message,TypeNumberA,ChangeTimeA,TypeNameA,RequestfromSqla,CurrentfromSqla,CurrentKorrA,CurrentMessageA,GoodfromSqla,EolfromSqla,EolkorrA,DownTimefromSqla,WtnrA,MessageA,TypeNumberB,ChangeTimeB,TypeNameB,RequestfromSqlb,CurrentfromSqlb,CurrentKorrB,CurrentMessageB,GoodfromSqlb,EolfromSqlb,EolkorrB,DownTimefromSqlb,WtnrB,MessageB,TypeNumberC,ChangeTimeC,TypeNameC,RequestfromSqlc,CurrentfromSqlc,CurrentKorrC,CurrentMessageC,GoodfromSqlc,EolfromSqlc,EolkorrC,DownTimefromSqlc,WtnrC,MessageC,TypeNumberD,ChangeTimeD,TypeNameD,RequestfromSqld,CurrentfromSqld,CurrentKorrD,CurrentMessageD,GoodfromSqld,EolfromSqld,EolkorrD,DownTimefromSqld,WtnrD,MessageD,TypeNumberE,ChangeTimeE,TypeNameE,RequestfromSqle,CurrentfromSqle,CurrentKorrE,CurrentMessageE,GoodfromSqle,EolfromSqle,EolkorrE,DownTimefromSqle,WtnrE,MessageE,SentEmail,Date,Time,ErrorIda1,ErrorIda2,ErrorIda3,ErrorIda4,ErrorIda5,ErrorIda6,ErrorIda7,ErrorIda8,ErrorIda9,ErrorIda10,ErrorIda11,ErrorIda12,ErrorIda13,ErrorIda14,ErrorIda15,ErrorNra1,ErrorNra2,ErrorNra3,ErrorNra4,ErrorNra5,ErrorNra6,ErrorNra7,ErrorNra8,ErrorNra9,ErrorNra10,ErrorNra11,ErrorNra12,ErrorNra13,ErrorNra14,ErrorNra15,ErrorIdb1,ErrorIdb2,ErrorIdb3,ErrorIdb4,ErrorIdb5,ErrorIdb6,ErrorIdb7,ErrorIdb8,ErrorIdb9,ErrorIdb10,ErrorIdb11,ErrorIdb12,ErrorIdb13,ErrorIdb14,ErrorIdb15,ErrorNrb1,ErrorNrb2,ErrorNrb3,ErrorNrb4,ErrorNrb5,ErrorNrb6,ErrorNrb7,ErrorNrb8,ErrorNrb9,ErrorNrb10,ErrorNrb11,ErrorNrb12,ErrorNrb13,ErrorNrb14,ErrorNrb15,ErrorIdc1,ErrorIdc2,ErrorIdc3,ErrorIdc4,ErrorIdc5,ErrorIdc6,ErrorIdc7,ErrorIdc8,ErrorIdc9,ErrorIdc10,ErrorIdc11,ErrorIdc12,ErrorIdc13,ErrorIdc14,ErrorIdc15,ErrorNrc1,ErrorNrc2,ErrorNrc3,ErrorNrc4,ErrorNrc5,ErrorNrc6,ErrorNrc7,ErrorNrc8,ErrorNrc9,ErrorNrc10,ErrorNrc11,ErrorNrc12,ErrorNrc13,ErrorNrc14,ErrorNrc15,ErrorIdd1,ErrorIdd2,ErrorIdd3,ErrorIdd4,ErrorIdd5,ErrorIdd6,ErrorIdd7,ErrorIdd8,ErrorIdd9,ErrorIdd10,ErrorIdd11,ErrorIdd12,ErrorIdd13,ErrorIdd14,ErrorIdd15,ErrorNrd1,ErrorNrd2,ErrorNrd3,ErrorNrd4,ErrorNrd5,ErrorNrd6,ErrorNrd7,ErrorNrd8,ErrorNrd9,ErrorNrd10,ErrorNrd11,ErrorNrd12,ErrorNrd13,ErrorNrd14,ErrorNrd15,ErrorIde1,ErrorIde2,ErrorIde3,ErrorIde4,ErrorIde5,ErrorIde6,ErrorIde7,ErrorIde8,ErrorIde9,ErrorIde10,ErrorIde11,ErrorIde12,ErrorIde13,ErrorIde14,ErrorIde15,ErrorNre1,ErrorNre2,ErrorNre3,ErrorNre4,ErrorNre5,ErrorNre6,ErrorNre7,ErrorNre8,ErrorNre9,ErrorNre10,ErrorNre11,ErrorNre12,ErrorNre13,ErrorNre14,ErrorNre15")] Dtreport dtreport)
        {
            if (id != dtreport.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dtreport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DtreportExists(dtreport.Id))
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
            string LineID = dtreport.LineId;
            #region Search Values
            ViewData["ErrorIda1"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIda1);
            ViewData["ErrorIda2"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIda2);
            ViewData["ErrorIda3"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIda3);
            ViewData["ErrorIda4"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIda4);
            ViewData["ErrorIda5"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIda5);
            ViewData["ErrorIda6"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIda6);
            ViewData["ErrorIda7"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIda7);
            ViewData["ErrorIda8"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIda8);
            ViewData["ErrorIda9"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIda9);
            ViewData["ErrorIda10"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIda10);
            ViewData["ErrorIda11"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIda11);
            ViewData["ErrorIda12"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIda12);
            ViewData["ErrorIda13"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIda13);
            ViewData["ErrorIda14"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIda14);
            ViewData["ErrorIda15"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIda15);
            ViewData["ErrorIdb1"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdb1);
            ViewData["ErrorIdb2"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdb2);
            ViewData["ErrorIdb3"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdb3);
            ViewData["ErrorIdb4"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdb4);
            ViewData["ErrorIdb5"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdb5);
            ViewData["ErrorIdb6"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdb6);
            ViewData["ErrorIdb7"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdb7);
            ViewData["ErrorIdb8"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdb8);
            ViewData["ErrorIdb9"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdb9);
            ViewData["ErrorIdb10"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdb10);
            ViewData["ErrorIdb11"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdb11);
            ViewData["ErrorIdb12"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdb12);
            ViewData["ErrorIdb13"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdb13);
            ViewData["ErrorIdb14"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdb14);
            ViewData["ErrorIdb15"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdb15);
            ViewData["ErrorIdc1"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdc1);
            ViewData["ErrorIdc2"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdc2);
            ViewData["ErrorIdc3"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdc3);
            ViewData["ErrorIdc4"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdc4);
            ViewData["ErrorIdc5"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdc5);
            ViewData["ErrorIdc6"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdc6);
            ViewData["ErrorIdc7"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdc7);
            ViewData["ErrorIdc8"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdc8);
            ViewData["ErrorIdc9"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdc9);
            ViewData["ErrorIdc10"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdc10);
            ViewData["ErrorIdc11"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdc11);
            ViewData["ErrorIdc12"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdc12);
            ViewData["ErrorIdc13"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdc13);
            ViewData["ErrorIdc14"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdc14);
            ViewData["ErrorIdc15"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdc15);
            ViewData["ErrorIdd1"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdd1);
            ViewData["ErrorIdd2"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdd2);
            ViewData["ErrorIdd3"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdd3);
            ViewData["ErrorIdd4"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdd4);
            ViewData["ErrorIdd5"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdd5);
            ViewData["ErrorIdd6"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdd6);
            ViewData["ErrorIdd7"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdd7);
            ViewData["ErrorIdd8"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdd8);
            ViewData["ErrorIdd9"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdd9);
            ViewData["ErrorIdd10"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdd10);
            ViewData["ErrorIdd11"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdd11);
            ViewData["ErrorIdd12"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdd12);
            ViewData["ErrorIdd13"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdd13);
            ViewData["ErrorIdd14"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdd14);
            ViewData["ErrorIdd15"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIdd15);
            ViewData["ErrorIde1"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIde1);
            ViewData["ErrorIde2"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIde2);
            ViewData["ErrorIde3"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIde3);
            ViewData["ErrorIde4"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIde4);
            ViewData["ErrorIde5"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIde5);
            ViewData["ErrorIde6"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIde6);
            ViewData["ErrorIde7"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIde7);
            ViewData["ErrorIde8"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIde8);
            ViewData["ErrorIde9"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIde9);
            ViewData["ErrorIde10"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIde10);
            ViewData["ErrorIde11"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIde11);
            ViewData["ErrorIde12"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIde12);
            ViewData["ErrorIde13"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIde13);
            ViewData["ErrorIde14"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIde14);
            ViewData["ErrorIde15"] = new SelectList(_context.IfsFailureCodeTable.Where(a => a.LineId == dtreport.LineId).OrderBy(b => b.FailureId), "Id", "Search", dtreport.ErrorIde15);
            #endregion
            ViewData["TypeNumbers"] = 0 < dtreport.TypeNumbers ? dtreport.TypeNumbers : null;
            return PartialView("_EditModalPartion", dtreport);
        }

        // GET: Dtreports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Dtreport == null)
            {
                return NotFound();
            }

            var dtreport = await _context.Dtreport
                .Include(d => d.ErrorIda10Navigation)
                .Include(d => d.ErrorIda11Navigation)
                .Include(d => d.ErrorIda12Navigation)
                .Include(d => d.ErrorIda13Navigation)
                .Include(d => d.ErrorIda14Navigation)
                .Include(d => d.ErrorIda15Navigation)
                .Include(d => d.ErrorIda1Navigation)
                .Include(d => d.ErrorIda2Navigation)
                .Include(d => d.ErrorIda3Navigation)
                .Include(d => d.ErrorIda4Navigation)
                .Include(d => d.ErrorIda5Navigation)
                .Include(d => d.ErrorIda6Navigation)
                .Include(d => d.ErrorIda7Navigation)
                .Include(d => d.ErrorIda8Navigation)
                .Include(d => d.ErrorIda9Navigation)
                .Include(d => d.ErrorIdb10Navigation)
                .Include(d => d.ErrorIdb11Navigation)
                .Include(d => d.ErrorIdb12Navigation)
                .Include(d => d.ErrorIdb13Navigation)
                .Include(d => d.ErrorIdb14Navigation)
                .Include(d => d.ErrorIdb15Navigation)
                .Include(d => d.ErrorIdb1Navigation)
                .Include(d => d.ErrorIdb2Navigation)
                .Include(d => d.ErrorIdb3Navigation)
                .Include(d => d.ErrorIdb4Navigation)
                .Include(d => d.ErrorIdb5Navigation)
                .Include(d => d.ErrorIdb6Navigation)
                .Include(d => d.ErrorIdb7Navigation)
                .Include(d => d.ErrorIdb8Navigation)
                .Include(d => d.ErrorIdb9Navigation)
                .Include(d => d.ErrorIdc10Navigation)
                .Include(d => d.ErrorIdc11Navigation)
                .Include(d => d.ErrorIdc12Navigation)
                .Include(d => d.ErrorIdc13Navigation)
                .Include(d => d.ErrorIdc14Navigation)
                .Include(d => d.ErrorIdc15Navigation)
                .Include(d => d.ErrorIdc1Navigation)
                .Include(d => d.ErrorIdc2Navigation)
                .Include(d => d.ErrorIdc3Navigation)
                .Include(d => d.ErrorIdc4Navigation)
                .Include(d => d.ErrorIdc5Navigation)
                .Include(d => d.ErrorIdc6Navigation)
                .Include(d => d.ErrorIdc7Navigation)
                .Include(d => d.ErrorIdc8Navigation)
                .Include(d => d.ErrorIdc9Navigation)
                .Include(d => d.ErrorIdd10Navigation)
                .Include(d => d.ErrorIdd11Navigation)
                .Include(d => d.ErrorIdd12Navigation)
                .Include(d => d.ErrorIdd13Navigation)
                .Include(d => d.ErrorIdd14Navigation)
                .Include(d => d.ErrorIdd15Navigation)
                .Include(d => d.ErrorIdd1Navigation)
                .Include(d => d.ErrorIdd2Navigation)
                .Include(d => d.ErrorIdd3Navigation)
                .Include(d => d.ErrorIdd4Navigation)
                .Include(d => d.ErrorIdd5Navigation)
                .Include(d => d.ErrorIdd6Navigation)
                .Include(d => d.ErrorIdd7Navigation)
                .Include(d => d.ErrorIdd8Navigation)
                .Include(d => d.ErrorIdd9Navigation)
                .Include(d => d.ErrorIde10Navigation)
                .Include(d => d.ErrorIde11Navigation)
                .Include(d => d.ErrorIde12Navigation)
                .Include(d => d.ErrorIde13Navigation)
                .Include(d => d.ErrorIde14Navigation)
                .Include(d => d.ErrorIde15Navigation)
                .Include(d => d.ErrorIde1Navigation)
                .Include(d => d.ErrorIde2Navigation)
                .Include(d => d.ErrorIde3Navigation)
                .Include(d => d.ErrorIde4Navigation)
                .Include(d => d.ErrorIde5Navigation)
                .Include(d => d.ErrorIde6Navigation)
                .Include(d => d.ErrorIde7Navigation)
                .Include(d => d.ErrorIde8Navigation)
                .Include(d => d.ErrorIde9Navigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dtreport == null)
            {
                return NotFound();
            }

            return await Task.Run(() => View(dtreport));
        }

        // POST: Dtreports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Dtreport == null)
            {
                return await Task.Run(() => Problem("Entity set 'EISDBContext.Dtreport'  is null."));
            }
            var dtreport = await _context.Dtreport.FindAsync(id);
            if (dtreport != null)
            {
                _context.Dtreport.Remove(dtreport);
            }

            await _context.SaveChangesAsync();
            return await Task.Run(() => RedirectToAction(nameof(Index)));
        }

        private bool DtreportExists(int id)
        {
            return _context.Dtreport.Any(e => e.Id == id);
        }
    }
}

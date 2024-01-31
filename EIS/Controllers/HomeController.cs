using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EIS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using EIS.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EIS.EISModels;
using ChartJSCore.Helpers;
using ChartJSCore.Models;
using Microsoft.Data.SqlClient;
using System.Data;
//using Microsoft.CodeAnalysis.Options;

namespace EIS.Controllers
{
    //[ApiController]
    //[Route("[controller]")]
    public class HomeController : Controller
    {

        private readonly EISDBContext _context;
        //private CustomActionInvoker ActionInvoker;

        //public class CustomActionInvoker
        //{
        //    public bool InvokeAction(ControllerContext controllerContext, string actionName)
        //    {
        //        bool answer = false;
        //        if (actionName.Equals("getsubmenuid", StringComparison.CurrentCultureIgnoreCase))
        //        {
        //            string last = "1";
        //            try
        //            {
        //                last = controllerContext.HttpContext.Session.GetString("Submenu");

        //                if (last == null)
        //                {
        //                    last = "1";

        //                    controllerContext.HttpContext.Session.SetString("Submenu", last);
        //                }

        //                string ip = controllerContext.HttpContext.Connection.LocalIpAddress?.ToString();
        //                if (ip == "::1" | ip == "127.0.0.1")
        //                {
        //                    last = "0" + last;
        //                }
        //                else
        //                {
        //                    last = "1" + last;
        //                }

        //                string logoutstatus = controllerContext.HttpContext.Session.GetString("loginstatus");
        //                switch (logoutstatus)
        //                {
        //                    case "login":
        //                        last = "1" + last;
        //                        break;
        //                    case "register":
        //                        last = "2" + last;
        //                        break;
        //                    case "logout":
        //                        last = "3" + last;
        //                        break;
        //                    default:
        //                        last = "0" + last;
        //                        break;
        //                }
        //                answer = true;
        //            }
        //            catch
        //            {
        //                controllerContext.HttpContext.Session.SetString("Submenu", last);
        //            }
        //            return answer;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}
        public HomeController(EISDBContext context)
        {
            _context = context;
            //this.ActionInvoker = new CustomActionInvoker();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult SetServerDatas(string LineID, int? YearFrom, int? MonthFrom, int? DayFrom, int? YearTo, int? MonthTo, int? DayTo, int? Shift, int? Company, int? DolgozoId, int? StationId, int? LZOPId, string returnUrl)
        {
            if (returnUrl is null)
            {
                returnUrl = "~/";
            }
            if (LineID is null)
            {
                LineID = "GBM Line1";// VALEO RSW2";
            }
            if (YearFrom is null)
            {
                YearFrom = DateTime.Now.Year;
            }
            int yearfrom = (int)YearFrom;
            int year = (int)YearFrom;
            if (MonthFrom is null)
            {
                MonthFrom = DateTime.Now.Month;
            }
            int monthfrom = (int)MonthFrom;
            int month = (int)MonthFrom;
            if (DayFrom is null)
            {
                DayFrom = DateTime.Now.Day;
            }
            int dayfrom = (int)DayFrom;
            int day = (int)DayFrom;
            if (YearTo is null)
            {
                YearTo = DateTime.Now.Year;
            }
            int yearto = (int)YearTo;
            if (MonthTo is null)
            {
                MonthTo = DateTime.Now.Month;
            }
            int monthto = (int)MonthTo;
            if (DayTo is null)
            {
                DayTo = DateTime.Now.Day;
            }
            int dayto = (int)DayTo;
            if (Shift is null)
            {
                Shift = 4;
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

            if (LZOPId is null)
            {
                LZOPId = 4;
            }
            int lzopid = (int)LZOPId;

            if (Company is null)
            {
                Company = 1;
            }
            int company = (int)Company;

            if (DolgozoId is null)
            {
                DolgozoId = 11005;
            }
            int dolgozoid = (int)DolgozoId;

            if (StationId is null)
            {
                StationId = 0;
            }
            int stationid = (int)StationId;

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
            DateTime dtfrom = new DateTime(yearfrom, monthfrom, dayfrom);
            string dtfromstring = dtfrom.ToString("MM") + "/" + dtfrom.ToString("dd") + "/" + dtfrom.ToString("yyyy");
            HttpContext.Session.SetString("DTFrom", dtfromstring);
            string dtfromhustring = dtfrom.ToString("yyyy") + "." + dtfrom.ToString("MM") + "." + dtfrom.ToString("dd") + ".";
            HttpContext.Session.SetString("DTFromHU", dtfromhustring);
            string dtfromtxtstring = dtfrom.ToString("MMMM", new System.Globalization.CultureInfo("en-US")) + "/" + dtfrom.ToString("yyyy");
            HttpContext.Session.SetString("DTFromTxt", dtfromtxtstring);
            string dtfromtxthustring = dtfrom.ToString("yyyy") + "." + dtfrom.ToString("MMMM", new System.Globalization.CultureInfo("hu-HU")) + ".";
            HttpContext.Session.SetString("DTFromTxtHU", dtfromtxthustring);
            DateTime dtto = new DateTime(yearto, monthto, dayto);
            string dttostring = dtto.ToString("MM") + "/" + dtto.ToString("dd") + "/" + dtto.ToString("yyyy");
            HttpContext.Session.SetString("DTTo", dttostring);
            string dttohustring = dtto.ToString("yyyy") + "." + dtto.ToString("MM") + "." + dtto.ToString("dd") + ".";
            HttpContext.Session.SetString("DTToHU", dttohustring);
            string dttotxtstring = dtto.ToString("MMMM", new System.Globalization.CultureInfo("en-US")) + "/" + dtto.ToString("yyyy");
            HttpContext.Session.SetString("DTToTxt", dttotxtstring);
            string dttotxthustring = dtto.ToString("yyyy") + "." + dtto.ToString("MMMM", new System.Globalization.CultureInfo("hu-HU")) + ".";
            HttpContext.Session.SetString("DTToTxtHU", dttotxthustring);
            string dtintervalstring = dtfromstring + " - " + dttostring;
            HttpContext.Session.SetString("DTInterval", dtintervalstring);
            string dtintervalhustring = dtfromhustring + " - " + dttohustring;
            HttpContext.Session.SetString("DTIntervalHU", dtintervalhustring);

            HttpContext.Session.SetInt32("Shift", shift);
            HttpContext.Session.SetString("ShiftTxt", shifttxt);

            HttpContext.Session.SetInt32("Company", company);

            HttpContext.Session.SetInt32("DolgozoId", dolgozoid);

            HttpContext.Session.SetInt32("StationID", stationid);

            HttpContext.Session.SetInt32("LZOPId", lzopid);

            return LocalRedirect(returnUrl);
        }

        [Authorize]
        public async Task<IActionResult> IndexAsync()
        {
            //HttpContext.Session.SetString("Submenu", "1");
            HttpContext.Session.SetInt32("LZGroup", 0);
            HttpContext.Session.SetInt32("LineIDGroup", 1);
            HttpContext.Session.SetInt32("StationGroup", 0);
            HttpContext.Session.SetInt32("ShiftGroup", 1);
            HttpContext.Session.SetInt32("DayGroup", 3);
            HttpContext.Session.SetInt32("AddGroup", 0);
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
            //try
            //{
            var shifttable = await _context.ShiftsTable.Where(a => ((a.LineId == LineID | LineID == "ALL Line") & a.Starttime >= firstday & a.Starttime < lastday.AddDays(1) & ((shift < 4 & a.Muszak == shift) | shift == 4 | (shift == 5 & (a.Muszak == 1 | a.Muszak == 3)) | (shift == 6 & (a.Muszak == 2 | a.Muszak == 3)) | (shift == 7 & (a.Muszak == 1 | a.Muszak == 2))))).ToListAsync();
            int stid = 0;
            switch (LineID)
            {
                case "GBM Line1": stid = 20; break;
                case "GBM Line2": stid = 230; break;
                case "VALEO RSW2": stid = 20; break;
                case "EGAS": stid = 1; break;
                case "DVE5 Line": stid = 1; break;
                case "BRM Line1": stid = 12; break;
                case "WSL_AL": stid = 230; break;
                case "WSL_RT": stid = 230; break;
            }
            var shifttablehourly = await _context.ViewShiftTable(LineID, stid, shift, dateTimeFrom, dateTimeTo.AddDays(1)).OrderBy(a=>a.Hourly).ToListAsync();
            IEnumerable<Cttable> downtimetable = await _context.Cttable.Where(a => ((a.LineId == LineID | LineID == "ALL Line") & a.StartTime >= dateTimeFrom & a.StartTime <= dateTimeTo & (a.Status == "DownTime" | a.Status == "Alert1" | a.Status == "Alert2" | a.Status == "Alert3" | a.Status == "Alert4"))).OrderByDescending(b => b.TimeStamp).Include(d => d.Dttypes).ToListAsync();
            //List<ParetoReadResult> pareto = await _context.ParetoRead(LineID, firstday, lastday).ToListAsync(); //Havi
            List<ParetoReadResult> pareto = await _context.ParetoRead(LineID, dateTimeFrom, dateTimeTo.AddDays(1)).ToListAsync(); //Napi
            int[] hourly = new int[25];
            int first = firstday.Day;
            int last = lastday.Day;
            float[] days = new float[32];
            float[] tempdays = new float[32];
            float[] cntdays = new float[32];
            int GoodParts = 0;
            float GoodPartsf = 0;
            int RequestParts = 0;
            int BadParts = 0;
            float BadPartsf = 0;
            float CT = 0;
            float OEE = 0;
            float A = 0;
            float P = 0;
            float Q = 0;
            int dt = 0;
            int wt = 0;
            float wtf = 0;
            int DownTime = 0;
            float DownTimef = 0;
            float montlyOEE = 0;
            float montlyday = 0;
            if (shifttablehourly.Count > 0)
            {
                for (int i = 0; i < shifttablehourly.Count; i++)
                {
                    //if (shifttablehourly[i].StartTime >= dateTimeFrom & shifttablehourly[i].StartTime <= dateTimeTo)
                    //{
                    if (shifttablehourly[i].Counter.HasValue & shifttablehourly[i].Hourly.HasValue)
                    {
                        if (shifttablehourly[i].Counter > 1)
                        {
                            hourly[shifttablehourly[i].Hourly.Value] += shifttablehourly[i].Counter.Value;
                        }
                    }
                    //}
                }
            }
            if (shifttable.Count > 0)
            {
                for (int i = 0; i < shifttable.Count; i++)
                {
                    int x = shifttable[i].Starttime.Day;
                    bool active = (await _context.MesproductPlan.Where(a => a.LineId == shifttable[i].LineId & a.StartTime == shifttable[i].Starttime & a.Muszak == shifttable[i].Muszak & a.Active == true).ToListAsync()).Count() > 0;
                    if (active)
                    {
                        float munkaido = 0;
                        if (!shifttable[i].Munkaido.HasValue)
                        {
                            munkaido = shifttable[i].Elvart.Value > 0 & shifttable[i].Ct.Value > 0 ? shifttable[i].Elvart.Value * shifttable[i].Ct.Value : 0;
                        }
                        else
                        {
                            munkaido = shifttable[i].Munkaido.Value * 60;
                        }
                        float maxmunkaido = 0;
                        switch (shifttable[i].Muszak)
                        {
                            case 1: maxmunkaido = (8 * 60 + 20 - 20 - 5) * 60; break;
                            case 2: maxmunkaido = (8 * 60 + 20 - 5 - 20) * 60; break;
                            case 3: maxmunkaido = (7 * 60 + 20 - 20) * 60; break;
                        }
                        munkaido = Math.Min(maxmunkaido, munkaido);
                        if (shifttable[i].Szazalek.HasValue & shifttable[i].Gyartott.HasValue & shifttable[i].Ct.HasValue)
                        {
                            if (shifttable[i].Gyartott.Value > 0 & shifttable[i].Szazalek.Value > 0 & shifttable[i].Elvart.Value > 0 & munkaido > 0 & shifttable[i].Ct.Value > 0)
                            {
                                tempdays[x] += shifttable[i].Ct.Value * shifttable[i].Gyartott.Value;// munkaido > 0 ? 100 * (float)shifttable[i].Ct.Value*shifttable[i].Gyartott.Value / munkaido : 0;// shifttable[i].Szazalek.Value;
                                cntdays[x] += munkaido;
                                days[x] = 100 * tempdays[x] / cntdays[x];
                            }
                        }
                        if (shifttable[i].Starttime >= dateTimeFrom & shifttable[i].Starttime <= dateTimeTo & shifttable[i].Gyartott.HasValue & shifttable[i].Gyartott.Value > 0)
                        {
                            //if (shifttable[i].Gyartott.Value > 1)
                            //{
                            if (shifttable[i].Szazalek.HasValue)
                            {
                                OEE = days[x];
                            }
                            if (shifttable[i].Allasido.HasValue)
                            {
                                DownTime += shifttable[i].Allasido.Value;
                                DownTimef += shifttable[i].Allasido.Value * 60;
                            }
                            if (shifttable[i].Ct.HasValue)
                            {
                                CT = shifttable[i].Ct.Value;
                            }
                            if (shifttable[i].PGyartott.HasValue)
                            {
                                BadParts += shifttable[i].PGyartott.Value;
                                BadPartsf += shifttable[i].PGyartott.Value * shifttable[i].Ct.Value;
                            }
                            if (shifttable[i].Elvart.HasValue)
                            {
                                RequestParts += shifttable[i].Elvart.Value;
                            }
                            if (shifttable[i].Gyartott.HasValue)
                            {
                                GoodParts += shifttable[i].Gyartott.Value;
                                GoodPartsf += shifttable[i].Gyartott.Value * shifttable[i].Ct.Value;
                            }
                            if (shifttable[i].Allasido.HasValue)
                            {
                                dt += shifttable[i].Allasido.Value;
                            }
                            if (shifttable[i].Munkaido.HasValue)
                            {
                                wt += shifttable[i].Munkaido.Value * 60;
                                wtf += shifttable[i].Munkaido.Value * 60;
                            }
                            else
                            {
                                //munkaido = 0;
                                //if (shifttable[i].Elvart.HasValue & shifttable[i].Ct.HasValue)
                                //{
                                //    munkaido = shifttable[i].Elvart.Value > 0 & shifttable[i].Ct.Value > 0 ? shifttable[i].Elvart.Value * shifttable[i].Ct.Value : 0;
                                //}
                                wtf += munkaido;
                                wt += (int)munkaido;
                            }
                        }
                    }
                }
                for (int i = 1; i < 32; i++)
                {
                    if (days[i] > 0)
                    {
                        montlyOEE += tempdays[i];
                        montlyday += cntdays[i];
                    }
                }
            }
            int DT = CT > 0 & DownTime > 0 ? (int)(DownTime * 60 / CT) : 0;
            Chart verticalBarChart = GenerateVerticalBarChart(hourly, @requestCulture.RequestCulture.UICulture.Name);
            Chart lineScatterChart = GenerateLineScatterChart(LineID, year, month, first, last, days, OEE, @requestCulture.RequestCulture.UICulture.Name);
            Chart paretoChart = GenerateParetoChart(LineID, pareto, @requestCulture.RequestCulture.UICulture.Name);
            double ppercent = 0;
            double pdthour = 0;
            double pdthours = 0;
            for (int i = 0; i < pareto.Count; i++)
            {
                if (pareto[i] != null)
                {
                    if (pareto[i].TypeID.Trim() == "0000")
                    {
                        if (pareto[i].DThours.HasValue)
                        {
                            pdthour = pareto[i].DThours.Value;
                        }
                        if (pareto[i].activity_percentage.HasValue)
                        {
                            ppercent = 100 * pareto[i].activity_percentage.Value;
                        }
                    }
                    if (pareto[i].DThours.HasValue)
                    {
                        pdthours += pareto[i].DThours.Value;
                    }
                }
            }
            ViewData["ParetoPercent"] = ppercent;
            ViewData["ParetoDTHour"] = pdthour;
            ViewData["ParetoDTHours"] = pdthours;
            ViewData["DownTimeTable"] = downtimetable;
            ViewData["LineID"] = LineID;
            ViewData["Year"] = year;
            ViewData["Month"] = month;
            ViewData["Day"] = day;
            ViewData["Shift"] = shifttxt;
            ViewData["VerticalBarChart"] = verticalBarChart;
            ViewData["LineScatterChart"] = lineScatterChart;
            ViewData["ParetoChart"] = paretoChart;
            ViewData["GoodParts"] = GoodParts;
            ViewData["RequestParts"] = RequestParts;
            ViewData["BadParts"] = BadParts;
            ViewData["DTParts"] = DT;
            int EP = RequestParts - GoodParts - DT > 0 ? RequestParts - GoodParts - DT : 0;
            ViewData["EfficiencyParts"] = EP;
            int DiffP = Math.Max(RequestParts - GoodParts, BadParts + DT + EP);
            ViewData["DiffParts"] = DiffP;
            int RP = Math.Max(RequestParts, GoodParts + BadParts + DT + EP);
            ViewData["RequestParts"] = RP;
            //old
            //OEE = (GoodParts + BadParts + DT + EP) > 0 ? 100 * (float)GoodParts / (float)(GoodParts + BadParts + DT + EP) : 0;
            //A = (GoodParts + BadParts + DT + EP) > 0 ? 100 * (float)(GoodParts + EP + BadParts) / (float)(GoodParts + BadParts + DT + EP) : 0;
            //P = (GoodParts + BadParts + EP) > 0 ? 100 * (float)(GoodParts + BadParts) / (float)(GoodParts + BadParts + EP) : 0;
            //Q = (GoodParts + BadParts) > 0 ? 100 * (float)(GoodParts) / (float)(GoodParts + BadParts) : 0;
            //new from time
            OEE = wtf > 0 ? 100 * (float)GoodPartsf / (float)(wtf) : 0;
            A = (wtf) > 0 ? 100 * (float)(wtf - DownTimef) / (float)(wtf) : 0;
            P = (wtf - DownTimef) > 0 ? 100 * (float)(GoodPartsf + BadPartsf) / (float)(wtf - DownTimef) : 0;
            Q = (GoodPartsf + BadPartsf) > 0 ? 100 * (float)(GoodPartsf) / ((float)(GoodPartsf + BadPartsf)) : 0;
            ViewData["OEE"] = OEE;
            ViewData["MontlyOEE"] = montlyday > 0 ? 100 * montlyOEE / (float)montlyday : OEE;
            ViewData["A"] = A;
            ViewData["P"] = P;
            ViewData["Q"] = Q;
            ViewData["DT"] = firstday;
            if (downtimetable == null)
            {
                return await Task.Run(() => View());
            }
            //else
            //{
            //    for (int i=0; i<downtimetable.Count; i++)
            //    {
            //        if (dttemp.TypeId.Trim() == "0000")
            //        {
            //            var update = await _context.ViewCttableUpdate.Where(a => a.Id == dttemp.Id).FirstOrDefaultAsync();
            //            if (update != null)
            //            {
            //                if (update.Id == dttemp.Id)
            //                {
            //                    dttemp.TypeId = update.TypeId;
            //                }
            //            }
            //        }
            //    }
            //}
            return await Task.Run(() => View(downtimetable));
            //}
            //catch
            //{
            //    return NoContent();
            //}

        }

        private static Chart GenerateVerticalBarChart(int[] Hourly, string Language)
        {
            Chart chart = new Chart();
            chart.Type = ChartJSCore.Models.Enums.ChartType.Bar;

            ChartJSCore.Models.Data data = new ChartJSCore.Models.Data();

            data.Labels = new List<string>() { "6:00", "7:00", "8:00", "9:00", "10:00", "11:00", "12:00", "13:00", "14:00", "15:00", "16:00", "17:00", "18:00", "19:00", "20:00", "21:00", "22:00", "23:00", "0:00", "1:00", "2:00", "3:00", "4:00", "5:00" };

            if (Hourly == null)
            {
                Hourly = new int[26];
            }


            BarDataset dataset = new BarDataset()
            {
                Label = Language == "hu-HU" ? "ÓRÁNKÉNTI JÓ DARABSZÁM" : "HOURLY GOOD PARTS",
                Data = new List<double?>() { Hourly[ 6], Hourly[ 7], Hourly[ 8], Hourly[ 9], Hourly[10], Hourly[11], Hourly[12], Hourly[13],
                                             Hourly[14], Hourly[15], Hourly[16], Hourly[17], Hourly[18], Hourly[19], Hourly[20], Hourly[21],
                                             Hourly[22], Hourly[23], Hourly[ 0], Hourly[ 1], Hourly[ 2], Hourly[ 3], Hourly[ 4], Hourly[ 5],0
                                           },
                BackgroundColor = new List<ChartColor> { ChartColor.FromRgba(185, 212, 60, 1) },
                BorderColor = new List<ChartColor> { ChartColor.FromRgb(185, 212, 60) },
                MinBarLength = 2,
                //BarThickness = 48,
            };

            data.Datasets = new List<Dataset>();
            data.Datasets.Add(dataset);

            chart.Data = data;

            var options = new Options
            {
                Scales = new Dictionary<string, Scale>()
                {
                    { "y", new CartesianLinearScale()
                        {
                            BeginAtZero = true,
                            Min = 0
                        }
                    },
                    { "x", new Scale()
                        {
                            Grid = new Grid()
                            {
                                Offset = true
                            }
                        }
                    },
                }
            };

            chart.Options = options;

            chart.Options.Layout = new Layout
            {
                Padding = new Padding
                {
                    PaddingObject = new PaddingObject
                    {
                        Left = 10,
                        Right = 12
                    }
                }
            };

            return chart;
        }

        private static Chart GenerateLineScatterChart(string lineid, int year, int month, int first, int last, float[] days, float oee, string Language)
        {
            Chart chart = new Chart();
            chart.Type = ChartJSCore.Models.Enums.ChartType.Bar;

            ChartJSCore.Models.Data data = new ChartJSCore.Models.Data();

            if (days == null)
            {
                days = new float[32];
            }

            data.Labels = new List<string>();

            string[] months = { "", "JANUAR", "FEBRUAR", "MARCIUS", "APRILIS", "MAJUS", "JUNIUS", "JULIUS", "AUGUSZTUS", "SZEPTEMBER", "OKTOBER", "NOVEMBER", "DECEMBER" };

            LineDataset dataset = new LineDataset()
            {
                Label = "OEE " + year.ToString("0000") + " " + months[month] + " - " + lineid,
                Data = new List<double?>(),
                BackgroundColor = new List<ChartColor>() { ChartColor.FromRgba(185, 212, 60, 1) },
                BorderColor = new List<ChartColor>() { ChartColor.FromRgb(185, 212, 60) },
                PointRadius = new List<int>() { 5 },
                PointHitRadius = new List<int>() { 5 },
                PointStyle = new List<string>() { "crossRot" },
                PointBackgroundColor = new List<ChartColor>() { ChartColor.FromRgba(0, 0, 0, 1) },
                PointBorderColor = new List<ChartColor>() { ChartColor.FromRgba(0, 0, 0, 1) },
            };

            for (int i = first; i <= last; i++)
            {
                data.Labels.Add(i.ToString("00"));
                dataset.Data.Add(Math.Round(days[i], 2));
            }

            LineScatterDataset dataset100 = new LineScatterDataset()
            {
                Label = Language == "hu-HU" ? "ELVÁRT EREDMÉNY %-BAN" : "EXPECTED VALUE [%]",
                Data = new List<LineScatterData>(),
                BackgroundColor = new List<ChartColor> { ChartColor.FromRgba(127, 127, 127, 0.1) },
                BorderColor = new List<ChartColor> { ChartColor.FromRgba(127, 127, 127, 0.2) },
                PointRadius = new List<int>() { 5 },
                PointHitRadius = new List<int>() { 5 },
                PointStyle = new List<string>() { "crossRot" },
                PointBackgroundColor = new List<ChartColor>() { ChartColor.FromRgba(127, 127, 127, 1) },
                PointBorderColor = new List<ChartColor>() { ChartColor.FromRgba(127, 127, 127, 1) },
            };

            for (int i = first; i <= last; i++)
            {
                LineScatterData scatterData1 = new LineScatterData();
                scatterData1.X = i.ToString();
                scatterData1.Y = 85.ToString();
                dataset100.Data.Add(scatterData1);
            }

            data.Datasets = new List<Dataset>();
            data.Datasets.Add(dataset);
            data.Datasets.Add(dataset100);

            chart.Data = data;

            return chart;
        }

        private static Chart GenerateParetoChart(string lineid, List<ParetoReadResult> pareto, string Language)
        {
            Chart chart = new Chart();
            chart.Type = ChartJSCore.Models.Enums.ChartType.HorizontalBar;


            ChartJSCore.Models.Data data = new ChartJSCore.Models.Data();

            data.Labels = new List<string>();

            string[] months = { "", "JANUAR", "FEBRUAR", "MARCIUS", "APRILIS", "MAJUS", "JUNIUS", "JULIUS", "AUGUSZTUS", "SZEPTEMBER", "OKTOBER", "NOVEMBER", "DECEMBER" };

            BarDataset dataset = new BarDataset()
            {
                Label = Language == "hu-HU" ? "ÁLLÁSOK PARETO TOP10" : "DOWNTIME PARETO TOP10",
                Type = ChartJSCore.Models.Enums.ChartType.HorizontalBar,
                Data = new List<double?>(),
                BackgroundColor = new List<ChartColor>(),
                BorderColor = new List<ChartColor>(),
                BorderWidth = new List<int>() { 1 },
            };

            for (int i = 0; i < Math.Min(pareto.Count(), 10); i++)
            {
                data.Labels.Add(Language == "hu-HU" ? pareto[i].Description.Trim() : pareto[i].DescriptionEN.Trim());
                dataset.Data.Add(Math.Round(pareto[i].DThours.Value, 2));
                dataset.BackgroundColor.Add(ChartColor.FromRgba(185, 212, 60, 1));
                dataset.BorderColor.Add(ChartColor.FromRgba(185, 212, 60, 1));
            }

            data.Datasets = new List<Dataset>();
            data.Datasets.Add(dataset);

            chart.Data = data;

            var options = new Options
            {
                Scales = new Dictionary<string, Scale>()
                {
                    { "y", new Scale()
                        {
                            Min = 0
                        }
                    },
                    { "x", new Scale()
                        {
                            Min = 0
                        }
                    },
                }
            };

            chart.Options = options;
            return chart;
        }

        public async Task<IActionResult> LZHoursOfMonthAsync()
        {
            HttpContext.Session.SetString("Submenu", "2");
            HttpContext.Session.SetInt32("LZGroup", 1);
            HttpContext.Session.SetInt32("DayGroup", 3);
            HttpContext.Session.SetInt32("AddGroup", 0);
            HttpContext.Session.SetInt32("LZOPGroup", 0);
            HttpContext.Session.SetInt32("ShiftGroup", 1);
            HttpContext.Session.SetInt32("HeaderGroup", 1);
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
            ViewData["Year"] = year;
            ViewData["Month"] = month;
            try
            {
                SqlConnection conn = new SqlConnection("Server=10.20.10.22,1433;Database=TTS;Trust Server Certificate = true;Encrypt=False;Integrated Security=False;User=ttsdb;Password=data;MultipleActiveResultSets=True;");
                conn.Open();
                SqlCommand dCmd = new SqlCommand(Language == "hu-HU" ? "[dbo].[GetLZHours]" : "[dbo].[GetLZHoursEN]", conn);
                dCmd.CommandType = CommandType.StoredProcedure;
                dCmd.Parameters.Add(new SqlParameter("@Year", year));
                dCmd.Parameters.Add(new SqlParameter("@Month", month));
                dCmd.Parameters.Add(new SqlParameter("@Shift", shift));
                SqlDataAdapter da = new SqlDataAdapter(dCmd);
                DataTable table = new DataTable();
                DataSet ds = new DataSet();
                ds.Clear();

                da.Fill(ds);
                conn.Close();

                var das = ds.Tables[0].AsEnumerable();
                ViewBag.das = das;
                ViewBag.dasl = das.Count();
                List<string> list = new List<string>();
                foreach (DataColumn dc in ds.Tables[0].Columns)
                {
                    list.Add(dc.ColumnName);
                }
                ViewBag.dc = list;
                ViewBag.dcl = list.Count();
            }
            catch
            {
                return await Task.Run(() => NoContent());
            }
            return await Task.Run(() => View());
        }

        public async Task<IActionResult> LZCategoryOfMonthAsync()
        {
            HttpContext.Session.SetString("Submenu", "2");
            HttpContext.Session.SetInt32("LZGroup", 1);
            HttpContext.Session.SetInt32("DayGroup", 3);
            HttpContext.Session.SetInt32("AddGroup", 0);
            HttpContext.Session.SetInt32("LZOPGroup", 0);
            HttpContext.Session.SetInt32("ShiftGroup", 1);
            HttpContext.Session.SetInt32("HeaderGroup", 1);
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
            ViewData["Year"] = year;
            ViewData["Month"] = month;
            try
            {
                SqlConnection conn = new SqlConnection("Server=10.20.10.22,1433;Database=TTS;Trust Server Certificate = true;Encrypt=False;Integrated Security=False;User=ttsdb;Password=data;MultipleActiveResultSets=True;");
                conn.Open();
                SqlCommand dCmd = new SqlCommand(Language == "hu-HU" ? "[dbo].[GetLZCategory]" : "[dbo].[GetLZCategoryEN]", conn);
                dCmd.CommandType = CommandType.StoredProcedure;
                dCmd.Parameters.Add(new SqlParameter("@Year", year));
                dCmd.Parameters.Add(new SqlParameter("@Month", month));
                SqlDataAdapter da = new SqlDataAdapter(dCmd);
                DataTable table = new DataTable();
                DataSet ds = new DataSet();
                ds.Clear();

                da.Fill(ds);
                conn.Close();

                var das = ds.Tables[0].AsEnumerable();
                ViewBag.das = das;
                ViewBag.dasl = das.Count();
                List<string> list = new List<string>();
                foreach (DataColumn dc in ds.Tables[0].Columns)
                {
                    list.Add(dc.ColumnName);
                }
                ViewBag.dc = list;
                ViewBag.dcl = list.Count();
            }
            catch
            {
                return await Task.Run(() => NoContent());
            }
            return await Task.Run(() => View());
        }

        public async Task<IActionResult> LZViewerAsync()
        {
            HttpContext.Session.SetString("Submenu", "2");
            HttpContext.Session.SetInt32("LZGroup", 0);
            HttpContext.Session.SetInt32("DayGroup", 0);
            HttpContext.Session.SetInt32("AddGroup", 0);
            HttpContext.Session.SetInt32("LZOPGroup", 0);
            HttpContext.Session.SetInt32("ShiftGroup", 0);
            HttpContext.Session.SetInt32("HeaderGroup", 1);
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
            ViewData["Year"] = year;
            ViewData["Month"] = month;
            try
            {
                SqlConnection conn = new SqlConnection("Server=10.20.10.22,1433;Database=TTS;Trust Server Certificate = true;Encrypt=False;Integrated Security=False;User=ttsdb;Password=data;MultipleActiveResultSets=True;");
                conn.Open();
                SqlCommand dCmd = new SqlCommand(Language == "hu-HU" ? "[dbo].[GetLZCategory]" : "[dbo].[GetLZCategoryEN]", conn);
                dCmd.CommandType = CommandType.StoredProcedure;
                dCmd.Parameters.Add(new SqlParameter("@Year", year));
                dCmd.Parameters.Add(new SqlParameter("@Month", month));
                SqlDataAdapter da = new SqlDataAdapter(dCmd);
                DataTable table = new DataTable();
                DataSet ds = new DataSet();
                ds.Clear();

                da.Fill(ds);
                conn.Close();

                var das = ds.Tables[0].AsEnumerable();
                ViewBag.das = das;
                ViewBag.dasl = das.Count();
                List<string> list = new List<string>();
                foreach (DataColumn dc in ds.Tables[0].Columns)
                {
                    list.Add(dc.ColumnName);
                }
                ViewBag.dc = list;
                ViewBag.dcl = list.Count();
            }
            catch
            {
                return await Task.Run(() => NoContent());
            }
            return await Task.Run(() => View());
        }
        public async Task<IActionResult> LZHoursAsync()
        {
            HttpContext.Session.SetString("Submenu", "2");
            HttpContext.Session.SetInt32("LZGroup", 1);
            HttpContext.Session.SetInt32("DayGroup", 1);
            HttpContext.Session.SetInt32("AddGroup", 0);
            HttpContext.Session.SetInt32("ShiftGroup", 1);
            HttpContext.Session.SetInt32("LZOPGroup", 0);
            HttpContext.Session.SetInt32("HeaderGroup", 1);
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
            var viewLzHours = await _context.ViewLzHours.Where(a => a.Id == DolgozoId).ToListAsync();
            ViewData["Year"] = year;
            ViewData["Month"] = month;
            return await Task.Run(() => View(viewLzHours));
        }

        public async Task<IActionResult> LZHolidayOfMonthAsync()
        {
            HttpContext.Session.SetString("Submenu", "2");
            HttpContext.Session.SetInt32("LZGroup", 1);
            HttpContext.Session.SetInt32("DayGroup", 3);
            HttpContext.Session.SetInt32("AddGroup", 0);
            HttpContext.Session.SetInt32("LZOPGroup", 1);
            HttpContext.Session.SetInt32("HeaderGroup", 1);
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

            int LZOPId = 66;
            if (!HttpContext.Session.GetInt32("LZOPId").HasValue)
            {
                LZOPId = 66;
            }
            else
            {
                LZOPId = HttpContext.Session.GetInt32("LZOPId").Value;
            }
            int lzopid = (int)LZOPId;

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

            HttpContext.Session.SetInt32("LZOPId", LZOPId);
            #endregion
            ViewData["Year"] = year;
            ViewData["Month"] = month;
            try
            {
                SqlConnection conn = new SqlConnection("Server=10.20.10.22,1433;Database=TTS;Trust Server Certificate = true;Encrypt=False;Integrated Security=False;User=ttsdb;Password=data;MultipleActiveResultSets=True;");
                conn.Open();
                SqlCommand dCmd = new SqlCommand(Language == "hu-HU" ? "[dbo].[GetLZSzabadsag]" : "[dbo].[GetLZSzabadsagEN]", conn);
                dCmd.CommandType = CommandType.StoredProcedure;
                dCmd.Parameters.Add(new SqlParameter("@Year", year));
                dCmd.Parameters.Add(new SqlParameter("@Month", month));
                dCmd.Parameters.Add(new SqlParameter("@Code", lzopid));
                SqlDataAdapter da = new SqlDataAdapter(dCmd);
                DataTable table = new DataTable();
                DataSet ds = new DataSet();
                ds.Clear();

                da.Fill(ds);
                conn.Close();

                var das = ds.Tables[0].AsEnumerable();
                ViewBag.das = das;
                ViewBag.dasl = das.Count();
                List<DataColumn> list = new List<DataColumn>();
                foreach (DataColumn dc in ds.Tables[0].Columns)
                {
                    list.Add(dc);
                }
                ViewBag.dc = list;
                ViewBag.dcl = list.Count();
            }
            catch
            {
                return await Task.Run(() => NoContent());
            }
            return await Task.Run(() => View());
        }

        public async Task<IActionResult> LZSzabadsagAsync()
        {
            HttpContext.Session.SetString("Submenu", "2");
            HttpContext.Session.SetInt32("LZGroup", 1);
            HttpContext.Session.SetInt32("DayGroup", 1);
            HttpContext.Session.SetInt32("AddGroup", 0);
            HttpContext.Session.SetInt32("LZOPGroup", 0);
            HttpContext.Session.SetInt32("HeaderGroup", 1);
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

            int LZOPId = 66;
            if (!HttpContext.Session.GetInt32("LZOPId").HasValue)
            {
                LZOPId = 66;
            }
            else
            {
                LZOPId = HttpContext.Session.GetInt32("LZOPId").Value;
            }
            int lzopid = (int)LZOPId;

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

            HttpContext.Session.SetInt32("LZOPId", dolgozoid);
            #endregion
            var viewLzSzabadsag = await _context.ViewLzSzabadsag.Where(a => a.Id == DolgozoId).ToListAsync();
            ViewData["Year"] = year;
            ViewData["Month"] = month;
            return await Task.Run(() => View(viewLzSzabadsag));
        }

        public async Task<IActionResult> LZAbforAsync()
        {
            HttpContext.Session.SetString("Submenu", "2");
            HttpContext.Session.SetInt32("LZGroup", 1);
            HttpContext.Session.SetInt32("DayGroup", 1);
            HttpContext.Session.SetInt32("AddGroup", 0);
            HttpContext.Session.SetInt32("LZOPGroup", 0);
            HttpContext.Session.SetInt32("HeaderGroup", 1);
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
            var viewLzAbFor = await _context.ViewLzAbFor.Where(a => a.DolgozoId == dolgozoid).ToListAsync();
            ViewData["Year"] = year;
            ViewData["Month"] = month;
            return await Task.Run(() => View(viewLzAbFor));
        }


        public async Task<IActionResult> ViewGbmDownTimeReasonsAsync()
        {
            HttpContext.Session.SetInt32("LZGroup", 0);
            HttpContext.Session.SetInt32("DayGroup", 1);
            HttpContext.Session.SetInt32("AddGroup", 0);
            HttpContext.Session.SetInt32("HeaderGroup", 1);
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
            DateTime dateTime = new DateTime(year, month, day, 6, 0, 0);
            var viewGbmDownTimeReasonsAll = await _context.ViewGbmDownTimeReasons.Where(a => (a.LineId == LineID & a.StartTime == dateTime & ((shift < 4 & a.Muszak == shift) | shift == 4 | (shift == 5 & (a.Muszak == 1 | a.Muszak == 3)) | (shift == 6 & (a.Muszak == 2 | a.Muszak == 3)) | (shift == 7 & (a.Muszak == 1 | a.Muszak == 2))))).ToListAsync();
            List<ViewGbmDownTimeReasons> viewGbmDownTimeReason = new List<ViewGbmDownTimeReasons>();
            if (viewGbmDownTimeReasonsAll.Count > 0)
            {
                int counter = 0;
                viewGbmDownTimeReason.Add(viewGbmDownTimeReasonsAll[counter]);
                foreach (var viewGbmDownTimeReasons in viewGbmDownTimeReasonsAll)
                {
                    if (viewGbmDownTimeReasons != null)
                    {
                        if (viewGbmDownTimeReason[counter] != null)
                        {
                            DateTime dt = viewGbmDownTimeReason[counter].TimeStamp.AddSeconds(viewGbmDownTimeReason[counter].Ct.Value);
                            if (viewGbmDownTimeReasons.TimeStamp >= dt.AddMinutes(1))
                            {
                                viewGbmDownTimeReason.Add(viewGbmDownTimeReasons);
                                counter++;
                            }
                        }
                    }
                }
            }
            ViewData["LineID"] = LineID;
            ViewData["Year"] = year;
            ViewData["Month"] = month;
            ViewData["Day"] = day;
            ViewData["Shift"] = shifttxt;
            return await Task.Run(() => View(viewGbmDownTimeReason.Count > 0 ? viewGbmDownTimeReason.OrderBy(a => a.TimeStamp) : viewGbmDownTimeReasonsAll.Count > 0 ? viewGbmDownTimeReasonsAll.OrderBy(a => a.TimeStamp) : new List<ViewGbmDownTimeReasons>()));
        }

        // GET: DownTimeTables/Edit/5
        public async Task<IActionResult> Edit(int? dtid)
        {
            if (dtid == null || _context.Cttable == null)
            {
                return await Task.Run(() => NotFound());
            }
            else
            {
                var downTimeTable = await _context.Cttable.FindAsync(dtid);
                if (downTimeTable == null)
                {
                    return await Task.Run(() => NotFound());
                }
                else
                {
                    ViewData["LineId"] = new SelectList(_context.Dttypes, "LineId", "LineId", downTimeTable.LineId);
                    ViewData["DTGroupId"] = new SelectList(_context.DttypesGroup.Where(a => a.LineId == downTimeTable.LineId).OrderBy(b => b.DtgroupId), "DtgroupId", "Description");
                    ViewData["TypeId"] = new SelectList(_context.Dttypes.Where(a => a.LineId == downTimeTable.LineId).OrderBy(b => b.CodeId), "CodeId", "Search", downTimeTable.TypeId);
                    ViewData["TypeIdEn"] = new SelectList(_context.Dttypes.Where(a => a.LineId == downTimeTable.LineId).OrderBy(b => b.CodeId), "CodeId", "SearchEn", downTimeTable.TypeId);
                    return await Task.Run(() => PartialView("_EditModalPartion", downTimeTable));
                }
            }
        }

        // POST: DownTimeTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LineId,TimeStamp,Downtime,TypeId,StartTime,Muszak,AlertLevel,Id")] Cttable newcttable)
        {
            var cttable = await _context.Cttable.FindAsync(id);
            if (id != cttable.Id)
            {
                return NotFound();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        cttable.TypeId = newcttable.TypeId;
                        _context.Update(cttable);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!CTTableExists(cttable.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewData["LineId"] = new SelectList(_context.Dttypes, "LineId", "LineId", cttable.LineId);
                    ViewData["TypeId"] = new SelectList(_context.Dttypes.Where(a => a.LineId == cttable.LineId).OrderBy(b => b.CodeId), "CodeId", "Search", cttable.TypeId);
                    ViewData["TypeIdEn"] = new SelectList(_context.Dttypes.Where(a => a.LineId == cttable.LineId).OrderBy(b => b.CodeId), "CodeId", "SearchEn", cttable.TypeId);
                    return PartialView("_EditModalPartion", cttable);
                }
            }
        }

        // GET: DownTimeTables/Edit/5
        //public async Task<IActionResult> Edit_new(int? dtid)
        //{
        //    bool viewflag = false;
        //    if (!viewflag)
        //    {
        //        #region GetServerDatas v1.2
        //        string LineID = "GBM Line1";
        //        if (HttpContext.Session.GetString("LineID") == null)
        //        {
        //            LineID = "GBM Line1";// VALEO RSW2";
        //        }
        //        else
        //        {
        //            LineID = HttpContext.Session.GetString("LineID");
        //        }
        //        int Year = DateTime.Now.Year;
        //        if (!HttpContext.Session.GetInt32("Year").HasValue)
        //        {
        //            Year = DateTime.Now.Year;
        //        }
        //        else
        //        {
        //            Year = HttpContext.Session.GetInt32("Year").Value;
        //        }
        //        int year = (int)Year;
        //        int Month = DateTime.Now.Month;
        //        if (!HttpContext.Session.GetInt32("Month").HasValue)
        //        {
        //            Month = DateTime.Now.Month;
        //        }
        //        else
        //        {
        //            Month = HttpContext.Session.GetInt32("Month").Value;
        //        }
        //        int month = (int)Month;
        //        int Day = DateTime.Now.Day;
        //        if (!HttpContext.Session.GetInt32("Day").HasValue)
        //        {
        //            Day = DateTime.Now.Day;
        //        }
        //        else
        //        {
        //            Day = HttpContext.Session.GetInt32("Day").Value;
        //        }
        //        int day = (int)Day;
        //        int Shift = 4;
        //        if (!HttpContext.Session.GetInt32("Shift").HasValue)
        //        {
        //            Shift = 4;
        //        }
        //        else
        //        {
        //            Shift = HttpContext.Session.GetInt32("Shift").Value;
        //        }
        //        int shift = 4;
        //        string shifttxt = "All day";
        //        switch (Shift)
        //        {
        //            case 4: shifttxt = "All day"; shift = 4; break;
        //            case 1: shifttxt = "Morning"; shift = 1; break;
        //            case 2: shifttxt = "Afternoon"; shift = 2; break;
        //            case 3: shifttxt = "Overnight"; shift = 3; break;
        //            case 5: shifttxt = "Night+Morning"; shift = 5; break; // 1 + 3
        //            case 6: shifttxt = "Night+Afternoon"; shift = 6; break; // 2 + 3
        //            case 7: shifttxt = "Morning+Afternoon"; shift = 7; break; // 1 + 2
        //        }
        //        var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
        //        if (@requestCulture.RequestCulture.UICulture.Name == "hu-HU")
        //        {
        //            shifttxt = "Egész nap";
        //            switch (Shift)
        //            {
        //                case 4: shifttxt = "Egész nap"; shift = 4; break;
        //                case 1: shifttxt = "Délelőtt"; shift = 1; break;
        //                case 2: shifttxt = "Délután"; shift = 2; break;
        //                case 3: shifttxt = "Éjszaka"; shift = 3; break;
        //                case 5: shifttxt = "Éjszaka+Nappal"; shift = 5; break; // 1 + 3
        //                case 6: shifttxt = "Éjszaka+Délután"; shift = 6; break; // 2 + 3
        //                case 7: shifttxt = "Nappal+Délután"; shift = 7; break; // 1 + 2
        //            }
        //        }
        //        string Language = @requestCulture.RequestCulture.UICulture.Name;

        //        int Company = 1;
        //        if (!HttpContext.Session.GetInt32("Company").HasValue)
        //        {
        //            Company = 1;
        //        }
        //        else
        //        {
        //            Company = HttpContext.Session.GetInt32("Company").Value;
        //        }
        //        int company = (int)Company;

        //        int DolgozoId = 11005;
        //        if (!HttpContext.Session.GetInt32("DolgozoId").HasValue)
        //        {
        //            DolgozoId = 11005;
        //        }
        //        else
        //        {
        //            DolgozoId = HttpContext.Session.GetInt32("DolgozoId").Value;
        //        }
        //        int dolgozoid = (int)DolgozoId;

        //        if (HttpContext.Session.GetString("LineID") == null |
        //            HttpContext.Session.GetInt32("Year") == null |
        //            HttpContext.Session.GetInt32("Month") == null |
        //            HttpContext.Session.GetInt32("Day") == null |
        //            HttpContext.Session.GetInt32("Shift") == null |
        //            HttpContext.Session.GetInt32("Company") == null |
        //            HttpContext.Session.GetInt32("DolgozoId") == null)
        //        {
        //            Headers header = new()
        //            {
        //                MyFields = false,
        //                SearchString = ""
        //            };
        //            SessionHelper.SetObjectAsJson(HttpContext.Session, "header", header);
        //        }
        //        HttpContext.Session.SetString("LineID", LineID);

        //        HttpContext.Session.SetInt32("Year", year);
        //        HttpContext.Session.SetInt32("Month", month);
        //        HttpContext.Session.SetInt32("Day", day);

        //        HttpContext.Session.SetInt32("Shift", shift);
        //        HttpContext.Session.SetString("ShiftTxt", shifttxt);

        //        HttpContext.Session.SetInt32("Company", company);

        //        HttpContext.Session.SetInt32("DolgozoId", dolgozoid);
        //        #endregion
        //        if (dtid == null || _context.DownTimeTable == null)
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            var downTimeTable = await _context.DownTimeTable.FindAsync(dtid);
        //            if (downTimeTable == null)
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                var dtevent = await _context.Dtevent.Include(a => a.Dtfamily).Include(a => a.Dtreason).Where(a => a.Dtid == dtid).FirstOrDefaultAsync();
        //                if (dtevent == null)
        //                {
        //                    dtevent = new Dtevent()
        //                    {
        //                        Id = 0,
        //                        Dtid = dtid,
        //                        LineId = downTimeTable.LineId,
        //                        TimeStamp = downTimeTable.TimeStamp,
        //                        Downtime = downTimeTable.Downtime,
        //                        TypeId = downTimeTable.TypeId,
        //                        StartTime = downTimeTable.StartTime,
        //                        Muszak = downTimeTable.Muszak,
        //                        Status = downTimeTable.Status,
        //                        EndTime = downTimeTable.EndTime,
        //                        AlertLevel = downTimeTable.AlertLevel,
        //                        LastAlertLevel = downTimeTable.LastAlertLevel,
        //                        DtvalueId = downTimeTable.DtvalueId,
        //                        //DtconnectId,  - Calc from DtfamilyId,DtreasonId
        //                        //DtfamilyId,   - Select
        //                        //DtreasonId,   - Select
        //                        //St,           - Value
        //                        //Wt,           - Value
        //                        //Ctc,          - No Value
        //                        //An,           - Value
        //                        //Comment,      - Value
        //                        //Stneed,       - OK
        //                        //Wtneed,       - OK
        //                        //Ctcneed,      - NOK
        //                        //Anneed        - OK
        //                    };
        //                    var familyid = _context.Dtfamily.OrderBy(a => a.Description);
        //                    int selectfamilyid = 0;
        //                    if (familyid.Count() > 0)
        //                    {
        //                        selectfamilyid = familyid.FirstOrDefault().Id;
        //                    }
        //                    var familyiden = _context.Dtfamily.OrderBy(a => a.DescriptionEn);
        //                    int selectfamilyiden = 0;
        //                    if (familyiden.Count() > 0)
        //                    {
        //                        selectfamilyiden = familyiden.FirstOrDefault().Id;
        //                    }
        //                    var reasonid = _context.Dtconnect.Include(a => a.Dtreason).Where(a => a.DtfamilyId == selectfamilyid).OrderBy(b => b.Dtreason.Description);
        //                    int selectreasonid = 0;
        //                    int connectid = 0;
        //                    bool anneed = reasonid.FirstOrDefault().Dtreason.Anneed;
        //                    bool stneed = reasonid.FirstOrDefault().Dtreason.Stneed;
        //                    bool wtneed = reasonid.FirstOrDefault().Dtreason.Wtneed;
        //                    ViewData["DTreason"] = reasonid.FirstOrDefault().Dtfamily.Description;
        //                    ViewData["STNeed"] = stneed;
        //                    ViewData["WTNeed"] = wtneed;
        //                    ViewData["ANNeed"] = anneed;
        //                    if (reasonid.Count() > 0)
        //                    {
        //                        selectreasonid = reasonid.FirstOrDefault().DtreasonId;
        //                        connectid = reasonid.FirstOrDefault().Id;
        //                    }
        //                    var reasoniden = _context.Dtconnect.Include(a => a.Dtreason).Where(a => a.DtfamilyId == selectfamilyiden).OrderBy(b => b.Dtreason.DescriptionEn);
        //                    int selectreasoniden = 0;
        //                    int connectiden = 0;
        //                    bool anneeden = reasoniden.FirstOrDefault().Dtreason.Anneed;
        //                    bool stneeden = reasoniden.FirstOrDefault().Dtreason.Stneed;
        //                    bool wtneeden = reasoniden.FirstOrDefault().Dtreason.Wtneed;
        //                    ViewData["DTreasonEn"] = reasoniden.FirstOrDefault().Dtfamily.DescriptionEn;
        //                    ViewData["STNeedEn"] = stneeden;
        //                    ViewData["WTNeedEn"] = wtneeden;
        //                    ViewData["ANNeedEn"] = anneeden;
        //                    if (reasoniden.Count() > 0)
        //                    {
        //                        selectreasoniden = reasoniden.FirstOrDefault().DtreasonId;
        //                        connectiden = reasoniden.FirstOrDefault().Id;
        //                    }
        //                    ViewData["DTFamilyId"] = new SelectList(familyid, "Id", "Description", selectfamilyid);
        //                    ViewData["DTFamilyIdEn"] = new SelectList(familyiden, "Id", "DescriptionEn", selectfamilyiden);
        //                    ViewData["DTReasonId"] = new SelectList(reasonid, "Dtreason.Id", "Dtreason.Description", selectreasonid);
        //                    ViewData["DTReasonIdEn"] = new SelectList(reasoniden, "Dtreason.Id", "Dtreason.DescriptionEn", selectreasoniden);
        //                    if (@requestCulture.RequestCulture.UICulture.Name == "hu-HU")
        //                    {
        //                        dtevent.DtfamilyId = selectfamilyid;
        //                        dtevent.DtreasonId = selectreasonid;
        //                        dtevent.DtconnectId = connectid;
        //                        dtevent.Anneed = anneed;
        //                        dtevent.Stneed = stneed;
        //                        dtevent.Wtneed = wtneed;
        //                        viewflag = true;
        //                    }
        //                    else
        //                    {
        //                        dtevent.DtfamilyId = selectfamilyiden;
        //                        dtevent.DtreasonId = selectreasoniden;
        //                        dtevent.DtconnectId = connectiden;
        //                        dtevent.Anneed = anneeden;
        //                        dtevent.Stneed = stneeden;
        //                        dtevent.Wtneed = wtneeden;
        //                        viewflag = true;
        //                    }
        //                }
        //                else
        //                {
        //                    var familyid = _context.Dtfamily.OrderBy(a => a.Description);
        //                    int selectfamilyid = 0;
        //                    if (familyid.Count() > 0)
        //                    {
        //                        selectfamilyid = dtevent.DtfamilyId;
        //                    }
        //                    var familyiden = _context.Dtfamily.OrderBy(a => a.DescriptionEn);
        //                    int selectfamilyiden = 0;
        //                    if (familyiden.Count() > 0)
        //                    {
        //                        selectfamilyiden = dtevent.DtfamilyId;
        //                    }
        //                    var reasonid = _context.Dtconnect.Include(a => a.Dtreason).Where(a => a.DtfamilyId == selectfamilyid).OrderBy(b => b.Dtreason.Description);
        //                    ViewData["DTreason"] = _context.Dtfamily.OrderBy(a => a.Description).Where(a => a.Id == dtevent.DtfamilyId).FirstOrDefault().Description.ToUpper();
        //                    ViewData["STNeed"] = dtevent.Stneed;
        //                    ViewData["WTNeed"] = dtevent.Wtneed;
        //                    ViewData["ANNeed"] = dtevent.Anneed;
        //                    var reasoniden = _context.Dtconnect.Include(a => a.Dtreason).Where(a => a.DtfamilyId == selectfamilyiden).OrderBy(b => b.Dtreason.DescriptionEn);
        //                    ViewData["DTreasonEn"] = _context.Dtfamily.OrderBy(a => a.DescriptionEn).Where(a => a.Id == dtevent.DtfamilyId).FirstOrDefault().Description.ToUpper();
        //                    ViewData["STNeedEn"] = dtevent.Stneed;
        //                    ViewData["WTNeedEn"] = dtevent.Wtneed;
        //                    ViewData["ANNeedEn"] = dtevent.Anneed;
        //                    ViewData["DTFamilyId"] = new SelectList(familyid, "Id", "Description", dtevent.DtfamilyId);
        //                    ViewData["DTFamilyIdEn"] = new SelectList(familyiden, "Id", "DescriptionEn", dtevent.DtfamilyId);
        //                    ViewData["DTReasonId"] = new SelectList(reasonid, "Dtreason.Id", "Dtreason.Description", dtevent.DtreasonId);
        //                    ViewData["DTReasonIdEn"] = new SelectList(reasoniden, "Dtreason.Id", "Dtreason.DescriptionEn", dtevent.DtreasonId);
        //                    viewflag = true;
        //                }

        //                if (viewflag)
        //                {
        //                    ViewData["LineId"] = new SelectList(_context.Dttypes, "LineId", "LineId", downTimeTable.LineId);

        //                    return await Task.Run(() => PartialView("_EditModalPartion", dtevent));
        //                }
        //                viewflag = false;
        //            }
        //        }
        //    }
        //    return NotFound();
        //}

        //// POST: DownTimeTables/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit_new(int dtid, [Bind("Id,Dtid,LineId,TimeStamp,Downtime,TypeId,StartTime,Muszak,Status,EndTime,AlertLevel,LastAlertLevel,DtvalueId,DtconnectId,DtfamilyId,DtreasonId,St,Wt,Ctc,An,Comment,Stneed,Wtneed,Ctcneed,Anneed")] Dtevent dtevent)
        //{
        //    if (dtid != dtevent.Dtid)
        //    {
        //        return NotFound();
        //    }
        //    else
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            try
        //            {
        //                if (dtevent.DtfamilyId != 0 & dtevent.DtreasonId != 0)
        //                {
        //                    Dtconnect dtconnect = await _context.Dtconnect.Include(a => a.Dtfamily).Include(a => a.Dtreason).Where(a => a.DtfamilyId == dtevent.DtfamilyId & a.DtreasonId == dtevent.DtreasonId).FirstOrDefaultAsync();
        //                    if (dtconnect != null)
        //                    {
        //                        dtevent.DtconnectId = dtconnect.Id;
        //                        dtevent.Anneed = dtconnect.Dtreason.Anneed;
        //                        dtevent.Ctcneed = dtconnect.Dtreason.Ctcneed;
        //                        dtevent.Stneed = dtconnect.Dtreason.Stneed;
        //                        dtevent.Wtneed = dtconnect.Dtreason.Wtneed;
        //                    }
        //                }
        //                _context.Update(dtevent);
        //                await _context.SaveChangesAsync();
        //                return await Task.Run(() => RedirectToAction(nameof(Index)));
        //            }
        //            catch (DbUpdateConcurrencyException)
        //            {
        //                if (!DteventExists(dtevent.Id))
        //                {
        //                    return NotFound();
        //                }
        //                else
        //                {
        //                    throw;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            ViewData["DtconnectId"] = new SelectList(_context.Dtconnect.Include(a => a.Dtreason), "Id", "Dtraeson.Description", dtevent.DtconnectId);
        //            ViewData["DtconnectIdEn"] = new SelectList(_context.Dtconnect.Include(a => a.Dtreason), "Id", "Dtraeson.DescriptionEn", dtevent.DtconnectId);
        //            ViewData["DtvalueId"] = new SelectList(_context.Dtvalue, "Id", "Id", dtevent.DtvalueId);
        //            ViewData["LineId"] = new SelectList(_context.Dttypes, "LineId", "LineId", dtevent.LineId);
        //            var familyid = _context.Dtfamily.OrderBy(a => a.Description);
        //            var familyiden = _context.Dtfamily.OrderBy(a => a.DescriptionEn);
        //            var reasonid = _context.Dtconnect.Include(a => a.Dtreason).Where(a => a.DtfamilyId == dtevent.DtfamilyId).OrderBy(b => b.Dtreason.Description);
        //            var reasoniden = _context.Dtconnect.Include(a => a.Dtreason).Where(a => a.DtfamilyId == dtevent.DtfamilyId).OrderBy(b => b.Dtreason.DescriptionEn);
        //            ViewData["DTFamilyId"] = new SelectList(familyid, "Id", "Description", dtevent.DtfamilyId);
        //            ViewData["DTFamilyIdEn"] = new SelectList(familyiden, "Id", "DescriptionEn", dtevent.DtfamilyId);
        //            ViewData["DTReasonId"] = new SelectList(reasonid, "Dtreason.Id", "Dtreason.Description", dtevent.DtreasonId);
        //            ViewData["DTReasonIdEn"] = new SelectList(reasoniden, "Dtreason.Id", "Dtreason.DescriptionEn", dtevent.DtreasonId);
        //            return await Task.Run(() => PartialView("_EditModalPartion", dtevent));
        //        }
        //    }
        //}

        [HttpPost]
        //public IActionResult SetLanguage(string culture, string returnUrl)
        //{
        //    Response.Cookies.Append(
        //        CookieRequestCultureProvider.DefaultCookieName,
        //        CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
        //        new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
        //    );
        //    return LocalRedirect(returnUrl);
        //}

        //[HttpPost]
        public IActionResult setlanghun(string returnUrl)
        {
            string culture = "hu-HU";
            if (string.IsNullOrEmpty(returnUrl))
            {
                returnUrl = "~/";
            }
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );
            return LocalRedirect(returnUrl);
        }
        //[HttpPost]
        public IActionResult setlangall(string returnUrl)
        {
            string culture = "hu-HU";
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            if (@requestCulture.RequestCulture.UICulture.Name == "hu-HU")
            {
                culture = "en-US";
            }
            if (string.IsNullOrEmpty(returnUrl))
            {
                returnUrl = "~/";
            }
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );
            return LocalRedirect(returnUrl);
        }

        //[HttpPost]
        public IActionResult setlangeng(string returnUrl)
        {
            string culture = "en-US";
            if (string.IsNullOrEmpty(returnUrl))
            {
                returnUrl = "~/";
            }
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );
            return LocalRedirect(returnUrl);
        }

        public string setoee()
        {
            string last = HttpContext.Session.GetString("Submenu");

            HttpContext.Session.SetString("Submenu", "1");
            return last;
        }

        public string setlz()
        {
            string last = HttpContext.Session.GetString("Submenu");

            HttpContext.Session.SetString("Submenu", "2");
            return last;
        }

        public string setkpis()
        {
            string last = HttpContext.Session.GetString("Submenu");

            HttpContext.Session.SetString("Submenu", "3");
            return last;
        }

        public string setdashboard()
        {
            string last = HttpContext.Session.GetString("Submenu");

            HttpContext.Session.SetString("Submenu", "4");
            return last;
        }

        public string settask()
        {
            string last = HttpContext.Session.GetString("Submenu");

            HttpContext.Session.SetString("Submenu", "5");
            return last;
        }

        [HttpGet]
        public string getsubmenuid()
        {
            string last = "1";
            try
            {
                last = HttpContext.Session.GetString("Submenu");

                if (last == null)
                {
                    last = "1";

                    HttpContext.Session.SetString("Submenu", last);
                }

                string ip = HttpContext.Connection.LocalIpAddress?.ToString();
                if (ip == "::1" | ip == "127.0.0.1")
                {
                    last = "0" + last;
                }
                else
                {
                    last = "1" + last;
                }

                string logoutstatus = HttpContext.Session.GetString("loginstatus");
                switch (logoutstatus)
                {
                    case "login":
                        last = "1" + last;
                        break;
                    case "register":
                        last = "2" + last;
                        break;
                    case "logout":
                        last = "3" + last;
                        break;
                    default:
                        last = "0" + last;
                        break;
                }
            }
            catch
            {
                HttpContext.Session.SetString("Submenu", last);
            }
            return last;
        }


        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}

        private bool CTTableExists(int id)
        {
            return _context.Cttable.Any(e => e.Id == id);
        }

        [Route("error/404")]
        public IActionResult Error404()
        {
            return View();
        }
        [Route("error/{code:int}")]
        public IActionResult Error(int code)
        {
            // handle different codes or just return the default error view
            ViewData["code"] = code.ToString();
            return View();
        }
        [Route("sqlerror")]
        public IActionResult SQLError()
        {
            return View();
        }

    }
}

using EIS.EISModels;
using EIS.Helpers;
using EIS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIS.Controllers
{
    public class ShiftViewController : Controller
    {
        private readonly EISDBContext _context;

        public ShiftViewController(EISDBContext context/*, TIPDBContext tipcontext*/)
        {
            _context = context;
            //_tipcontext = tipcontext;
        }

        public async Task<IActionResult> IndexAsync()
        {
            HttpContext.Session.SetInt32("LZGroup", 0);
            HttpContext.Session.SetInt32("LineIDGroup", 1);
            HttpContext.Session.SetInt32("StationGroup", 0);
            HttpContext.Session.SetInt32("ShiftGroup", 1);
            HttpContext.Session.SetInt32("DayGroup", 1);
            HttpContext.Session.SetInt32("AddGroup", 0);
            HttpContext.Session.SetInt32("HeaderGroup", 1);
            #region GetServerDatas v1.3.1
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

            int StationId = 0;
            if (!HttpContext.Session.GetInt32("StationID").HasValue)
            {
                StationId = 0;
            }
            else
            {
                StationId = HttpContext.Session.GetInt32("StationID").Value;
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

            HttpContext.Session.SetInt32("Shift", shift);
            HttpContext.Session.SetString("ShiftTxt", shifttxt);

            HttpContext.Session.SetInt32("Company", company);

            HttpContext.Session.SetInt32("DolgozoId", dolgozoid);

            HttpContext.Session.SetInt32("StationId", stationid);
            #endregion
            DateTime dateTimeFrom = new DateTime(yearfrom, monthfrom, dayfrom, 6, 0, 0);
            DateTime dateTimeTo = new DateTime(yearto, monthto, dayto, 6, 0, 0);
            //int stationid = 230;
            int stationidline = 0;
            if (stationidline == 0)
            {
                switch (LineID.Trim())
                {
                    case "BRM Line1": stationidline = 12; break;
                    case "EGAS": stationidline = 1; break;
                    case "DVE5 Line": stationidline = 1; break;
                    case "GBM Line1": stationidline = 20; break;
                    case "GBM Line2": stationidline = 230; break;
                    case "VALEO RSW2": stationidline = 20; break;
                    case "WSL_AL": stationidline = 230; break;
                    case "WSL_RT": stationidline = 230; break;
                    default: stationidline = 1; break;
                }
            }
            if (stationid == 0)
            {
                switch (LineID.Trim())
                {
                    case "BRM Line1": stationid = 12; break;
                    case "EGAS": stationid = 1; break;
                    case "DVE5 Line": stationid = 1; break;
                    case "GBM Line1": stationid = 20; break;
                    case "GBM Line2": stationid = 230; break;
                    case "VALEO RSW2": stationid = 20; break;
                    case "WSL_AL": stationid = 230; break;
                    case "WSL_RT": stationid = 230; break;
                    default: stationid = 1; break;
                }
            }
            double ct = 5.7;
            if (LineID.Trim()!="All Line")
            {
                var ctdata = _context.SystemParamTable.Where(a => a.LineId.Trim() == LineID.Trim()).FirstOrDefault();
                if (ctdata != null)
                {
                    ct = ctdata.Cycletime;
                }
            }
            //var viewShiftViewResults = await _context.ViewShiftView(LineID, Shift, stationid, dateTimeFrom).OrderBy(a => a.TS).ToListAsync(); //Napi
            var viewShiftViewResultsLine = await _context.ViewShiftView(LineID, Shift, stationidline, dateTimeFrom).OrderBy(a => a.TS).ToListAsync(); //Napi Line
            List<ViewShiftViewResult> viewheader = new List<ViewShiftViewResult>();
            List<ViewShiftViewResult> viewlines = new List<ViewShiftViewResult>();
            //List<ViewShiftViewResult> viewlinespoint = new List<ViewShiftViewResult>();
            List<ViewShiftViewResult> viewlinespointline = new List<ViewShiftViewResult>();
            //List<ViewShiftViewResult> viewlinesgreen = new List<ViewShiftViewResult>();
            List<ViewShiftViewResult> viewlinesgreenline = new List<ViewShiftViewResult>();
            //List<ViewShiftViewResult> viewlinesyellow = new List<ViewShiftViewResult>();
            List<ViewShiftViewResult> viewlinesyellowline = new List<ViewShiftViewResult>();
            //List<ViewShiftViewResult> viewlinesred = new List<ViewShiftViewResult>();
            List<ViewShiftViewResult> viewlinesredline = new List<ViewShiftViewResult>();
            //List<ViewShiftViewResult> viewlinestext = new List<ViewShiftViewResult>();
            List<ViewShiftViewResult> viewlinestextline = new List<ViewShiftViewResult>();
            List<ViewShiftViewResult> viewlinesgray = new List<ViewShiftViewResult>();
            int lastshift = -1;
            //ViewShiftViewResult lastvalue = new ViewShiftViewResult(); 
            int minshift = 1;
            int maxshift = 3;
            int minhour = 6;
            int maxhour = 24 + 5;
            int lasttime = 0;
            int counter = viewShiftViewResultsLine.Count()-1;
            //int shifthour = 6;
            if (viewShiftViewResultsLine.Count() > 0)
            {
                //shifthour = viewShiftViewResults.First().Hour.Value;
                lastshift = viewShiftViewResultsLine.First().Muszak.Value;
                minshift = viewShiftViewResultsLine.First().Muszak.Value;
                maxshift = viewShiftViewResultsLine.Last().Muszak.Value;
                //lastvalue = viewShiftViewResults.Last();
            }
            switch (shift)
            {
                case 1: minshift = 1; maxshift = 1; minhour = 6; maxhour = 14; break;
                case 2: minshift = 2; maxshift = 2; minhour = 14; maxhour = 22; break;
                case 3: minshift = 3; maxshift = 3; minhour = 22; maxhour = 24 + 5; break;
                case 4: minshift = 1; maxshift = 3; minhour = 6; maxhour = 24 + 5; break;
            }
            int timeinterval = 3600;
            for (int shifthour = minhour; shifthour <= maxhour; shifthour++)
            {
                //Hourly inverval calculator
                switch (shifthour % 24)
                {
                    case 14:
                        bool active1 = false;
                        bool active2 = false;
                        if (lasttime == 0)
                        {
                            DateTime starttime1 = new DateTime(year, month, day, 6, 0, 0);
                            DateTime starthour1 = new DateTime(year, month, day, 14, 0, 0);
                            lasttime = 0;
                            active1 = _context.MesproductPlan.Where(a => a.LineId == LineID & a.StartTime == starttime1 & a.Muszak == 1 & a.Active == true).Count() > 0;
                            //if (active1)
                            //{
                                ViewShiftViewResult firstbreak1 = new ViewShiftViewResult()
                                {
                                    LineID = LineID,
                                    StartTime = starttime1,
                                    TimeStamp = starthour1,
                                    Muszak = 1,
                                    Hour = 14,
                                    Time = 0,
                                    TS = (int)(starthour1 - starttime1).TotalSeconds,
                                    CNT = 0,
                                    CT = active1 ? 1200 : 0,
                                    Status = "Műszak"
                                };
                                viewlines.Add(firstbreak1);
                                lasttime = active1 ? 1200 : 0;
                            //}
                            starttime1 = new DateTime(year, month, day, 6, 0, 0);
                            starthour1 = new DateTime(year, month, day, 14, 20, 0);
                            active2 = _context.MesproductPlan.Where(a => a.LineId == LineID & a.StartTime == starttime1 & a.Muszak == 2 & a.Active == true).Count() > 0;
                            //if (active2)
                            //{
                                firstbreak1 = new ViewShiftViewResult()
                                {
                                    LineID = LineID,
                                    StartTime = starttime1,
                                    TimeStamp = starthour1,
                                    Muszak = 2,
                                    Hour = 14,
                                    Time = 1200,
                                    TS = (int)(starthour1 - starttime1).TotalSeconds,
                                    CNT = 0,
                                    CT = active2 ? 3600 : 1200,
                                    Status = "Műszak"
                                };
                                viewlines.Add(firstbreak1);
                                lasttime = active2 ? 3600 : active1 ? 1200 : 0;
                            //}
                        }
                        timeinterval = shift == 4 ? 3600 : shift == 1 ? 1200 : shift == 2 ? 2400 : 0;
                        if (active1 & active2)
                        {
                            timeinterval = Math.Min(timeinterval, 3600);
                        }
                        if (active1 & !active2)
                        {
                            timeinterval = Math.Min(timeinterval, 1200);
                        }
                        if (!active1 & active2)
                        {
                            timeinterval = Math.Min(timeinterval, 2400);
                        }
                        if (!active1 & !active2)
                        {
                            timeinterval = Math.Min(timeinterval, 0);
                        }
                        break;
                    case 22:
                        active2 = false;
                        bool active3 = false;
                        if (lasttime == 0)
                        {
                            lasttime = 0;
                            DateTime starttime1 = new DateTime(year, month, day, 6, 0, 0);
                            DateTime starthour1 = new DateTime(year, month, day, 22, 0, 0);
                            active2 = _context.MesproductPlan.Where(a => a.LineId == LineID & a.StartTime == starttime1 & a.Muszak == 2 & a.Active == true).Count() > 0;
                            //if (active2)
                            //{
                                ViewShiftViewResult firstbreak1 = new ViewShiftViewResult()
                                {
                                    LineID = LineID,
                                    StartTime = starttime1,
                                    TimeStamp = starthour1,
                                    Muszak = 2,
                                    Hour = 22,
                                    Time = 0,
                                    TS = (int)(starthour1 - starttime1).TotalSeconds,
                                    CNT = 0,
                                    CT = active2 ? 2400 : 0,
                                    Status = "Műszak"
                                };
                                viewlines.Add(firstbreak1);
                                lasttime = active2 ? 2400 : 0;
                            //}
                            starttime1 = new DateTime(year, month, day, 6, 0, 0);
                            starthour1 = new DateTime(year, month, day, 22, 40, 0);
                            active3 = _context.MesproductPlan.Where(a => a.LineId == LineID & a.StartTime == starttime1 & a.Muszak == 3 & a.Active == true).Count() > 0;
                            //if (active3)
                            //{
                                firstbreak1 = new ViewShiftViewResult()
                                {
                                    LineID = LineID,
                                    StartTime = starttime1,
                                    TimeStamp = starthour1,
                                    Muszak = 3,
                                    Hour = 22,
                                    Time = 2400,
                                    TS = (int)(starthour1 - starttime1).TotalSeconds,
                                    CNT = 0,
                                    CT = active3 ? 3600 : 2400,
                                    Status = "Műszak"
                                };
                                viewlines.Add(firstbreak1);
                                lasttime = active3 ? 3600 : active2 ? 2400 : 0;
                            //}
                        }
                        timeinterval = shift == 4 ? 3600 : shift == 2 ? 2400 : shift == 3 ? 1200 : 0;
                        if (active2 & active3)
                        {
                            timeinterval = Math.Min(timeinterval, 3600);
                        }
                        if (active2 & !active3)
                        {
                            timeinterval = Math.Min(timeinterval, 2400);
                        }
                        if (!active2 & active3)
                        {
                            timeinterval = Math.Min(timeinterval, 1200);
                        }
                        if (!active2 & !active3)
                        {
                            timeinterval = Math.Min(timeinterval, 0);
                        }
                        break;
                    default:
                        bool active = false;
                        if (lasttime == 0)
                        {
                            DateTime starttime1 = new DateTime(year, month, day, 6, 0, 0);
                            DateTime starthour1 = new DateTime(year, month, day, shifthour % 24, 0, 0);
                            if (shifthour % 24 < 6)
                            {
                                starthour1 = starthour1.AddDays(1);
                            }
                            active = _context.MesproductPlan.Where(a => a.LineId == LineID & a.StartTime == starttime1 & a.Muszak == (((starthour1.Hour < 14) | ((starthour1.Hour == 14) & (starthour1.Minute < 20))) & starthour1.Hour >= 6 ? 1 : ((starthour1.Hour > 22) | ((starthour1.Hour == 22) & (starthour1.Minute >= 40))) | starthour1.Hour < 6 ? 3 : 2) & a.Active == true).Count() > 0;
                            //if (active)
                            //{
                                ViewShiftViewResult firstbreak1 = new ViewShiftViewResult()
                                {
                                    LineID = LineID,
                                    StartTime = starttime1,
                                    TimeStamp = starthour1,
                                    Muszak = ((starthour1.Hour < 14) | ((starthour1.Hour == 14) & (starthour1.Minute < 20))) & starthour1.Hour >= 6 ? 1 : ((starthour1.Hour > 22) | ((starthour1.Hour == 22) & (starthour1.Minute >= 40))) | starthour1.Hour < 6 ? 3 : 2,
                                    Hour = shifthour % 24,
                                    Time = 0,
                                    TS = (int)(starthour1 - starttime1).TotalSeconds,
                                    CNT = 0,
                                    CT = active ? 3600 : 0,
                                    Status = "Munkaidő"
                                };
                                viewlines.Add(firstbreak1);
                            //}
                            lasttime = active ? 3600 : 0;
                        }
                        timeinterval = active ? 3600 : 0;
                        break;
                }
                //Planned Downtime
                ViewShiftViewResult data = viewShiftViewResultsLine.Where(a => a.Hour == shifthour % 24 & a.Type.Trim()=="PDT" & a.TimeStamp.Value <= DateTime.Now).FirstOrDefault();
                if (data != null)
                {
                    //active = _context.MesproductPlan.Where(a => a.LineId == LineID & a.StartTime == starttime1 & a.Muszak == (((starthour1.Hour < 14) | ((starthour1.Hour == 14) & (starthour1.Minute < 20))) & starthour1.Hour >= 6 ? 1 : ((starthour1.Hour > 22) | ((starthour1.Hour == 22) & (starthour1.Minute >= 40))) | starthour1.Hour < 6 ? 3 : 2) & a.Active == true).Count() > 0;
                    if (data.Type.Trim() == "PDT")
                    {
                        string s = data.Status.Trim();
                        if (s.Length * 16 > (Math.Min(data.CT.Value, 3600) - data.Time.Value))
                        {
                            if (s.Substring(0, 2).ToUpper() == "ST")
                            {
                                data.Type = s.Split(" - ")[0].Trim();// + "...";
                            }
                            else
                            {
                                data.Type = s.Substring(0, Math.Min(s.Length, Math.Max((Math.Min(data.CT.Value, 3600) - data.Time.Value - 48) / 16, 1))).Trim() + "...";
                            }
                        }
                        else
                        {
                            data.Type = s;
                        }
                        viewlinesgray.Add(data);
                        //viewlinestext.Add(data);
                        if (data.CT.Value > 3600)
                        {
                            for (int j = 0; j < (int)(data.CT.Value / 3600); j++)
                            {
                                DateTime hour = data.TimeStamp.Value.AddSeconds(3600 - data.Time.Value).AddHours(j);                                
                                ViewShiftViewResult firstbreak1 = new ViewShiftViewResult()
                                {
                                    LineID = LineID,
                                    StartTime = dateTimeFrom,
                                    Muszak = Math.Min(lastshift, ((hour.TimeOfDay.Hours == 14 & hour.TimeOfDay.Minutes <= 20) | (hour.TimeOfDay.Hours < 14)) & hour.TimeOfDay.Hours >= 6 ? 1 : hour.TimeOfDay.Hours < 6 & (hour.TimeOfDay.Hours > 22 | (hour.TimeOfDay.Hours == 22 & hour.TimeOfDay.Minutes >= 40)) ? 2 : 3),
                                    Hour = (data.Hour.Value + (j + 1)) % 24,
                                    TS = data.TS.Value + ((j + 1) * 3600 - data.Time.Value),
                                    TimeStamp = data.TimeStamp.Value.AddSeconds(3600 - data.Time.Value).AddHours(j),
                                    CNT = 0,
                                    CT = Math.Min(data.CT.Value - (j + 1) * 3600, 3600),
                                    Time = 0,
                                    Type = data.Type.Trim(),
                                    Status = data.Status.Trim() == "" ? "Gépállás" : data.Status.Trim(),
                                    Request = 0,
                                    OEE = 0,
                                };
                                string s1 = data.Status.Trim();
                                if (s1.Length * 16 > Math.Min(firstbreak1.CT.Value, 3600) - firstbreak1.Time.Value)
                                {
                                    if (s.Substring(0, 2).ToUpper() == "ST")
                                    {
                                        firstbreak1.Type = s1.Split(" - ")[0].Trim();// + "...";
                                    }
                                    else
                                    {
                                        firstbreak1.Type = s1.Substring(0, Math.Min(s1.Length, Math.Max((Math.Min(firstbreak1.CT.Value, 3600) - firstbreak1.Time.Value - 48) / 16, 1))).Trim() + "...";
                                    }
                                }
                                else
                                {
                                    firstbreak1.Type = s1;
                                }
                                viewlinesgray.Add(firstbreak1);
                                //viewlinestext.Add(firstbreak1);
                            }
                        }
                        lasttime = data.CT.Value > 3600 ? data.CT.Value % 3600 : data.CT.Value;
                        lasttime = data.CT.Value;
                        timeinterval = timeinterval - (data.CT.Value - data.Time.Value);
                    }
                }
                //Hourly OEE, Request, Qantity datas
                DateTime starttime = new DateTime(year, month, day, 6, 0, 0);
                DateTime starthour = new DateTime(year, month, day, shifthour % 24, 0, 0).AddSeconds(viewlines.Last().Time.Value);
                int cnt = viewShiftViewResultsLine.Where(a => a.Hour == shifthour % 24 & a.NOK == false).Count();
                int request = ct > 0 ? (int)(0.5 + timeinterval / ct) : 0;
                double oee = request > 0 ? Math.Round((100 * (double)cnt / (double)request), 2) : 0;
                ViewShiftViewResult firstbreak = new ViewShiftViewResult()
                {
                    LineID = LineID,
                    StartTime = starttime,
                    TimeStamp = starthour,
                    Muszak = lastshift,
                    Hour = shifthour % 24,
                    Time = viewlines.Last().Time.Value,
                    TS = (int)(starthour - starttime).TotalSeconds,
                    CNT = cnt,
                    CT = viewlines.Last().CT.Value - viewlines.Last().Time.Value,
                    Type = "DT",
                    Status = "Gépállás",
                    Request = request,
                    OEE = oee,
                };
                viewheader.Add(firstbreak);
                lasttime = 0;
            }
            //Dialy datas for station
            //for (int i = 0; i < viewShiftViewResults.Count(); i++)
            //{
            //    ViewShiftViewResult data = viewShiftViewResults[i];
            //    //Running
            //    switch (data.Status.Trim())
            //    {
            //        case "Normal":
            //            viewlinesgreen.Add(data);
            //            if (data.NOK.HasValue)
            //            {
            //                if (!data.NOK.Value)
            //                {
            //                    viewlinespoint.Add(data);
            //                }
            //            }
            //            else
            //            {
            //                if (data != null)
            //                {
            //                    viewlinespoint.Add(data);
            //                }
            //            }
            //            if (data.CT.Value > 3600)
            //            {
            //                for (int j = 0; j < (int)(data.CT.Value / 3600); j++)
            //                {
            //                    DateTime hour = data.TimeStamp.Value.AddSeconds(3600 - data.Time.Value + 0.5).AddHours(j);
            //                    ViewShiftViewResult firstbreak = new ViewShiftViewResult()
            //                    {
            //                        LineID = LineID,
            //                        StartTime = dateTimeFrom,
            //                        Muszak = Math.Min(lastshift, ((hour.TimeOfDay.Hours == 14 & hour.TimeOfDay.Minutes <= 20) | (hour.TimeOfDay.Hours < 14)) & hour.TimeOfDay.Hours >= 6 ? 1 : hour.TimeOfDay.Hours < 6 & (hour.TimeOfDay.Hours > 22 | (hour.TimeOfDay.Hours == 22 & hour.TimeOfDay.Minutes >= 40)) ? 2 : 3),
            //                        Hour = (data.Hour.Value + (j + 1)) % 24,
            //                        TS = data.TS.Value + ((j + 1) * 3600 - data.Time.Value),
            //                        TimeStamp = data.TimeStamp.Value.AddSeconds(3600 - data.Time.Value + 0.5).AddHours(j),
            //                        CNT = 0,
            //                        CT = Math.Min(data.CT.Value - (j + 1) * 3600, 3600),
            //                        Time = 0,
            //                        Type = data.Type.Trim(),
            //                        Status = data.Status.Trim() == "" ? "Gépállás" : data.Status.Trim(),
            //                        Request = 0,
            //                        OEE = 0,
            //                    };
            //                    string s1 = data.Status.Trim();
            //                    if (s1.Length * 16 > Math.Min(firstbreak.CT.Value, 3600) - firstbreak.Time.Value - 48)
            //                    {
            //                        if (s1.Substring(0, 2).ToUpper() == "ST")
            //                        {
            //                            firstbreak.Type = s1.Split(" - ")[0].Trim();// + "...";
            //                        }
            //                        else
            //                        {
            //                            firstbreak.Type = s1.Substring(0, Math.Min(s1.Length, Math.Max((Math.Min(firstbreak.CT.Value, 3600) - firstbreak.Time.Value - 48) / 16, 1))).Trim() + "...";
            //                        }
            //                    }
            //                    else
            //                    {
            //                        firstbreak.Type = s1;
            //                    }
            //                    viewlinesred.Add(firstbreak);
            //                }
            //            }
            //            lasttime = data.CT.Value > 3600 ? data.CT.Value % 3600 : data.CT.Value;
            //            break;
            //        case "Slow":
            //            viewlinesyellow.Add(data);
            //            if (data.NOK.HasValue)
            //            {
            //                if (!data.NOK.Value)
            //                {
            //                    viewlinespoint.Add(data);
            //                }
            //            }
            //            else
            //            {
            //                if (data != null)
            //                {
            //                    viewlinespoint.Add(data);
            //                }
            //            }
            //            if (data.CT.Value > 3600)
            //            {
            //                for (int j = 0; j < (int)(data.CT.Value / 3600); j++)
            //                {
            //                    DateTime hour = data.TimeStamp.Value.AddSeconds(3600 - data.Time.Value + 0.5).AddHours(j);
            //                    ViewShiftViewResult firstbreak = new ViewShiftViewResult()
            //                    {
            //                        LineID = LineID,
            //                        StartTime = dateTimeFrom,
            //                        Muszak = Math.Min(lastshift, ((hour.TimeOfDay.Hours == 14 & hour.TimeOfDay.Minutes <= 20) | (hour.TimeOfDay.Hours < 14)) & hour.TimeOfDay.Hours >= 6 ? 1 : hour.TimeOfDay.Hours < 6 & (hour.TimeOfDay.Hours > 22 | (hour.TimeOfDay.Hours == 22 & hour.TimeOfDay.Minutes >= 40)) ? 2 : 3),
            //                        Hour = (data.Hour.Value + (j + 1)) % 24,
            //                        TS = data.TS.Value + ((j + 1) * 3600 - data.Time.Value),
            //                        TimeStamp = data.TimeStamp.Value.AddSeconds(3600 - data.Time.Value + 0.5 ).AddHours(j),
            //                        CNT = 0,
            //                        CT = Math.Min(data.CT.Value - (j + 1) * 3600, 3600),
            //                        Time = 0,
            //                        Type = data.Type.Trim(),
            //                        Status = data.Status.Trim() == "" ? "Gépállás" : data.Status.Trim(),
            //                        Request = 0,
            //                        OEE = 0,
            //                    };
            //                    string s1 = data.Status.Trim();
            //                    if (s1.Length * 16 > Math.Min(firstbreak.CT.Value, 3600) - firstbreak.Time.Value - 48)
            //                    {
            //                        if (s1.Substring(0, 2).ToUpper() == "ST")
            //                        {
            //                            firstbreak.Type = s1.Split(" - ")[0].Trim();// + "...";
            //                        }
            //                        else
            //                        {
            //                            firstbreak.Type = s1.Substring(0, Math.Min(s1.Length, Math.Max((Math.Min(firstbreak.CT.Value, 3600) - firstbreak.Time.Value - 48) / 16, 1))).Trim() + "...";
            //                        }
            //                    }
            //                    else
            //                    {
            //                        firstbreak.Type = s1;
            //                    }
            //                    viewlinesred.Add(firstbreak);
            //                }
            //            }
            //            lasttime = data.CT.Value > 3600 ? data.CT.Value % 3600 : data.CT.Value;
            //            break;
            //        case "MicroStop":
            //        case "DownTime":
            //        case "Alert1":
            //        case "Alert2":
            //        case "Alert3":
            //            viewlinesred.Add(data);
            //            if (data.NOK.HasValue)
            //            {
            //                if (!data.NOK.Value)
            //                {
            //                    viewlinespoint.Add(data);
            //                }
            //            }
            //            else
            //            {
            //                if (data != null)
            //                {
            //                    viewlinespoint.Add(data);
            //                }
            //            }
            //            if (data.CT.Value > 3600)
            //            {
            //                for (int j = 0; j < (int)(data.CT.Value / 3600); j++)
            //                {
            //                    DateTime hour = data.TimeStamp.Value.AddSeconds(3600 - data.Time.Value + 0.5).AddHours(j);
            //                    ViewShiftViewResult firstbreak = new ViewShiftViewResult()
            //                    {
            //                        LineID = LineID,
            //                        StartTime = dateTimeFrom,
            //                        Muszak = Math.Min(lastshift, ((hour.TimeOfDay.Hours == 14 & hour.TimeOfDay.Minutes <= 20) | (hour.TimeOfDay.Hours < 14)) & hour.TimeOfDay.Hours >= 6 ? 1 : hour.TimeOfDay.Hours < 6 & (hour.TimeOfDay.Hours > 22 | (hour.TimeOfDay.Hours == 22 & hour.TimeOfDay.Minutes >= 40)) ? 2 : 3),
            //                        Hour = (data.Hour.Value + (j + 1)) % 24,
            //                        TS = data.TS.Value + ((j + 1) * 3600 - data.Time.Value),
            //                        TimeStamp = data.TimeStamp.Value.AddSeconds(3600 - data.Time.Value + 0.5).AddHours(j),
            //                        CNT = 0,
            //                        CT = Math.Min(data.CT.Value - (j + 1) * 3600, 3600),
            //                        Time = 0,
            //                        Type = data.Type.Trim(),
            //                        Status = data.Status.Trim() == "" ? "Gépállás" : data.Status.Trim(),
            //                        Request = 0,
            //                        OEE = 0,
            //                    };
            //                    string s1 = data.Status.Trim();
            //                    if (s1.Length * 16 > Math.Min(firstbreak.CT.Value, 3600) - firstbreak.Time.Value - 48)
            //                    {
            //                        if (s1.Substring(0, 2).ToUpper() == "ST")
            //                        {
            //                            firstbreak.Type = s1.Split(" - ")[0].Trim();// + "...";
            //                        }
            //                        else
            //                        {
            //                            firstbreak.Type = s1.Substring(0, Math.Min(s1.Length, Math.Max((Math.Min(firstbreak.CT.Value, 3600) - firstbreak.Time.Value - 48) / 16, 1))).Trim() + "...";
            //                        }
            //                    }
            //                    else
            //                    {
            //                        firstbreak.Type = s1;
            //                    }
            //                    viewlinesred.Add(firstbreak);
            //                }
            //            }
            //            lasttime = data.CT.Value > 3600 ? data.CT.Value % 3600 : data.CT.Value;
            //            break;
            //        default:
            //            if (data.Type.Trim() == "DT")
            //            {
            //                string s = data.Status.Trim();
            //                if (s.Length * 16 > (Math.Min(data.CT.Value, 3600) - data.Time.Value - 48))
            //                {
            //                    if (s.Substring(0, 2).ToUpper() == "ST")
            //                    {
            //                        data.Type = s.Split(" - ")[0].Trim();// + "...";
            //                    }
            //                    else
            //                    {
            //                        data.Type = s.Substring(0, Math.Min(s.Length, Math.Max((Math.Min(data.CT.Value,3600) - data.Time.Value - 48)/16,1))).Trim() + "...";
            //                    }
            //                }
            //                else
            //                {
            //                    data.Type = s;
            //                }
            //                viewlinesred.Add(data);
            //                viewlinestext.Add(data);
            //                if (data.CT.Value > 3600)
            //                {
            //                    for (int j = 0; j < (int)(data.CT.Value / 3600); j++)
            //                    {
            //                        DateTime hour = data.TimeStamp.Value.AddSeconds(3600 - data.Time.Value + 0.5).AddHours(j);
            //                        ViewShiftViewResult firstbreak = new ViewShiftViewResult()
            //                        {
            //                            LineID = LineID,
            //                            StartTime = dateTimeFrom,
            //                            Muszak = Math.Min(lastshift, ((hour.TimeOfDay.Hours==14 & hour.TimeOfDay.Minutes<=20) | (hour.TimeOfDay.Hours<14)) & hour.TimeOfDay.Hours >= 6 ? 1 : hour.TimeOfDay.Hours < 6 & (hour.TimeOfDay.Hours > 22 | (hour.TimeOfDay.Hours == 22 & hour.TimeOfDay.Minutes >= 40)) ? 2 : 3 ),
            //                            Hour = (data.Hour.Value + (j+1)) % 24,
            //                            TS = data.TS.Value + ((j+1)*3600 - data.Time.Value),
            //                            TimeStamp = data.TimeStamp.Value.AddSeconds(3600 - data.Time.Value + 0.5).AddHours(j),
            //                            CNT = 0,
            //                            CT = Math.Min(data.CT.Value - (j + 1) * 3600, 3600),
            //                            Time = 0,
            //                            Type = data.Type.Trim(),
            //                            Status = data.Status.Trim() == "" ? "Gépállás" : data.Status.Trim(),
            //                            Request = 0,
            //                            OEE = 0,
            //                        };
            //                        string s1 = data.Status.Trim();
            //                        if (s1.Length * 16 > Math.Min(firstbreak.CT.Value,3600) - firstbreak.Time.Value - 48)
            //                        {
            //                            if (s1.Substring(0, 2).ToUpper() == "ST")
            //                            {
            //                                firstbreak.Type = s1.Split(" - ")[0].Trim();// + "...";
            //                            }
            //                            else
            //                            {
            //                                firstbreak.Type = s1.Substring(0, Math.Min(s1.Length, Math.Max((Math.Min(firstbreak.CT.Value, 3600) - firstbreak.Time.Value - 48) / 16, 1))).Trim() + "...";
            //                            }
            //                        }
            //                        else
            //                        {
            //                            firstbreak.Type = s1;
            //                        }
            //                        viewlinesred.Add(firstbreak);
            //                        viewlinestext.Add(firstbreak);
            //                    }
            //                }
            //                lasttime = data.CT.Value > 3600 ? data.CT.Value % 3600 : data.CT.Value;
            //            }
            //            break;
            //    }
            //}
            //Dialy datas for line
            for (int i = 0; i < viewShiftViewResultsLine.Count(); i++)
            {
                ViewShiftViewResult data = viewShiftViewResultsLine[i];
                //Running
                switch (data.Status.Trim())
                {
                    case "Normal":
                        viewlinesgreenline.Add(data);
                        if (data.NOK.HasValue)
                        {
                            if (!data.NOK.Value)
                            {
                                viewlinespointline.Add(data);
                            }
                        }
                        else
                        {
                            if (data != null)
                            {
                                viewlinespointline.Add(data);
                            }
                        }
                        if (data.CT.Value > 3600)
                        {
                            for (int j = 0; j < (int)(data.CT.Value / 3600); j++)
                            {
                                DateTime hour = data.TimeStamp.Value.AddSeconds(3600 - data.Time.Value + 0.5).AddHours(j);
                                ViewShiftViewResult firstbreak = new ViewShiftViewResult()
                                {
                                    LineID = LineID,
                                    StartTime = dateTimeFrom,
                                    Muszak = Math.Min(lastshift, ((hour.TimeOfDay.Hours == 14 & hour.TimeOfDay.Minutes <= 20) | (hour.TimeOfDay.Hours < 14)) & hour.TimeOfDay.Hours >= 6 ? 1 : hour.TimeOfDay.Hours < 6 & (hour.TimeOfDay.Hours > 22 | (hour.TimeOfDay.Hours == 22 & hour.TimeOfDay.Minutes >= 40)) ? 2 : 3),
                                    Hour = (data.Hour.Value + (j + 1)) % 24,
                                    TS = data.TS.Value + ((j + 1) * 3600 - data.Time.Value),
                                    TimeStamp = data.TimeStamp.Value.AddSeconds(3600 - data.Time.Value + 0.5).AddHours(j),
                                    CNT = 0,
                                    CT = Math.Min(data.CT.Value - (j + 1) * 3600, 3600),
                                    Time = 0,
                                    Type = data.Type.Trim(),
                                    Status = data.Status.Trim() == "" ? "Gépállás" : data.Status.Trim(),
                                    Request = 0,
                                    OEE = 0,
                                };
                                string s1 = data.Status.Trim();
                                if (s1.Length * 16 > Math.Min(firstbreak.CT.Value, 3600) - firstbreak.Time.Value - 48)
                                {
                                    if (s1.Substring(0, 2).ToUpper() == "ST")
                                    {
                                        firstbreak.Type = s1.Split(" - ")[0].Trim();// + "...";
                                    }
                                    else
                                    {
                                        firstbreak.Type = s1.Substring(0, Math.Min(s1.Length, Math.Max((Math.Min(firstbreak.CT.Value, 3600) - firstbreak.Time.Value - 48) / 16, 1))).Trim() + "...";
                                    }
                                }
                                else
                                {
                                    firstbreak.Type = s1;
                                }
                                viewlinesredline.Add(firstbreak);
                            }
                        }
                        lasttime = data.CT.Value > 3600 ? data.CT.Value % 3600 : data.CT.Value;
                        break;
                    case "Slow":
                        viewlinesyellowline.Add(data);
                        if (data.NOK.HasValue)
                        {
                            if (!data.NOK.Value)
                            {
                                viewlinespointline.Add(data);
                            }
                        }
                        else
                        {
                            if (data != null)
                            {
                                viewlinespointline.Add(data);
                            }
                        }
                        if (data.CT.Value > 3600)
                        {
                            for (int j = 0; j < (int)(data.CT.Value / 3600); j++)
                            {
                                DateTime hour = data.TimeStamp.Value.AddSeconds(3600 - data.Time.Value + 0.5).AddHours(j);
                                ViewShiftViewResult firstbreak = new ViewShiftViewResult()
                                {
                                    LineID = LineID,
                                    StartTime = dateTimeFrom,
                                    Muszak = Math.Min(lastshift, ((hour.TimeOfDay.Hours == 14 & hour.TimeOfDay.Minutes <= 20) | (hour.TimeOfDay.Hours < 14)) & hour.TimeOfDay.Hours >= 6 ? 1 : hour.TimeOfDay.Hours < 6 & (hour.TimeOfDay.Hours > 22 | (hour.TimeOfDay.Hours == 22 & hour.TimeOfDay.Minutes >= 40)) ? 2 : 3),
                                    Hour = (data.Hour.Value + (j + 1)) % 24,
                                    TS = data.TS.Value + ((j + 1) * 3600 - data.Time.Value),
                                    TimeStamp = data.TimeStamp.Value.AddSeconds(3600 - data.Time.Value + 0.5).AddHours(j),
                                    CNT = 0,
                                    CT = Math.Min(data.CT.Value - (j + 1) * 3600, 3600),
                                    Time = 0,
                                    Type = data.Type.Trim(),
                                    Status = data.Status.Trim() == "" ? "Gépállás" : data.Status.Trim(),
                                    Request = 0,
                                    OEE = 0,
                                };
                                string s1 = data.Status.Trim();
                                if (s1.Length * 16 > Math.Min(firstbreak.CT.Value, 3600) - firstbreak.Time.Value - 48)
                                {
                                    if (s1.Substring(0, 2).ToUpper() == "ST")
                                    {
                                        firstbreak.Type = s1.Split(" - ")[0].Trim();// + "...";
                                    }
                                    else
                                    {
                                        firstbreak.Type = s1.Substring(0, Math.Min(s1.Length, Math.Max((Math.Min(firstbreak.CT.Value, 3600) - firstbreak.Time.Value - 48) / 16, 1))).Trim() + "...";
                                    }
                                }
                                else
                                {
                                    firstbreak.Type = s1;
                                }
                                viewlinesredline.Add(firstbreak);
                            }
                        }
                        lasttime = data.CT.Value > 3600 ? data.CT.Value % 3600 : data.CT.Value;
                        break;
                    case "MicroStop":
                    case "DownTime":
                    case "Alert1":
                    case "Alert2":
                    case "Alert3":
                        viewlinesredline.Add(data);
                        if (data.NOK.HasValue)
                        {
                            if (!data.NOK.Value)
                            {
                                viewlinespointline.Add(data);
                            }
                        }
                        else
                        {
                            if (data != null)
                            {
                                viewlinespointline.Add(data);
                            }
                        }
                        if (data.CT.Value > 3600)
                        {
                            for (int j = 0; j < (int)(data.CT.Value / 3600); j++)
                            {
                                DateTime hour = data.TimeStamp.Value.AddSeconds(3600 - data.Time.Value + 0.5).AddHours(j);
                                ViewShiftViewResult firstbreak = new ViewShiftViewResult()
                                {
                                    LineID = LineID,
                                    StartTime = dateTimeFrom,
                                    Muszak = Math.Min(lastshift, ((hour.TimeOfDay.Hours == 14 & hour.TimeOfDay.Minutes <= 20) | (hour.TimeOfDay.Hours < 14)) & hour.TimeOfDay.Hours >= 6 ? 1 : hour.TimeOfDay.Hours < 6 & (hour.TimeOfDay.Hours > 22 | (hour.TimeOfDay.Hours == 22 & hour.TimeOfDay.Minutes >= 40)) ? 2 : 3),
                                    Hour = (data.Hour.Value + (j + 1)) % 24,
                                    TS = data.TS.Value + ((j + 1) * 3600 - data.Time.Value),
                                    TimeStamp = data.TimeStamp.Value.AddSeconds(3600 - data.Time.Value + 0.5).AddHours(j),
                                    CNT = 0,
                                    CT = Math.Min(data.CT.Value - (j + 1) * 3600, 3600),
                                    Time = 0,
                                    Type = data.Type.Trim(),
                                    Status = data.Status.Trim() == "" ? "Gépállás" : data.Status.Trim(),
                                    Request = 0,
                                    OEE = 0,
                                };
                                string s1 = data.Status.Trim();
                                if (s1.Length * 16 > Math.Min(firstbreak.CT.Value, 3600) - firstbreak.Time.Value - 48)
                                {
                                    if (s1.Substring(0, 2).ToUpper() == "ST")
                                    {
                                        firstbreak.Type = s1.Split(" - ")[0].Trim();// + "...";
                                    }
                                    else
                                    {
                                        firstbreak.Type = s1.Substring(0, Math.Min(s1.Length, Math.Max((Math.Min(firstbreak.CT.Value, 3600) - firstbreak.Time.Value - 48) / 16, 1))).Trim() + "...";
                                    }
                                }
                                else
                                {
                                    firstbreak.Type = s1;
                                }
                                viewlinesredline.Add(firstbreak);
                            }
                        }
                        lasttime = data.CT.Value > 3600 ? data.CT.Value % 3600 : data.CT.Value;
                        break;
                    default:
                        if (data.Type.Trim() == "DT")
                        {
                            string s = data.Status.Trim();
                            if (s.Length * 16 > (Math.Min(data.CT.Value, 3600) - data.Time.Value - 48))
                            {
                                if (s.Substring(0, 2).ToUpper() == "ST")
                                {
                                    data.Type = s.Split(" - ")[0].Trim();// + "...";
                                }
                                else
                                {
                                    data.Type = s.Substring(0, Math.Min(s.Length, Math.Max((Math.Min(data.CT.Value, 3600) - data.Time.Value) / 16, 1))).Trim() + "...";
                                }
                            }
                            else
                            {
                                data.Type = s;
                            }
                            viewlinesredline.Add(data);
                            viewlinestextline.Add(data);
                            if (data.CT.Value > 3600)
                            {
                                for (int j = 0; j < (int)(data.CT.Value / 3600); j++)
                                {
                                    DateTime hour = data.TimeStamp.Value.AddSeconds(3600 - data.Time.Value + 0.5).AddHours(j);
                                    ViewShiftViewResult firstbreak = new ViewShiftViewResult()
                                    {
                                        LineID = LineID,
                                        StartTime = dateTimeFrom,
                                        Muszak = Math.Min(lastshift, ((hour.TimeOfDay.Hours == 14 & hour.TimeOfDay.Minutes <= 20) | (hour.TimeOfDay.Hours < 14)) & hour.TimeOfDay.Hours >= 6 ? 1 : hour.TimeOfDay.Hours < 6 & (hour.TimeOfDay.Hours > 22 | (hour.TimeOfDay.Hours == 22 & hour.TimeOfDay.Minutes >= 40)) ? 2 : 3),
                                        Hour = (data.Hour.Value + (j + 1)) % 24,
                                        TS = data.TS.Value + ((j + 1) * 3600 - data.Time.Value),
                                        TimeStamp = data.TimeStamp.Value.AddSeconds(3600 - data.Time.Value + 0.5).AddHours(j),
                                        CNT = 0,
                                        CT = Math.Min(data.CT.Value - (j + 1) * 3600, 3600),
                                        Time = 0,
                                        Type = data.Type.Trim(),
                                        Status = data.Status.Trim() == "" ? "Gépállás" : data.Status.Trim(),
                                        Request = 0,
                                        OEE = 0,
                                    };
                                    string s1 = data.Status.Trim();
                                    if (s1.Length * 16 > Math.Min(firstbreak.CT.Value, 3600) - firstbreak.Time.Value - 48)
                                    {
                                        if (s1.Substring(0, 2).ToUpper() == "ST")
                                        {
                                            firstbreak.Type = s1.Split(" - ")[0].Trim();// + "...";
                                        }
                                        else
                                        {
                                            firstbreak.Type = s1.Substring(0, Math.Min(s1.Length, Math.Max((Math.Min(firstbreak.CT.Value, 3600) - firstbreak.Time.Value - 48) / 16, 1))).Trim() + "...";
                                        }
                                    }
                                    else
                                    {
                                        firstbreak.Type = s1;
                                    }
                                    viewlinesredline.Add(firstbreak);
                                    viewlinestextline.Add(firstbreak);
                                }
                            }
                            lasttime = data.CT.Value > 3600 ? data.CT.Value % 3600 : data.CT.Value;
                        }
                        break;
                }
            }
            ViewData["sortedid"] = viewlines;
            //ViewData["sortedidpoint"] = viewlinespoint;
            ViewData["sortedidpointline"] = viewlinespointline;
            //ViewData["sortedidgreen"] = viewlinesgreen;
            ViewData["sortedidgreenline"] = viewlinesgreenline;
            //ViewData["sortedidyellow"] = viewlinesyellow;
            ViewData["sortedidyellowline"] = viewlinesyellowline;
            //ViewData["sortedidred"] = viewlinesred;
            ViewData["sortedidredline"] = viewlinesredline;
            //ViewData["sortedidtext"] = viewlinestext;
            ViewData["sortedidtextline"] = viewlinestextline;
            ViewData["sortedidgray"] = viewlinesgray;
            var lastvalue = viewShiftViewResultsLine.Where(a => (a.Type != "PDT")).FirstOrDefault();
            var lastvalueline = viewShiftViewResultsLine.Where(a => (a.Type != "PDT")).FirstOrDefault();
            ViewData["lastvalue"] = lastvalue;
            if (viewShiftViewResultsLine.Where(a => (a.Type != "PDT")).Count() > 0)
            {
                lastvalue = viewShiftViewResultsLine.Where(a => (a.Type != "PDT")).Last();
                lastvalueline = lastvalue;
                if (viewShiftViewResultsLine.Where(a => (a.Type != "PDT")).Count() > 0)
                {
                    lastvalueline = viewShiftViewResultsLine.Where(a => (a.Type != "PDT")).Last();
                }
                ViewData["lastvalue"] = lastvalue.TimeStamp.Value.AddSeconds(lastvalue.CT.Value) >= lastvalueline.TimeStamp.Value.AddSeconds(lastvalueline.CT.Value) ? lastvalue : lastvalueline;
            }
            ViewData["shift"] = shift;
            ViewData["stationid"] = stationid;
            ViewData["stationidline"] = stationidline;
            return View(viewheader);
        }
    }
}

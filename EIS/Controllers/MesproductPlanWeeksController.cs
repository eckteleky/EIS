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
using System.Globalization;
using Microsoft.VisualBasic;
using System.Data;
using Microsoft.IdentityModel.Tokens;

namespace EIS.Controllers
{
    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }
    }

    public class MesproductPlanWeeksController : Controller
    {
        private readonly EISDBContext _context;

        public MesproductPlanWeeksController(EISDBContext context)
        {
            _context = context;
        }

        // GET: MesproductPlanWeeks
        public async Task<IActionResult> Index(int? Year, int? Week)
        {
            HttpContext.Session.SetString("Submenu", "1");
            HttpContext.Session.SetInt32("LZGroup", 0);
            HttpContext.Session.SetInt32("LineIDGroup", 1);
            HttpContext.Session.SetInt32("StationGroup", 0);
            HttpContext.Session.SetInt32("ShiftGroup", 0);
            HttpContext.Session.SetInt32("DayGroup", 0);
            //HttpContext.Session.SetInt32("WeekGroup", 1);
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
            int year = DateTime.Now.Year;
            if (HttpContext.Session.GetInt32("Year").HasValue)
            {
                year = HttpContext.Session.GetInt32("Year").Value;
            }
            if (Year.HasValue)
            {
                year = Year.Value;
            }
            CultureInfo myCI = new CultureInfo("en-US");
            Calendar myCal = myCI.Calendar;
            DateTime date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, myCal);

            int week = myCal.GetWeekOfYear(date, System.Globalization.CalendarWeekRule.FirstFullWeek, DayOfWeek.Sunday);
            if (HttpContext.Session.GetInt32("Week").HasValue)
            {
                week = HttpContext.Session.GetInt32("Week").Value;
                if (week > ISOWeek.GetWeeksInYear(year))
                {
                    year++;
                    week = 1;
                }
                if (week < 1)
                {
                    year--;
                    week = ISOWeek.GetWeeksInYear(year);
                }
            }
            if (Week.HasValue)
            {
                if (Week > ISOWeek.GetWeeksInYear(year))
                {
                    year++;
                    Week = 1;
                }
                if (Week < 1)
                {
                    year--;
                    Week = ISOWeek.GetWeeksInYear(year);
                }
                week = Week.Value;
            }

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

            if (HttpContext.Session.GetString("LineID") == null |
                HttpContext.Session.GetInt32("Company") == null)
            {
                Headers header = new()
                {
                    MyFields = false,
                    SearchString = ""
                };
                SessionHelper.SetObjectAsJson(HttpContext.Session, "header", header);
            }
            HttpContext.Session.SetString("LineID", LineID);

            HttpContext.Session.SetInt32("Company", company);

            HttpContext.Session.SetInt32("Year", year);

            HttpContext.Session.SetInt32("Week", week);
            #endregion
            bool newflag = false;
            MesproductPlanWeekTemp mesproductPlanWeekTemp = await _context.MesproductPlanWeekTemp.Where(a => a.LineId == LineID & a.Year == year & a.Week == week).FirstOrDefaultAsync();
            if (mesproductPlanWeekTemp == null)
            {
                int workerid = 0;
                Mesworker mw = await _context.Mesworker.Where(a => a.LineId == LineID).OrderBy(b => b.WorkerName).FirstOrDefaultAsync();
                if (mw != null)
                {
                    workerid = mw.Id;
                    DateTime dt = FirstDateOfWeekIso8601(year, week).AddHours(6);
                    MesproductPlanWeekTemp mesproductPlanWeekTempLast = await _context.MesproductPlanWeekTemp.Where(a => a.LineId == LineID & a.Newflag.HasValue == false).OrderBy(b=>b.LineId).LastOrDefaultAsync();
                    if (mesproductPlanWeekTempLast != null)
                    {
                        mesproductPlanWeekTemp = new MesproductPlanWeekTemp
                        {
                            Id = 0,
                            LineId = LineID,
                            Year = year,
                            Week = week,
                            StartTime = dt,
                            WorkerId11 = mesproductPlanWeekTempLast.WorkerId11,
                            WorkerId12 = mesproductPlanWeekTempLast.WorkerId12,
                            WorkerId21 = mesproductPlanWeekTempLast.WorkerId21,
                            WorkerId22 = mesproductPlanWeekTempLast.WorkerId22,
                            WorkerId31 = mesproductPlanWeekTempLast.WorkerId31,
                            WorkerId32 = mesproductPlanWeekTempLast.WorkerId32,
                            Active01 = mesproductPlanWeekTempLast.Active01,
                            Active02 = mesproductPlanWeekTempLast.Active02,
                            Active03 = mesproductPlanWeekTempLast.Active03,
                            Active11 = mesproductPlanWeekTempLast.Active11,
                            Active12 = mesproductPlanWeekTempLast.Active12,
                            Active13 = mesproductPlanWeekTempLast.Active13,
                            Active21 = mesproductPlanWeekTempLast.Active21,
                            Active22 = mesproductPlanWeekTempLast.Active22,
                            Active23 = mesproductPlanWeekTempLast.Active23,
                            Active31 = mesproductPlanWeekTempLast.Active31,
                            Active32 = mesproductPlanWeekTempLast.Active32,
                            Active33 = mesproductPlanWeekTempLast.Active33,
                            Active41 = mesproductPlanWeekTempLast.Active41,
                            Active42 = mesproductPlanWeekTempLast.Active42,
                            Active43 = mesproductPlanWeekTempLast.Active43,
                            Active51 = mesproductPlanWeekTempLast.Active51,
                            Active52 = mesproductPlanWeekTempLast.Active52,
                            Active53 = mesproductPlanWeekTempLast.Active53,
                            Active61 = mesproductPlanWeekTempLast.Active61,
                            Active62 = mesproductPlanWeekTempLast.Active62,
                            Active63 = mesproductPlanWeekTempLast.Active63,
                            Newflag = true,
                        };
                    }
                    else
                    {
                        mesproductPlanWeekTemp = new MesproductPlanWeekTemp
                        {
                            Id = 0,
                            LineId = LineID,
                            Year = year,
                            Week = week,
                            StartTime = dt,
                            WorkerId11 = workerid,
                            WorkerId12 = workerid,
                            WorkerId21 = workerid,
                            WorkerId22 = workerid,
                            WorkerId31 = workerid,
                            WorkerId32 = workerid,
                            Active01 = false,
                            Active02 = false,
                            Active03 = false,
                            Active11 = false,
                            Active12 = false,
                            Active13 = false,
                            Active21 = false,
                            Active22 = false,
                            Active23 = false,
                            Active31 = false,
                            Active32 = false,
                            Active33 = false,
                            Active41 = false,
                            Active42 = false,
                            Active43 = false,
                            Active51 = false,
                            Active52 = false,
                            Active53 = false,
                            Active61 = false,
                            Active62 = false,
                            Active63 = false,
                            Newflag = true,
                        };
                    }
                    newflag = true;
                    try
                    {
                        _context.Add(mesproductPlanWeekTemp);
                    }
                    catch
                    { }
                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch
                    { }
                }
            }
            var eISDBContext = await _context.MesproductPlanWeek.Where(a => a.LineId == LineID & a.Year == year & a.Week == week).ToListAsync();
            if (eISDBContext.Count() == 0)
            {
                int workerid = 0;
                Mesworker mw = await _context.Mesworker.Where(a => a.LineId == LineID).OrderBy(b => b.WorkerName).FirstOrDefaultAsync();
                if (mw != null)
                {
                    workerid = mw.Id;
                    DateTime dt = FirstDateOfWeekIso8601(year, week).AddHours(6);
                    //ViewData["ST"] = dt;
                    MesproductPlanWeek mesproductPlanWeek = await _context.MesproductPlanWeek.Where(a => a.LineId == LineID & a.Year == year & a.Week == week).FirstOrDefaultAsync();
                    if (mesproductPlanWeek == null)
                    {
                        mesproductPlanWeek = new MesproductPlanWeek
                        {
                            Id = 0,
                            LineId = LineID,
                            Year = year,
                            Week = week,
                            StartTime = dt,
                            Mtype01 = 1,
                            Mtype02 = 1,
                            Mtype03 = 1,
                            Mtype11 = 1,
                            Mtype12 = 1,
                            Mtype13 = 1,
                            Mtype21 = 1,
                            Mtype22 = 1,
                            Mtype23 = 1,
                            Mtype31 = 1,
                            Mtype32 = 1,
                            Mtype33 = 1,
                            Mtype41 = 1,
                            Mtype42 = 1,
                            Mtype43 = 1,
                            Mtype51 = 1,
                            Mtype52 = 1,
                            Mtype53 = 1,
                            Mtype61 = 1,
                            Mtype62 = 1,
                            Mtype63 = 1,
                            WorkerId011 = workerid,
                            WorkerId012 = workerid,
                            WorkerId021 = workerid,
                            WorkerId022 = workerid,
                            WorkerId031 = workerid,
                            WorkerId032 = workerid,
                            WorkerId111 = workerid,
                            WorkerId112 = workerid,
                            WorkerId121 = workerid,
                            WorkerId122 = workerid,
                            WorkerId131 = workerid,
                            WorkerId132 = workerid,
                            WorkerId211 = workerid,
                            WorkerId212 = workerid,
                            WorkerId221 = workerid,
                            WorkerId222 = workerid,
                            WorkerId231 = workerid,
                            WorkerId232 = workerid,
                            WorkerId311 = workerid,
                            WorkerId312 = workerid,
                            WorkerId321 = workerid,
                            WorkerId322 = workerid,
                            WorkerId331 = workerid,
                            WorkerId332 = workerid,
                            WorkerId411 = workerid,
                            WorkerId412 = workerid,
                            WorkerId421 = workerid,
                            WorkerId422 = workerid,
                            WorkerId431 = workerid,
                            WorkerId432 = workerid,
                            WorkerId511 = workerid,
                            WorkerId512 = workerid,
                            WorkerId521 = workerid,
                            WorkerId522 = workerid,
                            WorkerId531 = workerid,
                            WorkerId532 = workerid,
                            WorkerId611 = workerid,
                            WorkerId612 = workerid,
                            WorkerId621 = workerid,
                            WorkerId622 = workerid,
                            WorkerId631 = workerid,
                            WorkerId632 = workerid,
                            Newflag = true,
                        };
                        newflag = true;
                        try
                        {
                            _context.Add(mesproductPlanWeek);
                        }
                        catch
                        { }
                        try
                        {
                            await _context.SaveChangesAsync();
                        }
                        catch
                        { }
                        eISDBContext = await _context.MesproductPlanWeek.Where(a => a.LineId == LineID & a.Year == year & a.Week == week).ToListAsync();
                    }
                }
            }
            ViewData["LineID"] = LineID;
            ViewData["Year"] = year;
            ViewData["Week"] = week;
            ViewData["Test"] = newflag ? "+" : "";
            HttpContext.Session.SetInt32("AddGroup", eISDBContext.Count() > 0 ? (eISDBContext[0].Newflag.HasValue ? (eISDBContext[0].Newflag.Value ? 1 : 0) : 1) : 0);
            if (eISDBContext.Count() > 0)
            {
                MesproductPlanWeek mesproductPlanWeek = await _context.MesproductPlanWeek.Where(a => a.LineId == LineID & a.Year == year & a.Week == week).FirstOrDefaultAsync();
                if (mesproductPlanWeek != null)
                {
                    //bool modifyflag = false;
                    //if (!mesproductPlanWeek.Mtype01.HasValue) { mesproductPlanWeek.Mtype01 = 1; modifyflag = true; }
                    //if (!mesproductPlanWeek.Mtype02.HasValue) { mesproductPlanWeek.Mtype02 = 1; modifyflag = true; }
                    //if (!mesproductPlanWeek.Mtype03.HasValue) { mesproductPlanWeek.Mtype03 = 1; modifyflag = true; }
                    //if (!mesproductPlanWeek.Mtype11.HasValue) { mesproductPlanWeek.Mtype11 = 1; modifyflag = true; }
                    //if (!mesproductPlanWeek.Mtype12.HasValue) { mesproductPlanWeek.Mtype12 = 1; modifyflag = true; }
                    //if (!mesproductPlanWeek.Mtype13.HasValue) { mesproductPlanWeek.Mtype13 = 1; modifyflag = true; }
                    //if (!mesproductPlanWeek.Mtype21.HasValue) { mesproductPlanWeek.Mtype21 = 1; modifyflag = true; }
                    //if (!mesproductPlanWeek.Mtype22.HasValue) { mesproductPlanWeek.Mtype22 = 1; modifyflag = true; }
                    //if (!mesproductPlanWeek.Mtype23.HasValue) { mesproductPlanWeek.Mtype23 = 1; modifyflag = true; }
                    //if (!mesproductPlanWeek.Mtype31.HasValue) { mesproductPlanWeek.Mtype31 = 1; modifyflag = true; }
                    //if (!mesproductPlanWeek.Mtype32.HasValue) { mesproductPlanWeek.Mtype32 = 1; modifyflag = true; }
                    //if (!mesproductPlanWeek.Mtype33.HasValue) { mesproductPlanWeek.Mtype33 = 1; modifyflag = true; }
                    //if (!mesproductPlanWeek.Mtype41.HasValue) { mesproductPlanWeek.Mtype41 = 1; modifyflag = true; }
                    //if (!mesproductPlanWeek.Mtype42.HasValue) { mesproductPlanWeek.Mtype42 = 1; modifyflag = true; }
                    //if (!mesproductPlanWeek.Mtype43.HasValue) { mesproductPlanWeek.Mtype43 = 1; modifyflag = true; }
                    //if (!mesproductPlanWeek.Mtype51.HasValue) { mesproductPlanWeek.Mtype51 = 1; modifyflag = true; }
                    //if (!mesproductPlanWeek.Mtype52.HasValue) { mesproductPlanWeek.Mtype52 = 1; modifyflag = true; }
                    //if (!mesproductPlanWeek.Mtype53.HasValue) { mesproductPlanWeek.Mtype53 = 1; modifyflag = true; }
                    //if (!mesproductPlanWeek.Mtype61.HasValue) { mesproductPlanWeek.Mtype61 = 1; modifyflag = true; }
                    //if (!mesproductPlanWeek.Mtype62.HasValue) { mesproductPlanWeek.Mtype62 = 1; modifyflag = true; }
                    //if (!mesproductPlanWeek.Mtype63.HasValue) { mesproductPlanWeek.Mtype63 = 1; modifyflag = true; }
                    //if (modifyflag)
                    //{
                    //    try
                    //    {
                    //        _context.Add(mesproductPlanWeek);
                    //    }
                    //    catch
                    //    { }
                    //    try
                    //    {
                    //        await _context.SaveChangesAsync();
                    //    }
                    //    catch
                    //    { }
                    //    mesproductPlanWeek = await _context.MesproductPlanWeek.Where(a => a.LineId == LineID & a.Year == year & a.Week == week).FirstOrDefaultAsync();
                    //}
                    ViewData["Mtype01"] = new SelectList(_context.MesproductPlanType.Where(a => a.Id == mesproductPlanWeek.Mtype01), "Id", "Description", mesproductPlanWeek.Mtype01).FirstOrDefault().Text.ToString();
                    ViewData["Mtype02"] = new SelectList(_context.MesproductPlanType.Where(a => a.Id == mesproductPlanWeek.Mtype02), "Id", "Description", mesproductPlanWeek.Mtype02).FirstOrDefault().Text.ToString();
                    ViewData["Mtype03"] = new SelectList(_context.MesproductPlanType.Where(a => a.Id == mesproductPlanWeek.Mtype03), "Id", "Description", mesproductPlanWeek.Mtype03).FirstOrDefault().Text.ToString();
                    ViewData["Mtype11"] = new SelectList(_context.MesproductPlanType.Where(a => a.Id == mesproductPlanWeek.Mtype11), "Id", "Description", mesproductPlanWeek.Mtype11).FirstOrDefault().Text.ToString();
                    ViewData["Mtype12"] = new SelectList(_context.MesproductPlanType.Where(a => a.Id == mesproductPlanWeek.Mtype12), "Id", "Description", mesproductPlanWeek.Mtype12).FirstOrDefault().Text.ToString();
                    ViewData["Mtype13"] = new SelectList(_context.MesproductPlanType.Where(a => a.Id == mesproductPlanWeek.Mtype13), "Id", "Description", mesproductPlanWeek.Mtype13).FirstOrDefault().Text.ToString();
                    ViewData["Mtype21"] = new SelectList(_context.MesproductPlanType.Where(a => a.Id == mesproductPlanWeek.Mtype21), "Id", "Description", mesproductPlanWeek.Mtype21).FirstOrDefault().Text.ToString();
                    ViewData["Mtype22"] = new SelectList(_context.MesproductPlanType.Where(a => a.Id == mesproductPlanWeek.Mtype22), "Id", "Description", mesproductPlanWeek.Mtype22).FirstOrDefault().Text.ToString();
                    ViewData["Mtype23"] = new SelectList(_context.MesproductPlanType.Where(a => a.Id == mesproductPlanWeek.Mtype23), "Id", "Description", mesproductPlanWeek.Mtype23).FirstOrDefault().Text.ToString();
                    ViewData["Mtype31"] = new SelectList(_context.MesproductPlanType.Where(a => a.Id == mesproductPlanWeek.Mtype31), "Id", "Description", mesproductPlanWeek.Mtype31).FirstOrDefault().Text.ToString();
                    ViewData["Mtype32"] = new SelectList(_context.MesproductPlanType.Where(a => a.Id == mesproductPlanWeek.Mtype32), "Id", "Description", mesproductPlanWeek.Mtype32).FirstOrDefault().Text.ToString();
                    ViewData["Mtype33"] = new SelectList(_context.MesproductPlanType.Where(a => a.Id == mesproductPlanWeek.Mtype33), "Id", "Description", mesproductPlanWeek.Mtype33).FirstOrDefault().Text.ToString();
                    ViewData["Mtype41"] = new SelectList(_context.MesproductPlanType.Where(a => a.Id == mesproductPlanWeek.Mtype41), "Id", "Description", mesproductPlanWeek.Mtype41).FirstOrDefault().Text.ToString();
                    ViewData["Mtype42"] = new SelectList(_context.MesproductPlanType.Where(a => a.Id == mesproductPlanWeek.Mtype42), "Id", "Description", mesproductPlanWeek.Mtype42).FirstOrDefault().Text.ToString();
                    ViewData["Mtype43"] = new SelectList(_context.MesproductPlanType.Where(a => a.Id == mesproductPlanWeek.Mtype43), "Id", "Description", mesproductPlanWeek.Mtype43).FirstOrDefault().Text.ToString();
                    ViewData["Mtype51"] = new SelectList(_context.MesproductPlanType.Where(a => a.Id == mesproductPlanWeek.Mtype51), "Id", "Description", mesproductPlanWeek.Mtype51).FirstOrDefault().Text.ToString();
                    ViewData["Mtype52"] = new SelectList(_context.MesproductPlanType.Where(a => a.Id == mesproductPlanWeek.Mtype52), "Id", "Description", mesproductPlanWeek.Mtype52).FirstOrDefault().Text.ToString();
                    ViewData["Mtype53"] = new SelectList(_context.MesproductPlanType.Where(a => a.Id == mesproductPlanWeek.Mtype53), "Id", "Description", mesproductPlanWeek.Mtype53).FirstOrDefault().Text.ToString();
                    ViewData["Mtype61"] = new SelectList(_context.MesproductPlanType.Where(a => a.Id == mesproductPlanWeek.Mtype61), "Id", "Description", mesproductPlanWeek.Mtype61).FirstOrDefault().Text.ToString();
                    ViewData["Mtype62"] = new SelectList(_context.MesproductPlanType.Where(a => a.Id == mesproductPlanWeek.Mtype62), "Id", "Description", mesproductPlanWeek.Mtype62).FirstOrDefault().Text.ToString();
                    ViewData["Mtype63"] = new SelectList(_context.MesproductPlanType.Where(a => a.Id == mesproductPlanWeek.Mtype63), "Id", "Description", mesproductPlanWeek.Mtype63).FirstOrDefault().Text.ToString();
                    ViewData["WorkerId011"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId & a.Id == mesproductPlanWeek.WorkerId011).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId011).FirstOrDefault().Text.ToString();
                    ViewData["WorkerId012"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId & a.Id == mesproductPlanWeek.WorkerId012).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId012).FirstOrDefault().Text.ToString();
                    ViewData["WorkerId021"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId & a.Id == mesproductPlanWeek.WorkerId021).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId021).FirstOrDefault().Text.ToString();
                    ViewData["WorkerId022"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId & a.Id == mesproductPlanWeek.WorkerId022).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId022).FirstOrDefault().Text.ToString();
                    ViewData["WorkerId031"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId & a.Id == mesproductPlanWeek.WorkerId031).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId031).FirstOrDefault().Text.ToString();
                    ViewData["WorkerId032"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId & a.Id == mesproductPlanWeek.WorkerId032).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId032).FirstOrDefault().Text.ToString();
                    ViewData["WorkerId111"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId & a.Id == mesproductPlanWeek.WorkerId111).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId111).FirstOrDefault().Text.ToString();
                    ViewData["WorkerId112"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId & a.Id == mesproductPlanWeek.WorkerId112).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId112).FirstOrDefault().Text.ToString();
                    ViewData["WorkerId121"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId & a.Id == mesproductPlanWeek.WorkerId121).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId121).FirstOrDefault().Text.ToString();
                    ViewData["WorkerId122"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId & a.Id == mesproductPlanWeek.WorkerId122).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId122).FirstOrDefault().Text.ToString();
                    ViewData["WorkerId131"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId & a.Id == mesproductPlanWeek.WorkerId131).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId131).FirstOrDefault().Text.ToString();
                    ViewData["WorkerId132"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId & a.Id == mesproductPlanWeek.WorkerId132).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId132).FirstOrDefault().Text.ToString();
                    ViewData["WorkerId211"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId & a.Id == mesproductPlanWeek.WorkerId211).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId211).FirstOrDefault().Text.ToString();
                    ViewData["WorkerId212"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId & a.Id == mesproductPlanWeek.WorkerId212).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId212).FirstOrDefault().Text.ToString();
                    ViewData["WorkerId221"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId & a.Id == mesproductPlanWeek.WorkerId221).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId221).FirstOrDefault().Text.ToString();
                    ViewData["WorkerId222"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId & a.Id == mesproductPlanWeek.WorkerId222).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId222).FirstOrDefault().Text.ToString();
                    ViewData["WorkerId231"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId & a.Id == mesproductPlanWeek.WorkerId231).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId231).FirstOrDefault().Text.ToString();
                    ViewData["WorkerId232"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId & a.Id == mesproductPlanWeek.WorkerId232).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId232).FirstOrDefault().Text.ToString();
                    ViewData["WorkerId311"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId & a.Id == mesproductPlanWeek.WorkerId311).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId311).FirstOrDefault().Text.ToString();
                    ViewData["WorkerId312"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId & a.Id == mesproductPlanWeek.WorkerId312).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId312).FirstOrDefault().Text.ToString();
                    ViewData["WorkerId321"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId & a.Id == mesproductPlanWeek.WorkerId321).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId321).FirstOrDefault().Text.ToString();
                    ViewData["WorkerId322"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId & a.Id == mesproductPlanWeek.WorkerId322).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId322).FirstOrDefault().Text.ToString();
                    ViewData["WorkerId331"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId & a.Id == mesproductPlanWeek.WorkerId331).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId331).FirstOrDefault().Text.ToString();
                    ViewData["WorkerId332"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId & a.Id == mesproductPlanWeek.WorkerId332).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId332).FirstOrDefault().Text.ToString();
                    ViewData["WorkerId411"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId & a.Id == mesproductPlanWeek.WorkerId411).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId411).FirstOrDefault().Text.ToString();
                    ViewData["WorkerId412"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId & a.Id == mesproductPlanWeek.WorkerId412).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId412).FirstOrDefault().Text.ToString();
                    ViewData["WorkerId421"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId & a.Id == mesproductPlanWeek.WorkerId421).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId421).FirstOrDefault().Text.ToString();
                    ViewData["WorkerId422"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId & a.Id == mesproductPlanWeek.WorkerId422).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId422).FirstOrDefault().Text.ToString();
                    ViewData["WorkerId431"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId & a.Id == mesproductPlanWeek.WorkerId431).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId431).FirstOrDefault().Text.ToString();
                    ViewData["WorkerId432"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId & a.Id == mesproductPlanWeek.WorkerId432).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId432).FirstOrDefault().Text.ToString();
                    ViewData["WorkerId511"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId & a.Id == mesproductPlanWeek.WorkerId511).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId511).FirstOrDefault().Text.ToString();
                    ViewData["WorkerId512"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId & a.Id == mesproductPlanWeek.WorkerId512).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId512).FirstOrDefault().Text.ToString();
                    ViewData["WorkerId521"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId & a.Id == mesproductPlanWeek.WorkerId521).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId521).FirstOrDefault().Text.ToString();
                    ViewData["WorkerId522"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId & a.Id == mesproductPlanWeek.WorkerId522).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId522).FirstOrDefault().Text.ToString();
                    ViewData["WorkerId531"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId & a.Id == mesproductPlanWeek.WorkerId531).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId531).FirstOrDefault().Text.ToString();
                    ViewData["WorkerId532"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId & a.Id == mesproductPlanWeek.WorkerId532).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId532).FirstOrDefault().Text.ToString();
                    ViewData["WorkerId611"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId & a.Id == mesproductPlanWeek.WorkerId611).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId611).FirstOrDefault().Text.ToString();
                    ViewData["WorkerId612"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId & a.Id == mesproductPlanWeek.WorkerId612).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId612).FirstOrDefault().Text.ToString();
                    ViewData["WorkerId621"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId & a.Id == mesproductPlanWeek.WorkerId621).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId621).FirstOrDefault().Text.ToString();
                    ViewData["WorkerId622"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId & a.Id == mesproductPlanWeek.WorkerId622).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId622).FirstOrDefault().Text.ToString();
                    ViewData["WorkerId631"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId & a.Id == mesproductPlanWeek.WorkerId631).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId631).FirstOrDefault().Text.ToString();
                    ViewData["WorkerId632"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId & a.Id == mesproductPlanWeek.WorkerId632).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId632).FirstOrDefault().Text.ToString();
                }
            }
            else
            {
                ViewData["Mtype01"] = new SelectList(_context.MesproductPlanType, "Id", "Description").FirstOrDefault().Text.ToString();
                ViewData["Mtype02"] = new SelectList(_context.MesproductPlanType, "Id", "Description").FirstOrDefault().Text.ToString();
                ViewData["Mtype03"] = new SelectList(_context.MesproductPlanType, "Id", "Description").FirstOrDefault().Text.ToString();
                ViewData["Mtype11"] = new SelectList(_context.MesproductPlanType, "Id", "Description").FirstOrDefault().Text.ToString();
                ViewData["Mtype12"] = new SelectList(_context.MesproductPlanType, "Id", "Description").FirstOrDefault().Text.ToString();
                ViewData["Mtype13"] = new SelectList(_context.MesproductPlanType, "Id", "Description").FirstOrDefault().Text.ToString();
                ViewData["Mtype21"] = new SelectList(_context.MesproductPlanType, "Id", "Description").FirstOrDefault().Text.ToString();
                ViewData["Mtype22"] = new SelectList(_context.MesproductPlanType, "Id", "Description").FirstOrDefault().Text.ToString();
                ViewData["Mtype23"] = new SelectList(_context.MesproductPlanType, "Id", "Description").FirstOrDefault().Text.ToString();
                ViewData["Mtype31"] = new SelectList(_context.MesproductPlanType, "Id", "Description").FirstOrDefault().Text.ToString();
                ViewData["Mtype32"] = new SelectList(_context.MesproductPlanType, "Id", "Description").FirstOrDefault().Text.ToString();
                ViewData["Mtype33"] = new SelectList(_context.MesproductPlanType, "Id", "Description").FirstOrDefault().Text.ToString();
                ViewData["Mtype41"] = new SelectList(_context.MesproductPlanType, "Id", "Description").FirstOrDefault().Text.ToString();
                ViewData["Mtype42"] = new SelectList(_context.MesproductPlanType, "Id", "Description").FirstOrDefault().Text.ToString();
                ViewData["Mtype43"] = new SelectList(_context.MesproductPlanType, "Id", "Description").FirstOrDefault().Text.ToString();
                ViewData["Mtype51"] = new SelectList(_context.MesproductPlanType, "Id", "Description").FirstOrDefault().Text.ToString();
                ViewData["Mtype52"] = new SelectList(_context.MesproductPlanType, "Id", "Description").FirstOrDefault().Text.ToString();
                ViewData["Mtype53"] = new SelectList(_context.MesproductPlanType, "Id", "Description").FirstOrDefault().Text.ToString();
                ViewData["Mtype61"] = new SelectList(_context.MesproductPlanType, "Id", "Description").FirstOrDefault().Text.ToString();
                ViewData["Mtype62"] = new SelectList(_context.MesproductPlanType, "Id", "Description").FirstOrDefault().Text.ToString();
                ViewData["Mtype63"] = new SelectList(_context.MesproductPlanType, "Id", "Description").FirstOrDefault().Text.ToString();
                ViewData["WorkerId011"] = new SelectList(_context.Mesworker.Where(a => a.LineId == LineID).OrderBy(b => b.WorkerName), "Id", "WorkerName").FirstOrDefault().Text.ToString();
                ViewData["WorkerId012"] = new SelectList(_context.Mesworker.Where(a => a.LineId == LineID).OrderBy(b => b.WorkerName), "Id", "WorkerName").FirstOrDefault().Text.ToString();
                ViewData["WorkerId021"] = new SelectList(_context.Mesworker.Where(a => a.LineId == LineID).OrderBy(b => b.WorkerName), "Id", "WorkerName").FirstOrDefault().Text.ToString();
                ViewData["WorkerId022"] = new SelectList(_context.Mesworker.Where(a => a.LineId == LineID).OrderBy(b => b.WorkerName), "Id", "WorkerName").FirstOrDefault().Text.ToString();
                ViewData["WorkerId031"] = new SelectList(_context.Mesworker.Where(a => a.LineId == LineID).OrderBy(b => b.WorkerName), "Id", "WorkerName").FirstOrDefault().Text.ToString();
                ViewData["WorkerId032"] = new SelectList(_context.Mesworker.Where(a => a.LineId == LineID).OrderBy(b => b.WorkerName), "Id", "WorkerName").FirstOrDefault().Text.ToString();
                ViewData["WorkerId111"] = new SelectList(_context.Mesworker.Where(a => a.LineId == LineID).OrderBy(b => b.WorkerName), "Id", "WorkerName").FirstOrDefault().Text.ToString();
                ViewData["WorkerId112"] = new SelectList(_context.Mesworker.Where(a => a.LineId == LineID).OrderBy(b => b.WorkerName), "Id", "WorkerName").FirstOrDefault().Text.ToString();
                ViewData["WorkerId121"] = new SelectList(_context.Mesworker.Where(a => a.LineId == LineID).OrderBy(b => b.WorkerName), "Id", "WorkerName").FirstOrDefault().Text.ToString();
                ViewData["WorkerId122"] = new SelectList(_context.Mesworker.Where(a => a.LineId == LineID).OrderBy(b => b.WorkerName), "Id", "WorkerName").FirstOrDefault().Text.ToString();
                ViewData["WorkerId131"] = new SelectList(_context.Mesworker.Where(a => a.LineId == LineID).OrderBy(b => b.WorkerName), "Id", "WorkerName").FirstOrDefault().Text.ToString();
                ViewData["WorkerId132"] = new SelectList(_context.Mesworker.Where(a => a.LineId == LineID).OrderBy(b => b.WorkerName), "Id", "WorkerName").FirstOrDefault().Text.ToString();
                ViewData["WorkerId211"] = new SelectList(_context.Mesworker.Where(a => a.LineId == LineID).OrderBy(b => b.WorkerName), "Id", "WorkerName").FirstOrDefault().Text.ToString();
                ViewData["WorkerId212"] = new SelectList(_context.Mesworker.Where(a => a.LineId == LineID).OrderBy(b => b.WorkerName), "Id", "WorkerName").FirstOrDefault().Text.ToString();
                ViewData["WorkerId221"] = new SelectList(_context.Mesworker.Where(a => a.LineId == LineID).OrderBy(b => b.WorkerName), "Id", "WorkerName").FirstOrDefault().Text.ToString();
                ViewData["WorkerId222"] = new SelectList(_context.Mesworker.Where(a => a.LineId == LineID).OrderBy(b => b.WorkerName), "Id", "WorkerName").FirstOrDefault().Text.ToString();
                ViewData["WorkerId231"] = new SelectList(_context.Mesworker.Where(a => a.LineId == LineID).OrderBy(b => b.WorkerName), "Id", "WorkerName").FirstOrDefault().Text.ToString();
                ViewData["WorkerId232"] = new SelectList(_context.Mesworker.Where(a => a.LineId == LineID).OrderBy(b => b.WorkerName), "Id", "WorkerName").FirstOrDefault().Text.ToString();
                ViewData["WorkerId311"] = new SelectList(_context.Mesworker.Where(a => a.LineId == LineID).OrderBy(b => b.WorkerName), "Id", "WorkerName").FirstOrDefault().Text.ToString();
                ViewData["WorkerId312"] = new SelectList(_context.Mesworker.Where(a => a.LineId == LineID).OrderBy(b => b.WorkerName), "Id", "WorkerName").FirstOrDefault().Text.ToString();
                ViewData["WorkerId321"] = new SelectList(_context.Mesworker.Where(a => a.LineId == LineID).OrderBy(b => b.WorkerName), "Id", "WorkerName").FirstOrDefault().Text.ToString();
                ViewData["WorkerId322"] = new SelectList(_context.Mesworker.Where(a => a.LineId == LineID).OrderBy(b => b.WorkerName), "Id", "WorkerName").FirstOrDefault().Text.ToString();
                ViewData["WorkerId331"] = new SelectList(_context.Mesworker.Where(a => a.LineId == LineID).OrderBy(b => b.WorkerName), "Id", "WorkerName").FirstOrDefault().Text.ToString();
                ViewData["WorkerId332"] = new SelectList(_context.Mesworker.Where(a => a.LineId == LineID).OrderBy(b => b.WorkerName), "Id", "WorkerName").FirstOrDefault().Text.ToString();
                ViewData["WorkerId411"] = new SelectList(_context.Mesworker.Where(a => a.LineId == LineID).OrderBy(b => b.WorkerName), "Id", "WorkerName").FirstOrDefault().Text.ToString();
                ViewData["WorkerId412"] = new SelectList(_context.Mesworker.Where(a => a.LineId == LineID).OrderBy(b => b.WorkerName), "Id", "WorkerName").FirstOrDefault().Text.ToString();
                ViewData["WorkerId421"] = new SelectList(_context.Mesworker.Where(a => a.LineId == LineID).OrderBy(b => b.WorkerName), "Id", "WorkerName").FirstOrDefault().Text.ToString();
                ViewData["WorkerId422"] = new SelectList(_context.Mesworker.Where(a => a.LineId == LineID).OrderBy(b => b.WorkerName), "Id", "WorkerName").FirstOrDefault().Text.ToString();
                ViewData["WorkerId431"] = new SelectList(_context.Mesworker.Where(a => a.LineId == LineID).OrderBy(b => b.WorkerName), "Id", "WorkerName").FirstOrDefault().Text.ToString();
                ViewData["WorkerId432"] = new SelectList(_context.Mesworker.Where(a => a.LineId == LineID).OrderBy(b => b.WorkerName), "Id", "WorkerName").FirstOrDefault().Text.ToString();
                ViewData["WorkerId511"] = new SelectList(_context.Mesworker.Where(a => a.LineId == LineID).OrderBy(b => b.WorkerName), "Id", "WorkerName").FirstOrDefault().Text.ToString();
                ViewData["WorkerId512"] = new SelectList(_context.Mesworker.Where(a => a.LineId == LineID).OrderBy(b => b.WorkerName), "Id", "WorkerName").FirstOrDefault().Text.ToString();
                ViewData["WorkerId521"] = new SelectList(_context.Mesworker.Where(a => a.LineId == LineID).OrderBy(b => b.WorkerName), "Id", "WorkerName").FirstOrDefault().Text.ToString();
                ViewData["WorkerId522"] = new SelectList(_context.Mesworker.Where(a => a.LineId == LineID).OrderBy(b => b.WorkerName), "Id", "WorkerName").FirstOrDefault().Text.ToString();
                ViewData["WorkerId531"] = new SelectList(_context.Mesworker.Where(a => a.LineId == LineID).OrderBy(b => b.WorkerName), "Id", "WorkerName").FirstOrDefault().Text.ToString();
                ViewData["WorkerId532"] = new SelectList(_context.Mesworker.Where(a => a.LineId == LineID).OrderBy(b => b.WorkerName), "Id", "WorkerName").FirstOrDefault().Text.ToString();
                ViewData["WorkerId611"] = new SelectList(_context.Mesworker.Where(a => a.LineId == LineID).OrderBy(b => b.WorkerName), "Id", "WorkerName").FirstOrDefault().Text.ToString();
                ViewData["WorkerId612"] = new SelectList(_context.Mesworker.Where(a => a.LineId == LineID).OrderBy(b => b.WorkerName), "Id", "WorkerName").FirstOrDefault().Text.ToString();
                ViewData["WorkerId621"] = new SelectList(_context.Mesworker.Where(a => a.LineId == LineID).OrderBy(b => b.WorkerName), "Id", "WorkerName").FirstOrDefault().Text.ToString();
                ViewData["WorkerId622"] = new SelectList(_context.Mesworker.Where(a => a.LineId == LineID).OrderBy(b => b.WorkerName), "Id", "WorkerName").FirstOrDefault().Text.ToString();
                ViewData["WorkerId631"] = new SelectList(_context.Mesworker.Where(a => a.LineId == LineID).OrderBy(b => b.WorkerName), "Id", "WorkerName").FirstOrDefault().Text.ToString();
                ViewData["WorkerId632"] = new SelectList(_context.Mesworker.Where(a => a.LineId == LineID).OrderBy(b => b.WorkerName), "Id", "WorkerName").FirstOrDefault().Text.ToString();
            }
            HttpContext.Session.SetInt32("Year", year);
            HttpContext.Session.SetInt32("Week", week);
            ViewData["ST"] = FirstDateOfWeekIso8601(year, week).AddHours(6);
            return View(eISDBContext);
        }

        // GET: MesproductPlanWeeks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MesproductPlanWeek == null)
            {
                return NotFound();
            }

            var mesproductPlanWeek = await _context.MesproductPlanWeek
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mesproductPlanWeek == null)
            {
                return NotFound();
            }

            return View(mesproductPlanWeek);
        }


        // GET: MesproductPlanWeeks/Create
        public async Task<IActionResult> Create(int? Year, int? Week)
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
            int year = DateTime.Now.Year;
            if (HttpContext.Session.GetInt32("Year").HasValue)
            {
                year = HttpContext.Session.GetInt32("Year").Value;
            }
            if (Year.HasValue)
            {
                year = Year.Value;
            }
            CultureInfo myCI = new CultureInfo("en-US");
            Calendar myCal = myCI.Calendar;
            DateTime date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, myCal);

            int week = myCal.GetWeekOfYear(date, System.Globalization.CalendarWeekRule.FirstFullWeek, DayOfWeek.Sunday);
            if (HttpContext.Session.GetInt32("Week").HasValue)
            {
                week = HttpContext.Session.GetInt32("Week").Value;
                if (week > ISOWeek.GetWeeksInYear(year))
                {
                    year++;
                    week = 1;
                }
                if (week < 1)
                {
                    year--;
                    week = ISOWeek.GetWeeksInYear(year);
                }
            }
            if (Week.HasValue)
            {
                if (Week > ISOWeek.GetWeeksInYear(year))
                {
                    year++;
                    Week = 1;
                }
                if (Week < 1)
                {
                    year--;
                    Week = ISOWeek.GetWeeksInYear(year);
                }
                week = Week.Value;
            }

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

            if (HttpContext.Session.GetString("LineID") == null |
                HttpContext.Session.GetInt32("Company") == null)
            {
                Headers header = new()
                {
                    MyFields = false,
                    SearchString = ""
                };
                SessionHelper.SetObjectAsJson(HttpContext.Session, "header", header);
            }
            HttpContext.Session.SetString("LineID", LineID);

            HttpContext.Session.SetInt32("Company", company);
            #endregion
            bool newflag = false;
            MesproductPlanWeek mesproductPlanWeek = await _context.MesproductPlanWeek.Where(a => a.LineId == LineID & a.Year == year & a.Week == week).FirstOrDefaultAsync();
            DateTime dt = FirstDateOfWeekIso8601(year, week).AddHours(6);
            while (newflag)
            {
                mesproductPlanWeek = await _context.MesproductPlanWeek.Where(a => a.LineId == LineID & a.Year == year & a.Week == week).FirstOrDefaultAsync();
                if (mesproductPlanWeek == null)
                {
                    newflag = true;
                    mesproductPlanWeek = new MesproductPlanWeek
                    {
                        Id = 0,
                        LineId = LineID,
                        Year = year,
                        Week = week,
                        StartTime = dt,
                        Newflag = true
                    };
                }
                else
                {
                    week++;
                    if (week > ISOWeek.GetWeeksInYear(year))
                    {
                        year++;
                        week = 1;
                    }
                }
            };
            if (mesproductPlanWeek.LineId != LineID | mesproductPlanWeek.Year != year | mesproductPlanWeek.Week != week | mesproductPlanWeek.StartTime != dt)
            {
                mesproductPlanWeek.LineId = LineID;
                mesproductPlanWeek.Year = year;
                mesproductPlanWeek.Week = week;
                mesproductPlanWeek.StartTime = dt;
            }
            //ViewData["Mtype01"] = new SelectList(_context.MesproductPlanType, "Id", "Description");
            //ViewData["Mtype02"] = new SelectList(_context.MesproductPlanType, "Id", "Description");
            //ViewData["Mtype03"] = new SelectList(_context.MesproductPlanType, "Id", "Description");
            //ViewData["Mtype11"] = new SelectList(_context.MesproductPlanType, "Id", "Description");
            //ViewData["Mtype12"] = new SelectList(_context.MesproductPlanType, "Id", "Description");
            //ViewData["Mtype13"] = new SelectList(_context.MesproductPlanType, "Id", "Description");
            //ViewData["Mtype21"] = new SelectList(_context.MesproductPlanType, "Id", "Description");
            //ViewData["Mtype22"] = new SelectList(_context.MesproductPlanType, "Id", "Description");
            //ViewData["Mtype23"] = new SelectList(_context.MesproductPlanType, "Id", "Description");
            //ViewData["Mtype31"] = new SelectList(_context.MesproductPlanType, "Id", "Description");
            //ViewData["Mtype32"] = new SelectList(_context.MesproductPlanType, "Id", "Description");
            //ViewData["Mtype33"] = new SelectList(_context.MesproductPlanType, "Id", "Description");
            //ViewData["Mtype41"] = new SelectList(_context.MesproductPlanType, "Id", "Description");
            //ViewData["Mtype42"] = new SelectList(_context.MesproductPlanType, "Id", "Description");
            //ViewData["Mtype43"] = new SelectList(_context.MesproductPlanType, "Id", "Description");
            //ViewData["Mtype51"] = new SelectList(_context.MesproductPlanType, "Id", "Description");
            //ViewData["Mtype52"] = new SelectList(_context.MesproductPlanType, "Id", "Description");
            //ViewData["Mtype53"] = new SelectList(_context.MesproductPlanType, "Id", "Description");
            //ViewData["Mtype61"] = new SelectList(_context.MesproductPlanType, "Id", "Description");
            //ViewData["Mtype62"] = new SelectList(_context.MesproductPlanType, "Id", "Description");
            //ViewData["Mtype63"] = new SelectList(_context.MesproductPlanType, "Id", "Description");
            //ViewData["WorkerId011"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName");
            //ViewData["WorkerId012"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName");
            //ViewData["WorkerId021"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName");
            //ViewData["WorkerId022"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName");
            //ViewData["WorkerId031"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName");
            //ViewData["WorkerId032"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName");
            //ViewData["WorkerId111"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName");
            //ViewData["WorkerId112"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName");
            //ViewData["WorkerId121"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName");
            //ViewData["WorkerId122"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName");
            //ViewData["WorkerId131"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName");
            //ViewData["WorkerId132"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName");
            //ViewData["WorkerId211"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName");
            //ViewData["WorkerId212"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName");
            //ViewData["WorkerId221"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName");
            //ViewData["WorkerId222"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName");
            //ViewData["WorkerId231"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName");
            //ViewData["WorkerId232"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName");
            //ViewData["WorkerId311"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName");
            //ViewData["WorkerId312"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName");
            //ViewData["WorkerId321"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName");
            //ViewData["WorkerId322"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName");
            //ViewData["WorkerId331"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName");
            //ViewData["WorkerId332"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName");
            //ViewData["WorkerId411"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName");
            //ViewData["WorkerId412"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName");
            //ViewData["WorkerId421"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName");
            //ViewData["WorkerId422"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName");
            //ViewData["WorkerId431"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName");
            //ViewData["WorkerId432"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName");
            //ViewData["WorkerId511"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName");
            //ViewData["WorkerId512"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName");
            //ViewData["WorkerId521"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName");
            //ViewData["WorkerId522"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName");
            //ViewData["WorkerId531"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName");
            //ViewData["WorkerId532"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName");
            //ViewData["WorkerId611"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName");
            //ViewData["WorkerId612"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName");
            //ViewData["WorkerId621"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName");
            //ViewData["WorkerId622"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName");
            //ViewData["WorkerId631"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName");
            //ViewData["WorkerId632"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName");
            MesproductPlanWeekTemp mesproductPlanWeekTemp = await _context.MesproductPlanWeekTemp.Where(a => a.LineId == LineID & a.Year == year & a.Week == week).FirstOrDefaultAsync();
            //DateTime dt = FirstDateOfWeekIso8601(year, week).AddHours(6);
            newflag = false;
            while (newflag)
            {
                mesproductPlanWeekTemp = await _context.MesproductPlanWeekTemp.Where(a => a.LineId == LineID & a.Year == year & a.Week == week).FirstOrDefaultAsync();
                if (mesproductPlanWeekTemp == null)
                {
                    newflag = true;
                    mesproductPlanWeekTemp = new MesproductPlanWeekTemp
                    {
                        Id = 0,
                        LineId = LineID,
                        Year = year,
                        Week = week,
                        StartTime = dt,
                        Newflag = true
                    };
                }
                else
                {
                    week++;
                    if (week > ISOWeek.GetWeeksInYear(year))
                    {
                        year++;
                        week = 1;
                    }
                }
            };
            if (mesproductPlanWeekTemp.LineId != LineID | mesproductPlanWeekTemp.Year != year | mesproductPlanWeekTemp.Week != week | mesproductPlanWeekTemp.StartTime != dt)
            {
                mesproductPlanWeekTemp.LineId = LineID;
                mesproductPlanWeekTemp.Year = year;
                mesproductPlanWeekTemp.Week = week;
                mesproductPlanWeekTemp.StartTime = dt;
            }
            ViewData["WorkerId11"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeekTemp.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeekTemp.WorkerId11);
            ViewData["WorkerId12"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeekTemp.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeekTemp.WorkerId12);
            ViewData["WorkerId21"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeekTemp.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeekTemp.WorkerId21);
            ViewData["WorkerId22"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeekTemp.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeekTemp.WorkerId22);
            ViewData["WorkerId31"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeekTemp.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeekTemp.WorkerId31);
            ViewData["WorkerId32"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeekTemp.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeekTemp.WorkerId32);
            //if (mesproductPlanWeek.Newflag.HasValue)
            //{
            //    if (mesproductPlanWeek.Newflag.Value)
            //    {
            //        return PartialView("_CreateModalPartion", mesproductPlanWeekTemp);
            //    }
            //    else
            //    {
            //        return RedirectToAction(nameof(Index));
            //    }
            //}
            //else
            //{
            //    return PartialView("_CreateModalPartion", mesproductPlanWeekTemp);
            //}
            HttpContext.Session.SetInt32("Year", year);
            HttpContext.Session.SetInt32("Week", week);
            return PartialView("_CreateModalPartion", mesproductPlanWeekTemp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LineId,Year,Week,Active01,Active11,Active21,Active31,Active41,Active51,Active61,WorkerId11,WorkerId12,Active02,Active12,Active22,Active32,Active42,Active52,Active62,WorkerId21,WorkerId22,Active03,Active13,Active23,Active33,Active43,Active53,Active63,WorkerId31,WorkerId32,StartTime,Newflag")] MesproductPlanWeekTemp mesproductPlanWeekTemp)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    DateTime dt = FirstDateOfWeekIso8601(mesproductPlanWeekTemp.Year, mesproductPlanWeekTemp.Week).AddHours(6);
                    MesproductPlanWeek mesproductPlanWeek = await _context.MesproductPlanWeek.Where(a => a.LineId == mesproductPlanWeekTemp.LineId & a.Year == mesproductPlanWeekTemp.Year & a.Week == mesproductPlanWeekTemp.Week).FirstOrDefaultAsync();
                    if (mesproductPlanWeek!=null)
                    {
                        int workerid = 1;
                        Mesworker mw = await _context.Mesworker.Where(a => a.LineId == mesproductPlanWeekTemp.LineId).OrderBy(b => b.WorkerName).FirstOrDefaultAsync();
                        if (mw != null)
                        {
                            workerid = mw.Id;
                        }
                        //mesproductPlanWeek.StartTime = mesproductPlanWeekTemp.StartTime;
                        mesproductPlanWeek.LineId = mesproductPlanWeekTemp.LineId;
                        mesproductPlanWeek.Year = mesproductPlanWeekTemp.Year;
                        mesproductPlanWeek.Week = mesproductPlanWeekTemp.Week;
                        mesproductPlanWeek.Active01 = mesproductPlanWeekTemp.Active01;
                        mesproductPlanWeek.Active11 = mesproductPlanWeekTemp.Active11;
                        mesproductPlanWeek.Active21 = mesproductPlanWeekTemp.Active21;
                        mesproductPlanWeek.Active31 = mesproductPlanWeekTemp.Active31;
                        mesproductPlanWeek.Active41 = mesproductPlanWeekTemp.Active41;
                        mesproductPlanWeek.Active51 = mesproductPlanWeekTemp.Active51;
                        mesproductPlanWeek.Active61 = mesproductPlanWeekTemp.Active61;
                        mesproductPlanWeek.Active02 = mesproductPlanWeekTemp.Active02;
                        mesproductPlanWeek.Active12 = mesproductPlanWeekTemp.Active12;
                        mesproductPlanWeek.Active22 = mesproductPlanWeekTemp.Active22;
                        mesproductPlanWeek.Active32 = mesproductPlanWeekTemp.Active32;
                        mesproductPlanWeek.Active42 = mesproductPlanWeekTemp.Active42;
                        mesproductPlanWeek.Active52 = mesproductPlanWeekTemp.Active52;
                        mesproductPlanWeek.Active62 = mesproductPlanWeekTemp.Active62;
                        mesproductPlanWeek.Active03 = mesproductPlanWeekTemp.Active03;
                        mesproductPlanWeek.Active13 = mesproductPlanWeekTemp.Active13;
                        mesproductPlanWeek.Active23 = mesproductPlanWeekTemp.Active23;
                        mesproductPlanWeek.Active33 = mesproductPlanWeekTemp.Active33;
                        mesproductPlanWeek.Active43 = mesproductPlanWeekTemp.Active43;
                        mesproductPlanWeek.Active53 = mesproductPlanWeekTemp.Active53;
                        mesproductPlanWeek.Active63 = mesproductPlanWeekTemp.Active63;
                        mesproductPlanWeek.WorkerId011 = mesproductPlanWeekTemp.Active01 ? Math.Max(mesproductPlanWeekTemp.WorkerId11, workerid) : workerid;
                        mesproductPlanWeek.WorkerId012 = mesproductPlanWeekTemp.Active01 ? Math.Max(mesproductPlanWeekTemp.WorkerId12, workerid) : workerid;
                        mesproductPlanWeek.WorkerId111 = mesproductPlanWeekTemp.Active11 ? Math.Max(mesproductPlanWeekTemp.WorkerId11, workerid) : workerid;
                        mesproductPlanWeek.WorkerId112 = mesproductPlanWeekTemp.Active11 ? Math.Max(mesproductPlanWeekTemp.WorkerId12, workerid) : workerid;
                        mesproductPlanWeek.WorkerId211 = mesproductPlanWeekTemp.Active21 ? Math.Max(mesproductPlanWeekTemp.WorkerId11, workerid) : workerid;
                        mesproductPlanWeek.WorkerId212 = mesproductPlanWeekTemp.Active21 ? Math.Max(mesproductPlanWeekTemp.WorkerId12, workerid) : workerid;
                        mesproductPlanWeek.WorkerId311 = mesproductPlanWeekTemp.Active31 ? Math.Max(mesproductPlanWeekTemp.WorkerId11, workerid) : workerid;
                        mesproductPlanWeek.WorkerId312 = mesproductPlanWeekTemp.Active31 ? Math.Max(mesproductPlanWeekTemp.WorkerId12, workerid) : workerid;
                        mesproductPlanWeek.WorkerId411 = mesproductPlanWeekTemp.Active41 ? Math.Max(mesproductPlanWeekTemp.WorkerId11, workerid) : workerid;
                        mesproductPlanWeek.WorkerId412 = mesproductPlanWeekTemp.Active41 ? Math.Max(mesproductPlanWeekTemp.WorkerId12, workerid) : workerid;
                        mesproductPlanWeek.WorkerId511 = mesproductPlanWeekTemp.Active51 ? Math.Max(mesproductPlanWeekTemp.WorkerId11, workerid) : workerid;
                        mesproductPlanWeek.WorkerId512 = mesproductPlanWeekTemp.Active51 ? Math.Max(mesproductPlanWeekTemp.WorkerId12, workerid) : workerid;
                        mesproductPlanWeek.WorkerId611 = mesproductPlanWeekTemp.Active61 ? Math.Max(mesproductPlanWeekTemp.WorkerId11, workerid) : workerid;
                        mesproductPlanWeek.WorkerId612 = mesproductPlanWeekTemp.Active61 ? Math.Max(mesproductPlanWeekTemp.WorkerId12, workerid) : workerid;
                        mesproductPlanWeek.WorkerId021 = mesproductPlanWeekTemp.Active02 ? Math.Max(mesproductPlanWeekTemp.WorkerId21, workerid) : workerid;
                        mesproductPlanWeek.WorkerId022 = mesproductPlanWeekTemp.Active02 ? Math.Max(mesproductPlanWeekTemp.WorkerId22, workerid) : workerid;
                        mesproductPlanWeek.WorkerId121 = mesproductPlanWeekTemp.Active12 ? Math.Max(mesproductPlanWeekTemp.WorkerId21, workerid) : workerid;
                        mesproductPlanWeek.WorkerId122 = mesproductPlanWeekTemp.Active12 ? Math.Max(mesproductPlanWeekTemp.WorkerId22, workerid) : workerid;
                        mesproductPlanWeek.WorkerId221 = mesproductPlanWeekTemp.Active22 ? Math.Max(mesproductPlanWeekTemp.WorkerId21, workerid) : workerid;
                        mesproductPlanWeek.WorkerId222 = mesproductPlanWeekTemp.Active22 ? Math.Max(mesproductPlanWeekTemp.WorkerId22, workerid) : workerid;
                        mesproductPlanWeek.WorkerId321 = mesproductPlanWeekTemp.Active32 ? Math.Max(mesproductPlanWeekTemp.WorkerId21, workerid) : workerid;
                        mesproductPlanWeek.WorkerId322 = mesproductPlanWeekTemp.Active32 ? Math.Max(mesproductPlanWeekTemp.WorkerId22, workerid) : workerid;
                        mesproductPlanWeek.WorkerId421 = mesproductPlanWeekTemp.Active42 ? Math.Max(mesproductPlanWeekTemp.WorkerId21, workerid) : workerid;
                        mesproductPlanWeek.WorkerId422 = mesproductPlanWeekTemp.Active42 ? Math.Max(mesproductPlanWeekTemp.WorkerId22, workerid) : workerid;
                        mesproductPlanWeek.WorkerId521 = mesproductPlanWeekTemp.Active52 ? Math.Max(mesproductPlanWeekTemp.WorkerId21, workerid) : workerid;
                        mesproductPlanWeek.WorkerId522 = mesproductPlanWeekTemp.Active52 ? Math.Max(mesproductPlanWeekTemp.WorkerId22, workerid) : workerid;
                        mesproductPlanWeek.WorkerId621 = mesproductPlanWeekTemp.Active62 ? Math.Max(mesproductPlanWeekTemp.WorkerId21, workerid) : workerid;
                        mesproductPlanWeek.WorkerId622 = mesproductPlanWeekTemp.Active62 ? Math.Max(mesproductPlanWeekTemp.WorkerId22, workerid) : workerid;
                        mesproductPlanWeek.WorkerId031 = mesproductPlanWeekTemp.Active03 ? Math.Max(mesproductPlanWeekTemp.WorkerId31, workerid) : workerid;
                        mesproductPlanWeek.WorkerId032 = mesproductPlanWeekTemp.Active03 ? Math.Max(mesproductPlanWeekTemp.WorkerId32, workerid) : workerid;
                        mesproductPlanWeek.WorkerId131 = mesproductPlanWeekTemp.Active13 ? Math.Max(mesproductPlanWeekTemp.WorkerId31, workerid) : workerid;
                        mesproductPlanWeek.WorkerId132 = mesproductPlanWeekTemp.Active13 ? Math.Max(mesproductPlanWeekTemp.WorkerId32, workerid) : workerid;
                        mesproductPlanWeek.WorkerId231 = mesproductPlanWeekTemp.Active23 ? Math.Max(mesproductPlanWeekTemp.WorkerId31, workerid) : workerid;
                        mesproductPlanWeek.WorkerId232 = mesproductPlanWeekTemp.Active23 ? Math.Max(mesproductPlanWeekTemp.WorkerId32, workerid) : workerid;
                        mesproductPlanWeek.WorkerId331 = mesproductPlanWeekTemp.Active33 ? Math.Max(mesproductPlanWeekTemp.WorkerId31, workerid) : workerid;
                        mesproductPlanWeek.WorkerId332 = mesproductPlanWeekTemp.Active33 ? Math.Max(mesproductPlanWeekTemp.WorkerId32, workerid) : workerid;
                        mesproductPlanWeek.WorkerId431 = mesproductPlanWeekTemp.Active43 ? Math.Max(mesproductPlanWeekTemp.WorkerId31, workerid) : workerid;
                        mesproductPlanWeek.WorkerId432 = mesproductPlanWeekTemp.Active43 ? Math.Max(mesproductPlanWeekTemp.WorkerId32, workerid) : workerid;
                        mesproductPlanWeek.WorkerId531 = mesproductPlanWeekTemp.Active53 ? Math.Max(mesproductPlanWeekTemp.WorkerId31, workerid) : workerid;
                        mesproductPlanWeek.WorkerId532 = mesproductPlanWeekTemp.Active53 ? Math.Max(mesproductPlanWeekTemp.WorkerId32, workerid) : workerid;
                        mesproductPlanWeek.WorkerId631 = mesproductPlanWeekTemp.Active63 ? Math.Max(mesproductPlanWeekTemp.WorkerId31, workerid) : workerid;
                        mesproductPlanWeek.WorkerId632 = mesproductPlanWeekTemp.Active63 ? Math.Max(mesproductPlanWeekTemp.WorkerId32, workerid) : workerid;
                        mesproductPlanWeek.Newflag = false;
                        bool needupdate = false;
                        if (mesproductPlanWeek.StartTime != dt)
                        {
                            needupdate = true;
                            mesproductPlanWeek.StartTime = dt;
                        }
                        if (ModelState.IsValid | needupdate)
                        {
                            _context.Update(mesproductPlanWeek);
                            await _context.SaveChangesAsync();
                            _context.Update(mesproductPlanWeekTemp);
                            await _context.SaveChangesAsync();
                            await DoMakingMesProductPlans(mesproductPlanWeek.Id).ConfigureAwait(false);
                            return RedirectToAction("Index", new { Year = mesproductPlanWeek.Year, Week = mesproductPlanWeek.Week });
                        }
                        //await DoMakingMesProductPlans(mesproductPlanWeek.Id).ConfigureAwait(false);
                        //await _context.SaveChangesAsync();
                        //return RedirectToAction(nameof(Index));
                    }
                }
                catch { }
            }
            ViewData["WorkerId11"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeekTemp.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeekTemp.WorkerId11);
            ViewData["WorkerId12"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeekTemp.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeekTemp.WorkerId12);
            ViewData["WorkerId21"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeekTemp.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeekTemp.WorkerId21);
            ViewData["WorkerId22"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeekTemp.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeekTemp.WorkerId22);
            ViewData["WorkerId31"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeekTemp.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeekTemp.WorkerId31);
            ViewData["WorkerId32"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeekTemp.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeekTemp.WorkerId32);
            return PartialView("_CreateModalPartion", mesproductPlanWeekTemp);
        }
        
        // POST: MesproductPlanWeeks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,LineId,Year,Week,Active01,Maintenance01,Mtype01,From01,To01,WorkerId011,WorkerId012,Active02,Maintenance02,Mtype02,From02,To02,WorkerId021,WorkerId022,Active03,Maintenance03,Mtype03,From03,To03,WorkerId031,WorkerId032,Active11,Maintenance11,Mtype11,From11,To11,WorkerId111,WorkerId112,Active12,Maintenance12,Mtype12,From12,To12,WorkerId121,WorkerId122,Active13,Maintenance13,Mtype13,From13,To13,WorkerId131,WorkerId132,Active21,Maintenance21,Mtype21,From21,To21,WorkerId211,WorkerId212,Active22,Maintenance22,Mtype22,From22,To22,WorkerId221,WorkerId222,Active23,Maintenance23,Mtype23,From23,To23,WorkerId231,WorkerId232,Active31,Maintenance31,Mtype31,From31,To31,WorkerId311,WorkerId312,Active32,Maintenance32,Mtype32,From32,To32,WorkerId321,WorkerId322,Active33,Maintenance33,Mtype33,From33,To33,WorkerId331,WorkerId332,Active41,Maintenance41,Mtype41,From41,To41,WorkerId411,WorkerId412,Active42,Maintenance42,Mtype42,From42,To42,WorkerId421,WorkerId422,Active43,From43,To43,Maintenance43,Mtype43,WorkerId431,WorkerId432,Active51,Maintenance51,Mtype51,From51,To51,WorkerId511,WorkerId512,Active52,Maintenance52,Mtype52,From52,To52,WorkerId521,WorkerId522,Active53,Maintenance53,Mtype53,From53,To53,WorkerId531,WorkerId532,Active61,Maintenance61,Mtype61,From61,To61,WorkerId611,WorkerId612,Active62,Maintenance62,Mtype62,From62,To62,WorkerId621,WorkerId622,Active63,Maintenance63,Mtype63,From63,To63,WorkerId631,WorkerId632,StartTime")] MesproductPlanWeek mesproductPlanWeek)
        //{
        //    DateTime dt = FirstDateOfWeekIso8601(mesproductPlanWeek.Year, mesproductPlanWeek.Week).AddHours(6);
        //    bool needupdate = false;
        //    if (mesproductPlanWeek.StartTime != dt)
        //    {
        //        needupdate = true;
        //        mesproductPlanWeek.StartTime = dt;
        //    }
        //    if (ModelState.IsValid | needupdate)
        //    {
        //        _context.Add(mesproductPlanWeek);
        //        await _context.SaveChangesAsync();
        //        await DoMakingMesProductPlans(mesproductPlanWeek.Id).ConfigureAwait(false);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["Mtype01"] = new SelectList(_context.MesproductPlanType, "Id", "Description", mesproductPlanWeek.Mtype01);
        //    ViewData["Mtype02"] = new SelectList(_context.MesproductPlanType, "Id", "Description", mesproductPlanWeek.Mtype02);
        //    ViewData["Mtype03"] = new SelectList(_context.MesproductPlanType, "Id", "Description", mesproductPlanWeek.Mtype03);
        //    ViewData["Mtype11"] = new SelectList(_context.MesproductPlanType, "Id", "Description", mesproductPlanWeek.Mtype11);
        //    ViewData["Mtype12"] = new SelectList(_context.MesproductPlanType, "Id", "Description", mesproductPlanWeek.Mtype12);
        //    ViewData["Mtype13"] = new SelectList(_context.MesproductPlanType, "Id", "Description", mesproductPlanWeek.Mtype13);
        //    ViewData["Mtype21"] = new SelectList(_context.MesproductPlanType, "Id", "Description", mesproductPlanWeek.Mtype21);
        //    ViewData["Mtype22"] = new SelectList(_context.MesproductPlanType, "Id", "Description", mesproductPlanWeek.Mtype22);
        //    ViewData["Mtype23"] = new SelectList(_context.MesproductPlanType, "Id", "Description", mesproductPlanWeek.Mtype23);
        //    ViewData["Mtype31"] = new SelectList(_context.MesproductPlanType, "Id", "Description", mesproductPlanWeek.Mtype31);
        //    ViewData["Mtype32"] = new SelectList(_context.MesproductPlanType, "Id", "Description", mesproductPlanWeek.Mtype32);
        //    ViewData["Mtype33"] = new SelectList(_context.MesproductPlanType, "Id", "Description", mesproductPlanWeek.Mtype33);
        //    ViewData["Mtype41"] = new SelectList(_context.MesproductPlanType, "Id", "Description", mesproductPlanWeek.Mtype41);
        //    ViewData["Mtype42"] = new SelectList(_context.MesproductPlanType, "Id", "Description", mesproductPlanWeek.Mtype42);
        //    ViewData["Mtype43"] = new SelectList(_context.MesproductPlanType, "Id", "Description", mesproductPlanWeek.Mtype43);
        //    ViewData["Mtype51"] = new SelectList(_context.MesproductPlanType, "Id", "Description", mesproductPlanWeek.Mtype51);
        //    ViewData["Mtype52"] = new SelectList(_context.MesproductPlanType, "Id", "Description", mesproductPlanWeek.Mtype52);
        //    ViewData["Mtype53"] = new SelectList(_context.MesproductPlanType, "Id", "Description", mesproductPlanWeek.Mtype53);
        //    ViewData["Mtype61"] = new SelectList(_context.MesproductPlanType, "Id", "Description", mesproductPlanWeek.Mtype61);
        //    ViewData["Mtype62"] = new SelectList(_context.MesproductPlanType, "Id", "Description", mesproductPlanWeek.Mtype62);
        //    ViewData["Mtype63"] = new SelectList(_context.MesproductPlanType, "Id", "Description", mesproductPlanWeek.Mtype63);
        //    ViewData["WorkerId011"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId011);
        //    ViewData["WorkerId012"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId012);
        //    ViewData["WorkerId021"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId021);
        //    ViewData["WorkerId022"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId022);
        //    ViewData["WorkerId031"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId031);
        //    ViewData["WorkerId032"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId032);
        //    ViewData["WorkerId111"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId111);
        //    ViewData["WorkerId112"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId112);
        //    ViewData["WorkerId121"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId121);
        //    ViewData["WorkerId122"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId122);
        //    ViewData["WorkerId131"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId131);
        //    ViewData["WorkerId132"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId132);
        //    ViewData["WorkerId211"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId211);
        //    ViewData["WorkerId212"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId212);
        //    ViewData["WorkerId221"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId221);
        //    ViewData["WorkerId222"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId222);
        //    ViewData["WorkerId231"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId231);
        //    ViewData["WorkerId232"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId232);
        //    ViewData["WorkerId311"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId311);
        //    ViewData["WorkerId312"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId312);
        //    ViewData["WorkerId321"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId321);
        //    ViewData["WorkerId322"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId322);
        //    ViewData["WorkerId331"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId331);
        //    ViewData["WorkerId332"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId332);
        //    ViewData["WorkerId411"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId411);
        //    ViewData["WorkerId412"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId412);
        //    ViewData["WorkerId421"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId421);
        //    ViewData["WorkerId422"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId422);
        //    ViewData["WorkerId431"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId431);
        //    ViewData["WorkerId432"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId432);
        //    ViewData["WorkerId511"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId511);
        //    ViewData["WorkerId512"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId512);
        //    ViewData["WorkerId521"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId521);
        //    ViewData["WorkerId522"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId522);
        //    ViewData["WorkerId531"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId531);
        //    ViewData["WorkerId532"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId532);
        //    ViewData["WorkerId611"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId611);
        //    ViewData["WorkerId612"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId612);
        //    ViewData["WorkerId621"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId621);
        //    ViewData["WorkerId622"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId622);
        //    ViewData["WorkerId631"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId631);
        //    ViewData["WorkerId632"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId632);
        //    return PartialView("_CreateModalPartion", mesproductPlanWeek);
        //}

        // GET: MesproductPlanWeeks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MesproductPlanWeek == null)
            {
                return NotFound();
            }

            var mesproductPlanWeek = await _context.MesproductPlanWeek.FindAsync(id);
            if (mesproductPlanWeek == null)
            {
                return NotFound();
            }
            DateTime dt = FirstDateOfWeekIso8601(mesproductPlanWeek.Year, mesproductPlanWeek.Week).AddHours(6);
            if (mesproductPlanWeek.StartTime != dt)
            {
                mesproductPlanWeek.StartTime = dt;
            }
            ViewData["Mtype01"] = new SelectList(_context.MesproductPlanType, "Id", "Description", mesproductPlanWeek.Mtype01);
            ViewData["Mtype02"] = new SelectList(_context.MesproductPlanType, "Id", "Description", mesproductPlanWeek.Mtype02);
            ViewData["Mtype03"] = new SelectList(_context.MesproductPlanType, "Id", "Description", mesproductPlanWeek.Mtype03);
            ViewData["Mtype11"] = new SelectList(_context.MesproductPlanType, "Id", "Description", mesproductPlanWeek.Mtype11);
            ViewData["Mtype12"] = new SelectList(_context.MesproductPlanType, "Id", "Description", mesproductPlanWeek.Mtype12);
            ViewData["Mtype13"] = new SelectList(_context.MesproductPlanType, "Id", "Description", mesproductPlanWeek.Mtype13);
            ViewData["Mtype21"] = new SelectList(_context.MesproductPlanType, "Id", "Description", mesproductPlanWeek.Mtype21);
            ViewData["Mtype22"] = new SelectList(_context.MesproductPlanType, "Id", "Description", mesproductPlanWeek.Mtype22);
            ViewData["Mtype23"] = new SelectList(_context.MesproductPlanType, "Id", "Description", mesproductPlanWeek.Mtype23);
            ViewData["Mtype31"] = new SelectList(_context.MesproductPlanType, "Id", "Description", mesproductPlanWeek.Mtype31);
            ViewData["Mtype32"] = new SelectList(_context.MesproductPlanType, "Id", "Description", mesproductPlanWeek.Mtype32);
            ViewData["Mtype33"] = new SelectList(_context.MesproductPlanType, "Id", "Description", mesproductPlanWeek.Mtype33);
            ViewData["Mtype41"] = new SelectList(_context.MesproductPlanType, "Id", "Description", mesproductPlanWeek.Mtype41);
            ViewData["Mtype42"] = new SelectList(_context.MesproductPlanType, "Id", "Description", mesproductPlanWeek.Mtype42);
            ViewData["Mtype43"] = new SelectList(_context.MesproductPlanType, "Id", "Description", mesproductPlanWeek.Mtype43);
            ViewData["Mtype51"] = new SelectList(_context.MesproductPlanType, "Id", "Description", mesproductPlanWeek.Mtype51);
            ViewData["Mtype52"] = new SelectList(_context.MesproductPlanType, "Id", "Description", mesproductPlanWeek.Mtype52);
            ViewData["Mtype53"] = new SelectList(_context.MesproductPlanType, "Id", "Description", mesproductPlanWeek.Mtype53);
            ViewData["Mtype61"] = new SelectList(_context.MesproductPlanType, "Id", "Description", mesproductPlanWeek.Mtype61);
            ViewData["Mtype62"] = new SelectList(_context.MesproductPlanType, "Id", "Description", mesproductPlanWeek.Mtype62);
            ViewData["Mtype63"] = new SelectList(_context.MesproductPlanType, "Id", "Description", mesproductPlanWeek.Mtype63);
            ViewData["WorkerId011"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId011);
            ViewData["WorkerId012"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId012);
            ViewData["WorkerId021"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId021);
            ViewData["WorkerId022"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId022);
            ViewData["WorkerId031"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId031);
            ViewData["WorkerId032"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId032);
            ViewData["WorkerId111"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId111);
            ViewData["WorkerId112"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId112);
            ViewData["WorkerId121"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId121);
            ViewData["WorkerId122"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId122);
            ViewData["WorkerId131"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId131);
            ViewData["WorkerId132"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId132);
            ViewData["WorkerId211"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId211);
            ViewData["WorkerId212"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId212);
            ViewData["WorkerId221"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId221);
            ViewData["WorkerId222"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId222);
            ViewData["WorkerId231"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId231);
            ViewData["WorkerId232"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId232);
            ViewData["WorkerId311"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId311);
            ViewData["WorkerId312"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId312);
            ViewData["WorkerId321"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId321);
            ViewData["WorkerId322"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId322);
            ViewData["WorkerId331"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId331);
            ViewData["WorkerId332"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId332);
            ViewData["WorkerId411"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId411);
            ViewData["WorkerId412"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId412);
            ViewData["WorkerId421"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId421);
            ViewData["WorkerId422"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId422);
            ViewData["WorkerId431"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId431);
            ViewData["WorkerId432"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId432);
            ViewData["WorkerId511"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId511);
            ViewData["WorkerId512"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId512);
            ViewData["WorkerId521"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId521);
            ViewData["WorkerId522"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId522);
            ViewData["WorkerId531"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId531);
            ViewData["WorkerId532"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId532);
            ViewData["WorkerId611"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId611);
            ViewData["WorkerId612"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId612);
            ViewData["WorkerId621"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId621);
            ViewData["WorkerId622"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId622);
            ViewData["WorkerId631"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId631);
            ViewData["WorkerId632"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId632);
            return PartialView("_EditModalPartion", mesproductPlanWeek);
        }

        // POST: MesproductPlanWeeks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LineId,Year,Week,Active01,Maintenance01,Mtype01,From01,To01,WorkerId011,WorkerId012,Active02,Maintenance02,Mtype02,From02,To02,WorkerId021,WorkerId022,Active03,Maintenance03,Mtype03,From03,To03,WorkerId031,WorkerId032,Active11,Maintenance11,Mtype11,From11,To11,WorkerId111,WorkerId112,Active12,Maintenance12,Mtype12,From12,To12,WorkerId121,WorkerId122,Active13,Maintenance13,Mtype13,From13,To13,WorkerId131,WorkerId132,Active21,Maintenance21,Mtype21,From21,To21,WorkerId211,WorkerId212,Active22,Maintenance22,Mtype22,From22,To22,WorkerId221,WorkerId222,Active23,Maintenance23,Mtype23,From23,To23,WorkerId231,WorkerId232,Active31,Maintenance31,Mtype31,From31,To31,WorkerId311,WorkerId312,Active32,Maintenance32,Mtype32,From32,To32,WorkerId321,WorkerId322,Active33,Maintenance33,Mtype33,From33,To33,WorkerId331,WorkerId332,Active41,Maintenance41,Mtype41,From41,To41,WorkerId411,WorkerId412,Active42,Maintenance42,Mtype42,From42,To42,WorkerId421,WorkerId422,Active43,From43,To43,Maintenance43,Mtype43,WorkerId431,WorkerId432,Active51,Maintenance51,Mtype51,From51,To51,WorkerId511,WorkerId512,Active52,Maintenance52,Mtype52,From52,To52,WorkerId521,WorkerId522,Active53,Maintenance53,Mtype53,From53,To53,WorkerId531,WorkerId532,Active61,Maintenance61,Mtype61,From61,To61,WorkerId611,WorkerId612,Active62,Maintenance62,Mtype62,From62,To62,WorkerId621,WorkerId622,Active63,Maintenance63,Mtype63,From63,To63,WorkerId631,WorkerId632,StartTime,NewFlag")] MesproductPlanWeek mesproductPlanWeek)
        {
            if (id != mesproductPlanWeek.Id)
            {
                return NotFound();
            }

            DateTime dt = FirstDateOfWeekIso8601(mesproductPlanWeek.Year, mesproductPlanWeek.Week).AddHours(6);
            bool needupdate = true;
            if (mesproductPlanWeek.StartTime != dt)
            {
                //needupdate = true;
                mesproductPlanWeek.StartTime = dt;
            }
            if (ModelState.IsValid | needupdate)
            {
                try
                {
                    mesproductPlanWeek.Newflag = false;
                    _context.Update(mesproductPlanWeek);
                    await _context.SaveChangesAsync();
                    if (needupdate)
                    {
                        await DoMakingMesProductPlans(mesproductPlanWeek.Id).ConfigureAwait(false);
                    }
                    return RedirectToAction("Index", new { Year = mesproductPlanWeek.Year, Week = mesproductPlanWeek.Week });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MesproductPlanWeekExists(mesproductPlanWeek.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }  
            }
            ViewData["Mtype01"] = new SelectList(_context.MesproductPlanType, "Id", "Description", mesproductPlanWeek.Mtype01);
            ViewData["Mtype02"] = new SelectList(_context.MesproductPlanType, "Id", "Description", mesproductPlanWeek.Mtype02);
            ViewData["Mtype03"] = new SelectList(_context.MesproductPlanType, "Id", "Description", mesproductPlanWeek.Mtype03);
            ViewData["Mtype11"] = new SelectList(_context.MesproductPlanType, "Id", "Description", mesproductPlanWeek.Mtype11);
            ViewData["Mtype12"] = new SelectList(_context.MesproductPlanType, "Id", "Description", mesproductPlanWeek.Mtype12);
            ViewData["Mtype13"] = new SelectList(_context.MesproductPlanType, "Id", "Description", mesproductPlanWeek.Mtype13);
            ViewData["Mtype21"] = new SelectList(_context.MesproductPlanType, "Id", "Description", mesproductPlanWeek.Mtype21);
            ViewData["Mtype22"] = new SelectList(_context.MesproductPlanType, "Id", "Description", mesproductPlanWeek.Mtype22);
            ViewData["Mtype23"] = new SelectList(_context.MesproductPlanType, "Id", "Description", mesproductPlanWeek.Mtype23);
            ViewData["Mtype31"] = new SelectList(_context.MesproductPlanType, "Id", "Description", mesproductPlanWeek.Mtype31);
            ViewData["Mtype32"] = new SelectList(_context.MesproductPlanType, "Id", "Description", mesproductPlanWeek.Mtype32);
            ViewData["Mtype33"] = new SelectList(_context.MesproductPlanType, "Id", "Description", mesproductPlanWeek.Mtype33);
            ViewData["Mtype41"] = new SelectList(_context.MesproductPlanType, "Id", "Description", mesproductPlanWeek.Mtype41);
            ViewData["Mtype42"] = new SelectList(_context.MesproductPlanType, "Id", "Description", mesproductPlanWeek.Mtype42);
            ViewData["Mtype43"] = new SelectList(_context.MesproductPlanType, "Id", "Description", mesproductPlanWeek.Mtype43);
            ViewData["Mtype51"] = new SelectList(_context.MesproductPlanType, "Id", "Description", mesproductPlanWeek.Mtype51);
            ViewData["Mtype52"] = new SelectList(_context.MesproductPlanType, "Id", "Description", mesproductPlanWeek.Mtype52);
            ViewData["Mtype53"] = new SelectList(_context.MesproductPlanType, "Id", "Description", mesproductPlanWeek.Mtype53);
            ViewData["Mtype61"] = new SelectList(_context.MesproductPlanType, "Id", "Description", mesproductPlanWeek.Mtype61);
            ViewData["Mtype62"] = new SelectList(_context.MesproductPlanType, "Id", "Description", mesproductPlanWeek.Mtype62);
            ViewData["Mtype63"] = new SelectList(_context.MesproductPlanType, "Id", "Description", mesproductPlanWeek.Mtype63);
            ViewData["WorkerId011"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId011);
            ViewData["WorkerId012"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId012);
            ViewData["WorkerId021"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId021);
            ViewData["WorkerId022"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId022);
            ViewData["WorkerId031"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId031);
            ViewData["WorkerId032"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId032);
            ViewData["WorkerId111"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId111);
            ViewData["WorkerId112"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId112);
            ViewData["WorkerId121"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId121);
            ViewData["WorkerId122"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId122);
            ViewData["WorkerId131"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId131);
            ViewData["WorkerId132"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId132);
            ViewData["WorkerId211"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId211);
            ViewData["WorkerId212"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId212);
            ViewData["WorkerId221"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId221);
            ViewData["WorkerId222"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId222);
            ViewData["WorkerId231"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId231);
            ViewData["WorkerId232"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId232);
            ViewData["WorkerId311"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId311);
            ViewData["WorkerId312"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId312);
            ViewData["WorkerId321"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId321);
            ViewData["WorkerId322"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId322);
            ViewData["WorkerId331"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId331);
            ViewData["WorkerId332"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId332);
            ViewData["WorkerId411"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId411);
            ViewData["WorkerId412"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId412);
            ViewData["WorkerId421"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId421);
            ViewData["WorkerId422"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId422);
            ViewData["WorkerId431"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId431);
            ViewData["WorkerId432"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId432);
            ViewData["WorkerId511"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId511);
            ViewData["WorkerId512"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId512);
            ViewData["WorkerId521"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId521);
            ViewData["WorkerId522"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId522);
            ViewData["WorkerId531"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId531);
            ViewData["WorkerId532"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId532);
            ViewData["WorkerId611"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId611);
            ViewData["WorkerId612"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId612);
            ViewData["WorkerId621"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId621);
            ViewData["WorkerId622"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId622);
            ViewData["WorkerId631"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId631);
            ViewData["WorkerId632"] = new SelectList(_context.Mesworker.Where(a => a.LineId == mesproductPlanWeek.LineId).OrderBy(b => b.WorkerName), "Id", "WorkerName", mesproductPlanWeek.WorkerId632);
            return PartialView("_EditModalPartion", mesproductPlanWeek);
        }

        // GET: MesproductPlanWeeks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MesproductPlanWeek == null)
            {
                return NotFound();
            }

            var mesproductPlanWeek = await _context.MesproductPlanWeek
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mesproductPlanWeek == null)
            {
                return NotFound();
            }

            return View(mesproductPlanWeek);
        }

        // POST: MesproductPlanWeeks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MesproductPlanWeek == null)
            {
                return Problem("Entity set 'EISDBContext.MesproductPlanWeek'  is null.");
            }
            var mesproductPlanWeek = await _context.MesproductPlanWeek.FindAsync(id);
            if (mesproductPlanWeek != null)
            {
                _context.MesproductPlanWeek.Remove(mesproductPlanWeek);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public static DateTime FirstDateOfWeekIso8601(int year, int week)
        {
            var firstThursdayOfYear = new DateTime(year, 1, 1);
            while (firstThursdayOfYear.DayOfWeek != DayOfWeek.Thursday)
            {
                firstThursdayOfYear = firstThursdayOfYear.AddDays(1);
            }

            var startDateOfWeekOne = firstThursdayOfYear.AddDays(-(DayOfWeek.Thursday - DayOfWeek.Sunday));

            return startDateOfWeekOne.AddDays(7 * (week - 1));
        }

        async Task<int> DoMakingMesProductPlans(int? id)
        {
            int records = -1;
            if (id == null || _context.MesproductPlanWeek == null)
            {
                return -2;
            }

            var mesproductPlanWeek = await _context.MesproductPlanWeek.FindAsync(id);
            if (mesproductPlanWeek == null)
            {
                return -2;
            }
            records = 0;
            for (int day = 0; day < 7; day++)
            {
                for (int shift = 1; shift < 4; shift++)
                {
                    bool active = false;
                    bool maintenence = false;
                    TimeSpan? maintenancefrom = null;
                    TimeSpan? maintenanceto = null;
                    int workerid1 = 0;
                    int workerid2 = 0;
                    switch (day)
                    {
                        case 0:
                            switch (shift)
                            {
                                case 1:
                                    active = mesproductPlanWeek.Active01;
                                    maintenence = mesproductPlanWeek.Maintenance01;
                                    maintenancefrom = mesproductPlanWeek.From01;
                                    maintenanceto = mesproductPlanWeek.To01;
                                    workerid1 = mesproductPlanWeek.WorkerId011;
                                    workerid2 = mesproductPlanWeek.WorkerId012;
                                    break;
                                case 2:
                                    active = mesproductPlanWeek.Active02;
                                    maintenence = mesproductPlanWeek.Maintenance02;
                                    maintenancefrom = mesproductPlanWeek.From02;
                                    maintenanceto = mesproductPlanWeek.To02;
                                    workerid1 = mesproductPlanWeek.WorkerId021;
                                    workerid2 = mesproductPlanWeek.WorkerId022;
                                    break;
                                case 3:
                                    active = mesproductPlanWeek.Active03;
                                    maintenence = mesproductPlanWeek.Maintenance03;
                                    maintenancefrom = mesproductPlanWeek.From03;
                                    maintenanceto = mesproductPlanWeek.To03;
                                    workerid1 = mesproductPlanWeek.WorkerId031;
                                    workerid2 = mesproductPlanWeek.WorkerId032;
                                    break;
                            }
                            break;
                        case 1:
                            switch (shift)
                            {
                                case 1:
                                    active = mesproductPlanWeek.Active11;
                                    maintenence = mesproductPlanWeek.Maintenance11;
                                    maintenancefrom = mesproductPlanWeek.From11;
                                    maintenanceto = mesproductPlanWeek.To11;
                                    workerid1 = mesproductPlanWeek.WorkerId111;
                                    workerid2 = mesproductPlanWeek.WorkerId112;
                                    break;
                                case 2:
                                    active = mesproductPlanWeek.Active12;
                                    maintenence = mesproductPlanWeek.Maintenance12;
                                    maintenancefrom = mesproductPlanWeek.From12;
                                    maintenanceto = mesproductPlanWeek.To12;
                                    workerid1 = mesproductPlanWeek.WorkerId121;
                                    workerid2 = mesproductPlanWeek.WorkerId122;
                                    break;
                                case 3:
                                    active = mesproductPlanWeek.Active13;
                                    maintenence = mesproductPlanWeek.Maintenance13;
                                    maintenancefrom = mesproductPlanWeek.From13;
                                    maintenanceto = mesproductPlanWeek.To13;
                                    workerid1 = mesproductPlanWeek.WorkerId131;
                                    workerid2 = mesproductPlanWeek.WorkerId132;
                                    break;
                            }
                            break;
                        case 2:
                            switch (shift)
                            {
                                case 1:
                                    active = mesproductPlanWeek.Active21;
                                    maintenence = mesproductPlanWeek.Maintenance21;
                                    maintenancefrom = mesproductPlanWeek.From21;
                                    maintenanceto = mesproductPlanWeek.To21;
                                    workerid1 = mesproductPlanWeek.WorkerId211;
                                    workerid2 = mesproductPlanWeek.WorkerId212;
                                    break;
                                case 2:
                                    active = mesproductPlanWeek.Active22;
                                    maintenence = mesproductPlanWeek.Maintenance22;
                                    maintenancefrom = mesproductPlanWeek.From22;
                                    maintenanceto = mesproductPlanWeek.To22;
                                    workerid1 = mesproductPlanWeek.WorkerId221;
                                    workerid2 = mesproductPlanWeek.WorkerId222;
                                    break;
                                case 3:
                                    active = mesproductPlanWeek.Active23;
                                    maintenence = mesproductPlanWeek.Maintenance23;
                                    maintenancefrom = mesproductPlanWeek.From23;
                                    maintenanceto = mesproductPlanWeek.To23;
                                    workerid1 = mesproductPlanWeek.WorkerId231;
                                    workerid2 = mesproductPlanWeek.WorkerId232;
                                    break;
                            }
                            break;
                        case 3:
                            switch (shift)
                            {
                                case 1:
                                    active = mesproductPlanWeek.Active31;
                                    maintenence = mesproductPlanWeek.Maintenance31;
                                    maintenancefrom = mesproductPlanWeek.From31;
                                    maintenanceto = mesproductPlanWeek.To31;
                                    workerid1 = mesproductPlanWeek.WorkerId311;
                                    workerid2 = mesproductPlanWeek.WorkerId312;
                                    break;
                                case 2:
                                    active = mesproductPlanWeek.Active32;
                                    maintenence = mesproductPlanWeek.Maintenance32;
                                    maintenancefrom = mesproductPlanWeek.From32;
                                    maintenanceto = mesproductPlanWeek.To32;
                                    workerid1 = mesproductPlanWeek.WorkerId321;
                                    workerid2 = mesproductPlanWeek.WorkerId322;
                                    break;
                                case 3:
                                    active = mesproductPlanWeek.Active33;
                                    maintenence = mesproductPlanWeek.Maintenance33;
                                    maintenancefrom = mesproductPlanWeek.From33;
                                    maintenanceto = mesproductPlanWeek.To33;
                                    workerid1 = mesproductPlanWeek.WorkerId331;
                                    workerid2 = mesproductPlanWeek.WorkerId332;
                                    break;
                            }
                            break;
                        case 4:
                            switch (shift)
                            {
                                case 1:
                                    active = mesproductPlanWeek.Active41;
                                    maintenence = mesproductPlanWeek.Maintenance41;
                                    maintenancefrom = mesproductPlanWeek.From41;
                                    maintenanceto = mesproductPlanWeek.To41;
                                    workerid1 = mesproductPlanWeek.WorkerId411;
                                    workerid2 = mesproductPlanWeek.WorkerId412;
                                    break;
                                case 2:
                                    active = mesproductPlanWeek.Active42;
                                    maintenence = mesproductPlanWeek.Maintenance42;
                                    maintenancefrom = mesproductPlanWeek.From42;
                                    maintenanceto = mesproductPlanWeek.To42;
                                    workerid1 = mesproductPlanWeek.WorkerId421;
                                    workerid2 = mesproductPlanWeek.WorkerId422;
                                    break;
                                case 3:
                                    active = mesproductPlanWeek.Active43;
                                    maintenence = mesproductPlanWeek.Maintenance43;
                                    maintenancefrom = mesproductPlanWeek.From43;
                                    maintenanceto = mesproductPlanWeek.To43;
                                    workerid1 = mesproductPlanWeek.WorkerId431;
                                    workerid2 = mesproductPlanWeek.WorkerId432;
                                    break;
                            }
                            break;
                        case 5:
                            switch (shift)
                            {
                                case 1:
                                    active = mesproductPlanWeek.Active51;
                                    maintenence = mesproductPlanWeek.Maintenance51;
                                    maintenancefrom = mesproductPlanWeek.From51;
                                    maintenanceto = mesproductPlanWeek.To51;
                                    workerid1 = mesproductPlanWeek.WorkerId511;
                                    workerid2 = mesproductPlanWeek.WorkerId512;
                                    break;
                                case 2:
                                    active = mesproductPlanWeek.Active52;
                                    maintenence = mesproductPlanWeek.Maintenance52;
                                    maintenancefrom = mesproductPlanWeek.From52;
                                    maintenanceto = mesproductPlanWeek.To52;
                                    workerid1 = mesproductPlanWeek.WorkerId521;
                                    workerid2 = mesproductPlanWeek.WorkerId522;
                                    break;
                                case 3:
                                    active = mesproductPlanWeek.Active53;
                                    maintenence = mesproductPlanWeek.Maintenance53;
                                    maintenancefrom = mesproductPlanWeek.From53;
                                    maintenanceto = mesproductPlanWeek.To53;
                                    workerid1 = mesproductPlanWeek.WorkerId531;
                                    workerid2 = mesproductPlanWeek.WorkerId532;
                                    break;
                            }
                            break;
                        case 6:
                            switch (shift)
                            {
                                case 1:
                                    active = mesproductPlanWeek.Active61;
                                    maintenence = mesproductPlanWeek.Maintenance61;
                                    maintenancefrom = mesproductPlanWeek.From61;
                                    maintenanceto = mesproductPlanWeek.To61;
                                    workerid1 = mesproductPlanWeek.WorkerId611;
                                    workerid2 = mesproductPlanWeek.WorkerId612;
                                    break;
                                case 2:
                                    active = mesproductPlanWeek.Active62;
                                    maintenence = mesproductPlanWeek.Maintenance62;
                                    maintenancefrom = mesproductPlanWeek.From62;
                                    maintenanceto = mesproductPlanWeek.To62;
                                    workerid1 = mesproductPlanWeek.WorkerId621;
                                    workerid2 = mesproductPlanWeek.WorkerId622;
                                    break;
                                case 3:
                                    active = mesproductPlanWeek.Active63;
                                    maintenence = mesproductPlanWeek.Maintenance63;
                                    maintenancefrom = mesproductPlanWeek.From63;
                                    maintenanceto = mesproductPlanWeek.To63;
                                    workerid1 = mesproductPlanWeek.WorkerId631;
                                    workerid2 = mesproductPlanWeek.WorkerId632;
                                    break;
                            }
                            break;
                    }
                    MesproductPlan mesproductPlan = new MesproductPlan
                    {
                        Id = 0,
                        LineId = mesproductPlanWeek.LineId,
                        StartTime = mesproductPlanWeek.StartTime.AddDays(day),
                        Muszak = shift,
                        Active = active,
                        Maintenance = maintenence,
                        Maintenancefrom = maintenancefrom,
                        Maintenanceto = maintenanceto,
                        WorkerId1 = workerid1,
                        WorkerId2 = workerid2
                    };
                    MesproductPlan mpp = await _context.MesproductPlan.Where(a => a.LineId == mesproductPlan.LineId & a.StartTime == mesproductPlan.StartTime & a.Muszak == mesproductPlan.Muszak).FirstOrDefaultAsync();
                    if (mpp == null)
                    {
                        //New record
                        _context.Add(mesproductPlan);
                        await _context.SaveChangesAsync();
                        records++;
                    }
                    else
                    {
                        mesproductPlan.Id = mpp.Id;
                        if (!(mpp.LineId == mesproductPlan.LineId &
                            mpp.StartTime == mesproductPlan.StartTime &
                            mpp.Muszak == mesproductPlan.Muszak &
                            mpp.Active == mesproductPlan.Active &
                            mpp.Maintenance == mesproductPlan.Maintenance &
                            mpp.Maintenancefrom == mesproductPlan.Maintenancefrom &
                            mpp.Maintenanceto == mesproductPlan.Maintenanceto &
                            mpp.WorkerId1 == mesproductPlan.WorkerId1 &
                            mpp.WorkerId2 == mesproductPlan.WorkerId2))
                        {
                            mpp.LineId = mesproductPlan.LineId;
                            mpp.StartTime = mesproductPlan.StartTime;
                            mpp.Muszak = mesproductPlan.Muszak;
                            mpp.Active = mesproductPlan.Active;
                            mpp.Maintenance = mesproductPlan.Maintenance;
                            mpp.Maintenancefrom = mesproductPlan.Maintenancefrom;
                            mpp.Maintenanceto = mesproductPlan.Maintenanceto;
                            mpp.WorkerId1 = mesproductPlan.WorkerId1;
                            mpp.WorkerId2 = mesproductPlan.WorkerId2;
                            //Update record
                            _context.Update(mpp);
                            await _context.SaveChangesAsync();
                            records++;
                        }
                    }
                }
            }
            if (records > 0)
            {
                await _context.SaveChangesAsync();
            }
            return records;
        }

        private bool MesproductPlanWeekExists(int id)
        {
          return _context.MesproductPlanWeek.Any(e => e.Id == id);
        }
    }
}

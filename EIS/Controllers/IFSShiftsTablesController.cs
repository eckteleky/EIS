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
    public class IFSShiftsTablesController : Controller
    {
        private readonly EISDBContext _context;

        public IFSShiftsTablesController(EISDBContext context)
        {
            _context = context;
        }

        // GET: IFSShiftsTables
        public async Task<IActionResult> Index()
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
            var alertsDBContext = await _context.ShiftsTable.Where(a => (a.LineId == LineID & a.Starttime == dateTime & ((shift < 4 & a.Muszak == shift) | shift == 4 | (shift == 5 & (a.Muszak == 1 | a.Muszak == 3)) | (shift == 6 & (a.Muszak == 2 | a.Muszak == 3)) | (shift == 7 & (a.Muszak == 1 | a.Muszak == 2))))).Include(b => b.IfsHeaderTable).ToListAsync();
            ViewData["LineID"] = LineID;
            ViewData["Year"] = year;
            ViewData["Month"] = month;
            ViewData["Day"] = day;
            ViewData["Shift"] = shifttxt;
            for(int i=0; i<alertsDBContext.Count();i++)
            {
                if (alertsDBContext[i] != null)
                {
                    var ifsHeaderTable = await _context.IfsHeaderTable.Where(a => (a.LineId == alertsDBContext[i].LineId & a.StartTime == alertsDBContext[i].Starttime & a.ShiftId == alertsDBContext[i].Muszak)).FirstOrDefaultAsync();
                    if (ifsHeaderTable == null)
                    {
                        DateTime start = alertsDBContext[i].Starttime;
                        DateTime end = alertsDBContext[i].Starttime.Date.AddHours(14).AddMinutes(20);
                        switch (alertsDBContext[i].Muszak)
                        {
                            case 1: //Shift #1
                                start = alertsDBContext[i].Starttime;
                                end = alertsDBContext[i].Starttime.Date.AddHours(14).AddMinutes(20);
                                break;
                            case 2: //Shift #2
                                start = alertsDBContext[i].Starttime.Date.AddHours(14).AddMinutes(20);
                                end = alertsDBContext[i].Starttime.Date.AddHours(22).AddMinutes(40);
                                break;
                            case 3: //Shift #3
                                start = alertsDBContext[i].Starttime.Date.AddHours(22).AddMinutes(40);
                                end = alertsDBContext[i].Starttime.AddDays(1);
                                break;
                        }
                        DateTime now = DateTime.Now;
                        if (now > start & now < end)
                        {
                            end = now;
                        }
                        else
                        {
                            ifsHeaderTable = new IfsHeaderTable()
                            {
                                LineId = alertsDBContext[i].LineId,
                                StartTime = alertsDBContext[i].Starttime,
                                ShiftId = alertsDBContext[i].Muszak,
                                AllParts = alertsDBContext[i].Gyartott.Value,
                                BadParts = alertsDBContext[i].PGyartott.Value,
                                GoodParts = alertsDBContext[i].Gyartott.Value - alertsDBContext[i].PGyartott.Value,
                                WorkingHours = (end - start - new TimeSpan(0, 20, 0)).TotalHours,
                                SettingsParts = 0,
                                DroppedParts = 0,
                                FailureRate = alertsDBContext[i].Gyartott.Value > 0 ? 100 * (double)alertsDBContext[i].PGyartott.Value / (double)alertsDBContext[i].Gyartott.Value : 0,
                            };
                            _context.Add(ifsHeaderTable);
                            await _context.SaveChangesAsync();
                            ViewData["Id"] = new SelectList(_context.IfsValueTable, "IfsheaderId", "IfsheaderId");
                            ViewData["ShiftId"] = new SelectList(_context.IfsMuszakTable, "Id", "Description");
                        }
                    }
                    ViewData["IFSHeaderTable"] = ifsHeaderTable;
                    var ifsHeaderListTable = await _context.ViewIfsHeadersList.OrderBy(a => a.ChangeTime).Where(a => (a.LineId == alertsDBContext[i].LineId & a.StartTime == alertsDBContext[i].Starttime & a.Muszak == alertsDBContext[i].Muszak)).ToListAsync();
                    if (ifsHeaderListTable != null)
                    {
                        for (int a = 0; a < Math.Max(ifsHeaderListTable.Count, 5); a++)
                        {
                            if (a < ifsHeaderListTable.Count)
                            {
                                ViewData["TypeName" + ifsHeaderListTable[a].Muszak + a] = ifsHeaderListTable[a].TypeName;
                                ViewData["GoodParts" + ifsHeaderListTable[a].Muszak + a] = ifsHeaderListTable[a].GoodParts;
                                ViewData["BadPartsEOL" + ifsHeaderListTable[a].Muszak + a] = ifsHeaderListTable[a].BadParts;
                            }
                            else
                            {
                                ViewData["TypeName" + alertsDBContext[i].Muszak + a] = "";
                                ViewData["GoodParts" + alertsDBContext[i].Muszak + a] = "";
                                ViewData["BadPartsEOL" + alertsDBContext[i].Muszak + a] = "";
                            }
                        }
                    }
                    else
                    {
                        for (int a = 0; a < 5; a++)
                        {
                            ViewData["TypeName" + alertsDBContext[i].Muszak + a] = "";
                            ViewData["GoodParts" + alertsDBContext[i].Muszak + a] = "";
                            ViewData["BadPartsEOL" + alertsDBContext[i].Muszak + a] = "";
                        }
                    }
                    ViewData["IFSHeaderListTable"] = ifsHeaderListTable;
                }
            }
            return View(alertsDBContext);
        }

        public async Task<IfsHeaderTable> GetIFSHeaderTableAsync(string lineid, DateTime? starttime, int? shiftid)
        {
            //if (lineid == null || starttime == null || shiftid == null || _context.IfsHeaderTable == null)
            //{
            //    return null;
            //}
            var ifsHeaderTable = await _context.IfsHeaderTable.Where(a => (a.LineId == lineid & a.StartTime == starttime & a.ShiftId == shiftid)).FirstOrDefaultAsync();
            ViewData["IFSHeaderTable"] = ifsHeaderTable;
            return ifsHeaderTable;
        }

        public JsonResult GetIFSValueSearchTable(string lineid, DateTime? starttime, int? shiftid)
        {
            //if (lineid == null || starttime == null || shiftid == null || _context.IfsHeaderTable == null)
            //{
            //    return null;
            //}
            var viewifsvaluesearchtable = _context.ViewIfsValueSearchTable.Where(a => (a.LineId == lineid & a.StartTime == starttime & a.ShiftId == shiftid)).FirstOrDefault();
            ViewData["ViewIFSValueSearchTable"] = viewifsvaluesearchtable;
            return Json(viewifsvaluesearchtable);
        }


        public async Task<IActionResult> EditAsync(string lineid, DateTime? starttime, int? shiftid)
        {
            if (lineid == null || starttime == null || shiftid == null || _context.IfsHeaderTable == null)
            {
                return NotFound();
            }
            var ifsShiftTable = await _context.ShiftsTable.Where(a => (a.LineId == lineid & a.Starttime == starttime & a.Muszak == shiftid)).FirstOrDefaultAsync();
            if (ifsShiftTable == null)
            {
                return NotFound();
            }
            var ifsHeaderTable = await _context.IfsHeaderTable.Where(a => (a.LineId == lineid & a.StartTime == starttime & a.ShiftId == shiftid)).FirstOrDefaultAsync();
            if (ifsHeaderTable == null)
            {
                DateTime start = ifsShiftTable.Starttime;
                DateTime end = ifsShiftTable.Starttime.Date.AddHours(14).AddMinutes(20);
                switch (ifsShiftTable.Muszak)
                {
                    case 1: //Shift #1
                        start = ifsShiftTable.Starttime;
                        end = ifsShiftTable.Starttime.Date.AddHours(14).AddMinutes(20);
                        break;
                    case 2: //Shift #2
                        start = ifsShiftTable.Starttime.Date.AddHours(14).AddMinutes(20);
                        end = ifsShiftTable.Starttime.Date.AddHours(22).AddMinutes(40);
                        break;
                    case 3: //Shift #3
                        start = ifsShiftTable.Starttime.Date.AddHours(22).AddMinutes(40);
                        end = ifsShiftTable.Starttime.AddDays(1);
                        break;
                }
                DateTime now = DateTime.Now;
                if (now > start & now < end)
                {
                    end = now;
                }
                ifsHeaderTable = new IfsHeaderTable()
                {
                    LineId = lineid,
                    StartTime = starttime.Value,
                    ShiftId = shiftid.Value,
                    AllParts = ifsShiftTable.Gyartott.Value,
                    BadParts = ifsShiftTable.PGyartott.Value,
                    GoodParts = ifsShiftTable.Gyartott.Value - ifsShiftTable.PGyartott.Value,
                    WorkingHours = (end - start - new TimeSpan(0, 20, 0)).TotalHours,
                    SettingsParts = 0,
                    DroppedParts = 0,
                    FailureRate = ifsShiftTable.Gyartott.Value > 0 ? 100 * (double)ifsShiftTable.PGyartott.Value / (double)ifsShiftTable.Gyartott.Value : 0,
                };
                _context.Add(ifsHeaderTable);
                await _context.SaveChangesAsync();
            }
            ViewData["Id"] = new SelectList(_context.IfsValueTable, "IfsheaderId", "IfsheaderId");
            ViewData["ShiftId"] = new SelectList(_context.IfsMuszakTable, "Id", "Description");
            return PartialView("_EditModalPartion", ifsHeaderTable);
        }

        // POST: IfsHeaderTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,LineId,StartTime,ShiftId,AllParts,BadParts,GoodParts,FailureRate,SettingsParts,DroppedParts,WorkingHours")] IfsHeaderTable ifsHeaderTable)
        {
            if (ModelState.IsValid)
            {
                ifsHeaderTable.FailureRate = ifsHeaderTable.AllParts.Value > 0 ? 100 * (double)ifsHeaderTable.BadParts.Value / (double)ifsHeaderTable.AllParts.Value : 0;
                _context.Update(ifsHeaderTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id"] = new SelectList(_context.IfsValueTable, "IfsheaderId", "IfsheaderId", ifsHeaderTable.Id);
            ViewData["ShiftId"] = new SelectList(_context.IfsMuszakTable, "Id", "Description", ifsHeaderTable.ShiftId);
            return PartialView("_EditModalPartion", ifsHeaderTable);
        }
    }
}

﻿using System;
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
    public class MesproductPlanTypesController : Controller
    {
        private readonly EISDBContext _context;

        public MesproductPlanTypesController(EISDBContext context)
        {
            _context = context;
        }

        // GET: MesproductPlanTypes
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
            return View(await _context.MesproductPlanType.ToListAsync());
        }

        // GET: MesproductPlanTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MesproductPlanType == null)
            {
                return NotFound();
            }

            var mesproductPlanType = await _context.MesproductPlanType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mesproductPlanType == null)
            {
                return NotFound();
            }

            return View(mesproductPlanType);
        }

        // GET: MesproductPlanTypes/Create
        public IActionResult Create()
        {
            MesproductPlanType mesproductPlanType = new MesproductPlanType()
            {
                Id = 0,
                Description = "Töltsd ki",
                DescriptionEn = "Fill in"
            };
            return PartialView("_CreateModalPartion", mesproductPlanType);
        }

        // POST: MesproductPlanTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,DescriptionEn")] MesproductPlanType mesproductPlanType)
        {
            if (ModelState.IsValid)
            {
                if (mesproductPlanType.DescriptionEn.IsNullOrEmpty())
                {
                    mesproductPlanType.DescriptionEn = "";
                }
                if (mesproductPlanType.Description.IsNullOrEmpty())
                {
                    mesproductPlanType.Description = "";
                }
                _context.Add(mesproductPlanType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return PartialView("_CreateModalPartion", mesproductPlanType);
        }

        // GET: MesproductPlanTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MesproductPlanType == null)
            {
                return NotFound();
            }

            var mesproductPlanType = await _context.MesproductPlanType.FindAsync(id);
            if (mesproductPlanType == null)
            {
                return NotFound();
            }
            return PartialView("_EditModalPartion", mesproductPlanType);
        }

        // POST: MesproductPlanTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,DescriptionEn")] MesproductPlanType mesproductPlanType)
        {
            if (id != mesproductPlanType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (mesproductPlanType.DescriptionEn.IsNullOrEmpty())
                    {
                        mesproductPlanType.DescriptionEn = "";
                    }
                    if (mesproductPlanType.Description.IsNullOrEmpty())
                    {
                        mesproductPlanType.Description = "";
                    }
                    _context.Update(mesproductPlanType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MesproductPlanTypeExists(mesproductPlanType.Id))
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
            return PartialView("_EditModalPartion", mesproductPlanType);
        }

        // GET: MesproductPlanTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MesproductPlanType == null)
            {
                return NotFound();
            }

            var mesproductPlanType = await _context.MesproductPlanType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mesproductPlanType == null)
            {
                return NotFound();
            }

            return View(mesproductPlanType);
        }

        // POST: MesproductPlanTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MesproductPlanType == null)
            {
                return Problem("Entity set 'EISDBContext.MesproductPlanType'  is null.");
            }
            var mesproductPlanType = await _context.MesproductPlanType.FindAsync(id);
            if (mesproductPlanType != null)
            {
                _context.MesproductPlanType.Remove(mesproductPlanType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MesproductPlanTypeExists(int id)
        {
          return _context.MesproductPlanType.Any(e => e.Id == id);
        }
    }
}

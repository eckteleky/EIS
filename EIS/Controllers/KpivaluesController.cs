using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EIS.EISModels;
using Microsoft.AspNetCore.Http;
using EIS.Helpers;
using EIS.Models;
using Microsoft.AspNetCore.Localization;
using ChartJSCore.Helpers;
using ChartJSCore.Models;
using System.Drawing;
using Newtonsoft.Json.Linq;

namespace EIS.Controllers
{
    public class KpivaluesController : Controller
    {
        private readonly EISDBContext _context;

        public KpivaluesController(EISDBContext context)
        {
            _context = context;
        }

        // GET: Kpivalues
        public async Task<IActionResult> Index()
        {
            HttpContext.Session.SetInt32("LZGroup", 0);
            HttpContext.Session.SetInt32("ShiftGroup", 0);
            HttpContext.Session.SetInt32("LineIDGroup", 0);
            HttpContext.Session.SetInt32("DayGroup", 2);
            HttpContext.Session.SetInt32("AddGroup", 1);
            HttpContext.Session.SetInt32("HeaderGroup", 1);
            #region GetServerDatas v1.2
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
            if (
                HttpContext.Session.GetInt32("Year") == null |
                HttpContext.Session.GetInt32("Company") == null)
            {
                Headers header = new()
                {
                    MyFields = false,
                    SearchString = ""
                };
                SessionHelper.SetObjectAsJson(HttpContext.Session, "header", header);
            }
            HttpContext.Session.SetInt32("Year", year);
            HttpContext.Session.SetInt32("Company", company);
            #endregion
            var eISDBContext = await _context.ViewKpivalues.Where(a => a.Year == year & (a.Visible.HasValue ? a.Visible.Value : false )==true).OrderBy(b => b.SortedId).ToListAsync();
            ViewData["Datas"] = await _context.ViewKpivalues.Where(a => a.Year == year & (a.Visible.HasValue ? a.Visible.Value : false) == true).OrderBy(b => b.SortedId).ToListAsync();
            var requestCulture = HttpContext.Features.Get<IRequestCultureFeature>();
            List<Chart> lineScatterChart = new List<Chart>();
            foreach (var item in eISDBContext)
            {
                float[] values = new float[15];
                values[0] = item.UnitNameEn == "%" ? (float)item.Value01.Value * 100 : (float)item.Value01.Value;
                values[1] = item.UnitNameEn == "%" ? (float)item.Value02.Value * 100 : (float)item.Value02.Value;
                values[2] = item.UnitNameEn == "%" ? (float)item.Value03.Value * 100 : (float)item.Value03.Value;
                values[3] = item.UnitNameEn == "%" ? (float)item.Value04.Value * 100 : (float)item.Value04.Value;
                values[4] = item.UnitNameEn == "%" ? (float)item.Value05.Value * 100 : (float)item.Value05.Value;
                values[5] = item.UnitNameEn == "%" ? (float)item.Value06.Value * 100 : (float)item.Value06.Value;
                values[6] = item.UnitNameEn == "%" ? (float)item.Value07.Value * 100 : (float)item.Value07.Value;
                values[7] = item.UnitNameEn == "%" ? (float)item.Value08.Value * 100 : (float)item.Value08.Value;
                values[8] = item.UnitNameEn == "%" ? (float)item.Value09.Value * 100 : (float)item.Value09.Value;
                values[9] = item.UnitNameEn == "%" ? (float)item.Value10.Value * 100 : (float)item.Value10.Value;
                values[10] = item.UnitNameEn == "%" ? (float)item.Value11.Value * 100 : (float)item.Value11.Value;
                values[11] = item.UnitNameEn == "%" ? (float)item.Value12.Value * 100 : (float)item.Value12.Value;
                float[] targets = new float[15];
                targets[0] = item.UnitNameEn == "%" ? (float)item.Target01.Value * 100 : (float)item.Target01.Value;
                targets[1] = item.UnitNameEn == "%" ? (float)item.Target02.Value * 100 : (float)item.Target02.Value;
                targets[2] = item.UnitNameEn == "%" ? (float)item.Target03.Value * 100 : (float)item.Target03.Value;
                targets[3] = item.UnitNameEn == "%" ? (float)item.Target04.Value * 100 : (float)item.Target04.Value;
                targets[4] = item.UnitNameEn == "%" ? (float)item.Target05.Value * 100 : (float)item.Target05.Value;
                targets[5] = item.UnitNameEn == "%" ? (float)item.Target06.Value * 100 : (float)item.Target06.Value;
                targets[6] = item.UnitNameEn == "%" ? (float)item.Target07.Value * 100 : (float)item.Target07.Value;
                targets[7] = item.UnitNameEn == "%" ? (float)item.Target08.Value * 100 : (float)item.Target08.Value;
                targets[8] = item.UnitNameEn == "%" ? (float)item.Target09.Value * 100 : (float)item.Target09.Value;
                targets[9] = item.UnitNameEn == "%" ? (float)item.Target10.Value * 100 : (float)item.Target10.Value;
                targets[10] = item.UnitNameEn == "%" ? (float)item.Target11.Value * 100 : (float)item.Target11.Value;
                targets[11] = item.UnitNameEn == "%" ? (float)item.Target12.Value * 100 : (float)item.Target12.Value;
                Chart chart = GenerateLineScatterChart(item.Id, year, values, targets, @requestCulture.RequestCulture.UICulture.Name, false);
                lineScatterChart.Add(chart);
            }
            ViewData["chart"] = lineScatterChart;
            return View(eISDBContext);
        }

        private static Chart GenerateLineScatterChart(int id, int year, float[] values, float[] targets, string Language, bool dual)
        {
            Chart chart = new Chart();
            chart.Type = ChartJSCore.Models.Enums.ChartType.Bar;

            ChartJSCore.Models.Data data = new ChartJSCore.Models.Data();

            data.Labels = new List<string>();

            string[] months = { "", "", "", "", "", "", "", "", "", "", "", "", "" };

            BarDataset dataset = new BarDataset()
            {
                Label = "",// "VALUES " + year.ToString("0000"),
                Data = new List<double?>(),
                BackgroundColor = new List<ChartColor>() { ChartColor.FromRgba(185, 212, 60, 1) },
                BorderColor = new List<ChartColor>() { ChartColor.FromRgb(185, 212, 60) },
                MinBarLength = 0,
                BarThickness = 10
            };

            for (int i = 0; i < 12; i++)
            {
                data.Labels.Add(months[i+1]);
                dataset.Data.Add(Math.Round(values[i], 2));
            }

            data.Datasets = new List<Dataset>();
            data.Datasets.Add(dataset);

            if (dual)
            {
                LineScatterDataset dataset100 = new LineScatterDataset()
                {
                    Label = Language == "hu-HU" ? "ELVÁRT EREDMÉNY" : "EXPECTED TARGET",
                    Data = new List<LineScatterData>(),
                    BackgroundColor = new List<ChartColor> { ChartColor.FromRgba(127, 127, 127, 0.1) },
                    BorderColor = new List<ChartColor> { ChartColor.FromRgba(127, 127, 127, 0.2) },
                    PointRadius = new List<int>() { 5 },
                    PointHitRadius = new List<int>() { 5 },
                    PointStyle = new List<string>() { "crossRot" },
                    PointBackgroundColor = new List<ChartColor>() { ChartColor.FromRgba(127, 127, 127, 1) },
                    PointBorderColor = new List<ChartColor>() { ChartColor.FromRgba(127, 127, 127, 1) },
                };

                for (int i = 0; i < 12; i++)
                {
                    LineScatterData scatterData1 = new LineScatterData();
                    scatterData1.X = months[i+1];
                    scatterData1.Y = Math.Round(targets[i], 2).ToString();
                    dataset100.Data.Add(scatterData1);
                }
                data.Datasets.Add(dataset100);
            }

            chart.Data = data;

            var options = new Options
            {
                Scales = new Dictionary<string, Scale>()
                {
                    { "y", new Scale()
                        {
                            Grid = new Grid()
                            {
                                Offset = true
                            },
                            Display = false
                        }
                    },
                    { "x", new Scale()
                        {
                            Grid = new Grid()
                            {
                                Offset = true
                            },
                            Display=false
                        }
                    },
                }
            };

            chart.Options = options;
 
            return chart;
        }

        // GET: Kpivalues/Create
        public async Task<IActionResult> CreateAsync()
        {
            #region GetServerDatas v1.2
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
            if (
                HttpContext.Session.GetInt32("Year") == null |
                HttpContext.Session.GetInt32("Company") == null)
            {
                Headers header = new()
                {
                    MyFields = false,
                    SearchString = ""
                };
                SessionHelper.SetObjectAsJson(HttpContext.Session, "header", header);
            }
            HttpContext.Session.SetInt32("Year", year);
            HttpContext.Session.SetInt32("Company", company);
            #endregion
            Kpiheader kpiheader = await _context.Kpiheader.OrderBy(a=>a.Id).FirstOrDefaultAsync();
            Kpivalues kpivalues = new Kpivalues()
            {
                Id = 0,
                Year = year,
                KpiheaderId= kpiheader.Id,
                Kpiheader = kpiheader,
                SortedId=1,
                Visible=true
            };
            ViewData["KpiheaderId"] = new SelectList(_context.Kpiheader, "Id", "DescriptionEn", kpivalues.KpiheaderId);
            return PartialView("_CreateModalPartion", kpivalues);
        }

        // POST: Kpivalues/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Year,KpiheaderId,TargetPrev,Target01,Target02,Target03,Target04,Target05,Target06,Target07,Target08,Target09,Target10,Target11,Target12,TargetNext,ValuePrev,Value01,Value02,Value03,Value04,Value05,Value06,Value07,Value08,Value09,Value10,Value011,Value012,ValueNext,SortedId,Visible")] Kpivalues kpivalues)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kpivalues);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KpiheaderId"] = new SelectList(_context.Kpiheader, "Id", "DescriptionEn", kpivalues.KpiheaderId);
            return PartialView("_CreateModalPartion", kpivalues);
        }

        // GET: Kpivalues/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Kpivalues == null)
            {
                return NotFound();
            }

            var kpivalues = await _context.Kpivalues.FindAsync(id);
            if (kpivalues == null)
            {
                return NotFound();
            }
            ViewData["KpiheaderId"] = new SelectList(_context.Kpiheader, "Id", "DescriptionEn", kpivalues.KpiheaderId);
            return PartialView("_EditModalPartion", kpivalues);
        }

        // POST: Kpivalues/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Year,KpiheaderId,TargetPrev,Target01,Target02,Target03,Target04,Target05,Target06,Target07,Target08,Target09,Target10,Target11,Target12,TargetNext,ValuePrev,Value01,Value02,Value03,Value04,Value05,Value06,Value07,Value08,Value09,Value10,Value011,Value012,ValueNext,SortedId,Visible")] Kpivalues kpivalues)
        {
            if (id != kpivalues.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kpivalues);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KpivaluesExists(kpivalues.Id))
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
            ViewData["KpiheaderId"] = new SelectList(_context.Kpiheader, "Id", "DescriptionEn", kpivalues.KpiheaderId);
            return PartialView("_EditModalPartion", kpivalues);
        }

        private bool KpivaluesExists(int id)
        {
          return _context.Kpivalues.Any(e => e.Id == id);
        }
    }
}

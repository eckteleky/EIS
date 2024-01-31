using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EIS.EISModels;
using ChartJSCore.Models;
using ChartJSCore.Helpers;
using Microsoft.AspNetCore.Http;

namespace EIS.Controllers
{
    public class ShiftsTablesController : Controller
    {
        private readonly EISDBContext _context;

        public ShiftsTablesController(EISDBContext context)
        {
            _context = context;
        }

        // GET: ShiftsTables
        public async Task<IActionResult> Index()
        {
            HttpContext.Session.SetInt32("LZGroup", 0);
            HttpContext.Session.SetInt32("LineIDGroup", 1);
            HttpContext.Session.SetInt32("ShiftGroup", 1);
            HttpContext.Session.SetInt32("DayGroup", 1);
            HttpContext.Session.SetInt32("AddGroup", 0);
            HttpContext.Session.SetInt32("HeaderGroup", 1);
            Chart verticalBarChart = GenerateVerticalBarChart();
            ViewData["VerticalBarChart"] = verticalBarChart;
            return View(await _context.ShiftsTable.Where(a => (a.Starttime > DateTime.Today.AddDays(-1) & a.Starttime < DateTime.Today.AddDays(0))).ToListAsync());
        }

        private static Chart GenerateVerticalBarChart()
        {
            Chart chart = new Chart();
            chart.Type = ChartJSCore.Models.Enums.ChartType.Bar;

            ChartJSCore.Models.Data data = new ChartJSCore.Models.Data();
            data.Labels = new List<string>() { "06:00", "07:00", "08:00", "09:00", "10:00", "11:00", "12:00", "13:00", "14:00", "15:00", "16:00", "17:00", "18:00", "19:00", "20:00", "21:00", "22:00", "23:00", "00:00", "01:00", "02:00", "03:00", "04:00", "05:00" };

            BarDataset dataset = new BarDataset()
            {
                Label = "ÓRÁNKÉNTI JÓ DARABSZÁM",
                Data = new List<double?>() { 152, 241, 140, 134, 216, 245, 247, 129,
                                               0,   0,   0,   0,   0,   0,   0,   0,
                                               0,   0,   0,   0,   0,   0,   0,  24,
                                           },
                BackgroundColor = new List<ChartColor> { ChartColor.FromRgba(185, 212, 60, 1) },
                BorderColor = new List<ChartColor> { ChartColor.FromRgb(185, 212, 60) },
                //BorderWidth = new List<int>() { 1 },
                //BarPercentage = 0.5,
                //BarThickness = 6,
                //MaxBarThickness = 8,
                MinBarLength = 2,
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
                            Stacked = false
                        }
                    },
                    { "x", new Scale()
                        {
                            Grid = new Grid()
                            {
                                Offset = false
                            },
                            Stacked = false
                        }
                    },
                },
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
    }
}

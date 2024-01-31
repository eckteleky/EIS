using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EIS.EISModels;
using Microsoft.AspNetCore.Http;

namespace EIS.Controllers
{
    public class SystemParamTablesController : Controller
    {
        private readonly EISDBContext _context;

        public SystemParamTablesController(EISDBContext context)
        {
            _context = context;
        }

        // GET: SystemParamTables
        public async Task<IActionResult> Index()
        {
            HttpContext.Session.SetInt32("LZGroup", 0);
            HttpContext.Session.SetInt32("DayGroup", 1);
            HttpContext.Session.SetInt32("AddGroup", 1);
            HttpContext.Session.SetInt32("HeaderGroup", 1);
            return View(await _context.SystemParamTable.ToListAsync());
        }

        // GET: SystemParamTables/Create
        public IActionResult Create()
        {
            SystemParamTable systemParamTable = new SystemParamTable();
            return PartialView("_CreateModalPartion", systemParamTable);
        }

        // POST: SystemParamTables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LineId,Cycletime,Minpercent,Meanpercent,M1start,M1end,M1s01,M1e01,M1s02,M1e02,M1s03,M1e03,M1s04,M1e04,M1s05,M1e05,M1s06,M1e06,M1s07,M1e07,M1s08,M1e08,M1s09,M1e09,M2start,M2end,M2s01,M2e01,M2s02,M2e02,M2s03,M2e03,M2s04,M2e04,M2s05,M2e05,M2s06,M2e06,M2s07,M2e07,M2s08,M2e08,M2s09,M2e09,M3start,M3end,M3s01,M3e01,M3s02,M3e02,M3s03,M3e03,M3s04,M3e04,M3s05,M3e05,M3s06,M3e06,M3s07,M3e07,M3s08,M3e08,M3s09,M3e09,Wasterate,Linename,Linelogo,Linelogo1,Id")] SystemParamTable systemParamTable)
        {
            if (ModelState.IsValid)
            {
                _context.Add(systemParamTable);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return PartialView("_CreateModalPartion", systemParamTable);
        }

        // GET: SystemParamTables/Edit/5
        public async Task<IActionResult> Edit(string LineId)
        {
            if (LineId == null || _context.SystemParamTable == null)
            {
                return NotFound();
            }

            var systemParamTable = await _context.SystemParamTable.FindAsync(LineId);//.FindAsync("GBM Line2");//.FindAsync(id);
            if (systemParamTable == null)
            {
                return NotFound();
            }
            return PartialView("_EditModalPartion", systemParamTable);
        }

        // POST: SystemParamTables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string LineId, [Bind("LineId,Cycletime,Minpercent,Meanpercent,M1start,M1end,M1s01,M1e01,M1s02,M1e02,M1s03,M1e03,M1s04,M1e04,M1s05,M1e05,M1s06,M1e06,M1s07,M1e07,M1s08,M1e08,M1s09,M1e09,M2start,M2end,M2s01,M2e01,M2s02,M2e02,M2s03,M2e03,M2s04,M2e04,M2s05,M2e05,M2s06,M2e06,M2s07,M2e07,M2s08,M2e08,M2s09,M2e09,M3start,M3end,M3s01,M3e01,M3s02,M3e02,M3s03,M3e03,M3s04,M3e04,M3s05,M3e05,M3s06,M3e06,M3s07,M3e07,M3s08,M3e08,M3s09,M3e09,Wasterate,Linename,Linelogo,Linelogo1,Id")] SystemParamTable systemParamTable)
        {
            if (LineId != systemParamTable.LineId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(systemParamTable);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SystemParamTableExists(systemParamTable.LineId))
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
            return PartialView("_EditModalPartion", systemParamTable);
        }

        private bool SystemParamTableExists(string id)
        {
          return _context.SystemParamTable.Any(e => e.LineId == id);
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EIS.Controllers
{
    public class KPITablesController : Controller
    {
        public IActionResult Index()
        {
            HttpContext.Session.SetInt32("LZGroup", 0);
            HttpContext.Session.SetInt32("ShiftGroup", 0);
            HttpContext.Session.SetInt32("LineIDGroup", 0);
            HttpContext.Session.SetInt32("DayGroup", 2);
            HttpContext.Session.SetInt32("AddGroup", 0);
            HttpContext.Session.SetInt32("HeaderGroup", 1);
            return View();
        }
    }
}

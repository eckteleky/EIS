﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EIS.Controllers
{
    public class EmptyController : Controller
    {
        public IActionResult Index(int? id)
        {
            HttpContext.Session.SetInt32("LZGroup", 0);
            HttpContext.Session.SetInt32("DayGroup", 3);
            HttpContext.Session.SetInt32("AddGroup", 0);
            HttpContext.Session.SetInt32("HeaderGroup", 1);
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace EIS.Models
{
    public class Headers
    {
        public bool MyFields { get; set; }   
        public string SearchString { get; set; }
    }
    public class Selects
    {
        public int Selected { get; set; }
        public List<SelectListItem> Fields { get; set; }
        public string Actual { get; set; }
        public int LastID { get; set; }
    }
}

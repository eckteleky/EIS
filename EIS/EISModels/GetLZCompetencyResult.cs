﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EIS.EISModels
{
    public partial class GetLZCompetencyResult
    {
        public int DolgozoID { get; set; }
        public string DolgozoNeve { get; set; }
        public int Muveletszam { get; set; }
        public string MuveletReszlet { get; set; }
        public string Description { get; set; }
        public bool Interval { get; set; }
        public string Kefetartoszam { get; set; }
        public string Tipus { get; set; }
        public int? OsszesDarab { get; set; }
        public double? OsszesOra { get; set; }
        public int? Darab { get; set; }
        public DateTime? Last { get; set; }
        public string CT { get; set; }
    }
}

﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EIS.EISModels
{
    public partial class ParetoReadResult
    {
        public string LineID { get; set; }
        public string TypeID { get; set; }
        public string Description { get; set; }
        public string DescriptionEN { get; set; }
        [Column("DT[hours]")]
        public double? DThours { get; set; }
        public long? resource { get; set; }
        public double? activity_percentage { get; set; }
    }
}

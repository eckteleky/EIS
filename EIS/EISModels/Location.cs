﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace EIS.EISModels;

public partial class Location
{
    public int Id { get; set; }

    public string Description { get; set; }

    public virtual ICollection<SystemParamTable> SystemParamTable { get; set; } = new List<SystemParamTable>();
}
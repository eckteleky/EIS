﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace EIS.EISModels;

public partial class Kpiresponsible
{
    public int Id { get; set; }

    public string Name { get; set; }

    public virtual ICollection<Kpiheader> Kpiheader { get; set; } = new List<Kpiheader>();
}
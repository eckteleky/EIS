﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace EIS.EISModels;

public partial class EasyMesttstypes
{
    public int Id { get; set; }

    public int TtstypeId { get; set; }

    public string TtstypeName { get; set; }

    public string TtsshortName { get; set; }

    public int SystemParamId { get; set; }

    public TimeSpan ChangeTime { get; set; }

    public double CycleTime { get; set; }

    public bool Enabled { get; set; }

    public virtual ICollection<EasyMesjob> EasyMesjob { get; set; } = new List<EasyMesjob>();

    public virtual SystemParamTable SystemParam { get; set; }
}
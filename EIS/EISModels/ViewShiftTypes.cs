﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace EIS.EISModels;

public partial class ViewShiftTypes
{
    public string LineId { get; set; }

    public DateTime StartTime { get; set; }

    public int Muszak { get; set; }

    public int TypeId { get; set; }

    public string TypeName { get; set; }

    public DateTime ChangeTime { get; set; }

    public int? CurrentParts { get; set; }

    public int? TargetParts { get; set; }

    public int? GoodParts { get; set; }

    public int? BadParts { get; set; }

    public int? ActiveWts { get; set; }

    public int? TargetWts { get; set; }

    public int? DownTime { get; set; }

    public int? WorkingTime { get; set; }

    public double? WasteRate { get; set; }

    public double? Oee { get; set; }

    public double? RatR { get; set; }

    public double? Ct { get; set; }

    public double? Cct { get; set; }

    public int? P1 { get; set; }

    public int? P2 { get; set; }

    public DateTime? ShiftFrom { get; set; }

    public DateTime? ShiftTo { get; set; }
}
﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace EIS.EISModels;

public partial class ErrorTable
{
    public DateTime TimeStamp { get; set; }

    public string LineId { get; set; }

    public int StationId { get; set; }

    public int ErrorCode { get; set; }

    public double? DownTime { get; set; }

    public DateTime? EndTime { get; set; }
}
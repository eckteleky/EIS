﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace EIS.EISModels;

public partial class LzDolgozoTable
{
    public int Id { get; set; }

    public int DolgozoId { get; set; }

    public string DolgozoNeve { get; set; }

    public int Irsz { get; set; }

    public string Varos { get; set; }

    public string UtcaHrsz { get; set; }

    public string Telefonszam { get; set; }

    public string Megjegyzes { get; set; }

    public bool Active { get; set; }

    public int Munkaido { get; set; }

    public DateTime BeDat { get; set; }

    public DateTime? KiDat { get; set; }

    public DateTime SzDat { get; set; }

    public string Search { get; set; }
}
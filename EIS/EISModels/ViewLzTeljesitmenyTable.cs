﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace EIS.EISModels;

public partial class ViewLzTeljesitmenyTable
{
    public int Id { get; set; }

    public int DolgozoId { get; set; }

    public string DolgozoNeve { get; set; }

    public DateTime Datum { get; set; }

    public int? Darab { get; set; }

    public int? HibasDarab { get; set; }

    public double Ora { get; set; }

    public int? Muveletszam { get; set; }

    public string Muveletreszlet { get; set; }

    public string Description { get; set; }

    public bool Interval { get; set; }

    public string Kefetartoszam { get; set; }

    public string Tipus { get; set; }

    public string Megjegyzes { get; set; }

    public string Muszak { get; set; }

    public int MuveletId { get; set; }

    public int KefetartoId { get; set; }

    public int MuszakId { get; set; }
}
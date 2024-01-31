﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace EIS.EISModels;

public partial class Kpiheader
{
    public int Id { get; set; }

    public int ProcessId { get; set; }

    public string DescriptionHu { get; set; }

    public string DescriptionEn { get; set; }

    public string DescriptionDe { get; set; }

    public int ResponsibleId { get; set; }

    public int DataSourceId { get; set; }

    public int UnitId { get; set; }

    public int ExpectationId { get; set; }

    public int SummantionId { get; set; }

    public virtual KpidataSource DataSource { get; set; }

    public virtual Kpiexpectation Expectation { get; set; }

    public virtual ICollection<Kpivalues> Kpivalues { get; set; } = new List<Kpivalues>();

    public virtual Kpiprocess Process { get; set; }

    public virtual Kpiresponsible Responsible { get; set; }

    public virtual Kpisummantion Summantion { get; set; }

    public virtual Kpiunit Unit { get; set; }
}
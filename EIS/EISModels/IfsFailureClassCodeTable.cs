﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace EIS.EISModels;

public partial class IfsFailureClassCodeTable
{
    public int Id { get; set; }

    public string LineId { get; set; }

    public string ClassCodeId { get; set; }

    public string Description { get; set; }

    public string DescriptionEn { get; set; }

    public virtual ICollection<IfsFailureCodeTable> IfsFailureCodeTable { get; set; } = new List<IfsFailureCodeTable>();
}
﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace EIS.EISModels;

public partial class IfsValueTable
{
    public int Id { get; set; }

    public int IfsheaderId { get; set; }

    public int FailureCodeId { get; set; }

    public int? BadParts { get; set; }

    public virtual IfsFailureCodeTable FailureCode { get; set; }

    public virtual IfsHeaderTable Ifsheader { get; set; }
}
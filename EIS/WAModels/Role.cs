﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace EIS.EISModels;

public partial class Role
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string NormalizedName { get; set; }

    public string ConcurrencyStamp { get; set; }

    public virtual ICollection<RoleClaims> RoleClaims { get; set; } = new List<RoleClaims>();

    public virtual ICollection<User> User { get; set; } = new List<User>();
}
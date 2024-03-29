﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace EIS.EISModels;

public partial class User
{
    public string Id { get; set; }

    public string UserName { get; set; }

    public string NormalizedUserName { get; set; }

    public string Email { get; set; }

    public string NormalizedEmail { get; set; }

    public bool EmailConfirmed { get; set; }

    public string PasswordHash { get; set; }

    public string SecurityStamp { get; set; }

    public string ConcurrencyStamp { get; set; }

    public string PhoneNumber { get; set; }

    public bool PhoneNumberConfirmed { get; set; }

    public bool TwoFactorEnabled { get; set; }

    public DateTimeOffset? LockoutEnd { get; set; }

    public bool LockoutEnabled { get; set; }

    public int AccessFailedCount { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public byte[] ProfilePicture { get; set; }

    public int UsernameChangeLimit { get; set; }

    public virtual ICollection<UserClaims> UserClaims { get; set; } = new List<UserClaims>();

    public virtual ICollection<UserLogins> UserLogins { get; set; } = new List<UserLogins>();

    public virtual ICollection<UserTokens> UserTokens { get; set; } = new List<UserTokens>();

    public virtual ICollection<Role> Role { get; set; } = new List<Role>();
}
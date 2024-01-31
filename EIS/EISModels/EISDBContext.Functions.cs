﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Linq;
using EIS.EISModels;

namespace EIS.EISModels
{
    public partial class EISDBContext
    {

        [DbFunction("fn_getbomid", "dbo")]
        public static string fn_getbomid(int? bomid)
        {
            throw new NotSupportedException("This method can only be called from Entity Framework Core queries");
        }

        [DbFunction("fn_getitemsid", "dbo")]
        public static string fn_getitemsid(int? itemsid)
        {
            throw new NotSupportedException("This method can only be called from Entity Framework Core queries");
        }

        [DbFunction("fn_getlineid", "dbo")]
        public static string fn_getlineid(int? systemparamid)
        {
            throw new NotSupportedException("This method can only be called from Entity Framework Core queries");
        }

        [DbFunction("GetLZCategoryList", "dbo")]
        public IQueryable<GetLZCategoryListResult> GetLZCategoryList(string Year, string Month)
        {
            return FromExpression(() => GetLZCategoryList(Year, Month));
        }

        [DbFunction("GetLZCategoryListEn", "dbo")]
        public IQueryable<GetLZCategoryListEnResult> GetLZCategoryListEn(string Year, string Month)
        {
            return FromExpression(() => GetLZCategoryListEn(Year, Month));
        }

        [DbFunction("GetLZCompetency", "dbo")]
        public IQueryable<GetLZCompetencyResult> GetLZCompetency(int? DolgozoId)
        {
            return FromExpression(() => GetLZCompetency(DolgozoId));
        }

        [DbFunction("GetLZDolgozo", "dbo")]
        public IQueryable<GetLZDolgozoResult> GetLZDolgozo(int? DolgozoId)
        {
            return FromExpression(() => GetLZDolgozo(DolgozoId));
        }

        [DbFunction("GetLZKefetartoId", "dbo")]
        public IQueryable<GetLZKefetartoIdResult> GetLZKefetartoId(int? MuveletId)
        {
            return FromExpression(() => GetLZKefetartoId(MuveletId));
        }

        [DbFunction("GetLZMuveletId", "dbo")]
        public IQueryable<GetLZMuveletIdResult> GetLZMuveletId(int? MuveletId)
        {
            return FromExpression(() => GetLZMuveletId(MuveletId));
        }

        [DbFunction("ParetoRead", "dbo")]
        public IQueryable<ParetoReadResult> ParetoRead(string LineID, DateTime? from, DateTime? to)
        {
            return FromExpression(() => ParetoRead(LineID, from, to));
        }

        [DbFunction("ViewShiftTable", "dbo")]
        public IQueryable<ViewShiftTableResult> ViewShiftTable(string LineID, int? StationID, int? muszak, DateTime? from, DateTime? to)
        {
            return FromExpression(() => ViewShiftTable(LineID, StationID, muszak, from, to));
        }

        [DbFunction("ViewShiftView", "dbo")]
        public IQueryable<ViewShiftViewResult> ViewShiftView(string LineID, int? muszak, int? stationid, DateTime? from)
        {
            return FromExpression(() => ViewShiftView(LineID, muszak, stationid, from));
        }

        protected void OnModelCreatingGeneratedFunctions(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GetLZCategoryListResult>().HasNoKey();
            modelBuilder.Entity<GetLZCategoryListEnResult>().HasNoKey();
            modelBuilder.Entity<GetLZCompetencyResult>().HasNoKey();
            modelBuilder.Entity<GetLZDolgozoResult>().HasNoKey();
            modelBuilder.Entity<GetLZKefetartoIdResult>().HasNoKey();
            modelBuilder.Entity<GetLZMuveletIdResult>().HasNoKey();
            modelBuilder.Entity<ParetoReadResult>().HasNoKey();
            modelBuilder.Entity<ViewShiftTableResult>().HasNoKey();
            modelBuilder.Entity<ViewShiftViewResult>().HasNoKey();
        }
    }
}

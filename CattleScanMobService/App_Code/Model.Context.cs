﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using System.Linq;

public partial class DB_A4A060_csEntities : DbContext
{
    public DB_A4A060_csEntities()
        : base("name=DB_A4A060_csEntities")
    {
    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        throw new UnintentionalCodeFirstException();
    }

    public virtual DbSet<Mob_Token> Mob_Token { get; set; }
    public virtual DbSet<Mob_Users> Mob_Users { get; set; }
    public virtual DbSet<Farm> Farms { get; set; }
    public virtual DbSet<MeasData> MeasDatas { get; set; }
    public virtual DbSet<FermerComment> FermerComments { get; set; }
    public virtual DbSet<Z_AlertLogs> Z_AlertLogs { get; set; }
    public virtual DbSet<Bolu> Bolus { get; set; }
    public virtual DbSet<MOB_EventFeedback> MOB_EventFeedback { get; set; }
    public virtual DbSet<Mob_Events> Mob_Events { get; set; }
    public virtual DbSet<Mob_Feedback> Mob_Feedback { get; set; }
    public virtual DbSet<Mob_DeviceToken> Mob_DeviceToken { get; set; }

    public virtual ObjectResult<MOB_Farm_TodayEventsList_Result> MOB_Farm_TodayEventsList(string user_id, Nullable<System.DateTime> dt, string @event)
    {
        var user_idParameter = user_id != null ?
            new ObjectParameter("user_id", user_id) :
            new ObjectParameter("user_id", typeof(string));

        var dtParameter = dt.HasValue ?
            new ObjectParameter("dt", dt) :
            new ObjectParameter("dt", typeof(System.DateTime));

        var eventParameter = @event != null ?
            new ObjectParameter("event", @event) :
            new ObjectParameter("event", typeof(string));

        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<MOB_Farm_TodayEventsList_Result>("MOB_Farm_TodayEventsList", user_idParameter, dtParameter, eventParameter);
    }

    public virtual ObjectResult<DataGapsFarm_Result> DataGapsFarm(Nullable<System.DateTime> dt, Nullable<int> interval, string user_id)
    {
        var dtParameter = dt.HasValue ?
            new ObjectParameter("dt", dt) :
            new ObjectParameter("dt", typeof(System.DateTime));

        var intervalParameter = interval.HasValue ?
            new ObjectParameter("interval", interval) :
            new ObjectParameter("interval", typeof(int));

        var user_idParameter = user_id != null ?
            new ObjectParameter("user_id", user_id) :
            new ObjectParameter("user_id", typeof(string));

        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<DataGapsFarm_Result>("DataGapsFarm", dtParameter, intervalParameter, user_idParameter);
    }

    public virtual ObjectResult<SP_GET_FarmLostCows_Result> SP_GET_FarmLostCows(string user_id, Nullable<int> interval_hours, Nullable<System.DateTime> dt_toronto)
    {
        var user_idParameter = user_id != null ?
            new ObjectParameter("user_id", user_id) :
            new ObjectParameter("user_id", typeof(string));

        var interval_hoursParameter = interval_hours.HasValue ?
            new ObjectParameter("interval_hours", interval_hours) :
            new ObjectParameter("interval_hours", typeof(int));

        var dt_torontoParameter = dt_toronto.HasValue ?
            new ObjectParameter("dt_toronto", dt_toronto) :
            new ObjectParameter("dt_toronto", typeof(System.DateTime));

        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_GET_FarmLostCows_Result>("SP_GET_FarmLostCows", user_idParameter, interval_hoursParameter, dt_torontoParameter);
    }

    public virtual int SP_GET_Farm_TotalAlertsPeriod(Nullable<System.DateTime> dt, string user_id, Nullable<int> days, string @event, ObjectParameter result)
    {
        var dtParameter = dt.HasValue ?
            new ObjectParameter("dt", dt) :
            new ObjectParameter("dt", typeof(System.DateTime));

        var user_idParameter = user_id != null ?
            new ObjectParameter("user_id", user_id) :
            new ObjectParameter("user_id", typeof(string));

        var daysParameter = days.HasValue ?
            new ObjectParameter("days", days) :
            new ObjectParameter("days", typeof(int));

        var eventParameter = @event != null ?
            new ObjectParameter("event", @event) :
            new ObjectParameter("event", typeof(string));

        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_GET_Farm_TotalAlertsPeriod", dtParameter, user_idParameter, daysParameter, eventParameter, result);
    }

    public virtual ObjectResult<ChartsXY_temp_Result> ChartsXY_temp(Nullable<System.DateTime> dt1, Nullable<System.DateTime> dt2, Nullable<int> bolus_id, Nullable<double> interval)
    {
        var dt1Parameter = dt1.HasValue ?
            new ObjectParameter("dt1", dt1) :
            new ObjectParameter("dt1", typeof(System.DateTime));

        var dt2Parameter = dt2.HasValue ?
            new ObjectParameter("dt2", dt2) :
            new ObjectParameter("dt2", typeof(System.DateTime));

        var bolus_idParameter = bolus_id.HasValue ?
            new ObjectParameter("bolus_id", bolus_id) :
            new ObjectParameter("bolus_id", typeof(int));

        var intervalParameter = interval.HasValue ?
            new ObjectParameter("Interval", interval) :
            new ObjectParameter("Interval", typeof(double));

        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ChartsXY_temp_Result>("ChartsXY_temp", dt1Parameter, dt2Parameter, bolus_idParameter, intervalParameter);
    }

    public virtual ObjectResult<WaterIntakes_sig_Result> WaterIntakes_sig(Nullable<System.DateTime> dt1, Nullable<System.DateTime> dt2, Nullable<int> bolus_id, Nullable<double> wi_calbr)
    {
        var dt1Parameter = dt1.HasValue ?
            new ObjectParameter("dt1", dt1) :
            new ObjectParameter("dt1", typeof(System.DateTime));

        var dt2Parameter = dt2.HasValue ?
            new ObjectParameter("dt2", dt2) :
            new ObjectParameter("dt2", typeof(System.DateTime));

        var bolus_idParameter = bolus_id.HasValue ?
            new ObjectParameter("bolus_id", bolus_id) :
            new ObjectParameter("bolus_id", typeof(int));

        var wi_calbrParameter = wi_calbr.HasValue ?
            new ObjectParameter("wi_calbr", wi_calbr) :
            new ObjectParameter("wi_calbr", typeof(double));

        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<WaterIntakes_sig_Result>("WaterIntakes_sig", dt1Parameter, dt2Parameter, bolus_idParameter, wi_calbrParameter);
    }

    public virtual ObjectResult<MOB_GET_AnimalListByUserID_Result> MOB_GET_AnimalListByUserID(string user_id)
    {
        var user_idParameter = user_id != null ?
            new ObjectParameter("user_id", user_id) :
            new ObjectParameter("user_id", typeof(string));

        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<MOB_GET_AnimalListByUserID_Result>("MOB_GET_AnimalListByUserID", user_idParameter);
    }

    public virtual ObjectResult<MOB_GetFarmCowsInfo_Result> MOB_GetFarmCowsInfo(string user_id)
    {
        var user_idParameter = user_id != null ?
            new ObjectParameter("user_id", user_id) :
            new ObjectParameter("user_id", typeof(string));

        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<MOB_GetFarmCowsInfo_Result>("MOB_GetFarmCowsInfo", user_idParameter);
    }

    public virtual ObjectResult<MOB_AlertEventsByDateUserID_Result> MOB_AlertEventsByDateUserID(string user_id, Nullable<System.DateTime> dt)
    {
        var user_idParameter = user_id != null ?
            new ObjectParameter("user_id", user_id) :
            new ObjectParameter("user_id", typeof(string));

        var dtParameter = dt.HasValue ?
            new ObjectParameter("dt", dt) :
            new ObjectParameter("dt", typeof(System.DateTime));

        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<MOB_AlertEventsByDateUserID_Result>("MOB_AlertEventsByDateUserID", user_idParameter, dtParameter);
    }

    public virtual ObjectResult<MOB_Get_AlertDetailsById_Result> MOB_Get_AlertDetailsById(Nullable<int> alert_id)
    {
        var alert_idParameter = alert_id.HasValue ?
            new ObjectParameter("alert_id", alert_id) :
            new ObjectParameter("alert_id", typeof(int));

        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<MOB_Get_AlertDetailsById_Result>("MOB_Get_AlertDetailsById", alert_idParameter);
    }

    public virtual ObjectResult<MOB_Get_AlertListByUserID_Result> MOB_Get_AlertListByUserID(string user_id, Nullable<int> bolus_id, Nullable<int> page, ObjectParameter total_pages)
    {
        var user_idParameter = user_id != null ?
            new ObjectParameter("user_id", user_id) :
            new ObjectParameter("user_id", typeof(string));

        var bolus_idParameter = bolus_id.HasValue ?
            new ObjectParameter("bolus_id", bolus_id) :
            new ObjectParameter("bolus_id", typeof(int));

        var pageParameter = page.HasValue ?
            new ObjectParameter("page", page) :
            new ObjectParameter("page", typeof(int));

        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<MOB_Get_AlertListByUserID_Result>("MOB_Get_AlertListByUserID", user_idParameter, bolus_idParameter, pageParameter, total_pages);
    }

    public virtual ObjectResult<Nullable<double>> WaterIntakes_Sum(Nullable<System.DateTime> dt1, Nullable<System.DateTime> dt2, Nullable<int> bolus_id, Nullable<double> wi_calbr)
    {
        var dt1Parameter = dt1.HasValue ?
            new ObjectParameter("dt1", dt1) :
            new ObjectParameter("dt1", typeof(System.DateTime));

        var dt2Parameter = dt2.HasValue ?
            new ObjectParameter("dt2", dt2) :
            new ObjectParameter("dt2", typeof(System.DateTime));

        var bolus_idParameter = bolus_id.HasValue ?
            new ObjectParameter("bolus_id", bolus_id) :
            new ObjectParameter("bolus_id", typeof(int));

        var wi_calbrParameter = wi_calbr.HasValue ?
            new ObjectParameter("wi_calbr", wi_calbr) :
            new ObjectParameter("wi_calbr", typeof(double));

        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<double>>("WaterIntakes_Sum", dt1Parameter, dt2Parameter, bolus_idParameter, wi_calbrParameter);
    }

    public virtual ObjectResult<MOB_GetCowDetails_Result> MOB_GetCowDetails(Nullable<int> bolus_id)
    {
        var bolus_idParameter = bolus_id.HasValue ?
            new ObjectParameter("bolus_id", bolus_id) :
            new ObjectParameter("bolus_id", typeof(int));

        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<MOB_GetCowDetails_Result>("MOB_GetCowDetails", bolus_idParameter);
    }
}

﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

public partial class Bolu
{
    public int id { get; set; }
    public int bolus_id { get; set; }
    public Nullable<int> animal_id { get; set; }
    public string AnimalInfo { get; set; }
    public Nullable<int> Age_Lactation { get; set; }
    public string Current_Stage_Of_Lactation { get; set; }
    public string Health_Concerns_Illness_History { get; set; }
    public string Overall_Health { get; set; }
    public string Comments { get; set; }
    public Nullable<System.DateTime> Date_of_Birth { get; set; }
    public Nullable<System.DateTime> Calving_Due_Date { get; set; }
    public Nullable<System.DateTime> Actual_Calving_Date { get; set; }
    public bool status { get; set; }
}

public partial class Farm
{
    public int id { get; set; }
    public string Name { get; set; }
    public string Owner { get; set; }
    public System.Data.Entity.Spatial.DbGeography GeoPosition { get; set; }
    public string AspNetUser_Id { get; set; }
    public string Phones { get; set; }
    public string Emails { get; set; }
    public string PhoneStatus { get; set; }
    public string EmailStatus { get; set; }
}

public partial class FermerComment
{
    public int id { get; set; }
    public int bolus_id { get; set; }
    public Nullable<int> animal_id { get; set; }
    public string user_id { get; set; }
    public Nullable<System.DateTime> time_stamp { get; set; }
    public string comment { get; set; }
    public bool status_read { get; set; }
}

public partial class MeasData
{
    public int id { get; set; }
    public string user_id { get; set; }
    public int bolus_id { get; set; }
    public string animal_id { get; set; }
    public double temperature { get; set; }
    public string bolus_timestamp { get; set; }
    public Nullable<System.DateTime> bolus_full_date { get; set; }
    public int base_station_id { get; set; }
    public bool is_average { get; set; }
    public string raw { get; set; }
}

public partial class Mob_DeviceToken
{
    public int id { get; set; }
    public string AspNetUser_ID { get; set; }
    public string device_token { get; set; }
}

public partial class MOB_EventFeedback
{
    public int id { get; set; }
    public int event_id { get; set; }
    public bool visual_symptoms { get; set; }
    public Nullable<double> rectal_temperature { get; set; }
    public Nullable<System.DateTime> rectal_temperature_measuring_time { get; set; }
    public string treatment_note { get; set; }
    public string general_note { get; set; }

    public virtual Mob_Events Mob_Events { get; set; }
}

public partial class Mob_Events
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public Mob_Events()
    {
        this.MOB_EventFeedback = new HashSet<MOB_EventFeedback>();
    }

    public int id { get; set; }
    public int bolus_id { get; set; }
    public string diagnosis { get; set; }
    public string comment { get; set; }
    public Nullable<System.DateTime> selected_date { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
    public virtual ICollection<MOB_EventFeedback> MOB_EventFeedback { get; set; }
}

public partial class Mob_Feedback
{
    public int id { get; set; }
    public int alert_id { get; set; }
    public bool visual_symptoms { get; set; }
    public Nullable<double> rectal_temperature { get; set; }
    public Nullable<System.DateTime> rectal_temperature_measuring_time { get; set; }
    public string diagnosis { get; set; }
    public string treatment_note { get; set; }
    public string general_note { get; set; }
}

public partial class Mob_Token
{
    public int id { get; set; }
    public string username { get; set; }
    public string hashcode { get; set; }
    public Nullable<System.DateTime> datetime_exp { get; set; }
}

public partial class Mob_Users
{
    public int id { get; set; }
    public string email { get; set; }
    public string passwordMD5 { get; set; }
    public string AspNetUser_ID { get; set; }
}

public partial class Z_AlertLogs
{
    public int id { get; set; }
    public int bolus_id { get; set; }
    public string @event { get; set; }
    public string message { get; set; }
    public Nullable<System.DateTime> date_emailsent { get; set; }
    public string email { get; set; }
    public Nullable<bool> read { get; set; }
    public Nullable<bool> feedback { get; set; }
}

public partial class ChartsXY_temp_Result
{
    public string t { get; set; }
    public double Temperature { get; set; }
}

public partial class DataGapsFarm_Result
{
    public string r { get; set; }
    public string Gaps { get; set; }
    public string DateCtrl { get; set; }
}

public partial class MOB_AlertEventsByDateUserID_Result
{
    public string @event { get; set; }
    public Nullable<int> number { get; set; }
}

public partial class MOB_Farm_TodayEventsList_Result
{
    public int id { get; set; }
    public int bolus_id { get; set; }
    public Nullable<int> animal_id { get; set; }
    public string @event { get; set; }
    public string message { get; set; }
    public Nullable<System.DateTime> date_emailsent { get; set; }
    public Nullable<bool> read { get; set; }
}

public partial class MOB_Get_AlertDetailsById_Result
{
    public int id { get; set; }
    public int bolus_id { get; set; }
    public Nullable<int> animal_id { get; set; }
    public string message { get; set; }
    public string date_emailsent { get; set; }
    public Nullable<bool> read { get; set; }
    public string day_in_milk { get; set; }
    public string lactation_stage { get; set; }
    public string current_lactation { get; set; }
}

public partial class MOB_Get_AlertListByUserID_Result
{
    public int id { get; set; }
    public int bolus_id { get; set; }
    public Nullable<int> animal_id { get; set; }
    public string date_emailsent { get; set; }
    public string @event { get; set; }
    public string message { get; set; }
    public bool read { get; set; }
}

public partial class MOB_GET_AnimalListByUserID_Result
{
    public int bolus_id { get; set; }
    public Nullable<int> animal_id { get; set; }
}

public partial class MOB_GetCowDetails_Result
{
    public int bolus_id { get; set; }
    public Nullable<int> animal_id { get; set; }
    public string name { get; set; }
    public string lastdate { get; set; }
    public string current_stage_of_lactation { get; set; }
    public Nullable<bool> has_unread_alerts { get; set; }
    public string lactation_day { get; set; }
}

public partial class MOB_GetFarmCowsInfo_Result
{
    public Nullable<int> lactnum { get; set; }
    public string lactname { get; set; }
}

public partial class SP_GET_FarmLostCows_Result
{
    public Nullable<int> bolus_id { get; set; }
    public string animal_id { get; set; }
    public Nullable<System.DateTime> lastdate { get; set; }
    public string Current_Stage_Of_Lactation { get; set; }
}

public partial class WaterIntakes_sig_Result
{
    public int id { get; set; }
    public Nullable<System.DateTime> bolus_full_date { get; set; }
    public Nullable<double> intakes { get; set; }
}

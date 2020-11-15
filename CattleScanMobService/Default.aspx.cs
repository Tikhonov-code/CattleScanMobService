using Newtonsoft.Json;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Web.Services;
using System.Data.Entity.Core.Objects;
using static BolusIDChart_Model;
using static WaterIntakes;
using Newtonsoft.Json.Linq;
using System.Configuration;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //string pp = MD5Hash("Harrop*20");Stonecreek^2019
        //string pp = MD5Hash("Stonecreek^2019"); 
        string parameters = string.Empty;
        using (var reader = new System.IO.StreamReader(Request.InputStream))
        {
            parameters = reader.ReadToEnd();
        }
        var action = Request.QueryString["action"];

        switch (action)
        {
            case "mob_0":
                CheckLogin(parameters);
                break;
            case "mob_1":
                if (IsTokenAlive(parameters)) MOB_GetFarmCowsInfo(parameters);
                ReturnTokenWrong("action:mob_01 -- ");
                break;
            case "mob_2":
                if (IsTokenAlive(parameters)) MOB_AlertEventsByDateUserID(parameters);
                ReturnTokenWrong("action:mob_02 -- ");
                break;
            case "mob_3":
                if (IsTokenAlive(parameters)) MOB_Farm_TodayEventsList(parameters, "Q41");
                ReturnTokenWrong("action:mob_03 -- ");
                break;
            case "mob_4":
                if (IsTokenAlive(parameters)) MOB_Farm_TodayEventsList(parameters, "Q40.5");
                ReturnTokenWrong("action:mob_04 -- ");
                break;
            case "mob_5":
                if (IsTokenAlive(parameters)) MOB_GetDataIntegrity(parameters);
                ReturnTokenWrong("action:mob_05 -- ");
                break;
            case "mob_6":
                if (IsTokenAlive(parameters)) MOB_GetLostCowsList(parameters);
                ReturnTokenWrong("action:mob_06 -- ");
                break;
            case "mob_7":
                if (IsTokenAlive(parameters)) MOB_GetFarmData(parameters);
                ReturnTokenWrong("action:mob_07 -- ");
                break;
            case "GetDataForChart":
                if (IsTokenAlive(parameters)) MOB_GetDataForChart(parameters);
                ReturnTokenWrong("action:GetDataForChart -- ");
                break;
            case "GetIntakesData":
                if (IsTokenAlive(parameters)) MOB_GetIntakesData(parameters);
                ReturnTokenWrong("action:GetIntakesData -- ");
                break;
            case "ReadFermerComment":
                if (IsTokenAlive(parameters)) MOB_ReadFermerComment(parameters);
                ReturnTokenWrong("action:SaveFermerComment -- ");
                break;
            case "SaveFermerComment":
                if (IsTokenAlive(parameters)) MOB_SaveFermerComment(parameters);
                ReturnTokenWrong("action:SaveFermerComment -- ");
                break;
            case "UpdateFermerComment":
                if (IsTokenAlive(parameters)) MOB_UpdateFermerComment(parameters);
                ReturnTokenWrong("action:UpdateFermerComment -- ");
                break;
            case "DeleteFermerComment":
                if (IsTokenAlive(parameters)) MOB_DeleteFermerComment(parameters);
                ReturnTokenWrong("action:DeleteFermerComment -- ");
                break;
            case "GetAnimalList":
                if (IsTokenAlive(parameters)) MOB_GetAnimalList(parameters);
                ReturnTokenWrong("action:GetAnimalList -- ");
                break;
            case "alert_list":
                if (IsTokenAlive(parameters)) MOB_GetAlertList(parameters);
                ReturnTokenWrong("action:GetAlertList -- ");
                break;
            case "alert_details":
                if (IsTokenAlive(parameters)) MOB_GetAlertDetails(parameters);
                ReturnTokenWrong("action:alert_details -- ");
                break;
            case "cow_details":
                if (IsTokenAlive(parameters)) MOB_GetCowDetails(parameters);
                ReturnTokenWrong("action:cow_details -- ");
                break;
            case "cow_details_update":
                if (IsTokenAlive(parameters)) MOB_UpdateCowDetails(parameters);
                ReturnTokenWrong("action:cow_details_update -- ");
                break;
            case "create_event":
                if (IsTokenAlive(parameters)) MOB_CreateEvent(parameters);
                ReturnTokenWrong("action:create_event -- ");
                break;
            case "update_event":
                if (IsTokenAlive(parameters)) MOB_UpdateEvent(parameters);
                ReturnTokenWrong("action:update_event -- ");
                break;
            case "delete_event":
                if (IsTokenAlive(parameters)) MOB_DeleteEvent(parameters);
                ReturnTokenWrong("action:delete_event -- ");
                break;
            case "list_events":
                if (IsTokenAlive(parameters)) MOB_lGetListEvents(parameters);
                ReturnTokenWrong("action:update_event -- ");
                break;
            case "list_feedback":
                if (IsTokenAlive(parameters)) MOB_EventListFeedback(parameters);
                ReturnTokenWrong("action:list_feedback -- ");
                break;
            case "add_feedback":
                if (IsTokenAlive(parameters)) MOB_AddFeedback(parameters);
                ReturnTokenWrong("action:add_feedback -- ");
                break;
            case "update_feedback":
                if (IsTokenAlive(parameters)) MOB_UpdateFeedback(parameters);
                ReturnTokenWrong("action:update_feedback -- ");
                break;
            case "delete_feedback":
                if (IsTokenAlive(parameters)) MOB_DeleteFeedback(parameters);
                ReturnTokenWrong("action:delete_feedback -- ");
                break;
            case "status_alert":
                if (IsTokenAlive(parameters)) MOB_SetStatusAlert(parameters);
                ReturnTokenWrong("action:status_alert -- ");
                break;
            case "save_device_token":
                if (IsTokenAlive(parameters)) MOB_SaveDeviceToken(parameters);
                ReturnTokenWrong("action:save_device_token -- ");
                break;
            case "get_intakes":
                if (IsTokenAlive(parameters)) MOB_GetIntakes(parameters);
                ReturnTokenWrong("action:get_intakes -- ");
                break;
            default:
                SendErrorResponse("wrong request:" + action);
                break;
        }
    }

    private void MOB_GetIntakes(string parameters)
    {
        JObject Pars_Obj = JObject.Parse(parameters);

        string[] parnames = { "token", "bolus_id", "dt1", "dt2" };
        IsInputParametersOK("get_intakes", parameters, parnames);
        //-----Parsing parameters---------------------
        string token = (string)Pars_Obj.Root["token"];
        string user_id = GetUserIdByToken(token);
        if (string.IsNullOrEmpty(user_id))
        {
            SendErrorResponse("user was not found");
        }
        int bolus_id = Convert.ToInt16((string)Pars_Obj.Root["bolus_id"]);
        DateTime dt1 = DateTime.Parse((string)Pars_Obj.Root["dt1"]);
        DateTime dt2 = DateTime.Parse((string)Pars_Obj.Root["dt2"]);
        //---------------------------------------------------------
        double? result = 0;
        try
        {
            using (DB_A4A060_csEntities context = new DB_A4A060_csEntities())
            {
                //save in database
                result = context.WaterIntakes_Sum(dt1, dt2, bolus_id, 2).SingleOrDefault().Value;
            }
        }
        catch (Exception ex)
        {
            SendErrorResponse("get_intakes: " + ex.Message);
        }
        // result preparation-----------------------------------------
        JObject data = new JObject(
            new JProperty("status", "ok"),
            new JProperty("data",
            new JObject(
                            new JProperty("sum", result)
                               )
                ));
        SendOKResponse_1(data);
    }

    private void MOB_SaveDeviceToken(string parameters)
    {
        JObject Pars_Obj = JObject.Parse(parameters);

        //-----Parsing parameters---------------------
        string token = (string)Pars_Obj.Root["token"];
        string user_id = GetUserIdByToken(token);
        if (string.IsNullOrEmpty(user_id))
        {
            SendErrorResponse("user was not found");
        }

        string device_token = (string)Pars_Obj.Root["device_token"];
        //---------------------------------------------------------
        Mob_DeviceToken result = new Mob_DeviceToken
        {
            AspNetUser_ID = user_id,
            device_token = device_token
        };
        try
        {
            using (DB_A4A060_csEntities context = new DB_A4A060_csEntities())
            {
                Mob_DeviceToken dt = context.Mob_DeviceToken.Where(x => x.AspNetUser_ID == result.AspNetUser_ID).SingleOrDefault();
                if (dt == null)
                {
                    //save in database
                    context.Mob_DeviceToken.Add(result);

                }
                else
                {
                    dt.device_token = result.device_token;
                }
                context.SaveChanges();
            }
        }
        catch (Exception ex)
        {
            SendErrorResponse("save_device_token: " + ex.Message);
        }
        // result preparation-----------------------------------------
        JObject data = new JObject(
            new JProperty("status", "ok"),
            new JProperty("data",
            new JObject(
                            new JProperty("token", token),
                            new JProperty("device_token", result.device_token)
                               )
                ));
        SendOKResponse_1(data);
    }

    private void MOB_SetStatusAlert(string parameters)
    {
        JObject Pars_Obj = JObject.Parse(parameters);
        Dictionary<string, string> dictObj = Pars_Obj.ToObject<Dictionary<string, string>>();

        //-----Parsing parameters---------------------
        string token = dictObj["token"];
        string user_id = GetUserIdByToken(token);
        if (string.IsNullOrEmpty(user_id))
        {
            SendErrorResponse("user was not found");
        }
        int alert_id = Convert.ToInt16(dictObj["alert_id"]);
        bool? read_null = ParseReadToBool(dictObj["read"]);
        bool? feedback = ParseReadToBool(dictObj["feedback"]);
        //---------------------------------------------------------
        Z_AlertLogs alert = new Z_AlertLogs();
        try
        {
            using (DB_A4A060_csEntities context = new DB_A4A060_csEntities())
            {
                alert = context.Z_AlertLogs.Where(x => x.id == alert_id).SingleOrDefault();
                if (alert != null)
                {
                    alert.read = read_null;
                    alert.feedback = feedback;
                    context.SaveChanges();
                }
                else
                {
                    alert = null;
                }
            }
        }
        catch (Exception ex)
        {
            SendErrorResponse("status_alert: " + ex.Message);
        }
        // result preparation-----------------------------------------
        if (alert == null)
        {
            SendErrorResponse("status_alert: record not found id=" + alert_id);
        }
        JObject data = new JObject(
            new JProperty("status", "ok"),
            new JProperty("data",
            new JObject(
                            new JProperty("alert_id", alert_id),
                            new JProperty("read", read_null),
                            new JProperty("feedback", feedback)
                               )
                ));

        SendOKResponse_1(data);
    }

    private bool? ParseReadToBool(string read)
    {
        bool? result = false;
        int readint = 0;
        if (!int.TryParse(read, out readint))
        {
            result = null;
            return result;
        }
        switch (readint)
        {
            case 0:
                result = false;
                break;
            case 1:
                result = true;
                break;
            default:
                result = null;
                break;
        }
        return result;
    }

    private void MOB_DeleteFeedback(string parameters)
    {
        JObject Pars_Obj = JObject.Parse(parameters);
        Dictionary<string, string> dictObj = Pars_Obj.ToObject<Dictionary<string, string>>();

        //-----Parsing parameters---------------------
        string token = dictObj["token"];
        int id = Convert.ToInt16(dictObj["id"]);

        string user_id = GetUserIdByToken(token);
        if (string.IsNullOrEmpty(user_id))
        {
            SendErrorResponse("user was not found");
        }
        //---------------------------------------------------------
        Mob_Feedback result = new Mob_Feedback();
        try
        {
            using (DB_A4A060_csEntities context = new DB_A4A060_csEntities())
            {
                result = context.Mob_Feedback.Where(x => x.id == id).SingleOrDefault();
                if (result != null)
                {
                    context.Mob_Feedback.Remove(result);
                    context.SaveChanges();
                }

            }
        }
        catch (Exception ex)
        {
            SendErrorResponse("delete_feedback: " + ex.Message);
        }
        // result preparation-----------------------------------------
        if (result == null)
        {
            SendErrorResponse("delete_feedback: record not found id=" + id);
        }
        JObject data = new JObject(
            new JProperty("status", "ok"),
            new JProperty("data",
            new JObject(
                            new JProperty("id", result.id)
                               )
                ));

        SendOKResponse_1(data);
    }

    private void MOB_UpdateFeedback(string parameters)
    {
        JObject Pars_Obj = JObject.Parse(parameters);

        //-----Parsing parameters---------------------
        string token = (string)Pars_Obj.Root["token"];
        string user_id = GetUserIdByToken(token);
        if (string.IsNullOrEmpty(user_id))
        {
            SendErrorResponse("user was not found");
        }

        int id = Convert.ToInt16(Pars_Obj.Root["id"]);
        int vs = Convert.ToInt16(Pars_Obj.Root["visual_symptoms"]);
        bool visual_symptoms = Convert.ToBoolean(vs);
        double rectal_temperature = Convert.ToDouble(Pars_Obj.Root["rectal_temperature"]);
        DateTime rectal_temperature_measuring_time = DateTime.Parse((string)Pars_Obj.Root["rectal_temperature_measuring_time"]);
        string treatment_note = (string)Pars_Obj.Root["treatment_note"];
        string general_note = (string)Pars_Obj.Root["general_note"];

        var diagnosis = Pars_Obj.Root["diagnosis"];
        string diagnosisStr = string.Empty;
        foreach (var item in diagnosis)
        {
            diagnosisStr += item + ",";
        }
        diagnosisStr = diagnosisStr.Substring(0, diagnosisStr.Length - 1);
        //---------------------------------------------------------
        Mob_Feedback result = new Mob_Feedback();
        try
        {
            using (DB_A4A060_csEntities context = new DB_A4A060_csEntities())
            {
                //save in database
                result = context.Mob_Feedback.Where(x => x.id == id).SingleOrDefault();
                if (result != null)
                {
                    result.visual_symptoms = visual_symptoms;
                    result.rectal_temperature = rectal_temperature;
                    result.rectal_temperature_measuring_time = rectal_temperature_measuring_time;
                    result.treatment_note = treatment_note;
                    result.general_note = general_note;
                    result.diagnosis = diagnosisStr;
                    context.SaveChanges();
                }
            }
        }
        catch (Exception ex)
        {
            SendErrorResponse("create_feedback: " + ex.Message);
        }
        // result preparation-----------------------------------------
        if (result == null) SendErrorResponse("update_feedback: no record id=" + id); ;
        JObject data = new JObject(
            new JProperty("status", "ok"),
            new JProperty("data",
            new JObject(
                            new JProperty("alert_id", result.alert_id),
                            new JProperty("visual_symptoms", result.visual_symptoms),
                            new JProperty("rectal_temperature", result.rectal_temperature),
                            new JProperty("rectal_temperature_measuring_time", result.rectal_temperature_measuring_time),
                            new JProperty("diagnosis", diagnosis),
                            new JProperty("treatment_note", result.treatment_note),
                            new JProperty("general_note", result.general_note)
                               )
                ));
        SendOKResponse_1(data);
    }

    private void MOB_AddFeedback(string parameters)
    {

        JObject Pars_Obj = JObject.Parse(parameters);

        //-----Parsing parameters---------------------
        string token = (string)Pars_Obj.Root["token"];
        string user_id = GetUserIdByToken(token);
        if (string.IsNullOrEmpty(user_id))
        {
            SendErrorResponse("user was not found");
        }

        int alert_id = Convert.ToInt16(Pars_Obj.Root["alert_id"]);
        int vs = Convert.ToInt16(Pars_Obj.Root["visual_symptoms"]);
        bool visual_symptoms = Convert.ToBoolean(vs);
        double rectal_temperature = Convert.ToDouble(Pars_Obj.Root["rectal_temperature"]);
        DateTime rectal_temperature_measuring_time = DateTime.Parse((string)Pars_Obj.Root["rectal_temperature_measuring_time"]);
        string treatment_note = (string)Pars_Obj.Root["treatment_note"];
        string general_note = (string)Pars_Obj.Root["general_note"];

        var diagnosis = Pars_Obj.Root["diagnosis"];
        string diagnosisStr = string.Empty;
        foreach (var item in diagnosis)
        {
            diagnosisStr += item + ",";
        }
        diagnosisStr = diagnosisStr.Substring(0, diagnosisStr.Length - 1);
        //---------------------------------------------------------
        Mob_Feedback result = new Mob_Feedback();
        result.alert_id = alert_id;
        result.visual_symptoms = visual_symptoms;
        result.rectal_temperature = rectal_temperature;
        result.rectal_temperature_measuring_time = rectal_temperature_measuring_time;
        result.treatment_note = treatment_note;
        result.general_note = general_note;
        result.diagnosis = diagnosisStr;
        try
        {
            using (DB_A4A060_csEntities context = new DB_A4A060_csEntities())
            {
                //save in database
                context.Mob_Feedback.Add(result);
                context.SaveChanges();
            }
        }
        catch (Exception ex)
        {
            SendErrorResponse("create_feedback: " + ex.Message);
        }
        // result preparation-----------------------------------------
        ;
        JObject data = new JObject(
            new JProperty("status", "ok"),
            new JProperty("data",
            new JObject(
                            new JProperty("alert_id", result.alert_id),
                            new JProperty("visual_symptoms", result.visual_symptoms),
                            new JProperty("rectal_temperature", result.rectal_temperature),
                            new JProperty("rectal_temperature_measuring_time", result.rectal_temperature_measuring_time),
                            new JProperty("diagnosis", diagnosis),
                            new JProperty("treatment_note", result.treatment_note),
                            new JProperty("general_note", result.general_note)
                               )
                ));

        SendOKResponse_1(data);
    }

    private void MOB_EventListFeedback(string parameters)
    {
        JObject Pars_Obj = JObject.Parse(parameters);
        Dictionary<string, string> dictObj = Pars_Obj.ToObject<Dictionary<string, string>>();

        //-----Parsing parameters---------------------
        string token = dictObj["token"];
        int event_id = 0;
        if (dictObj.ContainsKey("event_id"))
        {
            event_id = Convert.ToInt16(dictObj["event_id"]);
        }
        string user_id = GetUserIdByToken(token);
        if (string.IsNullOrEmpty(user_id))
        {
            SendErrorResponse("user was not found");
        }
        //---------------------------------------------------------
        List<MOB_EventFeedback> result = new List<MOB_EventFeedback>();
        try
        {
            using (DB_A4A060_csEntities context = new DB_A4A060_csEntities())
            {
                if (event_id == 0)
                {
                    result = null;
                }
                else
                {
                    result = context.MOB_EventFeedback.Where(x => x.event_id == event_id).ToList();
                }
            }
        }
        catch (Exception ex)
        {
            SendErrorResponse("list_events: " + ex.Message);
        }
        // result preparation-----------------------------------------
        if (result.Count == 0)
        {
            SendErrorResponse("list_events: no data");
        }

        JObject data = new JObject(
                    new JProperty("status", "ok"),
                    new JProperty("data",
                    new JArray(
                               from p in result
                               select new JObject(
                                   new JProperty("id", p.id),
                                    new JProperty("event_id", p.event_id),
                                    new JProperty("visual_symptoms", p.visual_symptoms),
                                    new JProperty("rectal_temperature", p.rectal_temperature),
                                    new JProperty("rectal_temperature_measuring_time", p.rectal_temperature_measuring_time),
                                    new JProperty("treatment_note", p.treatment_note),
                                    new JProperty("general_note", p.general_note)
                                       )
                        )));

        SendOKResponse_1(data);
    }

    private void MOB_DeleteEvent(string parameters)
    {
        JObject Pars_Obj = JObject.Parse(parameters);
        Dictionary<string, string> dictObj = Pars_Obj.ToObject<Dictionary<string, string>>();

        //-----Parsing parameters---------------------
        string token = dictObj["token"];
        int event_id = Convert.ToInt16(dictObj["event_id"]);

        string user_id = GetUserIdByToken(token);
        if (string.IsNullOrEmpty(user_id))
        {
            SendErrorResponse("user was not found");
        }
        //---------------------------------------------------------
        Mob_Events result = new Mob_Events();
        try
        {
            using (DB_A4A060_csEntities context = new DB_A4A060_csEntities())
            {
                result = context.Mob_Events.Where(x => x.id == event_id).SingleOrDefault();
                if (result != null)
                {
                    context.Mob_Events.Remove(result);
                    context.SaveChanges();
                }

            }
        }
        catch (Exception ex)
        {
            SendErrorResponse("update_event: " + ex.Message);
        }
        // result preparation-----------------------------------------
        if (result == null)
        {
            SendErrorResponse("update_event: record not found");
        }
        JObject data = new JObject(
            new JProperty("status", "ok"),
            new JProperty("data",
            new JObject(
                            new JProperty("id", result.id),
                            new JProperty("bolus_id", result.bolus_id),
                            new JProperty("diagnosis", result.diagnosis),
                            new JProperty("comment", result.comment),
                            new JProperty("selected_date", result.selected_date)
                               )
                ));

        SendOKResponse_1(data);
    }

    private void MOB_UpdateEvent(string parameters)
    {
        JObject Pars_Obj = JObject.Parse(parameters);
        Dictionary<string, string> dictObj = Pars_Obj.ToObject<Dictionary<string, string>>();

        //-----Parsing parameters---------------------
        string token = dictObj["token"];
        int event_id = Convert.ToInt16(dictObj["event_id"]);
        int bolus_id = Convert.ToInt16(dictObj["bolus_id"]);
        string diagnosis = dictObj["diagnosis"];
        string comment = dictObj["comment"];
        DateTime selected_date = DateTime.Parse(dictObj["selected_date"]);

        string user_id = GetUserIdByToken(token);
        if (string.IsNullOrEmpty(user_id))
        {
            SendErrorResponse("user was not found");
        }
        //---------------------------------------------------------
        Mob_Events result = new Mob_Events();
        try
        {
            using (DB_A4A060_csEntities context = new DB_A4A060_csEntities())
            {
                result = context.Mob_Events.Where(x => x.id == event_id).SingleOrDefault();
                if (result.id == event_id)
                {
                    result.bolus_id = bolus_id;
                    result.diagnosis = diagnosis;
                    result.comment = comment;
                    result.selected_date = selected_date;
                    context.SaveChanges();
                }
                else
                {
                    result = null;
                }
            }
        }
        catch (Exception ex)
        {
            SendErrorResponse("update_event: " + ex.Message);
        }
        // result preparation-----------------------------------------
        if (result == null)
        {
            SendErrorResponse("update_event: record not found");
        }
        JObject data = new JObject(
            new JProperty("status", "ok"),
            new JProperty("data",
            new JObject(
                            new JProperty("id", result.id),
                            new JProperty("bolus_id", result.bolus_id),
                            new JProperty("diagnosis", result.diagnosis),
                            new JProperty("comment", result.comment),
                            new JProperty("selected_date", result.selected_date)
                               )
                ));

        SendOKResponse_1(data);
    }

    private void MOB_lGetListEvents(string parameters)
    {
        JObject Pars_Obj = JObject.Parse(parameters);
        Dictionary<string, string> dictObj = Pars_Obj.ToObject<Dictionary<string, string>>();

        //-----Parsing parameters---------------------
        string token = dictObj["token"];
        int bolus_id = 0;
        if (dictObj.ContainsKey("bolus_id"))
        {
            bolus_id = Convert.ToInt16(dictObj["bolus_id"]);
        }
        string user_id = GetUserIdByToken(token);
        if (string.IsNullOrEmpty(user_id))
        {
            SendErrorResponse("user was not found");
        }
        //---------------------------------------------------------
        List<Mob_Events> result = new List<Mob_Events>();
        try
        {
            using (DB_A4A060_csEntities context = new DB_A4A060_csEntities())
            {
                if (bolus_id == 0)
                {
                    result = context.Mob_Events.ToList();
                }
                else
                {
                    result = context.Mob_Events.Where(x => x.bolus_id == bolus_id).ToList();
                }
            }
        }
        catch (Exception ex)
        {
            SendErrorResponse("list_events: " + ex.Message);
        }
        // result preparation-----------------------------------------
        if (result.Count == 0)
        {
            SendErrorResponse("list_events: no data");
        }

        JObject data = new JObject(
                new JProperty("status", "ok"),
                new JProperty("data",
                new JArray(
                           from p in result
                           select new JObject(
                               new JProperty("id", p.id),
                                new JProperty("bolus_id", p.bolus_id),
                                new JProperty("diagnosis", p.diagnosis),
                                new JProperty("comment", p.comment),
                                new JProperty("selected_date", p.selected_date)
                                   )
                    )));

        SendOKResponse_1(data);
    }

    private void MOB_CreateEvent(string parameters)
    {
        JObject Pars_Obj = JObject.Parse(parameters);
        Dictionary<string, string> dictObj = Pars_Obj.ToObject<Dictionary<string, string>>();

        //-----Parsing parameters---------------------
        string token = dictObj["token"];
        int bolus_id = Convert.ToInt16(dictObj["bolus_id"]);
        string diagnosis = dictObj["diagnosis"];
        string comment = dictObj["comment"];
        DateTime selected_date = DateTime.Parse(dictObj["selected_date"]);

        string user_id = GetUserIdByToken(token);
        if (string.IsNullOrEmpty(user_id))
        {
            SendErrorResponse("user was not found");
        }
        //---------------------------------------------------------
        Mob_Events result = new Mob_Events();
        result.bolus_id = bolus_id;
        result.diagnosis = diagnosis;
        result.comment = comment;
        result.selected_date = selected_date;
        try
        {
            using (DB_A4A060_csEntities context = new DB_A4A060_csEntities())
            {
                //save in database
                context.Mob_Events.Add(result);
                context.SaveChanges();
            }
        }
        catch (Exception ex)
        {
            SendErrorResponse("create_event: " + ex.Message);
        }
        // result preparation-----------------------------------------
        JObject data = new JObject(
            new JProperty("status", "ok"),
            new JProperty("data",
            new JObject(
                            new JProperty("id", result.id),
                            new JProperty("bolus_id", result.bolus_id),
                            new JProperty("diagnosis", result.diagnosis),
                            new JProperty("comment", result.comment),
                            new JProperty("selected_date", result.selected_date)
                               )
                ));

        SendOKResponse_1(data);
    }

    private void MOB_UpdateCowDetails(string parameters)
    {
        JObject Pars_Obj = JObject.Parse(parameters);
        Dictionary<string, string> dictObj = Pars_Obj.ToObject<Dictionary<string, string>>();

        //-----Parsing parameters---------------------
        string token = dictObj["token"];
        int bolus_id = Convert.ToInt16(dictObj["bolus_id"]);
        int Age_Lactation = Convert.ToInt16(dictObj["age_lactation"]); ;
        string Current_Stage_Of_Lactation = dictObj["current_stage_of_lactation"];
        string Comments = dictObj["comments"];
        DateTime Calving_Due_Date = DateTime.Parse(dictObj["calving_due_date"]);
        DateTime Actual_Calving_Date = DateTime.Parse(dictObj["actual_calving_date"]);
        int st = Convert.ToInt16(dictObj["status"]);

        bool status = false;
        if (st == 0 || st == 1)
        {
            status = Convert.ToBoolean(st);
        }

        string user_id = GetUserIdByToken(token);
        if (string.IsNullOrEmpty(user_id))
        {
            SendErrorResponse("user was not found");
        }
        //---------------------------------------------------------
        Bolu result = new Bolu();
        try
        {
            using (DB_A4A060_csEntities context = new DB_A4A060_csEntities())
            {
                result = context.Bolus.Where(x => x.bolus_id == bolus_id).SingleOrDefault();
                if (result != null)
                {
                    result.Age_Lactation = Age_Lactation;
                    result.Current_Stage_Of_Lactation = Current_Stage_Of_Lactation;
                    result.Comments = Comments;
                    result.Calving_Due_Date = Calving_Due_Date;
                    result.Actual_Calving_Date = Actual_Calving_Date;
                    result.status = status;

                    //save in database
                    context.SaveChanges();
                }
            }
        }
        catch (Exception ex)
        {
            SendErrorResponse("cow_details_update: " + ex.Message);
        }
        // result preparation-----------------------------------------
        if (result == null)
        {
            SendErrorResponse("record not found");
        }
        bool has_unread_alerts = GetCountOfUnreadAlerts(bolus_id);

        JObject data = new JObject(
            new JProperty("status", "ok"),
            new JProperty("data",
            new JObject(
                            new JProperty("bolus_id", result.bolus_id),
                            new JProperty("animal_id", result.animal_id),
                            new JProperty("comments", result.Comments),
                            new JProperty("age_lactation", result.Age_Lactation),
                            new JProperty("current_stage_of_lactation", result.Current_Stage_Of_Lactation),
                            new JProperty("actual_calving_date", result.Actual_Calving_Date),
                            new JProperty("status", result.status),
                            new JProperty("has_unread_alerts", has_unread_alerts)
                               )
                ));

        SendOKResponse_1(data);
    }

    private bool GetCountOfUnreadAlerts(int bolus_id)
    {
        bool result = false;
        try
        {
            using (DB_A4A060_csEntities context = new DB_A4A060_csEntities())
            {
                var c = context.Z_AlertLogs.Where(x => x.bolus_id == bolus_id && x.read == false).Count();
                result = (c > 0) ? true : false;
            }
        }
        catch (Exception)
        {
            result = false;
        }
        return result;
    }

    private void MOB_GetCowDetails(string parameters)
    {
        JObject Pars_Obj = JObject.Parse(parameters);
        Dictionary<string, string> dictObj = Pars_Obj.ToObject<Dictionary<string, string>>();

        //-----Parsing parameters---------------------
        string token = dictObj["token"];
        int bolus_id = Convert.ToInt16(dictObj["bolus_id"]);

        string user_id = GetUserIdByToken(token);
        if (string.IsNullOrEmpty(user_id))
        {
            SendErrorResponse("user was not found");
        }
        //---------------------------------------------------------
        List<MOB_GetCowDetails_Result> result = new List<MOB_GetCowDetails_Result>();
        try
        {
            using (DB_A4A060_csEntities context = new DB_A4A060_csEntities())
            {
                result = context.MOB_GetCowDetails(bolus_id).ToList();
            }
        }
        catch (Exception ex)
        {
            SendErrorResponse("MOB_GetAlertList: " + ex.Message);
        }
        // result preparation-----------------------------------------

        JObject data = new JObject(
            new JProperty("status", "ok"),
            new JProperty("data",
            new JArray(
                       from p in result
                       select new JObject(
                            new JProperty("bolus_id", p.bolus_id),
                            new JProperty("animal_id", p.animal_id),
                            new JProperty("name", p.name),
                            new JProperty("current_stage_of_lactation", p.current_stage_of_lactation),
                            new JProperty("has_unread_alerts", p.has_unread_alerts),
                            new JProperty("lactation_day", p.lactation_day)
                               )
                )));

        SendOKResponse_1(data);
    }

    private void MOB_GetAlertList(string parameters)
    {
        JObject Pars_Obj = JObject.Parse(parameters);
        Dictionary<string, string> dictObj = Pars_Obj.ToObject<Dictionary<string, string>>();

        //-----Parsing parameters---------------------
        string token = dictObj["token"];
        int par_number = dictObj.Count;
        int page = 0;
        int bolus_id = 0;
        int animal_id = 0;
        switch (par_number)
        {
            case 2:
                page = Convert.ToInt16(dictObj["page"]);
                break;
            case 3:
                page = Convert.ToInt16(dictObj["page"]);
                if (dictObj.ContainsKey("animal_id"))
                {
                    animal_id = Convert.ToInt16(dictObj["animal_id"]);
                    bolus_id = GetBolusIDbyAnimalID(animal_id);
                }
                if (dictObj.ContainsKey("bolus_id"))
                {
                    bolus_id = Convert.ToInt16(dictObj["bolus_id"]);
                }
                break;
            default:
                break;
        }

        string user_id = GetUserIdByToken(token);
        if (string.IsNullOrEmpty(user_id))
        {
            SendErrorResponse("user was not found");
        }
        //---------------------------------------------------------
        int total_pages = 0;
        //--------------------------------------------
        List<MOB_Get_AlertListByUserID_Result> result = new List<MOB_Get_AlertListByUserID_Result>();
        try
        {
            using (DB_A4A060_csEntities context = new DB_A4A060_csEntities())
            {
                ObjectParameter tp = new ObjectParameter("total_pages", typeof(Int16));

                result = context.MOB_Get_AlertListByUserID(user_id, bolus_id, page, tp).ToList();
                total_pages = Convert.ToInt16(tp.Value);
            }
        }
        catch (Exception ex)
        {
            SendErrorResponse("MOB_GetAlertList: " + ex.Message);
        }
        // result preparation-----------------------------------------
        if (total_pages == 1) page = 1;
        if (page == 0) page = 1;

        JObject data = new JObject(
            new JProperty("status", "ok"),
            new JProperty("total_pages", total_pages),
            new JProperty("current_page", page),
            new JProperty("data",
            new JArray(
                       from p in result
                       select new JObject(
                            new JProperty("id", p.id),
                            new JProperty("bolus_id", p.bolus_id),
                            new JProperty("animal_id", p.animal_id),
                            new JProperty("date_emailsent", p.date_emailsent),
                            new JProperty("@event", p.@event),
                            new JProperty("message", p.message),
                            new JProperty("read", p.read)
                               )
                )));

        SendOKResponse_1(data);
    }

    private int GetBolusIDbyAnimalID(int animal_id)
    {
        int result = 0;
        try
        {
            using (DB_A4A060_csEntities context = new DB_A4A060_csEntities())
            {
                result = context.Bolus.Where(x => x.animal_id == animal_id).SingleOrDefault().bolus_id;
            }
        }
        catch (Exception ex)
        {
            SendErrorResponse("getbolusidbyanimalid :" + ex.Message);
        }
        return result;
    }

    private void MOB_GetAlertDetails(string parameters)
    {
        JObject Pars_Obj = JObject.Parse(parameters);
        Dictionary<string, string> dictObj = Pars_Obj.ToObject<Dictionary<string, string>>();
        //------------------------------------------------------------------------------------
        if (string.IsNullOrEmpty(dictObj["id"]))
        {
            SendErrorResponse("alert is empty");
        }

        MOB_Get_AlertDetailsById_Result result = new MOB_Get_AlertDetailsById_Result();
        try
        {
            int alert_id = Convert.ToInt16(dictObj["id"]);
            using (DB_A4A060_csEntities context = new DB_A4A060_csEntities())
            {
                //Temperature--------------------------------------------------------
                result = context.MOB_Get_AlertDetailsById(alert_id).SingleOrDefault();
            }
        }
        catch (Exception ex)
        {
            SendErrorResponse("MOB_GetAlertDetails: " + ex.Message);
        }
        JObject data = new JObject(
            new JProperty("status", "ok"),
            new JProperty("data",
                new JObject(
                            new JProperty("id", result.id),
                            new JProperty("bolus_id", result.bolus_id),
                            new JProperty("animal_id", result.animal_id),
                            new JProperty("message", result.message),
                            new JProperty("date_emailsent", result.date_emailsent),
                            new JProperty("read", result.read),
                            new JProperty("day_in_milk", result.day_in_milk),
                            new JProperty("lactation_stage", result.lactation_stage),
                            new JProperty("current_lactation", result.current_lactation)
                               )
                ));
        SendOKResponse_1(data);
    }

    private void MOB_GetAnimalList(string parameters)
    {
        JObject Pars_Obj = JObject.Parse(parameters);
        Dictionary<string, string> dictObj = Pars_Obj.ToObject<Dictionary<string, string>>();
        //------------------------------------------------------------------------------------
        string token = dictObj["token"];
        string user_id = GetUserIdByToken(token);
        if (string.IsNullOrEmpty(user_id))
        {
            SendErrorResponse("user was not found");
        }

        List<MOB_GET_AnimalListByUserID_Result> result = new List<MOB_GET_AnimalListByUserID_Result>();
        try
        {
            using (DB_A4A060_csEntities context = new DB_A4A060_csEntities())
            {
                //Temperature--------------------------------------------------------
                result = context.MOB_GET_AnimalListByUserID(user_id).ToList();
            }
        }
        catch (Exception ex)
        {
            SendErrorResponse("MOB_GetAnimalList: " + ex.Message);
        }
        JObject data = new JObject(
            new JProperty("status", "ok"),
            new JProperty("data",
                new JArray(
                            from p in result
                            select new JObject(
                                new JProperty("animal_id", p.animal_id),
                                new JProperty("bolus_id", p.bolus_id))
                               )
                ));
        SendOKResponse_1(data);
    }

    private void MOB_ReadFermerComment(string parameters)
    {
        string res_json = string.Empty;

        JObject Pars_Obj = JObject.Parse(parameters);
        Dictionary<string, string> dictObj = Pars_Obj.ToObject<Dictionary<string, string>>();
        //------------------------------------------------------------------------------------
        //1. retrive parameters
        string user_id = GetUserIdByToken(dictObj["token"]);
        if (string.IsNullOrEmpty(user_id))
        {
            SendErrorResponse("user was not found");
        }
        int bolus_id = Convert.ToInt16(dictObj["bolus_id"]);
        List<FermerComment> result = new List<FermerComment>();

        try
        {
            using (DB_A4A060_csEntities context = new DB_A4A060_csEntities())
            {
                result = context.FermerComments.Where(x => x.user_id == user_id && x.bolus_id == bolus_id).ToList();
            }
        }
        catch (Exception ex)
        {
            SendErrorResponse("MOB_SaveFermerComment: " + ex.Message);
        }
        //----------------------------------------------------------------
        JObject data = new JObject(
                new JProperty("status", "ok"),
                new JProperty("data",
                    new JArray(
                                from p in result
                                select new JObject(
                                    new JProperty("id", p.id),
                                    new JProperty("bolus_id", p.bolus_id),
                                    new JProperty("animal_id", p.animal_id),
                                    new JProperty("user_id", p.user_id),
                                    new JProperty("comment", p.comment),
                                    new JProperty("time_stamp", p.time_stamp)
                                    )
                                   )
                    ));
        SendOKResponse_1(data);
    }

    private void MOB_DeleteFermerComment(string parameters)
    {
        string res_json = string.Empty;

        JObject Pars_Obj = JObject.Parse(parameters);
        Dictionary<string, string> dictObj = Pars_Obj.ToObject<Dictionary<string, string>>();
        //------------------------------------------------------------------------------------
        int index = Convert.ToInt16(dictObj["id"]);

        try
        {
            using (DB_A4A060_csEntities context = new DB_A4A060_csEntities())
            {
                var item = context.FermerComments.Where(x => x.id == index).SingleOrDefault();
                if (item != null)
                {
                    context.FermerComments.Remove(item);
                    context.SaveChanges();
                }

                res_json = "MOB_DeleteFermerComment: record was deleted";
            }
        }
        catch (Exception ex)
        {
            SendErrorResponse("MOB_DeleteFermerComment: " + ex.Message);
        }

        JObject data = new JObject(
            new JProperty("status", "ok"),
            new JProperty("data", res_json)
            );
        SendOKResponse_1(data);
    }

    private void MOB_UpdateFermerComment(string parameters)
    {
        string res_json = string.Empty;

        JObject Pars_Obj = JObject.Parse(parameters);
        Dictionary<string, string> dictObj = Pars_Obj.ToObject<Dictionary<string, string>>();
        //------------------------------------------------------------------------------------
        Int32 rec_id = Convert.ToInt32(dictObj["id"]);
        try
        {
            using (DB_A4A060_csEntities context = new DB_A4A060_csEntities())
            {
                FermerComment upitem = context.FermerComments.Where(x => x.id == rec_id).SingleOrDefault();
                if (upitem != null)
                {
                    upitem.time_stamp = DateTime.Parse(dictObj["time_stamp"]);
                    upitem.comment = dictObj["comment"]; ;

                    context.SaveChanges();

                    res_json = "MOB_UpdateFermerComment: record was updated";
                }
                else
                {
                    res_json = "MOB_UpdateFermerComment: record not exists";
                }

            }
        }
        catch (Exception ex)
        {
            SendErrorResponse("MOB_UpdateFermerComment: " + ex.Message);
        }

        JObject data = new JObject(
            new JProperty("status", "ok"),
            new JProperty("data", res_json)
            );
        SendOKResponse_1(data);
    }

    private void MOB_SaveFermerComment(string parameters)
    {
        string res_json = string.Empty;

        JObject Pars_Obj = JObject.Parse(parameters);
        Dictionary<string, string> dictObj = Pars_Obj.ToObject<Dictionary<string, string>>();
        //------------------------------------------------------------------------------------
        FermerComment comm_item = new FermerComment();
        comm_item.bolus_id = Convert.ToInt16(dictObj["bolus_id"]);
        comm_item.animal_id = Convert.ToInt16(dictObj["animal_id"]);
        comm_item.time_stamp = DateTime.Parse(dictObj["time_stamp"]);
        comm_item.comment = dictObj["comment"];

        string user_id = GetUserIdByToken(dictObj["token"]);
        if (string.IsNullOrEmpty(user_id))
        {
            SendErrorResponse("user was not found");
        }
        comm_item.user_id = user_id;
        try
        {
            using (DB_A4A060_csEntities context = new DB_A4A060_csEntities())
            {
                context.FermerComments.Add(comm_item);
                context.SaveChanges();
                res_json = "MOB_SaveFermerComment: record was done";
            }
        }
        catch (Exception ex)
        {
            SendErrorResponse("MOB_SaveFermerComment: " + ex.Message);
        }
        JObject data = new JObject(
            new JProperty("status", "ok"),
            new JProperty("data", res_json)
            );
        SendOKResponse_1(data);
    }

    private bool IsTokenAlivePar(string parameters)
    {
        JObject Pars_Obj = JObject.Parse(parameters);
        Dictionary<string, string> dictObj = Pars_Obj.ToObject<Dictionary<string, string>>();
        return false;
    }
    private bool IsTokenAlive(string parameters)
    {
        // 1. check input parameters
        if (string.IsNullOrEmpty(parameters)) return false;

        //2. Deserialize parameters to Object 
        loginToken lgtn = new loginToken();
        lgtn = JsonConvert.DeserializeObject<loginToken>(parameters);

        if (string.IsNullOrEmpty(lgtn.token) || lgtn.token == "null") return false;

        //3. Check in database token
        try
        {
            using (DB_A4A060_csEntities context = new DB_A4A060_csEntities())
            {
                var tn = context.Mob_Token.Where(x => x.hashcode == lgtn.token ||
                                            x.username == lgtn.email
                        ).SingleOrDefault();

                // not found in database
                if (tn == null) return false;

                // else check datetime expiration
                DateTime dt_now = DateTime.UtcNow;
                DateTime dt_exp = tn.datetime_exp.Value;

                if (DateTime.Compare(dt_now, dt_exp) > 0)
                {
                    // delete expired token
                    context.Mob_Token.Remove(tn);
                    context.SaveChanges();
                    return false;
                }
                return true;
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message);
            return false;
        }
    }

    //GetIntakesData mob_09
    private void MOB_GetIntakesData(string parameters)
    {
        string res_json = string.Empty;

        JObject Pars_Obj = JObject.Parse(parameters);
        Dictionary<string, string> dictObj = Pars_Obj.ToObject<Dictionary<string, string>>();
        //------------------------------------------------------------------------------------
        //public static string GetDataForChart(
        string token = dictObj["token"];
        string dt_from = dictObj["dt_from"];
        string dt_to = dictObj["dt_to"];
        int bolus_id = Convert.ToInt16(dictObj["bolus_id"]);
        DateTime dt1 = DateTime.Parse(dt_from);
        DateTime dt2 = DateTime.Parse(dt_to);

        List<IntakesStr> ilist = new List<IntakesStr>();
        List<Intakes> intakesList = new List<Intakes>();
        try
        {
            using (DB_A4A060_csEntities context = new DB_A4A060_csEntities())
            {
                var res = context.WaterIntakes_sig(dt1, dt2, bolus_id, 2).Select(x => new
                {
                    arg = x.bolus_full_date,
                    val = x.intakes
                }).ToList();

                //Check if intakes info exits
                if (res.Count == 0)
                {
                    res_json = null;
                }
                else
                {
                    // Intakes data exits!
                    //var arr_date = context.MeasDatas.Where(x => x.bolus_id == bolus_id &&
                    //        (x.bolus_full_date >= dt1 && x.bolus_full_date <= dt2)).Select(
                    //    y => new
                    //    {
                    //        t = y.bolus_full_date.Value
                    //    }).OrderBy(y => y.t).ToList();

                    //DateTime[] arr_dt = new DateTime[arr_date.Count + res.Count];
                    //arr_dt[0] = dt1;
                    //Intakes p = new Intakes();
                    //p.val = 0.0;
                    //foreach (var item in arr_date)
                    //{
                    //    p.arg = item.t;
                    //    res.Add(new { p.arg, p.val });

                    //}
                    var resOrdered = res.OrderBy(t => t.arg);

                    foreach (var item in resOrdered)
                    {
                        intakesList.Add(new Intakes { arg = item.arg, val = item.val });
                    }

                    foreach (var item in intakesList)
                    {
                        ilist.Add(new IntakesStr
                        {
                            //arg = item.arg.Value.ToShortDateString() + " " + item.arg.Value.ToShortTimeString(),
                            arg = item.arg.Value.ToString("s"),
                            val = item.val
                        });
                    }
                }
            }
        }
        catch (Exception ex)
        {
            SendErrorResponse("GetIntakesData: " + ex.Message);
        }

        JObject data = new JObject(
            new JProperty("status", "ok"),
            new JProperty("data",
                new JArray(
                            from p in ilist
                            select new JObject(
                                new JProperty("arg", p.arg),
                                new JProperty("val", p.val))
                               )
                ));
        SendOKResponse_1(data);
    }

    //mob_8
    private void MOB_GetDataForChart(string parameters)
    {

        JObject Pars_Obj = JObject.Parse(parameters);
        Dictionary<string, string> dictObj = Pars_Obj.ToObject<Dictionary<string, string>>();
        //------------------------------------------------------------------------------------
        //public static string GetDataForChart(
        string token = dictObj["token"];
        string dt_from = dictObj["dt_from"];
        string dt_to = dictObj["dt_to"];
        int bolus_id = Convert.ToInt16(dictObj["bolus_id"]);
        List<BolusIDChart> result = new List<BolusIDChart>();

        //---------------------------------------------------------
        try
        {
            using (DB_A4A060_csEntities context = new DB_A4A060_csEntities())
            {
                //Temperature--------------------------------------------------------
                var result2 = context.ChartsXY_temp(DateTime.Parse(dt_from), DateTime.Parse(dt_to), bolus_id, 0.5);
                DateTime dtvar = new DateTime();
                foreach (var item in result2)
                {
                    dtvar = DateTime.Parse(item.t);
                    var it = new BolusIDChart
                    {
                        // t = item.t,
                        //t = dtvar.ToShortDateString() + " " + dtvar.ToShortTimeString(),
                        t = dtvar.ToString("s"),
                        Temperature = item.Temperature
                    };
                    result.Add(it);
                    it = null;
                }
            }
        }
        catch (Exception ex)
        {
            SendErrorResponse("mob_08: " + ex.Message);
        }
        JObject data = new JObject(
            new JProperty("status", "ok"),
            new JProperty("data",
                new JArray(
                            from p in result
                            select new JObject(
                                new JProperty("t", p.t),
                                new JProperty("temperature", p.Temperature))
                               )
                ));
        SendOKResponse_1(data);
    }

    private void MOB_GetFarmData(string parameters)
    {
        string res_json = string.Empty;
        loginToken lgt = new loginToken();
        lgt = JsonConvert.DeserializeObject<loginToken>(parameters);
        //1. retrive parameters
        string user_id = GetUserIdByToken(lgt.token);
        if (string.IsNullOrEmpty(user_id))
        {
            SendErrorResponse("action:mob_07: user was not found");
        }
        try
        {
            using (DB_A4A060_csEntities context = new DB_A4A060_csEntities())
            {
                var farmdata = context.Farms.Where(x => x.AspNetUser_Id == user_id).Select(t => new
                {
                    name = t.Name
                    //,
                    //owner = t.Owner
                }).ToList();
                res_json = farmdata[0].name;// + ", " + farmdata[0].owner;
            }
        }
        catch (Exception ex)
        {
            SendErrorResponse("action:mob_07 -- " + ex.Message);
        }
        JObject data = new JObject(
            new JProperty("status", "ok"),
            new JProperty("data", res_json.ToLower())

                );
        SendOKResponse_1(data);
    }

    //mob_06
    private void MOB_GetLostCowsList(string parameters)
    {
        loginToken lgt = new loginToken();
        lgt = JsonConvert.DeserializeObject<loginToken>(parameters);
        //1. retrive parameters
        string user_id = GetUserIdByToken(lgt.token);
        if (string.IsNullOrEmpty(user_id))
        {
            SendErrorResponse("user was not found");
        }
        //2. request Farm stat information
        DateTime dt = GetTorontoLocalDateTime();
        List<SP_GET_FarmLostCows_Result> result = new List<SP_GET_FarmLostCows_Result>();
        try
        {
            using (DB_A4A060_csEntities context = new DB_A4A060_csEntities())
            {
                result = context.SP_GET_FarmLostCows(user_id, 2, dt).ToList();
            }
        }
        catch (Exception ex)
        {
            SendErrorResponse("mob_06: " + ex.Message);
        }

        JObject data = new JObject(
                    new JProperty("status", "ok"),
                    new JProperty("data",
                        new JArray(
                                    from p in result
                                    select new JObject(
                                        new JProperty("bolus_id", p.bolus_id),
                                        new JProperty("animal_id", p.animal_id),
                                        new JProperty("lastdate", p.lastdate),
                                        new JProperty("current_stage_of_lactation", p.Current_Stage_Of_Lactation.ToLower())
                                       )
                        )));
        SendOKResponse_1(data);
    }

    private void MOB_GetDataIntegrity(string parameters)
    {
        loginToken lgt = new loginToken();
        lgt = JsonConvert.DeserializeObject<loginToken>(parameters);
        //1. retrive parameters
        string user_id = GetUserIdByToken(lgt.token);
        if (string.IsNullOrEmpty(user_id))
        {
            SendErrorResponse("user was not found");
        }
        //2. request Farm stat information
        DateTime dt = GetTorontoLocalDateTime().Date.AddDays(-1);
        dt = new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59);
        List<DataGapsFarm_Result> result = new List<DataGapsFarm_Result>();
        try
        {
            using (DB_A4A060_csEntities context = new DB_A4A060_csEntities())
            {
                result = context.DataGapsFarm(dt, 7, user_id).ToList();
            }
        }
        catch (Exception ex)
        {
            SendErrorResponse("mob_5: " + ex.Message);
        }

        JObject data = new JObject(
                new JProperty("status", "ok"),
                new JProperty("data",
                    new JArray(
                                from p in result
                                select new JObject(
                                    new JProperty("r", p.r),
                                    new JProperty("gaps", p.Gaps),
                                    new JProperty("date", p.DateCtrl)
                                   )
                    )));
        SendOKResponse_1(data);
    }

    //mob_3
    private void MOB_Farm_TodayEventsList(string parameters, string eventpar)
    {
        loginToken lgt = new loginToken();
        lgt = JsonConvert.DeserializeObject<loginToken>(parameters);
        //1. retrive parameters
        string user_id = GetUserIdByToken(lgt.token);
        if (string.IsNullOrEmpty(user_id))
        {
            SendErrorResponse("user was not found");
        }
        //2. request Farm stat information
        List<MOB_Farm_TodayEventsList_Result> result = new List<MOB_Farm_TodayEventsList_Result>();
        DateTime dt = GetTorontoLocalDateTime();
        using (DB_A4A060_csEntities context = new DB_A4A060_csEntities())
        {
            result = context.MOB_Farm_TodayEventsList(user_id, dt, eventpar).ToList();
        }
        //-----------------------------------------------------------------------------
        JObject data = new JObject(
            new JProperty("status", "ok"),
             new JProperty("data",
            new JArray(
                        from p in result
                        select new JObject(
                            new JProperty("id", p.id),
                            new JProperty("bolus_id", p.bolus_id),
                            new JProperty("animal_id", p.animal_id),
                            new JProperty("date_emailsent", p.date_emailsent),
                            new JProperty("@event", p.@event.ToLower()),
                            new JProperty("message", p.message.ToLower()),
                            new JProperty("read", p.read)
                            )
                        )));
        //---------------------------
        ;
        SendOKResponse_1(data);
    }

    private void SendOKResponse_1(JObject data)
    {
        var res_json = JsonConvert.SerializeObject(data);
        Response.Clear();
        Response.ContentType = "application/json;charset=UTF-8";
        Response.Write(res_json);
        Response.End();
    }

    private void MOB_AlertEventsByDateUserID(string parameters)
    {
        loginToken lgt = new loginToken();
        lgt = JsonConvert.DeserializeObject<loginToken>(parameters);
        //1. retrive parameters
        string user_id = GetUserIdByToken(lgt.token);
        if (string.IsNullOrEmpty(user_id))
        {
            SendErrorResponse("user was not found");
        }
        //2. request Farm stat information
        List<MOB_AlertEventsByDateUserID_Result> result = new List<MOB_AlertEventsByDateUserID_Result>();
        //-------------------------------------------------------------------
        try
        {
            using (DB_A4A060_csEntities context = new DB_A4A060_csEntities())
            {
                DateTime dd = DateTime.Parse("2020-09-22");
                result = context.MOB_AlertEventsByDateUserID(user_id, dd).ToList();
                //result = context.MOB_AlertEventsByDateUserID(user_id, DateTime.Now).ToList();
            }
        }
        catch (Exception ex)
        {
            SendErrorResponse("MOB_AlertEventsByDateUserID -- " + ex.Message);
        }
        JObject data = new JObject(
            new JProperty("status", "ok"),
             new JProperty("data",
            new JObject(
                        from p in result
                        select new JProperty(p.@event, p.number))
                        ));
        //---------------------------
        ;
        SendOKResponse_1(data);
    }

    private void ReturnTokenWrong(string msg)
    {
        SendErrorResponse(msg + " token is wrong");
    }


    private void MOB_GetFarmCowsInfo(string parameters)
    {
        loginToken lgt = new loginToken();
        lgt = JsonConvert.DeserializeObject<loginToken>(parameters);
        //1. retrive parameters
        //string user_id = GetUserIdByEmail(lgt.email);
        string user_id = GetUserIdByToken(lgt.token);
        if (string.IsNullOrEmpty(user_id))
        {
            SendErrorResponse("MOB_GetFarmCowsInfo: user was not found");
        }
        //2. request Farm stat information
        List<MOB_GetFarmCowsInfo_Result> result = new List<MOB_GetFarmCowsInfo_Result>();
        try
        {
            using (DB_A4A060_csEntities context = new DB_A4A060_csEntities())
            {
                result = context.MOB_GetFarmCowsInfo(user_id).ToList();
            }
        }
        catch (Exception ex)
        {
            SendErrorResponse("MOB_GetFarmCowsInfo -- " + ex.Message);
        }
        //-------------------------------------------------------------------
        JObject data = new JObject(
            new JProperty("status", "ok"),
             new JProperty("data",
            new JObject(
                        from p in result
                        select
                            new JProperty(p.lactname, p.lactnum)

                        )));
        SendOKResponse_1(data);
    }

    private string GetUserIdByEmail(string email)
    {
        string user_id = string.Empty;
        try
        {
            using (DB_A4A060_csEntities context = new DB_A4A060_csEntities())
            {
                user_id = context.Mob_Users.Where(x => x.email == email).SingleOrDefault().AspNetUser_ID;
                if (string.IsNullOrEmpty(user_id))
                {
                    return string.Empty;
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message);
            return string.Empty;
        }
        return user_id;
    }

    private string GetUserIdByToken(string token)
    {
        string user_id = string.Empty;
        try
        {
            using (DB_A4A060_csEntities context = new DB_A4A060_csEntities())
            {
                user_id = (from t in context.Mob_Token
                           join u in context.Mob_Users on t.username equals u.email
                           where t.hashcode == token
                           select new
                           {
                               id = u.AspNetUser_ID
                           }
                    ).SingleOrDefault().id;
                //context.Mob_Token.Where(x => x.hashcode == token).SingleOrDefault().AspNetUser_ID;
                if (string.IsNullOrEmpty(user_id))
                {
                    return string.Empty;
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex.Message);
            return string.Empty;
        }
        return user_id;
    }


    private void CheckLogin(string parameters)
    {
        if (string.IsNullOrEmpty(parameters))
        {
            SendErrorResponse("Input parameters are empty");
        }
        login lg = new login();
        lg = JsonConvert.DeserializeObject<login>(parameters);

        if (CheckUser(lg.email, lg.password))
        {
            //1. create new token
            TokenTix mt = new TokenTix(lg.email);

            //2. delete all existing tokens for user
            DeleteTokenFromDatabase(lg.email);

            //3. Save new token
            if (!mt.SaveToken())
            {
                SendErrorResponse("SaveToken: saving was failed.");
            }

            //4. return hash code Token

            JObject token = JObject.FromObject(new { token = mt.hashcode });
            SendOKResponse(token);
        }
        else
        {
            SendErrorResponse("credentials are wrong");
        }
    }

    private void SendOKResponse(JObject data)
    {
        ResponseOK rok = new ResponseOK();
        rok.data = data;
        var res_json = JsonConvert.SerializeObject(rok);
        Response.Clear();
        Response.ContentType = "application/json;charset=UTF-8";
        Response.Write(res_json);
        Response.End();
    }

    private void SendErrorResponse(string message)
    {
        ResponseError re = new ResponseError();
        re.message = message;
        Response.Clear();
        Response.ContentType = "application/json;charset=UTF-8";
        Response.Write(JsonConvert.SerializeObject(re));
        Response.End();
    }

    private void DeleteTokenFromDatabase(string email)
    {
        try
        {
            using (DB_A4A060_csEntities context = new DB_A4A060_csEntities())
            {
                var tl = context.Mob_Token.Where(t => t.username == email).ToList();
                context.Mob_Token.RemoveRange(tl);
                context.SaveChanges();
            }
        }
        catch (Exception ex)
        {
            SendErrorResponse("DeleteTokenFromDatabase: " + ex.Message);
            throw;
        }
    }

    private bool CheckUser(string email, string password)
    {
        bool result = false;
        try
        {
            using (DB_A4A060_csEntities context = new DB_A4A060_csEntities())
            {
                var u = context.Mob_Users.Where(x => x.email == email && x.passwordMD5 == password).SingleOrDefault();
                if (u != null)
                {
                    result = true;
                }
            }
        }
        catch (Exception ex)
        {
            // TO Do log writer-------------------------
            SendErrorResponse("CheckUser: " + ex.Message);
            throw;
        }
        return result;
    }

    public bool ExecuteProc(string sp_code, string parameters)
    {
        bool result = false;
        string connstr = "data source = SQL5055.site4now.net; initial catalog = DB_A4A060_cs; persist security info = True; user id = DB_A4A060_cs_admin; password = Nikita13;";
        SqlConnection con = new SqlConnection(connstr);
        SqlCommand cmd = new SqlCommand("[ExecProcByCode]", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@SP_CODE", sp_code));
        cmd.CommandTimeout = 3 * 60;

        SqlDataReader rdr = null;
        try
        {
            con.Open();
            rdr = cmd.ExecuteReader();
            result = true;
        }
        catch (Exception ex)
        {
            var x = ex.Message;
        }
        finally
        {
            con.Close();
            if (rdr != null) rdr.Close();
        }
        return result;
    }

    public string MD5Hash(string input)
    {
        StringBuilder hash = new StringBuilder();
        MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
        byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

        for (int i = 0; i < bytes.Length; i++)
        {
            hash.Append(bytes[i].ToString("x2"));
        }
        return hash.ToString();
    }

    public DateTime GetTorontoLocalDateTime()
    {
        var timeUtc = DateTime.UtcNow;
        TimeZoneInfo easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
        DateTime easternTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, easternZone);
        //----------------------------------------------------------------------------
        //return tdTor;
        return easternTime;
    }

    private static bool IsTokenTrue(string token)
    {
        if (string.IsNullOrEmpty(token) || token == "null") return false;
        bool result = false;
        try
        {
            using (DB_A4A060_csEntities context = new DB_A4A060_csEntities())
            {
                Mob_Token tn = context.Mob_Token.Where(x => x.hashcode == token).SingleOrDefault();

                if (tn != null)
                {
                    DateTime dt_now = DateTime.UtcNow;
                    DateTime dt_exp = tn.datetime_exp.Value;

                    if (DateTime.Compare(dt_now, dt_exp) > 0)
                    {
                        // delete expired token
                        context.Mob_Token.Remove(tn);
                        context.SaveChanges();
                    }
                    else
                    {
                        result = true;
                    }
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
        return result;
    }

    //General methods
    private bool IsInputParametersOK(string procname, string parameters, string[] par_name)
    {
        JObject Pars_Obj = JObject.Parse(parameters);
        int par_num = Pars_Obj.Count;

        //1. check parameters number
        if (Pars_Obj.Count != par_name.Length) SendErrorResponse(procname + ": wrong parameters number");

        //2. check parameters existance 
        foreach (var item in par_name)
        {
            if ((string)Pars_Obj.Root[item] == null)
            {
                SendErrorResponse(procname + ":" + item + " is null");
            }
        }
        return true;
    }
}
var service_URL = 'Default.aspx';
//var service_URL ="http://193.27.73.63/Mob_service/Default.aspx";

$(function () {
    $("#token").dxTextBox({
        value: "null"
    });
    $("#device_token").dxTextBox({
        value: "cUyUH-"
    });
    $("#FarmStat").dxTextBox({
        value: ""
    });
    var exampleTextArea = $("#result-textarea").dxTextArea({
        value: "",
        height: 90
    }).dxTextArea("instance");

    $("#post-buttons").dxButtonGroup({
        items: buttonList,
        keyExpr: "query",
        stylingMode: "outlined",
        selectedItemKeys: ["left"],
        onItemClick: function (e) {
            //DevExpress.ui.notify({ message: 'The "' + e.itemData.hint + '" button was clicked', width: 320 }, "success", 1000);
            switch (e.itemData.query) {
                case "mob_0":
                    PostFarmStat("mobil@mob.mo", "09ebc5cb5447dd77465cf1001634943a", null, "mob_0");
                    //PostFarmStat("willembloemen@gmail.com", "5642ec030437f933af3989d2db87d1ef", null, "mob_0");
                   // PostFarmStat("jordan@stonecreekfarms.com", "b742faa6dc057775d5e6b18828513bd6", null, "mob_0");
                    //PostFarmStat("c@c.c", "5993d70589c68b1dcc5e03013c17a46e", null, "mob_0");
                    //PostFarmStat("wlstrenzke@gmail.com", "1f2e76ad94b1a720479c51b0f1cd861c", null, "mob_0");
                    break;
                case "mob_11":
                    //(email, password, token, action)
                    PostFarmStat_token($("#token").dxTextBox("instance").option('value'), "mob_1");
                    break;
                case "GetAnimalList":
                    //(email, password, token, action)
                    PostFarmStat_token($("#token").dxTextBox("instance").option('value'), "GetAnimalList");
                    break;
                default:
                    var tn1 = $("#token").dxTextBox("instance").option('value');
                    PostFarmStat("mobil@mob.mo", "", tn1, e.itemData.query);
                    break;
            }
        }
    });
    //-- Button group 2----------------Post with parameters
    $("#post-buttons2").dxButtonGroup({
        items: buttonList2,
        keyExpr: "query",
        stylingMode: "outlined",
        selectedItemKeys: ["left"],
        onItemClick: function (e) {
            //DevExpress.ui.notify({ message: 'The "' + e.itemData.hint + '" button was clicked', width: 320 }, "success", 1000);
            switch (e.itemData.query) {
                case "mob_8":
                    var data = {};
                    data.dt_from = '2020-08-30';
                    data.dt_to = '2020-08-31';
                    data.bolus_id = 116;
                    data.token = $("#token").dxTextBox("instance").option('value');
                    DataPostWithParameters("GetDataForChart", data)
                    break;
                case "mob_9":
                    var data = {};
                    data.dt_from = '2020-08-30';
                    data.dt_to = '2020-08-31';
                    data.bolus_id = 116;
                    data.token = $("#token").dxTextBox("instance").option('value');
                    DataPostWithParameters("GetIntakesData", data)
                    break;
                case "ReadComment":
                    var data = {};
                    data.token = $("#token").dxTextBox("instance").option('value');
                    data.bolus_id = 21;
                    DataPostWithParameters("ReadFermerComment", data)
                    break;
                case "SaveComment":
                    var data = {};
                    data.token = $("#token").dxTextBox("instance").option('value');
                    data.bolus_id = 21;
                    data.animal_id = 848;
                    //data.user_id = '14267301-d8d6-4974-a868-7e9cb5abd1b6';
                    data.time_stamp = '2020-09-7';
                    data.comment = 'test comment 7777777777777';
                    DataPostWithParameters("SaveFermerComment", data)
                    break;
                case "DeleteComment":
                    var data = {};
                    data.token = $("#token").dxTextBox("instance").option('value');
                    data.id = 9;
                    DataPostWithParameters("DeleteFermerComment", data)
                    break;
                case "UpdateComment":
                    var data = {};
                    data.token = $("#token").dxTextBox("instance").option('value');
                    data.id = 8;
                    data.bolus_id = 21;
                    data.animal_id = 848;
                    //data.user_id = '14267301-d8d6-4974-a868-7e9cb5abd1b6';
                    data.time_stamp = '2020-09-7 12:00 PM';
                    data.comment = 'test comment update555555555';
                    DataPostWithParameters("UpdateFermerComment", data)
                    break;
            }
        }
    });
    //-----------------------------------------------------
    $("#post-buttons3").dxButtonGroup({
        items: buttonList3,
        keyExpr: "query",
        stylingMode: "outlined",
        selectedItemKeys: ["left"],
        onItemClick: function (e) {
            PostFarmStat_token($("#token").dxTextBox("instance").option('value'), e.itemData.query);
        }
    });
    //------------------------------------------------------
    $("#post-buttons4").dxButtonGroup({
        items: buttonList4,
        keyExpr: "query",
        stylingMode: "outlined",
        selectedItemKeys: ["left"],
        onItemClick: function (e) {
            switch (e.itemData.query) {
                case "mob_2":
                case "mob_3":
                case "mob_4":
                case "mob_5":
                case "mob_6":
                    PostFarmStat_token($("#token").dxTextBox("instance").option('value'), e.itemData.query);
                    break;
                case "alert_details":
                    var data = {};
                    data.id = 116;
                    data.token = $("#token").dxTextBox("instance").option('value');
                    DataPostWithParameters("alert_details", data)
                    break;
                case "mob_8":
                    var data = {};
                    data.dt_from = '2020-08-30';
                    data.dt_to = '2020-08-31';
                    data.bolus_id = 116;
                    data.token = $("#token").dxTextBox("instance").option('value');
                    DataPostWithParameters("GetDataForChart", data)
                    break;
                case "mob_9":
                    var data = {};
                    data.dt_from = '2020-08-30';
                    data.dt_to = '2020-08-31';
                    data.bolus_id = 116;
                    data.token = $("#token").dxTextBox("instance").option('value');
                    DataPostWithParameters("GetIntakesData", data)
                    break;
                case "SaveComment":
                    var data = {};
                    data.token = $("#token").dxTextBox("instance").option('value');
                    data.bolus_id = 21;
                    data.animal_id = 848;
                    data.user_id = '14267301-d8d6-4974-a868-7e9cb5abd1b6';
                    data.time_stamp = '2020-09-7';
                    data.comment = 'test comment xxxxxxxxxxxxxxxxxxxxxxxxxx';
                    DataPostWithParameters("SaveFermerComment", data)
                    break;
                case "DeleteComment":
                    var data = {};
                    data.token = $("#token").dxTextBox("instance").option('value');
                    data.id = 1;
                    DataPostWithParameters("DeleteFermerComment", data)
                    break;
                case "UpdateComment":
                    var data = {};
                    data.token = $("#token").dxTextBox("instance").option('value');
                    data.id = 2;
                    data.bolus_id = 21;
                    data.animal_id = 848;
                    data.user_id = '14267301-d8d6-4974-a868-7e9cb5abd1b6';
                    data.time_stamp = '2020-09-7 12:00 PM';
                    data.comment = 'test comment update2222';
                    DataPostWithParameters("UpdateFermerComment", data);
                    break;
            }
        }
    });
    //------------------------------------------------------
    $("#post-buttons5").dxButtonGroup({
        items: buttonList5,
        keyExpr: "query",
        stylingMode: "outlined",
        selectedItemKeys: ["left"],
        onItemClick: function (e) {
            switch (e.itemData.query) {
                case "alert_list":
                    var data = {};
                    data.token = $("#token").dxTextBox("instance").option('value');
                    break;
                case "alert_list1":
                    var data = {};
                    data.page = 1;
                    data.token = $("#token").dxTextBox("instance").option('value');
                    break;
                case "alert_list2":
                    var data = {};
                    data.page = 1;
                    data.bolus_id = 128;
                    data.token = $("#token").dxTextBox("instance").option('value');
                    break;
                case "alert_list3":
                    var data = {};
                    data.page = 1;
                    data.animal_id = 112;
                    data.token = $("#token").dxTextBox("instance").option('value');
                    break;
            }
            DataPostWithParameters("alert_list", data);
        }
    });
    //-----------------------------------------------------
    $("#post-buttons6").dxButtonGroup({
        items: buttonList6,
        keyExpr: "query",
        stylingMode: "outlined",
        selectedItemKeys: ["left"],
        onItemClick: function (e) {
            switch (e.itemData.query) {
                case "cow_details":
                    var data = {};
                    data.bolus_id = 11;
                    data.token = $("#token").dxTextBox("instance").option('value');
                    DataPostWithParameters("cow_details", data);
                    break;
                case "cow_details_update":
                    var data = {};
                    data.token = $("#token").dxTextBox("instance").option('value');
                    data.bolus_id = 11;
                    data.age_lactation = 24;
                    data.current_stage_of_lactation = 'open';
                    data.comments = 'new comment 777777777';
                    data.calving_due_date = '2020-11-17';
                    data.actual_calving_date = '2020-12-03';
                    data.current_lactation = 3;
                    data.status = 1;

                    DataPostWithParameters("cow_details_update", data);
                    break;
            }
        }
    });
    //-----------------------------------------------------
    $("#post-buttons8").dxButtonGroup({
        items: buttonList8,
        keyExpr: "query",
        stylingMode: "outlined",
        selectedItemKeys: ["left"],
        onItemClick: function (e) {
            switch (e.itemData.query) {
                case "create_event":
                    var data = {};
                    data.token = $("#token").dxTextBox("instance").option('value');
                    data.bolus_id = 12;
                    data.diagnosis = "Retain Placenta";
                    data.comment = "new comment 3333";
                    data.selected_date = "2020-10-19 12:15";
                    break;
                case "update_event":
                    var data = {};
                    data.token = $("#token").dxTextBox("instance").option('value');
                    data.event_id = 3;
                    data.bolus_id = 11;
                    data.diagnosis = "up Retain Placenta";
                    data.comment = "up new comment 3333";
                    data.selected_date = "2020-10-19 15:15";
                    break;
                case "delete_event":
                    var data = {};
                    data.token = $("#token").dxTextBox("instance").option('value');
                    data.event_id = 5;
                    break;
                case "list_events":
                    var data = {};
                    data.token = $("#token").dxTextBox("instance").option('value');
                    //data.bolus_id = 11;
                    break;
            }
            DataPostWithParameters(e.itemData.query, data);
        }
    });
    //----------------------------------------------------
    $("#post-buttons9").dxButtonGroup({
        items: buttonList9,
        keyExpr: "query",
        stylingMode: "outlined",
        selectedItemKeys: ["left"],
        onItemClick: function (e) {
            switch (e.itemData.query) {
                case "add_feedback":
                    var data = {};
                    data.token = $("#token").dxTextBox("instance").option('value');
                    data.alert_id = 4;
                    data.visual_symptoms = 1;
                    data.rectal_temperature = 40;
                    data.rectal_temperature_measuring_time = "2020-10-19 12:15";
                    data.diagnosis = ["Mastitis", "Metrisis", "LDA"];
                    data.treatment_note = "treatment_note";
                    data.general_note = "general_note";

                    break;
                case "update_feedback":
                    var data = {};
                    data.token = $("#token").dxTextBox("instance").option('value');
                    data.id = 7;
                    data.visual_symptoms = 1;
                    data.rectal_temperature = 55;
                    data.rectal_temperature_measuring_time = "2020-10-30 17:15";
                    data.diagnosis = ["Mastitis", "LDA"];
                    data.treatment_note = "treatment_note 0000";
                    data.general_note = "general_note 0000";
                    break;
                case "delete_feedback":
                    var data = {};
                    data.token = $("#token").dxTextBox("instance").option('value');
                    data.id = 5;
                    break;
                case "list_feedback":
                    var data = {};
                    data.token = $("#token").dxTextBox("instance").option('value');
                    data.event_id = 4;
                    break;
                case "status_alert":
                    var data = {};
                    data.token = $("#token").dxTextBox("instance").option('value');
                    data.alert_id = 9762;
                    data.read = null;
                    data.feedback = 1;
                    break;
                case "save_device_token":
                    var data = {};
                    data.token = $("#token").dxTextBox("instance").option('value');
                    data.device_token = $("#device_token").dxTextBox("instance").option('value');
                    break;
                case "get_intakes":
                    data = {};
                    data.token = $("#token").dxTextBox("instance").option('value');
                    data.bolus_id = 113;
                    data.dt1 = '2020-11-04';
                    data.dt2 = '2020-11-05';
                    break;
            }
            DataPostWithParameters(e.itemData.query, data);
        }
    });

});
var buttonList = [
    {
        icon: "info",
        query: "mob_0",
        hint: "Request token",
        text: "Login",
        type: "danger"
    },
    {
        icon: "user",
        query: "GetAnimalList",
        text: "Get Animal List",
        type: "success"
    }
];
var buttonList2 = [
    {
        icon: "upload",
        query: "mob_8",
        hint: "Get Chart data",
        text: "Get Chart data mob_8",
        type: "success"
    },
    {
        icon: "upload",
        query: "mob_9",
        hint: "Get Intakes Data",
        text: "Get Intakes Data mob_9",
        type: "success"
    },
    {
        icon: "like",
        query: "ReadComment",
        hint: "Read Comment",
        text: "Read Comment *",
        type: "success"
    }, ,
    {
        icon: "like",
        query: "SaveComment",
        hint: "Save Comment",
        text: "Save Comment*",
        type: "success"
    },
    {
        icon: "like",
        query: "UpdateComment",
        hint: "Update Comment",
        text: "Update Comment*",
        type: "success"
    },
    {
        icon: "like",
        query: "DeleteComment",
        hint: "Delete Comment",
        text: "Delete Comment*",
        type: "success"
    }
];
var buttonList3 = [
    {
        icon: "info",
        query: "mob_1",
        hint: "Farm Stat",
        text: "Farm Stat mob_1"
    },
    {
        icon: "info",
        query: "mob_7",
        hint: "Farm name",
        text: "Get Farm name"

    },
    {
        icon: "globe",
        query: "alert_list",
        text: "Full Alert List"
    }
];
var buttonList4 = [
    {
        icon: "info",
        query: "mob_2",
        hint: "Farm Stat",
        text: "Farm Stat Events mob_2"
    },
    {
        icon: "info",
        query: "mob_3",
        hint: "Alerts Q41 ",
        text: "Alerts Q41 mob_3"

    },
    {
        icon: "info",
        query: "mob_4",
        hint: "Q40.5 today",
        text: "Q40.5 today mob_4"
    },
    {
        icon: "info",
        query: "alert_details",
        hint: "alert_details",
        text: "alert_details"
    },
    {
        icon: "info",
        query: "mob_5",
        hint: "Integrity today",
        text: "Integrity today mob_5"

    },
    {
        icon: "info",
        query: "mob_6",
        hint: "Lost Cows List today",
        text: "Lost Cows List today mob_6"

    }
];
var buttonList5 = [
    {
        icon: "info",
        query: "alert_list",
        text: "Farm Today alert_list full"
    },
    {
        icon: "info",
        query: "alert_list1",
        text: "Today alert_list 1 page"
    },
    {
        icon: "info",
        query: "alert_list2",
        text: "Today alert_list 1 page bolus_id"
    }
    ,
    {
        icon: "info",
        query: "alert_list3",
        text: "Today alert_list 1 page animal_id"
    },
    ,
    {
        icon: "background",
        query: "cow_details",
        text: "Cow Details by bolus_id"
    }
];
var buttonList6 = [
    {
        icon: "background",
        query: "cow_details",
        text: "Cow Details by bolus_id"
    },
    {
        icon: "background",
        query: "cow_details_update",
        text: "Cow update details"
    }
];
var buttonList8 = [
    {
        icon: "gift",
        query: "list_events",
        text: "Get list of events"
    },
    {
        icon: "gift",
        query: "create_event",
        text: "Create event"
    },
    {
        icon: "gift",
        query: "update_event",
        text: "Update event"
    },
    {
        icon: "gift",
        query: "delete_event",
        text: "Delete event"
    }
];
var buttonList9 = [
    {
        icon: "comment",
        query: "list_feedback",
        text: "Get list Feedback"
    },
    {
        icon: "comment",
        query: "add_feedback",
        text: "Add feedback"
    },
    {
        icon: "comment",
        query: "update_feedback",
        text: "Update Feedback"
    },
    {
        icon: "comment",
        query: "delete_feedback",
        text: "Delete Feedback"
    },
    {
        icon: "comment",
        query: "status_alert",
        text: "Set status of alert"
    }, 
    {
        icon: "tel",
        query: "save_device_token",
        text: "Save device token"
    },
    {
        icon: "tel",
        query: "get_intakes",
        text: "Get intakes Sum"
    }
];
//---------------------------------------

//Login , request token----------------------------------------
function PostFarmStat(email, password, token, action) {
    var url = service_URL + '?action=' + action;
    var data = {};
    data.email = email;
    data.password = password;
    data.token = token;
    var dd = JSON.stringify(data);
    var x = $.post(url, dd).done(function (data) {
        switch (action) {
            case "mob_0":
                $("#token").dxTextBox("instance").option('value', data.data.token);
                break;
            default:
        }
        if (action == "mob_1") $("#FarmStat").html(data);
        if (action == "mob_2") $("#FarmStat").html(data);
        if (action == "mob_3") Fillriskdata(data);
        if (action == "mob_4") Fillcheckdata(data);
        if (action == "mob_5") FillDataIntegrity(data);
        if (action == "mob_6") FillLostCowsList(data);
        if (action == "mob_7") {
            var xx = data[0].name + '  ' + data[0].owner;
            $("#FarmStat").html(xx);
        }
        return;
    });
}
//--Post ---------------------------------------------------------------------------------
function PostFarmStat_token(token, action) {
    var url = service_URL + '?action=' + action;
    var data = {};
    data.token = token;
    var dd = JSON.stringify(data);
    var x = $.post(url, dd).done(function (data) {
        switch (action) {
            case "mob_1":
            case "mob_2":
            case "mob_7":
                $("#FarmStat").html(JSON.stringify(data.data));
                break;
            case "mob_3":
                Fillriskdata(data.data);
                break;
            case "mob_4":
                Fillcheckdata(data.data);
                break;
            case "mob_5":
                FillDataIntegrity(data.data);
                break;
            case "mob_6":
                FillLostCowsList(data.data);
                break; GetAnimalList
            case "GetAnimalList":
            case "alert_list":
                Fill_data(data.data);
                break;
            default:
                break;
        }
        return;
    });
}

//----------------------------------------------------------------------------------------
function DataPostWithParameters(action, data) {

    var url = service_URL + '?action=' + action;

    var dd = JSON.stringify(data);
    var x = $.post(url, dd).done(function (data) {
        //Object { status: "error", message: "get_intakes: dt2 is null" }
        if (data.status =="error") {
            $("#FarmStat").html(data.message);
            return;
        }
        switch (action) {
            case "alert_list":
            case "alert_list1":
            case "alert_list2":
            case "alert_list3":
            case "GetDataForChart":
            case "GetIntakesData":
            case "ReadFermerComment":
            case "list_events":
            case "list_feedback":
                Fill_data(data.data);
                break;
            case "SaveFermerComment":
            case "DeleteFermerComment":
            case "UpdateFermerComment":

                $("#FarmStat").html(data.data);
                break;
            case "alert_details":
            case "cow_details":
            case "cow_details_update":
            case "create_event":
            case "update_event":
            case "delete_event":
            case "add_feedback":
            case "update_feedback":
            case "delete_feedback":
            case "status_alert":
            case "save_device_token":
            case "get_intakes":
                $("#FarmStat").html(JSON.stringify(data.data));
                break;
            default:
                break;
        }
        return;
    });
}

//--Post with parameters---------------------------------------------------------------------------------

// POST static webmethod
function RequestDataPostParams(action, data) {
    //----------------------------------------------------
    var jsonText = JSON.stringify(data);

    $.ajax({
        type: "POST",
        url: service_URL + "/" + action,
        data: jsonText,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        headers: {
            "Access-Control-Allow-Headers": "*",
            "Cache-Control": "private"
        },
        success: function (data) {
            //$("#Result").dxTextBox('instance').option('value', data.d);
            alert(data.d);
        },
        failure: function (response) {
            alert(response.d);
        }
    });
}


//---Table Area---------------------------------------------------------------------------
function Fillriskdata(riskdata) {
    $("#gridRisk").dxDataGrid({
        dataSource: riskdata,
        showBorders: true,
        paging: {
            pageSize: 10
        },
        pager: {
            showPageSizeSelector: true,
            allowedPageSizes: [5, 10],
            showInfo: true
        },
        loadPanel: {
            enabled: true
        },
        columns: [
            {
                caption: "Animal_id",
                cssClass: 'cls',
                dataField: "animal_id",
                alignment: 'center',
                width: 80,
                cellTemplate: function (cellElement, cellInfo) {
                    var d = new Date();
                    var dt = d.getFullYear() + '-' + Number(d.getMonth() + 1) + '-' + d.getDate();
                    $('<a/>')
                        .attr({
                            //href: 'BolusChart?Animal_id=' + cellInfo.value + '&Bolus_id=' + cellInfo.key + '&DateSearch=' + dt + '&SP=ShowChart'
                            href: 'CowPage?bid_ext=' + cellInfo.key + '&dt_ext=' + dt
                        })
                        .text(cellInfo.value)
                        .appendTo(cellElement);
                }
            },
            {
                cssClass: 'cls',
                alignment: 'center',
                caption: "Event",
                dataField: "@event",
                width: 60
            },
            {
                caption: "Message",
                cssClass: 'cls',
                dataField: "message",
                alignment: 'center',
                width: 550
            },
            {
                cssClass: 'cls',
                alignment: 'center',
                caption: "Date",
                dataField: "date_emailsent",
                dataType: "date",
                format: "yyyy-MM-dd hh:mm:ss",
                width: 150
            }
        ]
        ,
        onContentReady: function (e) {
            e.component.option("loadPanel.enabled", false);
        }
    });
}

function Fillcheckdata(checkdata) {
    $("#gridRisk").dxDataGrid({
        dataSource: checkdata,
        showBorders: true,
        paging: {
            pageSize: 10
        },
        pager: {
            showPageSizeSelector: true,
            allowedPageSizes: [5, 10],
            showInfo: true
        },
        loadPanel: {
            enabled: true
        },
        onContentReady: function (e) {
            e.component.option("loadPanel.enabled", false);
        },
        columns: [
            {
                cssClass: 'cls',
                alignment: 'center',
                caption: "Animal_id",
                dataField: "animal_id",
                alignment: 'center',
                width: 80,
                cellTemplate: function (cellElement, cellInfo) {
                    var d = new Date();
                    var dt = d.getFullYear() + '-' + Number(d.getMonth() + 1) + '-' + d.getDate();
                    $('<a/>')
                        .attr({
                            href: 'CowPage?bid_ext=' + cellInfo.key + '&dt_ext=' + dt
                        })
                        .text(cellInfo.value)
                        .appendTo(cellElement);
                }
            },
            {
                cssClass: 'cls',
                alignment: 'center',
                caption: "Event",
                dataField: "@event",
                width: 60
            },
            {
                cssClass: 'cls',
                alignment: 'center',
                caption: "Message",
                dataField: "message",
                width: 550
            },
            {
                cssClass: 'cls',
                alignment: 'center',
                caption: "Date",
                dataField: "date_emailsent",
                dataType: "date",
                format: "yyyy-MM-dd hh:mm:ss",
                width: 150
            }
        ]
    });
}

function FillDataIntegrity(dataIntegrity) {
    var salesPivotGrid = $("#gridDataIntegrity").dxPivotGrid({
        fieldChooser: false,
        showBorders: true,
        showRowGrandTotals: false,
        showColumnGrandTotals: false,
        showRowTotals: false,
        loadPanel: {
            enabled: true
        },
        onContentReady: function (e) {
            e.component.option("loadPanel.enabled", false);
        },
        onCellPrepared: function (e) {
            if (e.area == "data") {
                e.cellElement.css("text-align", "center");
                e.cellElement.css("font-weight", "bold");

                if (Number(e.cell.value) <= 5.0) {
                    e.cellElement.css("color", "green");
                    e.cellElement.attr("title", "Acceptable Data Loss");
                }
                else {
                    e.cellElement.css("color", "red");
                }
            }
        },

        dataSource: {
            fields: [
                {
                    dataField: "date",
                    dataType: "string",
                    area: "column"
                }, {

                    caption: "gaps",
                    dataField: "gaps",
                    dataType: "number",
                    summaryType: "sum",
                    format: "decimal",
                    area: "data"
                },
                {
                    caption: "gaps, %",
                    dataField: 'r',
                    area: "row"
                }
            ],
            store: dataIntegrity
        }
    }).dxPivotGrid("instance");
}

function FillLostCowsList(dataLostCows) {

    $("#gridRisk").dxDataGrid({
        dataSource: dataLostCows,
        showBorders: true,
        paging: {
            pageSize: 10
        },
        pager: {
            showPageSizeSelector: true,
            allowedPageSizes: [5, 10],
            showInfo: true
        },
        loadPanel: {
            enabled: true
        },
        onContentReady: function (e) {
            e.component.option("loadPanel.enabled", false);
        },
        columns: [
            {
                cssClass: 'cls',
                alignment: 'center',
                caption: "Animal_id",
                dataField: "animal_id",
                alignment: 'center',
                width: 80,
                cellTemplate: function (cellElement, cellInfo) {
                    var d = new Date();
                    var dt = d.getFullYear() + '-' + Number(d.getMonth() + 1) + '-' + d.getDate();
                    $('<a/>')
                        .attr({
                            href: 'CowPage?bid_ext=' + cellInfo.key + '&dt_ext=' + dt
                        })
                        .text(cellInfo.value)
                        .appendTo(cellElement);
                }
            },
            {
                cssClass: 'cls',
                alignment: 'center',
                caption: "Date",
                dataField: "lastdate",
                dataType: "date",
                format: "yyyy-MM-dd hh:mm:ss",
                width: 150
            },
            {
                cssClass: 'cls',
                alignment: 'center',
                caption: "Lactation Stage",
                dataField: "current_stage_of_lactation",
                width: 150
            }
        ]
    });
}

function Fill_data(data) {
    $("#gridRisk").dxDataGrid({
        dataSource: data,
        showBorders: true,
        paging: {
            pageSize: 10
        },
        pager: {
            showPageSizeSelector: true,
            allowedPageSizes: [5, 10],
            showInfo: true
        },
        loadPanel: {
            enabled: true
        },
        onContentReady: function (e) {
            e.component.option("loadPanel.enabled", false);
        }
    });
}

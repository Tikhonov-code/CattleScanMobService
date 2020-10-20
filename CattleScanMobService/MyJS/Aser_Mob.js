$(function () {
    $("#param").dxTextBox({ value: "null" });
    $("#Result").dxTextBox({ value: "000000000000000" });

    $("#post-buttons").dxButtonGroup({
        items: buttonList,
        keyExpr: "query",
        stylingMode: "outlined",
        selectedItemKeys: ["left"],
        onItemClick: function (e) {
  
                    PostReqFun(e.itemData.query);
        }
    });
});

var buttonList = [
    {
        icon: "user",
        query: "GetData",
        //hint: "Request token",
        text: "GetData"
    }
];
//---------------------------------------

function PostReqFun( action) {

    //----------------------------------------------------
    var mes = $("#param").dxTextBox('instance').option('value');
    var data ={};
    data.dt ='2020-08-30';
    data.user_id ='20595462-e9cd-40a7-81e9-08fc7fdbaa4c';
    data.days=30;
    data.eventpar ='Q40.5';
    var jsonText = JSON.stringify(data);

    $.ajax({
        type: "POST",
        url: "service_Mob.aspx/" + action,
        data: jsonText,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $("#Result").dxTextBox('instance').option('value',data.d);
        },
        failure: function(response) {
            alert(response.d);
        }
    });
}
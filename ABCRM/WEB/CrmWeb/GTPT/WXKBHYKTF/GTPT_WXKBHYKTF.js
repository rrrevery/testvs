vUrl = "../GTPT.ashx";
vCaption = "微信卡包会员卡投放设置";
sendToWX = false;//是否需要发送消息到微信
var selectId = -1;



$(document).ready(function ()
{
    $("#B_Insert").hide();
    $("#B_Exec").hide();
    $("#status-bar").hide();
    $("#selectPublicID").combobox({
        onSelect: function (record)
        {
            iWXPID = record.value;
            sWXPIF = record.pif;
        }
    });
    $.parser.parse("#WXPublicID");
    FillPublicID($("#selectPublicID"));
    Getwxcard();
});

function IsValidData()
{
    if ($("#TB_CARDID").val() == "")
    {
        ShowMessage("请输入微信卡包会员卡ID", 3);
        return false;
    }
    if ($("#TB_QRCODEURL").val() == "" && $("#TA_CONTENT").val())
    {
        ShowMessage("请至少生成一种投放方式", 3);
        return false;
    }
    return true;
}

function SaveData()
{
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = 0;
    Obj.sCARDID = $("#TB_CARD").val();
    Obj.sCONTENT = $("#TA_CONTENT").val();
    Obj.sQRCODEURL = $("#TB_QRCODEURL").val();
    Obj.sOLDCARDID = $("#HF_CARD").val();
    Obj.iLoginPUBLICID = iWXPID;
    return Obj;
}
function ShowData(data)
{
    Getwxcard();
}

function ShowQRCODE()
{
    var Obj = new Object();
    Obj.card_id = $("#TB_CARD").val();
    $.ajax({
        type: "post",
        url: "../GTPT_WXKBHYKTFSZ.ashx?func=QRCODE&PUBLICID=" + iWXPID + "&PUBLICIF=" + sWXPIF,
        async: true,
        data: {
            postData: JSON.stringify(Obj),
        },
        success: function (data)
        {
            var Obj = JSON.parse(data);

            $("#TB_QRCODEURL").val(Obj.show_qrcode_url);
        },
        error: function (data)
        {
            ShowMessage(data);
        }
    });

}

function ShowCONTENT()
{
    var Obj = new Object();
    Obj.card_id = $("#TB_CARD").val();
    $.ajax({
        type: "post",
        url: "../GTPT_WXKBHYKTFSZ.ashx?func=CONTENT&PUBLICID=" + iWXPID + "&PUBLICIF=" + sWXPIF,
        async: true,
        data: {
            postData: JSON.stringify(Obj),
        },
        success: function (data)
        {
            var Obj = JSON.parse(data);
            $("#TA_CONTENT").val(Obj.content);
        },
        error: function (data)
        {
            ShowMessage(data);
        }
    });
}
function Getwxcard()
{
    var str = GetWXCARDData();
    $("#HF_CARD").val("");
    if (str == "null" || str == "")
    {
        return;
    }
    if (str.indexOf("错误") >= 0)
    {
        ShowMessage(str);
        return;
    }
    var Obj = JSON.parse(str);
    $("#TB_CARD").val(Obj.sCARDID);
    $("#TA_CONTENT").val(Obj.sCONTENT);
    $("#TB_QRCODEURL").val(Obj.sQRCODEURL);
    $("#HF_CARD").val(Obj.sCARDID);
    $("#selectPublicID").combobox("setValue", Obj.iPUBLICID)
    //$("#TB_JLBH").val("1");
    //vJLBH = "1";
    document.getElementById("B_Update").disabled = false;
    //ShowDataBase(vJLBH, iWXPID);
}

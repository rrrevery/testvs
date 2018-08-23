vUrl = "../HYKGL.ashx";
hyk = GetUrlParam("hyk");
czk = GetUrlParam("czk");
var HYKNO = GetUrlParam("HYKNO");

vXKFS = 0;
if (hyk == 1)
    vCaption = "会员卡补磁";
else
    vCaption = "库存会员卡补磁";

$(document).ready(function () {

    if (HYKNO != "") {
        $("#TB_CZKHM").val(HYKNO);
        //GetHYXX();
        //vProcStatus = cPS_ADD;
        //SetControlBaseState();
        //$("#TB_HYKHM_OLD").attr("readonly", "readonly");
        ProcIt();
    }

    vProcStatus = cPS_ADD;
    SetControlBaseState();
    FillBGDDTree("TreeBGDD", "TB_BGDDMC", "menuContent");
    $("#TB_CZKHM").bind("keypress blur", function (event) {
        if (event.keyCode != 13) {
            return;
        }
        ProcIt();
    });

    RefreshButtonSep();

    $("#B_HYKBC").click(function () {
        if ($("#TB_CZKHM").val() == "") {
            return;
        }
        if ($("#HF_CDNR").val() == "") {
            ProcIt();
        }
        var sCDNR = "";
        var iTrack = 2;
        if (vXKFS == 0)
            sCDNR = $("#HF_CDNR").val();
        if (vXKFS == 1) {
            sCDNR = $("#HF_CDNR").val() + "a" + $("#TB_CZKHM").val();
            iTrack = 5;
        }
        if (vXKFS == 2) {
            sCDNR = $("#TB_CZKHM").val() + "a" + $("#HF_CDNR").val();
            iTrack = 5;
        }
        if (sCDNR == "")
            return;
        var canBeClose = false;
        var myDialog = art.dialog({
            lock: true, content: '正在写卡,请等待......'
            , close: function () {
                if (canBeClose) {
                    return true;
                }
                return false;
            }
        });
        // rwcard.WriteCard("3;1", sCDNR);
        var WriteResult = HttpGetWriteCard("http://localhost:22345", "write", "2;2;1,9600,n,8,1", $("#HF_CDNR").val());

        if (WriteResult.indexOf("error") >= 0) {//rwcard.LastError
            canBeClose = true;
            myDialog.close();
            ShowMessage("写卡失败：" + WriteResult);
        }
        //if (rwcard.LastError != "") {
        //    canBeClose = true;
        //    myDialog.close();
        //    ShowMessage("写卡失败：" + rwcard.LastError);
        //}
        else {
            posttosever(SaveData(), vUrl, "Insert");
            canBeClose = true;
            myDialog.close();
            //$.ajax({
            //    type: "post",
            //    url: vUrl + "?mode=Insert&func=" + vPageMsgID,
            //    async: false,
            //    data: { json: JSON.stringify(SaveData()), titles: 'cecece' },
            //    success: function (data) {
            //        canBeClose = true;
            //        myDialog.close();
            //        if (data.indexOf("错误") >= 0 || data.indexOf("error") >= 0) {
            //            art.dialog({ lock: true, content: data });
            //            canBeClose = false;
            //        }
            //        else {
            //            art.dialog({ lock: true, content: "操作成功(2秒后自动关闭)", time: 2 });
            //            canBeClose = true;
            //        }
            //    },
            //    error: function (data) {
            //        canBeClose = false;
            //        myDialog.close();
            //        art.dialog({ lock: true, content: "保存失败：" + data });
            //    }
            //});

            vProcStatus = cPS_ADD;
        }
        return canBeClose;

    });
});
function SetControlState() {
    $("#B_Insert").hide();
    $("#B_Exec").hide();
    $("#B_Update").hide();
    $("#B_Delete").hide();
    $("#B_Save").hide();
    $("#B_Cancel").hide();
    $("#status-bar").hide();
    ;
}
function IsValidData() {
    if ($.trim($("#TB_CZKHM").val()) == "" && $("#LB_HYKNAME").text() != "") {
        art.dialog({ content: '请输入正确卡号！', time: 1, lock: true });
        return false;
    }
    return true;
}

function onClick(e, treeId, treeNode) {
    $("#TB_BGDDMC").val(treeNode.name);
    $("#HF_BGDDDM").val(treeNode.id);
    hideMenu("menuContent");
}

function ProcIt() {
    if (hyk == "1"&&czk=="0" )//会员卡
    {
        if (!IsValidData()) {
            ClearData();
            return;
        }
        if ($("#TB_CZKHM").val() == "")
            return;
        var data = GetHYXXData("0", $("#TB_CZKHM").val());
        if (data) {
            ShowData(data);
        }
    }
    else if (hyk == "0" && czk == "0")//库存卡
    {
        if (!IsValidData()) {
            ClearData();
            return;
        }
        var data = GetKCKXXData($("#TB_CZKHM").val(), "");
        if (data != null && data != "" && data != "null") {
            ShowData(data);
        }
        else {
            ShowMessage("不存在此库存卡信息");
        }
    }
    else if (czk == "1" && hyk=="1") {

        if (!IsValidData()) {
            ClearData();
            return;
        }
        data = GetMZKXXData("0", $("#TB_CZKHM").val());
        if (data != null && data != "" && data != "null") {
            ShowData(data);
        }
        else {
            ShowMessage("不存在此卡信息");
        }
                //break;
            //case 0:
            //    data = GetMZKKCKXXData($("#TB_CZKHM").val(), "");
            //    break;
        }
    else if (czk == "1" && hyk == "0") {

        if (!IsValidData()) {
            ClearData();
            return;
        }
      data = GetMZKKCKXXData($("#TB_CZKHM").val(), "");
        if (data != null && data != "" && data != "null") {
            ShowData(data);
        }
        else {
            ShowMessage("不存在此卡信息");
        }
    
    }
  

}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sHYKNO = $("#TB_CZKHM").val();
    Obj.iHYID = $("#HF_HYID").val();
    if (Obj.iHYID == "")
        Obj.iHYID = "0";
    Obj.iCLLX = "2";
    Obj.sBGDDDM = $("#HF_BGDDDM").val();
    Obj.sLoginRYMC = sDJRMC;
    Obj.iLoginRYID = iDJR;
    return Obj;
}
function ClearData() {
    $("#MainPanel").find("input").val("");
    $("#MainPanel").find("text").text("");

}
function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#LB_HYKNAME").text(Obj.sHYKNAME);
    $("#HF_CDNR").val(Obj.sXKCDNR);
    $("#LB_YCJE").text(Obj.fQCYE);
    if (hyk == "1") {
        $("#HF_HYID").val(Obj.iHYID);
        switch (Obj.iSTATUS) {
            case -8:
                $("#LB_STATUS").text("无效卡(应回收卡)");
                break;
            case -7:
                $("#LB_STATUS").text("无效卡(应降级卡)");
                break;
            case -6:
                $("#LB_STATUS").text("无效卡(终止卡)");
                break;
            case -5:
                $("#LB_STATUS").text("无效卡(纸券已经消费)");
                break;
            case -4:
                $("#LB_STATUS").text("无效卡(停用卡)");
                break;
            case -3:
                $("#LB_STATUS").text("无效卡(退卡)");
                break;
            case -2:
                $("#LB_STATUS").text("无效卡(作废卡)");
                break;
            case -1:
                $("#LB_STATUS").text("无效卡(作废卡)");
                break;
            case 0:
                $("#LB_STATUS").text("有效卡(发售卡)");
                break;
            case 1:
                $("#LB_STATUS").text("有效卡(已消费卡)");
                break;
            case 2:
                $("#LB_STATUS").text("有效卡(升级卡)");
                break;
            case 3:
                $("#LB_STATUS").text("有效卡(睡眠卡)");
                break;
            case 4:
                $("#LB_STATUS").text("有效卡(异常卡");
                break;
        }
    }
    else {
        $("#HF_HYID").val(0);
        switch (Obj.iSTATUS) {

            case 0:
                $("#LB_STATUS").text("建卡");
                break;
            case 1:
                $("#LB_STATUS").text("领用");
                break;
            case 2:
                $("#LB_STATUS").text("发售");
                break;

        }
    }

}

function AddCustomerButton() {
    AddToolButtons("补磁", "B_HYKBC");
}
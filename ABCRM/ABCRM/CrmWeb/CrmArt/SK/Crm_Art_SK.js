var vCaption = "刷卡";
var vDialogName = "DialogSK";
var timeCDNR = setInterval(function () { $("#TB_CDNR").focus(); }, 1000);
var condData = $.dialog.data("DialogCondition");
var vBJ_KCK = 0;
var vBJ_MZK = 0;
if (condData) {
    condData = JSON.parse(condData);
    vBJ_KCK = condData.iBJ_KCK;
    vBJ_MZK = condData.iBJ_MZK;
}
var DBCaption = vBJ_MZK == "1" ? "CRMDBMZK" : "CRMDB";
var vReturnData;
var hykno;

$(document).ready(function () {
    AddToolButtons("保存", "B_Save");
    AddToolButtons("取消", "B_Cancel");
    document.getElementById("B_Save").onclick = ArtSaveClick;
    document.getElementById("B_Cancel").onclick = ArtCancelClick;
    $("#bftitle").html(vCaption);

    $("#TB_CDNR").bind('keypress', function (event) {
        if (event.keyCode == "13") {
            vReturnData = null;
            if ($("#CB_DZK").prop("checked") == true) {
                ProcElectronicCard()
            }
            else {
                ProcIt();
            }
        }
    });
});

function ArtSaveClick() {
    //T--确认事件
    var Rows = new Array();
    if (vReturnData)
        Rows.push(vReturnData);
    $.dialog.data(vDialogName, JSON.stringify(Rows));
    $.dialog.data('dialogSelected', Rows.length > 0);
    $.dialog.close();

};

function ArtCancelClick() {
    $.dialog.data('dialogSelected', false);
    $.dialog.close();
};

function ProcIt() {
    // ;二磁道?+三磁道?    二磁道是磁道内容。三磁道是卡号 无锡华地
    // 主流方式是;磁道?，不用按照华地的来
    var a = $("#TB_CDNR").val();
    var inx1 = $("#TB_CDNR").val().replace(/\s/g, "").indexOf(";");
    var inx2 = $("#TB_CDNR").val().replace(/\s/g, "").indexOf("+");
    var inx3 = $("#TB_CDNR").val().replace(/\s/g, "").indexOf("?");
    var inx4 = $("#TB_CDNR").val().replace(/\s/g, "").indexOf("?", inx2);
    var cdnr = $("#TB_CDNR").val().replace(/\s/g, "");
    cdnr = cdnr.substring(inx1 + 1, inx3 >= 0 ? inx3 : cdnr.length);
    var hykno = $("#TB_CDNR").val().substring(inx2 + 1, inx4);
    if (cdnr == "") {
        ShowMessage("磁道内容不合法！", 3, false, undefined, function () { document.getElementById("TB_CDNR").focus(); });
        $("#TB_CDNR").val("");
        return false;
    }


    if (vBJ_KCK == 0)
        GetHYXX(cdnr);
    if (vBJ_KCK == 1)
        GetKCKXX("", cdnr);
    if (vBJ_MZK == 1)
        GetMZKXX(0, "", cdnr);

}

function GetMZKXX(hyid, hykno, cdnr) {
    var str = GetMZKXXData(0, "", cdnr, "CRMDBMZK");
    if (str == "null" || str == "") {
        ShowMessage("没有找到卡", 3, false, undefined, function () { document.getElementById("TB_CDNR").focus(); });
        $("#TB_CDNR").val("");
        return;
    }
    vReturnData = JSON.parse(str);
    if (vReturnData.sHYK_NO == "") {
        ShowMessage("没有找到卡", 3, false, undefined, function () { document.getElementById("TB_CDNR").focus(); });
        $("#TB_CDNR").val("");
        return;
    }
    window.clearInterval(timeCDNR);
    ArtSaveClick();
}

function GetHYXX(cdnr, sDBConnName) {
    var str;
    if ($("#CB_DZK").prop("checked") == true) {
        str = GetHYXXData(0, hykno, DBCaption, "");
    }
    else {
        str = GetHYXXData(0, "", DBCaption, cdnr);
    }
    if (str == "null" || str == "") {
        ShowMessage("没有找到卡", 3, false, undefined, function () { document.getElementById("TB_CDNR").focus(); });
        $("#TB_CDNR").val("");
        return;
    }

    window.clearInterval(timeCDNR);
    vReturnData = JSON.parse(str);
    if (vReturnData.sHYK_NO == "") {
        ShowMessage("没有找到卡", 3, false, undefined, function () { document.getElementById("TB_CDNR").focus(); });
        $("#TB_CDNR").val("");
        return;
    }
    ArtSaveClick();

}

function GetKCKXX(hykno, cdnr) {
    var str = GetKCKXXData(hykno, cdnr,0, DBCaption);
    if (str == "null" || str == "" || str == undefined) {
        ShowMessage("没有找到库存卡", 3, false, undefined, function () { document.getElementById("TB_CDNR").focus(); });
        $("#TB_CDNR").val("");
        return;
    }
    window.clearInterval(timeCDNR);
    vReturnData = JSON.parse(str);
    vReturnData.sHYK_NO = vReturnData.sCZKHM;
    vReturnData.iHYID = 0;
    if (vReturnData.sHYK_NO == "") {
        ShowMessage("没有找到卡", 3, false, undefined, function () { document.getElementById("TB_CDNR").focus(); });
        $("#TB_CDNR").val("");
        return;
    }
    ArtSaveClick();
}


function ProcElectronicCard() {
    hykno = $("#TB_CDNR").val().substr(0, 9);
    if (hykno == "") {
        ShowMessage("电子卡不存在", 3, false, undefined, function () { document.getElementById("TB_CDNR").focus(); });
        $("#TB_CDNR").val("");
        return false;
    }
    GetHYXX(hykno);
}
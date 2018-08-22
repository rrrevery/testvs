vUrl = "../HYKGL.ashx";
var sCZKHM = "";
var sCDNR = "";
var timeCDNR = setInterval(function () { $("#TB_CDNR").focus(); }, 1000);
vCaption = "库存卡信息验证";

function InitGrid() {
    vColumnNames = ["卡号", "卡类型", '保管地点', '面值金额', '铺底金额', '状态', '有效期', '验卡通过标记'];
    vColumnModel = [
          { name: "sCZKHM", width: 120, },
          { name: "sHYKNAME", width: 80, },
          { name: "sBGDDMC", width: 80, },
          { name: "fQCYE", width: 80, },
          { name: "fPDJE", width: 80, },
          {
              name: "iSTATUS", width: 80, formatter: function (evl, co) {
                  switch (evl) {
                      case 0:
                          return "建卡";
                          break;
                      case 1:
                          return "领用";
                          break;
                      case 2:
                          return "回收";
                          break;
                  }
              }
          },
          { name: "dYXQ", width: 120, },
          { name: "iBJ_YK", width: 80, hidden: true }
    ];
}


$(document).ready(function () {
    SetControlState();
    $("#TB_CDNR").bind('keypress', function (event) {
        if (event.keyCode == "13") {
            //art.dialog({ lock: true, content: "抱歉！刷卡功能暂未开放，请输入卡号" });
            //return false;
            // ;二磁道?+三磁道?    二磁道是磁道内容。三磁道是卡号 无锡华地
            //var inx1 = $("#TB_CDNR").val().indexOf(";");
            //var inx2 = $("#TB_CDNR").val().indexOf("+");
            //var inx3 = $("#TB_CDNR").val().indexOf("?");
            //var inx4 = $("#TB_CDNR").val().indexOf("?", inx2);
            //var cdnr = $("#TB_CDNR").val().substring(inx1 + 1, inx3);
            //sCDNR = cdnr;
            //var hykno = $("#TB_CDNR").val().substring(inx2 + 1, inx4);
            //sCZKHM = hykno;
            var inx1 = $("#TB_CDNR").val().indexOf(";");
            var inx2 = $("#TB_CDNR").val().indexOf("+");
            var inx3 = $("#TB_CDNR").val().indexOf("?");
            var inx4 = $("#TB_CDNR").val().indexOf("?", inx2);
            var cdnr = $("#TB_CDNR").val();
            cdnr = cdnr.substring(inx1 + 1, inx3 >= 0 ? inx3 : cdnr.length);
            var hykno = $("#TB_CDNR").val().substring(inx2 + 1, inx4);
            if (cdnr == "") {
                art.dialog({ content: '磁道内容格式不正确(2秒后自动关闭)', time: 2, lock: true });
                $("#TB_CDNR").val("");
                $("#LB_FAILNUM").text(parseInt($("#LB_FAILNUM").text()) + 1);
                $("#TB_CDNR").focus();
                return;
            }
            if (vOLDDB == "0") {
                GetKCKXXData("", cdnr, "CRMDB");
            }
            else if (vOLDDB == "1") {
                GetKCKXXData("", cdnr, "CRMDBOLD");
            }
            $("#TB_CDNR").focus();
        }
    });
    $("#btnICK").click(function () {
        sCDNR = rwcard.ReadCard("4;4");
        if (sCDNR != "") {
            $("#TB_CDNR").val(";" + sCDNR + "?");
            ProcIt();
        }
    });
    RefreshButtonSep();
});

var search = "";
function GetKCKXXData(sCZKHM, sCDNR, sDBConn) {
    if (sDBConn == undefined)
        sDBConn = "CRMDB";
    sjson = { sCDNR: sCDNR, sDBConnName: sDBConn };
    result = "";
   // search = art.dialog({ content: '请稍候....', lock: true });
    var kxxXHR = $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=GetKCKXX",
        dataType: "json",
        async: false,
        data: { json: JSON.stringify(sjson), titles: 'cecece' },
    });
    kxxXHR.then(kckYK);
}
function AddCustomerButton() {
    AddToolButtons("IC卡", "btnICK");
}
function kckYK(responseText, state, jqxhr) {
    $("#TB_CDNR").val("");
   // search.close();
    if (state == "success") {
        if (responseText == null || responseText == "" || responseText == "null") {
            art.dialog({ content: '没有找到此卡数据(2秒后自动关闭)', lock: true, time: 2 });
            $("#LB_FAILNUM").text(parseInt($("#LB_FAILNUM").text()) + 1);
            $("#TB_CDNR").focus();
            return;
        }
        if ((typeof responseText == "string") && (responseText.indexOf("错误") != -1 || responseText.indexOf("error") != -1)) {
            art.dialog({ content: responseText, lock: true });
            $("#LB_FAILNUM").text(parseInt($("#LB_FAILNUM").text()) + 1);
            $("#TB_CDNR").focus();
            return;
        }
        if (typeof (responseText) != "object") {
            responseText = JSON.parse(responseText);
        }

        if (typeof responseText == "object" && responseText.sCZKHM != sCZKHM) {
            $("#LB_FAILNUM").text(parseInt($("#LB_FAILNUM").text()) + 1);
            art.dialog({ content: '磁道内容与卡号不匹配(2秒后自动关闭)', lock: true, time: 2 });
            $("#TB_CDNR").focus();
            return;
        }
        var rowdata = new Object();
        rowdata.sCZKHM = responseText.sCZKHM;
        rowdata.sHYKNAME = responseText.sHYKNAME;
        rowdata.sBGDDMC = responseText.sBGDDMC;
        rowdata.fQCYE = responseText.fQCYE;
        rowdata.fPDJE = responseText.fPDJE;
        rowdata.iSTATUS = responseText.iSTATUS;
        rowdata.dYXQ = responseText.dYXQ;
        rowdata.iBJ_YK = responseText.iBJ_YK;
        rowdata.dXKRQ = "";

        $("#list").datagrid("appendRow", rowdata);

        var kck = new Object();
        kck.sHYKNO = sCZKHM;
        kck.sCDNR = sCDNR;
        kck.iCLLX = 1;
        kck.iZXR = iDJR;
        kck.sZXRMC = sDJRMC;
        kck.iLoginRYID = iDJR;
        if (vOLDDB == "0") {
            kck.sDBConnName = "CRMDB";
        }
        else if (vOLDDB == "1") {
            kck.sDBConnName = "CRMDBOLD";
        }
        //验卡
        $.ajax({
            type: 'post',
            url: vUrl + "?mode=Insert&func=" + vPageMsgID,
            //dataType: "json",
            async: true,
            data: { json: JSON.stringify(kck), titles: 'cecece' },

        }).done(function (responseText, state, jqxhr) {
            //计数
            if (responseText == "yes" && state == "success") {
                $("#LB_SUCNUM").text(parseInt($("#LB_SUCNUM").text()) + 1);
            }
            else {
                art.dialog({ content: '卡信息验证通过，但处理记录插入数据库失败', lock: true });
                $("#TB_CDNR").focus();
            }
        });
    }
    else {
        art.dialog({ content: responseText, lock: true });
        $("#TB_CDNR").focus();
    }
}

function ProcIt() {
    var inx1 = $("#TB_CDNR").val().indexOf(";");
    var inx2 = $("#TB_CDNR").val().indexOf("+");
    var inx3 = $("#TB_CDNR").val().indexOf("?");
    var inx4 = $("#TB_CDNR").val().indexOf("?", inx2);
    var cdnr = $("#TB_CDNR").val().substring(inx1 + 1, inx3);
    sCDNR = cdnr;
    var hykno = $("#TB_CDNR").val().substring(inx2 + 1, inx4);
    sCZKHM = hykno;
    if (inx1 == "-1" || inx2 == "-1" || inx3 == "-1" || inx4 == "-1" || cdnr == "" || hykno == "") {
        art.dialog({ content: '磁道内容格式不正确(2秒后自动关闭)', time: 2, lock: true });
        $("#TB_CDNR").val("");
        $("#LB_FAILNUM").text(parseInt($("#LB_FAILNUM").text()) + 1);
        $("#TB_CDNR").focus();
        return;
    }
    if (vOLDDB == "0") {
        GetKCKXXData(hykno, cdnr, "CRMDB");
    }
    else if (vOLDDB == "1") {
        GetKCKXXData(hykno, cdnr, "CRMDBOLD");
    }
    window.clearInterval(timeCDNR);
    $("#TB_CDNR").focus();
}

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
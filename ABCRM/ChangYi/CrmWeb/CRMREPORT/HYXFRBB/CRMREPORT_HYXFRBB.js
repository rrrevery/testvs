var ReportServer = "";

$(document).ready(function () {
    ReportServer = GetReportServer();
    $("#TB_SHMC").click(function () {
        SelectSH("TB_SHMC", "HF_SHDM", "zHF_SHDM", true);
    });
    $("#TB_MDMC").click(function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", false,$("#HF_SHDM").val());
    });

    var Currentdate = new Date();
    Currentdate = new Date(Currentdate.getFullYear(), Currentdate.getMonth(), Currentdate.getDate() - 1);
    var PastTime = new Date(Currentdate.getFullYear()-1, Currentdate.getMonth(), Currentdate.getDate());
    $("#RQ1").val(getDate(Currentdate));
    $("#RQ2").val(getDate(Currentdate));
    $("#TQ1").val(getDate(PastTime));
    $("#TQ2").val(getDate(PastTime));    
});

function btnSrch1Click() {
    var addr = ReportServer + "?reportlet=R_HYMDXFFX.cpt";

    var rq1 = new Date($("#RQ1").val());
    var rq2 = new Date($("#RQ2").val());
    var tq1 = new Date($("#TQ1").val());
    var tq2 = new Date($("#TQ2").val());
    var shdm = $("#HF_SHDM").val();
    var mdid = $("#HF_MDID").val();
    //if (shdm == "" && mdid == "") {
    //    art.dialog({ lock: true, content: "请选择商户或者门店" });
    //    return;
    //}

    var srq1 = GetDateString(rq1, "0");
    var srq2 = GetDateString(rq2, "0");
    var stq1 = GetDateString(tq1, "0");
    var stq2 = GetDateString(tq2, "0");

    if ($("#HF_SHDM").val() != "") {
        addr += "&SHDM=" + $("#HF_SHDM").val();
    }
    if ($("#HF_MDID").val() != "") {
        addr += "&MDID=" + $("#HF_MDID").val();
    }

    if ($("#RQ1").val() != "" && $("#RQ2").val() != "") {
        addr += "&RQ1=" + srq1 + "&RQ2=" + srq2;
    }
    if ($("#TQ1").val() != "" && $("#TQ2").val() != "") {
        addr += "&TQ1=" + stq1 + "&TQ2=" + stq2;
    }
    addr += "&menu_id=" + vPageMsgID + "&PERSON_ID=" + iDJR;
    $("#fr1").attr("src", addr);
};

//getMonth()  返回0~11
function getDate(date) {
    var currentDate = date.getFullYear() + "-";
    if (date.getMonth() < 9) {
        currentDate += "0" + (date.getMonth() + 1) + "-";
    }
    else {
        currentDate += date.getMonth() + 1 + "-";
    }
    if (date.getDate() < 10) {
        currentDate += "0" + date.getDate();
    }
    else {
        currentDate += date.getDate();
    }
    return currentDate;
}
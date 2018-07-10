var ReportServer = "";

$(document).ready(function () {
    ReportServer = GetReportServer();
    $("#TB_SHMC").click(function () {
        SelectSH("TB_SHMC", "HF_SHDM", "zHF_SHDM", true);
    });
    $("#TB_MDMC").click(function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", false, $("#HF_SHDM").val(), "", "", "",true);
    });
    $("#RQ1").focus(function () {
        switch ($("#cbRQLX").val()) {
            case "1":
                WdatePicker({ isShowWeek: true, dateFmt: 'yyyyMM' });
                break;
            default:
                WdatePicker({ isShowWeek: true, });
                break;
        }
    });
    $("#RQ2").focus(function () {
        switch ($("#cbRQLX").val()) {
            case "1":
                WdatePicker({ isShowWeek: true, dateFmt: 'yyyyMM' });
                break;
            default:
                WdatePicker({ isShowWeek: true, });
                break;
        }
    });
    var Currentdate = new Date();
    Currentdate = new Date(Currentdate.getFullYear(), Currentdate.getMonth(), Currentdate.getDate() - 1);
    var PastTime = new Date(Currentdate.getFullYear() - 1, Currentdate.getMonth(), Currentdate.getDate());
    $("#RQ1").val(getDate(Currentdate));
    $("#RQ2").val(getDate(Currentdate));
});

function btnSrch1Click() {
    var addr = ReportServer + "?reportlet=HYXFSDXG_MD.cpt";
    var rqlx = $("#cbRQLX").val();
    if ($("#RQ1").val() != "" && $("#RQ2").val() != "") {
        if (rqlx != "1") {
            var rq1 = new Date($("#RQ1").val());
            var rq2 = new Date($("#RQ2").val());
            var tq1 = new Date(rq1.getFullYear() - 1, rq1.getMonth(), rq1.getDate());
            var tq2 = new Date(rq2.getFullYear() - 1, rq2.getMonth(), rq2.getDate());
            var srq1 = GetDateString(rq1, rqlx);
            var srq2 = GetDateString(rq2, rqlx);
            var stq1 = GetDateString(tq1, rqlx);
            var stq2 = GetDateString(tq2, rqlx);
            addr += "&RQ1=" + srq1 + "&RQ2=" + srq2;
            addr += "&TQ1=" + stq1 + "&TQ2=" + stq2;
        }
        else {
            var rq1 = $("#RQ1").val();
            var rq2 = $("#RQ2").val();
            var tq1 = GetYF1();
            var tq2 = GetYF2();
            addr += "&RQ1=" + rq1 + "&RQ2=" + rq2;
            addr += "&TQ1=" + tq1 + "&TQ2=" + tq2;
        }
    }
    else
        ShowMessage("请选择日期");
  
    var shdm = $("#HF_SHDM").val();
    var mdid = $("#HF_MDID").val();
    if (shdm == "") {
        ShowMessage("请选择商户");
        return;
    }
    if (mdid == "") {
        ShowMessage("请选择门店");
        return;
    }


    if ($("#HF_SHDM").val() != "") {
        addr += "&SHDM=" + $("#HF_SHDM").val();
    }
    if ($("#HF_MDID").val() != "") {
        addr += "&MDID=" + $("#HF_MDID").val();
    }
    addr += "&LX=" + rqlx;
   

    addr += "&menu_id=" + vPageMsgID + "&PERSON_ID=" + iDJR;
    $("#fr1").attr("src", addr);
};

//getMonth()  返回0~11
function getDate(date) {
    if ($("#cbRQLX").val() != "1") {
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
    }
    else {
        var currentDate = date.getFullYear();
        if (date.getMonth() < 9) {
            currentDate += "0" + (date.getMonth() + 1);
        }
        else {
            currentDate += date.getMonth() + 1;
        }
    }
    return currentDate;
}
function cbRQLXChange() {
    $("#RQ1").val("");
    $("#RQ2").val("");
    var Currentdate = new Date();
    Currentdate = new Date(Currentdate.getFullYear(), Currentdate.getMonth(), Currentdate.getDate() - 1);
    var PastTime = new Date(Currentdate.getFullYear() - 1, Currentdate.getMonth(), Currentdate.getDate());
    $("#RQ1").val(getDate(Currentdate));
    $("#RQ2").val(getDate(Currentdate));
}

function GetYF1() {  
    var dateString = $("#RQ1").val();
    var pattern = /(\d{4})(\d{2})/;
    var formatedDate = new Date(dateString.replace(pattern, '$1-$2-01'));
    formatedDate.setMonth(formatedDate.getMonth() - 12);
    rq1 = FormatDate(formatedDate, "yyyyMM");
    return rq1;
}
function GetYF2() {  
    var dateString = $("#RQ2").val();
    var pattern = /(\d{4})(\d{2})/;
    var formatedDate = new Date(dateString.replace(pattern, '$1-$2-01'));
    formatedDate.setMonth(formatedDate.getMonth() - 12);
    rq2 = FormatDate(formatedDate, "yyyyMM");
    return rq2;
}


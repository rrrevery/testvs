﻿var ReportServer = "";

$(document).ready(function () {
    ReportServer = GetReportServer();
    FillMD($("#cbMD"));
    //FillMD($("#cbMD2"));
});

function btnSrch1Click() {
    var addr = ReportServer + "?reportlet=RFM_WDXFX.cpt";

    var r = $("#cbR").val();
    var f = $("#cbF").val();
    var m = $("#cbM").val();
    var mdid = $("#cbMD").val();
    if (mdid == "") {
        ShowMessage("请选择门店");
        return;
    }

    addr += "&MDID=" + mdid;
    if (r != "")
        addr += "&R=" + r;
    if (f != "")
        addr += "&F=" + f;
    if (m != "")
        addr += "&M=" + m;
    addr += "&menu_id=" + vPageMsgID + "&PERSON_ID=" + iDJR;
    $("#fr1").attr("src", addr);
};

function btnSrch2Click() {
    var addr = ReportServer + "?reportlet=RFM_PLPLFX.cpt";
    var rq1 = new Date($("#edPLRQ1").val());
    var rq2 = new Date($("#edPLRQ2").val());
    var tq1 = new Date(rq1.getFullYear() - 1, rq1.getMonth(), rq1.getDate());
    var tq2 = new Date(rq2.getFullYear() - 1, rq2.getMonth(), rq2.getDate());
    var rqlx = $("#cbRQLX2").val();
    var mdid = $("#cbMD2").val();
    if (mdid == "") {
        ShowMessage("请选择门店");
        return;
    }
    var bmjc = $("#cbBMJC2").val();
    var srq1 = GetDateString(rq1, rqlx);
    var srq2 = GetDateString(rq2, rqlx);
    var stq1 = GetDateString(tq1, rqlx);
    var stq2 = GetDateString(tq2, rqlx);

    addr += "&LX=" + rqlx + "&MDID=" + mdid + "&BMJC=" + bmjc;
    addr += "&RQ1=" + srq1 + "&RQ2=" + srq2;
    addr += "&TQ1=" + stq1 + "&TQ2=" + stq2;
    addr += "&menu_id=" + vPageMsgID + "&PERSON_ID=" + iDJR;
    $("#fr2").attr("src", addr);
};

//function GetDateString(date, rqlx) {
//    var result = FormatDate(date, "yyyyMMdd");
//    switch (rqlx) {
//        case "1":
//            result = FormatDate(date, "yyyyMM");
//            break;
//        case "2":
//            result = DateToSeason(date);
//            break;
//    }
//    return result;
//}

//function DateToSeason(date) {
//    var month = date.getMonth();
//    var jd = Math.floor(month / 3) + 1;
//    return date.getFullYear().toString() + jd;
//}
var ReportServer = "";

$(document).ready(function () {
    ReportServer = GetReportServer();
    FillMD($("#cbMD"));
    //FillMD($("#cbMD2"));
});

function btnSrch1Click() {
    var addr = ReportServer + "?reportlet=RPM_PH.cpt";
    var rq1 = new Date($("#edRQ1").val());
    var rq2 = new Date($("#edRQ2").val());
    var tq1 = new Date(rq1.getFullYear() - 1, rq1.getMonth(), rq1.getDate());
    var tq2 = new Date(rq2.getFullYear() - 1, rq2.getMonth(), rq2.getDate());
    var ljrq1 = new Date(rq1.getFullYear(), 0, 1);
    var ljtq1 = new Date(tq1.getFullYear(), 0, 1);
    var rqlx = $("#cbRQLX").val();
    var mdid = $("#cbMD").val();
    if (mdid == "") {
        ShowMessage("请选择门店");
        return;
    }
    var wdlx = $("#cbWDLX").val();
    if (wdlx == "") {
        ShowMessage("请选择维度");
        return;
    }
    var srq1 = GetDateString(rq1, rqlx);
    var srq2 = GetDateString(rq2, rqlx);
    var stq1 = GetDateString(tq1, rqlx);
    var stq2 = GetDateString(tq2, rqlx);
    var sljrq1 = GetDateString(ljrq1, rqlx);
    var sljtq1 = GetDateString(ljtq1, rqlx);
    //&WDLX=1
    addr += "&LX=" + rqlx + "&MDID=" + mdid + "&WDLX=" + wdlx;
    addr += "&JD1=" + srq1 + "&JD2=" + srq2;
    //addr += "&LJRQ1=" + sljrq1 + "&LJTQ1=" + sljtq1;
    addr += "&menu_id=" + vPageMsgID + "&PERSON_ID=" + iDJR;
    $("#fr1").attr("src", addr);
};

//function btnSrch2Click() {
//    var addr = ReportServer + "?reportlet=RFM_PLPLFX.cpt";
//    var rq1 = new Date($("#edPLRQ1").val());
//    var rq2 = new Date($("#edPLRQ2").val());
//    var tq1 = new Date(rq1.getFullYear() - 1, rq1.getMonth(), rq1.getDate());
//    var tq2 = new Date(rq2.getFullYear() - 1, rq2.getMonth(), rq2.getDate());
//    var rqlx = $("#cbRQLX2").val();
//    var mdid = $("#cbMD2").val();
//    if (mdid == "") {
//        ShowMessage("请选择门店");
//        return;
//    }
//    var bmjc = $("#cbBMJC2").val();
//    var srq1 = GetDateString(rq1, rqlx);
//    var srq2 = GetDateString(rq2, rqlx);
//    var stq1 = GetDateString(tq1, rqlx);
//    var stq2 = GetDateString(tq2, rqlx);

//    addr += "&LX=" + rqlx + "&MDID=" + mdid + "&BMJC=" + bmjc;
//    addr += "&RQ1=" + srq1 + "&RQ2=" + srq2;
//    addr += "&TQ1=" + stq1 + "&TQ2=" + stq2;
//    addr += "&menu_id=" + vPageMsgID + "&PERSON_ID=" + iDJR;
//    $("#fr2").attr("src", addr);
//};

var ReportServer = "";

$(document).ready(function () {
    ReportServer = GetReportServer();
    FillMD($("#cbMD"));
    //FillMD($("#cbMD3"));
});

function btnSrch1Click() {
    var addr = ReportServer + "?reportlet=RFM_JZFX.cpt";
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
    var srq1 = GetDateString(rq1, rqlx);
    var srq2 = GetDateString(rq2, rqlx);
    var stq1 = GetDateString(tq1, rqlx);
    var stq2 = GetDateString(tq2, rqlx);
    var sljrq1 = GetDateString(ljrq1, rqlx);
    var sljtq1 = GetDateString(ljtq1, rqlx);

    addr += "&LX=" + rqlx + "&MDID=" + mdid;
    addr += "&RQ1=" + srq1 + "&RQ2=" + srq2;
    addr += "&TQ1=" + stq1 + "&TQ2=" + stq2;
    addr += "&LJRQ1=" + sljrq1 + "&LJTQ1=" + sljtq1;
    addr += "&menu_id=" + vPageMsgID + "&PERSON_ID=" + iDJR;
    $("#fr1").attr("src", addr);
};

function btnSrch2Click() {
    var addr = ReportServer + "?reportlet=HYJGFX_NLXB.cpt";
    var rq1 = new Date($("#edBMRQ1").val());
    var rq2 = new Date($("#edBMRQ2").val());
    var tq1 = new Date(rq1.getFullYear() - 1, rq1.getMonth(), rq1.getDate());
    var tq2 = new Date(rq2.getFullYear() - 1, rq2.getMonth(), rq2.getDate());
    var rqlx = $("#cbRQLX2").val();

    var srq1 = GetDateString(rq1, rqlx);
    var srq2 = GetDateString(rq2, rqlx);
    var stq1 = GetDateString(tq1, rqlx);
    var stq2 = GetDateString(tq2, rqlx);

    addr += "&LX=" + rqlx;
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
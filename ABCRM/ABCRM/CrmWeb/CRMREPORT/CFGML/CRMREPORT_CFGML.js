var ReportServer = "";

$(document).ready(function () {
    ReportServer = GetReportServer();
    FillSH($("#cbSH"));
    FillSH($("#cbSH2"));
    FillSH($("#cbSH3"));
    FillMD($("#cbMD"));
    FillMD($("#cbMD3"));
    $("#TB_MDMC").click(function () {
        if($("#cbSH2").val()!="")
        {
            SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", false, $("#cbSH2").val(),"","","",true);
        }
        else
        {
            SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", false,"","","","",true);
        }
    })
    $("#TB_MD").click(function () {
        SelectMD("TB_MD", "HF_MD", "zHF_MD", false, $("#cbSH3").val(), "", "", "", true);
    });
    $("#TB_RQ1").focus(function () {
        BindDateEvent($("#cbRQLX4").val());
        WdatePicker({ onpicked: datechange1($("#TB_RQ1").val()) });
    });
    $("#TB_RQ2").focus(function () {
        BindDateEvent($("#cbRQLX4").val());
        WdatePicker({ onpicked: datechange2($("#TB_RQ2").val()) });
    });
    $("#TB_TQ1").focus(function () {
        BindDateEvent($("#cbRQLX4").val());
    });
    $("#TB_TQ2").focus(function () {
        BindDateEvent($("#cbRQLX4").val());
    });

    //tab1 deal date formate
    $("#edRQ1").focus(function () {
        BindDateEvent($("#cbRQLX").val());
    });
    $("#edRQ2").focus(function () {
        BindDateEvent($("#cbRQLX").val());
    });

    //tab2 deal date formate
    $("#edBMRQ1").focus(function () {
        BindDateEvent($("#cbRQLX2").val());
    });
    $("#edBMRQ2").focus(function () {
        BindDateEvent($("#cbRQLX2").val());
    });


});

function btnSrch1Click() {
    var addr = ReportServer + "?reportlet=CFGML_MD.cpt";
    var rqlx = parseInt($("#cbRQLX").val());
    var srq1;
    var srq2;
    var stq1;
    var stq2;
    var sljrq1;
    var sljtq1
    if (rqlx == 0) {
        var rq1 = new Date($("#edRQ1").val());
        var rq2 = new Date($("#edRQ2").val());
        var tq1 = GetContrastDate($("#edRQ1").val(), rqlx);
        var tq2 = GetContrastDate($("#edRQ2").val(), rqlx);
        var ljrq1 = new Date(rq1.getFullYear(), 0, 1);
        var ljtq1 = new Date(tq1.getFullYear(), 0, 1);

        srq1 = GetDateString(rq1, rqlx);
        srq2 = GetDateString(rq2, rqlx);
        stq1 = GetDateString(tq1, rqlx);
        stq2 = GetDateString(tq2, rqlx);
        sljrq1 = GetDateString(ljrq1, rqlx);
        sljtq1 = GetDateString(ljtq1, rqlx);
    }
    else {
        srq1 = $("#edRQ1").val();
        srq2 = $("#edRQ2").val();
        stq1 = GetContrastDate($("#edRQ1").val(), rqlx);
        stq2 = GetContrastDate($("#edRQ2").val(), rqlx);
        var ljrq1 = new Date(parseInt(srq1.substr(0,4)), 0, 1);
        var ljtq1 = new Date(parseInt(stq1.toString().substr(0, 4)), 0, 1);

        sljrq1 = GetDateString(ljrq1, "" + rqlx + "");
        sljtq1 = GetDateString(ljtq1, "" + rqlx + "");
    }

    if ($("#cbSH2").val() != "")
    {
        addr += "&SHDM=" + $("#cbSH2").val();
    }
    if ($("#HF_MDID").val() != "")
    {
        addr += "&MDID=" + $("#HF_MDID").val();
    }
    else {
        art.dialog({ lock: true, content: "请选择门店", time: true });
        return;
    }
    addr += "&LX=" + rqlx;
    addr += "&RQ1=" + srq1 + "&RQ2=" + srq2;
    addr += "&TQ1=" + stq1 + "&TQ2=" + stq2;
    addr += "&LJRQ1=" + sljrq1 + "&LJTQ1=" + sljtq1;
    addr += "&menu_id=" + vPageMsgID + "&PERSON_ID=" + iDJR;
    $("#fr1").attr("src", addr);
};

function btnSrch2Click() {
    var addr = ReportServer + "?reportlet=CFGML_BM.cpt";
    var rqlx = parseInt($("#cbRQLX2").val());
    var srq1;
    var srq2;
    var stq1;
    var stq2;
    var sljrq1;
    var sljtq1
    if (rqlx == 0) {
        var rq1 = new Date($("#edBMRQ1").val());
        var rq2 = new Date($("#edBMRQ2").val());
        var tq1 = GetContrastDate($("#edBMRQ1").val(), rqlx);
        var tq2 = GetContrastDate($("#edBMRQ2").val(), rqlx);
        var ljrq1 = new Date(rq1.getFullYear(), 0, 1);
        var ljtq1 = new Date(tq1.getFullYear(), 0, 1);

        srq1 = GetDateString(rq1, rqlx);
        srq2 = GetDateString(rq2, rqlx);
        stq1 = GetDateString(tq1, rqlx);
        stq2 = GetDateString(tq2, rqlx);
        sljrq1 = GetDateString(ljrq1, rqlx);
        sljtq1 = GetDateString(ljtq1, rqlx);
    }
    else {
        srq1 = $("#edBMRQ1").val();
        srq2 = $("#edBMRQ2").val();
        stq1 = GetContrastDate($("#edBMRQ1").val(), rqlx);
        stq2 = GetContrastDate($("#edBMRQ2").val(), rqlx);
        var ljrq1 = new Date(parseInt(srq1.substr(0, 4)), 0, 1);
        var ljtq1 = new Date(parseInt(stq1.toString().substr(0, 4)), 0, 1);
        sljrq1 = GetDateString(ljrq1, "" + rqlx + "");
        sljtq1 = GetDateString(ljtq1, "" + rqlx + "");
    }

    var bmjc = IsNullValue($("#cbBMJC").val(), "");
    var mdid = $("#cbMD").val();
    if (bmjc == "") {
        ShowMessage("请选择商户和部门级次");
        return;
    }
    if (mdid == "") {
        ShowMessage("请选择门店");
        return;
    }
    addr += "&LX=" + rqlx + "&BMJC=" + bmjc + "&MDID=" + mdid;
    addr += "&RQ1=" + srq1 + "&RQ2=" + srq2;
    addr += "&TQ1=" + stq1 + "&TQ2=" + stq2;
    addr += "&LJRQ1=" + sljrq1 + "&LJTQ1=" + sljtq1;
    addr += "&menu_id=" + vPageMsgID + "&PERSON_ID=" + iDJR;
    $("#fr2").attr("src", addr);
};

function btnSrch3Click() {
    var addr = ReportServer + "?reportlet=CFGML_PL.cpt";
    var rq1 = new Date($("#edPLRQ1").val());
    var rq2 = new Date($("#edPLRQ2").val());
    var tq1 = new Date(rq1.getFullYear() - 1, rq1.getMonth(), rq1.getDate());
    var tq2 = new Date(rq2.getFullYear() - 1, rq2.getMonth(), rq2.getDate());
    var ljrq1 = new Date(rq1.getFullYear(), 0, 1);
    var ljtq1 = new Date(tq1.getFullYear(), 0, 1);
    var rqlx = $("#cbRQLX3").val();
    var bmjc = $("#cbBMJC").val();
    var mdid = $("#cbMD3").val();
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

    addr += "&LX=" + rqlx + "&BMJC=" + bmjc + "&MDID=" + mdid;
    addr += "&RQ1=" + srq1 + "&RQ2=" + srq2;
    addr += "&TQ1=" + stq1 + "&TQ2=" + stq2;
    addr += "&LJRQ1=" + sljrq1 + "&LJTQ1=" + sljtq1;
    addr += "&menu_id=" + vPageMsgID + "&PERSON_ID=" + iDJR;
    $("#fr3").attr("src", addr);
};
function btnSrch4Click() {
    var addr = ReportServer + "?reportlet=CFGML_DJ.cpt";
    var rqlx = $("#cbRQLX4").val();
    var ljrq1 = new Date(parseInt($("#TB_RQ1").val().substr(0, 4)), 0, 1);
    var ljtq1 = new Date(parseInt($("#TB_TQ1").val().substr(0, 4)), 0, 1);
    sljrq1 = GetDateString(ljrq1, rqlx);
    sljtq1 = GetDateString(ljtq1, rqlx);

    addr += "&LJRQ1=" + sljrq1 + "&LJTQ1=" + sljtq1;
    addr += "&RQ1=" + $("#TB_RQ1").val() + "&RQ2=" + $("#TB_RQ2").val();
    addr += "&TQ1=" + $("#TB_TQ1").val() + "&TQ2=" + $("#TB_TQ2").val();
    if ($("#cbSH3").val() != "") {
        addr += "&SHDM=" + $("#cbSH3").val();
    }
    else {
        ShowMessage("请选择商户");
        return;
    }
    var mdid = $("#HF_MD").val();
    if (mdid == "") {
        ShowMessage("请选择门店");
        return;
    }
    else
        addr += "&MDID=" + mdid;
    addr += "&LX=" + rqlx;
    addr += "&menu_id=" + vPageMsgID + "&PERSON_ID=" + iDJR;
    $("#fr4").attr("src", addr);
};
function SHChange() {
    if ($("#cbSH").val() != "") {
        $("#cbBMJC").html("");
        var sBMJC = $("#cbSH option:selected").attr("bmjc");
        var iBMCD = 0;
        for (var i = 0; i < sBMJC.length ; i++) {
            iBMCD += parseInt(sBMJC[i]);
            $("#cbBMJC").append("<option value='" + iBMCD + "'>" + (i + 1).toString() + "级</option>");;
        }
    }
}
function SH2Change() {
    $("#TB_MDMC").val("");
    $("#HF_MDID").val("");
    $("#zHF_MDID").val("");
}
function SH3Change() {
    $("#TB_MD").val("");
    $("#HF_MD").val("");
    $("#zHF_MD").val("");
}

function datechange1(date) {
    if (date != "") {
        if ($("#cbRQLX4").val() == "1") {
            $("#TB_TQ1").val(parseInt(date) - 100);
        }
        if ($("#cbRQLX4").val() == "2") {
            $("#TB_TQ1").val(parseInt(date) - 10);
        }
        if ($("#cbRQLX4").val() == "0") {
            var rq = new Date($("#TB_RQ1").val());
            var tq = new Date(rq.getFullYear() - 1, rq.getMonth(), rq.getDate());
            var stq = getDate(tq);
            $("#TB_TQ1").val(stq);
        }
    }
}
function datechange2(date) {
    if (date != "") {
        if ($("#cbRQLX4").val() == "1") {
            $("#TB_TQ2").val(parseInt(date) - 100);
        }
        if ($("#cbRQLX4").val() == "2") {
            $("#TB_TQ2").val(parseInt(date) - 10);
        }
        if ($("#cbRQLX4").val() == "0") {
            var rq = new Date($("#TB_RQ2").val());
            var tq = new Date(rq.getFullYear() - 1, rq.getMonth(), rq.getDate());
            var stq = getDate(tq);
            $("#TB_TQ2").val(stq);
        }
    }
}
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
function cbRQLX4Change() {
    $("#TB_RQ1").val("");
    $("#TB_RQ2").val("");
    $("#TB_TQ1").val("");
    $("#TB_TQ2").val("");
}
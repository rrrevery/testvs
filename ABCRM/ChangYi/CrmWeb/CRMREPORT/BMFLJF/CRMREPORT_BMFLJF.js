var ReportServer = "";

$(document).ready(function () {
    ReportServer = GetReportServer();
    FillSH($("#cbSH"));
    FillSH($("#cbSH2"));
    FillMD($("#cbMD"));
    FillMD($("#cbMD2"));
    //FillHYKTYPE($("#cbKLX"),1);
    //FillHYKTYPE($("#cbKLX2"),1);
    $("#TB_HYKNAME").click(function () {
        SelectKLX("TB_HYKNAME", "HF_HYKNAME", "zHF_HYKNAME", false,"",true);
    });
    $("#TB_HYKTYPE").click(function () {
        SelectKLX("TB_HYKTYPE", "HF_HYKTYPE", "zHF_HYKTYPE", false,"",true);
    });
    $("#TB_HYKLX").click(function () {
        SelectKLX("TB_HYKLX", "HF_HYKLX", "zHF_HYKLX", false, "", true);
    });
    $("#TB_SHMC").click(function () {
        SelectSH("TB_SHMC", "HF_SHDM", "zHF_SHDM", true);
    });
    $("#TB_MDMC").click(function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", false, $("#cbSH2").val(), "", "", "", true);
    });
    $("#edBMRQ1").focus(function () {
        switch ($("#cbRQLX2").val()) {       
            case "1":
                WdatePicker({ isShowWeek: true, dateFmt: 'yyyyMM' });
                //WdatePicker({ dateFmt: 'yyyy年MM季度', disabledDates: ['....-0[5-9]-..', '....-1[0-2]-..'], startDate: '%y-01-01' });
                break;
            default:
                WdatePicker({ isShowWeek: true, });
                break;
        }
    });
    $("#edBMRQ2").focus(function () {
        switch ($("#cbRQLX2").val()) {
            case "1":
                WdatePicker({ isShowWeek: true, dateFmt: 'yyyyMM' });
                break;
            default:
                WdatePicker({ isShowWeek: true, });
                break;
        }
    });
    $("#edRQ1").focus(function () {
        switch ($("#cbRQLX").val()) {
            case "1":
                WdatePicker({ isShowWeek: true, dateFmt: 'yyyyMM' });
                break;
            default:
                WdatePicker({ isShowWeek: true, });
                break;
        }
    });
    $("#edRQ2").focus(function () {
        switch ($("#cbRQLX").val()) {
            case "1":
                WdatePicker({ isShowWeek: true, dateFmt: 'yyyyMM' });
                break;
            default:
                WdatePicker({ isShowWeek: true, });
                break;
        }
    });
    $("#TB_RQ1").focus(function () {
        BindDateEvent($("#cbRQLX3").val());
        WdatePicker({ onpicked: datechange1( $("#TB_RQ1").val()) });
    });
    $("#TB_RQ2").focus(function () {
        BindDateEvent($("#cbRQLX3").val());
        WdatePicker({ onpicked: datechange2($("#TB_RQ2").val()) });
    });
    $("#TB_TQ1").focus(function () {
        BindDateEvent($("#cbRQLX3").val());       
    });
    $("#TB_TQ2").focus(function () {
        BindDateEvent($("#cbRQLX3").val());
    });
});

function btnSrch1Click() {
    var addr = ReportServer + "?reportlet=HYJF_BM.cpt";
    var rqlx = $("#cbRQLX").val();
    if (rqlx != "1") {
        var rq1 = new Date($("#edRQ1").val());
        var rq2 = new Date($("#edRQ2").val());
        var tq1 = new Date(rq1.getFullYear() - 1, rq1.getMonth(), rq1.getDate());
        var tq2 = new Date(rq2.getFullYear() - 1, rq2.getMonth(), rq2.getDate());
        var ljrq1 = new Date(rq1.getFullYear(), 0, 1);
        var ljtq1 = new Date(tq1.getFullYear(), 0, 1);
        var srq1 = GetDateString(rq1, rqlx);
        var srq2 = GetDateString(rq2, rqlx);
        var stq1 = GetDateString(tq1, rqlx);
        var stq2 = GetDateString(tq2, rqlx);
        var sljrq1 = GetDateString(ljrq1, rqlx);
        var sljtq1 = GetDateString(ljtq1, rqlx);
        addr += "&RQ1=" + srq1 + "&RQ2=" + srq2;
    }
    else {
        addr += "&RQ1=" + $("#edRQ1").val() + "&RQ2=" + $("#edRQ2").val();
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
    // var hyktype = $("#cbKLX").val();
    var hyktype = $("#HF_HYKNAME").val();
    if (hyktype == "") {
        ShowMessage("请选择卡类型");
        return;
    }


    addr += "&LX=" + rqlx + "&BMJC=" + bmjc + "&MDID=" + mdid + "&HYKTYPE=" + hyktype;
    addr += "&BMDM=" + $("#HF_BMDM").val(); 
    addr += "&menu_id=" + vPageMsgID + "&PERSON_ID=" + iDJR;
    $("#fr1").attr("src", addr);
};

function btnSrch2Click() {
    var addr = ReportServer + "?reportlet=HYJF_FL.cpt";
    var rqlx = $("#cbRQLX2").val();
    if (rqlx != "1") {
        var rq1 = new Date($("#edBMRQ1").val());
        var rq2 = new Date($("#edBMRQ2").val());
        var tq1 = new Date(rq1.getFullYear() - 1, rq1.getMonth(), rq1.getDate());
        var tq2 = new Date(rq2.getFullYear() - 1, rq2.getMonth(), rq2.getDate());
        var ljrq1 = new Date(rq1.getFullYear(), 0, 1);
        var ljtq1 = new Date(tq1.getFullYear(), 0, 1);
        var srq1 = GetDateString(rq1, rqlx);
        var srq2 = GetDateString(rq2, rqlx);
        var stq1 = GetDateString(tq1, rqlx);
        var stq2 = GetDateString(tq2, rqlx);
        var sljrq1 = GetDateString(ljrq1, rqlx);
        var sljtq1 = GetDateString(ljtq1, rqlx);
        addr += "&RQ1=" + srq1 + "&RQ2=" + srq2;
    }
    else {
        addr += "&RQ1=" + $("#edBMRQ1").val() + "&RQ2=" + $("#edBMRQ2").val();
    }
    var bmjc = $("#cbBMJC2").val();
    var mdid = $("#cbMD2").val();
    //if (bmjc == "") {
    //    ShowMessage("请选择商户和部门级次");
    //    return;
    //}
    if (mdid == "") {
        ShowMessage("请选择门店");
        return;
    }
    var hyktype = $("#HF_HYKTYPE").val();
    if (hyktype == "") {
        ShowMessage("请选择卡类型");
        return;
    }


    addr += "&LX=" + rqlx + "&BMJC=" + bmjc + "&MDID=" + mdid + "&HYKTYPE=" + hyktype; 
    addr += "&menu_id=" + vPageMsgID + "&PERSON_ID=" + iDJR;
    $("#fr2").attr("src", addr);
};
function btnSrch3Click() {
    var addr = ReportServer + "?reportlet=HYJF_RQ.cpt";
    var rqlx = $("#cbRQLX3").val();
    if (rqlx == "0") {
        var rq1 = new Date($("#TB_RQ1").val());
        var rq2 = new Date($("#TB_RQ2").val());
        var tq1 = new Date($("#TB_TQ1").val());
        var tq2 = new Date($("#TB_TQ2").val());
        var srq1 = GetDateString(rq1, rqlx);
        var srq2 = GetDateString(rq2, rqlx);
        var stq1 = GetDateString(tq1, rqlx);
        var stq2 = GetDateString(tq2, rqlx);
        addr += "&RQ1=" + srq1 + "&RQ2=" + srq2;
        addr += "&TQ1=" + stq1 + "&TQ2=" + stq2;
    }
    else {
        addr += "&RQ1=" + $("#TB_RQ1").val() + "&RQ2=" + $("#TB_RQ2").val();
        addr += "&TQ1=" + $("#TB_TQ1").val() + "&TQ2=" + $("#TB_TQ2").val();
    }
    if ($("#cbSH2").val() != "") {
        addr += "&SHDM=" + $("#cbSH2").val();
    }
    else {
        ShowMessage("请选择商户");
        return;
    }
    var mdid = $("#HF_MDID").val();
    if (mdid == "") {
        ShowMessage("请选择门店");
        return;
    }
    else
        addr += "&MDID=" + mdid;
    addr += "&LX=" + rqlx;
    var hyktype = $("#HF_HYKLX").val();
    if (hyktype == "") {
        ShowMessage("请选择卡类型");
        return;
    }
    else
        addr += "&HYKTYPE=" + hyktype;
    addr += "&menu_id=" + vPageMsgID + "&PERSON_ID=" + iDJR;
    $("#fr3").attr("src", addr);
};

function cbRQLX2Change() {
    $("#edBMRQ1").val("");
    $("#edBMRQ2").val("");
}

function cbRQLX3Change() {
    $("#TB_RQ1").val("");
    $("#TB_RQ2").val("");
    $("#TB_TQ1").val("");
    $("#TB_TQ2").val("");
}

function cbRQLXChange() {
    $("#edRQ1").val("");
    $("#edRQ2").val("");
}

function SHChange() {
    if ($("#cbSH").val() != "") {
        FillSHBMTreeBase("TreeSHBM", "TB_BMMC", "menuContent", $("#cbSH").val(), 5);
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

function onClick(e, treeId, treeNode) {
    $("#TB_BMMC").val(treeNode.name);
    $("#HF_BMDM").val(treeNode.id);
    hideMenuSHBM("menuContent");
}

function datechange1(date) {
    if (date != "") {
        if ($("#cbRQLX3").val() == "1") {
            $("#TB_TQ1").val(parseInt(date) - 100);
        }
        if ($("#cbRQLX3").val() == "2") {
            $("#TB_TQ1").val(parseInt(date) - 10);
        }
        if ($("#cbRQLX3").val() == "0") {
            var rq = new Date($("#TB_RQ1").val());
            var tq = new Date(rq.getFullYear() - 1, rq.getMonth(), rq.getDate());
            var stq = getDate(tq);
            $("#TB_TQ1").val(stq);
        }
    }
}
function datechange2(date) {
    if ($("#cbRQLX3").val() == "1") {
        $("#TB_TQ2").val(parseInt(date) - 100);
    }
    if ($("#cbRQLX3").val() == "2") {
        $("#TB_TQ2").val(parseInt(date) - 10);
    }
    if ($("#cbRQLX3").val() == "0") {
        var rq = new Date($("#TB_RQ2").val());
        var tq = new Date(rq.getFullYear() - 1, rq.getMonth(), rq.getDate());
        var stq = getDate(tq);
        $("#TB_TQ2").val(stq);
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
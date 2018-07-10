var ReportServer = "";

vHYZX = GetUrlParam("HYZX");

$(document).ready(function () {
    ReportServer = GetReportServer();
    //FillSH($("#cbSH"));
    //$("#TB_MDMC").click(function () {
    //    SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", false, $("#cbSH").val(), "", "", "", true);
    //});

    $("#TB_XFMD").click(function () {
        SelectMD("TB_XFMD", "HF_XFMD", "zHF_XFMD", false, "", "", "", 0, true)
    });

    $("#TB_SPMC").click(function () {
        SelectSHSP("TB_SPMC", "HF_SPID", "zHF_SPID", false);
    })

    $("#TB_RQ1").focus(function () {
        WdatePicker({ isShowWeek: true, onpicked: datechange1($("#TB_RQ1").val()) });
    });
    $("#TB_RQ2").focus(function () {
        WdatePicker({ isShowWeek: true, onpicked: datechange2($("#TB_RQ2").val()) });
    });
});

function btnSrch1Click() {
    var addr = ReportServer + "?reportlet=HYZXSP.cpt&HYZX=" + vHYZX;
    var rq1 = new Date($("#TB_RQ1").val());
    var rq2 = new Date($("#TB_RQ2").val());
    var tq1 = new Date($("#TB_TQ1").val());
    var tq2 = new Date($("#TB_TQ2").val());
    var hq1 = new Date($("#TB_HB1").val());
    var hq2 = new Date($("#TB_HB2").val());
    var srq1 = GetDateString(rq1, "0");
    var srq2 = GetDateString(rq2, "0");
    var stq1 = GetDateString(tq1, "0");
    var stq2 = GetDateString(tq2, "0");
    var shq1 = GetDateString(hq1, "0");
    var shq2 = GetDateString(hq2, "0");
    //if ($("#cbSH").val() != "") {
    //    addr += "&SHDM=" + $("#cbSH").val();
    //}
    //else {
    //    ShowMessage("请选择商户");



    //    return;
    //}
    var mdid = $("#HF_XFMD").val();
    if (mdid != "") {
        addr += "&MDID=" + mdid;
    }

    if ($("#TB_SP_ID").val() != "") {
        addr += "&SP_ID=" + $("#TB_SP_ID").val();
    }
    if ($("#HF_SPID").val() != "") {
        addr += "&SHSPID=" + $("#HF_SPID").val();
    }
    addr += "&RQ1=" + srq1 + "&RQ2=" + srq2;
    addr += "&TQ1=" + stq1 + "&TQ2=" + stq2;
    addr += "&HQ1=" + shq1 + "&HQ2=" + shq2;

    addr += "&menu_id=" + vPageMsgID + "&PERSON_ID=" + iDJR;
    $("#fr1").attr("src", addr);
};

//function SHChange() {
//    $("#TB_MDMC").val("");
//    $("#HF_MDID").val("");
//    $("#zHF_MDID").val("");
//}
function datechange1(date) {
    if (date != "") {
        var rq = new Date($("#TB_RQ1").val());
        var tq = new Date(rq.getFullYear() - 1, rq.getMonth(), rq.getDate());
        var stq = getDate(tq);

        var hb = new Date(rq.getFullYear(), rq.getMonth() - 1, rq.getDate());

        var sbh = getDate(hb);

        $("#TB_TQ1").val(stq);
        $("#TB_HB1").val(sbh);

    }
}
function datechange2(date) {
    if (date != "") {
        var rq = new Date($("#TB_RQ2").val());
        var tq = new Date(rq.getFullYear() - 1, rq.getMonth(), rq.getDate());
        var stq = getDate(tq);

        var hb = new Date(rq.getFullYear(), rq.getMonth() - 1, rq.getDate());
        var sbh = getDate(hb);

        $("#TB_TQ2").val(stq);
        $("#TB_HB2").val(sbh);

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



function SelectSHSP(SPMC, SPDM, zSPDM, Single) {
    var data = $("#" + zSPDM).val();
    SHDM = $("#HF_SHDM").val()
    var el = $("<input>", { type: 'text', val: data });
    $.dialog.data('IpValuesReturn', "");
    $.dialog.data('IpValues', el);
    $.dialog.data('IpValuesshdm', SHDM);
    if ($("#TB_SP_ID").val() != "") {
        $.dialog.data('IpValuesSP_ID', $("#TB_SP_ID").val());
    }
    $.dialog.data('IpValuesChoiceOne', Single);
    $.dialog.open("../../WUC/SHSP/WUC_SHSP.aspx", {
        lock: true, width: 420, height: 470, cancel: false
        , close: function () {
            WUC_SHSP_Return(SPMC, SPDM, zSPDM);
        }
    }, false);
}


function WUC_SHSP_Return(SPMC, SPDM, zSPDM) {
    var tp_return = $.dialog.data('IpValuesReturn');
    var tp_return_ChoiceOne = $.dialog.data('IpValuesReturnChoiceOne');
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            $("#" + SPMC).val(tp_mc);
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput.Articles;
            for (var i = 0; i <= contractValues.length - 1; i++) {
                tp_mc += contractValues[i].Name + ";";
                tp_hf += contractValues[i].Id + ",";
                //if (tp_return_ChoiceOne) {
                //    tp_hf += contractValues[i].Id;
                //} else {

                //}
            }
            $("#" + SPMC).val(tp_mc);
            $("#" + SPDM).val(tp_hf.substr(0, tp_hf.length - 1));
            $("#" + zSPDM).val(jsonString);
        }
    }
}
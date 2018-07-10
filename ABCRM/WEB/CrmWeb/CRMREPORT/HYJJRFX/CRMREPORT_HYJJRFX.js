var ReportServer = "";

$(document).ready(function () {
    ReportServer = GetReportServer();
    $("#TB_SHMC").click(function () {
        SelectSH("TB_SHMC", "HF_SHDM", "zHF_SHDM", true);
    });
    $("#TB_MDMC").click(function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", false, $("#HF_SHDM").val(), "", "", "",true);
    });
    $("#TB_SPFLMC").click(function () {
        if ($("#HF_SHDM").val() == "") {
            art.dialog({ lock: true, content: "请选择商户！" });
            return;
        }
        else {
            SelectSHSPFL("TB_SPFLMC", "HF_SPFLDM", "zHF_SPFLDM", $("#HF_SHDM").val(), true);
        }        
    })
    $("#TB_HYKNAME").click(function () {
        SelectKLX("TB_HYKNAME", "HF_HYKNAME", "zHF_HYKNAME", false, "", true);
    });
    $("#TB_JJRMC").click(function () {
        SelectJJR("TB_JJRMC", "HF_JJRID", "zHF_JJRID", false, true);
    });
    $("#TB_SBMC").click(function () {
        SelectSPSB("TB_SBMC", "HF_SBID", "zHF_SBID", false);
    });
    var date = new Date;
    var year1 = date.getFullYear();
    var year2 = date.getFullYear() - 1;
    $("#RQ1").val(year2);
    $("#RQ2").val(year1);
});

function btnSrch1Click() {
    var addr = ReportServer + "?reportlet=HYJJRFX.cpt";
    var rq1 = $("#RQ1").val();
    var rq2 = $("#RQ2").val();
    var shdm = $("#HF_SHDM").val();
    var mdid = $("#HF_MDID").val();
    var sbid = $("#HF_SBID").val();
    if (shdm == "") {
        ShowMessage("请选择商户");
        return;
    }
    if (mdid == "") {
        ShowMessage("请选择门店");
        return;
    }
    var jjrid = $("#HF_JJRID").val();
    var hyktype = $("#HF_HYKNAME").val();
    var spflid = $("#HF_SPFLID").val();
    var sbid = $("#HF_SBID").val();
    if ($("#HF_SHDM").val() != "") {
        addr += "&SHDM=" + $("#HF_SHDM").val();
    }
    if ($("#HF_MDID").val() != "") {
        addr += "&MDID=" + $("#HF_MDID").val();
    }
    addr += "&HYKTYPE=" + hyktype;
    //addr += "&SPFLID=" + spflid;
    //addr += "&SBID=" + sbid;
    addr += "&JJRID=" + jjrid;
    if ($("#RQ1").val() != "" && $("#RQ2").val() != "") {
        addr += "&RQ1=" + rq1 + "&RQ2=" + rq2;
    }
    else{
        ShowMessage("请输入日期");
        return;   
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
function WUC_SPFL_Return(SPFLMC, SPFLDM, zSPFLDM) {
    var tp_return = $.dialog.data('IpValuesReturn');
    if (tp_return) {
        var jsonString = tp_return;


        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            var tp_id = "";
            $("#" + SPFLMC).val(tp_mc);
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput.Depts;
            for (var i = 0; i <= contractValues.length - 1; i++) {
                tp_mc += contractValues[i].Name + ";";
                tp_hf += contractValues[i].Code + ",";
                tp_id += contractValues[i].id + ",";
            }
            $("#" + SPFLMC).val(tp_mc);
            $("#" + SPFLDM).val(tp_hf.substr(0, tp_hf.length - 1));
            $("#HF_SPFLID").val(tp_id.substr(0, tp_id.length - 1));
            $("#" + zSPFLDM).val(jsonString);
        }
    }
}
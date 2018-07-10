var ReportServer = "";

$(document).ready(function () {
    ReportServer = GetReportServer();
    FillMD($("#cbMD"));
    //FillMD($("#cbXFMD"));
    $("#TB_XFMD").click(function () {
        SelectMD("TB_XFMD", "HF_XFMD", "zHF_XFMD", false, "", "", "", 0, true)
    });
    var tp_length = parseInt($("#cbJC").val());
    if (tp_length != "1") {
        FillSHBMTreeBase("TreeSHBM", "TB_BMMC", "menuContent", "BH", tp_length);
    }

    $("#TB_HYZMC").click(function () {

        var mdid = $("#cbMD").val();
        var jb = $("#cbJB").val();
        if (mdid == "" && jb == "2") {
            ShowMessage("请选择门店");
            return;
        }
        var data = $("#zHF_HYZ").val();
        var el = $("<input>", { type: 'text', val: data });
        $.dialog.data('IpValues', el);
        $.dialog.data('IpValuesLevel', $("#cbJB").val());
        $.dialog.data('IpValuesMDID', mdid);
        $.dialog.data('IpValuesChoiceOne', false);
        $.dialog.open("../../WUC/HYZ/WUC_HYZ.aspx", {
            lock: true, width: 400, height: 430, cancel: false
                    , close: function () {
                        WUC_HYZ_Return();

                    }
        }, false);
    });

    $("#edRQ1").focus(function () {
        BindDateEvent($("#cbRQLX").val());
        WdatePicker({ onpicked: datechange1($("#edRQ1").val()) });
    });
    $("#edRQ2").focus(function () {
        BindDateEvent($("#cbRQLX").val());
        WdatePicker({ onpicked: datechange2($("#edRQ2").val()) });
    });
    $("#tdRQ1").focus(function () {
        BindDateEvent($("#cbRQLX").val());
    });
    $("#tdRQ1").focus(function () {
        BindDateEvent($("#cbRQLX").val());
    });

});

function btnSrch1Click() {
    var addr = ReportServer + "?reportlet=KQ_XSBD.cpt";
    var rqlx = $("#cbRQLX").val();
    if (rqlx == "0") {
        var rq1 = new Date($("#edRQ1").val());
        var rq2 = new Date($("#edRQ2").val());
        var tq1 = new Date($("#tdRQ1").val());
        var tq2 = new Date($("#tdRQ2").val());
        var srq1 = GetDateString(rq1, rqlx);
        var srq2 = GetDateString(rq2, rqlx);
        var stq1 = GetDateString(tq1, rqlx);
        var stq2 = GetDateString(tq2, rqlx);
        addr += "&RQ1=" + srq1 + "&RQ2=" + srq2;
        addr += "&TQ1=" + stq1 + "&TQ2=" + stq2;
    }
    else {
        //if ($("#edRQ1").val() != "" || $("#edRQ2").val() != "") {
        addr += "&RQ1=" + $("#edRQ1").val() + "&RQ2=" + $("#edRQ2").val();
        addr += "&TQ1=" + $("#tdRQ1").val() + "&TQ2=" + $("#tdRQ2").val();
    }
    //}
    var mdid = $("#cbMD").val();
    var xfmd = $("#HF_XFMD").val();
    var jb = $("#cbJB").val();
    var bmjc = parseInt($("#cbJC").val());
    if (bmjc != 5) {
        bmjc = parseInt(bmjc) * 2;
    }
    else {
        bmjc = 13;
    }
    if (mdid == "" && jb == "2") {
        ShowMessage("请选择门店");
        return;
    }
    if (xfmd == "") {
        ShowMessage("请选择消费门店");
        return;
    }
    addr += "&BMJC=" + bmjc;
    addr += "&LX=" + rqlx + "&JB=" + jb;
    if (mdid != "")
        addr += "&MDID=" + mdid;
    if (xfmd != "")
        addr += "&XFMD=" + xfmd;
    if ($("#HF_HYZ").val() != "")
        addr += "&GRPID=" + $("#HF_HYZ").val().substr(0, $("#HF_HYZ").val().length - 1);
    if ($("#HF_BMDM").val() != "")
        addr += "&BMDM=" + $("#HF_BMDM").val();
    //if ($("#edRQ1").val() != "" && $("#edRQ2").val() != "") {
    //    addr += "&RQ1=" + srq1 + "&RQ2=" + srq2;
    //}
    //if ($("#tdRQ1").val() != "" && $("#tdRQ2").val() != "") {
    //    addr += "&TQ1=" + stq1 + "&TQ2=" + stq2;
    //}
    addr += "&menu_id=" + vPageMsgID + "&PERSON_ID=" + iDJR;
    $("#fr1").attr("src", addr);
};

function SHChange() {
    if ($("#cbJC").val() != "") {
        var tp_length = parseInt($("#cbJC").val());
        if (tp_length != "1") {
            FillSHBMTreeBase("TreeSHBM", "TB_BMMC", "menuContent", "BH", tp_length);
        }
    }
    else {
        $("#TreeSHBM").html("");
        $("#TB_BMMC").val("");
        $("#HF_BMDM").val("");

    }
}

function onClick(e, treeId, treeNode) {
    $("#TB_BMMC").val(treeNode.name);
    $("#HF_BMDM").val(treeNode.id);
    hideMenuSHBM("menuContent");
}

function WUC_HYZ_Return() {
    var tp_return = $.dialog.data('IpValuesReturn');
    var tp_return_ChoiceOne = $.dialog.data('IpValuesChoiceOne');
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";

            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput.Articles;
            for (var i = 0; i <= contractValues.length - 1; i++) {
                if (tp_return_ChoiceOne) {
                    tp_mc += contractValues[i].Name;
                    tp_hf += contractValues[i].Id;
                } else {
                    tp_mc += contractValues[i].Name + ",";
                    tp_hf += contractValues[i].Id + ",";
                }
            }
            $("#TB_HYZMC").val(tp_mc);
            $("#HF_HYZ").val(tp_hf);
            $("#zHF_HYZ").val(jsonString);
        }
    }
}


function datechange1(date) {
    if (date != "") {
        if ($("#cbRQLX").val() == "1") {
            $("#tdRQ1").val(parseInt(date) - 100);
        }
        if ($("#cbRQLX").val() == "2") {
            $("#tdRQ1").val(parseInt(date) - 10);
        }
        if ($("#cbRQLX").val() == "0") {
            var rq = new Date($("#edRQ1").val());
            var tq = new Date(rq.getFullYear() - 1, rq.getMonth(), rq.getDate());
            var stq = getDate(tq);
            $("#tdRQ1").val(stq);
        }
    }
}

function datechange2(date) {
    if (date != "") {
        if ($("#cbRQLX").val() == "1") {
            $("#tdRQ2").val(parseInt(date) - 100);
        }
        if ($("#cbRQLX").val() == "2") {
            $("#tdRQ2").val(parseInt(date) - 10);
        }
        if ($("#cbRQLX").val() == "0") {
            var rq = new Date($("#edRQ2").val());
            var tq = new Date(rq.getFullYear() - 1, rq.getMonth(), rq.getDate());
            var stq = getDate(tq);
            $("#tdRQ2").val(stq);
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
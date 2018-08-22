var ReportServer = "";

$(document).ready(function () {
    ReportServer = GetReportServer();
    FillSH($("#cbSH"));
    FillMD($("#cbMD"));
    //FillMD($("#cbXFMD"));
    FillYear($("#cbND"));
    $("#TB_XFMD").click(function () {
        SelectMD("TB_XFMD", "HF_XFMD", "zHF_XFMD", false, "", "", "", 0, true)
    });
    var tp_length = parseInt($("#cbBMJC").val());
    if (tp_length != "1") {
        FillSHBMTreeBase("TreeSHBM", "TB_BMMC", "menuContent", "BH", tp_length);
    }
    var myDate = new Date();
    var myDateYear = myDate.getFullYear();
    $("#cbND").val(myDateYear);

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
});

function btnSrch1Click() {
    var addr = ReportServer + "?reportlet=KQ_XFQSFX.cpt";
    // var rq1 = new Date($("#edRQ1").val());
    // var rq2 = new Date($("#edRQ2").val());
    // var tq1 = new Date(rq1.getFullYear() - 1, rq1.getMonth(), rq1.getDate());
    // var tq2 = new Date(rq2.getFullYear() - 1, rq2.getMonth(), rq2.getDate());
    var bmjc = parseInt($("#cbBMJC").val());
    if (bmjc != 5) {
        bmjc = parseInt(bmjc) * 2;
    }
    else {
        bmjc = 13;
    }
    var mdid = $("#cbMD").val();
    var xfmd = $("#HF_XFMD").val();
    var xfxm = $("#cbXFXM").val();
    var bmlx = $("#cbBMLX").val();
    var jb = $("#cbJB").val();
    if (mdid == "" && jb == "2") {
        ShowMessage("请选择门店");
        return;
    }
    //if (xfmd == "" && bmjc == "2") {
    //    ShowMessage("请选择消费门店");
    //    return;
    //}
    if (xfmd == "") {
        ShowMessage("请选择消费门店");
        return;
    }


    // var srq1 = GetDateString(rq1, 0);
    // var srq2 = GetDateString(rq2, 0);

    addr += "&BMJC=" + bmjc + "&XFXM=" + xfxm;
    if (bmjc != 2) {
        addr += "&BMLX=" + bmlx;
    }
    addr += "&JB=" + jb;
    if (xfmd != "")
        addr += "&XFMD=" + xfmd;
    if (mdid != "")
        addr += "&MDID=" + mdid;
    if ($("#HF_HYZ").val() != "")
        addr += "&GRPID=" + $("#HF_HYZ").val().substr(0, $("#HF_HYZ").val().length - 1);
    if ($("#HF_BMDM").val() != "")
        addr += "&BMDM=" + $("#HF_BMDM").val();
    // if ($("#edRQ1").val() != "" && $("#edRQ2").val() != "") {
    //     addr += "&RQ1=" + srq1 + "&RQ2=" + srq2;      
    // }
    addr += "&ND=" + $("#cbND").val();
    addr += "&menu_id=" + vPageMsgID + "&PERSON_ID=" + iDJR;
    $("#fr1").attr("src", addr);
};


function BMJCChange() {
    if ($("#cbBMJC").val() != "" && $("#cbBMJC").val() != "1") {
        var tp_length = parseInt($("#cbBMJC").val());
        FillSHBMTreeBase("TreeSHBM", "TB_BMMC", "menuContent", "BH", tp_length);
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

function BMLXChange() {
    if ($("#cbBMLX").val() == 21) {
        $("#cbBMJC").empty();
        $("#cbBMJC").append("<option value='1'>1级</option>   ");
        $("#cbBMJC").append("<option value='3'>3级</option>   ");
        $("#cbBMJC").append("<option value='5'>5级</option>   ");
    }
    else {
        $("#cbBMJC").empty();
        $("#cbBMJC").append("<option value='3'>3级</option>   ");
        $("#cbBMJC").append("<option value='5'>5级</option>   ");

    }

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

function FillYear(selectName) {
    var myDate = new Date();
    var myDateYear = myDate.getFullYear();
    var myNextYear = parseInt(myDateYear) + 3;
    var myStartYear = parseInt(myDateYear) - 3;
    selectName.empty();
    for (var i = myStartYear; i <= myNextYear; i++) {
        selectName.append("<option value='" + i + "'>" + i + "年</option>");
    }
    // selectName.append("<option value='" + myNextYear + "'>" + myNextYear + "</option>");
}
vUrl = "../LPGL.ashx";

$(document).ready(function () {
    $("#TB_JLBH").hide();
    document.getElementById("JLBHCaption").innerHTML = "礼品ID";
    $("#JLBHCaption").hide();
    $("#B_Exec").hide();
    $("#status-bar").hide();
    //$("select[id*='S_LP']").each(function (index, element) {
    //    FillLPSX($(element), index);
    //});
    FillLPFLTree("TreeLPFL", "TB_LPFLMC");//只取未停用的礼品分类

    $("#TB_SHSBMC").click(function () {
        SelectSPSB("TB_SHSBMC", "HF_SHSBID", "zHF_SHSBID", true);
    });
});

function IsValidData() {
    var message;
    if ($("#TB_LPMC").val() == "") {
        ShowMessage("请输入礼品名称", 3);
        return false;
    }
    if ($("#TB_LPGG").val() == "") {
        ShowMessage("请输入礼品规格", 3);
        return false;
    }
    if ($("#TB_LPDJ").val() == "") {
        ShowMessage("请输入礼品单价", 3);
        return false;
    }
    if ($("#TB_LPJJ").val() == "") {
        ShowMessage("请输入礼品进价", 3);
        return false;
    }
    if ($("#DDL_LPLX option:selected").text() == "") {
        ShowMessage("请选择礼品类型", 3);
        return false;
    }
    if ($("#TB_LPJF").val() == "") {
        ShowMessage("请输入礼品积分", 3);
        return false;
    }

    if ($("#HF_LPFLID").val() == "") {
        ShowMessage("请输入礼品分类", 3);
        return false;
    }
    return true;
}
function TreeNodeClickCustom(e, treeId, treeNode) {
    $("#TB_LPFLMC").val(treeNode.name);
    $("#HF_LPFLID").val(treeNode.iJLBH);
}

function setDisable() {
    var tp_return = $.dialog.data('IpValuesReturn');
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var TP_id = "";
            $("#TB_SHSBMC").val(tp_mc);
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput.Articles;

            for (var i = 0; i < jsonInput.Articles.length; i++) {
                tp_mc = contractValues[i].Name + ";";
                TP_id = contractValues[i].Id + ",";
            }
            $("#TB_SHSBMC").val(tp_mc);
            $("#HF_SHSBID").val(TP_id.substring(0, TP_id.length - 1));
        }
    }
}


function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sLPDM = $("#TB_LPDM").val();
    Obj.sLPMC = $("#TB_LPMC").val();
    Obj.sLPGG = $("#TB_LPGG").val();
    Obj.fLPDJ = $("#TB_LPDJ").val();
    Obj.fLPJJ = $("#TB_LPJJ").val();
    Obj.iLPCZID = IsNullValue($("#S_LPCZ").val(), 0);
    Obj.iLPYSID = IsNullValue($("#S_LPYS").val(), 0);
    Obj.iLPKSID = IsNullValue($("#S_LPKS").val(), 0);
    Obj.sGJBM = $("#TB_LPGJBM").val();
    Obj.fLPJF = $("#TB_LPJF").val();
    Obj.iLPFLID = IsNullValue($("#HF_LPFLID").val(), 0);
    Obj.iBJ_WKC = $("#CK_BJ_WKC")[0].checked ? 1 : 0;
    Obj.iBJ_DEL = $("#CK_BJ_DEL")[0].checked ? 1 : 0;
    Obj.iSHSBID = IsNullValue($("#HF_SHSBID").val(), 0);
    Obj.iLPLX = GetSelectValue("DDL_LPLX");
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    return Obj;
}



function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#TB_LPDM").val(Obj.sLPDM);
    $("#TB_LPMC").val(Obj.sLPMC);
    $("#TB_LPGG").val(Obj.sLPGG);
    $("#TB_LPDJ").val(Obj.fLPDJ);
    $("#TB_LPJJ").val(Obj.fLPJJ);
    $("#TB_LPDM").val(Obj.sLPDM);
    $("#TB_LPGJBM").val(Obj.sGJBM);
    $("#TB_LPJF").val(Obj.fLPJF);
    SelectShow("S_LPCZ", Obj.iLPCZID);
    SelectShow("S_LPYS", Obj.iLPYSID);
    SelectShow("S_LPKS", Obj.iLPKSID);

    $("#HF_LPFLID").val(Obj.iLPFLID);
    $("#TB_LPFLMC").val(Obj.sLPFLMC);

    $("#TB_SHSBMC").val(Obj.sSBMC);
    $("#HF_SHSBID").val(Obj.iSHSBID);

    $("#CK_BJ_WKC")[0].checked = Obj.iBJ_WKC == "1" ? true : false;
    $("#CK_BJ_DEL")[0].checked = Obj.iBJ_DEL == "1" ? true : false;

    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
}



function SetControlState() {
    $("#TB_JLBH").hide();
}


function SelectShow(selectname, value) {
    if ($("#" + selectname).prop("disabled")) {
        $("#" + selectname).removeAttr("disabled")
            .val(value)
            .prop("disabled", true);
    }
    else {
        $("#" + selectname).val(value)
    }
}

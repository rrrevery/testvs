vUrl = "../GTPT.ashx";

var shdm = GetUrlParam("shdm");
var spdm = GetUrlParam("spdm");

$(document).ready(function () {
    document.getElementById("B_Update").onclick = function () {
        vProcStatus = cPS_MODIFY;
        SetControlBaseState();
        document.getElementById("B_Delete").disabled = true;
        document.getElementById("B_Update").disabled = true;
    }

    BFButtonClick("TB_SPMC", function () {
        if ($("#TB_SHMC").val() == "") {
            ShowMessage("请先选择商户", 3);
            return false;
        }
        SelectSHSP("TB_SPMC", "HF_SPDM", "zHF_SPDM", true);
    });

    BFButtonClick("TB_SHMC", function () {
        SelectSH("TB_SHMC", "HF_SHDM", "zHF_SHDM", true);
    });
    BFButtonClick("TB_MDMC", function () {
        SelectWXMD("TB_MDMC", "HF_MDID", "zHF_MDID", true);
    });

    BFButtonClick("TB_SBMC", function () {
        SelectWXSBMC("TB_SBMC", "HF_SBID", "zHF_SBID", true);
    });

    $('#upload').click(function () {
        if (!/\.(gif|jpg|jpeg|png|GIF|JPG|PNG)$/.test($("#files").val())) {
            ShowMessage("图片类型必须是.gif,jpeg,jpg,png中的一种");
            return;
        }
        UploadPicture("form2", "TB_IMG", "SPTJZHUTU");
    });

    $('#upload1').click(function () {
        if (!/\.(gif|jpg|jpeg|png|GIF|JPG|PNG)$/.test($("#files1").val())) {
            ShowMessage("图片类型必须是.gif,jpeg,jpg,png中的一种");
            return;
        }
        UploadPicture("form1", "TB_LOGO", "SPTJLOGO");

    });
});

//按钮事件单独写
function InsertClick() {
    PageDate_Clear();
    vProcStatus = cPS_ADD;
    $("#LB_DJRMC").text(sDJRMC);
    $("#HF_DJR").val(iDJR);
    SetControlBaseState();
    InsertClickCustom();

    var INXstr = GetSPTJINX();
    var INXdata = JSON.parse(INXstr);
    $("#TB_INX").val(INXdata.iINX + 1);

};
function zIsInt(vname, vInput) {
    var reg = /^[0-9]*$/;
    return returnMsg1(vname, vInput, reg);
}
function zIsEMail(vname, vInput) {
    var reg = /\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*/;
    return returnMsg2(vname, vInput, reg);
}
function zIsTelePhone(vname, vInput) {
    //if (vInput.length == 0) { return "手机号码为空"; }
    var strValue = Trim(vInput);
    if (strValue.length == 11) {
        var reg = /^1[3|4|5|7|8][0-9]\d{4,8}$/;
        return returnMsg(vname, vInput, reg);
    }
    else {
        art.dialog({ content: "手机号不合法，请重新填写", lock: true, time: 2 });
        return "" + vname + "（" + vInput + "）不合法，请重新填写!";


    }
}
function returnMsg1(vname, vInput, vReg) {
    //if (vInput.length == 0) { return ""; }
    var strValue = Trim(vInput);
    if (!vReg.test(strValue)) {
        art.dialog({ content: "序号不合法，请重新填写", lock: true, time: 2 });
        return "" + vname + "（" + vInput + "）不正确，请重新填写!";
    }
    else { return ""; }
}
function returnMsg2(vname, vInput, vReg) {
    //if (vInput.length == 0) { return ""; }
    var strValue = Trim(vInput);
    if (!vReg.test(strValue)) {
        art.dialog({ content: "网址不合法，请重新填写", lock: true, time: 2 });
        return "" + vname + "（" + vInput + "）不正确，请重新填写!";
    }
    else { return ""; }
}
function SetControlState() {
    $("#status-bar").hide();
    $("#B_Exec").hide();
}

function IsValidData() {

    if ($("#TB_SHMC").val() == "") {
        ShowMessage("商户名称能为空", 3);
        return false;
    }
    if ($("#TB_MDMC").val() == "") {
        ShowMessage("门店名称不能为空", 3);
        return false;
    }
    if ($("#TB_SPMC").val() == "") {
        ShowMessage("商品名称不能为空", 3);
        return false;
    }
    if ($("#TB_SPJC").val() == "") {
        ShowMessage("商品简称不能为空", 3);
        return false;
    }
    if ($("#TB_INX").val() == "") {
        ShowMessage("请输入序号", 3);
        return false;
    }
    if ($("#TB_SBMC").val() == "") {
        ShowMessage("请选择商标", 3);
        return false;
    }
    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";

    Obj.iWX_MDID = $("#HF_MDID").val();
    Obj.sSHDM = $("#HF_SHDM").val();
    Obj.sSPDM = $("#HF_SPDM").val();
    Obj.sSPMC = $("#TB_SPMC").val();
    Obj.sPHONE = $("#TB_PHONE").val();
    Obj.sDZ = $("#TB_DZ").val();
    Obj.sIP = $("#TB_IP").val();
    Obj.iINX = $("#TB_INX").val();
    Obj.sSPJG = $("#TB_SPJG").val();

    Obj.sIMG = $("#TB_IMG").val();
    Obj.sLOGO = $("#TB_LOGO").val();
    Obj.sSPJC = $("#TB_SPJC").val();
    Obj.iBJ_SHOW = $("[name='status']:checked").val();//展示标记
    Obj.iSBID = $("#HF_SBID").val();
    Obj.sSPCS = editor.html();
    Obj.sCONTENT = editor2.html();

    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);

    $("#TB_JLBH").val(Obj.iJLBH);
    $("#HF_MDID").val(Obj.iWX_MDID);
    $("#TB_SPMC").val(Obj.sSPMC);
    $("#HF_SPDM").val(Obj.sSPDM);
    $("#HF_SHDM").val(Obj.sSHDM);
    $("#TB_MDMC").val(Obj.sMDMC)

    $("#TB_PHONE").val(Obj.sPHONE);
    $("#TB_SHMC").val(Obj.sSHMC);
    $("#TB_IP").val(Obj.sIP);
    $("#TB_DZ").val(Obj.sDZ);
    $("#TB_INX").val(Obj.iINX);
    $("#TB_SPJG").val(Obj.sSPJG);
    $("#TB_IMG").val(Obj.sIMG);
    $("#TB_LOGO").val(Obj.sLOGO);
    $("#TB_SPJC").val(Obj.sSPJC);
    $("[name='status'][value='" + Obj.iBJ_SHOW + "']").prop("checked", true);
    $("#HF_SBID").val(Obj.iSBID);
    $("#TB_SBMC").val(Obj.sSBMC);
    editor.html(decodeURIComponent(Obj.sSPCS));
    editor2.html(decodeURIComponent(Obj.sCONTENT));
}

var fileuploader = "";
var uploaddialog = "";
var fileuploader1 = "";
var uploaddialog1 = "";

function UpdateClick() {
    vProcStatus = cPS_MODIFY;
    SetControlBaseState();
    UpdateClickCustom();
    document.getElementById("TB_MDMC").disabled = true;
    document.getElementById("TB_SHMC").disabled = true;
    document.getElementById("TB_SPMC").disabled = true;
};

function SelectSHSP(showField, hideField, hideData, mult, condData) {
    var dialogUrl = "../../CrmArt/ListSHSP/CrmArt_ListSHSP.aspx";
    MoseDialogModel("ListSHSP", hideField, showField, hideData, dialogUrl, "商品信息", mult, "Name", "Code", condData);
}
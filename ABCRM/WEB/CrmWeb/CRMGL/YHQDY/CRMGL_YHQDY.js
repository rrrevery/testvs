vUrl = "../CRMGL.ashx";


$(document).ready(function () {
    $("#B_Exec").hide();
    $("#status-bar").hide();
    $("#jlbh .dv_sub_left").html("优惠券ID");


    BFUploadClick("TB_IMAGEURL", "HF_IMAGEURL", "FtpImg/YHQDY");
})

function InsertClickCustom() {
    $("#CK_iBJ_DZYHQ")[0].checked = true;
}

function IsValidData() {
    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sYHQMC = $("#TB_HYQMC").val();

    Obj.iBJ_DZYHQ = $("#CK_iBJ_DZYHQ")[0].checked ? 1 : 0;
    Obj.iBJ_TY = $("#CK_iBJ_TY")[0].checked ? 1 : 0;
    Obj.iBJ_CXYHQ = $("#CK_iBJ_CXYHQ")[0].checked ? 1 : 0;
    Obj.iBJ_FQ = $("#CK_iBJ_FQ")[0].checked ? 1 : 0;

    Obj.iFS_YQMDFW = GetSelectValue("S_YQFS");
    Obj.iFS_FQMDFW = GetSelectValue("S_FQFS");
    Obj.iFQLX = GetSelectValue("S_FQLX");
    Obj.iBJ_TS = GetSelectValue("S_BJ_TS"); 
    Obj.iYXQTS = $("#TB_THYXQTS").val() != "" ? $("#TB_THYXQTS").val() : 0;
    Obj.iISCODED = $("#CK_ISCODED")[0].checked ? 1 : 0;
    Obj.iCODELEN = $("#TB_CODELEN").val() != "" ? $("#TB_CODELEN").val() : "-1";
    Obj.sCODEPRE = $("#TB_CODEPRE").val();
    Obj.sCODESUF = $("#TB_CODESUF").val();
    Obj.sIMAGEURL = $("#TB_IMAGEURL").val();

    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;

    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);

    $("#TB_HYQMC").val(Obj.sYHQMC);

    $("#CK_iBJ_DZYHQ")[0].checked = Obj.iBJ_DZYHQ == "1" ? true : false;
    $("#CK_iBJ_TY")[0].checked = Obj.iBJ_TY == "1" ? true : false;
    $("#CK_iBJ_CXYHQ")[0].checked = Obj.iBJ_CXYHQ == "1" ? true : false;
    $("#CK_iBJ_FQ")[0].checked = Obj.iBJ_FQ == "1" ? true : false;

    $("#S_YQFS").val(Obj.iFS_YQMDFW);
    $("#S_FQFS").val(Obj.iFS_FQMDFW);
    $("#S_FQLX").val(Obj.iFQLX);

    $("#TB_THYXQTS").val(Obj.iYXQTS);
    $("#CK_ISCODED")[0].checked = Obj.iISCODED == "1" ? true : false;

    $("#S_BJ_TS").val(Obj.iBJ_TS);

    $("#TB_HYKHM_OLD").val(Obj.sHYKHM_OLD);
    $("#TB_CODELEN").val(Obj.iCODELEN);
    $("#TB_CODEPRE").val(Obj.sCODEPRE);
    $("#TB_CODESUF").val(Obj.sCODESUF);
    $("#TB_IMAGEURL").val(Obj.sIMAGEURL);
}



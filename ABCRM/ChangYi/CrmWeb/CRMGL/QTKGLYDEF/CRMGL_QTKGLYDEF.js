vUrl = "../CRMGL.ashx";
vCaption = "前台卡管理员定义";

function InitGrid() {
    vColumnNames = ['门店名称', '门店ID', '人员名称', '人员ID', '操作地点', '操作地点代码'];
    vColumnModel = [
        { name: 'sMDMC', },//sortable默
        { name: 'iMDID', },
        { name: 'sRYMC', },
        { name: 'iPERSON_ID', },
        { name: 'sBGDDMC', },
        { name: 'sCZDD', },
    ];
}

function ShowData(rowData) {
    $("#HF_MDID").val(rowData.iMDID);
    $("#TB_MDMC").val(rowData.sMDMC);
    $("#HF_DJR").val(rowData.iPERSON_ID);
    $("#TB_DJRMC").val(rowData.sRYMC);
    $("#HF_BGDDDM").val(rowData.sCZDD);
    $("#TB_BGDDMC").val(rowData.sBGDDMC);
    $("#TB_JLBH").val("1");
}

$(document).ready(function () {
    $("#TB_MDMC").click(function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", true);
    });
    $("#TB_BGDDMC").click(function () {
        SelectBGDD("TB_BGDDMC", "HF_BGDDDM", "zHF_BGDDDM");
    });
    $("#TB_DJRMC").click(function () {
        SelectRYXX("TB_DJRMC", "HF_DJR", "zHF_DJR", true);
    });
});

function SetControlState() {
    $("#jlbh").hide();
    $("#B_Insert").show();
    $("#B_Exec").hide();
    $("#status-bar").hide();
}

function IsValidData() {
    if ($("#TB_MDMC").val() == "") {
        ShowMessage("门店不能为空", 2);
        return false;
    }
    if ($("#HF_BGDDDM").val() == "") {
        ShowMessage("操作地点不能为空", 2);
        return false;
    }
    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.iMDID = $("#HF_MDID").val();
    Obj.sCZDD = $("#HF_BGDDDM").val();
    Obj.iPERSON_ID = $("#HF_DJR").val();
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    return Obj;
}
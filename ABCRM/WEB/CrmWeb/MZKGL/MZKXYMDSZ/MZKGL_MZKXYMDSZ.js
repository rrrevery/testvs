vUrl = "../MZKGL.ashx";
var rowData;
var vCaption = "面值卡限用门店设置";
function InitGrid() {
    vColumnNames = ['卡类型', 'iHYKTYPE', 'iMDID', '门店名称', ];
    vColumnModel = [
           { name: 'sHYKNAME', width: 150, },
             { name: 'iHYKTYPE', width: 150, hidden: true },
             { name: 'iMDID', width: 150, hidden: true },
             { name: 'sMDMC', width: 150 },
    ];
}


$(document).ready(function () {


    BFButtonClick("TB_MDMC", function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", false);
    });
    BFButtonClick("TB_HYKNAME", function () {
        var condData = new Object();
        condData["vCZK"] = 1;
        SelectKLX("TB_HYKNAME", "HF_HYKTYPE", "zHF_HYKTYPE", true, condData);
    });




});

function IsValidData() {
    if ($("#HF_MDID").val() == "" ) {
        ShowMessage("门店不能为空");
        return false;
    }
    if ($("#HF_HYKTYPE").val() =="" ) {
        ShowMessage("卡类型不能为空");
        return false;
    }

    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.iHYKTYPE = $("#HF_HYKTYPE").val();
    Obj.sMDID = $("#HF_MDID").val();
    if ($("#HF_OLDHYKTYPE").val() != "" && $("#HF_OLDMDID").val() != "")
    {
        Obj.iHYKTYPEOLD = $("#HF_OLDHYKTYPE").val();
        Obj.iMDIDOLD = $("#HF_OLDMDID").val();
    }
    Obj.sDBConnName = "CRMDBMZK";
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;

    return Obj;
}

function ShowData(data) {

    $("#TB_JLBH").val(data.iMDID);
    $("#TB_HYKNAME").val(data.sHYKNAME);
    $("#TB_MDMC").val(data.sMDMC);
    $("#HF_HYKTYPE").val(data.iHYKTYPE);
    $("#HF_MDID").val(data.iMDID);
    $("#HF_OLDHYKTYPE").val(data.iHYKTYPE);
    $("#HF_OLDMDID").val(data.iMDID);

}

function SetControlState() {
    $("#jlbh").hide();
    $("#B_Insert").show();
    $("#B_Exec").hide();
    $("#status-bar").hide();
}
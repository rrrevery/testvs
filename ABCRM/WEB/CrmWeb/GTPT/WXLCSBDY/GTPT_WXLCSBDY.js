vUrl = "../GTPT.ashx";
var mdid = GetUrlParam("mdid");
var sbid = GetUrlParam("sbid");
$(document).ready(function () {
    $("#jlbh").hide();

    BFButtonClick("TB_SBMC", function () {
        SelectWXSBMC("TB_SBMC", "HF_SBID", "zHF_SBID", true);
    });

    
    BFButtonClick("TB_MDMC", function () {
        SelectWXMD("TB_MDMC", "HF_MDID", "zHF_MDID",true);
    });


    BFButtonClick("TB_LCMC", function () {
        if ($("#TB_MDMC").val() == "") {
            ShowMessage("请先选择门店", 3);
            return false;
        }
        var condData = new Object();
        condData["iMDID"] = $("#HF_MDID").val();
        SelectWXLC("TB_LCMC", "HF_LCID", "zHF_LCID", true, condData);
    });
    vProcStatus = cPS_BROWSE;
    SetControlBaseState();

    RefreshButtonSep();
});


function SetControlState() {


    $("#status-bar").hide();
    $("#B_Exec").hide();
    $("#B_Update").hide();

}

function IsValidData() {
    if ($("#TB_MDMC").val() == "") {
        ShowMessage("门店名称不能为空", 3);
        return false;
    }
    if ($("#TB_LCMC").val() == "") {
        ShowMessage("楼层名称不能为空", 3);
        return false;
    }
    if ($("#TB_SBMC").val() == "") {
        ShowMessage("商标名称不能为空",3);
        return false;
    }

    
  
    return true;
}



function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";

    //Obj.sSBIDString = $("#HF_SBID").val();
    Obj.iSBID = $("#HF_SBID").val();
    Obj.sSBMC = $("#TB_SBMC").val();
    Obj.iMDID = $("#HF_MDID").val();
    mdid = Obj.iMDID;
    sbid = $("#HF_SBID").val();
    Obj.iLCID = $("#HF_LCID").val();
    Obj.sNAME = $("#TB_LCMC").val();
    return Obj;
}



function ShowData(data) {
    var Obj = JSON.parse(data);
  
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#HF_SBID").val(Obj.iSBID);
    $("#TB_SBMC").val(Obj.sSBMC);
    $("#HF_FLMC").val(Obj.sFLMC);
    $("#HF_FLID").val(Obj.iFLID);
    $("#TB_MDMC").val(Obj.sMDMC);
    $("#HF_MDID").val(Obj.iMDID);
    $("#HF_LCID").val(Obj.iLCID);
    $("#TB_LCMC").val(Obj.sNAME);


}
function MakeJLBH(t_jlbh) {
    var arrayObj = new Array();
    MakeSrchCondition2(arrayObj, t_jlbh, "iJLBH", "=", false);
    MakeSrchCondition2(arrayObj, mdid, "iMDID", "=", false);
    MakeSrchCondition2(arrayObj, sbid, "iSBID", "=", false);
    return arrayObj;
}


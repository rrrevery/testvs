vUrl = "../GTPT.ashx";
var vProcStatus = 0;
var cPS_BROWSE = 0;
var bCanEdit = true;
var bHasData = $("#HF_MDID").val() != "";//有数据
var bEditMode = (vProcStatus != cPS_BROWSE);//编辑状态

$(document).ready(function () {
    document.getElementById("B_Delete").disabled = true;
    document.getElementById("B_Update").disabled = true;

    $("#B_Exec").hide();

    BFButtonClick("TB_MDMC", function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", true);
    });

    BFButtonClick("TB_CSMC", function () {
        SelectCS("TB_CSMC", "HF_CSID", "zHF_CSID", true);
    });

    document.getElementById("B_Update").onclick = function () {
        vProcStatus = cPS_MODIFY;
        SetControlBaseState();
        document.getElementById("B_Delete").disabled = true;
        document.getElementById("B_Update").disabled = true;

    };


    BFUploadClick("TB_IMG", "HF_IMAGEURL", "FtpImg/MDZHUTU");


    //$('#upload').click(function () {
    //    if (!/\.(gif|jpg|jpeg|png|GIF|JPG|PNG)$/.test($("#files").val())) {
    //        ShowMessage("图片类型必须是.gif,jpeg,jpg,png中的一种")
    //        return;
    //    }
    //    UploadPicture("form1", "TB_IMG", "MDZHUTU");
 
    //});
});
function SetControlState() {
    $("#status-bar").hide();
    $("#B_Exec").hide();
    document.getElementById("B_Update").disabled = !(!bEditMode && bHasData);
    document.getElementById("B_Delete").disabled = document.getElementById("B_Update").disabled;
}



function IsValidData() {
    if ($("#TB_LAT").val() == "") {
        ShowMessage("百度地图纬度请输入数字", 3);
        return false;
    }
    if ($("#TB_LEN").val() == "") {
        ShowMessage("百度地图经度请输入数字", 3);
        return false;
    }

    return true;
}
function SaveDataBase() {
    var Obj = new Object();
    Obj = SaveData(Obj);
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    Obj.iLoginPUBLICID = iWXPID;

    return Obj;
};
function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#HF_MDID").val();

    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sMDMC = $("#TB_MDMC").val();
    Obj.iCITYID = $("#HF_CSID").val();
    Obj.sCSMC = $("#TB_CSMC").val();
    Obj.sIMG = $("#TB_IMG").val();
    Obj.sMDNR = $("#TB_MDNR").val();
    Obj.sADDRESS = $("#TB_ADDRESS").val();
    Obj.sSUBWAY = $("#TB_SUBWAY").val();
    Obj.sDRIVE = $("#TB_DRIVE").val();
    Obj.sBUS = $("#TB_BUS").val();
    Obj.sTIME = $("#TB_TIME").val();
    Obj.sPARK = $("#TB_PARK").val();
    Obj.sPAY = $("#TB_PAY").val();
    Obj.sYHXX = $("#TB_YHXX").val();
    Obj.sPHONE = $("#TB_PHONE").val();
    Obj.sTITLE = $("#TB_TITLE").val();
    Obj.sCONTENT = $("#TB_CONTENT").val();
    if ($("#TB_LAT").val() != null && $("#TB_LAT").val() != "")
        Obj.fLAT = $("#TB_LAT").val();
    if ($("#TB_LEN").val() != null && $("#TB_LEN").val() != "")
        Obj.fLEN = $("#TB_LEN").val();
    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#HF_CSID").val(Obj.iCITYID);
    $("#TB_CSMC").val(Obj.sCSMC);
    $("#HF_MDID").val(Obj.iJLBH);
    $("#TB_MDMC").val(Obj.sMDMC);
    $("#TB_IMG").val(Obj.sIMG);
    $("#TB_MDNR").val(Obj.sMDNR);
    $("#TB_ADDRESS").val(Obj.sADDRESS);
    $("#TB_SUBWAY").val(Obj.sSUBWAY);
    $("#TB_DRIVE").val(Obj.sDRIVE);
    $("#TB_BUS").val(Obj.sBUS);
    $("#TB_TIME").val(Obj.sTIME);
    $("#TB_PARK").val(Obj.sPARK);
    $("#TB_PAY").val(Obj.sPAY);
    $("#TB_YHXX").val(Obj.sYHXX);
    $("#TB_PHONE").val(Obj.sPHONE);
    $("#TB_IP").val(Obj.sIP);
    $("#TB_TITLE").val(Obj.sTITLE);
    $("#TB_CONTENT").val(Obj.sCONTENT);
    $("#TB_LAT").val(Obj.fLAT);
    $("#TB_LEN").val(Obj.fLEN);
    $("#selectPublicID").combobox("setValue", Obj.iPUBLICID)


}
function InsertClick() {
    PageDate_Clear();
    vProcStatus = cPS_ADD;
    $("#LB_DJRMC").text(sDJRMC);
    $("#HF_DJR").val(iDJR);
    SetControlBaseState();
    InsertClickCustom();

};


function IsNumber(testChar) {
    var BoolVailed = true;
    var regstring = "^[0-9][0-9]*$";
    var ipReg = new RegExp(regstring);
    if (ipReg.test(testChar) == false) {
        BoolVailed = false;
    }
    return BoolVailed;

}



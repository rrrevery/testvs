vUrl = "../GTPT.ashx";

$(document).ready(function () {
    $("#ZZR").show();
    $("#ZZSJ").show();
    $("#QDR").show();
    $("#QDSJ").show();
    $("#B_Start").show();
    $("#B_Stop").show();

    BFButtonClick("TB_MDMC", function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", true);
    }); 


    BFUploadClick("TB_IMG", "HF_IMAGEURL", "FtpImg/YYFWZHUTU");

    RefreshButtonSep();

});


//按钮事件单独写
function InsertClick() {
    PageDate_Clear();
    vProcStatus = cPS_ADD;
    $("#LB_DJRMC").text(sDJRMC);
    $("#HF_DJR").val(iDJR);
    SetControlBaseState();
    InsertClickCustom();

    var INXstr = GetYYFWDEFINX();
    var INXdata = JSON.parse(INXstr);
    $("#TB_INX").val(INXdata.iINX + 1);

};
function IsValidData() {
    if ($("#HF_MDID").val() == "") {
        ShowMessage("请选择门店名称", 3);
        return false;
    }
    if ($("#TB_INX").val() == "") {
        ShowMessage("请输入序号", 3);
        return false;
    }
    if ($("#TB_YYZT").val() == "") {
        ShowMessage("请输入预约主题", 3);
        return false;
    }

    if ($("#TB_XZRS").val() == "") {
        ShowMessage("请输入预约限制最高人数", 3);
        return false;
    }
    return true;
}

function StartClick() {
    ShowYesNoMessage("启动本单执行将会终止正在启动的单据，是否覆盖继续？", function () {
        if (posttosever(SaveDataBase(), vUrl, "Start") == true) {
            vProcStatus = cPS_BROWSE;
            ShowDataBase(vJLBH);
            SetControlBaseState();
        }
    });
};
function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.iMDID = $("#HF_MDID").val();
    Obj.iINX = $("#TB_INX").val();
    Obj.iXZRS = $("#TB_XZRS").val();
    Obj.sMC = $("#TB_MC").val();
    Obj.sIMG = $("#TB_IMG").val();
    Obj.dKSRQ = $("#TB_KSRQ").val();
    Obj.dJSRQ = $("#TB_JSRQ").val();
    Obj.sYYTM = $("#TB_YYTM").val();
    Obj.sADDRESS = $("#TB_ADDRESS").val();
    Obj.sYYNR = $("#TB_YYNR").val();
    Obj.iTYPE = $("[name='fwlx']:checked").val();
    Obj.iCHANNELID = GetSelectValue("Select1");
    Obj.sXXNR = editor.html();
    Obj.iSTATUS = vProcStatus;
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#HF_MDID").val(Obj.iMDID);
    $("#TB_MDMC").val(Obj.sMDMC);
    $("#TB_INX").val(Obj.iINX);
    $("#TB_XZRS").val(Obj.iXZRS);
    $("#TB_MC").val(Obj.sMC);
    $("#TB_IMG").val(Obj.sIMG);
    $("#TB_KSRQ").val(Obj.dKSRQ);
    $("#TB_JSRQ").val(Obj.dJSRQ);
    $("#TB_ADDRESS").val(Obj.sADDRESS);
    $("#TB_YYTM").val(Obj.sYYTM);
    $("#TB_YYNR").val(Obj.sYYNR);
    $("[name='fwlx'][value='" + Obj.iTYPE + "']").prop("checked", true);
    $("#Select1").val(Obj.iCHANNELID);
    var userImageUrl = Obj.sIMG;
    $("#ImageShow").attr("src", userImageUrl);
    editor.html(decodeURIComponent(Obj.sXXNR));

    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);
    $("#LB_QDRMC").text(Obj.sQDRMC);
    $("#HF_QDR").val(Obj.iQDR);
    $("#LB_QDSJ").text(Obj.dQDSJ);
    $("#LB_ZZRMC").text(Obj.sZZRMC);
    $("#HF_ZZR").val(Obj.iZZR);
    $("#LB_ZZRQ").text(Obj.dZZRQ);
    $("#selectPublicID").combobox("setValue", Obj.iPUBLICID)


}





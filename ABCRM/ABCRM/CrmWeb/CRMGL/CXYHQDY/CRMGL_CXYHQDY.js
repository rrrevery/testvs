vUrl = "../CRMGL.ashx";
//var t_jlbh = GetUrlParam("yhqid");

var vSHDM = GetUrlParam("shdm");
var vCXID = GetUrlParam("cxid");

function IsValidData() {
    if ($("#TB_JSRQ").val() == "" && $("#TB_YXQTS").val() == "") {
        art.dialog({ lock: true, content: "结束日期与有效期天数必须输入一项", });
        return false;
    }
    if ($("#TB_JSRQ").val() != "" && $("#TB_YXQTS").val() != "") {
        art.dialog({ lock: true, content: "结束日期与有效期天数只能输入输入一项", });
        return false;
    }
    if ($("#HF_CXID").val() == "") {
        art.dialog({ lock: true, content: "请选择促销主题", });
        return false;
    }
    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#HF_YHQID").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sSHDM = $("#HF_SHDM").val();
    Obj.iCXID = $("#HF_CXID").val();
    Obj.iYHQID = $("#HF_YHQID").val();
    Obj.sZQBZ1 = $("#TB_ZQBZ1").val();
    Obj.sZQBZ2 = $("#TB_ZQBZ2").val();
    if ($("#TB_JSRQ").val() != "") {
        Obj.dYHQSYJSRQ = $("#TB_JSRQ").val();
    }


    if ($("#TB_YXQTS").val() != "") {
        Obj.iYXQTS = $("#TB_YXQTS").val();
    }
    if ($("#TB_YXQTS").val() == "") {
        Obj.iYXQTS =3;
    }

    t_jlbh = Obj.iYHQID;
    vCXID = Obj.iCXID;
    vSHDM = Obj.sSHDM;


    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;

    return Obj;
}

$(document).ready(function () {
    $("#B_Exec").hide();

    BFButtonClick("TB_CXZT", function () {
        if ($("#HF_SHDM").val() != "") {
            SelectCXHD("TB_CXZT", "HF_CXID", "zHF_CXID", true, $("#HF_SHDM").val());
        }
        else {
            ShowMessage("请选择商户");
        }
    });

    BFButtonClick("TB_SHMC", function () {
        SelectSH("TB_SHMC", "HF_SHDM", "zHF_SHDM", true);
    });
    BFButtonClick("TB_YHQMC", function () {
        SelectYHQ("TB_YHQMC", "HF_YHQID", "zHF_YHQID", true);
    });
})

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iYHQID);
    $("#TB_SHMC").val(Obj.sSHMC);
    $("#HF_SHDM").val(Obj.sSHDM);
    $("#HF_CXID").val(Obj.iCXID);
    $("#TB_CXZT").val(Obj.sCXZT);
    $("#TB_YHQMC").val(Obj.sYHQMC);
    $("#HF_YHQID").val(Obj.iYHQID);
    $("#TB_JSRQ").val(Obj.dYHQSYJSRQ);
    if (Obj.iYXQTS == 0)
        $("#TB_YXQTS").val("");
    else
        $("#TB_YXQTS").val(Obj.iYXQTS);

    $("#LB_CXID").text(Obj.iCXID);
    $("#LB_CXHDBH").text(Obj.iCXHDBH);
    $("#LB_KSSJ").text(Obj.dKSSJ);
    $("#LB_JSSJ").text(Obj.dJSSJ);
    $("#TB_ZQBZ1").val(Obj.sZQBZ1);
    $("#TB_ZQBZ2").val(Obj.sZQBZ2);
}

function MakeJLBH(t_jlbh) {
    //生成iJLBH的JSON
    var arrayObj = new Array();
  
        MakeSrchCondition2(arrayObj, t_jlbh, "iJLBH", "=", false);
        MakeSrchCondition2(arrayObj, vSHDM, "sSHDM", "=", true);
        MakeSrchCondition2(arrayObj, vCXID, "iCXID", "=", false);
        //MakeSrchCondition2(arrayObj, vYHQID, "iYHQID", "=", false);
    
    if (GetUrlParam("mzk") == "1") {
        Obj.sDBConnName = "CRMDBMZK";
    }
    return arrayObj;
}


function WUC_CXHD_Return(CXZT, CXID, zCXID) {
    var tp_return = $.dialog.data('IpValuesReturn');
    var tp_return_ChoiceOne = $.dialog.data('IpValuesReturnChoiceOne');
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            $("#" + CXZT).val(tp_mc);
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput.Articles;
            for (var i = 0; i <= contractValues.length - 1 && contractValues[i].sCXZT; i++) {
                tp_mc += contractValues[i].sCXZT + ";";
                tp_hf += contractValues[i].iCXID + ",";

            }
            $("#" + CXZT).val(tp_mc);
            $("#" + CXID).val(tp_hf.substr(0, tp_hf.length - 1));
            $("#" + zCXID).val(jsonString);

            if ($("#" + CXID).val() != "") {

                $("#LB_CXID").text(contractValues[0].iCXID);
                $("#LB_CXHDBH").text(contractValues[0].iCXHDBH);
                $("#LB_KSSJ").text(contractValues[0].dKSSJ);
                $("#LB_JSSJ").text(contractValues[0].dJSSJ);
            }
            else {
                $("#LB_CXID").text("");
                $("#LB_CXHDBH").text("");
                $("#LB_KSSJ").text("");
                $("#LB_JSSJ").text("");
            }
        }
    }
}
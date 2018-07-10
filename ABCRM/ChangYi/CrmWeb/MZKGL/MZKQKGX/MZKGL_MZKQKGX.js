vUrl = "../MZKGL.ashx";
//var HYKNO = GetUrlParam("HYKNO");

$(document).ready(function ()
{
    $("#TB_SKDJLBH").click(function ()
    {
        var DataArry = new Object();
        DataArry["iSTATUS"] = 1;
        DataArry["BJ_TS"] = 0;
        DataArry["BJ_QKGX"] = 1;
        SelectMZKSKD('TB_SKDJLBH', 'HF_SKDJLBH', 'zHF_SKDJLBH', false, DataArry);
    })



});




function SetControlState()
{
    ;
}

function IsValidData()
{
    if ($("#TB_SKDJLBH").val() == "")
    {
        ShowMessage("请选择售卡单");
        return false;
    }
    if ($("#TB_HKJE").val() == "")
    {
        ShowMessage("请输入还款金额");
        return false;
    }
    return true;
}

function SaveData()
{
    var Obj = new Object();
    Obj.sDBConnName = "CRMDBMZK";
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.fQKJE = $("#LB_DHKJE").text();
    Obj.fFKJE = $("#TB_HKJE").val();
    Obj.iSKDJLBH = $("#TB_SKDJLBH").val();
    Obj.sZY = $("#TB_ZY").val();
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    return Obj;
}

function ShowData(data)
{
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#TB_SKDJLBH").val(Obj.iSKDJLBH);
    $("#TB_ZY").val(Obj.sZY);
    $("#LB_DHKJE").text(Obj.fFKJE);
    $("#TB_HKJE").val(Obj.fFKJE);
    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);
}

function MoseDialogCustomerReturn(dialogName, lstData, showField)
{
    if (dialogName == "ListMZKSKD")
    {
        for (var i = 0; i < lstData.length; i++)
        {
            $("#TB_SKDJLBH").val(lstData[i].iJLBH);
            $("#LB_DHKJE").text(lstData[i].fDHJE);
        }
    }
};
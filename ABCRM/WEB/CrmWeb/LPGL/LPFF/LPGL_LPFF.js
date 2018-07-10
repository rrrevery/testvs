vUrl = "../LPGL.ashx";
zURL = "LPGL_LPFFCZ.ashx";
var slxz = 0
var rowNumer = 0;
var CZJLBH = 0;
var BoolKCSL = true;

var HYKNO = GetUrlParam("HYKNO");
function InitGrid() {
    vColumnNames = ["LPID", "礼品代码", "礼品名称", "积分", "金额", "库存数量", "兑换数量", "不限制库存"];
    vColumnModel = [
          { name: "iLPID", hidden: true },
          { name: "sLPDM", width: 60 },
          { name: "sLPMC", width: 120 },
          { name: "fLPJF", width: 60 },
          { name: "fLPJE", width: 60 },
          { name: "fKCSL", width: 60 },
          { name: "fSL", width: 60, editor: 'text' },
          { name: "iBJ_WKC", hidden: true },
    ];
};
$(document).ready(function () {
    FillBGDDTree("TreeBGDD", "TB_BGDDMC", "menuContent");
    $("#B_Exec").hide();
    $("#B_Delete").hide();
    $("#B_Update").hide();
    $("#B_UnExec").show();
    $("#B_UnExec").text("冲正");
    vUnExecMethod = "Rollback";

    val = document.getElementById("status-bar").innerHTML;
    val += "<div class='bfrow'>";
    val += "<div class='bffld' id='CZR'><div class='bffld_left' id='czr1'>冲正人</div><div class='bffld_right'><label id='LB_CZRMC' class='djr'></label><label id='LB_CZRQ' class='djsj'></label><input id='HF_CZR' type='hidden'/></div></div></div>";
    document.getElementById("status-bar").innerHTML = val;

    $("#TB_HYKNO").bind('keypress', function (event) {
        if (event.keyCode == "13") {
            GetHYKXX();
        }
    });
    if (HYKNO != "") {
        $("#TB_HYKNO").val(HYKNO);
        GetHYKXX();
        $("#TB_HYKNO").attr("readonly", "readonly");
    }
    $("#btn_HYKHM").click(function () {
        var conData = new Object();
        conData.iBJ_KCK = 0;
        SelectSK("TB_HYKNO", "HF_HYID", "", conData);
    });

    $("#DelItem").click(function () {
        DeleteRows("list");
        CalculatorsInteger();
    });
    $("#DDL_FFGZ").combobox({
        onClick: function (data) {
            if ($("#HF_BGDDDM").val() == "") {
                ShowMessage("请选择保管地点");
            }
            else {
                if (data.value != "")
                {
                    var lpxx = "";
                    lpxx = GetLPFFGZLP(data.value, $("#HF_BGDDDM").val());
                    if (lpxx != "") {
                        var lst = JSON.parse(lpxx);
                        $('#list').datagrid('loadData', { total: 0, rows: [] });
                        for (var i = 0; i < lst.length; i++) {
                            $('#list').datagrid('appendRow', {
                                iLPID: lst[i].iLPID,
                                sLPDM: lst[i].sLPDM,
                                sLPMC: lst[i].sLPMC,
                                fLPJF: lst[i].fLPJF,
                                fLPJE: lst[i].fLPJE,
                                fSL: 0,
                                fKCSL: lst[i].fKCSL,
                                iBJ_WKC: lst[i].iBJ_WKC,
                            });
                        }
                    }

                }
                slxz = data.slxz;
            }
                
        },
    }); 
    RefreshButtonSep();
})


function UnExecClick() {
    ShowYesNoMessage("是否冲正？", function () {
        if (posttosever(SaveData(), vUrl, vUnExecMethod) == true) {
            vProcStatus = cPS_BROWSE;
            ShowDataBase(vJLBH);
            SetControlBaseState();
        }
    });
};

function SetControlState() {
    if ($("#LB_ZXRMC").text() != "") {
        document.getElementById("B_UnExec").disabled = false;
    }
    if ($("#LB_CZRMC").text() != "") {
        document.getElementById("B_UnExec").disabled = true;
    }
    if (vProcStatus == 1) {
        $('#DDL_FFGZ').combobox("setValue", "");
    }
}

function onClick(e, treeId, treeNode) {
    if ($("#HF_BGDDDM").val() != "") {
        ShowYesNoMessage("是否清空？", function () {
            $('#list').datagrid('loadData', { total: 0, rows: [] });
            $("#TB_BGDDMC").val(treeNode.name);
            $("#HF_BGDDDM").val(treeNode.id);
        });
        $("#DDL_FFGZ").combobox("setValue", "");
    }
    else {
        $("#TB_BGDDMC").val(treeNode.name);
        $("#HF_BGDDDM").val(treeNode.id);
        $('#DDL_FFGZ').combobox("setValue", "");
    }
    hideMenu("menuContent");
}

function GetHYKXX() {
    var str = "";
    if ($("#TB_HYKNO").val() != "") {
        str = GetHYXXData(0, $("#TB_HYKNO").val());
    }
    else {
        ShowMessage("请输入卡号", 3);
        $("#list").datagrid("loadData", { total: 0, rows: [] });
        $("#HF_HYKTYPE").val("");
        $("#HF_HYID").val("");
        $("#LB_HYKNAME").text("");
        $("#LB_HYNAME").text("");
        $("#LB_ZJHH").text("");
        $("#LB_WCLJF_OLD").text("");
        return;
    }
    if (str == "null" || str == "") {
        ShowMessage("没有找到卡号", 3);
        $("#TB_HYKNO").val("");
        return;
    }
    var Obj = JSON.parse(str);
    if (Obj.iSTATUS < 0) {
        ShowMessage("无效卡不能参加活动", 3);
        $("#TB_HYKNO").val("");
        return;
    }
    $("#HF_HYKTYPE").val(Obj.iHYKTYPE);
    $("#HF_HYID").val(Obj.iHYID);
    $("#LB_HYKNAME").text(Obj.sHYKNAME);
    $("#LB_HYNAME").text(Obj.sHY_NAME);
    $("#LB_ZJHH").text(Obj.sSFZBH);
    $("#LB_WCLJF_OLD").text(Obj.fWCLJF);
    $("#list").datagrid("loadData", { total: 0, rows: [] });
    FillLPFFGZ($("#DDL_FFGZ"), vGZLX, $("#HF_HYKTYPE").val());


}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.iCZJLBH = $("#TB_JLBH").val();
    if (Obj.iCZJLBH == "")
        Obj.iCZJLBH = "0";
    Obj.iFLGZBH = $('#DDL_FFGZ').combobox('getValue');
    Obj.iHYID = $("#HF_HYID").val();
    Obj.sHYK_NO = $("#TB_HYKNO").val();
    Obj.iHYKTYPE = $("#HF_HYKTYPE").val();
    Obj.sZY = $("#TB_ZY").val();
    Obj.fCLJF = $("#LB_CLJF").text();
    Obj.fWCLJF_OLD = $("#LB_WCLJF_OLD").text();
    Obj.sBGDDDM = $("#HF_BGDDDM").val();
    Obj.iDJLX = vGZLX;
    var lst = new Array();
    lst = $("#list").datagrid("getRows");

    Obj.itemTable = lst;
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#HF_BGDDDM").val(Obj.sBGDDDM);
    $("#TB_BGDDMC").val(Obj.sBGDDMC);
    $("#TB_ZY").val(Obj.sZY);
    $("#HF_HYID").val(Obj.iHYID);
    $("#TB_HYKNO").val(Obj.sHYK_NO);
    GetHYKXX();
    $("#LB_CLJF").text(Obj.fCLJF);
    $("#LB_WCLJF_OLD").text(Obj.fWCLJF_OLD);
    $("#HF_HYKTYPE").val(Obj.iHYKTYPE);
    $("#HF_BJ_CZ").val(Obj.iBJ_CZ);

   
    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);
    $("#LB_CZRMC").text(Obj.sCZRMC);
    $("#HF_CZR").val(Obj.iCZR);
    $("#LB_CZRQ").text(Obj.dCZRQ);

    $('#list').datagrid('loadData', Obj.itemTable, "json");
    $('#list').datagrid("loaded");

    $('#DDL_FFGZ').val(Obj.iFLGZBH);
    window.setTimeout(function () {
        $('#DDL_FFGZ').combobox("setValue", Obj.iFLGZBH);
        $('#DDL_FFGZ').combobox("setText", Obj.sGZMC);
    }, 100);
   



}

function IsValidData() {
    CalculatorsInteger();
    if ($("#HF_LQSL").val() == 0) {
        ShowMessage("总兑换数量不能为0", 3);
        return false;
    }
    if (parseFloat($("#LB_CLJF").text()) > parseFloat($("#LB_WCLJF_OLD").text())) {
        ShowMessage("兑换积分不足", 3);
        return false;
    }
    if ($("#HYID").val() == "" || $("#TB_HYKNO").val() == "") {
        ShowMessage("会员卡号不能为空", 3);
        return false;
    }
    if ($("#HF_BGDDDM").val() == "") {
        ShowMessage("保管地点不能为空", 3);
        return false;
    }
    if (!CheckLPFF("BJ_DC", $("#TB_HYKNO").val(), $('#DDL_FFGZ').combobox('getValue'))) {
        ShowMessage("超过领取次数限制", 3);
        return false;
    }
    if (!CheckLPFF("BJ_SR", $("#TB_HYKNO").val(), $('#DDL_FFGZ').combobox('getValue'))) {
        ShowMessage("该会员生日不在规定范围内", 3);
        return false;
    }
    if (!CheckLPFF("BJ_BK", $("#TB_HYKNO").val(), $('#DDL_FFGZ').combobox('getValue'))) {
        ShowMessage("该会员办卡时间不在规定范围内", 3);
        return false;
    }
    if (slxz == 1 && $("#HF_LQSL").val() > 1) {
        ShowMessage("领取数量超过限制", 3);
        return false;
    }
    if (!BoolKCSL) {
        ShowMessage("领取数量不能大于库存数量", 3);
        return false;
    }


    return true;
}

function CalculatorsInteger() {
    var fCLJF = 0;
    var iLQSL = 0;
    BoolKCSL = true;
    var IdList = $("#list").datagrid("getRows");
    for (var i = 0; i < IdList.length; i++) {
        var rowData = IdList[i];
        fCLJF = parseFloat(fCLJF) + parseFloat(rowData.fSL * rowData.fLPJF);
        iLQSL = parseFloat(iLQSL) + parseFloat(rowData.fSL);
        if (rowData.fSL > rowData.fKCSL && rowData.iBJ_WKC == 0) {
            BoolKCSL = false;
        }
    }
    $("#LB_CLJF").text(fCLJF);
    $("#HF_LQSL").val(iLQSL);
}






function getDate(date) {
    var currentDate = date.getFullYear() + "-";
    if (date.getMonth() < 10) {
        currentDate += "0" + (date.getMonth() + 1) + "-";
    }
    else {
        currentDate += date.getMonth() + 1 + "-";
    }
    if (date.getDate() < 10) {
        currentDate += "0" + (date.getDate());
    }
    else {
        currentDate += date.getDate();
    }
    return currentDate;

};

function CheckLPFF(conditionName, hykno, ruleNumber) {
    var checkdata = CheckLPFFResult(conditionName, hykno, ruleNumber);
    if (checkdata) {
        var Obj = JSON.parse(checkdata);
        return Obj.bCHECKRESULT;
    }
}

//function cleardata()
//{
//    $("#list").datagrid("loadData", {});
//    $("#TB_BGDDMC").val("");
//    $("#HF_BGDDDM").val("");
//    $("#DDL_FFGZ").combobox("setValue", "");

//}
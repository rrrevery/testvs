vUrl = "../LPGL.ashx";
zURL = "LPGL_LPFFCZ.ashx";
var HYKNO = GetUrlParam("HYKNO");
var rowNumer = 0;
var CZJLBH = 0;
var BoolKCSL = true;
function InitGrid() {
    vColumnNames = ["LPID", "礼品代码", "礼品名称", "礼品单价", "礼品积分", "库存数量", "兑换数量", "不限制库存"];
    vColumnModel = [
          { name: "iLPID", hidden: true },
          { name: "sLPDM", width: 60, },
          { name: "sLPMC", width: 90 },
          { name: "fLPDJ", width: 60 },
          { name: "fLPJF", width: 60 },
          { name: "fKCSL", width: 60 },
          { name: "fSL", width: 60, editor: 'text' },
          { name: "iBJ_WKC", hidden: true },
    ];
};
$(document).ready(function () {
    FillBGDDTree("TreeBGDD", "TB_BGDDMC");
    $("#B_Exec").hide();
    $("#B_Delete").hide();
    $("#B_Update").hide();
    $("#B_UnExec").show();
    $("#B_UnExec").text("冲正");
    vUnExecMethod = "Rollback";

    val = document.getElementById("status-bar").innerHTML;
    val += "<div class='bffld' id='CZR'><div class='bffld_left' id='czr1'>冲正人</div><div class='bffld_right'><label id='LB_CZRMC' class='djr'></label><label id='LB_CZRQ' class='djsj'></label></div></div>";
    val += "<input id='HF_CZR' type='hidden'/>";

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

    $("#AddItem").click(function () {
        if ($("#HF_BGDDDM").val() == "") {
            ShowMessage("请先选择保管地点", 3);
            return;
        }
        if ($("#HF_HYID").val() == "") {
            ShowMessage("请刷卡", 3);
            return;
        }
        var DataArry = new Object();
        DataArry["iDJLX"] = 1;  //是否要显示库存数量 0不显示;1显示
        DataArry["sBGDDDM"] = $("#HF_BGDDDM").val();
        DataArry["fWCLJF"] = $("#LB_WCLJF_OLD").text();
        SelectLP('list', DataArry, 'iLPID');
    });

    $("#DelItem").click(function () {
        DeleteRows("list");
        CalculatorsInteger();
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
}

function TreeNodeClickCustom(e, treeId, treeNode) {
    if ($("#HF_BGDDDM").val() != "") {
        ShowYesNoMessage("是否清空数据？", function () {
            $('#list').datagrid('loadData', { total: 0, rows: [] });
            $("#TB_BGDDMC").val(treeNode.name);
            $("#HF_BGDDDM").val(treeNode.id);
        });
    }
    else {
        $("#TB_BGDDMC").val(treeNode.name);
        $("#HF_BGDDDM").val(treeNode.id);
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

}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.iCZJLBH = $("#TB_JLBH").val();
    if (Obj.iCZJLBH == "")
        Obj.iCZJLBH = "0";

    Obj.iHYID = $("#HF_HYID").val();
    Obj.sHYK_NO = $("#TB_HYKNO").val();
    Obj.iHYKTYPE = $("#HF_HYKTYPE").val();
    Obj.sZY = $("#TB_ZY").val();
    Obj.fCLJF = $("#LB_CLJF").text();
    Obj.fWCLJF_OLD = $("#LB_WCLJF_OLD").text();
    Obj.sBGDDDM = $("#HF_BGDDDM").val();

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

    $('#list').datagrid('loadData', Obj.itemTable, "json");
    $('#list').datagrid("loaded");

    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);
    $("#LB_CZRMC").text(Obj.sCZRMC);
    $("#HF_CZR").val(Obj.iCZR);
    $("#LB_CZRQ").text(Obj.dCZRQ);

}

function IsValidData() {
    CalculatorsInteger();
    if ($("#HF_LQSL").val() == 0) {
        ShowMessage("总兑换数量不能为0", 3);
        return false;
    }
    if (isNaN($("#HF_LQSL").val())) {
        ShowMessage("兑换数量不能为空", 3);
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

function CheckNumber(fDHSL) {
    var BoolVailed = true;
    var regstring = "^[1-9]d*$";
    var ipReg = new RegExp(regstring);
    if (ipReg.test(fDHSL) == false) {
        BoolVailed = false;
    }
    return BoolVailed;
}

function MoseDialogCustomerReturn(dialogName, lstData, showField) {
    if (dialogName == "DialogSK")
        GetHYKXX();

};

function myPreview() {
    //LODOP = getLodop(document.getElementById("LODOP_OB"), document.getElementById("LODOP_EM"));
    var WCLJF_OLD = parseFloat($("#LB_WCLJF_OLD").text());
    var CLJF = parseFloat($("#LB_CLJF").text());
    var Balance = parseFloat(WCLJF_OLD - CLJF);
    date = new Date();
    date1 = new Date(date.getFullYear(), date.getMonth(), date.getDate());
    var userAgent = window.navigator.userAgent;
    if (userAgent.indexOf("Firefox") >= 0) {
        LODOP = document.getElementById("LODOP_EM");
    }
    else {
        LODOP = document.getElementById("LODOP_OB");
    }
    LODOP.SET_LICENSES("北京长京益康信息科技有限公司", "653556581728688787994958093190", "", "");
    LODOP.PRINT_INIT("打印任务名");
    // LODOP.PRINT_INITA(0, 0, 330, 600, "打印任务名");
    LODOP.ADD_PRINT_TEXT(53, 78, 146, 25, "华地国际积分兑换确认单");
    LODOP.SET_PRINT_STYLEA(0, "FontColor", "#800080");
    LODOP.ADD_PRINT_TEXT(92, 15, 70, 20, "兑换日期：");
    LODOP.SET_PRINT_STYLEA(0, "FontColor", "#800080");
    LODOP.ADD_PRINT_TEXT(109, 15, 70, 20, "持卡顾客：");
    LODOP.SET_PRINT_STYLEA(0, "FontColor", "#800080");
    LODOP.ADD_PRINT_TEXT(143, 15, 70, 20, "会员卡号：");
    LODOP.SET_PRINT_STYLEA(0, "FontColor", "#800080");
    LODOP.ADD_PRINT_TEXT(126, 15, 70, 20, "身份证号：");
    LODOP.SET_PRINT_STYLEA(0, "FontColor", "#800080");
    LODOP.ADD_PRINT_TEXT(160, 15, 70, 20, "换购名称：");
    var listCount = $("#list").getGridParam("reccount");
    //  listCount = 2;
    for (var i = 0; i < listCount; i++) {
        var rowData = $("#list").getRowData(i);
        LODOP.ADD_PRINT_TEXT(160 + (i + 1) * 16, 84, 160, 20, "" + rowData.sLPMC + "* " + rowData.fSL + "");
    }
    LODOP.SET_PRINT_STYLEA(0, "FontColor", "#800080");
    LODOP.ADD_PRINT_TEXT(176 + listCount * 26, 15, 69, 20, "兑换积分：");
    LODOP.SET_PRINT_STYLEA(0, "FontColor", "#800080");
    LODOP.ADD_PRINT_TEXT(192 + listCount * 26, 15, 69, 20, "兑换金额：");
    LODOP.SET_PRINT_STYLEA(0, "FontColor", "#800080");
    LODOP.ADD_PRINT_TEXT(242 + listCount * 26, 15, 69, 20, "剩余积分：");
    LODOP.SET_PRINT_STYLEA(0, "FontColor", "#800080");
    LODOP.ADD_PRINT_TEXT(258 + listCount * 26, 15, 69, 20, "剩余金额：");
    LODOP.SET_PRINT_STYLEA(0, "FontColor", "#800080");
    LODOP.ADD_PRINT_TEXT(274 + listCount * 26, 15, 69, 20, "开票人  ：");
    LODOP.SET_PRINT_STYLEA(0, "FontColor", "#800080");
    LODOP.ADD_PRINT_TEXT(291 + listCount * 26, 15, 69, 20, "顾客签收：");
    LODOP.SET_PRINT_STYLEA(0, "FontColor", "#800080");
    LODOP.ADD_PRINT_TEXT(326 + listCount * 26, 13, 194, 20, "注：电子券自兑换之日起90天");

    LODOP.SET_PRINT_STYLEA(0, "FontColor", "#800080");
    LODOP.ADD_PRINT_TEXT(92, 84, 100, 20, "" + getDate(date1) + "");
    LODOP.SET_PRINT_STYLEA(0, "FontColor", "#800080");
    LODOP.ADD_PRINT_TEXT(109, 84, 100, 20, "" + $("#LB_HYNAME").text() + "");
    LODOP.SET_PRINT_STYLEA(0, "FontColor", "#800080");
    var sZJHM = "";
    if ($("#LB_ZJHH").text() != "") {
        sZJHM = $("#LB_ZJHH").text();
        sZJHM = sZJHM.substr(0, 6) + "********" + sZJHM.substr(sZJHM.length - 4, sZJHM.length);
    }
    LODOP.ADD_PRINT_TEXT(126, 84, 130, 20, "" + sZJHM + "");
    LODOP.ADD_PRINT_TEXT(143, 84, 100, 20, "" + $("#TB_HYKNO").val() + "");
    LODOP.SET_PRINT_STYLEA(0, "FontColor", "#800080");
    LODOP.ADD_PRINT_TEXT(160, 84, 100, 20, "礼品");



    LODOP.SET_PRINT_STYLEA(0, "FontColor", "#800080");
    LODOP.ADD_PRINT_TEXT(176 + listCount * 26, 84, 100, 20, "" + $("#LB_CLJF").text() + "");
    LODOP.SET_PRINT_STYLEA(0, "FontColor", "#800080");
    LODOP.ADD_PRINT_TEXT(192 + listCount * 26, 84, 100, 20, "0");
    LODOP.SET_PRINT_STYLEA(0, "FontColor", "#800080");
    LODOP.ADD_PRINT_TEXT(242 + listCount * 26, 84, 100, 20, "" + Balance + "");
    LODOP.SET_PRINT_STYLEA(0, "FontColor", "#800080");
    LODOP.ADD_PRINT_TEXT(258 + listCount * 26, 84, 100, 20, "0");
    LODOP.SET_PRINT_STYLEA(0, "FontColor", "#800080");
    LODOP.ADD_PRINT_TEXT(274 + listCount * 26, 84, 100, 20, "" + sDJRMC + "");
    LODOP.SET_PRINT_STYLEA(0, "FontColor", "#800080");
    LODOP.PREVIEW();
    //  LODOP.PRINT_DESIGN();
};


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
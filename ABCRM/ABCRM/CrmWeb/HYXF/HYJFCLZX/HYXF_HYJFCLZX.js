vUrl = "../HYXF.ashx";
vJFLX = GetUrlParam("JFLX");
vUnExecMethod = "Rollback";//取消审核的方法名，默认是UnExec，可选Rollback
var HYKNO = GetUrlParam("HYKNO");
function InitGrid() {
    vColumnNames = ['序号', '积分下限', '积分上限', '积分比例', '处理积分', '兑换金额'];
    vColumnModel = [
            { name: "iXH", width: 90, },
            { name: "fJFXX", width: 90, },
            { name: "fJFSX", width: 90, },
            { name: "fFLBL", width: 90, },
            { name: "fCLJF", width: 90, },
            { name: "fFQJE", width: 90, },
    ];
}

$(document).ready(function () {
    $("#B_Exec").hide();
    $("#B_Delete").hide();
    $("#B_Update").hide();
    $("#B_UnExec").show();
    $("#B_UnExec").text("冲正");
    $("[name='BJ_MD']").prop("disabled", true);
    FillBGDDTree("TreeBGDD", "TB_BGDDMC", "menuContent");

    RefreshButtonSep();
    val = document.getElementById("status-bar").innerHTML;
    val += "<div class='bfrow'><div class='bffld' id='CZR'><div class='bffld_left'>冲正人</div><div class='bffld_right'><label id='LB_CZRMC' runat='server' style='text-align:left'></label></div><input id='HF_CZR' type='hidden'/></div></div></div>";
    val += "<div class='bfrow'><div class='bffld' id='CZRQ'><div class='bffld_left'>冲正时间</div><div class='bffld_right'><label id='LB_CZRQ' runat='server' style='text-align:left'></label></div></div></div></div>";
    document.getElementById("status-bar").innerHTML = val;


    //document.getElementById("BTN_PRINT").disabled = true;
    $("#TB_HYKH").blur(function () {
        GetHYXX();
    })
    if (HYKNO != "") {
        $("#TB_HYKH").val(HYKNO);
        GetHYXX();
        SetControlBaseState();
        $("#TB_HYKH").attr("readonly", "readonly");
    }
    $("#TB_MDMC").click(function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", true);
    });

    $("#DDL_FQGZ").change(function () {
        //GetFQGZMX($("#DDL_FQGZ").val());
        var StrFLGZ = GetFLGZMXData($("#DDL_FQGZ").val());
        if (StrFLGZ) {
            ShowRuleData(StrFLGZ);
        }
        CalculatorMoneyIntegral();
    })

    $("#TB_CLJF").blur(function () {
        if ($("#HF_HYKTYPE").val() != "") {
            if ($("#HF_YHQID").val() != "" && $("#DDL_FQGZ").val() != "") {
                if ($("TB_CLJF").val() != "") {
                    if (parseFloat($("#TB_CLJF").val()) > parseFloat($("#LB_HYJF").text())) {
                        ShowMessage("处理积分不能大于会员积分", 3);
                    }
                    else {
                        CalculatorMoneyIntegral();
                    }
                }
            }
        }
    })

    $("#TB_CLJF").bind('keypress', function (event) {
        if (event.keyCode == "13") {
            if ($("#HF_HYKTYPE").val() != "") {
                if ($("#HF_YHQID").val() != "" && $("#DDL_FQGZ").val() != "") {
                    if ($("TB_CLJF").val() != "") {
                        if (parseFloat($("#TB_CLJF").val()) > parseFloat($("#LB_HYJF").text())) {
                            ShowMessage("处理积分不能大于会员积分", 3);
                        }
                        else {
                            CalculatorMoneyIntegral();
                        }
                    }
                }
            }
        }
    });

    $("#TB_HYKH").bind('keypress', function (event) {
        if (event.keyCode == "13") {
            GetHYXX();
        }
    });

    $("#btn_HYKHM").click(function () {
        var conData = new Object();
        conData.iBJ_KCK = 0;
        SelectSK("TB_HYKH", "HF_HYID", "", conData);
    });
})

function InsertClickCustom()
{
    $("#DDL_FQGZ").empty();
}

function IsValidData() {
    if ($("#HF_BGDDDM").val() == "") {
        ShowMessage("请选择保管地点", 3);
        return false;
    }
    if ($("#HF_FQJE").val() == 0 && $("#TB_CLJF").val() == 0)
    {
        ShowMessage("兑换金额不能为0", 3);
        return false;
    }
    return true;
}
function SetControlState() {
    $("[name='BJ_MD']").prop("disabled", true);
    if ($("#TB_SHRMC").text() != "") {
        document.getElementById("B_UnExec").disabled = false;
        document.getElementById("B_Exec").disabled = true;
    }
    if ($("#LB_CZRMC").text() != "") {
        document.getElementById("B_Exec").disabled = true;
        document.getElementById("B_UnExec").disabled = true;
    }
};

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.iHBFS = 1;
    Obj.iHYID = $("#HF_HYID").val();
    Obj.iMDID = $("#HF_MDID").val();
    Obj.fWCLJF_OLD = $("#LB_HYJF").text();
    Obj.iYHQID = $("#HF_YHQID").val();
    Obj.iFLGZBH = $("#DDL_FQGZ")[0].value;
    Obj.iFQGZID = $("#DDL_FQGZ")[0].value;
    Obj.fCLJF = $("#TB_CLJF").val();
    Obj.sHYK_NO = $("#TB_HYKH").val();
    Obj.iHYID = $("#HF_HYID").val();
    Obj.dYHQJSRQ = $("#TB_YHQJSRQ").val();
    Obj.sBGDDDM = $("#HF_BGDDDM").val();
    Obj.iBJ_WCLJF = vJFLX;
    Obj.fFQJE = $("#HF_FQJE").val();
    var lst = new Array();
    lst = $("#list").datagrid("getRows");

    Obj.iBJ_MD = ($("[name='BJ_MD']:checked").val() != undefined) ? $("[name='BJ_MD']:checked").val() : "-1";
    if (Obj.iBJ_MD == 1) {
        if ($("#HF_MDDM").val() != "") {
            Obj.sMDDM = $("#HF_MDDM").val();
        }
    }

    Obj.itemTable = lst;
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;

    return Obj;
};
function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#TB_BGDDMC").val(Obj.sBGDDMC);
    $("#HF_BGDDDM").val(Obj.sBGDDDM);
    $("#TB_HYKH").val(Obj.sHYK_NO);
    $("#DDL_FQGZ").val(Obj.iFQGZID);

    $("#HF_BJ_CZ").val(Obj.iBJ_CZ);
    $("#LB_HYJF").text(Obj.fWCLJF_OLD);
    $("#TB_MDMC").val(Obj.sMDMC);
    $("#HF_MDID").val(Obj.iMDID);
    $("#TB_YHQMC").val(Obj.sYHQMC);
    $("#HF_YHQID").val(Obj.iYHQID);
    $("#TB_KSRQ").val(Obj.dKSRQ);
    $("#TB_JSRQ").val(Obj.dJSRQ);
    $("#TB_YXQSL").val(Obj.iYHQSL == 0 ? "" : Obj.iYHQSL);
    $("#TB_YHQJSRQ").val(Obj.dYHQJSRQ == "" ? "" : Obj.dYHQJSRQ);
    //GetHYXXShow();
    GetHYXX();
    if ($("#HF_MDID").val() != "") {
        if ($("#HF_HYKTYPE").val() != "" && $("#HF_YHQID").val() != "") {
            FillFLGZ("DDL_FQGZ", $("#HF_HYKTYPE").val(), $("#HF_HYID").val());
        }
    }
    $("#DDL_FQGZ").val(Obj.iFQGZID);
    $("#HF_FQJE").val(Obj.fFQJE);
    $("#LB_ZJE").text(Obj.fFQJE);
    $("#TB_CLJF").val(Obj.fCLJF);
    $("#HF_MDDM").val(Obj.sMDDM);
    $("[name='BJ_MD'][value='" + Obj.iBJ_MD + "']").prop("checked", true);

    $("#list").datagrid("loadData", Obj.itemTable, 'json');
    $("#list").datagrid("loaded");

    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sSHRMC);
    $("#HF_ZXR").val(Obj.iSHR);
    $("#LB_ZXRQ").text(Obj.dSHRQ);
    $("#LB_CZRMC").text(Obj.sCZRMC);
    $("#HF_CZR").val(Obj.iCZR);
    $("#LB_CZRQ").text(Obj.dCZRQ);
}

function UnExecClick()
{
    ShowYesNoMessage("是否冲正？", function ()
    {
        if (posttosever(SaveData(), vUrl, vUnExecMethod) == true)
        {
            vProcStatus = cPS_BROWSE;
            ShowDataBase(vJLBH);
            SetControlBaseState();
        }
    });
};

function CheckSearchCondition() {
    $("#TB_KSRQ").val("");
    $("#TB_JSRQ").val("");
    $("#TB_YXQSL").val("");
    $("#TB_YHQJSRQ").val("");
    $("#HF_MDDM").val("");
    $("#list").datagrid("loadData", { total: 0, rows: [] });
    if ($("#HF_HYKTYPE").val() != "") {
        $("#DDL_FQGZ").empty();
        FillFLGZ("DDL_FQGZ", $("#HF_HYKTYPE").val(), $("#HF_HYID").val());
        if ($("#DDL_FQGZ").val()) {
            var StrFLGZ = GetFLGZMXData($("#DDL_FQGZ").val());
            if (StrFLGZ) {
                ShowRuleData(StrFLGZ);
            }
        }
        else {
            ShowMessage("规则不存在", 3);
        }
        //GetFQGZMX($("#DDL_FQGZ").val());
        if ($("#TB_CLJF").val() != "") {
            CalculatorMoneyIntegral();
        }
    }
}


function onClick(e, treeId, treeNode) {
    $("#TB_BGDDMC").val(treeNode.name);
    $("#HF_BGDDDM").val(treeNode.id);
    $("#HF_MDID").val(treeNode.mdid);
    hideMenu("menuContent");
    //  CheckSearchCondition();
}

function GetHYXX() {
    if ($("#TB_HYKH").val() != "") {
        var str = GetHYXXData(0, $("#TB_HYKH").val());
        if (str == "null" || str == "") {
            ShowMessage("没有找到卡号", 3);
            return;
        }
        var Obj = JSON.parse(str);
        $("#HF_HYID").val(Obj.iHYID);
        $("#HF_HYKTYPE").val(Obj.iHYKTYPE);
        $("#LB_HYNAME").text(Obj.sHY_NAME);
        $("#TB_HYKNAME").text(Obj.sHYKNAME);
        $("#LB_ZJHM").text(Obj.sSFZBH);
        $("#LB_HYJF").text(Obj.fWCLJF);
        //if (Obj.iHYID != "") {
        //    var gxshjf;
        //    if (vJFLX == 1) {
        //        gxshjf = GetGXSHJF(Obj.iHYID, iDJR);
        //    }
        //    if (vJFLX == 0) {
        //        gxshjf = GetGXSHJF(Obj.iHYID, iDJR, 0);
        //    }
        //    if (gxshjf != "") {
        //        $("#LB_HYJF").text(gxshjf);
        //        $("#TB_CLJF").val(gxshjf);
        //    }
        //}
        if (Obj.iHYKTYPE != 0) {
            CheckSearchCondition();
        }
    }
    else {
        ShowMessage("请输入卡号", 3);
        PageDate_Clear();
        $("#list").datagrid("loadData", { total: 0, rows: [] });
    }




}

//function GetHYXXShow() {
//    if ($("#TB_HYKH").val() != "") {
//        var str = GetHYXXData(0, $("#TB_HYKH").val());
//        if (str == "null" || str == "") {
//            ShowMessage("没有找到卡号", 3);
//            return;
//        }
//        var Obj = JSON.parse(str);
//        $("#HF_HYID").val(Obj.iHYID);
//        $("#HF_HYKTYPE").val(Obj.iHYKTYPE);
//        $("#LB_HYNAME").text(Obj.sHY_NAME);
//        $("#TB_HYKNAME").text(Obj.sHYKNAME);
//        $("#LB_ZJHM").text(Obj.sSFZBH);
//    }
//}

function GetHYJF() {
    var arrayObj = new Array();
    $("#list").jqGrid('setGridParam', {
        url: "../../CrmLib/CrmLib.ashx?func=GetHYJFXX&iHYID=" + $("#HF_HYID").val() + "&sHYK_NO=" + $("#TB_HYKH").val() + "&iHYKTYPE=" + $("#HF_HYKTYPE").val() + "&iJFLX=" + vJFLX + "&MDID=" + $("#HF_MDID").val(),
        postData: { 'afterFirst': JSON.stringify(arrayObj) },
        page: 1
    }).trigger("reloadGrid");

}

//function FillFLGZ(selectName) {
//    var obj = new Object();
//    obj.iHYKTYPE = $("#HF_HYKTYPE").val();
//    obj.iHYID = $("#HF_HYID").val();
//    $.ajax({
//        type: 'post',
//        url: "../../CrmLib/CrmLib.ashx?func=FillFQGZ&JFLX=" + vJFLX,
//        dataType: "json",
//        async: false,
//        data: { json: JSON.stringify(obj), titles: 'cecece' },
//        success: function (data) {
//            for (i = 0; i < data.length; i++) {
//                selectName.append("<option value='" + data[i].iFLGZBH + "' >" + data[i].iFLGZBH + "  " + data[i].sGZMC + "</option>");
//            }
//        },
//        error: function (data) {
//            ;
//        }
//    });
//}

//function GetFQGZMX(FQGZBH) {
//    $.ajax({
//        type: 'post',
//        url: "HYXF_HYJFFLZX.ashx?func=GetFQGZMX&iFQGZBH=" + FQGZBH,
//        dataType: "json",
//        async: false,
//        success: function (data) {
//            ShowRuleData(data);
//        },
//        error: function (data) {
//            art.dialog({ lock: true, content: "没有相应规则", time: 2 });
//        }
//    });

//}

function ShowRuleData(data) {
    var Obj = JSON.parse(data);
    $("#TB_KSRQ").val(Obj.dKSRQ);
    $("#TB_JSRQ").val(Obj.dJSRQ);
    $("#TB_YXQSL").val(Obj.iYHQSL == 0 ? "" : Obj.iYHQSL);
    $("#TB_YHQJSRQ").val(Obj.dYHQJSRQ == "" ? "" : Obj.dYHQJSRQ);
    $("#TB_YHQMC").val(Obj.sYHQMC);
    $("#HF_YHQID").val(Obj.iYHQID);
    $("[name='BJ_MD'][value='" + Obj.iBJ_MD + "']").prop("checked", true);
    $("#HF_MDDM").val(Obj.sMDDM);

    $("#list").datagrid("loadData", { total: 0, rows: [] });
    $("#list").datagrid("loadData", Obj.itemTable, "json");
    $("#list").datagrid("loaded");

}

function CalculatorMoneyIntegral() {
    var Obj = new Object();
    Obj.iJLBH = $("#DDL_FQGZ")[0].value;
    var lst = new Array();
    lst = $("#list").datagrid("getRows");
    //for (var i = 0; i < $("#list").getGridParam("reccount") ; i++) {
    //    $("#list").setRowData(i, { fCLJF: 0, fFQJE: 0 });
    //    var rowData = $("#list").jqGrid("getRowData", i);
    //    lst.push(rowData);
    //}
    Obj.itemTable = lst;
    if (lst.length > 0 && Obj.iJLBH != 0) {
        var data = GetJFFQJFJE(Obj, $("#TB_CLJF").val());
        ShowMoneyIntegral(data);        
    }
}

function ShowMoneyIntegral(data)
{
    var rows = $("#list").datagrid("getRows");
    for (var i = 0 ; i < rows.length; i++)
    {      
        $("#list").datagrid("updateRow", {
            index: i, row: { fCLJF: 0, fFQJE: 0 }
        });
    }
    // var jffqitem = eval('(' + data + ')');
    var Obj = JSON.parse(data);

    $("#TB_CLJF").val(Obj.fZCLJF);
    $("#HF_FQJE").val(Obj.fZFQJE);
    $("#LB_ZJE").text(Obj.fZFQJE);
    for (var j = 0; j < Obj.itemTable1.length ; j++) {
        $("#list").datagrid("updateRow", {
            index: Obj.itemTable1[j].iGridId, row: { fCLJF: Obj.itemTable1[j].fCLJF, fFQJE: Obj.itemTable1[j].fFQJE }
        });
        //$("#list").setRowData(jffqitem.itemTable1[j].iGridId, { fCLJF: jffqitem.itemTable1[j].fCLJF, fFQJE: jffqitem.itemTable1[j].fFQJE });

    }
}

function myPreview() {
    //LODOP = getLodop(document.getElementById("LODOP_OB"), document.getElementById("LODOP_EM"));
    var WCLJF_OLD = parseFloat($("#LB_HYJF").text());
    var CLJF = parseFloat($("#TB_CLJF").val());
    var Balance = parseFloat(WCLJF_OLD - CLJF);
    //var Date = new Date();
    date = new Date();
    date1 = new Date(date.getFullYear(), date.getMonth(), date.getDate());
    var userAgent = window.navigator.userAgent;
    if (userAgent.indexOf("Firefox") >= 0) {
        LODOP = document.getElementById("LODOP_EM");
    }
    else {
        LODOP = document.getElementById("LODOP_OB");
    }
    //      
    LODOP.SET_LICENSES("北京长京益康信息科技有限公司", "653556581728688787994958093190", "", "");

    LODOP.PRINT_INIT("打印任务名");
    // LODOP.PRINT_INITA(0, 0, 86, 54, "打印任务名");
    LODOP.ADD_PRINT_TEXT(53, 84, 146, 25, "华地国际积分兑换确认单");
    LODOP.SET_PRINT_STYLEA(0, "FontColor", "#800080");
    LODOP.ADD_PRINT_TEXT(92, 15, 69, 20, "兑换日期：");
    LODOP.SET_PRINT_STYLEA(0, "FontColor", "#800080");
    LODOP.ADD_PRINT_TEXT(109, 15, 69, 20, "持卡顾客：");
    LODOP.SET_PRINT_STYLEA(0, "FontColor", "#800080");
    LODOP.ADD_PRINT_TEXT(143, 15, 69, 20, "会员卡号：");
    LODOP.SET_PRINT_STYLEA(0, "FontColor", "#800080");
    LODOP.ADD_PRINT_TEXT(126, 15, 69, 20, "身份证号：");
    LODOP.SET_PRINT_STYLEA(0, "FontColor", "#800080");
    LODOP.ADD_PRINT_TEXT(160, 15, 69, 20, "换购名称：");
    LODOP.SET_PRINT_STYLEA(0, "FontColor", "#800080");
    LODOP.ADD_PRINT_TEXT(176, 15, 69, 20, "兑换积分：");
    LODOP.SET_PRINT_STYLEA(0, "FontColor", "#800080");
    LODOP.ADD_PRINT_TEXT(192, 15, 69, 20, "兑换金额：");
    LODOP.SET_PRINT_STYLEA(0, "FontColor", "#800080");
    LODOP.ADD_PRINT_TEXT(242, 15, 69, 20, "剩余积分：");
    LODOP.SET_PRINT_STYLEA(0, "FontColor", "#800080");
    LODOP.ADD_PRINT_TEXT(258, 15, 69, 20, "剩余金额：");
    LODOP.SET_PRINT_STYLEA(0, "FontColor", "#800080");
    LODOP.ADD_PRINT_TEXT(274, 15, 69, 20, "开票人  ：");
    LODOP.SET_PRINT_STYLEA(0, "FontColor", "#800080");
    LODOP.ADD_PRINT_TEXT(291, 15, 69, 20, "顾客签收：");
    LODOP.SET_PRINT_STYLEA(0, "FontColor", "#800080");
    LODOP.ADD_PRINT_TEXT(326, 13, 194, 20, "注：电子券自兑换之日起90天");
    LODOP.SET_PRINT_STYLEA(0, "FontColor", "#800080");
    LODOP.ADD_PRINT_TEXT(92, 84, 100, 20, "" + getDate(date1) + "");
    LODOP.SET_PRINT_STYLEA(0, "FontColor", "#800080");
    LODOP.ADD_PRINT_TEXT(109, 84, 100, 20, "" + $("#LB_HYNAME").text() + "");
    LODOP.SET_PRINT_STYLEA(0, "FontColor", "#800080");
    var sZJHM = "";
    if ($("#LB_ZJHM").text() != "") {
        sZJHM = $("#LB_ZJHM").text();
        sZJHM = sZJHM.substr(0, 6) + "********" + sZJHM.substr(sZJHM.length - 4, sZJHM.length);
    }
    LODOP.ADD_PRINT_TEXT(126, 84, 130, 20, "" + sZJHM + "");
    LODOP.ADD_PRINT_TEXT(143, 84, 100, 20, "" + $("#TB_HYKH").val() + "");
    LODOP.SET_PRINT_STYLEA(0, "FontColor", "#800080");
    LODOP.ADD_PRINT_TEXT(160, 84, 100, 20, "电子券");
    LODOP.SET_PRINT_STYLEA(0, "FontColor", "#800080");
    LODOP.ADD_PRINT_TEXT(176, 84, 100, 20, "" + $("#TB_CLJF").val() + "");
    LODOP.SET_PRINT_STYLEA(0, "FontColor", "#800080");
    LODOP.ADD_PRINT_TEXT(192, 84, 100, 20, "" + $("#HF_FQJE").val() + "");
    LODOP.SET_PRINT_STYLEA(0, "FontColor", "#800080");
    LODOP.ADD_PRINT_TEXT(242, 84, 100, 20, "" + Balance + "");
    LODOP.SET_PRINT_STYLEA(0, "FontColor", "#800080");
    LODOP.ADD_PRINT_TEXT(258, 84, 100, 20, "" + $("#HF_FQJE").val() + "");
    LODOP.SET_PRINT_STYLEA(0, "FontColor", "#800080");
    LODOP.ADD_PRINT_TEXT(274, 84, 100, 20, "" + sDJRMC + "");
    LODOP.SET_PRINT_STYLEA(0, "FontColor", "#800080");
    LODOP.PREVIEW();
    // LODOP.PRINT_DESIGN();
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

function GetFLJE(GZBH, CLJF) {
    sjson = "{'iFLGZBH':'" + GZBH + "','fCLJF':'" + CLJF + "'}";
    result = "";
    $.ajax({
        type: 'post',
        url: "../../CrmLib/CrmLib.ashx?func=GetJFFLJE",
        dataType: "json",
        async: false,
        data: { json: JSON.stringify(sjson), titles: 'cecece' },
        success: function (data) {
            result = data;
        },
        error: function (data) {
            result = "";
        }
    });
    return result;
}
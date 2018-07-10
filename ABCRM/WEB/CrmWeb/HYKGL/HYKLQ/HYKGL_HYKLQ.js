vDJLX = GetUrlParam("djlx");
vCZK = GetUrlParam("czk");
vUrl = vCZK == "0" ? "../HYKGL.ashx" : "../../MZKGL/MZKGL.ashx";
vDBConnName = vCZK == "0" ? "CRMDB" : "CRMDBMZK";
var vKLXColumnNames;
var vKLXColumnModel;
var sqsl = 0;
var txsl = 0;
var sksl = 0;
function SetControlState() {
    //if (($("#HF_SQD").val() == 0 || $("#HF_SQD").val() == "") && vProcStatus != cPS_BROWSE) {
    //    document.getElementById("TB_BGDDMC_BR").disabled = false;
    //}
    //else {
    //    document.getElementById("TB_BGDDMC_BR").disabled = true;

    //}

}

function IsValidData() {
    if ($("#HF_LQR").val() == "") {
        ShowMessage("请选择领取人", 3);
        return false;
    }

    if ($("#list").datagrid("getRows").length == 0) {
        ShowMessage("没有选中的卡", 3);
        return false;
    }
    if ($("#HF_BGDDDM_BC").val() == $("#HF_BGDDDM_BR").val()) {
        ShowMessage("拨出拨入地点不能相同", 3);
        return false;
    }
    //if ($("#HF_SQD").val() != "") {
    //    var rows1 = $('#list').datagrid('getRows')//获取当前的数据行

    //    var rows2 = $('#list_sqd').datagrid('getRows')//获取当前的数据行
    //    for (var i = 0; i < rows2.length; i++) {
    //        var klxsqsl = rows2[i].iSKSL;
    //        var klxtxsl = 0;
    //        for (var j = 0; j < rows1.length; j++) {
    //            if (rows1[j].iHYKTYPE == rows2[i].iHYKTYPE)
    //                klxtxsl += parseInt(rows1[j].iSKSL);

    //        }
    //        if (klxsqsl != klxtxsl) {
    //            ShowMessage("卡类型为" + rows2[i].sHYKNAME + "的卡数量与申请单数量不符");
    //            return false;
    //        }

    //    }
    //}
    return true;
}

function compute(lisName, colName) {
    var rows = $('#' + lisName + '').datagrid('getRows')//获取当前的数据行
    var total = 0;
    for (var i = 0; i < rows.length; i++) {
        total += rows[i]['' + colName + ''];
    }
    return total;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sBGDDDM_BC = $("#HF_BGDDDM_BC").val();
    Obj.sBGDDDM_BR = $("#HF_BGDDDM_BR").val();
    Obj.iLQR = $("#HF_LQR").val();
    Obj.sLQRMC = $("#TB_LQRMC").val();
    Obj.iBJ_CZK = vCZK;
    Obj.iHYKSL = compute("list", "iSKSL");//$("#list").getCol('iSKSL', false, 'sum');
    Obj.iDJLX = vDJLX;
    if (parseInt(Obj.iDJLX) == 1 && parseInt(Obj.iBJ_CZK) == 1) {
        Obj.sDBConnName = "CRMDBMZK";
    }
    if (parseInt(Obj.iDJLX) == 2 && parseInt(Obj.iBJ_CZK) == 1) {
        Obj.sDBConnName = "CRMDBMZK";
    }
    if ($("#HF_SQD").val() != "")
        Obj.iSQDJH = $("#HF_SQD").val();
    var lst = new Array();
    lst = $("#list").datagrid("getData").rows;

    Obj.kditemTable = lst;
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    return Obj;
}

function InitGrid() {
    vColumnNames = ['开始卡号', '结束卡号', 'iHYKTYPE', '卡类型', '数量', '面值金额'];
    vColumnModel = [
            { name: 'sCZKHM_BEGIN', width: 100, },
            { name: 'sCZKHM_END', width: 100,  },
            { name: 'iHYKTYPE', hidden: true, },
            { name: 'sHYKNAME', width: 80, },
            { name: 'iSKSL', width: 50, align: "right", },
            { name: 'fMZJE', width: 80, align: 'right', hidden: vCZK == "1" ? false : true },
    ];
    //vKLXColumnNames = ['iHYKTYPE', '卡类型', '申请数量', ];
    //vKLXColumnModel = [
    //     { name: 'iHYKTYPE', hidden: true },
    //     { name: 'sHYKNAME', width: 100 },
    //     { name: 'iSKSL', width: 100, align: "right", },

    //];
};

$(document).ready(function () {
    BFButtonClick("TB_SQD", function () {
        SelectSQD("TB_SQD", "HF_SQD", "zHF_SQD", true, vCZK);
    });

    $("#sqd").hide();
    $("#sqdxx").hide();
    $("#kdxx").css("width", "100%");


    FillHYKTYPETree("TreeHYKTYPE", "TB_HYKNAME", vCZK == "1" ? 2 : 1);
    switch (vDJLX) {
        case "0":
            FillBGDDTreeSK("TreeBGDD_BR", "TB_BGDDMC_BR");
            FillBGDDTreeZK("TreeBGDD_BC", "TB_BGDDMC_BC");
            vDBConnName = "CRMDB";
            iStatus = 0;
            break;
        case "1":
            FillBGDDTreeSK("TreeBGDD_BC", "TB_BGDDMC_BC");
            FillBGDDTreeSK("TreeBGDD_BR", "TB_BGDDMC_BR");
            vDBConnName = vCZK == "1" ? "CRMDBMZK" : "CRMDB";
            iStatus = 1;
            break;
        case "2":
            FillBGDDTreeSK("TreeBGDD_BC", "TB_BGDDMC_BC");
            FillBGDDTreeZK("TreeBGDD_BR", "TB_BGDDMC_BR");
            vDBConnName = vCZK == "1" ? "CRMDBMZK" : "CRMDB";
            iStatus = 1;
            break;
    }

    //DrawGrid("list_sqd", vKLXColumnNames, vKLXColumnModel, false);

    //添加卡
    $("#AddItem").click(function () {

        if ($("#HF_BGDDDM_BC").val() == "") {
            ShowMessage("请选择拨出地点");
            return;
        }
        if ($("#TB_CZKHM_BEGIN").val() == "") {
            ShowMessage("请输入开始卡号");
            return;
        }
        if ($("#TB_CZKHM_END").val() == "") {
            ShowMessage("请输入结束卡号");
            return;
        }
        if ($("#HF_HYKTYPE").val() == "") {
            ShowMessage("请选择卡类型");
            return;
        }
        if ($("#TB_CZKHM_BEGIN").val().length != $("#TB_CZKHM_END").val().length) {
            ShowMessage("开始卡号与结束卡号长度不一致");
            return;
        }
        var str1 = vCZK == "0" ? GetKCKXXData($("#TB_CZKHM_BEGIN").val(), "", 0, $("#HF_BGDDDM_BC").val(), vDBConnName, iStatus) : GetMZKKCKXXData($("#TB_CZKHM_BEGIN").val(), "", vDBConnName); //GetKCKXXData($("#TB_CZKHM_BEGIN").val(), "", vDBConnName);
        if (str1 == "" || str1 == undefined) {
            ShowMessage("没有找到开始卡号的库存卡");
            return;
        }
        var obj1 = JSON.parse(str1);
        if (obj1.sCZKHM == "") {
            ShowMessage("没有找到开始卡号的库存卡");
            return;
        }
        if (obj1.iSTATUS != iStatus) {
            ShowMessage("开始卡号的状态错误");
            return;
        }
        var str2 = vCZK == "0" ? GetKCKXXData($("#TB_CZKHM_END").val(), "", 0, $("#HF_BGDDDM_BC").val(), vDBConnName, iStatus) : GetMZKKCKXXData($("#TB_CZKHM_END").val(), "", vDBConnName);
        if (str2 == "") {
            ShowMessage("没有找到结束卡号的库存卡");
            return;
        }
        var obj2 = JSON.parse(str2);
        if (obj2.sCZKHM == "") {
            ShowMessage("没有找到结束卡号的库存卡");
            return;
        }
        if (obj2.iSTATUS != iStatus) {
            ShowMessage("结束卡号的状态错误");
            return;
        }
        if (obj1.iHYKTYPE != obj2.iHYKTYPE) {
            ShowMessage("开始卡类型与结束卡类型不一致");
            return;
        }
        CalcKD($("#TB_CZKHM_BEGIN").val(), $("#TB_CZKHM_END").val(), $("#HF_HYKTYPE").val());
    });


    $("#DelItem").click(function () {
        DeleteRows("list");
    });
    BFButtonClick("TB_LQRMC", function () {
        SelectRYXX("TB_LQRMC", "HF_LQR", "zHF_LQR", false);
    });
    //$("#TB_BGDDMC_BC").change(function () {
    //    $("#list").clearGridData();
    //});
    //$("#TB_BGDDMC_BR").click(function () {
    //    if ($("#S_MD").val() == "") {
    //        ShowMessage("请先选择门店");
    //        return false;
    //    }
    //});
    vJLBH = GetUrlParam("jlbh");//$.getUrlParam("jlbh");
    if (vJLBH != "") {
        ShowDataBase(vJLBH);
    };
    //$("#list").datagrid({
    //    onAfterEdit: function (index, row) {           
    //        updateActions(index, row);
    //    },
    //});
});

function TreeNodeClickCustom(event, treeId, treeNode) {
    switch (treeId) {
        case "TreeBGDD_BR":
            $("#HF_BGDDDM_BR").val(treeNode.id);
            break;
        case "TreeBGDD_BC":
            if ($("#list").datagrid("getData").length > 0) {
                art.dialog("警告：将要清空卡号段数据！", function () {
                    $('#list').datagrid('loadData', "", "json");
                    $('#list').datagrid("loaded");
                    $("#TB_BGDDMC_BC").val(treeNode.name);
                    $("#HF_BGDDDM_BC").val(treeNode.id);
                });
            } else {
                $("#TB_BGDDMC_BC").val(treeNode.name);
                $("#HF_BGDDDM_BC").val(treeNode.id);
            }
            break;
        case "TreeHYKTYPE":
            $("#HF_HYKTYPE").val(treeNode.id);
            break;
    }
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);//

    $("#HF_BGDDDM_BC").val(Obj.sBGDDDM_BC);
    $("#TB_BGDDMC_BC").val(Obj.sBGDDMC_BC);
    $("#HF_BGDDDM_BR").val(Obj.sBGDDDM_BR);
    $("#TB_BGDDMC_BR").val(Obj.sBGDDMC_BR);
    $("#TB_SQD").val(Obj.iSQDJH);
    $("#HF_SQD").val(Obj.iSQDJH);
    $("#HF_LQR").val(Obj.iLQR);
    $("#TB_LQRMC").val(Obj.sLQRMC);
    $("#LB_SL").text(Obj.iHYKSL);
    $('#list').datagrid('loadData', Obj.kditemTable, "json");
    $('#list').datagrid("loaded");
    //if (Obj.iSQDJH != 0) {
    //    $('#list_sqd').datagrid('loadData', Obj.itemTable2, "json");
    //    $('#list_sqd').datagrid("loaded");
    //}
    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);
}

//function onClick(e, treeId, treeNode) {
//    if (treeId == "TreeBGDD_BC") {
//        if ($("#list").datagrid("getData").length > 0) {
//            art.dialog("警告：将要清空卡号段数据！", function () {
//                $('#list').datagrid('loadData', "", "json");
//                $('#list').datagrid("loaded");
//                $("#TB_BGDDMC_BC").val(treeNode.name);
//                $("#HF_BGDDDM_BC").val(treeNode.id);
//            });
//        } else {
//            $("#TB_BGDDMC_BC").val(treeNode.name);
//            $("#HF_BGDDDM_BC").val(treeNode.id);
//        }
//        hideMenu("menuContent_BC");
//    }
//    if (treeId == "TreeBGDD_BR") {
//        $("#TB_BGDDMC_BR").val(treeNode.name);
//        $("#HF_BGDDDM_BR").val(treeNode.id);
//        hideMenu("menuContent_BR");
//    }
//}

//function onHYKClick(e, treeId, treeNode) {
//    $("#TB_HYKNAME").val(treeNode.name);
//    $("#HF_HYKTYPE").val(treeNode.id);
//    hideMenu("menuContentHYKTYPE");
//}

//打开弹出窗口，并绑定返回的数据
function WUCDialogToTable(url, widthpx, heightpx, returndata, tablename) {
    art.dialog.open(url, {
        lock: true, width: widthpx, height: heightpx, cancel: true,
        close: function () {
            //当关闭窗口时，接收返回的数据
            var returnValues = $.dialog.data(returndata);//接收的应该是转换成对象，或者数组
            var array = new Array();
            var addSl = 0;
            //有数据返回
            if (returnValues != null && returnValues != "") {

                array = JSON.parse(returnValues);//返回的数据需要符合JSON字符串格式，才能进行转换成数组，或者对象
                //遍历，将已经添加过来的卡不重复添加

                //添加到表中
                var rowdata = new Object();
                rowdata.sCZKHM_BEGIN = array[0].sCZKHM < array[array.length - 1].sCZKHM ? array[0].sCZKHM : array[array.length - 1].sCZKHM;
                rowdata.sCZKHM_END = array[0].sCZKHM < array[array.length - 1].sCZKHM ? array[array.length - 1].sCZKHM : array[0].sCZKHM;
                rowdata.iHYKTYPE = array[0].iHYKTYPE;
                rowdata.sHYKNAME = array[0].sHYKNAME;
                rowdata.iSKSL = array.length;
                rowdata.fMZJE = array[0].fQCYE;
                $("#" + tablename).addRowData($("#" + tablename).getGridParam("reccount"), rowdata);
                addSl = array.length;
            }

            $.dialog.data(returndata, "");//清空数据
            $("#LB_SL").text(parseInt($("#LB_SL").text() == "" ? 0 : $("#LB_SL").text()) + parseInt(addSl));


        }
    });

}

//删除选中行
//function DelTableRow(tablename) {
//    var delCount = 0;
//    var selRow = $("#" + tablename).jqGrid("getGridParam", "selarrrow");
//    var len = selRow.length;//每次删除时，selRow的值都会变化
//    if (len == 0) {
//        art.dialog({ title: '没有选中', content: '没有被选中的卡!' });
//        return;

//    }
//    for (var i = 0; i < len; i++) {
//        delCount += parseInt($("#" + tablename).jqGrid("getRowData", selRow[0]).iSKSL)

//        $("#" + tablename).jqGrid("delRowData", selRow[0]);//始终删除选中的行的第一行，

//    }
//    return delCount;
//}

function CalcKD(sCZKHM_BEGIN, sCZKHM_END, iHYKTYPE) {
    //先判断是否已经存在和添加号段有交叉的号段
    //var iHBXH = -1;
    //for (i = 0; i < $("#list").getGridParam("reccount") ; i++) {
    //    var rowData = $("#list").jqGrid("getRowData")[i];
    //    //卡类型一样的才合并
    //    if (rowData.iHYKTYPE == iHYKTYPE) {
    //        //添加号段在已有某号段之内时，不需要添加
    //        if (rowData.sCZKHM_BEGIN <= sCZKHM_BEGIN && rowData.sCZKHM_END >= sCZKHM_END)
    //            return;
    //        //添加号段在已有某号段之外，跳过本条
    //        if (rowData.sCZKHM_BEGIN > sCZKHM_END || rowData.sCZKHM_END < sCZKHM_BEGIN)
    //            continue;
    //        if ((rowData.sCZKHM_BEGIN > sCZKHM_BEGIN && rowData.sCZKHM_BEGIN <= sCZKHM_END)
    //            || (rowData.sCZKHM_END < sCZKHM_END && rowData.sCZKHM_END >= sCZKHM_BEGIN)) {
    //            sCZKHM_BEGIN = Math.min(rowData.sCZKHM_BEGIN,sCZKHM_BEGIN);
    //            sCZKHM_END = Math.max(rowData.sCZKHM_END,sCZKHM_END);
    //            iHBXH = i;
    //            AddKD(sCZKHM_BEGIN, sCZKHM_END, iHYKTYPE, iHBXH);
    //        }
    //    }
    //}
    //if (iHBXH == -1)
    //    AddKD(sCZKHM_BEGIN, sCZKHM_END, iHYKTYPE, iHBXH);
    //if (vDJLX == "0" && $("#HF_SQD").val() != "") {
    //    var rows2 = $('#list_sqd').datagrid("getData").rows;
    //    for (var i = 0; i < rows2.length; i++) {

    //        if (rows2[i].iHYKTYPE == iHYKTYPE)
    //            sqsl = rows2[i].iSKSL;
    //    }
    //}

    var rowList = $("#list").datagrid("getData").rows;
    txsl = 0;
    for (i = 0; i < rowList.length ; i++) {
        var rowData = rowList[i];
        //if (rowList[i].iHYKTYPE == iHYKTYPE)
        //    txsl += parseInt(rowList[i].iSKSL);
        if (rowData.sCZKHM_BEGIN == sCZKHM_BEGIN) {
            ShowMessage("新增卡号段和已有卡号段存在同样的开始卡号，不能添加");
            return;
        }
        if (rowData.sCZKHM_BEGIN == sCZKHM_BEGIN) {
            ShowMessage("新增卡号段和已有卡号段存在同样的结束卡号，不能添加");
            return;
        }
    }
    jsdata = "{'iSTATUS':" + iStatus + ",'sBGDDDM':'" + $("#HF_BGDDDM_BC").val() + "','iHYKTYPE':'" + iHYKTYPE + "','sCZKHM_BEGIN':'" + sCZKHM_BEGIN + "','sCZKHM_END':'" + sCZKHM_END + "','sDBConnName':'" + vDBConnName + "','iBJ_KCK':'" + 1 + "'}";
    $.ajax({
        type: 'post',
        asycn: false,
        url: vCZK == "0" ? "../../CrmLib/CrmLib.ashx?func=GetKCKKD" : "../../CrmLib/CrmLib.ashx?func=GetMZKKCKKD",//"../../CrmLib/CrmLib.ashx?func=GetKCKKD",
        dataType: "text",
        //postData: { sBGDDDM: $("#HF_BGDDDM_BC").val(), iSTATUS: 0, iHYKTYPE: iHYKTYPE, sCZKHM_Begin: sCZKHM_BEGIN, sCZKHM_End: sCZKHM_END, },
        data: { json: jsdata, titles: 'cecece' },
        success: function (data) {
            try {
                var Obj = JSON.parse(data);
                //var rowNum1 = $("#list").getGridParam("reccount");
                for (i = 0; i < Obj.length; i++) {

                    $('#list').datagrid('appendRow', {
                        sCZKHM_BEGIN: Obj[i].sCZKHM_BEGIN,
                        sCZKHM_END: Obj[i].sCZKHM_END,
                        iHYKTYPE: Obj[i].iHYKTYPE,
                        sHYKNAME: Obj[i].sHYKNAME,
                        iSKSL: Obj[i].iSKSL,
                        fMZJE: Obj[i].fMZJE,
                    });


                    //$("#list").addRowData($("#list").getGridParam("reccount"),
                    //    {
                    //        sCZKHM_BEGIN: Obj.rows[i].sCZKHM_BEGIN,
                    //        sCZKHM_END: Obj.rows[i].sCZKHM_END,
                    //        iHYKTYPE: Obj.rows[i].iHYKTYPE,
                    //        sHYKNAME: Obj.rows[i].sHYKNAME,
                    //        iSKSL: Obj.rows[i].iSKSL,
                    //        fMZJE: Obj.rows[i].fMZJE,
                    //    });
                }
                $("#TB_CZKHM_BEGIN").val("");
                $("#TB_CZKHM_END").val("");
            } catch (e) {
                alert(data);
            }
        }
    });
}

//function AddKD(sCZKHM_BEGIN, sCZKHM_END, iHYKTYPE, iHBXH) {
//    jsdata = "{'iSTATUS':" + iStatus + ",'sBGDDDM':'" + $("#HF_BGDDDM_BC").val() + "','iHYKTYPE':'" + iHYKTYPE + "','sCZKHM_BEGIN':'" + sCZKHM_BEGIN + "','sCZKHM_END':'" + sCZKHM_END + "','sDBConnName':'" + vDBConnName + "'}";
//    $.ajax({
//        type: 'post',
//        asycn: false,
//        url: "../../CrmLib/CrmLib.ashx?func=GetKCKKD",
//        dataType: "text",
//        //postData: { sBGDDDM: $("#HF_BGDDDM_BC").val(), iSTATUS: 0, iHYKTYPE: iHYKTYPE, sCZKHM_Begin: sCZKHM_BEGIN, sCZKHM_End: sCZKHM_END, },
//        data: { json: jsdata, titles: 'cecece' },
//        success: function (data) {
//            try {
//                var Obj = JSON.parse(data);
//                //var rowNum1 = $("#list").getGridParam("reccount");
//                for (i = 0; i < Obj.rows.length; i++) {
//                    if (iHBXH == -1)
//                        $("#list").addRowData($("#list").getGridParam("reccount"), { sCZKHM_BEGIN: Obj.rows[i].sCZKHM_BEGIN, sCZKHM_END: Obj.rows[i].sCZKHM_END, iHYKTYPE: Obj.rows[i].iHYKTYPE, sHYKNAME: Obj.rows[i].sHYKNAME, iSKSL: Obj.rows[i].iSKSL, fMZJE: Obj.rows[i].fMZJE, });
//                    else {
//                        $("#list").setRowData(iHBXH, Obj.rows[i]);
//                    }
//                }
//            } catch (e) {
//                alert(data);
//            }
//        }
//    });
//}

function LoadData() {;
}

//function MakeJLBH(t_jlbh) {
//    //生成iJLBH的JSON
//    var Obj = new Object();
//    Obj.iJLBH = t_jlbh;
//    if (GetUrlParam("czk") == "1") {
//        if (GetUrlParam("djlx") == "1") {
//            Obj.sDBConnName = "CRMDBMZK";
//        }
//        if (GetUrlParam("djlx") == "2") {
//            Obj.sDBConnName = "CRMDBMZK";
//        }
//    }
//    return Obj;
//}

//function MoseDialogCustomerReturn(dialogName, lstData, showField) {
//    if (dialogName == "ListSQD") {
//        AddKDXX($("#HF_SQD").val());
//    }

//};


//function AddKDXX(SQDJLBH) {
//    $('#list_sqd').datagrid('loadData', { total: 0, rows: [] });
//    var str = GetSQDXX(SQDJLBH, vCZK);
//    var Obj = JSON.parse(str);
//    $("#TB_BGDDMC_BR").val(Obj.sBGDDMC_BR);
//    $("#HF_BGDDDM_BR").val(Obj.sBGDDDM_BR);
//    document.getElementById("TB_BGDDMC_BR").disabled = true;
//    for (var i = 0; i < Obj.itemTable.length; i++)
//        $('#list_sqd').datagrid('appendRow', {
//            iHYKTYPE: Obj.itemTable[i].iHYKTYPE,
//            sHYKNAME: Obj.itemTable[i].sHYKNAME,
//            iSKSL: Obj.itemTable[i].iHYKSL,
//        });
//    $("#TB_HYKNAME").prop("disabled", true);
//}

//function updateActions(index, row) {
//    if (row.sCZKHM_BEGIN != "" && row.sCZKHM_BEGIN!=undefined) {
//        var str1 = GetKCKXXData(row.sCZKHM_BEGIN, "", vDBConnName);
//        if (str1 == "") {
//            ShowMessage("没有找到开始卡号的库存卡");
//            row['sCZKHM_BEGIN'] = "";
//            $('#list').datagrid('refreshRow', index);
//            return;

//        }
//        var obj1 = JSON.parse(str1);
//        if (obj1.sCZKHM == "") {
//            ShowMessage("没有找到开始卡号的库存卡");
//            row['sCZKHM_BEGIN'] = "";
//            $('#list').datagrid('refreshRow', index);
//            return;
//        }
//        if (obj1.iHYKTYPE != row.iHYKTYPE) {
//            ShowMessage("卡类型错误");
//            row['sCZKHM_BEGIN'] = "";
//            $('#list').datagrid('refreshRow', index);
//            return;
//        }
//        if (obj1.iSTATUS != iStatus) {
//            ShowMessage("开始卡号的状态错误");
//            row['sCZKHM_BEGIN'] = "";
//            $('#list').datagrid('refreshRow', index);
//            return;
//        }
//        if (obj1.sBGDDDM != $("#HF_BGDDDM_BC").val()) {
//            ShowMessage("开始卡号的保管地点错误");
//            row['sCZKHM_BEGIN'] = "";
//            $('#list').datagrid('refreshRow', index);
//            return;
//        }

//    }
//    if (row.sCZKHM_END != "" && row.sCZKHM_END != undefined) {
//        var str1 = GetKCKXXData(row.sCZKHM_END, "", vDBConnName);
//        if (str1 == "") {
//            ShowMessage("没有找到结束卡号的库存卡");
//            row['sCZKHM_END']= "";
//            $('#list').datagrid('refreshRow', index);
//            return;

//        }
//        var obj1 = JSON.parse(str1);
//        if (obj1.sCZKHM == "") {
//            ShowMessage("没有找到结束卡号的库存卡");
//            row['sCZKHM_END'] = "";
//            $('#list').datagrid('refreshRow', index);
//            return;
//        }
//        if (obj1.iSTATUS != iStatus) {
//            ShowMessage("结束卡号的状态错误");
//            row['sCZKHM_END'] = "";
//            $('#list').datagrid('refreshRow', index);
//            return;
//        }
//         if (obj1.iHYKTYPE != row.iHYKTYPE) {
//             ShowMessage("结束卡号卡类型错误");
//            row['sCZKHM_END'] = "";
//            $('#list').datagrid('refreshRow', index);
//            return;
//         }
//         if (obj1.sBGDDDM != $("#HF_BGDDDM_BC").val()) {
//             ShowMessage("结束卡号的保管地点错误");
//             row['sCZKHM_END'] = "";
//             $('#list').datagrid('refreshRow', index);
//             return;
//         }
//    }

//    if (row.sCZKHM_BEGIN != "" && row.sCZKHM_BEGIN != undefined && row.sCZKHM_END != "" && row.sCZKHM_END != undefined)
//    {
//        var str = CalcKDKSL(0, $("#HF_BGDDDM_BC").val(), row.sCZKHM_BEGIN, row.sCZKHM_END, row.iHYKTYPE);
//        var Obj = JSON.parse(str);
//        if (str != "");
//        {
//            if (row.iSKSL != Obj.rows[0].iSKSL) {
//                ShowMessage("卡数量错误,输入的卡段有" + Obj.rows[0].iSKSL + "张卡,申请单申请数量为" + row.iSKSL,5);
//                row['sCZKHM_BEGIN'] = "";
//                row['sCZKHM_END'] = "";
//                $('#list').datagrid('refreshRow', index);
//            }
//        }
//    }
//}

//function OnClickRow(rowIndex, rowData) {
//    if (this.id == "list_sqd") {
//        $("#TB_HYKNAME").val(rowData.sHYKNAME);
//        $("#HF_HYKTYPE").val(rowData.iHYKTYPE);
//    }
//}

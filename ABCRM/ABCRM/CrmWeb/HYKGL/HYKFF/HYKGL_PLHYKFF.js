//vPageMsgID = CM_HYKGL_JFKPLFF;
vUrl = "../HYKGL.ashx";
var sksl = 0;
var hyktype = 0;
var pPSW = 0;


function InitGrid() {
    vColumnNames = ['开始卡号', '结束卡号', 'iHYKTYPE', '卡类型', 'iBJ_PSW', '是否需要密码', '数量', '面值金额', '有效期'];
    vColumnModel = [
            { name: 'sCZKHM_BEGIN', index: 'sCZKHM_BEGIN', width: 100, editable: true, editrules: { required: true, custom: true, custom_func: GetKCKXX, }, },
            { name: 'sCZKHM_END', index: 'sCZKHM_END', width: 100, editable: true, editrules: { required: true, custom: true, custom_func: GetKCKXX, }, },
            { name: 'iHYKTYPE', hidden: true, },
            { name: 'sHYKNAME', index: 'sHYKNAME', width: 50, },//align: "right", editable: true,edittype:"select", editoptions: { value:"101:金卡;102:银卡" }
            { name: 'iBJ_PSW', hidden: true, },
            { name: 'sBJ_PSW', width: 50, hidden: true, },//align: "right", editable: true,edittype:"select", editoptions: { value:"101:金卡;102:银卡" }
            { name: 'iSKSL', index: 'iSKSL', width: 20, align: "right", editable: true, },
            { name: 'fMZJE', index: 'fMZJE', width: 50, align: 'right', editable: true, },
            { name: 'dYXQ', hidden: true, },
    ];
};

function SetControlState() {
    ;
}

function IsValidData() {

    if ($("#list").datagrid("getData").rows.length == 0) {
        ShowMessage("没有选中的卡号段", 3);
        return false;
    }
    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.iFS = 1;
    Obj.iSKSL = $("#LB_SKSL").text();
    Obj.sBGDDDM = $("#HF_BGDDDM").val();
    Obj.sZY = $("#TA_ZY").val();
    Obj.fDZKFJE = $("#TB_DZKFJE").val();
    Obj.iBJ_PSW = $("#BJ_MMBS").prop("checked") ? 1 : 0;
    Obj.iHYKTYPE = $("#HF_HYKTYPE").val();
    var lst = new Array();
    lst = $("#list").datagrid("getData").rows;
    Obj.kditemTable = lst;
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    //Obj.sLXR = $("#TB_LXRMC").val();
    //Obj.iKHID = ($("#HF_KHID").val() != "") ? $("#HF_KHID").val() : "-1";
    //Obj.dYXQ = $("#LB_YXQ").text();
    return Obj;
}

$(document).ready(function () {
    //$("#menu").tabify();
    FillBGDDTreeSK("TreeBGDD", "TB_BGDDMC");
    FillHYKTYPETree("TreeHYKTYPE", "TB_HYKNAME", 1, 1);
    //添加卡
    $("#AddItem").click(function () {
        if ($("#HF_BGDDDM").val() == "") {
            ShowMessage("请选择保管地点", 3);
            return;
        }
        if ($("#TB_CZKHM_BEGIN").val() == "") {
            ShowMessage("请输入开始卡号", 3);
            return;
        }
        if ($("#TB_CZKHM_END").val() == "") {
            ShowMessage("请输入结束卡号", 3);
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
        CalcKD($("#TB_CZKHM_BEGIN").val(), $("#TB_CZKHM_END").val(), $("#HF_HYKTYPE").val());
    });

    $("#DelItem").click(function () {
        DeleteRows("list");
    });
})

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);//input控件被换成了label...
    $("#LB_SKSL").text(Obj.iSKSL);
    $("#HF_BGDDDM").val(Obj.sBGDDDM);
    $("#TB_BGDDMC").val(Obj.sBGDDMC);
    $("#HF_HYKTYPE").val(Obj.iHYKTYPE);
    $("#TB_HYKNAME").val(Obj.sHYKNAME);
    $("#TA_ZY").val(Obj.sZY);
    $("#TB_DZKFJE").val(Obj.fDZKFJE);
    //$("#HF_LXR").val(Obj.iLXR);
    //$("#TB_LXRMC").val(Obj.sLXRMC);
    $("#TB_LXRMC").val(Obj.sLXR);
    $("#HF_KHID").val(Obj.iKHID);
    $("#TB_KHMC").val(Obj.sKHMC);

    $('#list').datagrid('loadData', Obj.kditemTable, "json");
    $('#list').datagrid("loaded");


    //for (var i = 0; i < Obj.kditemTable.length ; i++) {
    //    $("#list").addRowData(i, Obj.kditemTable[i]);
    //    hyktype = Obj.kditemTable[i].iHYKTYPE;
    //    isPSW(hyktype);
    //    $("#list").setRowData(i, { iBJ_PSW: pPSW, sBJ_PSW: pPSW == 1 ? "是" : "否", });
    //}

    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);
}

function TreeNodeClickCustom(event, treeId, treeNode) {
    switch (treeId) {
        case "TreeBGDD": $("#HF_BGDDDM").val(treeNode.sBGDDDM); break;
        case "TreeHYKTYPE":
            $("#HF_HYKTYPE").val(treeNode.iHYKTYPE);
            $("#TB_DZKFJE").val(treeNode.fKFJE);
            break;
    }
}
//function onClick(e, treeId, treeNode) {
//    $("#TB_BGDDMC").val(treeNode.name);
//    $("#HF_BGDDDM").val(treeNode.id);
//    hideMenu("menuContent");
//}

//function onHYKClick(e, treeId, treeNode) {
//    $("#TB_HYKNAME").val(treeNode.name);
//    $("#HF_HYKTYPE").val(treeNode.id);
//    $("#TB_DZKFJE").val(treeNode.kfje);
//    hideMenu("menuContentHYKTYPE");
//}

function GetKCKXX(value, colname) {

    var str = GetKCKXXData(value, 0);
    if (str == "null" || str == "" || str == undefined) {
        return [false, "没有找到卡号"];
    }
    else {
        var Obj = JSON.parse(str);
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var rowdata = $("#list").getRowData(rowid);
        $("#list").setCell(rowid, 'iHYKTYPE', Obj.iHYKTYPE);
        $("#list").setCell(rowid, 'sHYKNAME', Obj.sHYKNAME);
        $("#LB_SL").text(rowdata.sCZKHM_END - rowdata.sCZKHM_BEGIN);
        return [true, ""];
    }
}

//function LoadData(rowData) {
//    var rowNum1 = $("#list").getGridParam("reccount");
//    var rowNum2 = $("#list2").getGridParam("reccount");
//    $("#list").addRowData(rowNum1, { sCZKHM_BEGIN: rowData[0].sCZKHM, sCZKHM_END: rowData[rowData.length - 1].sCZKHM, iHYKTYPE: rowData[0].iHYKTYPE, sHYKNAME: rowData[0].sHYKNAME, iSKSL: rowData.length, fMZJE: rowData[0].fQCYE, });
//    for (i = 0; i < rowData.length; i++) {
//        $("#list2").addRowData(rowNum2 + i, rowData[i]);
//    }
//    //$("#list2").addRowData(sCZKHM, rowData);
//    sksl += rowData.length;
//    $("#LB_SKSL").text(sksl);
//}

function IsRepeat(array) {    //ys 2014.11.17
    array.sort();
    var tmpArr = [array[0]];
    for (var i = 1; i < array.length; i++) {
        if (array[i] != tmpArr[tmpArr.length - 1]) {
            tmpArr.push(array[i]);
        } else {
            //return true;
            return array[i];
        }
    }
    //return false;
    return null;
}

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


    var rowList = $("#list").datagrid("getData").rows;
    for (var i = 0; i < rowList.length ; i++) {
        var rowData = rowList[i];
        if (rowData.sCZKHM_BEGIN == sCZKHM_BEGIN) {
            ShowMessage("新增卡号段和已有卡号段存在同样的开始卡号，不能添加");
            return;
        }
        if (rowData.sCZKHM_BEGIN == sCZKHM_BEGIN) {
            ShowMessage("新增卡号段和已有卡号段存在同样的结束卡号，不能添加");
            return;
        }
    }

    PostToCrmlib("GetKCKKD", { iSTATUS: 1, sBGDDDM: $("#HF_BGDDDM").val(), iHYKTYPE: iHYKTYPE, sCZKHM_BEGIN: sCZKHM_BEGIN, sCZKHM_END: sCZKHM_END, sDBConnName: "CRMDB", iSKJLBH: -1 }, function (data) {
        var iSKSL = 0;
        for (i = 0; i < data.length; i++) {
            $("#list").datagrid("appendRow", {
                sCZKHM_BEGIN: data[i].sCZKHM_BEGIN,
                sCZKHM_END: data[i].sCZKHM_END,
                iHYKTYPE: data[i].iHYKTYPE,
                sHYKNAME: data[i].sHYKNAME,
                iBJ_PSW: data[i].iBJ_PSW,
                sBJ_PSW: data[i].iBJ_PSW == 1 ? "是" : "否",
                iSKSL: data[i].iSKSL,
                fMZJE: data[i].fMZJE,
            });
            iSKSL += data[i].iSKSL;
        }
        $("#TB_CZKHM_BEGIN").val("");
        $("#TB_CZKHM_END").val("");
        if ($("#LB_SKSL").text() == "")
            $("#LB_SKSL").text("0");
        $("#LB_SKSL").text((parseInt($("#LB_SKSL").text()) + iSKSL).toString());
    });
    //jsdata = "{'iSTATUS':" + "1" + ",'sBGDDDM':'" + $("#HF_BGDDDM").val() + "','iHYKTYPE':'" + iHYKTYPE + "','sCZKHM_BEGIN':'" + sCZKHM_BEGIN + "','sCZKHM_END':'" + sCZKHM_END + "','sDBConnName':'" + "CRMDB" + "'}";
    //$.ajax({
    //    type: 'post',
    //    asycn: false,
    //    url: "../../CrmLib/CrmLib.ashx?func=GetKCKKD",
    //    dataType: "text",
    //    //postData: { sBGDDDM: $("#HF_BGDDDM_BC").val(), iSTATUS: 0, iHYKTYPE: iHYKTYPE, sCZKHM_Begin: sCZKHM_BEGIN, sCZKHM_End: sCZKHM_END, },
    //    data: { json: jsdata, titles: 'ys' },
    //    success: function (data) {
    //        try {
    //            var Obj = JSON.parse(data);
    //            //var rowNum1 = $("#list").getGridParam("reccount");
    //            var iSKSL = 0;
    //            for (i = 0; i < Obj.rows.length; i++) {
    //                $("#list").datagrid("appendRow", {
    //                    sCZKHM_BEGIN: Obj.rows[i].sCZKHM_BEGIN,
    //                    sCZKHM_END: Obj.rows[i].sCZKHM_END,
    //                    iHYKTYPE: Obj.rows[i].iHYKTYPE,
    //                    sHYKNAME: Obj.rows[i].sHYKNAME,
    //                    iBJ_PSW: Obj.rows[i].iBJ_PSW,
    //                    sBJ_PSW: Obj.rows[i].iBJ_PSW == 1 ? "是" : "否",
    //                    iSKSL: Obj.rows[i].iSKSL,
    //                    fMZJE: Obj.rows[i].fMZJE,
    //                });
    //                iSKSL += Obj.rows[i].iSKSL;
    //            }
    //            $("#TB_CZKHM_BEGIN").val("");
    //            $("#TB_CZKHM_END").val("");
    //            if ($("#LB_SKSL").text() == "")
    //                $("#LB_SKSL").text("0");
    //            $("#LB_SKSL").text((parseInt($("#LB_SKSL").text()) + iSKSL).toString());
    //        } catch (e) {
    //            alert(data);
    //        }
    //    }
    //});
}
function isPSW(hyktype) {
    var data = CheckPSW(hyktype);
    if (data) {
        data = JSON.parse(data);
        pPSW = data.iBJ_PSW;
    }
}
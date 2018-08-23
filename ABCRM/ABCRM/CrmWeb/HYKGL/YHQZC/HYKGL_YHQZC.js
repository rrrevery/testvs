vUrl = "../HYKGL.ashx";
var HYKNO = GetUrlParam("HYKNO");

function InitGrid() {
    vColumnNames = ["转出会员卡号", "iHYID_ZC", "优惠券ID", "优惠券名称", "转出金额", "余额", "有效期", "门店范围代码", "促销活动编号", "促销主题"];
    vColumnModel = [
          { name: "sHYK_NO", width: 80, },
          { name: "iHYID", hidden: true },
          { name: "iYHQID", width: 60, hidden: true },
          { name: "sYHQMC", width: 80 },
          { name: "fZCJE", width: 60, editor: 'text' },
          { name: "fJE", width: 60 },
          { name: "dJSRQ", width: 80 },
          { name: "sMDFWDM", width: 80, hidden: true, },
          { name: "iCXID", width: 80, hidden: true, },
          { name: "sCXZT", width: 120 },
    ];
}

$(document).ready(function () {
    FillBGDDTree("TreeBGDD", "TB_BGDDMC", "menuContent");
    if (HYKNO != "") {
        $("#TB_ZRHYKNO").val(HYKNO);
        GetCheck();
        SetControlBaseState();
        $("#TB_ZRHYKNO").attr("readonly", "readonly");
    }

    //还没有做转出金额不能大于余额的判断
    //    afterSaveCell: function (rowid, name, val, iRow, iCol) {

    //        if (name == "fZCJE") {
    //            var rowData = $("#list").jqGrid("getRowData", rowid);
    //            if (parseFloat(val) > parseFloat(rowData.fJE)) {
    //                art.dialog({ lock: true, content: "转出金额不能大于余额" });
    //                val = 0;
    //            }
    //            if (parseFloat(val) < 0) {
    //                art.dialog({ lock: true, content: "转出金额不能为负" });
    //                val = 0;
    //            }
    //        }
    //});


    $("#AddItem").click(function () {
        var condData = new Object();
        var checkRepeatField = ["iHYID", "iYHQID", "iCXID", "sMDFWDM", "dJSRQ"];
        SelectYHQZH("list", condData, checkRepeatField);

    });
    $("#DelItem").click(function () {
        DeleteRows("list");
    });


});

function GetCheck() {
    if ($("#TB_ZRHYKNO").val() != "") {
        var str = GetHYXXData(0, $("#TB_ZRHYKNO").val());
        if (str) {
            var Obj = JSON.parse(str);
            $("#HF_HYID").val(Obj.iHYID);
            $("#HF_HYKTYPE").val(Obj.iHYKTYPE);
            $("#LB_HYKNAME").text(Obj.sHYKNAME);
        }
    }
}

function onClick(e, treeId, treeNode) {
    $("#TB_BGDDMC").val(treeNode.name);
    $("#HF_BGDDDM").val(treeNode.id);
    hideMenu("menuContent");
}


function IsValidData() {
    if ($("#HF_BGDDDM").val() == "") {
        ShowMessage("请选择操作地点");
        return false;
    }
    var lst = $("#list").datagrid("getRows");
    var zrhykno = $("#TB_ZRHYKNO").val();
    for (var i = 0; i < lst.length; i++) {
        if (zrhykno == lst[i].sHYK_NO) {
            ShowMessage("转入卡号不能和转出卡号相同！");
            return false;
        }
    }
    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";

    Obj.iHYID = $("#HF_HYID").val();
    Obj.sHYKNO = $("#TB_ZRHYKNO").val();
    Obj.iHYKTYPE = $("#HF_HYKTYPE").val();
    Obj.sBGDDDM = $("#HF_BGDDDM").val();
    Obj.fZRJE = compute("list", "fZCJE");;
    Obj.sZY = $("#TB_ZY").val();

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
    $("#HF_HYID").val(Obj.iHYID);
    $("#TB_ZRHYKNO").val(Obj.sHYKNO);
    $("#HF_BGDDDM").val(Obj.sBGDDDM);
    $("#TB_BGDDMC").val(Obj.sBGDDMC);
    $("#HF_HYKTYPE").val(Obj.iHYKTYPE);
    $("#LB_HYKNAME").text(Obj.sHYKNAME);
    $("#LB_ZRJE").text(Obj.fZRJE);
    $("#TB_ZY").val(Obj.sZY);

    $("#list").datagrid("loadData", Obj.itemTable, "json");
    $("#list").datagrid("loaded");

    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);
}

function compute(lisName, colName) {
    var rows = $('#' + lisName + '').datagrid('getRows')//获取当前的数据行
    var total = 0;
    for (var i = 0; i < rows.length; i++) {
        total += rows[i]['' + colName + ''];
    }
    return total;
}


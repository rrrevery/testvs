vUrl = "../MZKGL.ashx";
var HYKNO = GetUrlParam("HYKNO");

function InitGrid() {
    vColumnNames = ["转出卡号", "iHYID_ZC", "有效期", "余额", "转出金额"],
    vColumnModel = [
          { name: "sHYK_NO", width: 80, },
          { name: "iHYID", hidden: true },
          { name: "dYXQ", width: 80 },
          { name: "fYE", width: 60 },
          { name: "fZCJE", width: 80, editable: true, editor: 'text' },

    ];
};

$(document).ready(function () {
    FillBGDDTree("TreeBGDD", "TB_BGDDMC");
    $("#AddItem").click(function () {
        if ($("#HF_HYKTYPE").val() == "") {
            ShowMessage("请选择卡类型", 3);
            return;
        }
        var condData = new Object();
        condData["iHYKTYPE"] = $("#HF_HYKTYPE").val();
        condData["vZT"] = 1;
        var checkRepeatField = ["iHYID"];
        SelectMZK("list", condData, checkRepeatField);

    });
    $("#DelItem").click(function () {
        DeleteRows("list");
    });

    if (HYKNO != "") {
        $("#TB_HYKNO").val(HYKNO);
        GetMZKXX();
        vProcStatus = cPS_ADD;
        SetControlBaseState();
        $("#TB_HYKNO").attr("readonly", "readonly");
    }
});

function TreeNodeClickCustom(event, treeId, treeNode) {
    switch (treeId) {
        case "TreeBGDD": $("#HF_BGDDDM").val(treeNode.sBGDDDM); break;

    }
}


function IsValidData() {
    //var rows = $("#list").datagrid("getData").rows.length;
    //if (rows < 1) {
    //    ShowMessage("没有选择可操作的卡", 3);
    //    return false;
    //}
    //if ($("#HF_BGDDDM").val() == "") {
    //    ShowMessage("请填写操作地点!", 3);
    //    return false;

    //}
    return true;
}

function SaveData() {
    var Obj = new Object();
    if (vCZK == 1) {
        Obj.sDBConnName = "CRMDBMZK";
    }
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.iHYKTYPE = $("#HF_HYKTYPE").val();
    Obj.sBGDDDM = $("#HF_BGDDDM").val();
    Obj.iHYID = $("#HF_HYID").val();
    Obj.sHYKNO = $("#TB_HYKNO").val();
    Obj.sZY = $("#TB_ZY").val();
    Obj.fZRJE = compute("list", "fZCJE");
    var lst = new Array();
    lst = $("#list").datagrid("getData").rows;
    Obj.itemTable = lst;


    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;

    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#HF_HYID").val(Obj.iHYID);
    $("#TB_HYKNO").val(Obj.sHYKNO);

    $("#HF_BGDDDM").val(Obj.sBGDDDM);
    $("#TB_BGDDMC").val(Obj.sBGDDMC);
    $("#HF_HYKTYPE").val(Obj.iHYKTYPE);
    $("#LB_HYKNAME").text(Obj.sHYKNAME);
    $("#LB_ZRJE").text(Obj.fZRJE);
    $("#TB_ZY").val(Obj.sZY);
    $('#list').datagrid('loadData', Obj.itemTable, "json");
    $('#list').datagrid("loaded");

    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);

    GetMZKXX();
}

function GetMZKXX() {
    if ($("#TB_HYKNO").val() != "") {
        //根据卡号查询信息
        var str = GetMZKXXData(0, $("#TB_HYKNO").val(), "", "CRMDBMZK");
        if (str == "null" || str == "") {
            art.dialog({ lock: true, content: "卡号不存在或者校验失败" });
            return;
        }
        var Obj = JSON.parse(str);
        if (Obj.sHYK_NO == "") {
            ShowMessage("卡号不存在或者校验失败", 3);
            return;
        }
        if (Obj.iSTATUS < 0) {
            ShowMessage("卡状态错误", 3);
            return;
        }
        $("#HF_HYKTYPE").val(Obj.iHYKTYPE);
        $("#LB_HYKNAME").text(Obj.sHYKNAME);
        $("#HF_HYID").val(Obj.iHYID);
        $("#HF_HYKTYPE").val(Obj.iHYKTYPE);
        $("#LB_YXQ").text(Obj.dYXQ);
        $("#LB_HYKNAME").text(Obj.sHYKNAME);
        $("#LB_YE").text(Obj.fCZJE);


    }
}
function compute(lisName, colName) {
    var rows = $('#' + lisName + '').datagrid('getRows')//获取当前的数据行
    var total = 0;
    for (var i = 0; i < rows.length; i++) {
        total += rows[i]['' + colName + ''];
    }
    return total;
}

function CustomerOpenDialogReturn(rows, listName, lst, array, CheckFieldId) {
    for (var i = 0; i < lst.length; i++) {
        if (CheckReapet(array, CheckFieldId, lst[i]) && lst[i].sHYK_NO != $("#TB_HYKNO").val()) {
            $('#list').datagrid('appendRow', {
                sHYK_NO: lst[i].sHYK_NO,
                iHYID: lst[i].iHYID,
                dYXQ: lst[i].dYXQ,
                fYE: lst[i].fYE,
            });

        }
    }

}
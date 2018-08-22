var HYKNO = GetUrlParam("HYKNO");
var vCZK = IsNullValue(GetUrlParam("czk"), "0");
vUrl = vCZK == "0" ? "../HYKGL.ashx" : "../../MZKGL/MZKGL.ashx";
vDBName = vCZK == "0" ? "CRMDB" : "CRMDBMZK";

function InitGrid() {
    vColumnNames = ["HYID", "卡号", "姓名", 'HYKTYPE', '卡类型', "原有效期"],
    vColumnModel = [
          { name: "iHYID", hidden: true },
          { name: "sHYK_NO", width: 150 },
          { name: "sHY_NAME", width: 60 },
          { name: "iHYKTYPE", hidden: true },
          { name: "sHYKNAME", width: 100, },
          { name: "dYYXQ", width: 150 },

    ];
};

$(document).ready(function () {

    FillBGDDTree("TreeBGDD", "TB_BGDDMC", "menuContent");
    FillHYKTYPETree("TreeHYKTYPE", "TB_HYKNAME", "menuContentHYKTYPE", vCZK == "1" ? 2 : 1);

    if (HYKNO != "") {
        if (vCZK == 1) {
            var str = GetHYXXData(0, HYKNO, "CRMDBMZK");
        } else {
            var str = GetHYXXData(0, HYKNO);
        }
        if (str == "null" || str == "") {
            art.dialog({ lock: true, content: "没有找到卡号" });
            return;
        }
        var Obj = JSON.parse(str);
        $("#HF_HYKTYPE").val(Obj.iHYKTYPE);
        $("#TB_HYKNAME").val(Obj.sHYKNAME);
        $('#list').datagrid('appendRow', {
            sHYK_NO: HYKNO,
            iHYID: Obj.iHYID,
            sHY_NAME: Obj.sHY_NAME,
            iHYKTYPE: Obj.iHYKTYPE,
            sHYKNAME: Obj.sHYKNAME,
            dYYXQ: Obj.dYXQ,
        });
        vProcStatus = cPS_ADD;
        SetControlBaseState();
    }
    //$("#list").addRowData($("#list").getGridParam("reccount"), {});
    $("#AddItem").click(function () {
        if ($("#HF_HYKTYPE").val() == "") {
            ShowMessage("请选择卡类型", 3);
            return;
        }
        var DataArry = new Object();
        DataArry["iHYKTYPE"] = parseInt($("#HF_HYKTYPE").val());
        SelectHYK('list', DataArry, 'iHYID');
    });

    $("#DelItem").click(function () {
        DeleteRows("list");
    });

});

function onClick(e, treeId, treeNode) {
    $("#TB_BGDDMC").val(treeNode.name);
    $("#HF_BGDDDM").val(treeNode.id);
    hideMenu("menuContent");
}

function onHYKClick(e, treeId, treeNode) {
    var ids = $("#list").datagrid("getData").rows;
    if (ids.length > 0) {
        var rowdata = $('#list').datagrid('getData').rows[0];
        if (rowdata.iHYKTYPE != treeNode.id) {
            ShowYesNoMessage("卡类型不一致，是否清空卡号列表？", function () {

                $('#list').datagrid('loadData', { total: 0, rows: [] });
                $("#TB_HYKNAME").val(treeNode.name);
                $("#HF_HYKTYPE").val(treeNode.id);
                hideMenu("menuContentHYKTYPE");
            });
        }
        else {
            $("#TB_HYKNAME").val(treeNode.name);
            $("#HF_HYKTYPE").val(treeNode.id);
            hideMenu("menuContentHYKTYPE");
        }
    }
    else {
        $("#TB_HYKNAME").val(treeNode.name);
        $("#HF_HYKTYPE").val(treeNode.id);
        hideMenu("menuContentHYKTYPE");
    }
};


function SetControlState() {
    if (HYKNO != "") {
        $("#AddItem").prop("disabled", true);
        $("#DelItem").prop("disabled", true);
    }
}


function IsValidData() {

    var rows = $("#list").datagrid("getData").rows.length;
    if (rows < 1) {
        ShowMessage("没有选择可操作的卡", 3);
        return false;
    }
    if ($("#HF_BGDDDM").val() == "") {
        ShowMessage("请填写操作地点!", 3);
        return false;

    }
    if ($("#HF_HYKTYPE").val() == "") {
        ShowMessage("卡类型不能为空!", 3);
        return;

    }
    if ($.trim($("#TB_XYXQ").val()) == "") {
        ShowMessage("请输入新有效期!", 3);
        return;
    }

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
    Obj.dXYXQ = $("#TB_XYXQ").val();
    Obj.iHYKTYPE = $("#HF_HYKTYPE").val();
    Obj.sBGDDDM = $("#HF_BGDDDM").val();
    Obj.sZY = $("#TB_ZY").val();
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
    $("#TB_XYXQ").val(Obj.dXYXQ);
    $("#HF_HYKTYPE").val(Obj.iHYKTYPE);
    $("#TB_HYKNAME").val(Obj.sHYKNAME);
    $("#HF_BGDDDM").val(Obj.sBGDDDM);
    $("#TB_BGDDMC").val(Obj.sBGDDMC);
    $("#TB_ZY").val(Obj.sZY);
    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);


    $('#list').datagrid('loadData', Obj.itemTable, "json");
    $('#list').datagrid("loaded");
}

function CustomerOpenDialogReturn(rows, listName, lst, array, CheckFieldId) {
    for (var i = 0; i < lst.length; i++) {
        if (CheckReapet(array, CheckFieldId, lst[i])) {
            $('#list').datagrid('appendRow', {
                iHYID: lst[i].iHYID,
                sHYK_NO: lst[i].sHYK_NO,
                sHY_NAME: lst[i].sHY_NAME,
                iHYKTYPE: lst[i].iHYKTYPE,
                dYYXQ: lst[i].dYXQ,
                sHYKNAME: lst[i].sHYKNAME,
            });
        }
    }

}




function clearDataGrid() {
    $('#list').datagrid('loadData', { total: 0, rows: [] });
}

function InsertClickCustom() {
    window.setTimeout(function () {
        clearDataGrid();
    }, 1);

};

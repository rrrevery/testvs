vUrl = "../MZKGL.ashx";
var HYKNO = GetUrlParam("HYKNO");



function InitGrid() {
    vColumnNames = ["面值卡卡号", "会员姓名", "hyid", "累计消费金额", "当前积分", "当前余额", "iHYKTYPE", "卡类型"],
    vColumnModel = [
          { name: "sHYK_NO", width: 100, },
          { name: "sHY_NAME", hidden: true },
          { name: "iHYID", hidden: true },
          { name: "fLJXFJE", width: 60, hidden: true },
          { name: 'fWCLJF', width: 60, hidden: true,},
          { name: "fYE", width: 60 },
          { name: 'iHYKTYPE', hidden: true, },
          { name: 'sHYKNAME', hidden: true, },

    ];
};

$(document).ready(function () {
    FillBGDDTree("TreeBGDD", "TB_BGDDMC");
    FillHYKTYPETree("TreeHYKTYPE", "TB_HYKNAME",2);
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
        GetHYXX(HYKNO);
        //vProcStatus = cPS_ADD;
        //SetControlBaseState();
    }
});

function TreeNodeClickCustom(event, treeId, treeNode) {
    switch (treeId) {
        case "TreeBGDD": $("#HF_BGDDDM").val(treeNode.sBGDDDM); break;
        case "TreeHYKTYPE":
            if ($("#list").datagrid("getData").rows.length > 0) {
                art.dialog({
                    title: "删除",
                    lock: true,
                    content: "是否清空数据？",
                    ok: function () {
                        $('#list').datagrid("loadData", { total: 0, rows: [] });
                        $("#HF_HYKTYPE").val(treeNode.iHYKTYPE);
                    },
                    okVal: '是',
                    cancelVal: '否',
                    cancel: true
                });
            }
            else {
                $("#HF_HYKTYPE").val(treeNode.iHYKTYPE);
            }
            break;
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
    Obj.sZY = $("#TB_ZY").val();
    Obj.fJE = 0;
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
    $("#HF_BGDDDM").val(Obj.sBGDDDM);
    $("#TB_BGDDMC").val(Obj.sBGDDMC);
    $("#TB_ZY").val(Obj.sZY);
    $("#HF_HYKTYPE").val(Obj.iHYKTYPE);
    $("#TB_HYKNAME").val(Obj.sHYKNAME);
    $('#list').datagrid('loadData', Obj.itemTable, "json");
    $('#list').datagrid("loaded");

    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);
}

function GetHYXX(HYKNO) {
    if (HYKNO != "") {
        //根据卡号查询信息
        var str = GetMZKXXData(0, HYKNO, "", "CRMDBMZK");
        var Obj = JSON.parse(str);
        $("#HF_HYKTYPE").val(Obj.iHYKTYPE);
        $("#TB_HYKNAME").val(Obj.sHYKNAME);

        $('#list').datagrid('appendRow', {
            sHYK_NO: Obj.sHYK_NO,
            iHYID: Obj.iHYID,
            sHY_NAME: Obj.sHY_NAME,
            fLJXFJE: Obj.fLJXFJE,
            fWCLJF: Obj.fWCLJF,
            fYE: Obj.fYE,
            iHYKTYPE: Obj.iHYKTYPE,
            sHYKNAME: Obj.sHYKNAME,          
        });

    }
}
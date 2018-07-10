vUrl = "../HYKGL.ashx";
var HYKNO = GetUrlParam("HYKNO");
var vCZK = GetUrlParam("mzk");


function InitGrid() {
    vColumnNames = ["会员卡号", "会员姓名", "hyid", "累计消费金额", "当前积分", "当前余额", "iHYKTYPE", "sHYKNAME", "iBJ_CHILD"],
    vColumnModel = [
          { name: "sHYK_NO", width: 100, },
          { name: "sHY_NAME", width: 60 },
          { name: "iHYID", hidden: true },
          { name: "fLJXFJE", width: 60, hidden: true },
          { name: 'fWCLJF', width: 60 },
          { name: "fYE", width: 60 },
          { name: 'iHYKTYPE', hidden: true, },
          { name: 'sHYKNAME', hidden: true, },
          { name: 'iBJ_CHILD', hidden: true },

    ];
};

$(document).ready(function () {
    FillBGDDTree("TreeBGDD", "TB_BGDDMC");
    FillHYKTYPETree("TreeHYKTYPE", "TB_HYKNAME");
    $("#AddItem").click(function () {
        if ($("#HF_HYKTYPE").val() == "") {
            ShowMessage("请选择卡类型", 3);
            return;
        }
        var DataArry = new Object();
        DataArry["iHYKTYPE"] = parseInt($("#HF_HYKTYPE").val());
        SelectHYK('list', DataArry, 'ListHYK', 'iHYID');

    });
    $("#DelItem").click(function () {
        DeleteRows("list");
    });

    if (HYKNO != "") {
        GetHYXX(HYKNO);
        vProcStatus = cPS_ADD;
        SetControlBaseState();
    }
});



function TreeNodeClickCustom(e, treeId, treeNode) {

    switch (treeId) {
        case "TreeBGDD":
            $("#HF_BGDDDM").val(treeNode.sBGDDDM);
            break;
        case "TreeHYKTYPE":
            if (treeNode.iZFBJ != 1) {
                ShowMessage("该卡类型不允许作废", 3);
                return;
            }

            if ($("#list").datagrid("getData").rows.length > 0) {
                var rowdata = $('#list').datagrid('getData').rows[0];
                if (rowdata.iHYKTYPE != treeNode.iHYKTYPE) {
                    ShowYesNoMessage("卡类型不一致，是否清空卡号列表？", function () {

                        $('#list').datagrid('loadData', { total: 0, rows: [] });
                        $("#HF_HYKTYPE").val(treeNode.iHYKTYPE);
                        hideMenu("menuContentHYKTYPE");
                    });
                }
            } else {
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
        if (vCZK == 1) {
            var str = GetHYXXData(0, HYKNO, "CRMDBMZK");
        } else {
            var str = GetHYXXData(0, HYKNO);
        }
        if (str == "null" || str == "") {
            ShowMessage("没有找到卡号", 3);
            return;
        }
        var Obj = JSON.parse(str);
        if (Obj.sHYK_NO == "") {
            ShowMessage("没有找到卡号", 3);
            return;
        }
        if (Obj.iBJ_ZF != 1) {
            ShowMessage("该卡类型不允许作废", 3);
            return;
        }
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
            iBJ_CHILD: Obj.iBJ_CHILD,
        });

    }
}
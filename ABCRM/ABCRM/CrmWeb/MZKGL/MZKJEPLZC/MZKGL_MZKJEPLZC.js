vUrl = "../MZKGL.ashx";
var vZRColumnNames;
var vZRColumnModel;

function InitGrid()
{
    vColumnNames = ["转出卡号", "iHYID_ZC", "YE", "转出金额"];
    vColumnModel = [
          { name: "sHYK_NO", width: 80, },
          { name: "iHYID", hidden: true },
          { name: "fYE", hidden: true },
          { name: "fZCJE", width: 60, editable: true, editor: 'text' },
    ];
    vZRColumnNames = ["转入卡号", "iHYID_ZR", "YE", "转入金额"];
    vZRColumnModel = [
          { name: "sHYK_NO", width: 80, },
          { name: "iHYID", hidden: true },
          { name: "fYE", hidden: true },
          { name: "fZCJE", width: 60, editable: true, editor: 'text' },
    ];
};

$(document).ready(function ()
{
    DrawGrid("List_OutCard", vColumnNames, vColumnModel, false);
    DrawGrid("List_InCard", vZRColumnNames, vZRColumnModel, false);

    $("#TB_CZMD").click(function ()
    {
        SelectMD("TB_CZMD", "HF_MDID", "zHF_MDID", false);
    });
    ////FillBGDDTree("TreeBGDD", "LB_BGDDMC", "menuContent");
    FillHYKTYPETree("TreeHYKTYPE", "TB_HYKNAME", 2);
    $("#AddOutCard").click(function ()
    {
        if ($("#HF_HYKTYPE").val() == "")
        {
            ShowMessage("请选择卡类型", 3);
            return;
        }
        var condData = new Object();
        condData["iHYKTYPE"] = $("#HF_HYKTYPE").val();
        condData["vZT"] = 1;
       // var checkRepeatField = ["iHYID"];
        SelectMZK("List_OutCard", condData, 'iHYID');
    });

    $("#AddInCard").click(function ()
    {
        if ($("#HF_HYKTYPE").val() == "")
        {
            ShowMessage("请选择卡类型", 3);
            return;
        }
        var condData = new Object();
        condData["iHYKTYPE"] = $("#HF_HYKTYPE").val();
        condData["vZT"] = 1;
        //var checkRepeatField = ["iHYID"];
        SelectMZK("List_InCard", condData, 'iHYID');
    });

    $("#DelOutCard").click(function ()
    {
        DeleteRows("List_OutCard");
    });
    $("#DelInCard").click(function ()
    {
        DeleteRows("List_InCard");
    });

});
function TreeNodeClickCustom(event, treeId, treeNode)
{
    switch (treeId)
    {
        case "TreeHYKTYPE":
            if (($("#List_InCard").datagrid("getData").rows.length > 0 || $("#List_OutCard").datagrid("getData").rows.length > 0) && $("#HF_HYKTYPE").val() != treeNode.iHYKTYPE)
            {
                ShowYesNoMessage("卡类型不一致，是否清空卡号列表？", function ()
                {
                    $('#List_OutCard').datagrid("loadData", { total: 0, rows: [] });
                    $('#List_InCard').datagrid("loadData", { total: 0, rows: [] });
                    $("#HF_HYKTYPE").val(treeNode.iHYKTYPE);
                });
            }
            else
            {
                $("#HF_HYKTYPE").val(treeNode.iHYKTYPE);
            }
            break;

    }
}



function SetControlState()
{

}
function compute(lisName, colName)
{
    var rows = $('#' + lisName + '').datagrid('getRows')//获取当前的数据行
    var total = 0;
    for (var i = 0; i < rows.length; i++)
    {
        total += parseFloat(rows[i]['' + colName + '']);
    }
    return total;
}

function IsValidData()
{

    if ($("#HF_MDID").val() == "")
    {
        ShowMessage("请选择操作门店");
        return false;
    }
    if ($("#HF_HYKTYPE").val() == "")
    {
        ShowMessage("请选择卡类型");
        return false;
    }
    var fZRJE = compute("List_InCard", "fZCJE");
    var fZCJE = compute("List_OutCard", "fZCJE");
    if (parseFloat(fZCJE) != parseFloat(fZRJE))
    {
        ShowMessage("转出转入金额必须一致");
        return false;
    }
    var rowZCIDs = $("#List_OutCard").datagrid('getRows');
    if (rowZCIDs.length <= 0)
    {
        ShowMessage("请选择转储卡");
        return false;
    }
    else
    {
        for (var i = 0; i < rowZCIDs.length; i++)
        {

            var rowData = rowZCIDs[i];
            if (rowData.fCZJE == "" || rowData.fCZJE <= 0)
            {
                ShowMessage("请输入正确的转储金额");
                return false;
            }
            else
            {
                if (parseFloat(rowData.fZCJE) > parseFloat(rowData.fYE))
                {
                    ShowMessage("转出金额不能大于余额");
                    return false;
                }
            }
        }

    }

    var rowZRIDs = $("#List_InCard").datagrid('getRows');
    if (rowZRIDs.length <= 0)
    {
        ShowMessage("请选择转入卡");
        return false;
    }
    else
    {
        for (var i = 0; i < rowZRIDs.length; i++)
        {
            var rowData = rowZRIDs[i];
            if (rowData.fCZJE == "" || rowData.fCZJE <= 0)
            {
                ShowMessage("请输入正确的转储金额");
                return false;
            }
        }

    }
    return true;
}

function SaveData()
{
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.iHYKTYPE = $("#HF_HYKTYPE").val();
    //  Obj.sBGDDDM = $("#HF_BGDDDM").val();
    Obj.iMDID = $("#HF_MDID").val();
    Obj.fZCJE = compute("List_OutCard", "fZCJE");
    Obj.fZRJE = compute("List_InCard", "fZCJE");
    Obj.iZCSL = $("#List_OutCard").datagrid('getRows').length;
    Obj.iZRSL = $("#List_InCard").datagrid('getRows').length;

    var lst_ZC = new Array();
    lst_ZC = $("#List_OutCard").datagrid("getData").rows;
    Obj.itemTable_ZC = lst_ZC;


    var lst_ZR = new Array();
    lst_ZR = $("#List_InCard").datagrid("getData").rows;
    Obj.itemTable_ZR = lst_ZR;


    Obj.sZY = $("#TB_ZY").val();
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    return Obj;
}

function ShowData(data)
{
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#HF_MDID").val(Obj.iMDID);
    $("#TB_CZMD").val(Obj.sMDMC);
    $("#HF_HYKTYPE").val(Obj.iHYKTYPE);
    $("#LB_HYKNAME").text(Obj.sHYKNAME);
    $("#LB_ZCJE").text(Obj.fZCJE);
    $("#LB_ZRJE").text(Obj.fZRJE);
    $("#LB_ZCSL").text(Obj.iZCSL);
    $("#LB_ZRSL").text(Obj.iZRSL);
    $("#TB_ZY").val(Obj.sZY);

    $('#List_OutCard').datagrid('loadData', Obj.itemTable_ZC, "json");
    $('#List_OutCard').datagrid("loaded");
    $('#List_InCard').datagrid('loadData', Obj.itemTable_ZR, "json");
    $('#List_InCard').datagrid("loaded");
    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);
}

function CustomerOpenDialogReturn(rows, listName, lst, array, CheckFieldId)
{
    var arrayList = [];
    var rowsout, rowsin;
    rowsout = $('#List_OutCard').datagrid("getRows");
    rowsin = $('#List_InCard').datagrid("getRows");
    for (var j = 0; j < rowsout.length; j++)
    {
        arrayList.push(rowsout[j]);
    }
    for (var j = 0; j < rowsin.length; j++)
    {
        arrayList.push(rowsin[j]);
    }
    if (listName == "List_OutCard")
    {
        for (var i = 0; i < lst.length; i++)
        {
            if (CheckReapet(arrayList, CheckFieldId, lst[i]))
            {
                $('#List_OutCard').datagrid('appendRow', {
                    sHYK_NO: lst[i].sHYK_NO,
                    iHYID: lst[i].iHYID,
                    fYE: lst[i].fYE,
                    fZCJE: lst[i].fYE,
                });
            }
        }
    }
    if (listName == "List_InCard")
    {
        for (var i = 0; i < lst.length; i++)
        {
            if (CheckReapet(arrayList, CheckFieldId, lst[i]))
            {
                $('#List_InCard').datagrid('appendRow', {
                    sHYK_NO: lst[i].sHYK_NO,
                    iHYID: lst[i].iHYID,
                    fZCJE: 0,
                });
            }

        }
    }

}
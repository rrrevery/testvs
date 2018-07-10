vUrl = "../GTPT.ashx";
var rowDataYHQ;
var rowDataKLX;
var vDialogName;
function InitGrid() {
    vColumnNames = [ '门店名称','MDID',];
    vColumnModel = [
             { name: 'sMDMC', width: 100, },
             { name: 'iMDID', hidden: true, },

    ];

    vYHQColumnNames = ['YHQID', 'MDID', '门店名称', '优惠券名称', '使用结束日期', '限制总数量', '总数量达到限制提示', '活动期每日限制数量', '活动期每日达到限制提示', '单会员每日限制数量', '单会员每日达到限制提示', '单会员活动期限制数量', '单会员活动期达到限制提示', '描述'];
    vYHQColumnModel = [
                { name: 'iYHQID', hidden: true, },
                { name: 'iMDID', hidden: true, },
                { name: 'sMDMC', width: 120,},
                { name: 'sYHQMC', width: 120, },
                { name: 'dSYJSRQ', width: 120, editor: 'text', },
                { name: 'iXZCS', width: 120, editor: 'text', },
                { name: 'sXZTS', width: 120, editor: 'text', },
                { name: 'iXZCS_DAY', width: 120, editor: 'text', },
                { name: 'sXZTS_DAY', width: 120, editor: 'text', },
                { name: 'iXZCS_DAYHY', width: 120, editor: 'text', },
                { name: 'sXZTS_DAYHY', width: 120, editor: 'text', },
                { name: 'iXZCS_HY', width: 120, editor: 'text', },
                { name: 'sXZTS_HY', width: 120, editor: 'text', },
                { name: 'sWXZY', width: 120, editor: 'text', },
    ];
    vKLXColumnNames = ['YHQID', 'MDID', '门店名称', '优惠券名称', 'HYKTYPE', '卡类型', '优惠券金额'];
    vKLXColumnModel = [
              { name: 'iYHQID', hidden: true, },
              { name: 'iMDID', hidden: true, },
              { name: 'sMDMC', width: 100, },
              { name: 'sYHQMC', width: 100, },
              { name: 'iHYKTYPE', hidden: true, },
              { name: 'sHYKNAME', width: 100, },
              { name: 'fJE', width: 100, editor: 'text', },
    ];
}
$(document).ready(function () {
    DrawGrid("listYHQ", vYHQColumnNames, vYHQColumnModel);
    DrawGrid("listKLX", vKLXColumnNames, vKLXColumnModel);

    $("#AddItem").click(function () {
        var dialogUrl = "../../CrmArt/ListWXMD/CrmArt_ListWXMD.aspx";
        vDialogName = "ListWXMD";
        var DataObject = new Object();
        OpenDialog(dialogUrl, "list", DataObject, vDialogName, "iMDID", false);

    });

    $("#DelItem").click(function () {
        DeleteRows("list");
    });


    $("#Addyhq").click(function () {
        if ($("#TB_SYJSRQ").val() == "") {
            ShowMessage("请输入使用结束日期", 3);
            return false;
        }
        if ($("#TB_XZCS").val() == "") {
            ShowMessage("请输入限制总数量", 3);
            return false;
        }
        if ($("#TB_XZTS").val() == "") {
            ShowMessage("请输入总数量达到限制提示", 3);
            return false;
        }
        if ($("#TB_XZCS_DAY").val() == "") {
            ShowMessage("请输入活动期每日限制数量", 3);
            return false;
        }
        if ($("#TB_XZTS_DAY").val() == "") {
            ShowMessage("请输入活动期每日达到限制提示", 3);
            return false;
        }
        if ($("#TB_XZCS_DAYHY").val() == "") {
            ShowMessage("请输入单会员每日限制数量", 3);
            return false;
        }
        if ($("#TB_XZTS_DAYHY").val() == "") {
            ShowMessage("请输入单会员每日达到限制提示", 3);
            return false;
        }
        if ($("#TB_XZCS_HY").val() == "") {
            ShowMessage("请输入单会员活动期限制数量", 3);
            return false;
        }
        if ($("#TB_XZTS_HY").val() == "") {
            ShowMessage("请输入单会员活动期达到限制提示", 3);
            return false;
        }
        if ($("#TB_WXZY").val() == "") {
            ShowMessage("请输入单会员活动描述", 3);
            return false;
        }

        rowDataYHQ = $("#list").datagrid("getSelections");
        if (rowDataYHQ.length == 0) {
            ShowMessage("还未选中要添加的门店", 3);
            return;
        }
        vDialogName = "ListYHQ";

        var DataArry = new Object();
        var checkRepeatField = ["iMDID","iYHQID"];
        SelectYHQList('listYHQ', DataArry, checkRepeatField, false);


    });


    $("#Delyhq").click(function () {
        DeleteRows("listYHQ");
    });

    $("#AddKLX").click(function () {
        if ($("#TB_JE").val() == "") {
            ShowMessage("请输入金额", 3);
            return false;
        }
        rowDataKLX = $("#listYHQ").datagrid("getSelections");
        if (rowDataKLX.length == 0) {
            ShowMessage("还未选中要添加的优惠券", 3);
            return;
        }

        var DataArry = new Object();

        vDialogName = "ListKLX";
        var DataArry = new Object();
        var checkRepeatField = ["iMDID", "iYHQID","iHYKTYPE"];
        SelectKLXList('listKLX', DataArry, checkRepeatField, false);

    });


    $("#DelKLX").click(function () {
        DeleteRows("listKLX");
    });



})


function SetControlState() {

    $("#status-bar").hide();
    $("#B_Exec").hide();
}

function SaveData() {

    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sGZMC = $("#TB_GZMC").val();
    Obj.sNOTE = $("#TB_GZJJ").val();

    var lst = new Array();
    lst = $("#list").datagrid("getRows");
    Obj.itemTable = lst;

    var lstYHQ = new Array();
    lstYHQ = $("#listYHQ").datagrid("getRows");
    Obj.itemTable2 = lstYHQ;
    var lstKLX = new Array();
    var lstKLX = $("#listKLX").datagrid("getRows");
    Obj.itemTable3 = lstKLX;

    return Obj;
}




function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#TB_GZMC").val(Obj.sGZMC);
    $("#TB_GZJJ").val(Obj.sNOTE);

    $('#list').datagrid("loadData", { total: 0, rows: [] });
    $('#listYHQ').datagrid("loadData", { total: 0, rows: [] });
    $('#listKLX').datagrid("loadData", { total: 0, rows: [] });

    $('#list').datagrid('loadData', Obj.itemTable, "json");
    $('#list').datagrid("loaded");

    $('#listYHQ').datagrid('loadData', Obj.itemTable2, "json");
    $('#listYHQ').datagrid("loaded");

    $('#listKLX').datagrid('loadData', Obj.itemTable3, "json");
    $('#listKLX').datagrid("loaded");

}


function IsValidData() {

    if ($("#TB_GZMC").val() == "") {
        ShowMessage("请输入规则名称", 3);
        return false;
    }
    if ($("#TB_GZJJ").val() == "") {
        ShowMessage("请输入规则简介", 3);
        return false;
    }
    return true;
}


function CustomerOpenDialogReturn(rows, listName, lst, array, CheckFieldId) {
    switch (vDialogName) {
        case "ListWXMD":
            if (rows.length == 0) {
                $('#' + listName).datagrid('loadData', lst, "json");
            }
            else {
                for (var i = 0; i < lst.length; i++) {
                    if (CheckReapet(array, CheckFieldId, lst[i])) {
                        $('#' + listName).datagrid('appendRow', lst[i]);
                    }
                }
            }
            break;
        case "ListYHQ":
            for (var j = 0; j < rowDataYHQ.length; j++) {

                for (var i = 0; i < lst.length; i++) {
                    lst[i].iMDID = rowDataYHQ[j].iMDID;
                    lst[i].sMDMC = rowDataYHQ[j].sMDMC;
                    if (CheckReapet(array, CheckFieldId, lst[i])) {                
                        $('#' + listName).datagrid('appendRow', {
                            iYHQID: lst[i].iYHQID,
                            sYHQMC: lst[i].sYHQMC,
                            iMDID: lst[i].iMDID,
                            sMDMC: lst[i].sMDMC,
                            dSYJSRQ: $("#TB_SYJSRQ").val(),
                            iXZCS: $("#TB_XZCS").val(),
                            sXZTS: $("#TB_XZTS").val(),
                            iXZCS_DAY: $("#TB_XZCS_DAY").val(),
                            sXZTS_DAY: $("#TB_XZTS_DAY").val(),
                            iXZCS_DAYHY: $("#TB_XZCS_DAYHY").val(),
                            sXZTS_DAYHY: $("#TB_XZTS_DAYHY").val(),
                            iXZCS_HY: $("#TB_XZCS_HY").val(),
                            sXZTS_HY: $("#TB_XZTS_HY").val(),
                            sWXZY: $("#TB_WXZY").val()
                        });
                    }
                }
            }
            break;
        case "ListKLX":
            for (var j = 0; j < rowDataKLX.length; j++) {
                for (var i = 0; i < lst.length; i++) {
                    lst[i].iMDID = rowDataKLX[j].iMDID;
                    lst[i].sMDMC = rowDataKLX[j].sMDMC;
                    lst[i].iYHQID = rowDataKLX[j].iYHQID;
                    lst[i].sYHQMC = rowDataKLX[j].sYHQMC;
                    if (CheckReapet(array, CheckFieldId, lst[i])) {
                 
                        $('#' + listName).datagrid('appendRow', {
                            iYHQID: lst[i].iYHQID,
                            sYHQMC: lst[i].sYHQMC,
                            iMDID: lst[i].iMDID,
                            sMDMC: lst[i].sMDMC,
                            iHYKTYPE: lst[i].iJLBH,
                            sHYKNAME: lst[i].sHYKNAME,
                            fJE: $("#TB_JE").val()
                        });
                    }
                }
            }
            break;
    }
}




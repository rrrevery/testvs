vUrl = "../GTPT.ashx";
var iGZID = 0;
var vKLXColumnNames;
var vKLXColumnModel;
function InitGrid() {
    vColumnNames = ['礼品ID', '礼品名称', '兑换成功提示', '单会员单礼品发放限制', '单会员单礼品每日发放限制', '活动期内单礼品发放限制', '活动期内每日礼品发放限制', '单会员单礼品发放限制提示', '单会员单礼品每日发放限制提示', '活动期内单礼品发放限制提示', '活动期内每日礼品发放限制提示'];
    vColumnModel = [
             { name: 'iLPID', hidden: true },
          { name: 'sLPMC', width: 100 },
          { name: 'sWXZY', width: 120, editor: 'text', },
          { name: 'iXZCS_HY', width: 120, editor: 'text', },
          { name: 'iXZCS_DAY_HY', width: 120, editor: 'text', },
          { name: 'iXZCS', width: 120, editor: 'text', },
          { name: 'iXZCS_DAY', width: 120, editor: 'text', },
          { name: 'sXZCS_HY_TS', width: 120, editor: 'text', },
          { name: 'sXZCS_DAY_HY_TS', width: 300, editor: 'text', },
          { name: 'sXZCS_TS', width: 300, editor: 'text', },
          { name: 'sXZCS_DAY_TS', width: 300, editor: 'text', },

    ];
    vKLXColumnNames = ['礼品ID', '礼品名称', '会员卡类型ID', '卡类型', '兑换积分'];
    vKLXColumnModel = [
        { name: 'iLPID', hidden: true },
          { name: 'sLPMC', width: 100, },
          { name: 'iHYKTYPE', hidden: true },
          { name: 'sHYKNAME', width: 100 },
          { name: 'fLPJF', width: 100, editor: 'text', },
    ];
}

function SetControlState() {
    $("#status-bar").hide();
    $("#B_Exec").hide();
};

function IsValidData() {
    if ($("#TB_GZMC").val() == "") {
        ShowMessage("请输入规则名称");
        return false;
    }
    var rowData = $("#list").datagrid("getData").rows;
    if (rowData.length == 0) {
        ShowMessage("请添加礼品信息表");
        return false;
    }
    else {
        for (var  i = 0; i < rowData.length  ; i++) {           
              
            if (rowData[i].iXZCS_HY == "") {
                    ShowMessage("请输入单会员单礼品发放限制数量");
                    return false;
                }
            if (rowData[i].iXZCS_DAY_HY == "") {
                    ShowMessage("请输入单会员单礼品每日发放限制数量");
                    return false;
                }
            if (rowData[i].iXZCS == "") {
                    ShowMessage("请输入活动期内单礼品发放限制数量");
                    return false;
                }
            if (rowData[i].iXZCS_DAY == "") {
                    ShowMessage("请输入活动期内每日礼品发放限制数量");
                    return false;
                }
            if (rowData[i].sWXZY == "") {
                    ShowMessage("请输入兑换成功提示");
                    return false;
                }
            if (rowData[i].iLPID == "") {
                    ShowMessage("请添加礼品");
                    return false;
                }
            if (rowData[i].iXZCS_HY < 0) {
                    ShowMessage("单会员单礼品每日发放限制数量不能小于0");
                    return false;
                }
            if (rowData[i].iXZCS_DAY_HY < 0) {
                    ShowMessage("单会员单礼品发放限制数量不能小于0");
                    return false;
                }
            if (rowData[i].iXZCS < 0) {
                    ShowMessage("活动期内单礼品发放限制数量不能小于0");
                    return false;
                }
            if (rowData[i].iXZCS_DAY < 0) {
                    ShowMessage("活动期内每日礼品发放限制数量不能小于0");
                    return false;
                }
            
        }
    }
    return true;
};

$(document).ready(function () {
    DrawGrid("list2", vKLXColumnNames, vKLXColumnModel,false);
    $("#Add_LP").click(function (){
        var DataArry = new Object();
        DataArry["iDJLX"] = 0;
        SelectLP('list', DataArry, 'iLPID');

    });

    $("#Del_LP").click(function () {
        DeleteRows("list");

    });

    $("#Add_KLX").click(function () {
        var rowData = $("#list").datagrid("getSelections");
        if (rowData.length == 0) {
            ShowMessage("还未选中要添加礼品", 3);
            return;
        }
        var DataArry = new Object();
        var checkRepeatField = ["iLBID", "iHYKTYPE"];
        SelectKLXList('list2', DataArry, checkRepeatField,false);

   
    });

    $("#Del_KLX").click(function () {
        DeleteRows("list2");

    });
    document.getElementById("B_Update").onclick = function () {
        vProcStatus = cPS_MODIFY;

        var str = GetJFDHLPDYD(iGZID);
        var data = JSON.parse(str);
        var gzid = data.iGZID;
        if (gzid > 0) {
            ShowYesNoMessage("此规则在已启动的积分兑换礼品定义单中被调用，如要修改将重新启动定义单，是否继续修改？", function () {
                {

                    SetControlBaseState();
                    document.getElementById("B_Delete").disabled = true;
                    document.getElementById("B_Update").disabled = true;
                }
            });

        }
        else {

            SetControlBaseState();
            document.getElementById("B_Delete").disabled = true;
            document.getElementById("B_Update").disabled = true;
        }

    }





});
function CustomerOpenDialogReturn(rows, listName, lst, array, CheckFieldId) {
    if (listName == "list") {

        if (rows.length == 0) {
            $('#' + listName).datagrid('loadData', lst, "json");
        }
        else {
            for (var i = 0; i < lst.length; i++) {
                if (CheckReapet(array, CheckFieldId, lst[i])) {//[CheckFieldId]
                    $('#' + listName).datagrid('appendRow', lst[i]);
                }
            }
        }
    }


    else {

        for (var i = 0; i < lst.length; i++) {
            var rows = $('#list').datagrid('getSelections');
            for (var j = 0; j < rows.length; j++) {
                lst[i].iLPID = rows[j].iLPID;
                lst[i].sLPMC = rows[j].sLPMC;
                if (CheckReapet(array, CheckFieldId, lst[i])) {
                    $('#list2').datagrid('appendRow', {
                        iHYKTYPE: lst[i].iHYKTYPE,
                        sHYKNAME: lst[i].sHYKNAME,
                        iLPID: rows[j].iLPID,
                        sLPMC: rows[j].sLPMC,
                    });
                }
            }
        }

    }
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sGZMC = $("#TB_GZMC").val();

    var lst = new Array();

    var lst = $("#list").datagrid("getData").rows;
    Obj.itemLP = lst;

    var lst2 = new Array();

    var lst2 = $("#list2").datagrid("getData").rows;
    Obj.itemKLX = lst2;


    return Obj;
};

function ShowData(data) {
    var Obj = JSON.parse(data);
    iGZID = Obj.iJLBH;
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#TB_GZMC").val(Obj.sGZMC);

    $('#list').datagrid('loadData', Obj.itemLP, "json");
    $('#list').datagrid("loaded");
    $('#list2').datagrid('loadData', Obj.itemKLX, "json");
    $('#list2').datagrid("loaded");

};
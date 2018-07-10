vUrl = "../GTPT.ashx";



var KLX = "";
var Name = "";
var id = 0;


var iGZID = 0;
var vZPJDColumnNames;
var vZPJDColumnModel;
function InitGrid() {
    vColumnNames = ['LBID', '奖品名称', 'JC', '奖品级次', '发放总数量', '温馨提示'];
    vColumnModel = [
               { name: 'iLBID', hidden: true },
               { name: 'sLBMC', width: 60, },
               { name: 'iJC', hidden: true },
               { name: 'sMC', width: 60, },
               { name: 'iFFZSL', editor: 'text' },
               { name: 'sWXZY', width: 100, editor: 'text', },
    ];
    vZPJDColumnNames = ['奖品级次', 'iXH', '转盘角度', ];
    vZPJDColumnModel = [
              { name: 'sMC', width: 120 },
              { name: 'iJC', hidden: true },
              { name: 'iVAL', width: 60 },

    ];

}


function SetControlState() {

    $("#status-bar").hide();
    $("#B_Exec").hide();
}
function selectValueChange(ruleId) {
    if (parseInt(ruleId) == 2) {
        $("#zpRule").show();
        $("#Add").show();
        $("#Del").show();
        $("#tab_2").show();
    }
    else {
        $("#zpRule").hide();
        $("#Add").hide();
        $("#Del").hide();
        $("#tab_2").hide();
    }
}
function IsValidData() {
    if ($("#TB_GZMC").val() == "") {
        ShowMessage("请输入规则名称");
        return false;
    }
    if ($("#DDL_GZLX").val() == "") {
        ShowMessage("请选择规则类型");
        return false;
    }

    if ($("#DDL_QD").val() == "") {
        ShowMessage("请选择渠道");
        return false;
    }
    if ($("#TB_XZCS").val() == "" || isNaN($("#TB_XZCS").val())) {
        ShowMessage("请输入正确的活动期内限制次数");
        return false;
    }
    if ($("#TB_XZTS").val() == "") {
        ShowMessage("请输入活动期内限制提示");
        return false;
    }
    if ($("#TB_XZCS_HY").val() == "" || isNaN($("#TB_XZCS_HY").val())) {
        ShowMessage("请输入正确的活动期内单会员限制次数");
        return false;
    }
    if ($("#TB_XZTS_HY").val() == "") {
        ShowMessage("请输入活动期内单会员限制提示");
        return false;
    }
    if ($("#TB_XZCS_DAY_HY").val() == "" || isNaN($("#TB_XZCS_DAY_HY").val())) {
        ShowMessage("请输入正确的单会员每日限制次数");
        return false;
    }
    if ($("#TB_XZTS_DAY_HY").val() == "") {
        ShowMessage("请输入单会员每日限制提示");
        return false;
    }

    var rowData1 = $("#list").datagrid("getData").rows;

    if (rowData1.length == 0) {
        ShowMessage("请添礼品表");
        return false;
    } else {
        for (var i = 0; i < rowData1.length  ; i++) {
            if (rowData1[i].iFFZSL == 0 || rowData1[i].iFFZSL == "" || isNaN(rowData1[i].iFFZSL)) {
                ShowMessage("请输入发放总数量");
                return false;
            }
            if (rowData1[i].sWXZY =="") {
                ShowMessage("请输入温馨提示");
                return false;
            }
        }
    }

    return true;
}
$(document).ready(function () {
    DrawGrid("listJD", vZPJDColumnNames, vZPJDColumnModel, false);

    $('#DDL_GZLX').change(function () {
        selectValueChange($("#DDL_GZLX").find("option:selected").val())
    });
    document.getElementById("B_Update").onclick = function () {
        vProcStatus = cPS_MODIFY;

        var str = GetCJDYD(iGZID);
        var data = JSON.parse(str);
        var gzid = data.iGZID;
        if (gzid > 0) {
            ShowYesNoMessage("此规则在已启动的微信礼品发放规则定义单中被调用，如要修改将重新启动定义单，是否继续修改？", function () {
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

    FillJPJC("DDL_JPJC");
    function ShowYesNoMessage(sMsg, okfunc) {
        cls = "errorlog";
        var top = $(window).height() * 0.7;
        var width = $(window).width() * 0.7;
        art.dialog({
            lock: true,
            top: top,
            content: "<div class='bfdialog " + cls + "' style='width:" + width + "px'>" + sMsg + "</div>",
            ok: okfunc,
            okVal: '是',
            cancelVal: '否',
            cancel: true
        });
    }

    $("#AddItem").click(function () {
        if ($("#TB_GZMC").val() == "") {
            ShowMessage("请输入规则名称");
            return false;
        }
        if ($("#DDL_GZLX").val() == "") {
            ShowMessage("请选择规则类型");
            return false;
        }
        if ($("#DDL_QD").val() == "") {
            ShowMessage("请选择渠道");
            return false;
        }
        if ($("#TB_XZCS").val() == "") {
            ShowMessage("请输入活动期内限制次数");
            return false;
        }
        if ($("#TB_XZCS_HY").val() == "") {
            ShowMessage("请输入活动期内单会员限制次数");
            return false;
        }
        if ($("#TB_XZCS_DAY_HY").val() == "") {
            ShowMessage("请输入单会员每日限制次数");
            return false;
        }
        if ($("#DDL_JPJC").val() == "") {
            ShowMessage("请选择奖品级次");
            return false;
        }

        var DataArry = new Object();
        var checkRepeatField = ["iJC", "iLBID"];
        SelectLBList('list', DataArry, checkRepeatField, false);

    });

    $("#DelItem").click(function () {
        DeleteRows("list");

    });

    $("#Add").click(function () {

        if ($("#TB_VAL").val() == "") {
            ShowMessage("请输入转盘角度！");
            return false;
        }
        var rows = $('#list').datagrid('getSelections');
        //var rowData1 = $("#list").datagrid("getData").rows;

        if (rows.length <= 0) {
            ShowMessage("还未选中要添加奖品级次对应转盘角度");
            return;
        }

        for (var j = 0; j < rows.length; j++) {

            $('#listJD').datagrid('appendRow', {

                iVAL: $("#TB_VAL").val(),
                sMC: rows[j].sMC,
                iJC: rows[j].iJC,

            });
        }
    });

    $("#Del").click(function () {
        DeleteRows("listJD");
    });
    
})

function SaveData() {

    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sGZMC = $("#TB_GZMC").val();
    Obj.iXZCS = $("#TB_XZCS").val();
    Obj.iXZCS_HY = $("#TB_XZCS_HY").val();
    Obj.iXZCS_DAY_HY = $("#TB_XZCS_DAY_HY").val();
    Obj.sXZTS = $("#TB_XZTS").val();
    Obj.sXZTS_HY = $("#TB_XZTS_HY").val();
    Obj.sXZTS_DAY_HY = $("#TB_XZTS_DAY_HY").val();
    Obj.sGZLXMC = $("#DDL_GZLX").val();
    Obj.iGZLX = GetSelectValue("DDL_GZLX");
    Obj.iCHANNELID = GetSelectValue("DDL_QD");
    var lst = new Array();

    var lst = $("#list").datagrid("getData").rows;
    Obj.itemTable = lst;
    var lstJD = new Array();
    lstJD = $("#listJD").datagrid("getData").rows;

    Obj.itemTableJD = lstJD;
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    Obj.status = vProcStatus
    return Obj;
}


function ShowData(data) {
    var Obj = JSON.parse(data);
    iGZID = Obj.iJLBH;
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#TB_GZMC").val(Obj.sGZMC);
    $("#DDL_GZLX").val(Obj.iGZLX);
    $("#DDL_QD").val(Obj.iCHANNELID);
    selectValueChange(Obj.iGZLX);
    $("#TB_XZCS").val(Obj.iXZCS);
    $("#TB_XZCS_HY").val(Obj.iXZCS_HY);
    $("#TB_XZCS_DAY_HY").val(Obj.iXZCS_DAY_HY);
    $("#TB_XZTS").val(Obj.sXZTS);
    $("#TB_XZTS_HY").val(Obj.sXZTS_HY);
    $("#TB_XZTS_DAY_HY").val(Obj.sXZTS_DAY_HY)
    $('#listJD').datagrid('loadData', Obj.itemTableJD, "json");
    $('#listJD').datagrid("loaded");
    $('#list').datagrid('loadData', Obj.itemTable, "json");
    $('#list').datagrid("loaded");
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#LB_DJSJ").text(Obj.dDJSJ);

    $("#LB_ZZRQ").text(Obj.dZZRQ);

}
function CustomerOpenDialogReturn(rows, listName, lst, array, CheckFieldId) {
    for (var i = 0; i < lst.length; i++) {
        var A = GetSelectValue("DDL_JPJC");
        var B = $("#DDL_JPJC [value=" + GetSelectValue("DDL_JPJC") + "]").text()
        lst[i].iJC = GetSelectValue("DDL_JPJC");
        if (lst[i].iLBID!=0){
            if (CheckReapet(array, CheckFieldId, lst[i])) {
                $('#list').datagrid('appendRow', {
                    iLBID: lst[i].iLBID,
                    sLBMC: lst[i].sLBMC,
                    iJC: GetSelectValue("DDL_JPJC"),
                    sMC: $("#DDL_JPJC [value=" + GetSelectValue("DDL_JPJC") + "]").text(),
                });
            }
    }

    }
}








vUrl = "../GTPT.ashx";
var vLXQDColumnNames;
var vLXQDColumnModel;
function InitGrid() {
    vColumnNames = ['HYKTYPE', '卡类型', '赠送积分'];
    vColumnModel = [
          { name: 'iHYKTYPE', hidden: true },
           { name: 'sHYKNAME', width: 100, },
           { name: 'iZSJF', editor: 'text' },
    ];
    vLXQDColumnNames = ['HYKTYPES', '卡类型', '赠送积分', '累计天数'];
    vLXQDColumnModel = [

        { name: 'iHYKTYPES', hidden: true },
        { name: 'sHYKNAMES', width: 100, },
        { name: 'iZSJFS', editor: 'text' },
        { name: 'iCOUNTS', editor: 'text' },
    ];
}


function SetControlState() {

    $("#B_Start").show();
    $("#B_Stop").show();

    if ($("#LB_ZXRMC").text() != "") {
        $("#QDR").show();
        $("#QDSJ").show();

    }
    if ($("#LB_QDRMC").text() != "") {
        $("#ZZR").show();
        $("#ZZSJ").show();
    }
    if ($("#LB_ZZRMC").text() != "") {
        document.getElementById("B_Stop").disabled = true;

    }




}

function IsValidData() {
    if ($("#TB_JSRQ").val() == "") {
        ShowMessage("请输入选择结束日期");
        return false;
    }
    if ($("#TB_KSRQ").val() == "") {
        ShowMessage("请输入选择开始日期");
        return false;
    }
    if ($("#TB_KSRQ").val() > $("#TB_JSRQ").val()) {
        ShowMessage("开始日期不得大于结束日期");

        return false;
    }
    if ($("#TB_COUNTS").val() == "") {
        ShowMessage("请输入连续签到天数");
        return false;
    }
    var rowData1 = $("#list").datagrid("getData").rows;
    var rowData2 = $("#list2").datagrid("getData").rows;

    if (rowData1.length == 0) {
        ShowMessage("请添加卡类型表");
        return false;
    } else {
        for (var i = 0; i < rowData1.length  ; i++) {
            if (rowData1[i].iZSJF == 0) {
                ShowMessage("赠送积分不能为零");
                return false;
            }    
        }
    }
    for (var i = 0; i < rowData2.length  ; i++) {
        if (rowData2[i].iCOUNTS < 2) {
            ShowMessage("累计天数不能小于2天");
            return false;
        }
        if (rowData2[i].iCOUNTS > Number($("#TB_COUNTS").val())) {
            ShowMessage("累计天数要小于连续签到天数");
            return false;
        }
    }
    return true;
}



$(document).ready(function () {
    DrawGrid("list2", vLXQDColumnNames, vLXQDColumnModel, false);
    $("#AddItem").click(function () {
        if ($("#TB_JSRQ").val() == "") {
            ShowMessage("请输入选择结束日期");
            return false;
        }
        if ($("#TB_KSRQ").val() == "") {
            ShowMessage("请输入选择开始日期");

            return false;
        }
        var dKSRQ = $("#TB_KSRQ").val();
        var dJSRQ = $("#TB_JSRQ").val();
        vProcStatus = cPS_MODIFY;

        var str = GetWXSIGNData(dKSRQ, dJSRQ);
        var data = JSON.parse(str);
        var jlbh1 = data.iJLBH;
        if (jlbh1 > 0) {
            ShowYesNoMessage("在此时间段内已经设定签到积分规则！", function () {
                {

                    document.getElementById("B_Save").disabled = true;
                    document.getElementById("AddItem").disabled = true;
                    return;
                }
            });

        }
        else {
            var DataArry = new Object();
            SelectKLXList('list', DataArry, 'iHYKTYPE', false);

        }

    });

    $("#DelItem").click(function () {
        DeleteRows("list");
    });

    $("#Add").click(function () {
        var rows = $('#list').datagrid('getSelections');
        if (rows.length <= 0) {
            ShowMessage("还未选中要添加连续签到的卡类型");
            return;
        }

        for (var j = 0; j < rows.length; j++) {
            if (CheckReapetklx(rows[j].iHYKTYPE)) {
                $('#list2').datagrid('appendRow', {
                    iHYKTYPES: rows[j].iHYKTYPE,
                    sHYKNAMES: rows[j].sHYKNAME,

                });
            }
        }

    });

    $("#Del").click(function () {
        DeleteRows("list2");
    });

})


function CheckReapetklx(hyktype) {
    var boolReapet = true;
    var lst = $("#list2").datagrid("getRows");
    var d=lst.length;
    //var row = $("#list2").datagrid("getRows").rows;

    if (d> 0) {
        for (var i = 0; i < d ; i++) {
            var rowData = lst[i];

            if (rowData.iHYKTYPES == hyktype) {
                boolReapet = false;
            }
        }
    }
    return boolReapet;

}
function SaveData() {

    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.dKSRQ = $("#TB_KSRQ").val();
    Obj.dJSRQ = $("#TB_JSRQ").val();
    Obj.iCOUNTS = $("#TB_COUNTS").val();
    Obj.sCONTENT = $("#TB_CONTENT").val();
    var lst = new Array();
    var lst = $("#list").datagrid("getRows");
    Obj.itemTable = lst;
    var lst1 = new Array();
    lst1 = $("#list2").datagrid("getRows");
    Obj.itemTable1 = lst1;
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    return Obj;
}

function StartClick() {
    ShowYesNoMessage("启动本单执行将会终止正在启动的单据，是否覆盖继续？", function () {
        if (posttosever(SaveDataBase(), vUrl, "Start") == true) {
            vProcStatus = cPS_BROWSE;
            ShowDataBase(vJLBH);
            SetControlBaseState();
        }
    });
};
function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#TB_KSRQ").val(Obj.dKSRQ);
    $("#TB_JSRQ").val(Obj.dJSRQ);
    $("#TB_COUNTS").val(Obj.iCOUNTS);
    $("#TB_CONTENT").val(Obj.sCONTENT);
    $('#list2').datagrid('loadData', Obj.itemTable1, "json");
    $('#list2').datagrid("loaded");
    $('#list').datagrid('loadData', Obj.itemTable, "json");
    $('#list').datagrid("loaded");
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#LB_ZXRQ").text(Obj.dZXRQ);
    $("#HF_QDR").val(Obj.iQDR);
    $("#LB_QDRMC").text(Obj.sQDRMC);
    $("#LB_QDSJ").text(Obj.dQDRQ);
    $("#HF_ZZR").val(Obj.iZZR);
    $("#LB_ZZRMC").text(Obj.sZZRMC);
    $("#LB_ZZRQ").text(Obj.dZZRQ);

}





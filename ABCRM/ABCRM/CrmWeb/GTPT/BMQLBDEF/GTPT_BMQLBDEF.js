vUrl = "../GTPT.ashx";

function InitGrid() {
    vColumnNames = ['编码券id', '编码券名称', '面值', '名称'];
    vColumnModel = [
               { name: 'iBMQID', hidden: true },
                { name: 'sBMQMC', width: 180 },
                { name: 'fMZJE', width: 100, editor: 'text' },
                { name: 'sNAME', hidden: true },

    ];
}



$(document).ready(function () {

    $("#jlbh .dv_sub_left").html("礼包ID");


    $("#AddMK").click(function () {
        var DataArry = new Object();
        SelectBMQList('list', DataArry, 'iBMQID', false);


    });
  
    $("#DelMK").click(function () {
        DeleteRows("list");

    });
 
    document.getElementById("B_Update").onclick = function () {
        vProcStatus = cPS_MODIFY;
        var str = GetBMQFFD(iLBID);
        var data = JSON.parse(str);
        var jlbh1 = data.iJLBH;
        if (jlbh1 > 0) {
            ShowYesNoMessage("在此时间段内已经设定签到积分规则！", function () {
                {

                    document.getElementById("B_Save").disabled = true;
                    return;
                }
            });

        }
        else {
                            SetControlBaseState();
                            document.getElementById("B_Delete").disabled = true;
                            document.getElementById("B_Update").disabled = true;

        }
    };

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
    Obj.sLBMC = $("#TB_LBMC").val();
  
    Obj.sSYSM = $("#TB_SYSM").val(); 
    var lst = new Array();
    lst = $("#list").datagrid("getData").rows;
    Obj.itemTable = lst;
    return Obj;
}



function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    iLBID = Obj.iJLBH;

    $("#TB_LBMC").val(Obj.sLBMC);
    $("#HF_HYKTYPE").val(Obj.iHYKTYPE);
    $("#TB_SYSM").val(Obj.sSYSM);

    $('#list').datagrid('loadData', Obj.itemTable, "json");
    $('#list').datagrid("loaded");
}


function IsValidData() {
    if ($("#TB_LBMC").val() == "") {
        ShowMessage("请输入礼包名称");
        return false;
    }
 
    var rowData2 = $("#list").datagrid("getData").rows;
    if (rowData2.length == 0) {
        ShowMessage("请先添加编码券");
        return false;
    }
    return true;
}

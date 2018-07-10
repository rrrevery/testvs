vUrl = "../GTPT.ashx";
var bj;
var idsc;

function InitGrid() {
    vColumnNames = ['标签id', '标签名称', '粉丝数'];
    vColumnModel = [
            { name: 'iTAGID', width: 50, },
            { name: 'sTAGMC', width: 100, },
            { name: 'iCOUNT', width: 50, },

    ];
}
$(document).ready(function () {
    //$("#a").hide();
    $("#B_Exec").hide();
    $("#status-bar").hide();
    QBBQ();
    document.getElementById("B_POST").onclick = postTowx;
    document.getElementById("B_QBBQ").onclick = QBBQ;


});
function IsValidInputData() {
    return true;
}

function QBBQ() {
    //$("#a").show();
    $('#list').datagrid("loadData", { total: 0, rows: [] });//清空
    var sjson = { "name": $("#TB_BQMC").val() }
    $.ajax({
        type: "post",
        url: "../GTPT_WXBQ.ashx?requestType=groups&mode=GetTagList&PUBLICID=" + iWXPID + "&PUBLICIF=" + sWXPIF,
        data: { json: JSON.stringify(sjson) },
        success: function (data) {
            var dataArray = new Array();
            dataArray = JSON.parse(data);

            for (var j = 0; j < dataArray.length; j++) {
                
            
                        $('#list').datagrid('appendRow', {
                            iTAGID: dataArray[j].id,
                            sTAGMC: dataArray[j].name,
                            iCOUNT: dataArray[j].count,
                        });
                    
                }
            



        },
        error: function (data) {
            ShowMessage(data)
        }
    });


}
function postTowx() {
    if ($("#selectPublicID").combobox("getValue") == "选择公众号") {
        ShowMessage("请选择公众号");
        return;
    }

    if ($("#TB_JLBH").val() == null) {
        ShowMessage("还未定义微信标签！无法发布");
        return;
    }
  
    if (bj == 2)
    {     
        var sjson = { "tag": { "id": Number($("#TB_TAGID").val()), "name": $("#TB_BQMC").val() } }
        $.ajax({
            type: "post",
            async: false,
            url: "../GTPT_WXBQ.ashx?requestType=groups&mode=UpdateTag&PUBLICID=" + iWXPID + "&PUBLICIF=" + sWXPIF,
            data: { json: JSON.stringify(sjson) },
            success: function (data) {
                if (data=="ok") {
                    ShowMessage("标签修改成功");
                } else {
                    ShowMessage(data);
                }
            },
            error: function (data) {
                ShowMessage(data)
            }
        });
        QBBQ()
        document.getElementById("B_Update").disabled = false;
        document.getElementById("B_Insert").disabled = false;

    }

    else if (bj == 3) {
        var sjson = { "tag": { "id": Number($("#TB_TAGID").val()), "name": $("#TB_BQMC").val() } }
        $.ajax({
            type: "post",
            async: false,

            url: "../GTPT_WXBQ.ashx?requestType=groups&mode=DeleteTag&PUBLICID=" + iWXPID + "&PUBLICIF=" + sWXPIF + "&id=" +idsc,
            data: { json: JSON.stringify(sjson) },
            success: function (data) {
                if (data == "ok") {
                    ShowMessage("标签删除成功");
                } else {
                    ShowMessage(data);
                }
            },
            error: function (data) {
                ShowMessage(data)
            }
        });

        QBBQ();

    }

    else {
        var sjson = { "name": $("#TB_BQMC").val() }
        $.ajax({
            type: "post",
            async: false,
            url: "../GTPT_WXBQ.ashx?requestType=groups&mode=CreateTag&PUBLICID=" + iWXPID + "&PUBLICIF=" + sWXPIF + "&name=" + encodeURIComponent($("#TB_BQMC").val()),
            data: { json: JSON.stringify(sjson) },
            success: function (data) {
                if (data != "" && data != null && data != undefined) {
                    ShowMessage("标签添加成功");
                } else {
                    ShowMessage(data);
                }
            },
            error: function (data) {
                ShowMessage(data)
            }
        });


        QBBQ();



    }
}

function IsValidData() {
     
    return true;
}
function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sBQMC = $("#TB_BQMC").val();
    Obj.iTAGID=$("#TB_TAGID").val();
    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#TB_BQMC").val(Obj.sBQMC);
    $("#TB_TAGID").val(Obj.iTAGID);
}
function AddCustomerButton() {
    AddToolButtons("发布", "B_POST");
    AddToolButtons("全部标签", "B_QBBQ");

};


function UpdateClickCustom() {
    var rows = $('#list').datagrid('getSelections');  
    if (rows.length !=1) {
        ShowMessage("请选择一行");
        return false;
    }
    document.getElementById("B_Save").disabled = true;

    bj = 2;
};

function InsertClickCustom() {
    bj = 1;
}

function DeleteClick() {

    var rows = $('#list').datagrid('getSelections');
    if (rows.length != 1) {
        ShowMessage("请选择一行");
        return false;
    }

  idsc = Number($("#TB_TAGID").val());
    bj = 3;
    ShowYesNoMessage("是否删除？", function () {
        if (posttosever(SaveDataBase(), vUrl, "Delete", "操作成功", false) == true) {
            PageDate_Clear();
            vProcStatus = cPS_BROWSE;
            SetControlBaseState();
            if ($("list") != null) {
                $("#list").trigger("reloadGrid");
            }
            if ($("list_rw") != null) {
                $("#list_rw").trigger("reloadGrid");
            }

        }
    });
};
function OnClickRow() {

    var row1 = $('#list').datagrid('getSelected')
    if (row1 != null) {    
        $("#TB_BQMC").val(row1.sTAGMC);
        $("#TB_TAGID").val(row1.iTAGID);
        document.getElementById("B_Update").disabled = false;
        document.getElementById("B_Delete").disabled = false;

    }
 
}

//var sUpdateCurrentPath = "";
//var sCurrentPath = "CrmWeb/GTPT/HQBQXFS/GTPT_HQBQXFS.aspx"
//sUpdateCurrentPath = sCurrentPath + "?TAGID=" + row1.iTAGID + "&TAGMC=" + row1.sTAGMC;
//MakeNewTab(sUpdateCurrentPath, "标签下粉丝", vPageMsgID);
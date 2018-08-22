vUrl = "../GTPT.ashx";
var irow = 0;

function InitGrid() {
    vColumnNames = ['选项ID', '内容', ];
    vColumnModel = [
              { name: 'iNRID', hidden: false, editor: 'text' },
               { name: 'sNRMC', width: 230, editor: 'text' },
    ];
}
$(document).ready(function () {
    $("#AddItem").click(function () {
        if ($("#TB_MC").val() == "") {
            ShowMessage("请填写题号名称");
            return false;
        }
        var a = 1;
        var rowData = $("#list").datagrid("getData").rows;
        if (rowData.length != 0)
           a= rowData[rowData.length - 1].iNRID+1;                                         
        $('#list').datagrid('appendRow',{      
            iNRID:a,
        });
    });
    $("#DelItem").click(function () {
        DeleteRows("list");
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
    Obj.sMC = $("#TB_MC").val();
    Obj.sBZ = $("#TB_BZ").val();
    var lst = new Array();
    lst = $("#list").datagrid("getRows");
    Obj.itemTable = lst;
    return Obj;
}


function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#TB_MC").val(Obj.sMC);
    $("#TB_BZ").val(Obj.sBZ);
    $('#list').datagrid('loadData', Obj.itemTable, "json");
    $('#list').datagrid("loaded");
}


function IsValidData() {
    if ($("#TB_MC").val() == "") {
        ShowMessage("请输入问题名称");
        return false;
    }
    else {
        var rows = $("#list").datagrid("getData").rows;
        if (rows.length < 1) {
            ShowMessage("请先添加数据", 3);
            return false;
        }
        else {
            for (var i = 0; i < rows.length  ; i++) {
                if (isNaN(rows[i].iNRID)) {
                    ShowMessage("选项ID应填数字");
                    return false;
                }

            }

        }
    }  
    return true;
}

vUrl = "../HYKGL.ashx";
var XH = 0;

function bindZtree() {
    var setting = {
        view: {
            showIcon: false
        },
        data: {
            simpleData: {
                enable: true
            }
        },
        callback: {
            beforeClick: beforeClick,
            onClick: onClick
        }
    };

    if ($("#DDL_BQLB").val() != "" && $("#DDL_BQLB").val() != null) {
        PostToCrmlib("FillBQXMTree", { iLABELLBID: $("#DDL_BQLB").val() }, function (data) {
            if (data.length < 1) {
                var zNode = '{"id":"00","pid":"0","name":"暂无数据..."}';
                $.fn.zTree.init($("#TreeBQ"), setting, JSON.parse(zNode));
                return;
            }
            var zNodes = "[";
            for (var i = 0; i < data.length; i++) {
                zNodes = zNodes + "{id:'" + data[i].sLABELXMDM + "',pId:'" + ((data[i].sPLABELXMDM == "") ? "0" : data[i].sPLABELXMDM) + "',name:'" + data[i].sLABELXMQC + "',bqmc:'" + data[i].sLABELXMMC + "',bqms:'" + data[i].sLABELXMMS + "',bqxmid:'" + data[i].iLABELXMID + "',bjwy:'" + data[i].iBJ_WY + "',mjbj:" + data[i].iSTATUS + "}";
                if (i < data.length - 1)
                    zNodes = zNodes + ",";
            }
            zNodes = zNodes + "]";
            $.fn.zTree.init($("#TreeBQ"), setting, eval(zNodes));
            SetControlBaseState();
        }, true);
    }
    else
        $.fn.zTree.destroy("TreeLeft");
}
function beforeClick() {
    ;
}
function InitGrid() {
    vColumnNames = ['标签项目名称', '标签项目代码', '标签项目ID', '标签值ID', '标签值', '组合标签标记','备注', '序号'];
    vColumnModel = [
            { name: 'sLABELXMMC', width: 100 },
            { name: 'sLABELXMDM', hidden: true },
            { name: 'iLABELXMID', hidden: true },
            { name: 'iLABEL_VALUEID', width: 100 },
            { name: 'sLABEL_VALUE', width: 100, editor: 'text' },
            {
                name: 'iLABELLX', width: 100, editor: {
                    type: 'checkbox',
                    options: {
                        on: "1",
                        off: "0"
                    }
                }
        }, //formatter: BoolCellFormat
            { name: 'sBZ', width: 200, editor:'text'},
            { name: 'iLABELID', hidden: true },
    ];
}

$(document).ready(function () {
    vProcStatus = cPS_BROWSE;
    FillBQLB("DDL_BQLB");
    RefreshButtonSep();

})

function OnClickRow(rowIndex, rowData) {
    if (vProcStatus != cPS_MODIFY) {
        vProcStatus = cPS_BROWSE;
        SetControlBaseState();
        document.getElementById("B_Delete").disabled = false;
        document.getElementById("B_Update").disabled = false;
    }
}

function InsertClick() {
    vProcStatus = cPS_MODIFY;
    SetControlBaseState();
    var zTree = $.fn.zTree.getZTreeObj("TreeBQ");
    nodes = zTree.getSelectedNodes();
    if (nodes.length == 0) {
        ShowMessage("请先选择标签", 3);
        return;
    }
    else {
        if (nodes[0].mjbj == 0) {
            ShowMessage("请选末级标签", 3);
            return;
        }
        else {
            var rows = $("#list").datagrid("getRows");
            if (rows.length == 0) {
                XH = 1;
            }
            else {
                XH = rows[rows.length - 1].iLABEL_VALUEID + 1;
            }
            var myData = {
                sLABELXMMC: nodes[0].bqmc,
                sLABELXMDM: nodes[0].id,
                iLABELXMID: nodes[0].bqxmid,
                iLABEL_VALUEID: parseInt(XH),
            };
            $("#list").datagrid("appendRow", myData);
        }
    }
};


function DeleteClick() {
    ShowYesNoMessage("是否删除？", function () {
        var rows = $('#list').datagrid("getSelections");
        var copyRows = [];
        if (rows.length != 0) {
            for (var j = 0; j < rows.length; j++) {
                copyRows.push(rows[j]);
            }
            for (var i = 0; i < copyRows.length; i++) {
                var index = $('#list').datagrid('getRowIndex', copyRows[i]);
                $('#list').datagrid('deleteRow', index);
            }
            $('#list').datagrid('clearSelections');
            //document.getElementById("B_Save").disabled = false;
            SaveClick();
        }
        else {
            ShowMessage("请选择要删除的行", 3);
        }
    });
}
function BQLBChange() {
    if ($("#DDL_BQLB").val() != "" && $("#DDL_BQLB").val() != null) {
        bindZtree();       
    }
    else {
        $.fn.zTree.destroy("TreeBQ");
    }
    $("#list").datagrid("loadData", { total: 0, rows: [] });
}



function onClick(e, treeId, treeNode) {
    $("#list").datagrid("loadData", { total: 0, rows: [] });
    var zTree = $.fn.zTree.getZTreeObj("TreeBQ");
    nodes = zTree.getSelectedNodes();
    if (nodes[0].mjbj == 1 && nodes[0].id != "" && nodes[0].id != "0") {
        $("#HF_LABELXMID").val(nodes[0].bqxmid);
        DoSearch(nodes[0].bqxmid);
    }
    else {
        $("#TreeBQ").prop("disabled", true);
        if (vProcStatus == cPS_MODIFY) {
            vProcStatus = cPS_BROWSE;
            SetControlBaseState();
        }
    }
}


function SetControlState() {
    $("#B_Exec").hide();
    $("#status-bar").hide();
}

function IsValidData() {
    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.iLABELXMID = $("#HF_LABELXMID").val();
    var lst = new Array();
    lst = $("#list").datagrid("getRows");
    Obj.itemTable = lst;
    return Obj;
}

function ShowData() {
    DoSearch($("#HF_LABELXMID").val());
};

function SaveClick() {
    var vMode;
    if (IsValidInputData()) {
        if (vJLBH != "") {
            vMode = "Update";
        }
        else {
            vMode = "Insert";
        }
        if (posttosever(SaveDataBase(), vUrl, vMode) == true) {
            vProcStatus = cPS_BROWSE;
            SetControlBaseState();
            DoSearch($("#HF_LABELXMID").val());
        }
    }
};
function DoSearch(LABELXMID) {
    sjson = "{'iLABELXMID':'" + LABELXMID + "'}";
    $('#list').datagrid("loading");
    $.ajax({
        type: "post",
        url: vUrl + "?mode=Search&func=" + vPageMsgID,
        async: true,
        data: {
            json: sjson,
            titles: 'cybillsearch',
            page: $('#list').datagrid("options").pageNumber,
            rows: $('#list').datagrid("options").pageSize,
        },
        success: function (data) {
            if (data.indexOf('错误') >= 0 || data.indexOf('error') >= 0) {
                ShowMessage(data);
            }
            $('#list').datagrid('loadData', JSON.parse(data), "json");
            $('#list').datagrid("loaded");
        },
        error: function (data) {
            ShowMessage(data);
        }
    });
};



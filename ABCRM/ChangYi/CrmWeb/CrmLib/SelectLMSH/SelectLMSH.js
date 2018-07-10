vUrl = "../../HYKGL/HYKGL.ashx";

function InitGrid() {
    vColumnNames = ['记录编号', '商户名称',];
    vColumnModel = [
        { name: 'iJLBH', index: 'iJLBH', width: 80, },
        { name: 'sSHMC',  },
    ];
};

function DoSelect() {
    var inx;
    var sel = new Array();
    if (bMulSel) {
        var inx = $("#list").jqGrid("getGridParam", "selarrrow");
        if (inx != null && inx.length) {
            for (var i = 0; i < inx.length; i++) {
                sel.push($("#list").jqGrid("getRowData", inx[i]));
            }
        }
    }
    else {
        inx = $("#list").jqGrid("getGridParam", "selrow");
        sel.push($("#list").getRowData(inx));
    }
    $.dialog.data("dialogLMSH", sel);
    //$.dialog.close();
};

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "TB_SHMC", "sSHMC", "like", true);
    return arrayObj;
};
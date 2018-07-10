vUrl = "../JKPT.ashx";
vCaption = "预警规则定义"
function InitGrid() {
    vColumnNames = ['规则编号', 'iYJLX', '预警类型', 'iZQLX', '周期类型', 'iZBLX', '指标类型', '指标数', 'iBJ_TY', '停用标记', '会员数量', 'iYJJB', '预警级别', 'TYPE', '指标数2'];
    vColumnModel = [
               { name: 'iJLBH', width: 80, },
               { name: 'iYJLX', hidden: true, },
               { name: 'sYJLX', width: 80, },
               { name: 'iZQLX', hidden: true, },
               { name: 'sZQLX', width: 80, },
               { name: 'iZBLX', hidden: true, },
               { name: 'sZBLX', width: 80, },
               { name: 'fZBS', width: 80, },
               { name: 'iBJ_TY', hidden: true, },
               { name: 'sBJ_TY', width: 80, },
               { name: 'iHYSL', width: 80, hidden: true },
               { name: 'iYJJB', hidden: true, },
               { name: 'sYJJB', width: 80, hidden: true, },
               { name: 'iTYPE', hidden: true, },
               { name: 'fZBS2', width: 80, hidden: true, },
    ];
}

$(document).ready(function () {
    //$("#list").jqGrid("setGridParam", {
    //    ondblClickRow: function (rowid) {
    //        var rowData = $("#list").getRowData(rowid);
    //        MakeNewTab("CrmWeb/JKPT/YJGZDEF/JKPT_YJGZDEF.aspx?jlbh=" + rowData.iJLBH, "预警规则编辑", vPageMsgID);
    //    },
    //});
    //document.getElementById("B_Insert").onclick = function () {
    //    MakeNewTab("CrmWeb/JKPT/YJGZDEF/JKPT_YJGZDEF.aspx?action=add", "预警规则录入", vPageMsgID);
    //};
    //document.getElementById("B_Update").onclick = function () {
    //    MakeNewTab("CrmWeb/JKPT/YJGZDEF/JKPT_YJGZDEF.aspx?jlbh=" + vJLBH + "&action=edit", "预警规则修改", vPageMsgID);
    //};
});

function MakeSearchCondition() {
    var arrayObj = new Array();
    var vDJZT = $("[name='CB_BJ_TY']:checked").val();
    MakeSrchCondition(arrayObj, "DDL_YJLX", "iYJLX", "=", false);
    MakeSrchCondition(arrayObj, "DDL_ZQLX", "iZQLX", "=", false);
    MakeSrchCondition(arrayObj, "DDL_ZBLX", "iZBLX", "=", false);
    MakeSrchCondition(arrayObj, "DDL_TYPE", "iTYPE", "=", false);
    MakeSrchCondition(arrayObj, "DDL_YJJB", "iYJJB", "=", false);
    MakeSrchCondition(arrayObj, "TB_ZBS", "iZBS", ">=", false);
    MakeSrchCondition(arrayObj, "TB_ZBS2", "iZBS", "<=", false);
    if (vDJZT == 0) {
        MakeSrchCondition2(arrayObj, "0", "iBJ_TY", "=", false);  //未标记
    }
    if (vDJZT == 1) {
        MakeSrchCondition2(arrayObj, "1", "iBJ_TY", "=", false);  //标记
    }
    var arrayObjMore = new Array();
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};
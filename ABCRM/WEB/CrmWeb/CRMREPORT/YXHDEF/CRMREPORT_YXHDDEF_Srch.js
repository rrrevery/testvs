
vUrl = "../CRMREPORT.ashx";

function InitGrid() {
    vColumnNames = ['活动编号', '年度', '开始日期', '结束日期', '活动主题',];
    vColumnModel = [
            { name: 'iJLBH', index: 'iJLBH', width: 80, },//sortable默认为true width默认150
			{ name: 'iND', width: 80, },
            { name: 'dKSRQ', width: 120, },
            { name: 'dJSRQ', width: 120 },
			
			
			{ name: 'sHDZT', width: 180, },
			
    ];
}


$(document).ready(function () {

    $("#A_Export").hide();



    $("#list").jqGrid("setGridParam", {
        ondblClickRow: function (rowid) {
            var rowData = $("#list").getRowData(rowid);
            MakeNewTab("CrmWeb/CRMREPORT/YXHDEF/CRMREPORT_YXHDDEF.aspx?jlbh=" + rowData.iJLBH+ "&ND=" + rowData.iND,"营销活动定义", vPageMsgID);
        },
    });

    document.getElementById("B_Insert").onclick = function () {
        MakeNewTab("CrmWeb/CRMREPORT/YXHDEF/CRMREPORT_YXHDDEF.aspx?action=add", "营销活动定义", vPageMsgID);
    };
    document.getElementById("B_Update").onclick = function () {
        MakeNewTab("CrmWeb/CRMREPORT/YXHDEF/CRMREPORT_YXHDDEF.aspx?jlbh=" + vJLBH + + "&ND=" + vND+ "&action=edit", "营销活动定义", vPageMsgID);
    };
    ZSel_MoreCondition_Load(v_ZSel_rownum);

   

});



function SaveData() {
    var Obj = new Object();
    var rowid = $("#list").jqGrid("getGridParam", "selrow");
    var rowData = $("#list").getRowData(rowid);
    Obj.iJLBH = rowData.iJLBH;
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    Obj.sHYKHM_OLD = rowData.sHYKHM_OLD;
    Obj.iHYID = rowData.iHYID;
    Obj.iHYKTYPE = rowData.iHYKTYPE;
    Obj.sHYKHM_NEW = rowData.sHYKHM_NEW;

    return Obj;
}

function DoSearch() {
   
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
 
    MakeSrchCondition(arrayObj, "TB_ND", "iND", "=", false);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dKSRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dJSRQ", "<=", true);
   
    MakeMoreSrchCondition(arrayObj);
    $("#list").jqGrid('setGridParam', {
        url: vUrl + "?mode=Search&func=" + vPageMsgID,
        postData: { 'afterFirst': JSON.stringify(arrayObj), },
        page: 1
    }).trigger("reloadGrid");
    location.hash = "tab2-tab";
    return arrayObj;
};
vUrl = "../KFPT.ashx";

function InitGrid() {
    vColumnNames = ['记录编号', '客服经理ID', '人员代码', '客服经理', '登记人名称', '登记时间', '审核人名称', '审核时间', '备注'];
    vColumnModel = [
            { name: 'iJLBH', width: 100, },
			{ name: 'iKFRYID', hidden: true },
            { name: 'sRYDM', width: 100, },
            { name: 'sRYMC', width: 100, },
            { name: 'sDJRMC', width: 80, },
            { name: 'dDJSJ', width: 150, },
            { name: 'sZXRMC', width: 80, },
            { name: 'dZXRQ', width: 150, },
			{ name: 'sBZ', width: 100 },

    ];
}

$(document).ready(function () {

    //$("#list").jqGrid("setGridParam", {
    //    ondblClickRow: function (rowid) {
    //        var rowData = $("#list").getRowData(rowid);
    //        MakeNewTab("CrmWeb/KFPT/KFJLDY/KFPT_KFJLDY.aspx?jlbh=" + rowData.iJLBH, "会员客户经理定义", vPageMsgID);
    //    },
    //    gridComplete: function () {
    //        $("#list").footerData('set', { iKFRYID: "" });
    //    }
    //});

    //document.getElementById("B_Insert").onclick = function () {
    //    MakeNewTab("CrmWeb/KFPT/KFJLDY/KFPT_KFJLDY.aspx?action=add", "会员客户经理定义", vPageMsgID);
    //};

    //document.getElementById("B_Update").onclick = function () {

    //    MakeNewTab("CrmWeb/KFPT/KFJLDY/KFPT_KFJLDY.aspx?jlbh=" + vJLBH + "&action=edit", "会员客户经理定义", vPageMsgID);
    //};


    $("#TB_KFJLMC").click(function () {
        SelectKFJL("TB_KFJLMC","HF_KFJL");
    });


});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "HF_KFJL", "iKFRYID", "=", false);
    if ($("#TB_BZ").val() != "")
        MakeSrchCondition2(arrayObj, "%" + $("#TB_BZ").val() + "%", "sBZ", "like", true);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};
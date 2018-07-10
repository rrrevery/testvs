vUrl = "../../GTPT/GTPT.ashx";
vCaption = "预约记录";
var data = $.dialog.data("DialogCondition");
if (data)
    data = JSON.parse(data);
var vDialogName = $.dialog.data("vDialogName");

function InitGrid() {
    vColumns = [
        { field: 'iJLBH', title: '记录编号', hidden: true },
        { field: 'iID', title: 'ID', hidden: true },
        { field: 'sGKXM', title: '顾客姓名', width: 100 },
        { field: 'sMC', title: '主题', width: 100 },
        { field: 'sOPENID', title: 'OPENID', hidden: true },
        { field: 'iHYID', title: 'HYID', hidden: true },
        { field: 'dRQ', title: '日期', hidden: true },
        { field: 'sLXDH', title: '联系电话', width: 100 },
        { field: 'sBZ', title: '备注', hidden: true },
        { field: 'dDJSJ', title: '登记时间', width: 100 },
        { field: 'iMDID', title: 'MDID', hidden: true },
        { field: 'sMDMC', title: '门店', width: 100 },

    ];
    vIdField = "iJLBH";
}

$(document).ready(function () {
   
});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", true);
    MakeSrchCondition(arrayObj, "TB_GKXM", "sGKXM", "like", true);
    //MakeSrchCondition2(arrayObj, 0, "iBJ_CJ", "=", false);
    //MakeSrchCondition2(arrayObj, 1, "iSTATUS", "=", false);
    for (var item in data) {
        MakeSrchCondition2(arrayObj, data[item], item, "=", typeof (data[item]) == "string" ? true : false);
    }
    return arrayObj;
};
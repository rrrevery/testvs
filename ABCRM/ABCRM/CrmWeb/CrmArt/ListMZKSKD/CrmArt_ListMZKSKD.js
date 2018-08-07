vUrl = "../../HYKGL/HYKGL.ashx";
vCaption = "面值卡售卡单";
var vDialogName = "ListMZKSKD";
if ($.dialog.data("SingleSelect") != undefined)
    vSingleSelect = $.dialog.data("SingleSelect");
var data = $.dialog.data("DialogCondition");
if (data)
    data = JSON.parse(data);


function InitGrid()
{
    vColumns = [
            { field: "iJLBH", title: '售卡单编号', width: 80, },
            { field: "iSKSL", title: '售卡数量', width: 60, },
            { field: "cSSJE", title: '实收金额', width: 80, hidden: true },
            { field: "sYHQMC", title: '优惠券名称', width: 80, hidden: true },
            { field: "iYHQID", title: 'yhqid', width: 80, hidden: true },
            { field: "iYXQTS", title: '有效期天数', width: 80, hidden: true },
            { field: "fSJZSJF", title: '实际赠送积分', width: 80, hidden: true },
            { field: "fSJZSJE", title: '实际赠送金额', width: 80, hidden: true },
            { field: "sKHMC", title: '客户', width: 80 },
            { field: "dDJSJ", title: '登记时间', width: 150, },
            { field: "sDJRMC", title: '登记人', width: 80 },
            { field: "dZXRQ", title: '审核日期', width: 70, },
            { field: "sZXRMC", title: '审核人', width: 80 },
            { field: "sZY", title: '摘要', width: 100, },
            { field: "iKHID", title: 'KHID', hidden: true },
            { field: "iDKHLX", title: '客户类型', hidden: true },
            { field: "fDHJE", title: '待还金额', hidden: true },
    ];
    vIdField = "iJLBH";
}

$(document).ready(function ()
{
    //pass
});

function MakeSearchCondition()
{
    var arrayObj = new Array();
    if (data)
    {
        for (var item in data)
        {
            if (item != 'BJ_TS' && item != 'BJ_QKGX')
            {
                MakeSrchCondition2(arrayObj, data[item], item, "=", typeof (data[item]) == "string" ? true : false);
            }

        }
    }
    return arrayObj;
};
function AddCustomerCondition(Obj)
{
    if (data)
    {
        Obj.iBJ_TS = data['BJ_TS'] || 0;
        Obj.dialogName = vDialogName;
        Obj.iBJ_QKGX = data['BJ_QKGX'] || 0;
    }
}

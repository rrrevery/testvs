vUrl = "../../HYKGL/HYKGL.ashx";
vCaption = "面值卡售卡明细";
var vDialogName = "ListMZKSKMX";
if ($.dialog.data("SingleSelect") != undefined)
    vSingleSelect = $.dialog.data("SingleSelect");
var data = $.dialog.data("DialogCondition");
if (data)
    data = JSON.parse(data);


function InitGrid()
{
    vColumns = [
            { field: "iSKDJLBH", title: '售卡单编号', width: 80, },
            { field: "sCZKHM", title: '卡号', width: 100, },
            { field: "sHYKNAME", title: '卡类型', width: 80 },
            { field: "iHYKTYPE", title: 'iHYKTYPE', width: 80, hidden: true },
            { field: "fQCYE", title: '期初金额', width: 80 },
            { field: "fPDJE", title: '铺底金额', width: 80 },
            { field: "dYXQ", title: '有效期', width: 100 },
            { field: "iHYID", title: 'HYID', width: 80, hidden: true },
    ];
    vIdField = "iHYID";
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
            if (item == 'iSKJLBH')
            {
                MakeSrchCondition2(arrayObj, data[item], item, "=", false);
            }
            if (item == 'sKSKH')
            {
                MakeSrchCondition2(arrayObj, data[item], "sHYK_NO", ">=", true);
            }
            if (item == 'sJSKH')
            {
                MakeSrchCondition2(arrayObj, data[item], "sHYK_NO", "<=", true);
            }            
        }
    }
    return arrayObj;
};
function AddCustomerCondition(Obj)
{
    Obj.dialogName = vDialogName;
}

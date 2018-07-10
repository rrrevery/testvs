vUrl = "../../CRMGL/CRMGL.ashx";
vCaption = "小区信息";
var vDialogName = "ListXQ";
var data = $.dialog.data("DialogCondition");
if (data)
    data = JSON.parse(data);
if ($.dialog.data("SingleSelect") != undefined)
    vSingleSelect = $.dialog.data("SingleSelect");

function InitGrid() {
    vColumns = [
        { field: 'iJLBH', title: '小区编号', hidden: true },
        { field: 'sXQMC', title: '小区名称', width: 100 },
    ];
    vIdField = "iJLBH";
}

$(document).ready(function () {

});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_XQMC", "sXQMC", "like", true);
    if (data)
    {
        for (var item in data)
        {
            if (data[item] && item != 'vHF' && item != 'vZT')
            {
                MakeSrchCondition2(arrayObj, data[item], item, "=", typeof (data[item]) == "string" ? true : false);
            }
        }
    }
    return arrayObj;
};


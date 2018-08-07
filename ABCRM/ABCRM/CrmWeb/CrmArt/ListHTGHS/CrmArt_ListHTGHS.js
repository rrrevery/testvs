vUrl = "../../CRMGL/CRMGL.ashx";
vCaption = "供货商信息";
var vDialogName = "ListHTGHS";
function InitGrid()
{
    vColumns = [
        { field: 'sGHSDM', title: '供货商代码', width: 100 },
        { field: 'sGHSMC', title: '供货商名称', width: 100 },
    ];
    vIdField = "sGHSDM";
}

$(document).ready(function ()
{
    //pass
});

function MakeSearchCondition()
{
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_GHSDM", "sGHSDM", "=", true);
    MakeSrchCondition(arrayObj, "TB_GHSMC", "sGHSMC", "like", true);
    return arrayObj;
};

function AddCustomerCondition(Obj)
{
    Obj.iSEARCHMODE = 1;
}
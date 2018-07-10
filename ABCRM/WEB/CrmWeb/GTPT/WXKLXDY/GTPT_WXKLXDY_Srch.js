vUrl = "../GTPT.ashx";
vCaption = "微信卡券包会员卡定义";

function InitGrid()
{
    vColumnNames = ['记录编号', '会员卡名称', '登记人', '登记人名称', '登记时间'];
    vColumnModel = [
               { name: 'iJLBH', width: 80, },
               { name: 'sTITLE', width: 80, },
               { name: 'iDJR', width: 80, },
               { name: 'sDJRMC', width: 80, },
               { name: 'dDJSJ', width: 80, },
    ];
}

$(document).ready(function ()
{
    BFButtonClick("TB_DJRMC", function ()
    {
        SelectRYXX("TB_DJRMC", "HF_DJR", "zHF_DJR", false);
    });

});

function MakeSearchCondition()
{
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "TB_TITLE", "sTITLE", "=", false);
    MakeSrchCondition(arrayObj, "HF_DJR", "iDJR", "in", false);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);

    return arrayObj;
};
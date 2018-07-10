vUrl = "../MZKGL.ashx";
vCaption = "面值卡售卡单欠款购销";

function InitGrid()
{
    vColumnNames = ["记录编号", "还款金额", "启动数量", "售卡单编号", "DJR", "登记人", "登记时间", "摘要"];
    vColumnModel = [
			{ name: "iJLBH", width: 80, },
            { name: "fFKJE", width: 120, },
            { name: "iQDSL", width: 120, },
            { name: 'iSKDJLBH', width: 120, },
            { name: 'iDJR', hidden: true, },
			{ name: 'sDJRMC', width: 80, },
			{ name: 'dDJSJ', width: 150, },
			{ name: 'sZY', width: 80, },
    ];
}

$(document).ready(function ()
{
    $("#TB_DJRMC").click(function ()
    {
        SelectRYXX("TB_DJRMC", "HF_DJR");
    });
    $("#TB_ZXRMC").click(function ()
    {
        SelectRYXX("TB_ZXRMC", "HF_ZXR");
    });


   
});

function MakeSearchCondition()
{
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "HF_DJR", "iDJR", "=", false);
    MakeSrchCondition(arrayObj, "HF_ZXR", "iZXR", "=", false);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    return arrayObj;

};


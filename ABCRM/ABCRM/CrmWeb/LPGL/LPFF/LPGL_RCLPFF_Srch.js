vUrl = "../LPGL.ashx";
vCaption = "日常礼品发放";
function InitGrid() {
    vColumnNames = ['记录编号', '会员卡号', '发放地点', '礼品名称', '发放数量', 'BGDDDM', '原积分', '处理积分', '摘要', '登记人', '登记人', '登记时间', '审核人', '审核人', '审核时间', '冲正人', '冲正人', '冲正时间'];
    vColumnModel = [
        { name: 'iJLBH', width: 80, },
        { name: 'sHYK_NO', width: 80, },
        { name: 'sBGDDMC', width: 60 },
        { name: 'sLPMC', width: 80, hidden: true },
        { name: 'iFFSL', width: 60, hidden: true },
        { name: 'sBGDDDM', hidden: true, },
        { name: 'fWCLJF_OLD', width: 60 },
        { name: 'fCLJF', width: 60, },
        { name: 'sZY', width: 120 },
        { name: 'iDJR', hidden: true, },
        { name: 'sDJRMC', width: 80, },
        { name: 'dDJSJ', width: 120, },
        { name: 'iZXR', hidden: true, },
        { name: 'sZXRMC', width: 80, },
        { name: 'dZXRQ', width: 120, },
        { name: 'iCZR', hidden: true, },
        { name: 'sCZRMC', width: 80, },
        { name: 'dCZRQ', width: 120, },
    ]
}

$(document).ready(function () {
    $("#B_Update").hide();
    $("#B_Delete").hide();
    $("#B_Exec").hide();

    BFButtonClick("TB_DJRMC", function () {
        SelectRYXX("TB_DJRMC", "HF_DJR", "zHF_DJR", false);
    });
    BFButtonClick("TB_ZXRMC", function () {
        SelectRYXX("TB_ZXRMC", "HF_ZXR", "zHF_ZXR", false);
    });

    BFButtonClick("TB_CZRMC", function () {
        SelectRYXX( "TB_CZRMC","HF_CZR","zHF_CZR",false);
    });

    BFButtonClick("TB_MDMC", function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", false);
    });
    RefreshButtonSep();
});




function MakeSearchCondition()
{
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "HF_DJR", "iDJR", "=", false);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeSrchCondition(arrayObj, "HF_MDID", "iMDID", "in", false);
    MakeSrchCondition(arrayObj, "HF_CZR", "iCZR", "=", false);
    MakeSrchCondition(arrayObj, "TB_CZRQ1", "dCZRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_CZRQ2", "dCZRQ", "<=", true);
    MakeSrchCondition2(arrayObj, "0", "iGZLX", "=", false);
    MakeSrchCondition(arrayObj, "HF_ZXR", "iZXR", "=", false);
    MakeSrchCondition(arrayObj, "TB_ZXRQ1", "dZXRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ2", "dZXRQ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_HYKNO", "sHYK_NO", "=", true);
    
    return arrayObj;
}

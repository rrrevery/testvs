vUrl = "../LPGL.ashx";
vCaption = "礼品发放规则";
var vGZLX = GetUrlParam("GZLX");

function InitGrid() {
    vColumnNames = ['记录编号', '规则名称',  '规则类型', '开始日期', '结束日期', 'HYKTYPE', '卡类型', '登记人', '登记人', '登记时间', ],//'审核人', '审核人', '审核日期'
    vColumnModel = [
        { name: 'iJLBH', index: 'iJLBH', width: 80, },//sortable默认为true width默认150
        { name: 'sGZMC', width: 80 },
        //{ name: 'iGZLX', hidden: true },
        { name: 'sGZLXMC', width: 60, },
        { name: 'dKSRQ', },
        { name: 'dJSRQ', },
        { name: 'iHYKTYPE', hidden: true, },
        { name: 'sHYKNAME', width: 60, },
        { name: 'iDJR', hidden: true, },
        { name: 'sDJRMC', width: 80, },
        { name: 'dDJSJ', width: 120, },
        //{ name: 'iZXR', hidden: true, },
        //{ name: 'sZXRMC', width: 80, },
        //{ name: 'dZXRQ', width: 120, },
    ]
}

$(document).ready(function () {
    $("#B_Exec").hide();
    ConbinDataArry["GZLX"] = vGZLX;
    BFButtonClick("TB_DJRMC", function () {
        SelectRYXX("TB_DJRMC", "HF_DJR", "zHF_DJR", false);
    });
    BFButtonClick("TB_ZXRMC", function () {
        SelectRYXX("TB_ZXRMC", "HF_ZXR", "zHF_ZXR", false);
    });

});


function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "TB_GZMC", "sGZMC", "=", true);
    MakeSrchCondition(arrayObj, "HF_DJR", "iDJR", "in", true);
    MakeSrchCondition(arrayObj, "HF_ZXR", "iZXR", "in", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ1", "dZXRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ2", "dZXRQ", "<=", true);
    if (vGZLX)
        MakeSrchCondition2(arrayObj, vGZLX, "iGZLX", "=", false);
    return arrayObj;
}

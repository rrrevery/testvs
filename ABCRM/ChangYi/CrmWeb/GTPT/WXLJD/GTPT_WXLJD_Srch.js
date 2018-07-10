vUrl = "../GTPT.ashx";
vCaption = "微信领奖单";


function InitGrid() {
    vColumnNames = ['记录编号', 'DJR', '登记人', '登记时间', 'SHR', '审核人', '审核日期', '会员卡号','会员名称','CZDD', '操作地点'],
    vColumnModel = [
            { name: 'iJLBH', index: 'iJLBH', width: 80, },
            { name: 'iDJR', hidden: true, },
			{ name: 'sDJRMC', width: 80, },
			{ name: 'dDJSJ', width: 150, },
			{ name: 'iZXR', hidden: true, },
			{ name: 'sZXRMC', width: 80, },
			{ name: 'dZXRQ', width: 150, },
            { name: 'sHYK_NO', width: 150, },
            { name: 'iHY_NAME', width: 80, },
			{ name: 'sBGDDDM', hidden: true, },
			{ name: 'sBGDDMC', width: 120, },
    ]
}
$(document).ready(function () {
    BFButtonClick("TB_DJRMC", function () {
        SelectRYXX("TB_DJRMC", "HF_DJR", "zHF_DJR", false);
    });
    BFButtonClick("TB_ZXRMC", function () {
        SelectRYXX("TB_ZXRMC", "HF_ZXR", "zHF_ZXR", false);
    });
})

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "HF_DJR", "iDJR", "=", false);
    MakeSrchCondition(arrayObj, "HF_ZZR", "iZXR", "=", false);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true); 
    MakeSrchCondition(arrayObj, "TB_ZZRQ1", "dZZRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_ZZRQ2", "dZZRQ", "<=", true);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};

vUrl = "../GTPT.ashx";
vCaption = "送券活动定义单";
function InitGrid() {
    vColumnNames = ['记录编号', 'GZID', '规则名称', '开始日期', '结束日期', 'DJR', '登记人', '登记时间', 'SHR', '执行人', '审核日期', 'DQR', '启动人', '启动日期', 'ZZR', '终止人', '终止日期', '状态', '公共号名称'];
    vColumnModel = [
        { name: 'iJLBH', index: 'iJLBH', width: 80, },//sortable默认为true width默认150
             { name: 'iGZID', hidden: true },
            { name: 'sGZMC', width: 120, },
            { name: 'dKSRQ', width: 120, },
            { name: 'dJSRQ', width: 120, },
            { name: 'iDJR', hidden: true, },
			{ name: 'sDJRMC', width: 80, },
			{ name: 'dDJSJ', width: 120, },
			{ name: 'iZXR', hidden: true, },
			{ name: 'sZXRMC', width: 80, },
			{ name: 'dZXRQ', width: 120, },
            { name: 'iQDR', hidden: true, },
			{ name: 'sQDRMC', width: 80, },
			{ name: 'dQDSJ', width: 120, },
			{ name: 'iZZR', hidden: true, },
			{ name: 'sZZRMC', width: 80, },
			{ name: 'dZZRQ', width: 120, },
			{
			    name: 'iSTATUS', width: 80, formatter: function (cellvalue, icol) {
			        switch (cellvalue) {
			            case 0:
			                return "保存";
			                break;
			            case 1:
			                return "审核";
			                break;
			            case 2:
			                return "启动";
			                break;
			            case 3:
			                return "终止";
			                break;
			        }
			    }
			},
            	{ name: 'sPUBLICNAME', width: 120, },
    ];
}
$(document).ready(function () {
    BFButtonClick("TB_GZMC", function () {
        SelectWXYHQGZ("TB_GZMC", "HF_GZID", "zHF_GZID",false);
    });
    BFButtonClick("TB_DJRMC", function () {
        SelectRYXX("TB_DJRMC", "HF_DJR", "zHF_DJR", false);
    });
    BFButtonClick("TB_ZXRMC", function () {
        SelectRYXX("TB_ZXRMC", "HF_ZXR", "zHF_ZXR", false);
    });
    BFButtonClick("TB_QDRMC", function () {
        SelectRYXX("TB_QDRMC", "HF_QDR", "zHF_QDR", false);
    });
    BFButtonClick("TB_ZZRMC", function () {
        SelectRYXX("TB_ZZRMC", "HF_ZZR", "zHF_ZZR", false);
    });

});



function MakeSearchCondition() {
    var arrayObj = new Array();
    var vSTATUS = $("[name='STATUS']:checked").val();
    if (vSTATUS) {
        MakeSrchCondition2(arrayObj, vSTATUS, "iSTATUS", "=", false);
    }
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "HF_GZID", "iGZID", "in", false);
    MakeSrchCondition(arrayObj, "TB_KSRQ1", "dKSRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_KSRQ2", "dKSRQ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_JSRQ1", "dJSRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_JSRQ2", "dJSRQ", "<=", true);
    MakeSrchCondition(arrayObj, "HF_DJR", "iDJR", " in", false);
    MakeSrchCondition(arrayObj, "HF_ZXR", "iZXR", " in", false);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ1", "dZXRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ2", "dZXRQ", "<=", true);
    MakeSrchCondition(arrayObj, "HF_QDR", "iQDR", " in", false);
    MakeSrchCondition(arrayObj, "HF_ZZR", "iZZR", " in", false);
    MakeSrchCondition(arrayObj, "TB_QDSJ1", "dQDSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_QDSJ2", "dQDSJ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_ZZSJ1", "dZZSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_ZZSJ2", "dZZSJ", "<=", true);

    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
}


vUrl = "../MZKGL.ashx";
vCaption = "面值卡回收";


function InitGrid() {
    vColumnNames = ['记录编号', '卡类型',
				   '卡类型', '保管地点代码',
				   '保管地点名称', '退卡数量',
				   '退卡金额', '退卡方式',
				   '保管人', '保管人名称',
				   '发行单位名称', 'DJR', '登记人', '登记时间', 'ZXR', '审核人', '审核日期', '摘要', ];
    vColumnModel = [

		 { name: 'iJLBH', index: 'iJLBH', width: 80, },//sortable默认为true width默认150
			{ name: 'iHYKTYPE', hidden: true },
			{ name: 'sHYKNAME', },
			{ name: 'sBGDDDM', width: 80, },
			{ name: 'sBGDDMC', hidden: true, },
			{ name: 'iTKSL', width: 80, },

			{ name: 'fTKJE', width: 80, },
			{ name: 'iTKFS', hidden: true, },
			{ name: 'iBGR', width: 80, },
			{ name: 'sBGRMC', width: 80, },
			{ name: 'sFXDWMC', hidden: true, },

			{ name: 'iDJR', hidden: true, },
			{ name: 'sDJRMC', width: 80, },
			{ name: 'dDJSJ', width: 120, },
			{ name: 'iZXR', hidden: true, },
			{ name: 'sZXRMC', width: 80, },
			{ name: 'dZXRQ', width: 120, },
			{ name: 'sZY', width: 120, },
    ];
}

$(document).ready(function () {


    $("#TB_DJRMC").click(function () {
        SelectRYXX("TB_DJRMC", "HF_DJR");
    });
    $("#TB_ZXRMC").click(function () {
        SelectRYXX("TB_ZXRMC", "HF_ZXR");
    });

    $("#TB_BGRMC").click(function () {
        SelectRYXX("TB_BGRMC", "HF_BGR");
    });
    $("#TB_LYRMC").click(function () {
        SelectRYXX("TB_LYRMC", "HF_LYR");
    });

    $("#TB_HYKNAME").click(function () {
        var condData = new Object();
        condData["vCZK"] = 1;
        SelectKLX("TB_HYKNAME", "HF_HYKTYPE", "zHF_HYKTYPE", false, condData);
    });
    $("#TB_BGDDMC").click(function () {
        SelectBGDD("TB_BGDDMC", "HF_BGDDDM", "zHF_BGDDDM");
    });

});


function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "HF_BGDDDM", "sBGDDDM", "in", false);
    MakeSrchCondition(arrayObj, "HF_HYKTYPE", "iHYKTYPE", "in", false);
    MakeSrchCondition(arrayObj, "TB_DJRMC", "sDJRMC", " =", true);
    MakeSrchCondition(arrayObj, "TB_BGRMC", "sBGRMC", " =", true);
    MakeSrchCondition(arrayObj, "TB_LYRMC", "sLYRMC", " =", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRMC", "sZXRMC", " =", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ1", "dZXRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ2", "dZXRQ", "<=", true);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;

};

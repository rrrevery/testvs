vUrl = "../MZKGL.ashx";
vCaption = "库存卡查询";

function InitGrid() {
    vColumnNames = ["卡号", "保管地点名称", "保管地点代码", "卡类型", "卡类型", "保管人ID", "保管人", "状态", "期初余额", "YXTZJE", "验卡", "铺底金额", "有效期", "写卡日期", "售卡编号"];
    vColumnModel = [
            { name: 'sCZKHM', width: 80, },//sortable默认为true width默认150
			{ name: 'sBGDDMC', width: 80, },
			{ name: 'sBGDDDM', hidden: true, },
	        { name: 'iHYKTYPE', width: 80, hidden: true, },
            { name: 'sHYKNAME', width: 80, },
            { name: 'iBGR', width: 80, hidden: true, },
            { name: 'sBGRMC', width: 80, },
            {
                name: 'iSTATUS', width: 80,
                formatter: function (cellvalue, options, rowObject) {
                    if (cellvalue == "0") { return "建卡"; }
                    if (cellvalue == "1") { return "领用"; }
                }
            },
            { name: 'fQCYE', width: 80, },
            { name: 'fYXTZJE', width: 80, hidden: true, },
            { name: 'iBJ_YK', width: 60, formatter: "checkbox" },
            { name: 'fPDJE', width: 80, },
            { name: 'dYXQ', width: 80, },
            { name: 'dXKRQ', width: 80, },
            { name: 'iSKJLBH', width: 80, },
    ];
};

$(document).ready(function () {
    $("#B_Exec").hide();
    $("#B_Insert").hide();
    $("#B_Update").hide();
    $("#B_Delete").hide();
    //jQuery("#list").jqGrid('navGrid', '#pager', { edit: false, add: false, del: false }, {}, {}, {}, { multipleSearch: true, multipleGroup: true });
    $("#TB_BGRMC").click(function () {
        SelectRYXX("TB_BGRMC", "HF_BGR", "zHF_BGR", false);
    });
    $("#TB_BGDDDM").click(function () {
        SelectBGDD("TB_BGDDDM", "HF_BGDDDM", "zHF_BGDDDM");
    });
   BFButtonClick("TB_HYKNAME", function () {
        var condData = new Object();
        condData["vCZK"] = 1;
        SelectKLX("TB_HYKNAME", "HF_HYKTYPE", "zHF_HYKTYPE", false, condData);
    });
    RefreshButtonSep();
});


function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_KCKHM", "sCZKHM", "=", true);
    MakeSrchCondition(arrayObj, "TB_SKBH", "iSKJLBH", "=", false);
    MakeSrchCondition(arrayObj, "HF_BGR", "iBGR", "=", false);
    MakeSrchCondition(arrayObj, "HF_BGDDDM", "sBGDDDM", "in", false);
    MakeSrchCondition(arrayObj, "HF_HYKTYPE", "iHYKTYPE", "in", false);
    MakeSrchCondition2(arrayObj, $("input[name='status']:checked").val(), "iSTATUS", "=", false);
    MakeSrchCondition2(arrayObj, $("input[name='bj_yk']:checked").val(), "iBJ_YK", "=", false);
    MakeSrchCondition2(arrayObj, 1, "iBJ_CZK", "=", false);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};
vUrl = "../HYKGL.ashx";
vCaption = "基础标签定义";
function InitGrid() {
    vColumnNames = ['iLABELID', '标签名称',  '有效月份', '继承标签', '状态', '登记人', '登记人', '登记时间'];
    vColumnModel = [
			{ name: 'iJLBH', width: 150, hidden: true, },
            { name: 'sLABELVALUE', index: 'sLABELVALUE', width: 150, },
            { name: 'iYXYF', width: 150, },
            { name: 'sNEW_LABELVALUE', width: 150, },
            {
                name: 'iSTATUS', width: 150, formatter: function (value) {
                    if (value == "0")
                        return "有效";
                    else {
                        return "已停用";
                    }
                }
            },
			{ name: 'iDJR', hidden: true, },
			{ name: 'sDJRMC', width: 150, },
			{ name: 'dDJSJ', width: 150, },
    ];
}

$(document).ready(function () {
    BFButtonClick("TB_DJRMC", function () {
        SelectRYXX("TB_DJRMC", "HF_DJR", "zHF_DJR", false);
    });

    BFButtonClick("TB_HYBQ", function () {
        SelectHYBQ("TB_HYBQ", "HF_HYBQ", "zHF_HYBQ", false);
    });
});


function MakeSearchCondition() {
    var arrayObj = new Array();
    var iSTATUS = $("[name='RD_ZT']:checked").val();
    MakeSrchCondition(arrayObj, "HF_HYBQ", "iLABELID", "in", false);
    if (iSTATUS != "1") {
        MakeSrchCondition2(arrayObj, iSTATUS, "iSTATUS", "=", false);
    }
    MakeSrchCondition(arrayObj, "HF_DJR", "iDJR", "in", false);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeMoreSrchCondition(arrayObj);   
    return arrayObj;
};

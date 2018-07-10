vUrl = "../CRMGL.ashx";
vCaption = "小区定义";
function InitGrid() {
    vColumnNames = ['小区编号', '小区名称', '小区户数', 'iQYID', '所属区域', '小区代码', 'iBJ_TY', '是否停用', "所属商圈"];
    vColumnModel = [
            { name: 'iJLBH', width: 150, hidden: true, },
			{ name: 'sXQMC', width: 150, },
            { name: 'iXQHS', width: 150, },
            { name: 'iQYID', width: 150, hidden: true, },
			{ name: 'sQYMC', width: 150, },
            { name: 'sXQDM', width: 150, },
            { name: 'iBJ_TY', width: 150, hidden: true, },
			{ name: 'sBJ_TY', width: 150, },
            { name: "sSQMC", width: 200 }
    ];
}

$(document).ready(function () {

    $("#B_Exec").hide();
    //FillQYTree("TreeQY", "TB_QYMC");
    BFButtonClick("TB_QYMC", function () {
        SelectQY("TB_QYMC", "HF_QYDM", "zHF_QYDM", false);
    });
    BFButtonClick("TB_SQMC", function () {
        SelectSQ("TB_SQMC", "HF_SQID", "zHF_SQID", false);
    });
    RefreshButtonSep();
});


function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "TB_XQMC", "sXQMC", "like", true);
    MakeSrchCondition(arrayObj, "TB_XQDM", "sXQDM", "like", true);
    MakeSrchCondition(arrayObj, "HF_QYDM", "sQYDM", "in", false);
    MakeSrchCondition(arrayObj, "HF_SQID", "iSQID", "in", false);
    return arrayObj;
}


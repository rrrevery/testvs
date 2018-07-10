vUrl = "../GTPT.ashx";


function InitGrid() {
    vColumnNames = ["绑卡时间", "会员卡号", "会员姓名", "卡类型", "iHYKTYPE", "手机号码"];//"微信名称","开卡时间", "取消时间",
    vColumnModel = [
            { name: 'dDJSJ', width: 150, },
            //{ name: 'dKKSJ', width: 150, },
            //{ name: 'dQXSJ', width: 150, },
            { name: 'sHYKNO', width: 80, },
            { name: 'sHY_NAME', width: 80, },
            { name: 'sHYKNAME', width: 80, },
            { name: 'iHYKTYPE', hidden: true },
            { name: 'sSJHM', width: 80, },
    ];
};

$(document).ready(function () {
    $("#B_Exec").hide();
    $("#B_Insert").hide();
    $("#B_Update").hide();
    $("#B_Delete").hide();

    //ZSel_MoreCondition_Load(v_ZSel_rownum);
});



function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_GZSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_GZSJ2", "dDJSJ", "<=", true);
    //MakeSrchCondition(arrayObj, "TB_KKSJ1", "dKKSJ", ">=", true);
    //MakeSrchCondition(arrayObj, "TB_KKSJ2", "dKKSJ", "<=", true);
    //MakeSrchCondition(arrayObj, "TB_QXSJ1", "dQXSJ", ">=", true);
    //MakeSrchCondition(arrayObj, "TB_QXSJ2", "dQXSJ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_HYKNO1", "sHYKNO", ">=", true);
    MakeSrchCondition(arrayObj, "TB_HYKNO2", "sHYKNO", "<=", true);
    //MakeSrchCondition(arrayObj, "TB_WX_NAME", "sWX_NAME", "=", true);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};
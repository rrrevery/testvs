vUrl = "../HYKGL.ashx";

vCaption = "顾客重要信息修改";

function InitGrid() {
    vColumnNames = ['记录编号', '顾客ID', '姓名', 'iSEX', '性别', "原手机号码", "修改后手机号码", '原证件号', '身份证号', "出生日期", 'GXR', '更新人', '更新日期'];// "原微信", "修改后微信",
    vColumnModel = [
            { name: 'iJLBH', index: 'iJLBH', width: 80, },
            { name: 'iGKID', width: 80, },
			{ name: 'sHY_NAME', width: 80, },
            { name: "iSEX", hidden: true, },
            { name: "sSEX", width: 50, },
            { name: 'sOLD_SJHM', },
            { name: 'sNEW_SJHM', },
            //{ name: 'sOLD_WX', },
            //{ name: 'sNEW_WX', },
             { name: 'sOLD_SFZBH', width: 150, },
            { name: 'sSFZBH', width: 150, },
            { name: 'dCSRQ', width: 80, },
			{ name: 'iDJR', hidden: true, },
			{ name: 'sDJRMC', width: 80, },
			{ name: 'dDJSJ', width: 120, },
    ];
}




$(document).ready(function () {
    $("#B_Exec").hide();
    $("#B_Update").hide();
    $("#TB_GXRMC").click(function () {
        SelectRYXX("HF_GXR", "TB_GXRMC");
    });
    RefreshButtonSep();
});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "TB_GKID", "iGKID", "=", false);
    //MakeSrchCondition(arrayObj, "TB_GKNAME", "sHY_NAME", "=", true);
    MakeSrchCondition(arrayObj, "TB_HYKNO", "sHYK_NO", "=", true);
    //MakeSrchCondition(arrayObj, "S_SEX", "iSEX", "=", false);
    //MakeSrchCondition(arrayObj, "TB_SJHM", "sSJHM", "=", true);
    MakeSrchCondition(arrayObj, "TB_SFZBH", "sSFZBH", "=", true);
    MakeSrchCondition(arrayObj, "TB_GXRMC", "sGXRMC", "=", true);
    MakeSrchCondition(arrayObj, "TB_GXSJ1", "dGXSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_GXSJ2", "dGXSJ", "<=", true);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};



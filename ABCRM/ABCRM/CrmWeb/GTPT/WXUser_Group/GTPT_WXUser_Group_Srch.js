vUrl = "../GTPT.ashx";
vCaption = "微信用户分组操作";
function InitGrid()
{
    vColumnNames = ['记录编号', '分组记录名称', '分组编号', '分组名称', '登记时间', '登记人名称', '登记人', '执行日期', '执行人名称', '执行人', '备注'],
   vColumnModel = [
           { name: 'iJLBH', index: 'iJLBH', width: 80, },//sortable默认为true width默认150
			{ name: 'sGROUPJL_NAME', width: 80, },
			{ name: 'iGROUPID', width: 80, },
            { name: 'sGROUP_NAME', width: 80, },
            { name: 'dDJSJ', width: 120, },
            { name: 'sDJRMC', width: 120, },
			{ name: 'iDJR', hidden: true, },
			{ name: 'dZXRQ', width: 80, },
			{ name: 'sZXRMC', width: 120, },
			{ name: 'iZXR', hidden: true, },
			{ name: 'sZY', width: 120, hidden: true },
   ]
}
$(document).ready(function () {
    BFButtonClick("TB_GRPMC", function () {
        SelectWXGroup("TB_GRPMC", "HF_GRPID", "zHF_GRPID", true);
    });
    BFButtonClick("TB_ZXRMC", function () {
        SelectRYXX("TB_ZXRMC", "HF_ZXR", "zHF_ZXR", false);
    });
    //登记人弹出框
    BFButtonClick("TB_DJRMC", function () {
        SelectRYXX("TB_DJRMC", "HF_DJR", "zHF_DJR", false);
    });
    BFButtonClick("TB_ZXRMC", function () {
        SelectRYXX("TB_ZXRMC", "HF_ZXR", "zHF_ZXR", false);
    });

});

function SaveData() {
    var Obj = new Object();
    var rowid = $("#list").jqGrid("getGridParam", "selrow");
    var rowData = $("#list").getRowData(rowid);
    Obj.iJLBH = rowData.iJLBH;
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    return Obj;
}

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "TB_GROUPJL_NAME", "sGROUPJL_NAME", "like", true);//改成模糊查询
    MakeSrchCondition(arrayObj, "HF_GRPID", "iGROUPID", "=", false);
    MakeSrchCondition(arrayObj, "TB_DJRMC", "sDJRMC", "=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRMC", "sZXRMC", "=", true);
    MakeSrchCondition(arrayObj, "TB_ZY", "sZY", "like", true);//改成模糊查询
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ1", "dZXRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ2", "dZXRQ", "<=", true);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};

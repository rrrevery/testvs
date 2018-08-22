vUrl = "../HYKGL.ashx";
vCZK = GetUrlParam("czk");
var vOLDDB = GetUrlParam("old");
vCaption = vCZK == "1" ? "面值库存卡有效期变动" : "会员库存卡有效期变动";

function InitGrid() {
    vColumnNames = ['记录编号', '卡类型', '卡类型代码', '新有效期', '操作门店', '更改卡数量', '登记人代码', '登记人名称', '登记时间', '执行人代码', '审核人', '执行日期', ];
    vColumnModel = [
            { name: 'iJLBH', index: 'iJLBH', width: 80, },//sortable默认为true width默认150
			{ name: 'sHYKNAME', width: 80, },
			{ name: 'iHYKTYPE', hidden: true, },
			{ name: 'dXYXQ', width: 80, },
            { name: 'sMDMC', width: 80, },
            { name: 'iKSL', width: 80, },
			{ name: 'iDJR', width: 80, hidden: true, },
			{ name: 'sDJRMC', },
            { name: 'dDJSJ', width: 80, },
            { name: 'iZXR', width: 80, hidden: true, },
            { name: 'sZXRMC', width: 80, },
	        { name: 'dZXRQ', width: 80, },
    ];
};

$(document).ready(function () {

    ConbinDataArry["czk"] = vCZK;
    ConbinDataArry["old"] = vOLDDB;

    $("#TB_DJRMC").click(function () {
        SelectRYXX("TB_DJRMC", "HF_DJR", "zHF_DJR");
    });
    $("#TB_ZXRMC").click(function () {
        SelectRYXX("HF_ZXR", "TB_ZXRMC", "zHF_ZXR");
    });
    $("#TB_MDMC").click(function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", false);
    });
    $("#TB_HYKNAME").click(function () {
        SelectKLX("TB_HYKNAME", "HF_HYKTYPE", "zHF_HYKTYPE", false);
    })
});


function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "HF_MDID", "iMDID", "in", false);
    MakeSrchCondition(arrayObj, "HF_HYKTYPE", "iHYKTYPE", "in", false);
    MakeSrchCondition(arrayObj, "TB_XYXQ1", "dXYXQ", " >=", true);
    MakeSrchCondition(arrayObj, "TB_XYXQ2", "dXYXQ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRMC", "sZXRMC", " =", true);
    MakeSrchCondition(arrayObj, "TB_DJRMC", "sDJRMC", " =", true);
    MakeSrchCondition(arrayObj, "TB_SHSJ1", "dZXRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_SHSJ2", "dZXRQ", "<=", true);
   // MakeSrchCondition2(arrayObj, vCZK, "iCZK", "=", false);
    return arrayObj;
};


function AddCustomerCondition(Obj) {
    if ($("#TB_HYKNO").val() != "") {
        Obj.sHYKNO = $("#TB_HYKNO").val();
    }
}
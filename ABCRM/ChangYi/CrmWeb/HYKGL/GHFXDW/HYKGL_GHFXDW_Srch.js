vCZK = IsNullValue(GetUrlParam("czk"), "0");
vUrl = vCZK == "0" ? "../HYKGL.ashx" : "../../MZKGL/MZKGL.ashx";
vCaption = "更换发行单位";


function InitGrid() {
    vColumnNames = ['记录编号', '新发行单位', '操作门店', 'iDJR', '登记人', '登记时间', 'iZXR', '审核人', '审核日期', '包含卡数', '摘要'];
    vColumnModel = [
		   { name: 'iJLBH', index: 'iJLBH', width: 80, },//sortable默认为true width默认150
			//{ name: 'sHYKNAME', width: 80, },
			{ name: 'sFXDWMC', width: 80, },
           // { name: 'sBGDDMC',width:80 },
            { name: 'sMDMC', width: 80 },
			{ name: 'iDJR', hidden: true, },
			{ name: 'sDJRMC', width: 80, },
			{ name: 'dDJSJ', width: 120, },
			{ name: 'iZXR', hidden: true, },
			{ name: 'sZXRMC', width: 80, },
			{ name: 'dZXRQ', width: 120, },
            { name: 'iBHKS', width: 80 },
			{ name: 'sZY', width: 120, },

    ];
};

$(document).ready(function () {


    $("#TB_DJRMC").click(function () {
        SelectRYXX("TB_DJRMC", "HF_DJR");
    });
    $("#TB_ZXRMC").click(function () {
        SelectRYXX("TB_ZXRMC", "HF_ZXR");
    });

    $("#TB_MDMC").click(function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID");
    });

    $("#TB_BGDDMC").click(function () {
        SelectBGDD("TB_BGDDMC", "HF_BGDDDM", "zHF_BGDDDM");
    });
    $("#TB_FXDW").click(function () {
        SelectFXDW("TB_FXDW", "HF_FXDWDM", "zHF_FXDWID");
    });

});

function AddCustomerCondition(Obj) {
    Obj.sBHKH = $("#TB_BHKH").val();
}
function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "HF_FXDWDM", "sFXDWDM", "in", false);
    MakeSrchCondition(arrayObj, "HF_MDID", "iMDID_CZ", "=", false);
    MakeSrchCondition(arrayObj, "TB_DJRMC", "sDJRMC", "=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRMC", "sZXRMC", "=", true);
    MakeSrchCondition(arrayObj, "TB_ZY", "sZY", "=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ1", "dZXRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ2", "dZXRQ", "<=", true);
    MakeSrchCondition(arrayObj, "HF_BGDDDM", "sBGDDDM", "in", false);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;

};


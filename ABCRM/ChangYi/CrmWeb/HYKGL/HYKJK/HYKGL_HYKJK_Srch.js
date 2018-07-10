//vPageMsgID = CM_HYKGL_HYKJK;
//bCanEdit = CheckMenuPermit(iDJR, CM_HYKGL_HYKJK_LR);
//bCanExec = CheckMenuPermit(iDJR, CM_HYKGL_HYKJK_SH);
//bCanSrch = CheckMenuPermit(iDJR, CM_HYKGL_HYKJK_CX);
//vUrl = "../HYKGL.ashx";
//var vCZK = GetUrlParam("czk");
var vCZK = IsNullValue(GetUrlParam("czk"), "0");
vUrl = vCZK == "0" ? "../HYKGL.ashx" : "../../MZKGL/MZKGL.ashx";
vCaption = vCZK == "0" ? "会员卡建卡" : "面值卡建卡";

function InitGrid() {
    vColumnNames = ['记录编号', '卡类型', '卡类型代码', '保管地点', '保管地点代码', '发行单位', '发行单位ID', '建卡数量', '写卡数量', "开始卡号", "结束卡号", "登记人ID", "登记人", "登记时间", "审核人ID", "审核人", "审核日期", '备注'];
    vColumnModel = [
        { name: 'iJLBH', index: 'iJLBH', width: 80, },//sortable默认为true width默认150
        { name: 'sHYKNAME', width: 80, },
        { name: 'iHYKTYPE', hidden: true, },
        { name: 'sBGDDMC', width: 80, },
        { name: 'sBGDDDM', hidden: true, },
        { name: 'sFXDWMC', width: 80, },
        { name: 'iFXDWID', hidden: true, },
        { name: 'iJKSL', width: 80, },
        { name: 'iXKSL', width: 80, },
        { name: 'sCZKHM_BEGIN', width: 80, },
        { name: 'sCZKHM_END', width: 80, },
        { name: 'iDJR', hidden: true, },
        { name: 'sDJRMC', width: 90, },
        { name: 'dDJSJ', width: 150, },
        { name: 'iZXR', hidden: true, },
        { name: 'sZXRMC', width: 90, },
        { name: 'dZXRQ', width: 120, },
        { name: 'sZY', width: 80, },
    ];
};
$(document).ready(function () {
    ConbinDataArry["czk"] = vCZK;

    $("#TB_DJRMC").click(function () {
        SelectRYXX("TB_DJRMC", "HF_DJR", "zHF_DJR", false);
    });
    $("#TB_ZXRMC").click(function () {
        SelectRYXX("TB_ZXRMC", "HF_ZXR", "zHF_ZXR", false);
    });
    $("#TB_BGDDDM").click(function () {
        SelectBGDD("TB_BGDDDM", "HF_BGDDDM", "zHF_BGDDDM", false);
    });
    $("#TB_HYKNAME").click(function () {
        var condData = new Object();
        condData["vCZK"] = vCZK || 0;
        SelectKLX("TB_HYKNAME", "HF_HYKTYPE", "zHF_HYKTYPE", false, condData);
    });
    $("#TB_FXDW").click(function () {
        SelectHYKFXDW("TB_FXDW", "HF_FXDWID", "zHF_FXDWID", false);
    });
    $("#Button1").click(function () {
        var ret;
        DoShowTest1(123, function (ret) {
            $("#Text1").val(ret);
        });
    });
});

function MakeSearchCondition() {
    var arrayObj = new Array();
    var vDJZT = $("[name='djzt']:checked").val();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "TB_JKSL", "iJKSL", "=", false);
    MakeSrchCondition(arrayObj, "HF_DJR", "iDJR", " =", false);
    MakeSrchCondition(arrayObj, "HF_FXDWID", "iFXDWID", " =", false);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeSrchCondition(arrayObj, "HF_ZXR", "iZXR", " =", false);
    MakeSrchCondition(arrayObj, "TB_ZXRQ1", "dZXRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ2", "dZXRQ", "<=", true);
    MakeSrchCondition(arrayObj, "HF_BGDDDM", "sBGDDDM", "in", false);
    MakeSrchCondition(arrayObj, "HF_HYKTYPE", "iHYKTYPE", "in", false);
    MakeSrchCondition(arrayObj, "TB_HYKNO", "sCZKHM_BEGIN", "<=", true);
    MakeSrchCondition(arrayObj, "TB_HYKNO", "sCZKHM_END", ">=", true);
    MakeSrchCondition(arrayObj, "TB_ZY", "sZY", "like", true);
    MakeSrchCondition2(arrayObj, vCZK, "iBJ_CZK", "=", false);
    if (vDJZT == 1) {
        MakeSrchCondition2(arrayObj, "NOT NULL", "iZXR", "is", false);
    }
    if (vDJZT == 2) {
        MakeSrchCondition2(arrayObj, " NULL", "iZXR", "is", false);
    }
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
}
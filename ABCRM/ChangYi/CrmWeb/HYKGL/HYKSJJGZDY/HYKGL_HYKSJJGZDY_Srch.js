vUrl = "../HYKGL.ashx";
sj = GetUrlParam("sj");
var vCaption;
if (sj == 1)
    vCaption = "会员卡升级规则定义";
if (sj == 0)
    vCaption = "会员卡降级规则定义";

var mdid = 0;

function InitGrid() {
    vColumnNames = ['HYKKZID', '卡种', '原卡等级', '原卡类型', 'HYKTYPE', '新卡等级', '新卡类型', 'HYKTYPE_NEW', '起点积分', "起点消费金额", '消费金额方式', "判断标准"];
    vColumnModel = [
            { name: 'iHYKKZID', hidden: true, },
            { name: 'sHYKKZNAME', width: 80, },
			{ name: 'sHYKJCNAME', width: 80, },
            { name: 'sHYKNAME', width: 80, },
            { name: 'iHYKTYPE_OLD', hidden: true, },
            { name: 'sHYKJCNAME_NEW', width: 80, },
            { name: 'sHYKNAME_NEW', width: 80, },
            { name: 'iHYKTYPE_NEW', hidden: true, },
            { name: 'fQDJF', width: 80, },
            { name: 'fDRXFJE', width: 80, },
            { name: 'sXFSJ', width: 80,  hidden: true, },
            { name: 'sBJ_XFJE', width: 80 },
    ];
};

function DBClickRow(rowIndex, rowData) {
    //T--表格双击事件
    if ($("#B_Insert #B_Update").css("display") != "none") {
        var sDbCurrentPath = "";
        ConbinDataArry["HYKTYPE_NEW"] = rowData.iHYKTYPE_NEW;
        ConbinDataArry["HYKTYPE_OLD"] = rowData.iHYKTYPE_OLD;
        ConbinDataArry["MDID"] = rowData.iMDID;
        ConbinDataArry["XFSJ"] = rowData.iXFSJ == "当日" ? 1 : 0;
        sDbCurrentPath = sCurrentPath + "?jlbh=" + rowData.iJLBH;
        sDbCurrentPath = ConbinPath(sDbCurrentPath);
        MakeNewTab(sDbCurrentPath, vCaption, vPageMsgID);
    }
};

function UpdateClick() {
    //T-- 修改事件
    var sUpdateCurrentPath = "";
    var rowData = $("#list").datagrid("getSelected");
    ConbinDataArry["HYKTYPE_NEW"] = rowData.iHYKTYPE_NEW;
    ConbinDataArry["HYKTYPE_OLD"] = rowData.iHYKTYPE_OLD;
    sUpdateCurrentPath = sCurrentPath + "?action=edit&jlbh=" + vJLBH;
    sUpdateCurrentPath = ConbinPath(sUpdateCurrentPath);
    MakeNewTab(sUpdateCurrentPath, vCaption, vPageMsgID);
};


$(document).ready(function () {
    ConbinDataArry["sj"] = sj;
    $("#B_Exec").hide();
    //$("#TB_MDMC").click(function () {
    //    SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", true);
    //})

    $("#TB_HYKNAME_OLD").click(function () {
        SelectKLX("TB_HYKNAME_OLD", "HF_HYKTYPE_OLD", "zHF_HYKTYPE_OLD", true);
    });
    $("#TB_HYKNAME_NEW").click(function () {
        SelectKLX("TB_HYKNAME_NEW", "HF_HYKTYPE_NEW", "zHF_HYKTYPE_NEW", true);
    });
    $("#HF_BJ_SJ").val(sj);
    //$("#S_MDMC").change(function () {
    //    $("#HF_MDID").val($(this).val());
    //});

});

function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition2(arrayObj, sj, "iBJ_SJ", "=", false);
    MakeSrchCondition(arrayObj, "HF_HYKTYPE_OLD", "iHYKTYPE_OLD", "=", false);
    MakeSrchCondition(arrayObj, "HF_HYKTYPE_NEW", "iHYKTYPE_NEW", "=", false);
    MakeSrchCondition(arrayObj, "TB_QDJF", "fQDJF", "=", false);
    //MakeSrchCondition(arrayObj, "HF_MDID", "iMDID", "=", false);
    var bj_xfje = $("[name='CB_BJ_XFJE']:checked").val();
    if (bj_xfje != "all") {
        MakeSrchCondition2(arrayObj, bj_xfje, "iBJ_XFJE", "=", false);
    }
    return arrayObj;
};
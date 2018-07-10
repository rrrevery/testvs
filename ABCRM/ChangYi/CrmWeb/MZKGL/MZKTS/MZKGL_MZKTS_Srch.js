vUrl = "../MZKGL.ashx";
vCaption = "面值卡退售"

function InitGrid()
{
    vColumnNames = ['记录编号', '交款单标记', '交款单编号', '返利单标记', '返利单编号', '赠礼数', '赠礼金额', '方式', 'MDID_CZMC', '操作门店', 'sBGDDDM', '保管地点', '退卡数量', '应退总额', '实退金额', '有效期', '状态', '赠卡数', '赠卡金额', '摘要', 'DJR', '登记人', '登记时间', 'ZXR', '审核人', '审核日期', ];
    vColumnModel = [
		    { name: 'iJLBH', index: 'JLBH', width: 80, },//sortable默认为true width默认150
            {
                 name: 'BJ_JKD', width: 50, hidden: true, formatter: function (cellvalue, options, rowObject)
                 {
                     if (cellvalue == 0) { return "未交款"; }
                     if (cellvalue == 1) { return "交款中"; }
                     if (cellvalue == 2) { return "已交款"; }
                 },
            },
            { name: 'JKDJLBH', index: 'JLBH', width: 80, hidden: true, },
            {
                name: 'BJ_FL', width: 50, hidden: true, formatter: function (cellvalue, options, rowObject)
                {
                    if (cellvalue == 0) { return "未返利"; }
                    if (cellvalue == 1) { return "返利中"; }
                    if (cellvalue == 2) { return "已返利"; }
                },
            },
            { name: 'FLJLBH', index: 'JLBH', width: 80, hidden: true, },
            { name: 'ZKLPSL', width: 80, hidden: true, },
            { name: 'ZKLPSJE', width: 80, hidden: true, },

            {
                name: 'FS', width: 50, formatter: function (cellvalue, options, rowObject)
                {
                    if (cellvalue == 0) { return "会员卡发售"; }
                    if (cellvalue == 1) { return "发售"; }
                    if (cellvalue == 2) { return "退售"; }
                },
            },
            { name: 'MDID_CZ', hidden: true, },
            { name: 'MDMC', },
            { name: 'BGDDDM', hidden: true, },
            { name: 'BGDDMC', },
            { name: 'SKSL', width: 60, },
            { name: 'YSZE', width: 60, },
            { name: 'SSJE', width: 60, hidden: true },
            { name: 'YXQ', hidden: true, },
            {
                name: 'STATUS', width: 60, formatter: function (cellvalue)
                {
                    switch (cellvalue)
                    {
                        case 0:
                            return "保存"; break;
                        case 1:
                            return "审核"; break;
                        case 2:
                            return "启动"; break;
                        default:
                            return "未知"; break;
                    }
                }
            },
            { name: 'ZKSL', width: 80, hidden: true, },
            { name: 'SJZSJE', width: 80, hidden: true, },
			{ name: 'ZY', width: 120, },
            { name: 'iDJR', hidden: true, },
			{ name: 'sDJRMC', width: 80, },
			{ name: 'dDJSJ', width: 120, },
			{ name: 'ZXR', hidden: true, },
			{ name: 'ZXRMC', width: 80, },
			{ name: 'ZXRQ', width: 120, },


    ];
}

$(document).ready(function ()
{
    $("#TB_BGDDDM").click(function ()
    {
        SelectBGDD("TB_BGDDDM", "HF_BGDDDM", "zHF_BGDDDM", false);
    });

    $("#TB_DJRMC").click(function ()
    {
        SelectRYXX("TB_DJRMC", "HF_DJR");
    });
    $("#TB_ZXRMC").click(function ()
    {
        SelectRYXX("TB_ZXRMC", "HF_ZXR");
    });

    $("#TB_HYKNAME").click(function ()
    {
        var condData = new Object();
        condData["vCZK"] = 1;
        SelectKLX("TB_HYKNAME", "HF_HYKTYPE", "zHF_HYKTYPE", false, condData);
    });
    $("#TB_MDMC").click(function ()
    {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", false);
    });
});

function MakeSearchCondition()
{
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "HF_BGDDDM", "sBGDDDM", "in", true);
    //MakeSrchCondition(arrayObj, "TB_BGDDDM", "sBGDDDM", "=", true);
    MakeSrchCondition(arrayObj, "TB_SKSL", "iSKSL", "=", false);
    MakeSrchCondition(arrayObj, "HF_MDID", "iMDID_CZ", " in", false);
    //  MakeSrchCondition(arrayObj, "TB_YJQ", "dYXQ", "=", true);
    //MakeSrchCondition(arrayObj, "TB_HYKNO", "sCZKHM", "=", true);
    MakeSrchCondition(arrayObj, "HF_DJR", "iDJR", "=", false);
    MakeSrchCondition(arrayObj, "HF_ZXR", "iZXR", "=", false);
    //MakeSrchCondition(arrayObj, "HF_FS", "iFS", "=", false);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ1", "dZXRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ2", "dZXRQ", "<=", true);
    return arrayObj;

};
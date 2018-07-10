vUrl = "../MZKGL.ashx";
vCaption = "面值卡客户档案录入";



function InitGrid() {
    vColumnNames = ["记录编号", "客户名称", "客户地址", "联系人名称", "联系人电话码", "联系人手机", "DJR", "登记人", "登记时间", ];
    vColumnModel = [
                    { name: "iJLBH", width: 80, },
                    { name: "sKHMC", width: 80 },
                    { name: "sKHDZ", width: 80, },
                    { name: "sLXRXM", width: 80 },
                    { name: 'sLXRDH', width: 80, },
                    { name: 'sLXRSJ', width: 80, },
                    { name: 'iDJR', hidden: true, },
                    { name: 'sDJRMC', width: 80, },
                    { name: 'dDJSJ', width: 120, },


    ];
};


$(document).ready(function () {

    $("#TB_DJRMC").click(function () {
        SelectRYXX("TB_DJRMC", "HF_DJR");
    });
    $("#TB_ZXRMC").click(function () {
        SelectRYXX("TB_ZXRMC", "HF_ZXR");
    });


});




function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "TB_DJRMC", "sDJRMC", "=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRMC", "sZXRMC", "=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ1", "dZXRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ2", "dZXRQ", "<=", true);
    return arrayObj;
};

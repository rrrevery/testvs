vUrl = "../GTPT.ashx";
vDJLX = 0;
vCaption = "积分兑换比例单";

function InitGrid() {
    vColumnNames = ['记录编号', '开始日期', '结束日期', '登记人', 'DJR', '登记时间', '审核人', 'ZXR', '审核时间', '启动人', 'QDR', '启动时间', '终止人', 'ZZR', '终止时间', '状态'],// '登记类型','启动人', 'QDR', '启动时间',
    vColumnModel = [
        { name: 'iJLBH', index: 'iJLBH', width: 80, },
        //{ name: 'iDJLX', width: 120, },
        { name: 'dKSRQ', width: 120, },
        { name: 'dJSRQ', width: 120, },
		{ name: 'sDJRMC', width: 80, },
        { name: 'iDJR', hidden: true, },
		{ name: 'dDJSJ', width: 150, },
		{ name: 'sZXRMC', width: 80, },
        { name: 'iZXR', hidden: true, },
		{ name: 'dZXRQ', width: 150, },
        { name: 'sQDRMC', width: 80, },
        { name: 'iQDR', hidden: true, },
		{ name: 'dQDSJ', width: 100, },
        { name: 'sZZRMC', width: 80, },
        { name: 'iZZR', hidden: true, },
		{ name: 'dZZSJ', width: 150, },
          {
              name: 'iSTATUS', width: 80, formatter: function (cellvalue, icol) {
                  switch (cellvalue) {
                      case 0:
                          return "保存";
                          break;
                      case 1:
                          return "审核";
                          break;
                      case 2:
                          return "启动";
                          break;
                      case 3:
                          return "终止";
                          break;
                  }
              }
          },

    ]

}
$(document).ready(function () {

    $("#TB_DJRMC").click(function () {
        SelectRYXX("HF_DJR", "TB_DJRMC");
    });
    $("#TB_ZXRMC").click(function () {
        SelectRYXX("HF_ZXR", "TB_ZXRMC");
    });
    $("#TB_QDRMC").click(function () {
        SelectRYXX("HF_QDR", "TB_QDRMC");
    });
    $("#TB_ZZRMC").click(function () {
        SelectRYXX("HF_ZZR", "TB_ZZRMC");
    });
 
})


function MakeSearchCondition() {
    var arrayObj = new Array();
    //MakeSrchCondition2(arrayObj, vDJLX, "iDJLX", "=", false);
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "TB_GZID", "iGZID", "=", false);
    MakeSrchCondition(arrayObj, "TB_KSRQ", "dKSRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_JSRQ", "dJSRQ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_DJRMC", "sDJRMC", "=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRMC", "sZXRMC", "=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ1", "dZXRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ2", "dZXRQ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_QDRMC", "sQDRMC", "=", true);
    MakeSrchCondition(arrayObj, "TB_ZZRMC", "sZZRMC", "=", true);
    MakeSrchCondition(arrayObj, "TB_QDSJ", "dQDSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_ZZSJ", "dZZSJ", ">=", true);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};
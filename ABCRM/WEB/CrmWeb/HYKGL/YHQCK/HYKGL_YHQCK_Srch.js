vUrl = "../HYKGL.ashx";
var vCZK = 0;

vCaption ="优惠券存款"

function InitGrid() {
    vColumnNames = ['记录编号', '卡号', '姓名', 'HYID', '操作门店', '优惠券', 'iYHQID', '结束日期', '促销活动', 'iCXID', '存款金额', '原金额', 'DJR', '登记人', '登记时间', 'ZXR', '审核人', '审核日期', '摘要', ];
    vColumnModel = [
                     { name: 'iJLBH', index: 'iJLBH', width: 80, },//sortable默认为true width默认150
            //{ name: 'sBGDDMC', },
            //{ name: 'sBGDDDM', hidden: true, },
            { name: 'sHYKNO', width: 80, },
			{ name: 'sHY_NAME', width: 80, },
            { name: 'iHYID', hidden: true, },
            { name: 'sMDMC', width: 120 },
            { name: 'sYHQMC', },
            { name: 'iYHQID', hidden: true, },
            { name: 'dJSRQ', width: 120, },
            { name: 'sCXZT', },
            { name: 'iCXID', hidden: true, },
            { name: 'fCKJE', width: 80, },
            { name: 'fYYE', width: 80, },

            { name: 'iDJR', hidden: true, },
			{ name: 'sDJRMC', width: 80, },
			{ name: 'dDJSJ', width: 120, },
			{ name: 'iZXR', hidden: true, },
			{ name: 'sZXRMC', width: 80, },
			{ name: 'dZXRQ', width: 120, },
			{ name: 'sZY', width: 120, },


    ];
}
$(document).ready(function () {

    ConbinDataArry["czk"] = vCZK;


    //$("#list").jqGrid("setGridParam", {
    
    //    onCellSelect: function (rowid) {
    //        var rowData = $("#list").getRowData(rowid);
    //        var bExecuted = rowData.iZXR != "0";//$("#LB_ZXRMC").text() != "";//已审核
    //        var bHasData = rowData.iJLBH != "0";//$("#TB_JLBH").text() != "";//有数据
    //        vJLBH = rowData.iJLBH;

    //        document.getElementById("B_Update").disabled = !(bHasData && !bExecuted);
    //        document.getElementById("B_Delete").disabled = !(bHasData && !bExecuted);
    //        document.getElementById("B_Exec").disabled = !(bHasData && !bExecuted);
    //    },
    //});
  


    $("#TB_DJRMC").click(function () {
        SelectRYXX("TB_DJRMC", "HF_DJR", "zHF_DJR", false);
    });
    $("#TB_ZXRMC").click(function () {
        SelectRYXX("TB_ZXRMC", "HF_ZXR", "zHF_ZXR", false);
    });

    $("#TB_BGDDMC").click(function () {
        SelectBGDD("TB_BGDDMC", "HF_BGDDDM", "zHF_BGDDDM", false);
    });

    $("#TB_YHQ").click(function () {
        SelectYHQ("TB_YHQ", "HF_YHQ", "zHF_YHQ", false);
    });
    $("#TB_CXHD").click(function () {
        SelectCXHD("TB_CXHD", "HF_CXHD", "zHF_CXHD", false);
    });

});



function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "HF_MDID", "iMDID", "in", false);
    //MakeSrchCondition(arrayObj, "HF_BGDDDM", "sBGDDDM", "in", true);
    MakeSrchCondition(arrayObj, "TB_HYKNO", "sHYKNO", "=", true);
    //  MakeSrchCondition(arrayObj, "HF_YHQID", "iYHQID", "=", false);
    MakeSrchCondition(arrayObj, "HF_YHQ", "iYHQID", "=", false);
    MakeSrchCondition(arrayObj, "HF_CXHD", "iCXID", "in", false);
    //MakeSrchCondition(arrayObj, "HF_CXID", "iCXID", "=", false);
    MakeSrchCondition(arrayObj, "TB_YJE", "fYJE", "=", false);
    MakeSrchCondition(arrayObj, "TB_JSRQ", "dJSRQ", "=", true);
    //MakeSrchCondition(arrayObj, "HF_DJR", "iDJR", "=", false);
    MakeSrchCondition(arrayObj, "TB_DJRMC", "sDJRMC", "=", true);
    //MakeSrchCondition(arrayObj, "HF_ZXR", "iZXR", "=", false);
    MakeSrchCondition(arrayObj, "TB_ZXRMC", "sZXRMC", "=", true);
    MakeSrchCondition(arrayObj, "TB_CKJE", "fCKJE", "=", false);
    MakeSrchCondition(arrayObj, "TB_ZY", "sZY", "=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ1", "dZXRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ2", "dZXRQ", "<=", true);
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};

function WUC_BGDD_Return() {
    var tp_return = $.dialog.data('IpValuesReturn');
    if (tp_return) {
        var jsonString = tp_return;


        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            $("#TB_BGDDDM").val(tp_mc);
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput.Depts;
            for (var i = 0; i <= contractValues.length - 1; i++) {
                tp_mc += contractValues[i].Name + ";";
                tp_hf += "'" + contractValues[i].Code + "',";
            }
            $("#TB_BGDDDM").val(tp_mc);
            $("#HF_BGDDDM").val(tp_hf.substr(0, tp_hf.length - 1));
            $("#zHF_BGDDDM").val(jsonString);
        }
    }
}

function WUC_YHQ_Return() {
    var tp_return = $.dialog.data('IpValuesReturn');
    var tp_return_ChoiceOne = $.dialog.data('IpValuesReturnChoiceOne');
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            $("#TB_YHQ").val(tp_mc);
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput.Articles;
            for (var i = 0; i <= contractValues.length - 1; i++) {
                tp_mc += contractValues[i].Name + ";";
                if (tp_return_ChoiceOne) {
                    tp_hf += contractValues[i].Code;
                } else {
                    tp_hf += contractValues[i].Code + ";";
                }
            }
            $("#TB_YHQ").val(tp_mc);
            $("#HF_YHQ").val(tp_hf);
            $("#zHF_YHQ").val(jsonString);
        }
    }
}

function WUC_CXHD_Return() {
    var tp_return = $.dialog.data('IpValuesReturn');
    var tp_return_ChoiceOne = $.dialog.data('IpValuesReturnChoiceOne');
    if (tp_return) {
        var jsonString = tp_return;
        if (jsonString != null && jsonString.length > 0) {
            var tp_mc = "";
            var tp_hf = "";
            $("#TB_CXHD").val(tp_mc);
            var jsonInput = JSON.parse(jsonString);
            var contractValues = new Array();
            contractValues = jsonInput.Articles;
            for (var i = 0; i <= contractValues.length - 1; i++) {
                tp_mc += contractValues[i].sCXZT + ";";
                if (tp_return_ChoiceOne) {
                    tp_hf += contractValues[i].iCXID;
                } else {
                    tp_hf += contractValues[i].iCXID + ",";
                }
            }
            $("#TB_CXHD").val(tp_mc);
            $("#HF_CXHD").val(tp_hf.substr(0, tp_hf.length - 1));
            $("#zHF_CXHD").val(jsonString);
        }
    }
}
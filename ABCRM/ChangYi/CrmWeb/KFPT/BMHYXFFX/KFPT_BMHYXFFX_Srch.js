vUrl = "../KFPT.ashx";
var aa = "";

function InitGrid() {
    vColumnNames = ['会员卡号', '姓名', '性别', '手机号码', '通讯地址', '客服经理', '部门', '商户', '消费金额'];
    vColumnModel = [
            { name: 'sHYK_NO', width: 120, },

            { name: 'sHY_NAME', width: 60, },
             {
                 name: 'iSEX', width: 40, formatter: function (cellvalues) {
                     return cellvalues == "0" ? "男" : "女";
                 }
             },
			{ name: 'sSJHM', width: 80, },
			{ name: 'sTXDZ', width: 120, },
			{ name: 'sKFJLMC', width: 80, },
			{ name: 'sBMMC', hidden: false, },
			{ name: 'sSHMC', width: 80, },
			{ name: 'fXFJE', width: 60, },
    ];
}
$(document).ready(function () {
    $("#a").hide();
    $("#B_Insert").hide();
    $("#B_Update").hide();
    $("#TB_HYKNAME").click(function () {
        SelectKLX("TB_HYKNAME", "HF_HYKTYPE", "zHF_HYKTYPE", false);
    });

});

function SaveData() {
    var Obj = new Object();
    var rowid = $("#list").jqGrid("getGridParam", "selrow");
    var rowData = $("#list").getRowData(rowid);
    Obj.iJLBH = rowData.iJLBH;
    obj.iSTATUS = rowData.iSTATUS;
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;

    return Obj;
}

//function DoSearch() {

//    if ($("#TB_HYK_NO").val() == "") {
//        art.dialog({ content: '请输入卡号!', lock: true });
//        return false;
//    }


//    var aa = $('input:radio[name="cld"]:checked').val(); //选择被选中Radio的Value值


//    var arrayObj = new Array();
//    var sjson = "";

//    if (iDJR != "") {
//        sjson += ',"iLoginRYID":' + iDJR;
//    }


//    if (aa != "") {
//        sjson += ',"aa":' + aa;
//    }

//    sjson += '}';


//    //MakeSrchCondition(arrayObj, "HF_BMJC", "iBMJC", "=", false);
//    MakeSrchCondition(arrayObj, "TB_RQ1", "dRQ", ">=", true);
//    MakeSrchCondition(arrayObj, "TB_RQ2", "dRQ", "<=", true);
//    MakeSrchCondition(arrayObj, "TB_HYK_NO", "sHYK_NO", "=", true);
//    MakeSrchCondition(arrayObj, "TB_HYKNAME", "sHYKNAME", "=", true);
//    MakeSrchCondition(arrayObj, "TB_SJHM", "sSJHM", "=", true);

//    MakeMoreSrchCondition(arrayObj);
//    $("#list").jqGrid('setGridParam', {
//        url: vUrl + "?mode=Search&func=" + vPageMsgID,
//        postData: { 'afterFirst': JSON.stringify(arrayObj), 'conditionData': sjson },
//        //postData: { 'json': JSON.stringify(arrayObj), 'conditionData': sjson },
//        page: 1
//    }).trigger("reloadGrid");
//    location.hash = "tab2-tab";
//}

function MakeSearchCondition() {
    var aa = $('input:radio[name="cld"]:checked').val(); //选择被选中Radio的Value值
    var arrayObj = new Array();
    var sjson = "";
    if (iDJR != "") {
        sjson += ',"iLoginRYID":' + iDJR;
    }

    if (aa != "") {
        sjson += ',"aa":' + aa;
    }
    sjson += '}';

    //MakeSrchCondition(arrayObj, "HF_BMJC", "iBMJC", "=", false);
    MakeSrchCondition(arrayObj, "TB_RQ1", "dRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_RQ2", "dRQ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_HYK_NO", "sHYK_NO", "=", true);
    MakeSrchCondition(arrayObj, "TB_HYKNAME", "sHYKNAME", "=", true);
    MakeSrchCondition(arrayObj, "TB_SJHM", "sSJHM", "=", true);

    MakeMoreSrchCondition(arrayObj);
    $("#list").jqGrid('setGridParam', {
        url: vUrl + "?mode=Search&func=" + vPageMsgID,
        postData: { 'afterFirst': JSON.stringify(arrayObj), 'conditionData': sjson },
        //postData: { 'json': JSON.stringify(arrayObj), 'conditionData': sjson },
        page: 1
    }).trigger("reloadGrid");
    location.hash = "tab2-tab";
    //return arrayObj;
};





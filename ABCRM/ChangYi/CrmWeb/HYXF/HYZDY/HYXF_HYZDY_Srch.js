vUrl = "../HYXF.ashx";
vCaption = status == "0" ? "静态客群组定义" : "动态客群组定义";
var YXXG = 0;
var ZXR = "0";
var ZXRMC = "";
var ZXRQ = "";
function InitGrid() {
    vColumnNames = ['记录编号', '客群组名称', '客群组用途', '客群组级别', '客群组类型', '客群门店', '生日方式', '微信会员', '客群组描述', '开始时间', '结束时间', '更新周期（天）', '更新截止日期', '动静态标记', '允许修改标记', '状态', '登记人', 'iDJR', '登记时间', '最后修改人', 'iXGR', '修改时间', '审核人', 'iZXR', '审核日期', '终止人', 'iZZR', '终止时间', ];
    vColumnModel = [
        { name: 'iJLBH', index: 'iGROUP', width: 80, },//sortable默认为true width默认150
        { name: 'sGRPMC', width: 80, },
        {
            name: 'iGRPYT', width: 80,
            formatter: function (cellvalue, opts, rowdata) {
                switch (cellvalue) {
                    case 0:
                        return "促销";
                        break;
                    case 1:
                        return "短信";
                        break;
                    case 2:
                        return "分析";
                        break;
                    case 3:
                        return "其它";
                        break;
                }
            }
        },
        {
            name: "iJB", width: 80, formatter: function (cellvalues) {
                switch (cellvalues) {
                    case 0:
                        return "总部";
                        break;
                    case 1:
                        return "事业部";
                        break;
                    case 2:
                        return "门店";
                        break;

                }
            }
        },
        { name: 'sHYZLXMC', width: 120, },
        { name: 'sMDMC', width: 120, },
        {
            name: 'iSRFS', width: 80,
            formatter: function (cellvalue, opts, rowdata) {
                switch (cellvalue) {
                    case -1:
                        return "无";
                        break;
                    case 0:
                        return "生日当日";
                        break;
                    case 1:
                        return "生日当月";
                        break;
                    case 2:
                        return "有效期内生日";
                        break;
                }
            }
        },
        { name: 'iBJ_WXHY', width: 80, formatter: 'checkbox' },
        { name: 'sGRPMS', width: 120, },
        { name: 'dKSSJ', width: 120, },
        { name: 'dJSSJ', width: 120, },
        { name: 'iGXZQ', width: 120, hidden:  true },
        { name: 'dYXQ', width: 120, hidden: true },
        {
            name: 'iBJ_DJT', width: 80, formatter: function (cellvalues) {
                return cellvalues == "0" ? "静态" : "动态";
            }
        },
        {
            name: 'iBJ_YXXG', width: 80, formatter: function (cellvalues) {
                return cellvalues == "0" ? "禁止" : "允许";
            }
        },
        {
            name: 'iSTATUS', width: 80,
            formatter: function (cellvalue, opts, rowdata) {
                switch (cellvalue) {
                    case 0:
                        return "未审核";
                        break;
                    case 2:
                        return "已审核";
                        break;
                    case -1:
                        return "到期";
                        break;
                    case -2:
                        return "已终止";
                        break;
                    default:
                        return "";
                        break;
                }
            }
        },
        { name: 'sDJRMC', width: 80, },
        { name: 'iDJR', width: 80, hidden: true, },
        { name: 'dDJSJ', width: 130, },
        { name: 'sXGRMC', width: 80, },
        { name: 'iXGR', width: 80, hidden: true, },
        { name: 'dXGRQ', width: 130, },
        { name: 'sZXRMC', width: 80, },
        { name: 'iZXR', width: 80, hidden: true, },
        { name: 'dZXRQ', width: 130, },
        { name: 'sZZRMC', width: 80, },
        { name: 'iZZR', width: 80, hidden: true, },
        { name: 'dZZRQ', width: 130, },
    ];
};

$(document).ready(function () {
    ConbinDataArry["status"] = status;
    ConbinDataArry["pZXRQ"] = ZXRQ;
    ConbinDataArry["pYXXG"] = YXXG;
    //$("#list").jqGrid("setGridParam", {
    //    onSelectRow: function (rowid, status, e) {
    //        var rowData = $("#list").getRowData(rowid);
    //        ZXRQ = rowData.dZXRQ;
    //        YXXG = rowData.iBJ_YXXG == "允许" ? 1 : 0;
    //    },
    //    onCellSelect: function (rowid) {
    //        var rowData = $("#list").getRowData(rowid);
    //        ZXR = rowData.sZXRMC;
    //        var bExecuted = rowData.dZXRQ != "";
    //        var bHasData = rowData.iJLBH != "0";
    //        var ZZRQ = rowData.dZZRQ;
    //        vJLBH = rowData.iJLBH;
    //        YXXG = rowData.iBJ_YXXG == "允许" ? 1 : 0;
    //        if (YXXG == 1 && bExecuted && ZZRQ == "") {
    //            document.getElementById("B_Update").disabled = false;
    //        }
    //        else {
    //            document.getElementById("B_Update").disabled = !(bHasData && !bExecuted);
    //        }
    //        document.getElementById("B_Delete").disabled = !(bHasData && !bExecuted);
    //    },
    //});



    if (status == 0) {
        $("#DT").hide();
        $("#WXHY").hide();
        //jQuery("#list").setGridParam().hideCol("iGXZQ").trigger("reloadGrid");
        //jQuery("#list").setGridParam().hideCol("dYXQ").trigger("reloadGrid");
    }
    $("[name='CB_BJ_DJT'][value=" + status + "]").prop("checked", true);
    $("#HF_BJ_DJT").val(status);

    //登记人弹出框
    $("#TB_DJRMC").click(function () {
        SelectRYXX("TB_DJRMC", "HF_DJR");
    });
    //执行人弹出框
    $("#TB_ZXRMC").click(function () {
        SelectRYXX("TB_ZXRMC", "HF_ZXR");
    });
    //修改人弹出框
    $("#TB_XGRMC").click(function () {
        SelectRYXX("TB_XGRMC", "HF_XGR");
    });
    $("#TB_MDMC").click(function () {
        SelectMD("TB_MDMC", "HF_MDID", 1);
    })

    $("input[type='checkbox'][id!='BJ_WXHY']").change(function () {
        var val = $(this).val();
        var hf = $(this).attr("name").replace("CB", "HF");
        var s = $("#" + hf).val();
        var arr = s == "" ? [] : s.split(",");
        if (this.checked) {
            if (val == "") {
                arr.length = 0;
                $(this).siblings().prop("checked", false);
            }
            else {
                $(this).siblings().eq(0).prop("checked", false);
                arr.push(val);
            }
        }
        else {
            if (val != "") {
                var index = $.inArray(val, arr);
                arr.splice(index, 1);
            }

        }
        $("#" + hf).val(arr.toString());
        $(this).prop("checked", this.checked);
    });

    FillHYZLXTree("TreeHYZLX", "TB_HYZLXMC");

});

function TreeNodeClickCustom(e, treeId, treeNode) {
    $("#HF_HYZLXID").val(treeNode.iHYZLXID);
};

//点击删除和修改时使用，作为依据
function SaveData() {
    var Obj = new Object();
    var rowid = $("#list").jqGrid("getGridParam", "selrow");
    var rowData = $("#list").getRowData(rowid);
    Obj.iJLBH = rowData.iJLBH;
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    Obj.iHYID = rowData.iHYID;
    Obj.iHYKTYPE = rowData.iHYKTYPE;
    return Obj;
}


function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);
    MakeSrchCondition(arrayObj, "HF_HYZLXID", "iHYZLXID", "=", false);
    if ($("#TB_GRPMC").val() != "")
        MakeSrchCondition2(arrayObj, "%" + $("#TB_GRPMC").val() + "%", "sGRPMC", "like", true);
    MakeSrchCondition(arrayObj, "S_GRPYT", "iGRPYT", "=", false);
    MakeSrchCondition(arrayObj, "HF_SRFS", "iSRFS", "in", false);
    MakeSrchCondition(arrayObj, "HF_STATUS", "iSTATUS", "in", false);
    MakeSrchCondition(arrayObj, "TB_KSSJ1", "dKSSJ", ">=", true);//加一个
    MakeSrchCondition(arrayObj, "TB_KSSJ2", "dKSSJ", "<=", true);//加一个
    MakeSrchCondition(arrayObj, "TB_JSSJ1", "dJSSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_JSSJ2", "dJSSJ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_YXQ", "dYXQ", "=", true);//加一个
    if ($("#HF_BJ_DJT").val() == "1") {
        MakeSrchCondition(arrayObj, "HF_BJ_DJT", "iBJ_DJT", "=", false);
    }
    else {
        MakeSrchCondition2(arrayObj, "0 or W.BJ_DJT is null", "iBJ_DJT", "=", false);
    }
    MakeSrchCondition(arrayObj, "HF_BJ_YXXG", "iBJ_YXXG", "in", false);
    if ($("#TB_GRPMS").val() != "")
        MakeSrchCondition2(arrayObj, "%" + $("#TB_GRPMS").val() + "%", "sGRPMS", "=", true);
    MakeSrchCondition(arrayObj, "HF_XGR", "iXGR", "=", false);
    MakeSrchCondition(arrayObj, "TB_GXZQ", "iGXZQ", "=", false);
    MakeSrchCondition(arrayObj, "HF_MDID", "iMDID", "in", false);
    MakeSrchCondition(arrayObj, "TB_XGRQ1", "dXGRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_XGRQ2", "dXGRQ", "<=", true);
    MakeSrchCondition(arrayObj, "HF_DJR", "iDJR", "=", false);
    MakeSrchCondition(arrayObj, "HF_ZXR", "iZXR", "=", false);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ1", "dZXRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ2", "dZXRQ", "<=", true);
    var iBJ_WXHY = $("#BJ_WXHY")[0].checked ? "1" : "0";
    if ($("#BJ_WXHY")[0].checked) {
        MakeSrchCondition2(arrayObj, iBJ_WXHY, "iBJ_WXHY", "=", false);
    }
    MakeMoreSrchCondition(arrayObj);
    return arrayObj;
};
function SetControlState() {
    $("#HF_BJ_DJT").val(status);
}
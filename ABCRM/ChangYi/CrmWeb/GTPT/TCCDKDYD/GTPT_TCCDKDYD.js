vUrl = "../GTPT.ashx";
var irow = 0;

$(document).ready(function () {
    FillWXMD($("#DDL_WX_MDID"));//门店下拉框

    $("#ZZR").show();
    $("#ZZSJ").show();
    $("#QDR").show();
    $("#QDSJ").show();
    $("#B_Start").show();
    $("#B_Stop").show();

    //$("#TB_GZMC").click(function () {
    //    SelectWX_TCCDKGZ("TB_GZMC", "HF_GZID", "zHF_GZID", true);

    //});

    jQuery("#list").jqGrid({
        datatype: "json",
        colNames: ['HYKTYPE', '卡类型名称', '免费时限', ],
        colModel: [
          { name: 'iHYKTYPE', hidden: true },
          { name: 'sHYKNAME', width: 120 },
            { name: 'fMFSX_KLX', editable: true, width: 100, },
        ],
        cellEdit: true,
        caption: "停车场抵扣规则卡类型明细",
        rowNum: 10,//用于设置Grid中一次显示的行数，默认值为20。正是这个选项将参数rows（prmNames中设置的）通过url选项设置的链接传递到Server。注意如果Server返回的数据行数超过了rowNum的设定，则Grid也只显示rowNum设定的行数
        pager: '#pager',
        viewrecords: true,
        height: 100,
        width: 400,
        rownumbers: true,
        multiselect: true,
        cellsubmit: "clientArray",

    });
    jQuery("#list2").jqGrid({
        datatype: "json",
        colNames: ['序号', '消费金额', '免费时限'],
        colModel: [
          { name: 'iXH', width: 60, },
          { name: 'fXFJE', editable: true, width: 100, },// editable: true, index: 'iXH', formatter: 'select', edittype: "select", editoptions: { value: "0:鼓励奖;1:一等奖;2:二等奖;3:三等奖;4:四等奖" },
          { name: 'fMFSX_XF', editable: true, width: 100, },

        ],
        cellEdit: true,
        caption: "停车场抵扣规则消费明细",
        rowNum: 10,//用于设置Grid中一次显示的行数，默认值为20。正是这个选项将参数rows（prmNames中设置的）通过url选项设置的链接传递到Server。注意如果Server返回的数据行数超过了rowNum的设定，则Grid也只显示rowNum设定的行数
        pager: '#pager2',
        viewrecords: false,
        height: 100,
        width: 400,
        rownumbers: true,
        multiselect: true,
        cellsubmit: "clientArray",
        //onCellSelect: function (rowid, iCol, cellcontent, c) {
        //    //记录行号，列号，唯一标示 单元格。  
        //    if (document.getElementById("B_Save").disabled) {
        //        return;
        //    }
        //    irow = rowid;
        //    icol = iCol;
        //},
        //onSelectRow: function (rowid) {
        //    if ($.inArray(rowid, $("#list2").getGridParam("selarrrow")) == -1) {
        //        var rowData = $("#list2").jqGrid('getRowData', rowid);
        //        var ids2 = $("#list3").getDataIDs();
        //        for (var j = 0; j < ids2.length; j++) {
        //            var rowData2 = $("#list3").getRowData(j);
        //            if (rowData.iXH != rowData2.iXH) {
        //                $("#list3").setRowData(ids2[j], null, { display: 'none' })

        //            }
        //            if (rowData.iXH == rowData2.iXH) {
        //                $("#list3").setRowData(ids2[j], null, { display: 'block' })

        //            }
        //        }
        //    }
        //    else {
        //        var rowData = $("#list2").jqGrid('getRowData', rowid);
        //        var ids2 = $("#list3").getDataIDs();
        //        for (j = 0; j < ids2.length; j++) {
        //            var rowData2 = $("#list3").getRowData(j);
        //            if (rowData.iXH != rowData2.iXH) {
        //                $("#list3").setRowData(ids2[j], null, { display: 'none' })


        //            }

        //            if (rowData.iXH == rowData2.iXH) {
        //                $("#list3").setRowData(ids2[j], null, { display: 'block' })

        //            }
        //        }
        //    }
        //}

    });

    $("#AddKLX").click(function () {
        $.dialog.data('IpValuesChoiceOne', false);//控件单选与多选状态
        art.dialog.open("../../WUC/KLX/WUC_KLX.aspx? ", {
            lock: true, width: 470, height: 440, cancel: true,
            close: function () {
                //当关闭窗口时，接收返回的数据
                var returnData = $.dialog.data('IpValuesReturn');//接收的应该是转换成对象，或者数组
                var dataArray = new Array();

                //有数据返回
                if (returnData != null && returnData.length > 0) {

                    dataArray = JSON.parse(returnData).Articles;//返回的数据需要符合JSON字符串格式，才能进行转换成数组，或者对象                 
                }

                //将添加到表中
                for (var i = 0; i <= dataArray.length - 1; i++) {
                    var ids = $("#list").getDataIDs();
                    var lastid = ids[ids.length - 1];
                    if (ids.length == 0) {
                        var lastid1 = 0;
                    }
                    else {
                        lastid1 = parseInt(lastid) + 1;
                    }
                    var rowidarr = $("#list").getDataIDs();
                    for (var j = 0; j < rowidarr.length  ; j++) {
                        var rowData = $("#list").jqGrid("getRowData", rowidarr[j]);
                        if (rowData.iHYKTYPE == dataArray[i].Id) {
                            ShowMessage("重复卡类型，请从新选择");
                            return;
                        }
                    }
                    if (dataArray[i].Id != "") {
                        $("#list").addRowData(lastid1, {
                            iHYKTYPE: dataArray[i].Id,
                            sHYKNAME: dataArray[i].Name,
                        });
                    }
                }
                $.dialog.data('IpValuesReturn', "");//清空数据
            }
        })
    });

    $("#DelKLX").click(function () {
        var gr = $("#list").jqGrid("getGridParam", "selarrrow");//获取多个选中行的id.　1、获取单个id //获取行号，有这种方式,var rowid = $("#grid-table").jqGrid("getGridParam", "selrow");
        if (gr.length == 0) {
            art.dialog({ title: '没有选中', content: '选择要删除的行!' });
        }
        else {
            for (i = 0; i < gr.length;) {
                $("#list").jqGrid("delRowData", gr[i]);
            }
        }
    });
    //$("#AddKLXXH").click(function () {
    //    var ids2 = jQuery('#list').jqGrid('getGridParam', 'selarrrow');//获得选中行数组（行号）
    //    if (ids2.length <= 0) {
    //        ShowMessage("还未选中“停车场抵扣规则卡类型明细”表的数据");
    //        return;
    //    }

    //    for (var i = 0; i < ids2.length; i++) {
    //        var ids3 = $("#list4").getDataIDs();
    //        var lastid3 = ids3[ids3.length - 1];
    //        if (ids3.length == 0) {
    //            var lastid4 = 0;
    //        }
    //        else {
    //            lastid4 = parseInt(lastid3) + 1;
    //        }
    //        var rowData = $("#list").getRowData(ids2[i]);


    //        var rowidarr3 = $("#list4").getDataIDs();
    //        var flag = true;
    //        if (rowidarr3.length != 0) {
    //            for (var j = 0; j < rowidarr3.length  ; j++) {
    //                var rowData3 = $("#list4").jqGrid("getRowData", rowidarr3[j]);
    //                //var XH = $("#TB_XH").val();
    //                //if (rowData3.iXH == XH && rowData3.iHYKTYPE == rowData.iHYKTYPE) {
    //                //    ShowMessage("重复,请重新选择卡类型 或重新选择序号");
    //                //    flag = false;
    //                //}
    //            }
    //        }
    //        if (flag == true) {
    //            $("#list4").addRowData(lastid4,
    //            {
    //              "iXH": lastid4 + 1,
    //              "iHYKTYPE": rowData.iHYKTYPE,
    //              "sHYKNAME": rowData.sHYKNAME,
    //            });
    //        }

    //    }

    //    var listcount = $("#list4").getGridParam("reccount");
    //    $.dialog.data('IpValuesReturn', "");//清空数据
    //});

    //$("#DelKLXXH").click(function () {
    //    var gr2 = $("#list4").jqGrid("getGridParam", "selarrrow");//获取多个选中行的id.　1、获取单个id //获取行号，有这种方式,var rowid = $("#grid-table").jqGrid("getGridParam", "selrow");
    //    if (gr2.length == 0) {
    //        art.dialog({ title: '没有选中', content: '选择要删除的行!' });
    //    }
    //    else {
    //        for (i = 0; i < gr2.length;) {
    //            $("#list4").jqGrid("delRowData", gr2[i]);
    //        }
    //    }
    //});


    $("#AddItem").click(function () {

        var ids = $("#list2").getDataIDs();
        var lastid = ids[ids.length - 1];
        if (ids.length == 0) {
            var lastid1 = 0;
        }
        else {
            lastid1 = parseInt(lastid) + 1;
        }
        $("#list2").addRowData(lastid1,
       {
           iXH: lastid1 + 1,
       });
        var listcount = $("#list2").getGridParam("reccount");
        $.dialog.data('IpValuesReturn', "");//清空数据
    });

    $("#DelItem").click(function () {
        var gr2 = $("#list2").jqGrid("getGridParam", "selarrrow");//获取多个选中行的id.　1、获取单个id //获取行号，有这种方式,var rowid = $("#grid-table").jqGrid("getGridParam", "selrow");
        if (gr2.length == 0) {
            art.dialog({ title: '没有选中', content: '选择要删除的行!' });
        }
        else {
            for (i = 0; i < gr2.length;) {
                $("#list2").jqGrid("delRowData", gr2[i]);
            }
        }
    });


    //$("#Add").click(function () {

    //    if ($("#TB_BMMC").val() == "") {
    //        art.dialog({ lock: true, content: "请选择商户部门" });
    //        return false;
    //    }
    //    var rowidarr = $("#list2").getDataIDs();
    //    for (var i = 0; i < rowidarr.length  ; i++) {
    //        var rowData = $("#list2").getRowData(rowidarr[i]);
    //        if (rowData.iXH <= 0) {
    //            ShowMessage("请先设置-停车场抵扣规则消费明细-的数据");
    //            return;
    //        }
    //        var checkid = Number(Number(i) + Number(1));
    //        if ($("#" + checkid + "_fXFJE").length != 0) {
    //            ShowMessage("<font color='red'>请先按回车保存数据</font>");
    //            return;
    //        }
    //    }
    //    var ids2 = jQuery('#list2').jqGrid('getGridParam', 'selarrrow');//获得选中行数组（行号）
    //    if (ids2.length <= 0) {
    //        ShowMessage("还未选中“停车场抵扣规则消费明细”的数据");
    //        return;
    //    }
    //    for (var i = 0; i < ids2.length; i++) {
    //        var ids3 = $("#list3").getDataIDs();
    //        var lastid3 = ids3[ids3.length - 1];
    //        if (ids3.length == 0) {
    //            var lastid4 = 0;
    //        }
    //        else {
    //            lastid4 = parseInt(lastid3) + 1;
    //        }
    //        var rowData = $("#list2").getRowData(ids2[i]);


    //        var rowidarr3 = $("#list3").getDataIDs();
    //        var tp_flag = true;
    //        if (rowidarr3.length != 0) {
    //            for (var j = 0; j < rowidarr3.length  ; j++) {
    //                var rowData3 = $("#list3").jqGrid("getRowData", rowidarr3[j]);
    //                var BMDM = $("#HF_BMDM").val();
    //                if (rowData3.sBMDM == BMDM && rowData3.iXH == rowData.iXH) {
    //                    ShowMessage("重复部门,请重新选择部门 或重新选择序号");
    //                    tp_flag = false;
    //                }
    //            }
    //        }
    //        if (tp_flag == true) {
    //            $("#list3").addRowData(lastid4,

    //            {
    //                "iXH": rowData.iXH,
    //                "iSHBMID": $("#HF_SHBMID").val(),
    //                "sBMMC": $("#TB_BMMC").val(),
    //              "sBMDM": $("#HF_BMDM").val(),
    //              "sSHDM": GetSelectValue("S_SH"),
    //            });
    //        }
    //    }
    //});

    //$("#Del").click(function () {
    //    var gr2 = $("#list3").jqGrid("getGridParam", "selarrrow");//获取多个选中行的id.　1、获取单个id //获取行号，有这种方式,var rowid = $("#grid-table").jqGrid("getGridParam", "selrow");
    //    if (gr2.length == 0) {
    //        art.dialog({ title: '没有选中', content: '选择要删除的行!' });
    //    }
    //    else {

    //        for (i = 0; i < gr2.length;) {
    //            $("#list3").jqGrid("delRowData", gr2[i]);
    //        }

    //    }


    //});
})
function SetControlState() {
    if ($("#LB_ZZRMC").text() != "") {
        document.getElementById("B_Stop").disabled = true;
    }
}


function IsValidData() {

    //if ($("#TB_GZMC").val() == "") {
    //    art.dialog({ content: "请选择规则", lock: true, time: 2 });
    //    return false;
    //}
    if ($("#DDL_WX_MDID").val() == "") {
        art.dialog({ content: "请选择门店", lock: true, time: 2 });
        return false;
    }

    if ($("#TB_KSRQ").val() == "") {
        art.dialog({ lock: true, time: 2, content: "请选择开始日期" });
        return false;
    }
    if ($("#TB_JSRQ").val() == "") {
        art.dialog({ lock: true, time: 2, content: "请选择结束日期" });
        return false;
    }

    if ($("#TB_KSRQ").val() > $("#TB_JSRQ").val()) {
        art.dialog({ lock: true, time: 2, content: "开始日期不得大于结束日期" });
        return false;
    }

    var rowidarr = $("#list").getDataIDs();
    if (rowidarr.length <= 0) {
        ShowMessage("请先添加-停车场抵扣规则卡类型明细-数据");
        return false;
    }
    else {
        for (var i = 0; i < rowidarr.length  ; i++) {
            var rowData = $("#list").jqGrid("getRowData", rowidarr[i]);
            var checkid = Number(Number(i) + Number(1));
            if ($("#" + checkid + "_fMFSX_KLX").length != 0) {
                ShowMessage("请先按回车保存数据");
                return false;
            }

            else {
                var rowData = $("#list").getRowData(rowidarr[i]);
                if (rowData.fMFSX_KLX != "") {
                    if (rowData.iFFSL < 0) {
                        ShowMessage("免费时限不能为负");
                        return false;
                    }
                    if (rowData.fMFSX_KLX == 0) {
                        ShowMessage("免费时限不能为零");
                        return false;
                    }
                }

                if (rowData.fMFSX_KLX == "") {

                    ShowMessage("免费时限（）不能为空");
                    return false;


                }
            }
        }
    }

    var rowidarr = $("#list2").getDataIDs();
    if (rowidarr.length <= 0) {
        ShowMessage("请先添加-停车场抵扣规则消费明细-数据");
        return false;
    }
    else {
        for (i = 0; i < rowidarr.length  ; i++) {
            var rowData = $("#list2").jqGrid("getRowData", rowidarr[i]);
            var checkid = Number(Number(i) + Number(1));
            if ($("#" + checkid + "_fXFJE").length != 0 || $("#" + checkid + "_fMFSX_XF").length != 0) {
                ShowMessage("请先按回车保存数据");
                return false;
            }
            else {
                var rowData = $("#list2").getRowData(rowidarr[i]);
                if (rowData.fXFJE != "") {
                    if (rowData.fXFJE < 0) {
                        ShowMessage("消费金额下线不能为负");
                        return false;
                    }
                }
                if (rowData.fXFJE == "") {

                    ShowMessage("消费金额不能为空");
                    return false;

                }

                if (rowData.fMFSX_XF == "") {

                    ShowMessage("免费时限(消费)不能为空");
                    return false;


                }

            }
        }
    }
    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.iGZID = $("#HF_GZID").val();
    Obj.dKSRQ = $("#TB_KSRQ").val();
    Obj.dJSRQ = $("#TB_JSRQ").val();
    Obj.iWX_MDID = GetSelectValue("DDL_WX_MDID");
    Obj.sBZ = $("#TB_BZ").val();
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    var lst = new Array();
    var ids = $("#list").getDataIDs();//获得所有行的ID数组 var ids = $("jqgridtableid").jqGrid('getDataIDs');
    for (var i = 0; i < ids.length; i++) {
        var rowData = $("#list").jqGrid("getRowData")[i];
        lst.push(rowData);
    }
    Obj.itemTable = lst;

    var lst2 = new Array();
    var ids = $("#list2").getDataIDs();//获得所有行的ID数组 var ids = $("jqgridtableid").jqGrid('getDataIDs');
    for (var i = 0; i < ids.length; i++) {
        var rowData = $("#list2").jqGrid("getRowData")[i];
        lst2.push(rowData);
    }
    Obj.itemTable3 = lst2;
    return Obj;
}
function StartClick() {
    art.dialog({
        title: "启动",
        lock: true,
        content: "启动本单执行将会终止正在启动的单据，是否覆盖继续？",
        ok: function () {
            if (posttosever(SaveDataBase(), vUrl, "Start") == true) {
                vProcStatus = cPS_BROWSE;
                ShowDataBase(vJLBH);
                SetControlBaseState();
            }
        },
        okVal: '是',
        cancelVal: '否',
        cancel: true
    });
};
function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#HF_GZID").val(Obj.iGZID);
    //$("#TB_GZMC").val(Obj.sGZMC);
    $("#TB_KSRQ").val(Obj.dKSRQ);
    $("#TB_JSRQ").val(Obj.dJSRQ);
    $("#DDL_WX_MDID").val(Obj.iWX_MDID);

    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);
    $("#LB_QDRMC").text(Obj.sQDRMC);
    $("#HF_QDR").val(Obj.iQDR);
    $("#LB_QDSJ").text(Obj.dQDSJ);
    $("#LB_ZZRMC").text(Obj.sZZRMC);
    $("#HF_ZZR").val(Obj.iZZR);
    $("#LB_ZZRQ").text(Obj.dZZSJ);
    $("#TB_BZ").val(Obj.sBZ);
    $("#list").jqGrid("clearGridData");
    $("#list2").jqGrid("clearGridData");

    for (var i = 0; i < Obj.itemTable.length; i++) {
        $("#list").addRowData(i, Obj.itemTable[i]);
    }
    for (var i = 0; i < Obj.itemTable3.length; i++) {
        $("#list2").addRowData(i, Obj.itemTable3[i]);
    }

}
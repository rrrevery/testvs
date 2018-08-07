var canvas = new Array();//标签
var vColumnNames = [];
var vColumnModel = [];
var vColumns = [];
function BindKey() {

}

function InitGrid() {
    vColumnNames = ["卡号", "姓名", "卡类型", "证件号", "手机号", "主卡标记"];
    vColumnModel = [
               { name: 'sHYK_NO', width: 80 },
               { name: 'sHY_NAME', width: 80 },
               { name: 'sHYKNAME', width: 80 },
               { name: 'sSFZBH', width: 150 },
               { name: 'sSJHM', width: 150 },
               { name: 'iBJ_CHILD', width: 80, formatter: ChildCellFormat, },
    ];
}

function DrawGrid(listName, vColName, vColModel, vSingle, vHeight) {
    //为简化查询模板开发流程，统一Grid格式，新的查询可以使用InitGrid函数初始化vColumnNames和vColumnModel
    InitGrid();
    if (vHeight == undefined) { vHeight = 300; }
    if (vSingle == undefined) { vSingle = false; }
    if (listName == undefined) { listName = "list"; tabName = "#tb"; }
    if (listName != "list") { tabName = "#" + listName + "_tb" }
    if (vColName == undefined) { vColName = vColumnNames; }
    if (vColModel == undefined) { vColModel = vColumnModel; }
    if (vColumns.length == 0 || vColName != vColumnNames) {
        vColumns = InitColumns(undefined, vColModel, vColName);
        vAllColumns = vColumns;
    }
    $("#" + listName + "").datagrid({
        width: '100%',
        height: vHeight,
        autoRowHeight: false,
        singleSelect: vSingle,
        striped: true,
        columns: [vColumns],
        sortName: vColumns[0].field,
        sortOrder: 'desc',
        fitColumns: true,
        showHeader: true,
        showFooter: false,
        pagePosition: 'bottom',
        rownumbers: true, //添加一列显示行号
        // pagination: true,  //启用分页
        pageNumber: 1,
        pageSize: 1000,
        //toolbar: '' + tabName + '', //+ tabName,
        //onClickCell: onClickCell,
        //onClickRow: OnClickRow
    });
}

$(document).ready(function () {
    AddToolButtons("刷卡", "B_SK");
    AddToolButtons("查卡", "B_CK");
    AddToolButtons("刷新", "B_SX");
    $("#zMP5").hide();
    $("#zMP5_Hidden").hide();

    $(".cztright a").click(clickfunction);
    //$("#B_Insert").hide();
    //$("#B_Exec").hide();
    //$("#B_Update").hide();
    //$("#B_Delete").hide();
    //$("#B_Save").hide();
    //$("#B_Cancel").hide();
    //$("#status-bar").hide();
    //DrawGrid();
    RefreshButtonSep();
    var HYID = "";
    $("#B_SK").click(function () {
        var conData = new Object();
        conData.iBJ_KCK = 0;
        SelectSK("TB_HYK_NO", "HF_HYID", "", conData)

        //$.dialog.open("../../WUC/SK/WUC_SK.aspx?czk=0", {
        //    lock: true, width: 600, height: 200,
        //    close: function () {
        //        if ($.dialog.data("passValue")) {
        //            $("#TB_HYK_NO").val($.dialog.data("passValue"));
        //            pHYKNO = $.dialog.data("passValue");
        //            $.dialog.data("passValue", "");
        //            $.dialog.data("skHYID", "");
        //            //根据卡号查询一些必要的信息
        //            ShowData(pHYKNO);
        //        }
        //        else {
        //            pHYKNO = "";
        //        }
        //    }
        //}, false);
    });
    //$("#B_CK").click(function () {
    //});
    $("#B_CK").click(function () {
        $.dialog.data("iDJR", iDJR);
        var dialogUrl = "../../CrmArt/ListCZTHYK/CrmArt_ListCZTHYK.aspx?czk=0";
        $.dialog.open(dialogUrl, {
            lock: true, width: 1000, height: 350, cancel: false,
            close: function () {
                var bSelected = $.dialog.data('dialogSelected');
                if (bSelected) {
                    var lst = JSON.parse($.dialog.data("ListCZTHYK"));
                    if (lst.length == 0)
                    { return; }
                    $("#TB_HYK_NO").val(lst[0].sHYK_NO);
                    pHYKNO = lst[0].sHYK_NO;
                    $.dialog.data("ListCZTHYK", "");
                    ShowData(pHYKNO);
                }
                else {
                    pHYKNO = "";
                }
            }
        }, false);
    });
    $("#B_SX").click(function () {
        if (pHYKNO == "") {
            art.dialog({ content: "请输入卡号", time: 2 });
            return;
        }
        ShowData(pHYKNO);
    });


    $("#TB_HYK_NO").keydown(function (event) {
        event = (event) ? event : ((window.event) ? window.event : ""); //兼容IE和Firefox获得keyBoardEvent对象
        var key = event.keyCode ? event.keyCode : event.which; //兼容IE和Firefox获得keyBoardEvent对象的键值 
        if (key == 13) {
            //判断是否进行过换卡，若经过换卡显示新卡号
            var resultData = GetHKdata($(this).val());
            if (resultData) { //不为空，有换过卡
                $("#TB_HYK_NO").val("");
                resultData = JSON.parse(resultData);
                var pHYKHM_NEW = resultData.sHYKHM_NEW;
                //art.dialog({ content: "此卡已换卡，新卡号为" + pHYKHM_NEW, lock: true });
                alert("此卡已换卡，新卡号为" + pHYKHM_NEW);
                return false;
            }
            else {
                ShowData($(this).val());
            }

            //if (resultData !="") {
            //    ShowData($(this).val());
            //}
        }
    });
    $("#TB_SJHM").keydown(function (event) {
        event = (event) ? event : ((window.event) ? window.event : ""); //兼容IE和Firefox获得keyBoardEvent对象
        var key = event.keyCode ? event.keyCode : event.which; //兼容IE和Firefox获得keyBoardEvent对象的键值 
        if (key == 13) {
            ShowData(undefined, $(this).val());
        }
    });

});
function ShowData(hykno, sjhm) {
    $("#MainPanel label").text("");
    var data = GetHYXXData(0, hykno, undefined, undefined, undefined, sjhm);
    if (data) {
        data = JSON.parse(data);
        pHYKNO = data.sHYK_NO;
        pHYKTYPE = data.iHYKTYPE;
        pHYID = data.iHYID;
        $("#TB_HYK_NO").val(data.sHYK_NO);
        $("#LB_HYKTYPE").text(data.sHYKNAME);
        $("#LB_YXQ").text(data.dYXQ);
        $("#LB_JKRQ").text(data.dJKRQ);
        //var sSTATUS = "";
        //switch (data.iSTATUS - 0) {
        //    case -8:
        //    case -7:
        //    case -6:
        //    case -5:
        //    case -4:
        //    case -3:
        //    case -2:
        //    case -1:
        //        sSTATUS = "无效卡"
        //        break;
        //    case 0:
        //    case 1:
        //    case 2:
        //    case 3:
        //    case 4:
        //        sSTATUS = "有效卡";
        //        break;
        //    case 5:
        //        sSTATUS = "休眠卡";
        //        break;
        //    case 6:
        //        sSTATUS = "呆滞卡";
        //        break;
        //    case 7:
        //        sSTATUS = "升级卡";
        //        break;
        //}
        $("#sendMail").prop("href", "mailto:" + data.sEMAIL + "")
        $("#LB_EMAIL").text(data.sEMAIL);
        $("#LB_STATUS").text(data.sStatusName);

        $("#LB_FKMDMC").text(data.sMDMC); //发行门店
        $("#LB_MDMC").text(data.sMDMC);
        $("#LB_FXDW").text(data.sFXDWMC);
        $("#LB_KFRYNAME").text(data.KHJLMC);
        $("#LB_HY_NAME").text(data.sHY_NAME);
        $("#LB_ZJHM").text(data.sSFZBH);
        $("#LB_CSRQ").text(data.dCSRQ);
        $("#LB_XB").text(data.iSEX == 0 ? "男" : "女");
        //$("#LB_SJHM").text(data.sSJHM);
        $("#TB_SJHM").val(data.sSJHM);
        $("#LB_TEL").text(data.sPHONE);
        $("#LB_JTZZ").text(data.sTXDZ);
        $("#LB_YZBM").text(data.sYZBM);

        $("#LB_BZ").text(data.sBZ);

        $("#LB_WCLJF").text(data.fWCLJF);
        $("#LB_SJJF").text(data.fBQJF);

        $("#LB_YHQJE").text(data.fYHQJE);
        $("#LB_ZZJSRQ").text(data.dYHQJSRQ);

        $("#LB_QCYE").text(data.fQCYE);
        $("#LB_YE").text(data.fCZJE);

        $("#LB_PDJE").text(data.fPDJE);
        $("#LB_DJJE").text(data.fJYDJJE);
        //$('#list').datagrid('loadData', data.SubCardTable, "json");
        //$('#list').datagrid("loaded");
        GetHYBQ(hykno);
        if (data.iSTATUS < 0) {
            GetStatusControl("czt_gs", false);
            GetStatusControl("czt_zf", false);
            GetStatusControl("czt_jffl", false);
            GetStatusControl("czt_jfdhlp", false);
            GetStatusControl("czt_jfbd", false);
            GetStatusControl("czt_jftz", false);
            GetStatusControl("czt_yhqck", false);
            GetStatusControl("czt_yhqqk", false);
            GetStatusControl("czt_yhqzhzc", false);
            GetStatusControl("czt_jeck", false);
            GetStatusControl("czt_jeqk", false);
            GetStatusControl("czt_hyksj", false);
            GetStatusControl("czt_hk", false);
            GetStatusControl("czt_yxqgg", false);
            GetStatusControl("czt_ghklx", false);
            GetStatusControl("czt_dbq", false);
            GetStatusControl("czt_gshf", false);
        }
        else {
            GetStatusControl("czt_gs", true);
            GetStatusControl("czt_zf", true);
            GetStatusControl("czt_jffl", true);
            GetStatusControl("czt_jfdhlp", true);
            GetStatusControl("czt_jfbd", true);
            GetStatusControl("czt_jftz", true);
            GetStatusControl("czt_yhqck", true);
            GetStatusControl("czt_yhqqk", true);
            GetStatusControl("czt_yhqzhzc", true);
            GetStatusControl("czt_jeck", true);
            GetStatusControl("czt_jeqk", true);
            GetStatusControl("czt_hyksj", true);
            GetStatusControl("czt_hk", true);
            GetStatusControl("czt_yxqgg", true);
            GetStatusControl("czt_ghklx", true);
            GetStatusControl("czt_dbq", true);
            GetStatusControl("czt_gshf", true);
            GetStatusControl("czt_hykbc", true);

        }
        if (data.iSTATUS == -3) {
            GetStatusControl("czt_gshf", true);
        }
        else {
            GetStatusControl("czt_gshf", false);
        }


    }
    else {
        pHYKNO = "";
        HYID = "";
        art.dialog({ content: "不存在此卡", lock: true, time: 2 });
    }
}

function GetHYBQ(hykno) {
    var data = GetHYBQData(hykno);
    if (data) {
        data = JSON.parse(data);
        canvas = zDataStringToArray(data.sCANVAS);
        showSign(canvas);
    }

}

function zDataStringToArray(varray) {
    if (varray == "") { return new Array(); }
    return tp_str1 = varray.split(";");
    if (varray.length > 0) {
        for (var i = 0; i <= tp_str1.length - 1; i++) {
            tp_target.push(varray[i]);
        }
    }
    return tp_target;
}

function showSign(array) {
    //if (array == []) { return;}
    var htmlstr = "";
    for (i = 0; i < array.length - 1; i++) {
        htmlstr += "<input type='button' class='form_button' value=" + array[i] + "  />";
    }
    $("#myCanvas").html(htmlstr);
}

function MoseDialogCustomerReturn(dialogName, lst, showField) {
    if (dialogName == "DialogSK") {
        pHYKNO = $("#TB_HYK_NO").val();
        ShowData(pHYKNO);
    }
}

function clickfunction() {
    //event.preventDefault();
    if (!pHYKNO) {
        return;
    }
    var tp_filename = "";
    var title = $(this)[0].text;//.substr(2, 99);
    var tabid = $(this).attr("menuid");
    switch ($(this).attr("id")) {
        case "czt_bsk":
            alert("此功能暂无实现!");
            break;
        case "czt_hykzr":
            tp_filename = "CrmWeb/HYKGL/HYKZR/HYKGL_HYKZR.aspx?HYKNO=" + pHYKNO;
            title = "会员卡转让";
            break;
        case "czt_tk":
            tp_filename = "CrmWeb/HYKGL/HYKTK/HYKGL_HYKTK.aspx?HYKNO=" + pHYKNO;
            title = "会员卡退卡";
            break;
        case "czt_hykzstk":
            tp_filename = "CrmWeb/HYKGL/HYKHK/HYKGL_HYKHK.aspx?mzk=0&djlx=1&HYKNO=" + pHYKNO;
            title = "电子卡转实体卡";
            break;
        case "czt_hk":
            tp_filename = "CrmWeb/HYKGL/HYKHK/HYKGL_HYKHK.aspx?HYKNO=" + pHYKNO;
            title = "会员卡补卡";
            break;
        case "czt_ghklx":
            tp_filename = "CrmWeb/HYKGL/GHKLX/HYKGL_GHKLX.aspx?HYKNO=" + pHYKNO;
            break;
        case "更换发行单位":
            alert("此功能暂无实现!");
            break;
        case "czt_daxx":
            tp_filename = "CrmWeb/HYKGL/HYDALR/HYKGL_HYDALR.aspx?action=add&HYK_NO=" + pHYKNO;//"&SFZBH=" + $("#LB_ZJHM").text() + "&SJHM=" + $("#LB_SJHM").text();
            title = "会员档案";
            break;
        case "积分消费明细":
            alert("此功能暂无实现!");
            //tp_filename = "cJFXFMX/HYKCZT_cJFXFMX.aspx?pid=" + op;
            //title = "积分消费明细";
            break;
        case "管辖商户积分":
            alert("此功能暂无实现!");
            //tp_filename = "CrmWeb/HYKGL/HYGXSHJFXF/HYKGL_HYGXSHJFXF_Srch.aspx?HYKNO=" + pHYKNO;
            //title = "管辖商户积分";
            break;
        case "优惠券账户明细":
            alert("此功能暂无实现!");
            //tp_filename = "dYHQZHMX/HYKCZT_dYHQZHMX.aspx?pid=" + op;
            //title = "优惠券账户明细";
            break;
        case "金额帐处理明细":
            tp_filename = "eJEZCLMX/HYKCZT_eJEZCLMX.aspx?hyid=" + op;
            title = "金额账处理明细";
            break;
        case "收发券记录明细":
            alert("此功能暂无实现!");
            //tp_filename = "";
            //title = "收发券记录明细";
            break;
        case "卡积分变动明细":
            alert("此功能暂无实现!");
            //tp_filename = "tJFBDMX/HYKCZT_tJFBDMX.aspx?pid=" + op;
            //title = "卡积分变动明细";
            break;
        case "卡积分调整明细":
            alert("此功能暂无实现!");
            //tp_filename = "sJFTZMX/HYKCZT_sJFTZMX.aspx?pid=" + op;
            //title = "卡积分调整明细";
            break;
        case "会员卡制卡信息":
            tp_filename = "../HYKGL/ZKXX/HYKGL_ZKXX_Srch.aspx?HYKNO=" + pHYKNO;
            title = "会员卡制卡信息";
            break;
        case "消费补刷卡":
            alert("此功能暂无实现!");
            //tp_filename = "zHYKXFBSK/HYKCZT_zHYKXFBSK.aspx?pid=" + op;
            //title = "消费补刷卡";
            break;
        case "会员兑换情况":
            alert("此功能暂无实现!");
            break;
        case "积分兑换礼品明细":
            alert("此功能暂无实现!");
            break;
        case "czt_gs":
            tp_filename = "CrmWeb/HYKGL/HYKGS/HYKGL_HYKGS.aspx?hf=0&czk=0&HYKNO=" + pHYKNO;
            title = "会员卡挂失";
            break;
        case "czt_gshf":
            tp_filename = "CrmWeb/HYKGL/HYKGS/HYKGL_HYKGS.aspx?hf=1&czk=0&HYKNO=" + pHYKNO;
            title = "会员卡挂失恢复";
            break;
        case "czt_xfpcjf":
            alert("此功能暂无实现!");
            break;
        case "签到积分":
            alert("此功能暂无实现!");
            break;
        case "czt_hyksj":
            tp_filename = "CrmWeb/HYKGL/HYKSJ/HYKGL_HYKSJ.aspx?sj=1&HYKNO=" + pHYKNO;
            title = "会员卡升级";
            break;
        case "czt_hykzf":
            tp_filename = "CrmWeb/HYKGL/HYKZF/HYKGL_HYKZF.aspx?HYKNO=" + pHYKNO;
            title = "会员卡作废";
            break;
        case "czt_yxqgg":
            tp_filename = "CrmWeb/HYKGL/YXQYC/HYKGL_YXQYC.aspx?HYKNO=" + pHYKNO;
            break;
        case "czt_yhqzhzc":
            tp_filename = "CrmWeb/HYKGL/YHQZC/HYKGL_YHQZC.aspx?action=add&HYKNO=" + pHYKNO;
            break;
        case "czt_zdbd":
            tp_filename = "CrmWeb/HYKGL/ZTBD/HYKGL_ZTBD.aspx?czk=0&HYKNO=" + pHYKNO;
            break;
        case "czt_jftz":
            tp_filename = "CrmWeb/HYXF/JFTZD/HYXF_JFTZD.aspx?HYKNO=" + pHYKNO;
            break;
        case "czt_jfbd":
            tp_filename = "CrmWeb/HYXF/JFBDD/HYXF_JFBDD.aspx?HYKNO=" + pHYKNO;
            break;
        case "czt_jfzc":
            tp_filename = "CrmWeb/HYXF/JFZCD/HYXF_JFZCD.aspx?HYKNO=" + pHYKNO;
            break;
        case "czt_jeck":
            tp_filename = "CrmWeb/HYKGL/HYKCK/HYKGL_HYKCK.aspx?HYKNO=" + pHYKNO;
            break;
        case "czt_jeqk":
            tp_filename = "CrmWeb/HYKGL/HYKQK/HYKGL_HYKQK.aspx?HYKNO=" + pHYKNO;
            break;
        case "czt_yhqck":
            tp_filename = "CrmWeb/HYKGL/YHQCK/HYKGL_YHQCK.aspx?HYKNO=" + pHYKNO;
            break;
        case "czt_yhqqk":
            tp_filename = "CrmWeb/HYKGL/YHQQK/HYKGL_YHQQK.aspx?HYKNO=" + pHYKNO;
            break;
        case "czt_jfxfjx":
            tp_filename = "CrmWeb/HYKGL/JFXFMX/HYKGL_JFXFMX_Srch.aspx?HYKNO=" + pHYKNO;
            break;
        case "czt_gxshjf":
            tp_filename = "CrmWeb/HYKGL/HYGXSHJFXF/HYKGL_HYGXSHJFXF_Srch.aspx?HYKNO=" + pHYKNO + "&HYID=" + pHYID;
            break;
        case "czt_yhqzhmx":
            tp_filename = "CrmWeb/HYKGL/YHQZHMX/HYKGL_YHQZHMX_Srch.aspx?HYKNO=" + pHYKNO;
            break;
        case "czt_jfbljl":
            tp_filename = "CrmWeb/HYKGL/JFBDJLMX/HYKGL_JFBDJLMX_Srch.aspx?HYKNO=" + pHYKNO;
            break;
        case "czt_hykzkxx":
            tp_filename = "CrmWeb/HYKGL/ZKXX/HYKGL_ZKXX_Srch.aspx?HYKNO=" + pHYKNO;
            break;
        case "czt_fwxs":
            tp_filename = "CrmWeb/HYKGL/FWXS/HYKGL_FWXS.aspx?action=add&HYKNO=" + pHYKNO + "&HYKTYPE=" + pHYKTYPE + "&HYID=" + pHYID;
            break;
        case "czt_hyfwcljl":
            tp_filename = "CrmWeb/HYKGL/FWCLCX/HYKGL_FWCLCX_Srch.aspx?HYID=" + pHYID + "&HYKNO=" + pHYKNO;
            break;
        case "czt_hysyfwcx":
            tp_filename = "CrmWeb/HYKGL/FWZHCX/HYKGL_FWZH_Srch.aspx?HYKNO=" + pHYKNO;
            break;
        case "czt_dbq":
            tp_filename = "CrmWeb/HYKGL/HYDBQ/HYKGL_HYDBQ.aspx?action=add&HYKNO=" + pHYKNO;
            break;
        case "czt_zyxx":
            tp_filename = "CrmWeb/HYKGL/GKZYDALR/HYKGL_GKZYDALR.aspx?action=add&HYK_NO=" + pHYKNO;//"&SFZBH=" + $("#LB_ZJHM").text() + "&SJHM=" + $("#LB_SJHM").text();
            break;
        case "czt_hydjlbcx":
            tp_filename = "CrmWeb/KFPT/DYHYFX/KFPT_DYHYFX.aspx?ShowOne=13&HYKNO=" + pHYKNO;//"&SFZBH=" + $("#LB_ZJHM").text() + "&SJHM=" + $("#LB_SJHM").text();
            title = "会员单据列表";
            break;
        case "czt_hykty":
            tp_filename = "CrmWeb/HYKGL/HYKTY/HYKGL_HYKTY.aspx?HYKNO=" + pHYKNO;//"&SFZBH=" + $("#LB_ZJHM").text() + "&SJHM=" + $("#LB_SJHM").text();
            title = "会员卡停用";
            break;
        case "czt_jffl":
            tp_filename = "CrmWeb/HYXF/HYJFCLZX/HYXF_HYJFCLZX.aspx?action=add&HYKNO=" + pHYKNO;
            title = "积分返利执行";
            break;
        case "czt_jfdhlp":
            tp_filename = "CrmWeb/LPGL/LPFF/LPGL_RCLPFF.aspx?action=add&HYKNO=" + pHYKNO;
            title = "积分兑换礼品";
            break;
        case "czt_hykbc":
            tp_filename = "CrmWeb/HYKGL/KCKBC/HYKGL_KCKBC.aspx?hyk=1&czk=0&action=add&HYKNO=" + pHYKNO;
            title = "会员卡补磁";
            break;
        default:
            alert("此功能暂无实现!");
            break;
    }
    if (tp_filename) {
        MakeNewTab(tp_filename, title, tabid);
    }
    event.preventDefault();
    console.log($(this).text());//生成不同的newTabe
}
function GetStatusControl(element, status) {
    if (status) {
        $("#" + element).css('color', '');
        $("#" + element).bind('click', clickfunction);
    }
    else {
        $("#" + element).css('color', '#999');
        $("#" + element).unbind('click', clickfunction);
    }


}
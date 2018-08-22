//vPageMsgID = CM_HYKGL_JFKPLFF;
vUrl = "../MZKGL.ashx";
var sksl = 0;
var vSKFSColumnNames;
var vSKFSColumnModel;
var vZKColumnNames;
var vZKColumnModel;
var vZJFColumnNames;
var vZJFColumnModel;
var vDBConnName = "CRMDBMZK";

function InitGrid()
{
    vColumnNames = ['iSKDBH', '卡号', 'iHYKTYPE', 'iHYID', '卡类型', '铺底金额', '面值金额', '有效期'];
    vColumnModel = [
            { name: 'iSKDBH', index: 'iSKDBH', hidden: true, },
            { name: 'sCZKHM', index: 'sCZKHM', width: 80, },
            { name: 'iHYKTYPE', index: 'iHYKTYPE', hidden: true, },
            { name: 'iHYID', index: 'iHYID', hidden: true, },
            { name: 'sHYKNAME', index: 'sHYKNAME', width: 50, },
            { name: 'fPDJE', index: 'fPDJE', width: 20, align: "right", },
            { name: 'fQCYE', index: 'fQCYE', width: 50, align: 'right', },
            { name: 'dYXQ', index: 'dYXQ', width: 50, align: 'right', hidden: true, },
    ];
    vSKFSColumnNames = ["支付方式编号", "支付方式代码", "支付方式名称", "退款金额", "交易号"];
    vSKFSColumnModel = [
          { name: "iZFFSID", width: 150 },
          { name: "sZFFSDM", hidden: true },
          { name: "sZFFSMC", width: 150 },
          { name: "fJE", width: 150, editable: true, editor: 'text' },
          { name: "sJYBH", width: 150, editable: true, editor: 'text',hidden: true,},

    ];
    vZKColumnNames = ['卡号', '优惠券', '优惠券ID', '有效期', '赠券金额'];
    vZKColumnModel = [
            { name: 'sHYK_NO', index: 'sHYK_NO', width: 100, },
            { name: 'sYHQMC', index: 'sYHQMC', width: 100, },
            { name: 'iYHQLX', index: 'iYHQLX', hidden: true, },
            { name: 'iYXQTS', index: 'iYXQTS', width: 50, },
            { name: 'fZKJE', index: 'fZKJE', width: 50, align: 'right', },
    ];
    vZJFColumnNames = ['卡号', '赠送积分'];
    vZJFColumnModel = [
            { name: 'sHYK_NO', index: 'sHYK_NO', width: 100, },
            { name: 'fZSJF', index: 'fZSJF', width: 50, },
    ];

};

$(document).ready(function ()
{
    $("#btnout_B_Start").show();
    $("#B_Start").show();
    DrawGrid("GV_SKFS", vSKFSColumnNames, vSKFSColumnModel, true);
    DrawGrid("GV_ZK", vZKColumnNames, vZKColumnModel, true);
    DrawGrid("GV_ZJF", vZJFColumnNames, vZJFColumnModel, true);
    FillBGDDTreeSK("TreeBGDD", "TB_BGDDMC");
    FillHYKTYPETree("TreeHYKTYPE", "TB_HYKNAME", 2);//会员卡建卡
    $("#B_ZK_Search").prop("disabled", true);
    $("#B_ZJF_Search").prop("disabled", true);

    $("#TB_KHMC").click(function ()
    {
        SelectKH("TB_KHMC", "HF_KHID", "zHF_KHID", false);
    })

    $("#B_ZK_Search").click(function ()
    {
        var listrow = $("#list").datagrid("getData").rows;
        if (listrow.length <= 0)
        {
            ShowMessage("请先添加面值卡", 3);
            return;
        }
        if ($("#TB_HYKNO_ZQ").val() != "")
        {
            var str = GetHYXXData(0, $("#TB_HYKNO_ZQ").val(), "CRMDB");
            if (str == "null" || str == "")
            {
                ShowMessage("没有找到卡号", 3);
                $('#GV_ZK').datagrid('loadData', { total: 0, rows: [] });
                return;
            }
            else
            {
                var Obj = JSON.parse(str);
                if (Obj.iSTATUS < 0)
                {
                    ShowMessage("卡号状态错误", 3);
                    $('#GV_ZK').datagrid('loadData', { total: 0, rows: [] });
                    return;
                }
                $('#GV_ZK').datagrid('loadData', { total: 0, rows: [] });
                $('#GV_ZK').datagrid('appendRow', {
                    sHYK_NO: Obj.sHYK_NO,
                    sYHQMC: $("#HF_ZKYHQMC").val(),
                    iYHQLX: $("#HF_ZKYHQLX").val(),
                    iYXQTS: $("#HF_ZKYHQTS").val(),
                    fZKJE: $("#LB_SK_ZK").text(),
                });
                $("#LB_ZK_YSJE").text($("#LB_SK_ZK").text());
            }


        }


    });


    $("#B_ZF_Search").click(function ()
    {
        var listrow = $("#list").datagrid("getData").rows;
        if (listrow.length <= 0)
        {
            ShowMessage("请先添加面值卡", 3);
            return;
        }
        if ($("#TB_HYKNO_ZF").val() != "")
        {
            var str = GetHYXXData(0, $("#TB_HYKNO_ZF").val(), "CRMDB");
            if (str == "null" || str == "")
            {
                ShowMessage("没有找到卡号", 3);
                $('#GV_ZJF').datagrid('loadData', { total: 0, rows: [] });
                return;
            }
            else
            {
                var Obj = JSON.parse(str);
                if (Obj.iSTATUS < 0)
                {
                    ShowMessage("卡号状态错误", 3);
                    $('#GV_ZJF').datagrid('loadData', { total: 0, rows: [] });
                    return;
                }
                $('#GV_ZJF').datagrid('loadData', { total: 0, rows: [] });
                $('#GV_ZJF').datagrid('appendRow', {
                    sHYK_NO: Obj.sHYK_NO,
                    fZSJF: $("#LB_SK_JF").text(),
                });
                $("#LB_ZSJF").text($("#LB_SK_JF").text());
            }


        }


    });

    $("#TB_SKJLBH").click(function ()
    {
        if ($("#HF_BGDDDM").val() == "")
        {
            ShowMessage("请选择退售地点", 3);
            return;
        }
        var DataArry = new Object();
        DataArry["iSTATUS"] = 2;
        DataArry["BJ_TS"] = 1;
        SelectMZKSKD('TB_SKJLBH', 'HF_SKJLBH', 'zHF_SKJLBH', false ,DataArry);
    });
    $("#B_ZK_Delete").click(function ()
    {
        $("#LB_ZK_YSJE").text(0);
        DeleteRows("GV_ZK");
    });

    $("#B_ZF_Delete").click(function ()
    {
        $("#LB_ZSJF").text(0);
        DeleteRows("GV_ZJF");
    });
    $("#AddItem").click(function ()
    {
        var DataArry = new Object();
        var tp_SKJLBH = $("#TB_SKJLBH").val();
        if (tp_SKJLBH == "")
        {
            ShowMessage("请选择售卡单",3);
            return;
        }
        else
            DataArry["iSKJLBH"] = parseInt(tp_SKJLBH);
        if ($("#TB_CZKHM_BEGIN").val() != "")
        {
            DataArry["sKSKH"] = $("#TB_CZKHM_BEGIN").val();
        }
        if ($("#TB_CZKHM_END").val() != "")
        {
            DataArry["sJSKH"] = $("#TB_CZKHM_END").val();
        }
        SelectMZKSKMX('list', DataArry, 'iHYID');

    });

})


function TreeNodeClickCustom(event, treeId, treeNode)
{
    switch (treeId)
    {
        case "TreeBGDD": $("#HF_BGDDDM").val(treeNode.sBGDDDM); break;
        case "TreeHYKTYPE": $("#HF_HYKTYPE").val(treeNode.iHYKTYPE); break;
    }
}
function CustomerOpenDialogReturn(rows, listName, lst, array, CheckFieldId)
{
    if (listName == "list")
    {
        if (rows.length == 0)
        {
            $('#' + listName).datagrid('loadData', lst, "json");
        }
        else
        {
            for (var i = 0; i < lst.length; i++)
            {
                if (CheckReapet(array, CheckFieldId, lst[i]))
                {//[CheckFieldId]
                    $('#' + listName).datagrid('appendRow', lst[i]);
                }
            }
        }
        AddSumData();
        if (parseFloat($("#LB_SK_ZK").text()) > 0)
        {
            $("#B_ZK_Search").prop("disabled", false);
        }
        if (parseFloat($("#LB_SK_JF").text()) > 0)
        {
            $("#B_ZJF_Search").prop("disabled", false);
        }
    }

}

function AddSumData()
{
    var rows = $('#list').datagrid('getRows');
    var total = 0;
    for (var i = 0; i < rows.length; i++)
    {
        total += parseFloat(rows[i]['fQCYE']); //获取指定列  
    }
    $("#LB_SK_YSJE").text(total);
    $("#LB_SK_YSZS").text(rows.length);
    if (rows.length > 0) { $("#TB_SKJLBH").prop("disabled", true) }
    else { $("#TB_SKJLBH").prop("disabled", false) }
}



function ShowData(data)
{
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);//input控件被换成了label...
    $("#LB_SK_YSJE").html(Obj.YSZE);
    $("#LB_SK_YSZS").html(Obj.SKSL);
    $("#LB_ZKL").html(Obj.ZKL);
    $("#LB_SK_ZK").html(Obj.ZKJE);
    $("#LB_ZK_YSJE").html(Obj.SJZSJE);
    $("#LB_ZK_YSZS").html(Obj.ZKSL);

    $("#HF_BGDDDM").val(Obj.BGDDDM);
    $("#TB_BGDDMC").val(Obj.BGDDMC);
    $("#TA_ZY").val(Obj.ZY);
    $("#TB_DZKFJE").val(Obj.DZKFJE);
    $("#TB_LXRMC").val(Obj.YWY);
    $("#HF_STATUS").val(Obj.STATUS);
    $("#LB_ZK_YSJE").html(Obj.SJZSJE);
    $("#LB_JF_YSJF").html(Obj.SJZSJF);

    $('#list').datagrid('loadData', Obj.SKMXITEM, "json");
    $('#list').datagrid("loaded");

    $('#GV_ZK').datagrid('loadData', Obj.ZQMX, "json");
    $('#GV_ZK').datagrid("loaded");

    $('#GV_SKFS').datagrid('loadData', Obj.ZFFS, "json");
    $('#GV_SKFS').datagrid("loaded");

    $('#GV_ZJF').datagrid('loadData', Obj.ZFMX, "json");
    $('#GV_ZJF').datagrid("loaded");

    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.ZXRMC);
    $("#HF_ZXR").val(Obj.ZXR);
    $("#LB_ZXRQ").text(Obj.ZXRQ);
    //
    $("#HF_KHID").val(Obj.KHID);
    $("#TB_KHMC").val(Obj.KHMC);
    $("#LB_LXRXM").text(Obj.LXRXM);
    $("#LB_LXRSJ").text(Obj.LXRSJ);
    $("#LB_SK_JF").text(Obj.ZSJF);
    $("#TB_SKJLBH").val(Obj.iTKJLBH);
    $("#HF_ZKYHQMC").text(Obj.ZKYHQMC);
    $("#HF_ZKYHQLX").text(Obj.ZKYHQLX);
    $("#HF_ZKYHQTS").text(Obj.ZKYHQTS);
    $("#HF_YWY").val(Obj.YWY);
}


function SaveData()
{
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "") { Obj.iJLBH = "0"; }
    Obj.FS = 2;
    Obj.SKSL = $("#LB_SK_YSZS").text();
    Obj.YSZE = $("#LB_SK_YSJE").text();
    Obj.ZKL = $("#LB_ZKL").text();
    Obj.ZKJE = $("#LB_SK_ZK").text();
    Obj.SSJE = "0";
    Obj.BGDDDM = $("#HF_BGDDDM").val();
    Obj.ZY = $("#TA_ZY").val();
    Obj.ZSJE = $("#LB_SK_ZK").html();
    Obj.KHID = ($("#HF_KHID").val() == "") ? "0" : $("#HF_KHID").val();
    Obj.LXR = "0";
    Obj.YWY = ($("#HF_YWY").val() == "") ? "0" : $("#HF_YWY").val();
    Obj.ZSJF = $("#LB_SK_JF").text();
    Obj.iTKJLBH = $("#TB_SKJLBH").val();

    var lst = new Array();
    var rowIDs = $("#list").datagrid('getRows');
    for (var i = 0; i < rowIDs.length; i++)
    {
        var rowData = rowIDs[i];
        lst.push(rowData);
    }

    Obj.SKMXITEM = lst;

    var lst_zk = new Array();
    var SJZQJE = 0;
    var rowIDs = $("#GV_ZK").datagrid('getRows');
    for (i = 0; i < rowIDs.length ; i++)
    {
        var rowData = rowIDs[i];
        SJZQJE = parseFloat(SJZQJE) + parseFloat(rowData.fZKJE);
        lst_zk.push(rowData);
    }
    Obj.ZQMX = lst_zk;
    Obj.SJZSJE = SJZQJE;
    var lst_skfs = new Array();
    var Rows = $("#GV_SKFS").datagrid("getData").rows;
    for (var i = 0; i < Rows.length; i++)
    {
        var rowData = Rows[i]
        if (parseFloat(rowData.fJE) > 0)
        {
            lst_skfs.push(rowData);
        }
    }
    Obj.ZFFS = lst_skfs;

    var lst_jf = new Array();
    var SJZSJF = 0;
    var rowJFIDs = $("#GV_ZJF").datagrid('getRows');
    for (var i = 0; i < rowJFIDs.length; i++)
    {
        var rowData = rowJFIDs[i];
        SJZSJF = parseFloat(SJZSJF) + parseFloat(rowData.fZSJF);
        lst_jf.push(rowData);
    }
    Obj.ZFMX = lst_jf;
    Obj.SJZSJF = SJZSJF;
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    Obj.sDBConnName = "CRMDBMZK";
    //
    return Obj;
}

function InsertClickCustom()
{
    window.setTimeout(function ()
    {
        $('#GV_ZK').datagrid('loadData', { total: 0, rows: [] });
        $('#GV_SKFS').datagrid('loadData', { total: 0, rows: [] });
        $('#GV_ZJF').datagrid('loadData', { total: 0, rows: [] });
        SearchData();
    }, 100);
};
function MoseDialogCustomerReturn(dialogName, lstData, showField)
{
    if (dialogName == "ListKH")
    {
        $("#LB_LXRXM").text(lstData[0].sLXRXM);
        $("#LB_LXRSJ").text(lstData[0].sLXRSJ);

    }
    if (dialogName == "ListMZKSKD")
    {
        for (var i = 0; i < lstData.length; i++)
        {
            $("#TB_SKJLBH").val(lstData[i].iJLBH);
            $("#HF_ZKYHQMC").val(lstData[i].sYHQMC);
            $("#HF_ZKYHQLX").val(lstData[i].iYHQID);
            $("#HF_ZKYHQTS").val(lstData[i].iYXQTS);
            $("#LB_SK_ZK").text(lstData[i].fSJZSJE);
            $("#LB_SK_JF").text(lstData[i].fSJZSJF);
            $("#HF_SKJE").val(lstData[i].cSSJE);
        }
        var tmp_data = SearchBackCardData($("#TB_SKJLBH").val(), "CRMDBMZK");
        var obj = JSON.parse(tmp_data);        
        if (obj && obj.sData != "")
        {
            $("#LB_SK_ZK").text(0);
            $("#LB_SK_JF").text(0);
        }
    }
};

function SearchData(page, rows, sort, order)
{
    var vtp_Url = "../../CRMGL/CRMGL.ashx";
    var vPageMsgID_ZFFS = 5160003;
    var obj = MakeSearchJSON();
    page = page || $('#GV_SKFS').datagrid("options").pageNumber;
    rows = rows || $('#GV_SKFS').datagrid("options").pageSize;
    sort = sort || $('#GV_SKFS').datagrid("options").sortName;
    order = order || $('#GV_SKFS').datagrid("options").sortOrder;
    $('#GV_SKFS').datagrid("loading");
    $.ajax({
        type: "post",
        url: vtp_Url + "?mode=Search&func=" + vPageMsgID_ZFFS,
        async: true,
        data: {
            json: JSON.stringify(obj),
            titles: 'cybillsearch',
            page: page,
            rows: rows,
            sort: sort,
            order: order,
        },
        success: function (data)
        {
            if (data.indexOf('错误') >= 0 || data.indexOf('error') >= 0)
            {
                ShowMessage(data);
            }
            $('#GV_SKFS').datagrid('loadData', JSON.parse(data), "json");
            $('#GV_SKFS').datagrid("loaded");
            vSearchData = data;
        },
        error: function (data)
        {
            ShowMessage(data);
        }
    });
}


function MakeSearchJSON()
{
    var cond = MakeSearchCondition();
    if (cond == null)
        return;
    var Obj = new Object();
    Obj.SearchConditions = cond;
    Obj.iLoginRYID = iDJR;
    return Obj;
}

function MakeSearchCondition()
{
    var arrayObj = new Array();
    //MakeSrchCondition2(arrayObj, "1, 2", "iBJ_KF", "in", false);
    //MakeSrchCondition2(arrayObj, "0, 2", "iBJ_XSMD", "in", false);
    return arrayObj;
}

function IsValidData()
{


    return true;
}

function SetControlState()
{
    if (parseInt($("#HF_STATUS").val()) == 2)
    {
        $("#B_Start").prop("disabled", true);
    }
    if (vProcStatus == cPS_MODIFY)
    {
        var ListCount = $("#list").datagrid('getRows');
        if (ListCount.length > 0)
        { $("#TB_SKJLBH").prop("disabled", true) }
        else
        { $("#TB_SKJLBH").prop("disabled", false) }
    }
}
vUrl = "../MZKGL.ashx";
var HYKNO = GetUrlParam("HYKNO");


function InitGrid() {
    vSKFSColumnNames = ["支付方式编号", "支付方式代码", "支付方式名称", "收款金额", "交易号"];
    vSKFSColumnModel = [
          { name: "iZFFSID", width: 150 },
          { name: "sZFFSDM", hidden: true },
          { name: "sZFFSMC", width: 150 },
          { name: "fJE", width: 150, editable: true, editor: 'text' },
          { name: "sJYBH", width: 150, editable: true, editor: 'text', hidden: true },

    ];

};

$(document).ready(function () {
    DrawGrid("GV_SKFS", vSKFSColumnNames, vSKFSColumnModel, true);

    if (HYKNO != "") {
        $("#TB_HYKNO").val(HYKNO);
        GetHYXX();
        vProcStatus = cPS_ADD;
        SetControlBaseState();
        $("#TB_HYKNO").attr("readonly", "readonly");
    }
    //vJLBH = GetUrlParam("jlbh");
    //if (vJLBH != "") {
    //    ShowDataBase(vJLBH);
    //};
    $("#TB_MDMC").click(function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", true);
    });


    $("#TB_CDNR").bind('keypress', function (event) {
        if (event.keyCode == "13") {
            GetHYXX();
        }
    });

    $("#TB_HYKNO").bind('keypress', function (event) {
        if (event.keyCode == "13") {
            GetHYXX();
        }
    });
    $("#R_ZFFS").click(function () {
        window.setTimeout(function () {
            SearchData();
        }, 1);

    });



});
function GetZL() {
        var fkje = $("#LB_FKJE").text();
        var ckje = $("#TB_CKJE").val();
        $("#LB_ZL").text((parseFloat($("#LB_FKJE").text()) - parseFloat($("#TB_CKJE").val())).toFixed(2));
}

function InsertClickCustom() {
    window.setTimeout(function () {
        $('#GV_SKFS').datagrid('loadData', { total: 0, rows: [] });
        SearchData();
    }, 100);
}
function GetHYXX() {
    var str = "";
    if ($("#TB_CDNR").val() != "") {
        str = GetMZKXXData(0, "", $("#TB_CDNR").val());
    }
    if ($("#TB_HYKNO").val() != "") {
        str = GetMZKXXData(0, $("#TB_HYKNO").val());
    }
    if (str == "null" || str == "") {
        ShowMessage("卡号不存在或者校验失败");
        return;
    }
    var Obj = JSON.parse(str);
    if (Obj.sHYK_NO == "") {
        ShowMessage("卡号不存在或者校验失败", 3);
        return;
    }
    if (Obj.iBJ_XK == 0) {
        ShowMessage("该卡不允许存取款");
        return;
    }
    if (Obj.iSTATUS < 0) {
        ShowMessage("该卡不是有效卡");
        return;
    }
    $("#HF_HYID").val(Obj.iHYID);
    $("#HF_HYKTYPE").val(Obj.iHYKTYPE);
    $("#LB_HYKNAME").text(Obj.sHYKNAME);
    $("#LB_YJE").text(Obj.fCZJE);
    $("#LB_YXQ").text(Obj.dYXQ);
    $("#LB_PDJE").text(Obj.fPDJE);
    $("#LB_HYK_NO").text(Obj.sHYK_NO);
    $("#LB_FXDWMC").text(Obj.sFXDWMC);

}







function SetControlState() {
    $("#B_Exec").hide();
    $("#status-bar").hide();
    $("#B_Update").hide();
    $("#B_Delete").hide();
}
function IsValidData() {
    if ($("#HF_HYID").val() == "" || $("#HF_HYID").val() == undefined || $("#HF_HYID").val() == null) {
        ShowMessage("会员不存在！请重新录入信息");
        return false;
    }
    if ($("#HF_MDID").val() == "") {
        ShowMessage('请选择操作门店');
        return false;
    }
    if ($("#TB_QKJE").val()>0&&($("#TB_QKJE").val()> Number($("#LB_YJE").text())-Number($("#LB_PDJE").text()))) {
        ShowMessage( '取款金额不可以超过铺底金额');
        return false;
    }

    if ($("#TB_CKJE").val() != "") {
        var fkje = 0;

        var Rows = $("#GV_SKFS").datagrid("getData").rows;
        for (var i = 0; i < Rows.length; i++) {
            var rowData = Rows[i]
            if (parseFloat(rowData.fJE) > 0) {
                fkje = Number(fkje) + Number(rowData.fJE);
            }
        }
        $("#LB_FKJE").text(fkje);
        GetZL();
        if (isNaN($("#LB_ZL").text())) {
            ShowMessage("请确认找零金额");
            return;
        }
    }

    return true;
}

function SaveData() {
    var tab = $('#tabs').tabs('getSelected');
    var tab_index = $('#tabs').tabs('getTabIndex', tab);
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sHYKNO = $("#LB_HYK_NO").text();

    Obj.iHYID = $("#HF_HYID").val();
    Obj.fYJE = $("#LB_YJE").text();
    Obj.iMDID = $("#HF_MDID").val();
    Obj.sZY = $("#TB_ZY").val();
    if ($("#TB_CKJE").val()!="") {
        Obj.fCKJE = $("#TB_CKJE").val();
        Obj.iCK = 1;
        Obj.fFKJE = $("#LB_FKJE").text();
        Obj.fZL = $("#LB_ZL").text();

        var lst_skfs = new Array();
        var Rows = $("#GV_SKFS").datagrid("getData").rows;
        for (var i = 0; i < Rows.length; i++) {
            var rowData = Rows[i]
            if (parseFloat(rowData.fJE) > 0) {
                lst_skfs.push(rowData);
            }
        }
        Obj.ZFFS = lst_skfs;

    }
    if ($("#TB_QKJE").val()!="") {
        Obj.fQKJE = $("#TB_QKJE").val();
        Obj.iQK = 1;

        var lst_skfsqk = new Array();
        var Rows = $("#GV_SKFS").datagrid("getData").rows;
        for (var i = 0; i < Rows.length; i++) {
            var rowData = Rows[i]
            if (parseFloat(rowData.fJE) > 0) {
                lst_skfsqk.push(rowData);
            }
        }
        Obj.ZFFSQK = lst_skfsqk;

    }
    if ($("#TB_TKJE").val() != "") {
        Obj.fQKJE = $("#TB_TKJE").val();
        Obj.iQK = 1;

        var lst_skfsqk = new Array();
        var Rows = $("#GV_SKFS").datagrid("getData").rows;
        for (var i = 0; i < Rows.length; i++) {
            var rowData = Rows[i]
            if (parseFloat(rowData.fJE) > 0) {
                lst_skfsqk.push(rowData);
            }
        }
        Obj.ZFFSQK = lst_skfsqk;
    }

    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#HF_MDID").val(Obj.iMDID);
    $("#TB_MDMC").val(Obj.sMDMC);
    $("#TB_HYKNO").val(Obj.sHYKNO);
    $("#HF_HYID").val(Obj.iHYID);
    $("#HF_HYKTYPE").val(Obj.iHYKTYPE);
    $("#LB_HY_NAME").text(Obj.sHY_NAME);
    $("#LB_HYKNAME").text(Obj.sHYKNAME);
    $("#LB_YJE").text(Obj.fYJE);
    $("#LB_YXQ").text(Obj.dYXQ);
    $("#LB_PDJE").text(Obj.fPDJE);
    $("#TB_CKJE").val(Obj.fCKJE);
    $("#TB_QKJE").val(Obj.fQKJE);
    //$("#GV_SKFS").jqGrid("clearGridData");

    //if (Obj.ZFFS.length != 0) {
    //    $('#GV_SKFS').datagrid('loadData', Obj.ZFFS, "json");
    //}
    //if (Obj.ZFFSQK.length != 0) {
    //    $('#GV_SKFS').datagrid('loadData', Obj.ZFFSQK, "json");
    //}
    //$('#GV_SKFS').datagrid("loaded");

    $("#TB_ZY").val(Obj.sZY);

   
}

function toDecimal2(x) {
    var f = parseFloat(x);
    if (isNaN(f)) {
        return false;
    }
    var f = Math.round(x * 100) / 100;
    var s = f.toString();
    var rs = s.indexOf('.');
    if (rs < 0) {
        rs = s.length;
        s += '.';
    }
    while (s.length <= rs + 2) {
        s += '0';
    }
    return s;
}




function SearchData(page, rows, sort, order) {
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
        success: function (data) {
            if (data.indexOf('错误') >= 0 || data.indexOf('error') >= 0) {
                ShowMessage(data);
            }
            $('#GV_SKFS').datagrid('loadData', JSON.parse(data), "json");
            $('#GV_SKFS').datagrid("loaded");
            vSearchData = data;
        },
        error: function (data) {
            ShowMessage(data);
        }
    });
}


function MakeSearchJSON() {
    var cond = MakeSearchCondition();
    if (cond == null)
        return;
    var Obj = new Object();
    Obj.SearchConditions = cond;
    Obj.iLoginRYID = iDJR;
    return Obj;
}

function MakeSearchCondition() {
    var arrayObj = new Array();
    return arrayObj;
}



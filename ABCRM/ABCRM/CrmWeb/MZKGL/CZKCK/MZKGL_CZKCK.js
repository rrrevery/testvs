
vUrl = "../MZKGL.ashx";
var HYKNO = GetUrlParam("HYKNO");

function InitGrid() {
    vColumnNames = ["支付方式编号", "支付方式代码", "支付方式名称", "收款金额", "交易号"];
    vColumnModel = [
          { name: "iZFFSID", width: 150 },
          { name: "sZFFSDM", hidden: true },
          { name: "sZFFSMC", width: 150 },
          { name: "fJE", width: 150, editable: true, editor: 'text' },
          { name: "sJYBH", width: 150, editable: true, editor: 'text', hidden: true },
    ];
};

$(document).ready(function () {
    if (HYKNO != "") {
        $("#TB_HYKNO").val(HYKNO);
        GetMZKXX();
        //vProcStatus = cPS_ADD;
        //SetControlBaseState();
        $("#TB_HYKNO").attr("readonly", "readonly");
    }
    vJLBH = GetUrlParam("jlbh");//$.getUrlParam("jlbh");
    if (vJLBH != "") {
        ShowDataBase(vJLBH);
    };
    $("#TB_MDMC").click(function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", false);
    });


    $("#TB_HYKNO").bind('keypress', function (event) {//#TB_CDNR,
        if (event.keyCode == "13") {
            GetMZKXX();
        }
    });
});

function GetMZKXX() {
    if ($("#TB_HYKNO").val() != "") {
        var str = GetMZKXXData(0, $("#TB_HYKNO").val(), "", "CRMDBMZK");
        if (str == "null" || str == "") {
            art.dialog({ lock: true, content: "卡号不存在或者校验失败" });
            return;
        }
        var Obj = JSON.parse(str);
        if (Obj.sHYK_NO == "") {
            ShowMessage("卡号不存在或者校验失败", 3);
            return;
        }
        if (Obj.iSTATUS < 0) {
            ShowMessage("卡状态错误，无法存款", 3);
            return;
        }
        $("#HF_HYID").val(Obj.iHYID);
        $("#HF_HYKTYPE").val(Obj.iHYKTYPE);
        $("#LB_HY_NAME").text(Obj.sHY_NAME);
        $("#LB_HYKNAME").text(Obj.sHYKNAME);
        $("#LB_YJE").text(Obj.fCZJE);
    }
}

function SetControlState() {
    ;
}

function IsValidData() {
    if ($("#HF_HYID").val() == "" || $("#HF_HYID").val() == undefined || $("#HF_HYID").val() == null) {
        art.dialog({ lock: true, content: "会员不存在！请重新录入信息" });
        return false;
    }
    if ($("#HF_MDID").val() == "") {
        art.dialog({ lock: true, content: '请选择操作门店' });
        return false;
    }
    return true;
}

function onClick(e, treeId, treeNode) {
    $("#TB_BGDDMC").val(treeNode.name);
    $("#HF_BGDDDM").val(treeNode.id);
    hideMenu("menuContentBGDD");
}

function SaveData() {

    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sHYKNO = $("#TB_HYKNO").val();
    Obj.iHYID = $("#HF_HYID").val();
    Obj.iHYKTYPE = $("#HF_HYKTYPE").val();
    Obj.fYJE = $("#LB_YJE").text();
    Obj.iMDID = $("#HF_MDID").val();
    Obj.sZY = $("#TB_ZY").val();
    Obj.fCKJE = "0";
    var lst_skfs = new Array();
    var rows = $("#list").datagrid("getData").rows;
    for (var i = 0; i < rows.length; i++) {
        if (parseFloat(rows[i].fJE) > 0) {
            lst_skfs.push(rows[i]);
            Obj.fCKJE = parseFloat(Obj.fCKJE) + parseFloat(rows[i].fJE);
        }
    }
    Obj.ZFFS = lst_skfs;

    //Obj.iZFFSID = $("#DDL_ZFFS").combobox("getValue")//; GetSelectValue("DDL_ZFFS");//$("#DDL_SHZFFSID")[0].value;
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    Obj.sDBConnName = "CRMDBMZK";
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
    $("#LB_CKJE").text(Obj.fCKJE);
    $("#TB_ZY").val(Obj.sZY);
    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);

    $('#list').datagrid('loadData', Obj.ZFFS, "json");
    $('#list').datagrid("loaded");
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
    //MakeSrchCondition2(arrayObj, "1, 2", "iBJ_KF", "in", false);
    //MakeSrchCondition2(arrayObj, "0, 2", "iBJ_XSMD", "in", false);
    return arrayObj;
}

function InsertClickCustom() {
    window.setTimeout(function () {
        SearchData();
    }, 1);

};

function SearchData(page, rows, sort, order) {
    var vtp_Url = "../../CRMGL/CRMGL.ashx";
    var vPageMsgID_ZFFS = 5160003;
    var obj = MakeSearchJSON();
    page = page || $('#list').datagrid("options").pageNumber;
    rows = rows || $('#list').datagrid("options").pageSize;
    sort = sort || $('#list').datagrid("options").sortName;
    order = order || $('#list').datagrid("options").sortOrder;
    $('#list').datagrid("loading");
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
            $('#list').datagrid('loadData', JSON.parse(data), "json");
            $('#list').datagrid("loaded");
            vSearchData = data;
        },
        error: function (data) {
            ShowMessage(data);
        }
    });
}
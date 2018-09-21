vUrl = "../LPGL.ashx";
var rowNumer = 0;
var vGZLX = GetUrlParam("GZLX");

function InitGrid() {
    vColumnNames = ["LPID", "礼品代码", "礼品名称", "礼品扣减积分", "所需消费金额"];
    vColumnModel = [
          { name: "iLPID", hidden: true },
          { name: "sLPDM", width: 60, },//editable: true
          { name: "sLPMC", width: 120 },
          { name: "fLPJF", width: 100, editor: 'text', editrules: { required: true, number: true, minValue: 1 } },
          { name: "fLPJE", width: 100, editor: 'text', editrules: { required: true, number: true, minValue: 1 } },
    ];
};

$(document).ready(function () {
    $("#B_Stop").show();
    $("#ZZR").show();
    $("#ZZSJ").show();
    FillHYKTYPETree("TreeHYKTYPE", "TB_HYKNAME");
    if (vGZLX)
    {
        $("input[name='GZLX'][value=" + vGZLX + "]").attr("checked", true);
    }

    $("#AddItem").click(function () {
        var DataArry = new Object();
        DataArry["iDJLX"] =0;
        SelectLP('list', DataArry, 'iLPID');
    });

    $("#DelItem").click(function () {
        DeleteRows("list");
    });


});

function TreeNodeClickCustom(e, treeId, treeNode) {
    $("#HF_HYKTYPE").val(treeNode.id);
}

function SetControlState() {
    document.getElementById("B_Stop").disabled = !($("#LB_ZXRMC").text() != "");
}

function IsValidData() {
    var rowidarr = $("#list").datagrid("getData").rows;
    if ($("#TB_GZMC").val() == "") {
        ShowMessage("请输入规则名称");
        return false;
    }
    if ($("#TB_KSRQ").val() == "" || $("#TB_JSRQ").val() == "") {
        ShowMessage("请输入日期范围");
        return false;
    }
    if ($("#HF_HYKTYPE").val() == "") {
        ShowMessage("请选择卡类型");
        return false;
    }
    if (!checkRadio("GZLX", "规则类型") || !checkRadio("BJ_SR", "生日判断方式") || !checkRadio("BJ_DC", "限制参加次数")
        || !checkRadio("BJ_LJ", "消费金额计算方式") || !checkRadio("BJ_BK", "办卡时间限制") || !checkRadio("BJ_SL", "礼品数量限制")) {
        return false;
    }
    if (rowidarr.length <= 0) {
        ShowMessage("请先选择操作商品");
        return false
    }
    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sGZMC = $("#TB_GZMC").val();
    Obj.dKSRQ = $("#TB_KSRQ").val();
    Obj.dJSRQ = $("#TB_JSRQ").val();
    Obj.iHYKTYPE = $("#HF_HYKTYPE").val();
    Obj.iGZLX = $("[name='GZLX']:checked").val();
    Obj.iBJ_SR = $("[name='BJ_SR']:checked").val();
    Obj.iBJ_DC = $("[name='BJ_DC']:checked").val();
    Obj.iBJ_LJ = $("[name='BJ_LJ']:checked").val();
    Obj.iBJ_BK = $("[name='BJ_BK']:checked").val();
    Obj.iBJ_SL = $("[name='BJ_SL']:checked").val();
    
   

    var lst = new Array();
    lst = $("#list").datagrid("getRows");
    Obj.itemTable = lst;

    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;

    return Obj;
}

function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#TB_GZMC").val(Obj.sGZMC);
    $("#TB_KSRQ").val(Obj.dKSRQ);
    $("#TB_JSRQ").val(Obj.dJSRQ);
    $("#HF_HYKTYPE").val(Obj.iHYKTYPE);
    $("#TB_HYKNAME").val(Obj.sHYKNAME);

    if (Obj.iGZLX == "5") {
        $("#rd_SRL")[0].checked = true;
    }
    if (Obj.iGZLX == "2") {
        $("#rd_BKL")[0].checked = true;
    }
    if (Obj.iGZLX == "4") {
        $("#rd_LDL")[0].checked = true;
    }
    if (Obj.iGZLX == "1") {
        $("#rd_SSL")[0].checked = true;
    }
    if (Obj.iGZLX == "3") {
        $("#rd_JJFL")[0].checked = true;
    }

    if (Obj.iBJ_SR == "0") {
        $("#rd_BXZ")[0].checked = true;
    }
    if (Obj.iBJ_SR == "1") {
        $("#rd_DYSR")[0].checked = true;
    }
    if (Obj.iBJ_SR == "2") {
        $("#rd_DRSR")[0].checked = true;
    }

    if (Obj.iBJ_DC == "0") {
        $("#rd_BXZCS")[0].checked = true;
    }
    if (Obj.iBJ_DC == "1") {
        $("#rd_ZCJYC")[0].checked = true;
    }
    if (Obj.iBJ_DC == "2") {
        $("#rd_DRYC")[0].checked = true;
    }

    if (Obj.iBJ_LJ == "0") {
        $("#rd_BXZ_XF")[0].checked = true;
    }
    if (Obj.iBJ_LJ == "4") {
        $("#rd_DRLJ_XF")[0].checked = true;
    }
    if (Obj.iBJ_LJ == "2") {
        $("#rd_HDQLJ_XF")[0].checked = true;
    }
    if (Obj.iBJ_LJ == "5") {
        $("#rd_SCSC_XF")[0].checked = true;
    }

    if (Obj.iBJ_SL == "0") {
        $("#rd_BXZ_SL")[0].checked = true;
    }
    if (Obj.iBJ_SL == "1") {
        $("#rd_ZNLYG_SL")[0].checked = true;
    }

    if (Obj.iBJ_BK == "0") {
        $("#rd_BXZ_BK")[0].checked = true;
    }
    if (Obj.iBJ_BK == "1") {
        $("#rd_HDQBK_BK")[0].checked = true;
    }

    $('#list').datagrid('loadData', Obj.itemTable, "json");
    $('#list').datagrid("loaded");

    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);
}


function checkRadio(radioname, dialogcontent) {
    var checked = false;
    $("input[name='" + radioname + "']").each(function (index, ele) {
        if ($(this).prop("checked") == true) {
            checked = true;
        }
    });
    if (!checked) {
        ShowMessage("请选择" + dialogcontent);
        return false;
    }
    return true;
}
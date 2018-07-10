vUrl = "../HYKGL.ashx";
var rowNumer = 0;

function InitGrid() {
    vColumnNames = ["hyid", "会员卡号", "余额", "调整金额", "结束日期", "门店范围代码", "促销活动编号", "促销活动"]; 
    vColumnModel = [
            { name: "iHYID", hidden: true, },
          { name: "sHYK_NO" },
          { name: "fJE" },
          { name: "fJE" },
          //{ name: "fTZJE" },
          { name: "dJSRQ", width: 120 },
          { name: "sMDFWDM", hidden: true, },
          { name: "iCXID", hidden: true, },
          { name: "sCXZT", width: 120 },
    ];
};

$(document).ready(function () {
    FillBGDDTree("TreeBGDD", "TB_BGDDMC", "menuContent");
    FillHYKTYPETree("TreeHYKTYPE", "TB_HYKNAME", "menuContentHYKTYPE", 1); 

    $("#pager_center").hide();
    $("#AddItem").click(function () {
        if ($.trim($("#HF_YHQID").val()) == "") {
            ShowMessage("请选择优惠券！", 3);
            return;
        }
        if ($.trim($("#HF_CXID").val()) == "") {
            ShowMessage("请选择促销活动！", 3);
            return;
        }
        if ($.trim($("#TB_HYKNAME").val()) == "") {
            ShowMessage("请选择卡类型！", 3);
            return;
        }
        if ($("#TB_JSSJ1").val() == "" && $("#TB_JSSJ2").val() == "") {
            ShowMessage("请输入结束日期范围！", 3);
            return;
        }

        var condData = new Object();
        condData["iYHQID"] = $("#HF_YHQID").val();
        condData["iCXID"] = $("#HF_CXID").val();
        condData["dJSRQ1"] = $("#TB_JSSJ1").val();
        condData["dJSRQ2"] = $("#TB_JSSJ2").val();
        condData["iHYKTYPE"] = $("#HF_HYKTYPE").val();
        var checkRepeatField = ["iHYID", "iCXID", "sMDFWDM"];
        SelectYHQZH("list", condData, checkRepeatField);
    });

    $("#DelItem").click(function () {
        DeleteRows("list");
    });

    $("#TB_YHQMC").click(function () {
        SelectYHQ("TB_YHQMC", "HF_YHQID", "zHF_YHQID", true);
    });

    $("#TB_CXZT").click(function () {
        if ($("#HF_YHQID").val() == "") {
            ShowMessage("请先选择优惠券", 3);
            return;
        }
        if ($("#list").datagrid("getRows").length > 0) {
            art.dialog({
                title: "删除",
                lock: true,
                content: "是否清空数据？",
                ok: function () {
                    $("#list").datagrid("loadData", { total: 0, rows: [] });
                    SelectCXHD("TB_CXZT", "HF_CXID", "zHF_CXID", true);
                },
                okVal: '是',
                cancelVal: '否',
                cancel: true
            });
        }
        else {
            SelectCXHD("TB_CXZT", "HF_CXID", "zHF_CXID", true, $("#HF_YHQID").val());
        }
    });

    $("#HF_YHQID").change(function () {
        $("#TB_CXZT").val("");
        $("#HF_CXID").val("");
        $("#zHF_CXID").val("");
        $("#list").jqGrid("clearGridData");
    });

})


function IsValidData() {
    if ($.trim($("#HF_BGDDDM").val()) == "") {
        ShowMessage("请选择操作地点！", 3);
        return;
    }
    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.iHYKTYPE = $("#HF_HYKTYPE").val();
    Obj.sBGDDDM = $("#HF_BGDDDM").val();
    Obj.iYHQID = $("#HF_YHQID").val();
    Obj.fTZJE = compute("list", "fJE");
    Obj.iTZSL = $("#list").datagrid("getRows").length;
    //Obj.dJSSJ1 = $("#TB_JSSJ1").val();
   // Obj.dJSSJ2 = $("#TB_JSSJ2").val();
    if ($("#HF_CXID").val() != "") {
        Obj.iCXID = $("#HF_CXID").val();
    }
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
    $("#HF_HYKTYPE").val(Obj.iHYKTYPE);
    $("#TB_HYKNAME").val(Obj.sHYKNAME);
    $("#HF_BGDDDM").val(Obj.sBGDDDM);
    $("#TB_BGDDMC").val(Obj.sBGDDMC);
    $("#HF_YHQID").val(Obj.iYHQID);
    $("#TB_YHQMC").val(Obj.sYHQMC);
    $("#LB_TZJE").text(Obj.fTZJE);
    $("#LB_TZSL").text(Obj.iTZSL);
    $("#HF_CXID").val(Obj.iCXID);
    $("#TB_CXZT").val(Obj.sCXZT);
    $("#TB_MDMC").val(Obj.sMDMC);
    $("#HF_MDID").val(Obj.iMDID);


    $('#list').datagrid('loadData', Obj.itemTable, "json");
    $('#list').datagrid("loaded");

    //$("#TB_JSSJ1").val(Obj.dJSSJ1);
    //$("#TB_JSSJ2").val(Obj.dJSSJ2);
    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);
}

function compute(lisName, colName) {
    var rows = $('#' + lisName + '').datagrid('getRows')//获取当前的数据行
    var total = 0;
    for (var i = 0; i < rows.length; i++) {
        total += rows[i]['' + colName + ''];
    }
    return total;
}
function onClick(e, treeId, treeNode) {
    $("#TB_BGDDMC").val(treeNode.name);
    $("#HF_BGDDDM").val(treeNode.id);
    hideMenu("menuContent");
}

function onHYKClick(e, treeId, treeNode) {
    if ($("#list").datagrid("getData").rows.length > 0) {
        art.dialog({
            title: "删除",
            lock: true,
            content: "是否清空数据？",
            ok: function () {
                $('#list').datagrid("loadData", { total: 0, rows: [] });
                $("#TB_HYKNAME").val(treeNode.name);
                $("#HF_HYKTYPE").val(treeNode.id);
            },
            okVal: '是',
            cancelVal: '否',
            cancel: true
        });
    }
    else {
        $("#TB_HYKNAME").val(treeNode.name);
        $("#HF_HYKTYPE").val(treeNode.id);
    }
    hideMenu("menuContentHYKTYPE");
}
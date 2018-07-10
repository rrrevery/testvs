
vUrl = "../MZKGL.ashx";
vCaption = "面值卡批量存款";
iStatus = 0;


function InitGrid() {
    vPayColumnNames = ["支付方式编号", "支付方式代码", "支付方式名称", "收款金额", "交易号"],
    vPayColumnModel = [
          { name: "iZFFSID", hidden: true },
          { name: "sZFFSDM", width: 150 },
          { name: "sZFFSMC", width: 150 },
          { name: "fJE", width: 150, editable: true, editor: 'text' },
          { name: "sJYBH", width: 150, editable: true, editor: 'text' ,hidden:true},
    ];
    idPayField = "iZFFSID";
    vColumnNames = ['开始卡号', '结束卡号', 'iHYKTYPE', '卡类型', '数量', '充值金额', 'iRANDOM_LEN', ],
    vColumnModel = [
        { name: 'sCZKHM_BEGIN', index: 'CZKHM_BEGIN', width: 100, },
        { name: 'sCZKHM_END', index: 'CZKHM_END', width: 100, },
        { name: 'iHYKTYPE', index: 'HYKTYPE', hidden: true, },
        { name: 'sHYKNAME', index: 'HYKNAME', width: 50, hidden: true, },//align: "right", editable: true,edittype:"select", editoptions: { value:"101:金卡;102:银卡" }
        { name: 'iSKSL', index: 'SKSL', width: 20, align: "right", },
        { name: 'fMZJE', index: 'MZJE', width: 50, align: 'right', },
        { name: 'iRANDOM_LEN', index: 'iRANDOM_LEN', hidden: true, },

    ];

    vCouponColumnNames = ['卡号', '优惠券', '优惠券ID', '有效期', '赠券金额'],
    vCouponColumnModel = [
        { name: 'sHYK_NO', index: 'sHYK_NO', width: 100, },
        { name: 'sYHQMC', index: 'sYHQMC', width: 100, },
        { name: 'iYHQLX', index: 'iYHQLX', hidden: true, },
        { name: 'iYXQTS', index: 'iYXQTS', width: 50, },
        { name: 'fZKJE', index: 'fZKJE', width: 50, align: 'right', },  //integral 
    ];
    vIntegralColumnNames = ['卡号', '赠送积分'],
    vIntegralColumnModel = [
        { name: 'sHYK_NO', index: 'sHYK_NO', width: 160, },
        { name: 'fZSJF', index: 'fZSJF', width: 160, },
    ];
};

function AddCustomerButton() {
    AddToolButtons("打印", "B_Print");
}

function TreeNodeClickCustom(event, treeId, treeNode) {
    switch (treeId) {
        case "TreeBGDD": $("#HF_BGDDDM").val(treeNode.sBGDDDM); break;
    }
}
$(document).ready(function () {
    FillBGDDTree("TreeBGDD", "TB_BGDDMC");

    DrawGrid("GV_SKFS", vPayColumnNames, vPayColumnModel, true);
    if (vJLBH == "") {
        SearchData("", "", "", "", "GV_SKFS");
    }
    $("#TB_MDMC").click(function () {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", false);
    })

    $("#TB_HYKNAME").click(function () {
        var condData = new Object();
        condData["vCZK"] = 1;
        SelectKLX("TB_HYKNAME", "HF_HYKTYPE", "zHF_HYKTYPE", false, condData);
    });

    $("#AddItem").click(function () {
        if ($("#TB_CZKHM_BEGIN").val() == "") {
            ShowMessage("请输入开始卡号");
            return;
        }
        if ($("#TB_CZKHM_END").val() == "") {
            ShowMessage("请输入结束卡号");
            return;
        }
        if ($("#HF_HYKTYPE").val() == "") {
            ShowMessage("请选择卡类型", 3);
            return;
        }
        if ($("#TB_SK_JE").val() == "") {
            ShowMessage("请输入存款金额", 3);
            return;
        }
        if ($("#TB_CZKHM_BEGIN").val().length != $("#TB_CZKHM_END").val().length) {
            ShowMessage("开始卡号与结束卡号长度不一致");
            return;
        }

        var str1 = GetMZKXXData(0, $("#TB_CZKHM_BEGIN").val(), "", "CRMDBMZK");
        if (str1 == "" || str1 == undefined) {
            ShowMessage("没有找到开始卡号的面值卡");
            return;
        }
        var obj1 = JSON.parse(str1);
        //if (obj1.sCZKHM == "") {
        //    ShowMessage("没有找到开始卡号的面值卡");
        //    return;
        //}
        //if (obj1.iSTATUS != iStatus) {
        //    ShowMessage("开始卡号的状态错误");
        //    return;
        //}
        var str2 = GetMZKXXData(0, $("#TB_CZKHM_END").val(), "", "CRMDBMZK");
        if (str2 == "") {
            ShowMessage("没有找到结束卡号的面值卡");
            return;
        }
        var obj2 = JSON.parse(str2);
        //if (obj2.sCZKHM == "") {
        //    ShowMessage("没有找到结束卡号的面值卡");
        //    return;
        //}
        //if (obj2.iSTATUS != iStatus) {
        //    ShowMessage("结束卡号的状态错误");
        //    return;
        //}
        if (obj1.iHYKTYPE != obj2.iHYKTYPE) {
            ShowMessage("开始卡类型与结束卡类型不一致");
            return;
        }
        CalcKD($("#TB_CZKHM_BEGIN").val(), $("#TB_CZKHM_END").val(), $("#HF_HYKTYPE").val());
    });
    $("#DelItem").click(function () {
        DeleteRows("list");
    });
})

function SetControlState() {
    ;
}

function IsValidData() {
    if ($("#HF_MDID").val() == "") {
        ShowMessage("请选择门店！", 3);
        return false;
    }
    if ($("#HF_HYKTYPE").val() == "") {
        ShowMessage("请选择卡类型！", 3);
        return false;
    }
    vYSJE = 0;
    vSSJE = 0;
    var lst = $("#list").datagrid("getData").rows;
    for (var i = 0; i < lst.length; i++) {
        vYSJE = parseFloat(vYSJE) + (parseInt(lst[i].iSKSL) * parseFloat(lst[i].fMZJE))
    }
    var rows = $("#GV_SKFS").datagrid("getData").rows;
    for (var i = 0; i < rows.length; i++) {
        if (parseFloat(rows[i].fJE) > 0) {
            vSSJE = parseFloat(vSSJE) + parseFloat(rows[i].fJE);
        }
    }
    if (vYSJE != vSSJE) {
        ShowMessage("应付金额与实际付款金额不符，请检查！", 3);
        return false;
    }
    return true;
}

function SaveData() {
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "") { Obj.iJLBH = "0"; }
    Obj.FS = 1;
    Obj.iSKSL = 0;
    Obj.fYSZE = 0;
    Obj.fZKL = 0;
    Obj.fSSJE = "0";
    //Obj.iMDID_CZ = $("#HF_MDID").val();
    Obj.sCZDD = $("#HF_BGDDDM").val();

    Obj.sZY = $("#TA_ZY").val();
    Obj.iKHID = ($("#HF_KHID").val() == "") ? "0" : $("#HF_KHID").val();
    Obj.LXR = "0";
    Obj.YWY = ($("#HF_YWY").val() == "") ? "0" : $("#HF_YWY").val();
    Obj.iHYKTYPE = $("#HF_HYKTYPE").val();
    var lst = new Array();
    lst = $("#list").datagrid("getData").rows;
    for (var i = 0; i < lst.length; i++) {
        Obj.iSKSL = parseInt(Obj.iSKSL) + parseInt(lst[i].iSKSL);
        Obj.fYSZE = parseFloat(Obj.fYSZE) + (parseInt(lst[i].iSKSL) * parseFloat(lst[i].fMZJE))
    }
    Obj.SKKD = lst;
    var lst_skfs = new Array();
    var rows = $("#GV_SKFS").datagrid("getData").rows;
    for (var i = 0; i < rows.length; i++) {
        if (parseFloat(rows[i].fJE) > 0) {
            lst_skfs.push(rows[i]);
            Obj.fSSJE = parseFloat(Obj.fSSJE) + parseFloat(rows[i].fJE);
        }
    }
    Obj.ZFFS = lst_skfs;
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    Obj.BJ_CK = 1;
    return Obj;
}
function ShowData(data) {
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#LB_SK_YSJE").html(Obj.fYSZE);
    $("#LB_SK_YSZS").html(Obj.iSKSL);
    $("#LB_ZKL").html(Obj.ZKL);
    $("#LB_SK_ZK").html(Obj.fZSJE);
    $("#LB_SK_JF").html(Obj.fYZSJF);
    $("#LB_ZK_YSJE").html(Obj.fSJZSJE);
    $("#LB_ZK_YSZS").html(Obj.ZKSL);
    $("#LB_CK_ZSJE").html(Obj.fSJZSJE);
    $("#LB_CK_ZSJF").html(Obj.fSJZSJF);
    //$("#HF_MDID").val(Obj.iMDID_CZ);

    //$("#TB_MDMC").val(Obj.sMDMC);
    $("#HF_BGDDDM").val(Obj.sCZDD);
    $("#TB_BGDDMC").val(Obj.sBGDDMC);

    $("#TB_HYKNAME").val(Obj.sHYKNAME);
    $("#HF_HYKTYPE").val(Obj.iHYKTYPE);
    $("#TA_ZY").val(Obj.sZY);
    $("#TB_DZKFJE").val(Obj.DZKFJE);
    $("#TB_LXRMC").val(Obj.YWY);

    $('#list').datagrid('loadData', Obj.SKKD, "json");
    $('#list').datagrid("loaded");

    $('#GV_SKFS').datagrid('loadData', Obj.ZFFS, "json");
    $('#GV_SKFS').datagrid("loaded");


    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);
    if (Obj.iZXR == "") {
        $("#B_Print").prop("disabled", true);
    }
    else {
        $("#B_Print").prop("disabled", false);
    }
    //
    $("#LB_SX_JLBH").html(Obj.SXJLBH);
    $("#LB_SX_JE").html(Obj.SXJE);
    //
    $("#HF_KHID").val(Obj.KHID);
    $("#TB_KHMC").val("[" + Obj.sLXRSJ + "]+[" + Obj.sLXRXM + "]");
    $("#TB_KHMC1").val("[" + Obj.iKHID + "]+[" + Obj.sKHMC + "]");
    $("#HF_ZKYHQMC").val(Obj.ZKYHQMC);
    $("#HF_ZKYHQLX").val(Obj.ZKYHQLX);
    $("#HF_ZKYHQTS").val(Obj.ZKYHQTS);


    $("#LB_ZCKJE").text(Obj.fYSZE);
    $("#LB_ZCKSL").text(Obj.iSKSL);
}

function onClick(e, treeId, treeNode) {
    $("#TB_BGDDMC").val(treeNode.name);
    $("#HF_BGDDDM").val(treeNode.id);
    hideMenu("menuContent");
}

function onHYKClick(e, treeId, treeNode) {
    $("#TB_HYKNAME").val(treeNode.name);
    $("#HF_HYKTYPE").val(treeNode.id);
    hideMenu("menuContentHYKTYPE");
}

function GetKCKXX(value, colname) {

    var str = GetKCKXXData(value, 0);
    if (str == "null" || str == "") {
        return [false, "没有找到卡号"];
    }
    else {
        var Obj = JSON.parse(str);
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var rowdata = $("#list").getRowData(rowid);
        $("#list").setCell(rowid, 'iHYKTYPE', Obj.iHYKTYPE);
        $("#list").setCell(rowid, 'sHYKNAME', Obj.sHYKNAME);
        $("#LB_SL").text(rowdata.sCZKHM_END - rowdata.sCZKHM_BEGIN);
        return [true, ""];
    }
}

function LoadData(rowData) {
    $("#list").clearGridData();
    $("#GV_ZK").clearGridData();
}


function SearchData(page, rows, sort, order, list) {
    var vtp_Url = "../../CRMGL/CRMGL.ashx";
    var vPageMsgID_ZFFS = 5160003;
    var obj = MakeSearchJSON();
    page = page || $('#' + list).datagrid("options").pageNumber;
    rows = rows || $('#' + list).datagrid("options").pageSize;
    sort = sort || $('#' + list).datagrid("options").sortName;
    order = order || $('#' + list).datagrid("options").sortOrder;
    $('#' + list).datagrid("loading");
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
            $('#' + list).datagrid('loadData', JSON.parse(data), "json");
            $('#' + list).datagrid("loaded");
            vSearchData = data;
        },
        error: function (data) {
            ShowMessage(data);
        }
    });
}


function CalcKD(sCZKHM_BEGIN, sCZKHM_END, iHYKTYPE) {

    var rowList = $("#list").datagrid("getData").rows;
    txsl = 0;
    for (i = 0; i < rowList.length ; i++) {
        var rowData = rowList[i];
        if (rowList[i].iHYKTYPE == iHYKTYPE)
            txsl += parseInt(rowList[i].iSKSL);
        if (rowData.sCZKHM_BEGIN == sCZKHM_BEGIN) {
            ShowMessage("新增卡号段和已有卡号段存在同样的开始卡号，不能添加");
            return;
        }
        if (rowData.sCZKHM_BEGIN == sCZKHM_BEGIN) {
            ShowMessage("新增卡号段和已有卡号段存在同样的结束卡号，不能添加");
            return;
        }
    }
    var vDBConnName = "CRMDBMZK";
    var vCZK = 0;
    var ZSL=0;
    jsdata = "{'iSTATUS':" + iStatus + ",'sBGDDDM':'" + $("#HF_BGDDDM_BC").val() + "','iHYKTYPE':'" + iHYKTYPE + "','sCZKHM_BEGIN':'" + sCZKHM_BEGIN + "','sCZKHM_END':'" + sCZKHM_END + "','sDBConnName':'" + vDBConnName + "','iBJ_KCK':" + 0 + "}";
    $.ajax({
        type: 'post',
        asycn: false,
        url: "../../CrmLib/CrmLib.ashx?func=GetMZKKCKKD",
        dataType: "text",
        data: { json: jsdata, titles: 'cecece' },
        success: function (data) {
            try {
                var Obj = JSON.parse(data);
                for (i = 0; i < Obj.length; i++) {
                    $('#list').datagrid('appendRow', {
                        sCZKHM_BEGIN: Obj[i].sCZKHM_BEGIN,
                        sCZKHM_END: Obj[i].sCZKHM_END,
                        iHYKTYPE: Obj[i].iHYKTYPE,
                        sHYKNAME: Obj[i].sHYKNAME,
                        iSKSL: Obj[i].iSKSL,
                        fMZJE: $("#TB_SK_JE").val(),
                        
                    });
                    ZSL = Number(ZSL) +Number(Obj[i].iSKSL);
                }
                $("#TB_CZKHM_BEGIN").val("");
                $("#TB_CZKHM_END").val("");
                 var S= $("#TB_SK_JE").val();
                 $("#LB_ZCKJE").text(Number(S) * Number(ZSL));
                 $("#LB_ZCKSL").text(ZSL);
                 $("#TB_SK_JE").val("");


            } catch (e) {
                alert(data);
            }
        }
    });
}

function InsertClickCustom() {
    if ($("#GV_SKFS")[0].className != "") {
        SearchData("", "", "", "", "GV_SKFS");
    }
};

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





//vPageMsgID = CM_HYXF_HYZDY;
vUrl = "../HYXF.ashx";

var YXXG = GetUrlParam("pYXXG");
var ZXRQ = GetUrlParam("pZXRQ");
var zjlxarr = new Array();
var zylxarr = new Array();
var sexarr = new Array();

var vGKXXColumnNames;
var vGKXXColumnModel;
var vBQColumnNames;
var vBQColumnModel;

if (status == 0)
{
    function InitGrid()
    {
        vColumnNames = ['HYID', "卡号", "卡类型", '会员姓名'];
        vColumnModel = [
              { name: "sSJNR", hidden: true, },
              { name: "sSJDM", width: 150, search: true },
              { name: "sSJMC", width: 100, search: false, hidden: true },
              { name: "sSJMC2", width: 100, search: true }
        ];
        vGKXXColumnNames = ["顾客ID", '顾客姓名', "性别", "手机号码"];
        vGKXXColumnModel = [
            { name: "iGKID", width: 100, },
            { name: "sGK_NAME", width: 100, },
            {
                name: "iSEX", width: 100, formatter: function (cellvalue)
                {
                    switch (cellvalue)
                    {
                        case "0":
                        case 0:
                            return "男";
                            break;
                        case "1":
                        case 1:
                            return "女";
                            break;
                        default:
                            return cellvalue;
                            break;

                    }
                }
            },
            { name: "sSJHM", width: 100, }
        ];
     
    }

}
if (status == 1)
{
    function InitGrid()
    {
        vBQColumnNames = ['标签项目名称', "项目代码", "标签项目值", "项目值ID"];
        vBQColumnModel = [
              { name: "sSJMC", hidden: false, },
              { name: "sSJNR", width: 150, hidden: true },
              { name: "sSJMC2", width: 100, },
              { name: "sSJNR2", width: 100, hidden: true }
        ];
    }
}



$(document).ready(function ()
{
    if (status == 0)
    {
        DrawGrid("list_gkxx", vGKXXColumnNames, vGKXXColumnModel, true);      
    }
    else
        DrawGrid("HYBQList", vBQColumnNames, vBQColumnModel, true);
    $("#B_Stop").show();
    //$("#ZZR").show();
    //$("#ZZSJ").show();
    CYCBL_ADD_ITEM("CB_ZJLX", GetHYXXXM(0));
    CYCBL_ADD_ITEM("CB_ZYLX", GetHYXXXM(1));
    $("#HF_BJ_DJT").val(status);
    FillCheckTree("TreeHYBQ", "FillHYBQTree", iDJR);
    //vJLBH = GetUrlParam("jlbh");//$.getUrlParam("jlbh");
    //if (vJLBH != "") {
    //    ShowDataBase(vJLBH);
    //};
    // FillHYKTYPETree("TreeHYKTYPE", "TB_HYKNAME", "menuContentHYKTYPE", 1);
    BFButtonClick("TB_MDMC", function ()
    {
        SelectMD("TB_MDMC", "HF_MDID", "zHF_MDID", false);
    });
    BFButtonClick("TB_MDXF_MD", function ()
    {
        SelectMD("TB_MDXF_MD", "HF_MDXF_MD", "zHF_MDXF_MD", false);
    });
    BFButtonClick("TB_HYKNAME", function ()
    {
        SelectKLX("TB_HYKNAME", "HF_HYKTYPE", "zHF_HYKTYPE", false);
    });



    //$("h2[id*='H']").each(function () {
    //    $(this).show();
    //});
    FillHYZLXTree("TreeHYZLX", "TB_HYZLXMC");
    FillFXDWTree("TreeFXDW", "TB_FXDWMC");
    //FillMD($("#S_MDXF_MD"));
    //FillMD($("#S_PLXF_MD"));
    //FillMD($("#S_SPXF_MD"));
    //FillMD($("#S_MDID"));

    $("#TB_GXZQ").inputmask("9{*}", { skipOptionalPartCharacter: "" });;

    //证件类型
    $("#CB_ZJLX").on("click", "input[type='checkbox']", function ()
    {
        var ele = $(this).prop("checked", this.checked);
        var index = $.inArray(ele.val(), zjlxarr);
        if (this.checked)
        {//选中
            if (index == -1)
            {
                zjlxarr.push(ele.val());
            }
        }
        else
        {//取消选中
            if (index != -1)
            {
                var temp = zjlxarr[index];
                zjlxarr[index] = zjlxarr[zjlxarr.length - 1];
                zjlxarr[zjlxarr.length - 1] = temp;
                zjlxarr.pop();
            }
        }
    });
    //职业类型
    $("#CB_ZYLX").on("click", "input[type='checkbox']", function ()
    {
        var ele = $(this).prop("checked", this.checked);
        var index = $.inArray(ele.val(), zylxarr);
        if (this.checked)
        {//选中
            if (index == -1)
            {
                zylxarr.push(ele.val());
            }
        }
        else
        {//取消选中
            if (index != -1)
            {
                var temp = zylxarr[index];
                zylxarr[index] = zylxarr[zylxarr.length - 1];
                zylxarr[zylxarr.length - 1] = temp;
                zylxarr.pop();
            }
        }
    });

    //添加卡
    $("#AddItem").click(function ()
    {
        var DataArry = new Object();
        //DataArry["iMDID"] = parseInt($("#HF_MDID").val());
        SelectHYK('list', DataArry, 'iHYID');

    });
    $("#DelItem").click(function ()
    {
        DeleteRows("list");
    });
    $("#BTN_DELBQ").click(function ()
    {
        DeleteRows("HYBQList");
    });
    $("#Add_GKXX").click(function ()
    {
        var DataArry = new Object();
        //DataArry["iMDID"] = parseInt($("#HF_MDID").val());
        SelectGKXX('list_gkxx', DataArry, 'iJLBH');

    });
    //删除顾客
    $("#Del_GKXX").click(function ()
    {
        DeleteRows("list_gkxx");
    });
    CheckBox("CB_BJ_DJT", "HF_BJ_DJT");
    CheckBox("CB_BJ_YXXG", "HF_BJ_YXXG");
    CheckBox("CB_SEX", "HF_SEX");
    CheckBox("CB_SRFS", "HF_SRFS");
    CheckBox("CB_MDXF_XFPMQH", "HF_MDXF_XFPMQH");


    //下拉框控制 
    $("select").each(function ()
    {
        $(this).change(function ()
        {
            var hfinput = $(this).attr("id").replace("S", "HF");
            var yt = $(this).val();
            $("#" + hfinput).val($(this).val());
        });
    });

});


//function onHYZLXClick(e, treeId, treeNode) {
//    $("#TB_HYZLXMC").val(treeNode.name);
//    $("#HF_HYZLXID").val(treeNode.lxid);
//    hideMenu("menuContentHYZLX");
//};
//function onHYKClick(e, treeId, treeNode) {
//    $("#TB_HYKNAME").val(treeNode.name);
//    $("#HF_HYKTYPE").val(treeNode.id);
//    hideMenu("menuContentHYKTYPE");
//};
//function onFXDWClick(e, treeId, treeNode) {
//    $("#TB_FXDWMC").val(treeNode.name);
//    $("#HF_FXDWDM").val(treeNode.data);
//    hideMenu("menuContentFXDW");
//}
function TreeNodeClickCustom(e, treeId, treeNode)
{
    switch (treeId)
    {
        case "TreeHYZLX": $("#HF_HYZLXID").val(treeNode.iHYZLXID); break;
        case "TreeFXDW": $("#HF_FXDWDM").val(treeNode.sFXDWDM); break;
    }
};

function SetControlState()
{
    $("[name='CB_BJ_DJT']").prop("disabled", true);
    $("#tab1 input,#tab1 button,#tab1 label").prop("disabled", vProcStatus == cPS_BROWSE);
    if (YXXG == "1" && ZXRQ != "" && $("#LB_ZZRQ").text() == "" && $("#B_Save").prop("disabled"))
    {
        $("#B_Update").prop("disabled", false);
    }
    if ($("#HF_ZXR").text() == "") {
        $("#B_Update").prop("disabled", true);
    }
    //if ($("#LB_ZXRQ").text() != "" && $("#LB_ZZRQ").text() == "" && vProcStatus == cPS_BROWSE)
    //{
    //    $("#B_Stop").prop("disabled", false);
    //}
    $("#dthyz").find("input").attr("disabled", vProcStatus == cPS_BROWSE)
    $("#jthyz").find("input").attr("disabled", vProcStatus == cPS_BROWSE)
    if (status == 0)
    {
        $("#jthyz").show();
        $("#dthyz").hide();
        $("#DT").hide();
    }
    else
    {
        $("#jthyz").hide();
        $("#dthyz").show();
        $("#DT").hide();
    }
    $("[name='CB_BJ_DJT']").prop("disabled", true);
    $("[name='CB_BJ_DJT'][value=" + status + "]").prop("checked", true);
    $("#HF_BJ_DJT").val(status);

}

function IsValidData()
{
    if ($("#TB_GRPMC").val() == "")
    {
       ShowMessage('请输入客群组名称!');
        return false;
    }
    if ($("#HF_HYZLXID").val() == "")
    {
        ShowMessage('请选择客群组类型!');
        return false;
    }
    if ($("#HF_GRPYT").val() == "")
    {
        ShowMessage('请选择客群组用途!');
        return false;
    }
    if ($("#HF_JB").val() == "")
    {
        ShowMessage('请选择客群组级别!');
        return false;
    }
    //if ($("#TB_GXZQ").val() == "" && status == 1) {
    //    art.dialog({ content: '请输入更新周期!', lock: true, time: 2 });
    //    return false;
    //}
    if ($("#HF_MDID").val() == "" && $("#S_JB").val() == 2)
    {
        ShowMessage( '请选择操作门店!');
        return false;
    }
    if ($("#HF_BJ_DJT").val() == "")
    {
        ShowMessage('请选择动静态标记!');
        return false;
    }
    if ($("#HF_BJ_YXXG").val() == "")
    {
        ShowMessage('请选择是否允许修改!');
        return false;
    }

    return true;
}

//保存子表数据（）
function SaveItemData(sjnr_element, sjnr2_element, sjlx, datestring)
{
    var Item_m = new Object();

    Item_m.iSJLX = sjlx;
    if ($("#" + sjnr_element).val() == "")
    {
        Item_m.sSJNR = "0";
    }
    else
    {
        Item_m.sSJNR = $("#" + sjnr_element).val();
        if (datestring)
            Item_m.sSJNR = Item_m.sSJNR.replace(/\-/g, "")

    }
    Item_m.sSJNR2 = ($("#" + sjnr2_element).val() != "" && sjnr2_element != "") ? $("#" + sjnr2_element).val() : "";
    if (datestring)
    {
        Item_m.sSJNR2 = Item_m.sSJNR2.replace(/\-/g, "")
    }
    Item_m.iGZBH = "0";

    return Item_m;

}



//SJLX： 1 是会员id
//2 是证件类型
//3 是卡类型
//4 是性别
//5 是职业类型
//6 是发行单位


function SaveData()
{
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    //主表数据
    Obj.sGRPMC = $("#TB_GRPMC").val();
    Obj.iGRPYT = $("#HF_GRPYT").val();
    Obj.sGRPMS = $("#TB_GRPMS").val();
    Obj.iGZFS = 1;
    if (status == 0)
    {
        Obj.iGZFS = 0;
    }
    Obj.dKSSJ = $("#TB_KSSJ").val();
    Obj.dJSSJ = $("#TB_JSSJ").val();
    Obj.sGRPMS = $("#TB_GRPMS").val();

    Obj.iHYZLXID = $("#HF_HYZLXID").val();

    Obj.iJB = $("#HF_JB").val();
    Obj.iBJ_DJT = $("[name='CB_BJ_DJT']:checked").val();
    Obj.iBJ_YXXG = $("[name='CB_BJ_YXXG']:checked").val();
    Obj.iBJ_WXHY = $("#BJ_WXHY")[0].checked ? "1" : "0";
    if ($("#S_JB").val() != 2)
        Obj.iMDID = 0;
    else
        Obj.iMDID = $("#HF_MDID").val();

    //子表数据
    var ItemTable = new Array();

    //静态客群组 规则
    if (status == 0)
    {
        //卡数据（同样在子表当中）
        var lst = new Array();
        lst = $("#list").datagrid("getData").rows;
        for (var i = 0; i < lst.length ; i++)
        {
            var Item_lst = new Object();

            Item_lst = lst[i];
            Item_lst.sSJNR = lst[i].sSJNR;
            Item_lst.iSJLX = 1;//单卡模式下的 SJLX=1时，代表SJNR存的是HYID
            Item_lst.iGZBH = "0";
            ItemTable.push(Item_lst);
            if (!lst[i].sSJNR)
            {
                Item_lst.iSJLX = 7;
                Item_lst.sSJNR = rowData.sSJDM;
            }

        }
        //顾客
        //var lst_gk = new Array();
        //lst_gk = $("#list_gkxx").datagrid("getData").rows;
        //for (var i = 0; i < lst_gk.length ; i++) {
        //    var Item_lst = new Object();
        //    Item_lst = lst_gk[i];
        //    Item_lst.sSJNR = lst_gk[i].iGKID;
        //    Item_lst.iSJLX = 6;
        //    Item_lst.iGZBH = "0";
        //    ItemTable.push(Item_lst);
        //}
    }
    else
    {
        if ($("#TB_GXZQ").val() == "")
        {
            Obj.iGXZQ = 0;
        }
        else
        {
            Obj.iGXZQ = $("#TB_GXZQ").val();
        }

        Obj.dYXQ = $("#TB_YXQ").val();
        Obj.iSRFS = $("#HF_SRFS").val() == "" ? -1 : $("#HF_SRFS").val();
        //动态客群组规则
        //基本条件
        //ItemTable.push(SaveItemData("TB_KKRQ1", "TB_KKRQ2", "20", true));

        //2 证件类型
        for (i = 0; i < zjlxarr.length; i++)
        {
            var Item_lst = new Object();
            Item_lst.sSJNR = zjlxarr[i];
            Item_lst.iGZBH = 0;
            Item_lst.iSJLX = 2;
            ItemTable.push(Item_lst);
        }
        //3  会员卡类型
        if ($("#HF_HYKTYPE").val() != "")
        {
            ItemTable.push(SaveItemData("HF_HYKTYPE", "", "3"));
        }
        // 4 性别
        if ($("#HF_SEX").val() != "")
        {
            var Item_lst = new Object();
            Item_lst.sSJNR = $("#HF_SEX").val();
            Item_lst.iGZBH = 0;
            Item_lst.iSJLX = 4;
            ItemTable.push(Item_lst);
        }
        //5 职业类型
        for (i = 0; i < zylxarr.length; i++)
        {
            var Item_lst = new Object();
            Item_lst.sSJNR = zylxarr[i];
            Item_lst.iGZBH = 0;
            Item_lst.iSJLX = 5;
            ItemTable.push(Item_lst);
        }
        //6  发行单位
        if ($("#HF_FXDWDM").val() != "")
        {
            ItemTable.push(SaveItemData("HF_FXDWDM", "", "6"));
        }
        //ItemTable.push(SaveItemData("HF_FXDWDM", "", "22"));
        ItemTable.push(SaveItemData("TB_ZHXFRQ1", "TB_ZHXFRQ2", "23", true));
        ItemTable.push(SaveItemData("TB_JFYE1", "TB_JFYE2", "24"));
        ItemTable.push(SaveItemData("TB_SJJF1", "TB_SJJF2", "25"));
        ItemTable.push(SaveItemData("TB_XFJF1", "TB_XFJF2", "26"));

        //会员门店消费条件
        if ($("#HF_MDXF_MD").val() != "")
        {
            ItemTable.push(SaveItemData("HF_MDXF_MD", "", "27"));
            ItemTable.push(SaveItemData("TB_MDXF_HZSJ1", "TB_MDXF_HZSJ2", "28", true));
            ItemTable.push(SaveItemData("TB_MDXF_ZHXF1", "TB_MDXF_ZHXF2", "29", true));
            ItemTable.push(SaveItemData("TB_MDXF_XFJE1", "TB_MDXF_XFJE2", "30"));
            ItemTable.push(SaveItemData("TB_MDXF_LDCS1", "TB_MDXF_LDCS2", "31"));
            ItemTable.push(SaveItemData("TB_MDXF_KDJ1", "TB_MDXF_KDJ2", "32"));
            ItemTable.push(SaveItemData("TB_MDXF_XFCS1", "TB_MDXF_XFCS2", "33"));
            ItemTable.push(SaveItemData("HF_MDXF_XFPMQH", "TB_MDXF_XFPM", "34"));
        }

        //会员品类消费条件
        //if ($("#S_PLXF_MD").val() != "") {
        //    ItemTable.push(SaveItemData("S_PLXF_MD", "", "35"));
        //    ItemTable.push(SaveItemData("S_PLXF_BM", "", "36"));
        //    ItemTable.push(SaveItemData("TB_PLXF_HZSJ1", "TB_PLXF_HZSJ2", "37"));
        //}

        //会员商品消费条件
        //if ($("#S_SPXF_MD").val() != "") {
        //    ItemTable.push(SaveItemData("S_SPXF_MD", "", "38"));
        //    ItemTable.push(SaveItemData("TB_SPXF_HZSJ1", "TB_SPXF_HZSJ2", "39"));
        //}

        //会员便签 SJLX =40

        var IDs = $("#HYBQList").datagrid("getData").rows;
        for (i = 0; i < IDs.length ; i++) {
            var rowData = IDs[i];
            var Item_lst = new Object();
            Item_lst = rowData;
            Item_lst.sSJNR = rowData.sSJNR;
            Item_lst.sSJNR2 = rowData.sSJNR2;
            Item_lst.sSJMC = rowData.sSJMC;
            Item_lst.sSJMC2 = rowData.sSJMC2;
            Item_lst.iSJLX = 40;
            Item_lst.iGZBH = "0";
            ItemTable.push(Item_lst);
            //同购商品数据（待做）
        }
    }



    //设置子表
    Obj.itemTable = ItemTable;

    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    return Obj;

}
function ShowData(data)
{
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val(Obj.iJLBH);
    $("#TB_GRPMC").val(Obj.sGRPMC);
    $("#S_GRPYT").val(Obj.iGRPYT);
    $("#HF_GRPYT").val(Obj.iGRPYT);
    $("#TB_GRPMS").val(Obj.sGRPMS);
    $("#TB_KSSJ").val(Obj.dKSSJ);
    $("#TB_JSSJ").val(Obj.dJSSJ);
    $("#TB_HYZLXMC").val(Obj.sHYZLXMC);
    $("#HF_HYZLXID").val(Obj.iHYZLXID);
    $("#BJ_WXHY").prop("checked", Obj.iBJ_WXHY == "1");
    $("#HF_SRFS").val(Obj.iSRFS);
    //document.getElementById("id").checked
    $("input[name='CB_SRFS']").each(function ()
    {
        if ($(this).val() == Obj.iSRFS)
        {
            $(this).prop("checked", "true");
        }
    });
    $("#TB_GXZQ").val(Obj.iGXZQ);

    $("#HF_JB").val(Obj.iJB);
    $("#S_JB").val(Obj.iJB);
    $("#TB_YXQ").val(Obj.dYXQ);

    $("#HF_BJ_DJT").val(Obj.iBJ_DJT);
    $("input[name='CB_BJ_DJT']").each(function ()
    {
        if ($(this).val() == Obj.iBJ_DJT)
        {
            $(this).prop("checked", "true");
        }
    });
    $("#HF_BJ_YXXG").val(Obj.iBJ_YXXG);

    $("input[name='CB_BJ_YXXG']").each(function ()
    {
        if ($(this).val() == Obj.iBJ_YXXG)
        {
            $(this).prop("checked", "true");
        }
    });

    switch (Obj.iSTATUS)
    {
        case 0:
            $("#LB_STATUS").text("未审核");
            break;
        case 2:
            $("#LB_STATUS").text("已审核");
            break;
        case -1:
            $("#LB_STATUS").text("到期");
            break;
        case -2:
            $("#LB_STATUS").text("已终止");
            break;
    }
    $("#HF_MDID").val(Obj.iMDID);
    $("#TB_MDMC").val(Obj.sMDMC);
    //修改人
    $("#LB_XGRMC").text(Obj.sXGRMC);
    $("#LB_XGRQ").text(Obj.dXGRQ);
    //停用人
    $("#LB_ZZRMC").text(Obj.sZZRMC);
    $("#HF_ZZR").val(Obj.iZZR);
    $("#LB_ZZRQ").text(Obj.dZZRQ);
    //登记人
    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);

    //审核人
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);

    if (Obj.iBJ_YXXG == 1 && Obj.iZXR != "" && Obj.iZZR == "0")
    {
        YXXG = 1;
        ZXRQ = Obj.dZXRQ;
    }
    $("#TB_HYKNAME").val("");
    $("#HF_HYKTYPE").val("");

    //子表数据绑定


    zjlxarr = new Array();
    zylxarr = new Array();
    sexarr = new Array();
    ShowDataWay = new Array();
    gkxxarr = new Array();
    hybqarr = new Array();
    for (i = 0; i < Obj.itemTable.length ; i++)
    {
        var row = Obj.itemTable[i];
        if (row.iSJLX == "1")
        {//绑定记录集中，SJLX为1的数据（卡数据）
            if (Obj.itemTable[i].sSJNR)
            {
                ShowDataWay.push(row)
            }
        }

        else if (row.iSJLX == "2")
        {
            //证件类型
            zjlxarr.push(row.sSJNR);
            //$("[name='CB_ZJLX'][value=" + row.sSJNR + "]").prop("checked", true);
        }
        else if (row.iSJLX == "5")
        {
            //职业类型
            zylxarr.push(row.sSJNR);
            //$("[name='CB_ZYLX'][value=" + row.sSJNR + "]").prop("checked", true);
        }
        else if (row.iSJLX == "4")
        {
            //性别   
            $("[name='CB_SEX'][value=" + row.sSJNR + "]").prop("checked", true);
            //sexarr.push(row.sSJNR);
            //$("input[type='checkbox'][name='CB_SEX']").each(function () {
            //    if ($(this).val() == row.sSJNR) {
            //        $(this).prop("checked", true)
            //            .siblings()
            //            .prop("checked", false);
            //    }
            //});
        }
        else if (row.iSJLX == "20")
        {
            //开卡日期
            $("#TB_KKRQ1").val(row.sSJMC.substring(0, 4) + "-" + row.sSJMC.substring(4, 6) + "-" + row.sSJMC.substring(6));
            $("#TB_KKRQ2").val(row.sSJMC2.substring(0, 4) + "-" + row.sSJMC2.substring(4, 6) + "-" + row.sSJMC2.substring(6));
        }
        else if (row.iSJLX == "3")
        {
            //会员卡类型
            tp_name = $("#TB_HYKNAME").val();
            tp_id = $("#HF_HYKTYPE").val();
            if (tp_name == "")
            {
                $("#TB_HYKNAME").val(row.sSJMC);
                $("#HF_HYKTYPE").val(row.sSJNR);
            }
            else
            {
                $("#TB_HYKNAME").val(tp_name + ";" + row.sSJMC);
                $("#HF_HYKTYPE").val(tp_id + "," + row.sSJNR);
            }
        }
        else if (row.iSJLX == "6")
        {
            //发行单位
            $("#HF_FXDWDM").val(row.sSJDM);
            $("#TB_FXDWMC").val(row.sSJMC);
        }
        else if (row.iSJLX == "23")
        {
            //最后消费日期
            $("#TB_ZHXFRQ1").val(row.sSJMC.substring(0, 4) + "-" + row.sSJMC.substring(4, 6) + "-" + row.sSJMC.substring(6));
            $("#TB_ZHXFRQ2").val(row.sSJMC2.substring(0, 4) + "-" + row.sSJMC2.substring(4, 6) + "-" + row.sSJMC2.substring(6));

        }
        else if (row.iSJLX == "24")
        {
            //积分金额
            $("#TB_JFYE1").val(row.sSJMC);
            $("#TB_JFYE2").val(row.sSJMC2);
        }
        else if (row.iSJLX == "25")
        {
            //升级积分
            $("#TB_SJJF1").val(row.sSJMC);
            $("#TB_SJJF2").val(row.sSJMC2);
        }
        else if (row.iSJLX == "26")
        {
            //消费积分
            $("#TB_XFJF1").val(row.sSJMC);
            $("#TB_XFJF2").val(row.sSJMC2);
        }
        else if (row.iSJLX == "27")
        {
            //门店消费 门店
            $("#HF_MDXF_MD").val(row.sSJDM);
            $("#TB_MDXF_MD").val(row.sSJMC);
        }
        else if (row.iSJLX == "28")
        {
            //门店消费 汇总时间
            $("#TB_MDXF_HZSJ1").val(row.sSJMC);
            $("#TB_MDXF_HZSJ2").val(row.sSJMC2);
        }
        else if (row.iSJLX == "29")
        {
            //门店消费 最后消费时间
            $("#TB_MDXF_ZHXF1").val(row.sSJMC);
            $("#TB_MDXF_ZHXF2").val(row.sSJMC2);
        }
        else if (row.iSJLX == "30")
        {
            //门店消费 消费金额
            $("#TB_MDXF_XFJE1").val(row.sSJMC);
            $("#TB_MDXF_XFJE2").val(row.sSJMC2);
        }
        else if (row.iSJLX == "31")
        {
            //门店消费 来店次数
            $("#TB_MDXF_LDCS1").val(row.sSJMC);
            $("#TB_MDXF_LDCS2").val(row.sSJMC2);
        }
        else if (row.iSJLX == "32")
        {
            //门店消费 卡单价
            $("#TB_MDXF_KDJ1").val(row.sSJMC);
            $("#TB_MDXF_KDJ2").val(row.sSJMC2);
        }
        else if (row.iSJLX == "33")
        {
            //门店消费 消费次数
            $("#TB_MDXF_XFCS1").val(row.sSJMC);
            $("#TB_MDXF_XFCS2").val(row.sSJMC2);
        }
        else if (row.iSJLX == "34")
        {
            //门店消费 消费排名
            $("input[name='CB_MDXF_XFPMQH']").each(function ()
            {
                if ($(this).val() == row.sSJNR)
                {
                    $(this).prop("checked", "true");
                }
            });
            $("#HF_MDXF_XFPMQH").val(row.sSJMC);
            $("#TB_MDXF_XFPM").val(row.sSJMC2);
        }
        else if (row.iSJLX == "35")
        {
            //品类消费 门店
            $("#S_PLXF_MD").val(row.sSJMC);
            //$("#TB_XFJF2").val(row.sSJMC2);
        }
        else if (row.iSJLX == "36")
        {
            //品类消费 部门
            $("#S_PLXF_BM").val(row.sSJMC);
            //$("#TB_XFJF2").val(row.sSJMC2);
        }
        else if (row.iSJLX == "37")
        {
            //品类消费 汇总时间
            $("#TB_PLXF_HZSJ1").val(row.sSJMC);
            $("#TB_PLXF_HZSJ2").val(row.sSJMC2);
        }
        else if (row.iSJLX == "38")
        {
            //商品消费 门店
            $("#S_SPXF_MD").val(row.sSJMC);
            //$("#TB_XFJF2").val(row.sSJMC2);
        }
        else if (row.iSJLX == "39")
        {
            //商品消费 汇总时间
            $("#TB_SPXF_HZSJ1").val(row.sSJMC);
            $("#TB_SPXF_HZSJ2").val(row.sSJMC2);
        }
        else if (row.iSJLX == "40")
        {//绑定记录集中，SJLX为1的数据（卡数据）         
            if (Obj.itemTable[i].sSJNR)
            {
                hybqarr.push(row);
            }
        }
            //待完成
        else if (row.iSJLX == "XXX")
        {
            //会员门店消费 同购商品子表数据
        }
        else if (row.iSJLX == "XXXX")
        {
            //会员品类消费 规则数据
        }
        else if (row.iSJLX == "XXXX")
        {
            //会员商品消费 规则数据
        }

    }
    Set_CYCBL_Item("CB_ZJLX", zjlxarr);
    Set_CYCBL_Item("CB_ZYLX", zylxarr);
    if (status == 0)
    {
        $('#list').datagrid('loadData', ShowDataWay, "json");
        $('#list').datagrid("loaded");
    }
    if (hybqarr.length > 0 && status == 1)
    {
        $('#HYBQList').datagrid('loadData', hybqarr, "json");
        $('#HYBQList').datagrid("loaded");
    }
    //$("#list").jqGrid("clearGridData");
    //var data = {
    //    "page": "1",
    //    "records": ShowDataWay.length,
    //    "rows": ShowDataWay
    //};
    //$("#list")[0].addJSONData(data);

}






//获取子表数据
function SaveItemTable(ItemTable, tablename, cols, sjlx)
{
    var ids = $("#" + tablename).getDataIDs()
    for (i = 0; i < ids.length ; i++)
    {
        var rowData = $("#" + tablename).getRowData(ids[i]);
        var Item_lst = new Object();
        Item_lst = rowData;
        for (var i = 0; i < cols.length; i++)
        {
            Item_lst[cols[i]] = rowData[cols[i]];
        }
        Item_lst.iSJLX = sjlx;
        Item_lst.iGZBH = "0";
        ItemTable.push(Item_lst);
    }


}

//重新
//function SetControlBaseState() {
//    if (sDJRMC == "") {
//        alert("您已离线，请重新登录！");
//    }
//    //此处if有一个修改ZXRQ == ""
//    if (vProcStatus == cPS_MODIFY && $("#HF_ZXR").val() != "" && $("#HF_ZXR").val() != "0" && ZXRQ == "") {
//        vProcStatus = cPS_BROWSE;
//    }
//    var bEditMode = (vProcStatus != cPS_BROWSE);//编辑状态
//    var bExecuted = $("#LB_ZXRMC").text() != "";//已审核
//    var bStarted = $("#LB_QDRMC").text() != "";//已启动
//    var bStopped = $("#LB_ZZRMC").text() != "";//已终止
//    var bHasData = $("#TB_JLBH").val() != "";//有数据

//    document.getElementById("B_Save").disabled = !bEditMode;
//    document.getElementById("B_Insert").disabled = bEditMode || !bCanEdit;
//    document.getElementById("B_Update").disabled = !(!bEditMode && bHasData && !bExecuted) || !bCanEdit;
//    document.getElementById("B_Delete").disabled = document.getElementById("B_Update").disabled; //!(!bEditMode && bHasData && !bExecuted);
//    document.getElementById("B_Cancel").disabled = !bEditMode;
//    document.getElementById("B_Exec").disabled = !(!bEditMode && bHasData && !bExecuted) || !bCanExec;
//    document.getElementById("TB_JLBH").disabled = bEditMode;
//    //取消审核启动终止按钮控制
//    //取消审核=审核
//    document.getElementById("B_UnExec").disabled = document.getElementById("B_Exec").disabled;
//    //启动=有数据 and 已审核 and 未终止
//    document.getElementById("B_Start").disabled = !(bHasData && bExecuted && !bStopped);
//    //终止=有数据 and 已启动
//    document.getElementById("B_Stop").disabled = !(bHasData && bStarted);
//    PageControl(bEditMode, false);
//    document.getElementById("TB_JLBH").disabled = bEditMode;
//    $("#B_Insert").hide();
//    SetControlState();
//};



function CustomerOpenDialogReturn(rows, listName, lst, array, CheckFieldId)
{
    if (listName == "list")
    {
        for (var i = 0; i < lst.length; i++)
        {
            if (CheckReapet(array, CheckFieldId, lst[i]))
            {
                $('#list').datagrid('appendRow', {
                    sSJNR: lst[i].iHYID,
                    sSJDM: lst[i].sHYK_NO,
                    sSJMC: lst[i].sHYKNAME,
                    sSJMC2: lst[i].sHY_NAME,
                });
            }
        }
    }
    if (listName == "list_gkxx")
    {
        for (var i = 0; i < lst.length; i++)
        {
            if (CheckReapet(array, CheckFieldId, lst[i]))
            {
                $('#list_gkxx').datagrid('appendRow', {
                    iGKID: lst[i].iJLBH,
                    sGK_NAME: lst[i].sHY_NAME,
                    iSEX: lst[i].iSEX,
                    sSJHM: lst[i].sSJHM,
                });
            }
        }
    }
}

function CheckBox(cbname, hfname)
{
    $("input[type='checkbox'][name='" + cbname + "']").click(function ()
    {
        if (this.checked)
        {
            $(this).prop("checked", this.checked).siblings().prop("checked", !this.checked);
            $("#" + hfname).val($(this).val());
        }
        else
        {
            $(this).prop("checked", this.checked);
            $("#" + hfname).val("");
        }
    });

}

function Set_CYCBL_Item(cbl_name, CBLHYBJ)
{
    if (CBLHYBJ != null)
    {
        var s1 = $("input[name^='" + cbl_name + "']").length;
        for (var i = 0; i <= s1 - 1; i++)
        {
            for (var j = 0; j <= CBLHYBJ.length - 1; j++)
            {
                if ($("#" + cbl_name + "_" + i).val() == CBLHYBJ[j])
                {
                    $("#" + cbl_name + "_" + i).prop("checked", true);
                }
            }
        }

    }
}

function zTreeNoCheck()
{
    var treeObj = $.fn.zTree.getZTreeObj("TreeHYBQ");
    var nodes = treeObj.getNodes();
    if (nodes.length > 0)
    {
        for (var i = 0; i < nodes.length; i++)
        {
            var node = nodes[i];
            node.chkDisabled = true;
            zTree.updateNode(node);
        }
    }
}

function zTreeOnChecking(event, treeId, treeNode)
{
    // alert(treeNode.id + ", " + treeNode.pId + "," + treeNode.name);
    if (treeNode.checked == true && treeNode.isParent != true)
    {
        if (treeNode.id.substring(0, 2) == "CC")
        {
            if (checkReapet(treeNode.id, treeNode.pId) == true)
            {

                $('#HYBQList').datagrid('appendRow', {
                    sSJMC: treeNode.getParentNode().name,//.split(" ")[1],
                    sSJNR2: treeNode.pId.substring(2, treeNode.pId.length),
                    sSJMC2: treeNode.name,//.split(" ")[1],
                    //sSJNR: treeNode.uid.substring(2, treeNode.id.length)
                    sSJNR: treeNode.uid,
                });
            }
        }
        else
        {
            var treeObj = $.fn.zTree.getZTreeObj("TreeHYBQ");
            treeObj.checkNode(treeNode, false, true);
            treeNode.chkDisabled = true;
        }
    }
    //else {
    //    var rowIDs = $("#HYBQList").getDataIDs();
    //    if (rowIDs.length > 0) {
    //        for (var i = 0; i < rowIDs.length; i++) {
    //            var rowData = $("#HYBQList").getRowData(rowIDs[i]);
    //            if (rowData.sSJNR == treeNode.pId.substring(2, treeNode.pId.length) && rowData.sSJNR2 == treeNode.id.substring(2, treeNode.id.length)) {
    //                $("#HYBQList").delRowData(rowIDs[i]);
    //            }
    //        }
    //    }
    //}
};

function checkReapet(id, pid)
{
    var boolInsert = true;
    var rowIDs = $("#HYBQList").datagrid("getData").rows;
    if (rowIDs.length > 0)
    {
        for (var i = 0; i < rowIDs.length; i++)
        {
            var rowData = rowIDs[i];
            if (rowData.sSJNR == id.substring(2, id.length) && rowData.sSJNR2 == pid.substring(2, pid.length))
            {
                ShowMessage( "已添加该标签" );
                boolInsert = false;
            }
        }
    }
    return boolInsert;
}

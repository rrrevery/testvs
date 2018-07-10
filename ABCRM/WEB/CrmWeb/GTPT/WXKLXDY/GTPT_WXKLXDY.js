vUrl = "../GTPT.ashx";
sendToWX = false;//是否需要发送消息到微信
var selectId = -1;

function InitGrid()
{
    vColumnNames = ["名称", "地址", ];
    vColumnModel = [
			{ name: "sNAME", width: 80, },
			{ name: "sURL", width: 120, },
    ];
};

$(document).ready(function ()
{
    bNeedItemData = false;
    $("#custom").hide();
    $("#selectPublicID").combobox({
        onSelect: function (record)
        {
            iWXPID = record.value;
            sWXPIF = record.pif;
        }
    });
    $.parser.parse("#WXPublicID");
    FillPublicID($("#selectPublicID"));

    $("#AddItem").click(function ()
    {
        if ($("#TB_DJBH").val() == "")
        {
            ShowMessage("请输入等级编号", 3);
            return;
        }
        if ($("#TB_JCMC").val() == "")
        {
            ShowMessage("请输入级次名称", 3);
            return;
        }
        var rows = $("#list").datagrid("getRows");
        if (rows.length >= 3)
        {
            ShowMessage("最多只能添加三项");
            return;
        }
        else
        {
            $('#list').datagrid('appendRow', {
                sNAME: $("#TB_NAME").val(),
                sURL: $("#TB_URL").val(),
            });
            $("#TB_NAME").val("");
            $("#TB_URL").val("");
        }
    });
    $("#DelItem").click(function ()
    {
        DeleteRows("list");
    });


    $('#upload_logo').click(function ()
    {
        if (!/\.(bmp|png|jpeg|jpg)$/.test($("#file_logo").val()))
        {
            ShowMessage("图片类型必须是.bmp,jpeg,jpg,png中的一种")
            return;
        }
        UploadLogoToWXServer("form1", "HF_LOGO");
    });
    $('#upload_background').click(function ()
    {
        if (!/\.(bmp|png|jpeg|jpg)$/.test($("#file_background").val()))
        {
            ShowMessage("图片类型必须是.bmp,jpeg,jpg,png中的一种")
            return;
        }
        UploadLogoToWXServer("form2", "HF_BACKGROUND");
    });
    $('input[name="PAY"]').change(function ()
    {
        if ($("[name='PAY']:checked").val() == 1)
        {
            $("#custom").hide();
            $("#TB_CENTER_URL_NAME").val("");
            $("#TB_CENTER_URL_SUBNAME").val("");
            $("#TB_CENTER_URL").val("");
        }
        else
            $("#custom").show();

    });
    $('input[name="AC"]').change(function ()
    {
        if ($("[name='AC']:checked").val() == 1)
        {
            $("#old").hide();
            $("#TB_BINDNAME").val("");
            $("#TB_BINDURL").val("");
        }
        else
            $("#old").show();

    });
    $("input[type='checkbox'][name='required']").click(function ()
    {
        if (this.checked)
        {
            $("[name='optional'][value='" + $(this).val() + "']").prop("checked", false);
        }

    });
    $("input[type='checkbox'][name='optional']").click(function ()
    {
        if (this.checked)
        {
            $("[name='required'][value='" + $(this).val() + "']").prop("checked", false);
        }

    });
});

function UploadLogoToWXServer(formID, tbName)
{

    $("#" + formID).ajaxSubmit({
        url: '../UpLoadLogoToWXServer.ashx?PUBLICIF=' + sWXPIF,
        dataType: "json",
        success: function (data)
        {
            if (data.errCode == 0)
            {
                ShowMessage("上传成功！");
                $("#" + tbName).val(data.result);
            } else
            {
                ShowMessage(data.errMessage);
            }
        },
        error: function (data)
        {
            ShowMessage("失败:" + data.responsetext);
        }
    });

}


function IsValidData()
{
    if ($("#TB_BRANDNAME").val() == "")
    {
        ShowMessage("请输入商户名称", 3);
        return false;
    }
    if ($("#TB_TITLE").val() == "")
    {
        ShowMessage("请输入会员卡名称", 3);
        return false;
    }
    if ($("#TB_ACTIVATE_URL").val() == "")
    {
        ShowMessage("请输入激活地址", 3);
        return false;
    }
    if ($("#HF_LOGO").val() == "")
    {
        ShowMessage("请上传LOGO图片", 3);
        return false;
    }
    if ($("#HF_BACKGROUND").val() == "")
    {
        ShowMessage("请上传卡片背景图片", 3);
        return false;
    }
    if ($("#TB_SL").val() == "")
    {
        ShowMessage("请填写库存数量", 3);
        return false;
    }
    if ($("#TB_DESCRIPTION").val() == "")
    {
        ShowMessage("请填写卡使用说明", 3);
        return false;
    }
    if ($("#TB_PREROGATIVE").val() == "")
    {
        ShowMessage("请填写卡特权说明", 3);
        return false;
    }
    var string = Get_CYCBL_CheckItem("required")||"";
    if (string == "")
    {
        ShowMessage("请至少选择一项必填项", 3);
        return false;
    }

    return true;
}


function SaveData()
{
    var Obj = new Object();
    Obj.iJLBH = $("#TB_JLBH").val();
    if (Obj.iJLBH == "")
        Obj.iJLBH = "0";
    Obj.sBRANDNAME = $("#TB_BRANDNAME").val();
    Obj.sTITLE = $("#TB_TITLE").val();
    Obj.sNOTICE = $("#TB_NOTICE").val();
    Obj.sPHONE = $("#TB_PHONE").val();
    Obj.sACTIVATE_URL = $("#TB_ACTIVATE_URL").val();
    Obj.sDESCRIPTION = $("#TB_DESCRIPTION").val();
    Obj.sPREROGATIVE = $("#TB_PREROGATIVE").val();
    Obj.sCUSTOM_URL_NAME = $("#TB_CUSTOM_URL_NAME").val();
    Obj.sCUSTOM_URL_SUBNAME = $("#TB_CUSTOM_URL_SUBNAME").val();
    Obj.sCUSTOM_URL = $("#TB_CUSTOM_URL").val();
    Obj.sCENTER_URL_NAME = $("#TB_CENTER_URL_NAME").val();
    Obj.sCENTER_URL_SUBNAME = $("#TB_CENTER_URL_SUBNAME").val();
    Obj.sCENTER_URL = $("#TB_CENTER_URL").val();
    Obj.sPROMOTION_NAME = $("#TB_PROMOTION_NAME").val();
    Obj.sPROMOTION_SUBNAME = $("#TB_PROMOTION_SUBNAME").val();
    Obj.sPROMOTION_URL = $("#TB_PROMOTION_URL").val();
    Obj.sLOGO = $("#HF_LOGO").val();
    Obj.sBACKGROUND = $("#HF_BACKGROUND").val();
    Obj.iSL = $("#TB_SL").val();
    Obj.iBJ_PAY = $("[name='PAY']:checked").val();
    Obj.iBJ_ACTIVATE = $("[name='AC']:checked").val();

    Obj.iBJ_SHOW = $("#C_SHOW").prop("checked") ? 1 : 0;
    Obj.sBINDNAME = $("#TB_BINDNAME").val();
    Obj.sBINDURL = $("#TB_BINDURL").val();
    var lst = new Array();
    lst = $("#list").datagrid("getData").rows;
    Obj.itemTable = lst;
    Obj.iLoginPUBLICID = iWXPID;
    Obj.iLoginRYID = iDJR;
    Obj.sLoginRYMC = sDJRMC;
    Obj.sREQUIREDXX = Get_CYCBL_CheckItem("required");
    Obj.sOPTIONALXX = Get_CYCBL_CheckItem("optional");
    Obj.iCODETYPE = $("[name='codetype']:checked").val();
    return Obj;
}

function ShowData(data)
{
    var Obj = JSON.parse(data);
    $("#TB_JLBH").val( Obj.iJLBH);
    $("#TB_BRANDNAME").val(Obj.sBRANDNAME);
    $("#TB_TITLE").val(Obj.sTITLE);
    $("#TB_NOTICE").val(Obj.sNOTICE);
    $("#TB_PHONE").val(Obj.sPHONE);
    $("#TB_SL").val(Obj.iSL);
    $("#TB_ACTIVATE_URL").val(Obj.sACTIVATE_URL);
    $("#TB_DESCRIPTION").val(Obj.sDESCRIPTION);
    $("#TB_PREROGATIVE").val(Obj.sPREROGATIVE);
    $("#TB_CUSTOM_URL_NAME").val(Obj.sCUSTOM_URL_NAME);
    $("#TB_CUSTOM_URL_SUBNAME").val(Obj.sCUSTOM_URL_SUBNAME);
    $("#TB_CUSTOM_URL").val(Obj.sCUSTOM_URL);
    $("#TB_PROMOTION_NAME").val(Obj.sPROMOTION_NAME);
    $("#TB_PROMOTION_SUBNAME").val(Obj.sPROMOTION_SUBNAME);
    $("#TB_PROMOTION_URL").val(Obj.sPROMOTION_URL);
    $("#TB_CENTER_URL_NAME").val(Obj.sCENTER_URL_NAME);
    $("#TB_CENTER_URL_SUBNAME").val(Obj.sCENTER_URL_SUBNAME);
    $("#TB_CENTER_URL").val(Obj.sCENTER_URL);
    $("#HF_LOGO").val(Obj.sLOGO);
    $("#HF_BACKGROUND").val(Obj.sBACKGROUND);
    $("#LB_CARDID").text(Obj.sCARDID);
    $('#list').datagrid('loadData', Obj.itemTable, "json");
    $('#list').datagrid("loaded");
    $("#LB_DJRMC").text(Obj.sDJRMC);
    $("#HF_DJR").val(Obj.iDJR);
    $("#LB_DJSJ").text(Obj.dDJSJ);
    $("#LB_ZXRMC").text(Obj.sZXRMC);
    $("#HF_ZXR").val(Obj.iZXR);
    $("#LB_ZXRQ").text(Obj.dZXRQ);
    $("#TB_BINDNAME").val(Obj.sBINDNAME);
    $("#TB_BINDURL").val(Obj.sBINDURL);
    $("#selectPublicID").combobox("setValue", Obj.iPUBLICID)
    $("[name='PAY'][value='" + Obj.iBJ_PAY + "']").prop("checked", true);
    $("[name='AC'][value='" + Obj.iBJ_ACTIVATE + "']").prop("checked", true);
    if (Obj.iBJ_PAY == 0)
        $("#custom").show();
    else
        $("#custom").hide();
    if (Obj.iBJ_ACTIVATE == 0)
        $("#old").show();
    else
        $("#old").hide();
    $("#C_SHOW").prop("checked", Obj.iBJ_SHOW == 1 ? true : false);
    Set_CYCBL_Item("required", Obj.sREQUIREDXX);
    Set_CYCBL_Item("optional", Obj.sOPTIONALXX);
    $("[name='codetype'][value='" + Obj.iCODETYPE + "']").prop("checked", true);
}

function Get_CYCBL_CheckItem(cbl_name)
{
    var valuelist = "";
    $("input[name^='" + cbl_name + "']").each(function ()
    {
        if (this.checked)
        {
            valuelist += $(this).val() + ";";
        }
    });
    if (valuelist.length > 0)
    {
        valuelist = valuelist.substring(0, valuelist.length - 1);
    }
    return valuelist;
}

function Set_CYCBL_Item(cbl_name, CBLHYBJ)
{
    if (CBLHYBJ != null)
    {

        var splitArray = CBLHYBJ.split(";");
        var s1 = $("input[name^='" + cbl_name + "']").length;
        for (var i = 0; i <= s1 - 1; i++)
        {
            for (var j = 0; j <= splitArray.length - 1; j++)
            {
                if ($("#" + cbl_name + "_" + i).val() == splitArray[j])
                {
                    $("#" + cbl_name + "_" + i).prop("checked", true);
                }
            }
        }

    }
}

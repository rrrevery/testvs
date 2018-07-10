vUrl = "../MZKGL.ashx";
function InitGrid()
{
    vColumnNames = ['记录编号', '客户名称', 'KHID', '联系人姓名', '联系人电话', '联系人手机', 'BGDDDM', '保管地点名称', '售卡数量', '应收金额', '未付款金额', '已付款金额', '状态', '审核时期', '启动时间'];
    vColumnModel = [
		   { name: 'iJLBH', width: 80, },
            { name: 'sKHMC', width: 120, },
			{ name: 'iKHID', hidden: true, },
			{ name: 'sLXRXM', hidden: false },
			{ name: 'sLXRDH', width: 120, },
            { name: 'sLXRSJ', width: 80, },
			{ name: 'sBGDDDM', width: 80, hidden: true },
            { name: 'sBGDDMC', width: 80, },
            { name: 'iSKSL', width: 80, },
            { name: 'fYSZE', width: 80, },
            { name: 'fWFKJE', width: 80, },
            { name: 'fYFKJE', width: 80, },
            {
                name: 'iSTATUS', formatter: function (cellvalues)
                {
                    if (cellvalues == 0)
                        return "未审核";
                    if (cellvalues == 1)
                        return "已审核"
                    if (cellvalues == 2)
                        return "已启动";

                }
            },
           { name: 'dSHRQ', width: 120, },
           { name: 'dQDSJ', width: 120, },
    ];
}
$(document).ready(function ()
{
    CheckBox("CB_STATUS", "HF_STATUS");
    $("#B_Insert").hide();
    $("#B_Delete").hide();
    $("#B_Update").hide();
    $("#B_Exec").hide();
    $("#status-bar").hide();

    $("#TB_BGDDDM").click(function ()
    {
        SelectBGDD("TB_BGDDDM", "HF_BGDDDM", "zHF_BGDDDM", false);
    });

    $("#TB_DJRMC").click(function ()
    {
        SelectRYXX("TB_DJRMC", "HF_DJR");
    });
    $("#TB_ZXRMC").click(function ()
    {
        SelectRYXX("TB_ZXRMC", "HF_ZXR");
    });

    $("#TB_HYKNAME").click(function ()
    {
        var condData = new Object();
        condData["vCZK"] = 1;
        SelectKLX("TB_HYKNAME", "HF_HYKTYPE", "zHF_HYKTYPE", false, condData);
    });

    $("#TB_KHMC").click(function ()
    {
        SelectKH("TB_KHMC", "HF_KHID", "zHF_KHDM", false);
    })
    RefreshButtonSep();
});


function MakeSearchCondition()
{
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "HF_STATUS", "iSTATUS", "=", true);
    if ($("[name='RD_FKQK']:checked").val() == 0)
        MakeSrchCondition2(arrayObj, 0, "iBJ_QD", "=", false);
    if ($("[name='RD_FKQK']:checked").val() == 1)
        MakeSrchCondition2(arrayObj, 1, "iBJ_QD", "=", false);
    MakeSrchCondition(arrayObj, "HF_KHID", "iKHID", "=", false);
    MakeSrchCondition(arrayObj, "HF_BGDDDM", "sBGDDDM", "in", false);
    MakeSrchCondition(arrayObj, "HF_HYKTYPE", "iHYKTYPE", "in", false);
    MakeSrchCondition(arrayObj, "TB_DJSJ1", "dDJSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_DJSJ2", "dDJSJ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ1", "dZXRQ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_ZXRQ2", "dZXRQ", "<=", true);
    MakeSrchCondition(arrayObj, "TB_QDSJ1", "dQDSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_QDSJ2", "dQDSJ", ">=", true);
    MakeSrchCondition(arrayObj, "TB_JLBH", "iJLBH", "=", false);

    return arrayObj;

};

//复选框控制
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
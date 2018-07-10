vUrl = "../GTPT.ashx";

vCaption = "微信首页导航定义";

function InitGrid() {
    vColumnNames = ['导航ID', '导航名称', '停用标记', '导航类型'],
    vColumnModel = [
        { name: 'iJLBH', index: 'iJLBH', width: 80, },
		{ name: 'iNAME', width: 120, },
        {
            name: 'iTY', width: 120,
            formatter: function (cellvalues) {
                if (cellvalues == 0)
                    return "否";
                if (cellvalues == 1)
                    return "是";
            },
        },
        {
            name: 'iLX', width: 120,
            formatter: function (cellvalues) {
                if (cellvalues == 0)
                    return "首页导航";
                if (cellvalues == 1)
                    return "会员区导航";
            },
        },
    ]

}
$(document).ready(function () {
    $("#B_Insert").hide();
    document.getElementById("B_Update").onclick = function () {
        var str = GetSydhdy1(vJLBH);
        var data = JSON.parse(str);
        var gzid = data.iID;
        if (gzid > 0) {
            ShowYesNoMessage("此导航在已启动的子导航定义单中被调用，如要修改将重新启动定义单，是否继续修改？", function () {
                {
                    var sUpdateCurrentPath = "";
                    sUpdateCurrentPath = sCurrentPath + "?action=edit&jlbh=" + vJLBH;
                    sUpdateCurrentPath = ConbinPath(sUpdateCurrentPath);
                    MakeNewTab(sUpdateCurrentPath, vCaption, vPageMsgID);
                }
            });

        }
        else {

            SetControlBaseState();
            document.getElementById("B_Delete").disabled = true;
            document.getElementById("B_Update").disabled = true;
        }

    }

    BFButtonClick("TB_DJRMC", function () {
        SelectRYXX("TB_DJRMC", "HF_DJR", "zHF_DJR", false);
    });
    BFButtonClick("TB_ZXRMC", function () {
        SelectRYXX("TB_ZXRMC", "HF_ZXR", "zHF_ZXR", false);
    });
    CheckBox("CB_TY", "DH_DHTY");
    RefreshButtonSep();
})





function MakeSearchCondition() {
    var arrayObj = new Array();
    MakeSrchCondition(arrayObj, "TB_DHID", "iJLBH", "=", true);
    MakeSrchCondition(arrayObj, "TB_DHMC", "iNAME", "=", true);
    MakeSrchCondition(arrayObj, "DH_DHTY", "iTY", "=", true);
    MakeSrchCondition(arrayObj, "DDL_DHLX", "iLX", "=", true);
    MakeMoreSrchCondition(arrayObj);//

    return arrayObj;
};

//复选框控制
function CheckBox(cbname, hfname) {
    $("input[type='checkbox'][name='" + cbname + "']").click(function () {
        if (this.checked) {
            $(this).prop("checked", this.checked).siblings().prop("checked", !this.checked);
            $("#" + hfname).val($(this).val());
        }
        else {
            $(this).prop("checked", this.checked);
            $("#" + hfname).val("");
        }
    });

}

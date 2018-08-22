/**
* @description 此文件的方法用来将数据访问层对象显示到 html上
*/



/**
* @description 根据订单ID从后台获取 订单对象
* @param {int} ids 订单ID
*/ 
function requestObjectByID(ids){//根据ID获取对象
    $.ajax({
        type: "get",
        dataType: "json",
        url: "getCrmBill.ashx?departmentid=" + ids + "&time=" + new Date(),
        beforeSend: function (XMLHttpRequest) {

        },
        success: function (data, textStatus) {
            var CrmPromBaseBill = data;
            setHtmlInfoByCrmPromBaseBill(CrmPromBaseBill); //将单据的基础内容显示到页面
            subBillToHtml(CrmPromBaseBill); //將分單内容显示到页面
            if (modifyDisable) {
                setControlDisable();
            }
            if (initmodifyType==0)
                $("#BillId").val(00000000);
        },
        complete: function (XMLHttpRequest, textStatus) {
        },
        error: function () {
        }
    });
}

/**
* @description 根据订单ID从后台获取 订单对象 此方法在添加完毕之后重新读取时调用
* @param {int} id 订单ID
*/
//保存完毕后重新读取
function readAfterSub(id) {
    $.ajax({
        type: "get",
        dataType: "json",
        url: "getCrmBill.ashx?departmentid=" + id + "&time=" + new Date(),
        beforeSend: function (XMLHttpRequest) {
        },
        success: function (data, textStatus) {
            var CrmPromBaseBill = data;
            if (CrmPromBaseBill != null) {
                setHtmlInfoByCrmPromBaseBill(CrmPromBaseBill);
            }
            setControlDisable();
        },
        complete: function (XMLHttpRequest, textStatus) {
        },
        error: function () {
            setControlDisable();
        }
    });
}
/**
* @description 将对象显示在页面中
* @param {object} CrmPromBaseBill  需要显示的订单对象
*/
//将单据显示到html中
function setHtmlInfoByCrmPromBaseBill(CrmPromBaseBill) {
    if (CrmPromBaseBill == null)
        return;
    $("#CrmVipCentBill").attr("BaseDiscRate", CrmPromBaseBill.BaseDiscRate);
    $("#CrmVipCentBill").attr("CanCentMultiple", CrmPromBaseBill.CanCentMultiple);
    $("#BillId").val(CrmPromBaseBill.BillId);
    if (CrmPromBaseBill.Status == 0)
        $("#status").html("未审核");
    if (CrmPromBaseBill.Status == 1)
        $("#status").html("审核中");
    if (CrmPromBaseBill.Status == 2)
        $("#status").html("已审核");
    if (CrmPromBaseBill.Status == 3)
        $("#status").html("已启动");
    if (CrmPromBaseBill.Status == 4)
        $("#status").html("已终止");
    $("#status").attr("code", CrmPromBaseBill.Status);
    holdStatus = $("#status").html();
    holdStatusCode = CrmPromBaseBill.Status;
    $("#CrmVipCentBill").attr("DeptId", CrmPromBaseBill.DeptId);
    $("#CrmVipCentBill").attr("PromId", CrmPromBaseBill.PromId);
    $("#departmentId").val(CrmPromBaseBill.DeptCode);
    $("#dateBegin").val(CrmPromBaseBill.BeginTime);
    $("#dateEnd").val(CrmPromBaseBill.EndTime);
    $("#BusinessName").val(CrmPromBaseBill.CompanyId); //商户code

    var HykTypes = CrmPromBaseBill.HykTypes; //选择会员卡类型
    for (var i = 0; i < HykTypes.length; i++) {
        var cardType = HykTypes[i].HykType;
        $("input[value='" + cardType + "']").attr("checked", true);
    }
    $("#EnterUser").attr("EnterUserId", CrmPromBaseBill.CreateUserId);
    if (CrmPromBaseBill.CreateTime == "" || CrmPromBaseBill.CreateTime == null || CrmPromBaseBill.CreateTime.indexOf('0001-01-01') >= 0) {
        setStatusLine("Enter", "0", "");
    }
    else {
        setStatusLine("Enter", CrmPromBaseBill.CreateName, CrmPromBaseBill.CreateTime);
    }

    $("#CheckedUser").attr("CheckedUserId", CrmPromBaseBill.CheckUserId);
    if (CrmPromBaseBill.CheckTime == "" || CrmPromBaseBill.CheckTime == null || CrmPromBaseBill.CheckTime.indexOf('0001-01-01') >= 0) {
        
        setStatusLine("Checked", "0", "");
    }
    else {
        setStatusLine("Checked", CrmPromBaseBill.CheckedName, CrmPromBaseBill.CheckedTime);
    }

    $("#ActionUser").attr("ActionUserId", CrmPromBaseBill.StartUserId);
    if (CrmPromBaseBill.StartTime == "" || CrmPromBaseBill.StartTime == null || CrmPromBaseBill.StartTime.indexOf('0001-01-01') >= 0) {
        setStatusLine("Action", "0", "");
    }
    else {
        setStatusLine("Action", CrmPromBaseBill.StartUserName, CrmPromBaseBill.StartTime);
    }

    $("#StopUser").attr("StopUserId", CrmPromBaseBill.StopUserId);
    if (CrmPromBaseBill.StopTime == "" || CrmPromBaseBill.StopTime == null || CrmPromBaseBill.StopTime.indexOf('0001-01-01') >= 0) {
        setStatusLine("Stop", "0", "");
    }
    else {
        setStatusLine("Stop", CrmPromBaseBill.StopUserName, CrmPromBaseBill.StopTime);
    }
}

/**
* @description 显示状态信息 （时间，人员，状态）
* @param {string} prefix     控件的ID前缀
* @param {string} userValue  人员名称
* @param {string} timeValue  时间
* @example
* setStatusLine("Stop", CrmPromBaseBill.StopUserName, CrmPromBaseBill.StopTime);
*/
function setStatusLine(prefix,userValue,timeValue) {
    var user = prefix + "User";
    var time = prefix + "Time";
    $("#" + user).html(userValue);
    $("#" + time).html(timeValue);
}

/**
* @description 将订单的分单显示在页面中
* @param {object} CrmPromBaseBill  需要显示的订单对象
*/
//分单显示到页面
function subBillToHtml(CrmPromBaseBill) {
    //分单数组
    var crmPromBaseSubBillArray = CrmPromBaseBill.SubBills;

    for (var i = 0; i < crmPromBaseSubBillArray.length; i++) {
        //创建分单tab  创建tab中的表格。
        var currentNewGridId = CreatNewTab();
        var active = tabs.tabs("option", "active");
        $("#Fendan li:eq(" + active + ")").attr("benginTime", crmPromBaseSubBillArray[i].BeginTime);
        $("#Fendan li:eq(" + active + ")").attr("endTime", crmPromBaseSubBillArray[i].EndTime);
        $("#Fendan li:eq(" + active + ")").attr("Period", crmPromBaseSubBillArray[i].Period);
        $("#Fendan li:eq(" + active + ")").attr("Inx", crmPromBaseSubBillArray[i].Inx);
        $("#subBillBeginTime"+(i + 1)).val(crmPromBaseSubBillArray[i].BeginTime);
        $("#subBillEndTime" + (i + 1)).val(crmPromBaseSubBillArray[i].EndTime);
        //表格生成对象
        bindGrid(currentNewGridId);
        //表格数据数组
        var crmPromBaseBillItemArray = crmPromBaseSubBillArray[i].Items;
        //把数据写到表格中去。
        if (crmPromBaseBillItemArray.length > 0) {
            for (var j = 0; j < crmPromBaseBillItemArray.length; j++) {
                var strNotes = JSON.stringify(crmPromBaseBillItemArray[j].crmpromConditions);
                jQuery("#" + currentNewGridId).jqGrid('addRowData', j , crmPromBaseBillItemArray[j]);
                jQuery("#" + currentNewGridId).setCell(j , 7, strNotes);
                jQuery("#" + currentNewGridId).setCell(j , 0, j );
            }
        } else {
            $("#" + currentNewGridId).addRowData(1, { Inx: "1", RuleId: "" + 1, BaseCent: "1", CentMultiple: "1", CentShareRate: "1", CentMoneyMultipleRuleId: "1", IsJoined: "true", CrmPromConditions: "" }, "last");
        }

    }
}

/**
* @description 点击添加时，将页面所有的元素重新设置
* zy changed
*/
function reAdd() {
//分单号
    $("#BillId").val(00000000);
    //状态
    $("#status").html("未审核");
    $("#status").attr("code", 0);
    holdStatus = $("#status").html();
    holdStatusCode = 0;
    setStatusLine("Stop", "0", "");
    setStatusLine("Action", "0", "");
    setStatusLine("Enter", "0", ""); 
    setStatusLine("Checked", "0", "");
    //卡类型
    $("input[name='cardType']").each(function () {
        $(this).attr("checked", false);;
    });
    //促销时间
    $("#dateBegin").val("");
    $("#dateEnd").val("");

    //重新加载分单
    var count = $("#Fendan li").length;
    for (var i = 0; i < count; i++) {
        $("#Fendan li:eq(" + 0 + ")").remove();
        var aa = $("#Fendan li:eq(" + 0 + ")").attr("aria-controls")
        $("#" + aa).remove();
    }
    var currentTabsGrid = CreatNewTab();
    bindGrid(currentTabsGrid);
    $("#" + currentTabsGrid).addRowData(1, { Inx: "1", RuleId: "" + 1, BaseCent: "1", CentMultiple: "1", CentShareRate: "1", CentMoneyMultipleRuleId: "1", IsJoined: "true", CrmPromConditions: "" }, "last");
    tabs.tabs("refresh");
}
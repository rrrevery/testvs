var settingCheck = {
    check: {
        enable: true,
        autoCheckTrigger: true
    },
    view: {
        showIcon: false
    },
    callback: {
        onCheck: zTreeOnCheck,
        beforeCheck: beforeCheck
    },
    data: {
        simpleData: {
            enable: true
        }
    },
};

function beforeCheck(treeId, treeNode) {
    return beforeCheckCustomer(treeId, treeNode);
};

function beforeCheckCustomer(treeId, treeNode) {
    return true;
}

function zTreeOnCheck(event, treeId, treeNode) {
    zTreeOnChecking(event, treeId, treeNode);
};

function zTreeOnChecking(event, treeId, treeNode) {
    ;
}

function FillCheckTree(treename, func, personid) {
    PostToCrmlib(func, { iRYID: personid, iID: iDJR }, function (data) {
        var zNodes = "[";
        for (var i = 0; i < data.length; i++) {
            zNodes = zNodes + "{id:'" + data[i].sDM + "',pId:'" + ((data[i].sPDM == "") ? "0" : data[i].sPDM)
                + "',name:\"" + data[i].sQC + "\",data:'" + data[i].iID + "',checked:" + data[i].bChecked
                + ",rootId:'" + data[i].sGDM + "',uid:'" + data[i].iUID + "',halfCheck:false}";
            if (i < data.length - 1)
                zNodes = zNodes + ",";
        }
        zNodes = zNodes + "]";
        //zNodes = "[{id:1,pID:0,name:'aaaa'}]";
        $.fn.zTree.init($("#" + treename), settingCheck, data);
    });
    //var url = "../../CrmLib/CrmLib.ashx?func=" + func + "&personid=" + personid;
    //$.ajax({
    //    async: true,
    //    type: 'post',
    //    url: url,
    //    dataType: "json",
    //    success: function (data) {
    //        var zNodes = "[";
    //        for (var i = 0; i < data.length; i++) {
    //            zNodes = zNodes + "{id:'" + data[i].sDM + "',pId:'" + ((data[i].sPDM == "") ? "0" : data[i].sPDM)
    //                + "',name:\"" + data[i].sQC + "\",data:'" + data[i].iID + "',checked:" + data[i].bChecked
    //                + ",rootId:'" + data[i].sGDM + "',uid:'" + data[i].iUID + "',halfCheck:false}";
    //            if (i < data.length - 1)
    //                zNodes = zNodes + ",";
    //        }
    //        zNodes = zNodes + "]";
    //        //zNodes = "[{id:1,pID:0,name:'aaaa'}]";
    //        $.fn.zTree.init($("#" + treename), settingCheck, eval(zNodes));
    //    },
    //    error:function (data) {
    //        ShowMessage("数据错误");
    //    },
    //    callback: function () {
    //        alter("加载完成");
    //    }
    //});
}
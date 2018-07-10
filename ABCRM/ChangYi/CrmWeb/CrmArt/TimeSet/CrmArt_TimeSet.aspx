<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CrmArt_TimeSet.aspx.cs" Inherits="BF.CrmWeb.CrmArt.TimeSet.CrmArt_TimeSet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />
    <title>LGrid</title>
    <%=V_Head_WebArt%>
    <link href="CrmArt_TimeSet.css" rel="stylesheet" />
    <script src="../../../js/LJS.js" type="text/javascript"></script>
    <script src="../../../Js/LjsUIGrid.js" type="text/javascript"></script>
</head>
<body style="height: auto; overflow-y: hidden">
    <div id="MainPanel" class="bfbox zero_padding">
        <%=V_ArtToolBar%>
        <div id="dataList" style="width: 100%"></div>
        <div class="bfrow" style="margin-top: 15px; text-align: center">
            <button class="bfbut bfblue" onclick='return btnYes_onclick()' id="btn_yes">参加活动&nbsp</button>
            <button class="bfbut bfblue" onclick='return btnNo_onclick()' id="btn_no">不参加活动</button>
        </div>
    </div>
</body>

<script language="javascript" type="text/javascript">
    $(document).ready(function () {
        if ($.dialog.data('SetDialogTimeEnable') != undefined) {
            Enable = $.dialog.data('SetDialogTimeEnable');
            if (Enable == false) {
                $("#B_Save").attr("disabled", true);
                $("#btn_yes").attr("disabled", true);
                $("#btn_no").attr("disabled", true);

            }
            else {
                $("#B_Save").attr("disabled", false);
                $("#btn_yes").attr("disabled", false);
                $("#btn_no").attr("disabled", false);
            }
        };
    });


    var TabInput;
    LJS.root("js")
    var grid_ds = {
        Values: {
            0: { 1: "0", 2: ".", 3: "1", 4: ".", 5: "2", 6: ".", 7: "3", 8: ".", 9: "4", 10: ".", 11: "5", 12: ".", 13: "6", 14: ".", 15: "7", 16: ".", 17: "8", 18: ".", 19: "9", 20: ".", 21: "10", 22: ".", 23: "11", 24: ".", 25: "12", 26: ".", 27: "13", 28: ".", 29: "14", 30: ".", 31: "15", 32: ".", 33: "16", 34: ".", 35: "17", 36: ".", 37: "18", 38: ".", 39: "19", 40: ".", 41: "20", 42: ".", 43: "21", 44: ".", 45: "22", 46: ".", 47: "23", 48: "." },
            1: { 0: "一" },
            2: { 0: "二" },
            3: { 0: "三" },
            4: { 0: "四" },
            5: { 0: "五" },
            6: { 0: "六" },
            7: { 0: "日" }
        },
        clear: function () {
            grid_ds.Values = {}
        },
        set: function (row, col, val) {
            if (grid_ds.Values[row] == null) grid_ds.Values[row] = {}
            grid_ds.Values[row][col] = val
        },
        get: function (row, col) {
            if (grid_ds.Values[row] != null)
                return grid_ds.Values[row][col];
            else
                return null
        },
        remove: function (row, col) {
            if (grid_ds.Values[row] != null && grid_ds.Values[row][col] != null)
                delete grid_ds.Values[row][col];
        },
        getRowCount: function () {
            return 8
        },
        getColumnCount: function () {
            return 49
        }
    }

    var grid_css = {
        getColumnCss: function (columnIndex) {
            if (columnIndex == 1) return 'col_1'; else return ''
        },
        getRowCss: function (rowIndex) {
            return ''
        }
    }
    try {
        var grid = LJS.create(
				"LJS.UI.Grid", //控件类型
				'dataList', //包含该控件的容器
				'Grid1', //表格的ID
				grid_ds//数据源
			);

        grid.AfterSelect = function (sender, evt) {
            if (evt.ctrlKey) {
                var area = sender.getSelectedArea()
                if (area != null) {
                    val = sender.get(area.startX, area.startY)
                    sender.set(val, area.startX, area.startY, area.endX, area.endY)
                }
            }
        }
        grid.create()


    }
    catch (ex) {
        alert(ex);
        throw ex
    }

    function btnYes_onclick() {

        var $trAry = $("table#Grid1 tr");
        for (var i = 1; i < $trAry.length; i++) {
            var $tr = $($trAry[i]);
            var $tdAry = $tr.find("td");
            for (var d = 1; d < $tdAry.length; d++) {
                var uu = $tdAry[d].style.backgroundColor;
                if (uu == 'rgb(214, 224, 245)' || uu == "#d6e0f5") {
                    $tdAry[d].style.backgroundColor = "";
                    $tdAry[d].className = "tdClass1";
                }
            }
        }
    }

    function btnNo_onclick() {
        var $trAry = $("table#Grid1 tr");
        for (var i = 1; i < $trAry.length; i++) {
            var $tr = $($trAry[i]);
            var $tdAry = $tr.find("td");
            for (var d = 1; d < $tdAry.length; d++) {
                var uu = $tdAry[d].style.backgroundColor;
                if (uu == 'rgb(214, 224, 245)' || uu == "#d6e0f5") {
                    $tdAry[d].style.backgroundColor = "";
                    $tdAry[d].className = "";
                }
            }
        }
    }
    $(document).ready(function () {
        if ($.dialog.data('DialogTimeData')) {
            TabInput = $.dialog.data('DialogTimeData');
        };
        if (TabInput) {
            setdatas(TabInput);
        }
        else {
            setdatasALLCheck();
        }

        //
    });
    function closeDialogs() {


    }

    function getdatas() {
        var tp_target = "";
        var $trAry = $("table#Grid1 tr");
        for (var i = 1; i < $trAry.length; i++) {
            var tp_line = "";
            var $tr = $($trAry[i]);
            var $tdAry = $tr.find("td");
            for (var d = 1; d < $tdAry.length; d++) {
                var uu = $tdAry[d].className;
                if (uu == "tdClass1") {
                    tp_line += "1";
                }
                else {
                    tp_line += "0";
                }
            }
            tp_target += tp_line + ";";
        }
        tp_target = tp_target.substring(0, tp_target.lastIndexOf(';'));
        return tp_target;
    }
    function setdatas(values) {
        //values = "111111111111110000000000000000000000000000000000;000000000000000000000000000000000000000000000000;000000000000000000000000000000000000000000000000;000000000000000000000000000000000000000000000000;000000000000000000000000000000000000000000000000;000000000000000000000000000000000000000000000000;000000000000000000000000000000000000000000000000";
        var tp_target = values.split(";");
        var $trAry = $("table#Grid1 tr");
        for (var i = 1; i < $trAry.length; i++) {
            var $tr = $($trAry[i]);
            var $tdAry = $tr.find("td");
            for (var d = 1; d < $tdAry.length; d++) {
                var tp_1 = tp_target[i - 1].substring(d - 1, d);
                //var uu = $tdAry[d].style.backgroundColor;
                if (tp_1 == "1") {
                    $tdAry[d].style.backgroundColor = "";
                    $tdAry[d].className = "tdClass1";
                }
            }
        }

    }
    function setdatasALLCheck() {
        var values = "111111111111111111111111111111111111111111111111;111111111111111111111111111111111111111111111111;111111111111111111111111111111111111111111111111;111111111111111111111111111111111111111111111111;111111111111111111111111111111111111111111111111;111111111111111111111111111111111111111111111111;111111111111111111111111111111111111111111111111";
        var tp_target = values.split(";");
        var $trAry = $("table#Grid1 tr");
        for (var i = 1; i < $trAry.length; i++) {
            var $tr = $($trAry[i]);
            var $tdAry = $tr.find("td");
            for (var d = 1; d < $tdAry.length; d++) {
                var tp_1 = tp_target[i - 1].substring(d - 1, d);
                //var uu = $tdAry[d].style.backgroundColor;
                if (tp_1 == "1") {
                    $tdAry[d].style.backgroundColor = "";
                    $tdAry[d].className = "tdClass1";
                }
            }
        }

    }


</script>
<script src="CrmArt_TimeSet.js" type="text/javascript"></script>
</html>

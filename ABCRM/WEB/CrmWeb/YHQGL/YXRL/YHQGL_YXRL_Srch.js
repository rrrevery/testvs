var date = new Date();
var d = date.getDate();
var m = date.getMonth();
var y = date.getFullYear();
var dataArray = new Array();
var vCaption = "促销活动主题定义";
function InitGrid() {
	vColumnNames = ['促销ID', '商户代码', "商户", '促销活动编号', '年度', '促销主题', '促销期数', '开始时间', '结束时间', 'iDJR', '登记人', '登记时间', 'iZXR', '审核人', '审核日期'];
	vColumnModel = [
			{ name: 'iJLBH', index: 'iCXID',width: 80 },
			{ name: 'sSHDM', width: 60, hidden: true },
			{ name: 'sSHMC', width: 80, },
			{ name: 'iCXHDBH', width: 80, hidden: true },
			{ name: 'iNIAN', width: 60, },
			{ name: 'sCXZT', width: 160, },
			{ name: 'iCXQS', width: 60, },
			{ name: 'dKSSJ', width: 80, },
			{ name: 'dJSSJ', width: 80, },
			{ name: 'iDJR', hidden: true, },
			{ name: 'sDJRMC', width: 80, },
			{ name: 'dDJSJ', width: 120, },
			{ name: 'iZXR', hidden: true, },
			{ name: 'sZXRMC', width: 80, },
			{ name: 'dZXRQ', width: 120, },
	];
}
$(document).ready(function () {
   
	$('#calendar').fullCalendar({
		customButtons: {
			//自定义 按钮可以添加多个，自己命名  在header 部分调用即可 比如这里的myCustomButton
			refresh: {
				text: '刷新',
				click: function () {
					RefleshCalendar();
				}
			}
		},
		header: {
			left: 'prev,next today',
			center: 'title',
			//right: 'month,agendaWeek,agendaDay'
			//right: 'month',
			right: 'refresh',
		},
		buttonText: {
			today: '今天',
			month: '月',
			week: '周',
			day: '天'
		},
		allDaySlot: false,
		allDayText: '全天',
		aspectRatio: 1.8,
		monthNames: ["一月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "十一月", "十二月"],
		monthNamesShort: ["一月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "十一月", "十二月"],
		dayNames: ["周日", "周一", "周二", "周三", "周四", "周五", "周六"],
		dayNamesShort: ["周日", "周一", "周二", "周三", "周四", "周五", "周六"],
		firstDay: 1,
		editable: false,
		eventLimit: true, // allow "more" link when too many events
		timeFormat: 'H:mm',
		axisFormat: 'H:mm',
		eventClick: function (event) {
			var jlbh = event.id;
			MakeNewTab("CrmWeb/YHQGL/CXHD/YHQGL_CXHDCX.aspx?jlbh=" + jlbh, "促销活动主题定义", 5167001);
		},
		eventMouseover: function (calEvent, jsEvent, view) {
			var fstart = moment(calEvent.start).format('YYYY/MM/DD');
			var end = new Date(calEvent.end);
			AddDays(end, -1);
			var fend = moment(end).format('YYYY/MM/DD');

			for (i = 0; i < dataArray.length; i++) {
				if (dataArray[i].iCXID == calEvent.id) {
					var ZT = "";
					ZT = dataArray[i].iZXR == "0" ? "未审核" : "已审核";
					$(this).attr('title', calEvent.title + " [活动时间：" + fstart + " —— " + fend + " " + " 状态：" + ZT + "]");
					$(this).css('font-weight', 'normal');
					//$(this).tooltip({
					//	effect: 'toggle',
					//	cancelDefault: true
					//});
				}
			}
		},
		eventMouseout: function (calEvent, jsEvent, view) {
		    $(this).css('font-weight', 'normal');
		},
		dayClick: function (date, allDay, jsEvent, view) {
			var dt = new Date(date);
			var kssj = FormatDate(dt, "yyyy-MM-dd");
			MakeNewTab("CrmWeb/YHQGL/CXHD/YHQGL_CXHDCX.aspx?action=add&kssj=" + kssj, "促销活动主题定义", 5167001);
		}
	});
	RefleshCalendar();
});

function RefleshCalendar() {
	//清除原本所有的日历记录
	$('#calendar').fullCalendar('removeEvents');
	var str = GetCXHD();
	var data = JSON.parse(str);
	for (i = 0; i < data.length; i++) {
		var eventObj = new Object();
		dataArray = data;
		eventObj.id = data[i].iCXID;
		eventObj.title = data[i].sCXZT + "：" + data[i].sCXNR;
		dataArray[i].sCXNR = data[i].sCXNR;
		dataArray[i].iCXID = data[i].iCXID;
		if (data[i].iZXR != "0") {
			//eventObj.title += "  [状态：已审核]  ";
			eventObj.color = "#06E052";   //"#33FF33"; 

		}// 已审核
		else {
			//eventObj.title += " [状态：未审核]  ";                  
			eventObj.color = "#0079FF";//"#3300FF";
		}

		eventObj.start = data[i].dKSSJ
		var end = data[i].dJSSJ;
		end = end.replace(/-/g, "/");
		var jssj = new Date(end);
		AddDays(jssj, 1);
		var e = moment(jssj).format('YYYY-MM-DD HH:mm:ss');
		eventObj.end = e;
		//AddDays(eventObj.end, 1);
		//if (eventObj.start <= date && eventObj.end >= date){// 今天处于活动时间内
		// 未审核
		//}
		//if (eventObj.end < date)// 活动已结束
		//    eventObj.color = "#FF0033";
		//if (eventObj.start > date )//还未到开始时间
		//    eventObj.color = "#999999";
		//eventObj.color ='#' + ('00000' + (Math.random() * 0x1000000 << 0).toString(16)).slice(-6);
		eventObj.allDay = false;
		$('#calendar').fullCalendar('renderEvent', eventObj, true);
	}

}
function SearchData() {
	;
}


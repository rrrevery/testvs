
// JScript source code
(function(){
	LJS.creators["LJS.UI.Grid"]=function(containerId,tableId,dataSource,css)
	{
		return new Grid(containerId,tableId,dataSource,css);
		
		function Grid(containerId,tableId,dataSource,css)
		{
			var container=document.getElementById(containerId);
			var rowCount,columnCount;
			if(container==null) throw '无效的容器'
			if(tableId==null || tableId=='') throw '表格名称不能为空'
			if(dataSource==null) throw '数据源不能为空'
			if(dataSource.set==null) throw '数据源必须有set(rowIndex,columnIndex,value)方法'
			if(dataSource.get==null) throw '数据源必须有get(rowIndex,columnIndex)方法'
			if(dataSource.getRowCount==null) throw '数据源必须有getRowCount()方法'
			if(dataSource.getColumnCount==null) throw '数据源必须有getColumnCount()方法'
			if(dataSource.remove==null) throw '数据源必须有remove(rowIndex,columnIndex)方法'
			if(dataSource.clear==null) throw '数据源必须有clear()方法'
			
			if(css!=null)
			{
				if(css.getColumnCss==null) throw 'css必须有getColumnCss()方法'
				if(css.getRowCss==null) throw 'css必须有getRowCss()方法'
			}
			
			rowCount=dataSource.getRowCount()
			columnCount=dataSource.getColumnCount()

			var data=null
			var table=null
			var selectArea={startX:-1,startY:-1,endX:-1,endY:-1}
			var srcArea={left:-1,top:-1,right:-1,bottom:-1,srcValue:null}
			var currentCell={x:-1,y:-1,cell:null}
			var currentEditCell={x:-1,y:-1,cell:null}
			
			var ref=this
			var draging=false
			
			this.AfterSelect=null
			
			var menu=document.createElement('div')
			menu.className='lgrid_menu'
			menu.style.display='none'
			menu.style.position='absolute'
			
			var clearMenu=document.createElement('div')
			clearMenu.innerHTML='清除内容'
			clearMenu.className='lgrid_menu_item'
			clearMenu.style.display='block'
			clearMenu.onclick=function()
			{
				menu_onclick('delete')
			}
			menu.appendChild(clearMenu)
			
			var copyMenu=document.createElement('div')
			copyMenu.innerHTML='复制'
			copyMenu.className='lgrid_menu_item'
			copyMenu.style.display='block'
			copyMenu.onclick=function()
			{
				menu_onclick('copy')
			}
			menu.appendChild(copyMenu)
			
			var cutMenu=document.createElement('div')
			cutMenu.innerHTML='剪切'
			cutMenu.className='lgrid_menu_item'
			cutMenu.style.display='block'
			cutMenu.onclick=function()
			{
				menu_onclick('cut')
			}
			menu.appendChild(cutMenu);
			
			var pasteMenu=document.createElement('div')
			pasteMenu.innerHTML='粘帖'
			pasteMenu.className='lgrid_menu_item'
			pasteMenu.style.display='block'
			pasteMenu.onclick=function()
			{
				menu_onclick('paste')
			}
			//menu.appendChild(pasteMenu)
			
			//document.getElementsByTagName('body')[0].appendChild(menu)
			
			var selectedColor='#D6E0F5'
			//var currentColor='#FBEDBB'
			var currentColor='#D6E0F5'
			function getTarget(evt)
			{
				var target=null
				target=evt.srcElement
				if(target==null) target=evt.target
				return target
			}
			function onselectstart(evt)
			{
				var target=getTarget(evt)
				return target.nodeName.toUpperCase()=='INPUT'
				return false
			}
			
			function copy()
			{
				if(hasSelected())
				{
					srcArea.left=Math.min(selectArea.startX,selectArea.endX)
					srcArea.right=Math.max(selectArea.startX,selectArea.endX)
					srcArea.top=Math.min(selectArea.startY,selectArea.endY)
					srcArea.bottom=Math.max(selectArea.startY,selectArea.endY)
				}
				else
				{
					srcArea.left=currentCell.x
					srcArea.right=currentCell.x
					srcArea.top=currentCell.y
					srcArea.bottom=currentCell.y
				}
				srcArea.srcValue=new Array()
				for(var y=srcArea.top;y<=srcArea.bottom;y++)
				{
					var r=new Array()
					srcArea.srcValue.push(r)
					for(var x=srcArea.left;x<=srcArea.right;x++)
					{
						r.push(dataSource.get(y,x))
					}
				}
			}
			
			function cut()
			{
				if(hasSelected())
				{
					srcArea.left=Math.min(selectArea.startX,selectArea.endX)
					srcArea.right=Math.max(selectArea.startX,selectArea.endX)
					srcArea.top=Math.min(selectArea.startY,selectArea.endY)
					srcArea.bottom=Math.max(selectArea.startY,selectArea.endY)
				}
				else
				{
					srcArea.left=currentCell.x
					srcArea.right=currentCell.x
					srcArea.top=currentCell.y
					srcArea.bottom=currentCell.y
				}
				srcArea.srcValue=new Array()
				for(var y=srcArea.top;y<=srcArea.bottom;y++)
				{
					var r=new Array()
					srcArea.srcValue.push(r)
					for(var x=srcArea.left;x<=srcArea.right;x++)
					{
						r.push(dataSource.get(y,x))
					}
				}
				clearCells(srcArea.left,srcArea.top,srcArea.right,srcArea.bottom)
			}
			
			function paste()
			{
				if(srcArea.srcValue!=null)
				{
					for(var y=0;y<srcArea.srcValue.length;y++)
					{
						for(var x=0;x<srcArea.srcValue[y].length;x++)
						{
							dataSource.set(currentCell.y+y,currentCell.x+x,srcArea.srcValue[y][x])
						}
					}
					var ex=currentCell.x+srcArea.srcValue[0].length-1
					var ey=currentCell.y+srcArea.srcValue.length-1
					if(ex>=columnCount) ex=columnCount-1
					if(ey>=rowCount) ey=rowCount-1
					refresh(currentCell.x,currentCell.y,ex,ey)
				}
			}
			
			
			function menu_onclick(command)
			{
				switch(command.toUpperCase())
				{
				case 'DELETE':
					{
						if(hasSelected())
						{
							clearCells(selectArea.startX,selectArea.startY,selectArea.endX,selectArea.endY)
						}
						else
						{
							clearCells(currentCell.x,currentCell.y,currentCell.x,currentCell.y)
						}
						break;
					}
				case 'COPY':
					{
						copy()
						break;
					}
				case 'CUT':
					{
						cut()
						break;
					}
				case 'PASTE':
					{
						paste()
						break;
					}
				}
				showMenu(false,0,0)
			}
			
			function onclick(evt)
			{
				var target=getTarget(evt)
				if(target.nodeName.toUpperCase()=='INPUT') target=(target.parentElement==undefined?target.parentNode:target.parentElement)
				
				if(target.nodeName.toUpperCase()=='TD')
				{
					//editCell(target)
				}
			}
			function getButton(evt)
			{
				if((evt.which!=undefined && evt.which==1) || evt.button==1)
					return 1
				else if((evt.which!=undefined && evt.which==3) || evt.button==2)
					return 2
			}
			function onmousedown(evt)
			{
				var target=getTarget(evt)
				if(target.nodeName.toUpperCase()=='INPUT') target=(target.parentElement==undefined?target.parentNode:target.parentElement)
				
				if(target.nodeName.toUpperCase()=='TD')
				{
					var btn=getButton(evt)
					var c=target.cellIndex
					var r=target.parentNode.rowIndex
					if(btn==1)
					{
						setCurrentCell(c,r)
						clearSelect()
						selectArea.startX=c
						selectArea.startY=r
						selectArea.endX=-1
						selectArea.endY=-1
						draging=true
					}
					else if(btn==2)
					{
						if(!inSelectedArea(c,r)) setCurrentCell(c,r)
					}
				}
				return false
			}
			function onmousemove(evt)
			{
				if(draging)
				{
					var target=getTarget(evt)
					if(target.nodeName.toUpperCase()=='INPUT') target=(target.parentElement==undefined?target.parentNode:target.parentElement)
					if(target.nodeName.toUpperCase()=='TD' && selectArea.startX!=-1 && selectArea.startY!=-1)
					{
						var c=target.cellIndex
						var r=target.parentNode.rowIndex
						if(selectArea.endY==-1 && selectArea.endX==-1)
						{
							//第一次触发，高亮选择区域
							highLight(selectArea.startX,selectArea.startY,c,r,selectedColor)
						}
						else if(selectArea.endX!=c || selectArea.endY!=r)
						{
							//选择区域改变，高亮新的选择区域
							highLight(selectArea.startX,selectArea.startY,selectArea.endX,selectArea.endY,'')
							highLight(selectArea.startX,selectArea.startY,c,r,selectedColor)
						}
						selectArea.endX=c
						selectArea.endY=r
					}
				}
			}
			function onmouseup(evt)
			{
				if((evt.which!=undefined && evt.which==1) || evt.button==1)
				{
					if(draging)
					{
						try
						{
							var target=getTarget(evt)
							if(target.nodeName.toUpperCase()=='INPUT') target=(target.parentElement==undefined?target.parentNode:target.parentElement)
							if(target.nodeName.toUpperCase()=='TD')
							{
								var c=target.cellIndex
								var r=target.parentNode.rowIndex
								//判断是否启动了拖动
								if(hasSelected()) 
								{
									try
									{
										if(ref.AfterSelect!=null) grid.AfterSelect(ref,evt)
									}
									catch(msg)
									{
										alert(msg)
									}
									container.firstChild.focus()
								}
							}
						}
						finally
						{
							draging=false
						}
					}
				}
				else if((evt.which!=undefined && evt.which==3) || evt.button==2)
				{
					var target=getTarget(evt)
					if(target.nodeName.toUpperCase()=='INPUT') target=(target.parentElement==undefined?target.parentNode:target.parentElement)
					if(target.nodeName.toUpperCase()=='TD')
					{
						var x=target.cellIndex
						var y=target.parentNode.rowIndex
						var left=Math.min(selectArea.startX,selectArea.endX)
						var right=Math.max(selectArea.startX,selectArea.endX)
						var top=Math.min(selectArea.startY,selectArea.endY)
						var bottom=Math.max(selectArea.startY,selectArea.endY)
						if(!(x>=left && x<=right && y>=top && y<=bottom))
						{
							clearSelect()
						}
					}
				}
				return true
			}
			function onkeydown(evt)
			{
				var target=getTarget(evt)
				if(target!=null)
				{
					switch(evt.keyCode)
					{
					case 46:
						{
							if(hasSelected())
							{
								clearCells(selectArea.startX,selectArea.startY,selectArea.endX,selectArea.endY)
							}
							break;
						}
					case 37:
						{
							var td=(target.parentElement==undefined?target.parentNode:target.parentElement)
							if(td.nodeName.toUpperCase()=='TD')
							{
								var c=td.cellIndex
								var r=td.parentNode.rowIndex
								if(c>0) 
								{
									editCell(table.rows[r].cells[c-1])
									setCurrentCell(c-1,r)
								}
							}
							break;
						}
					case 38:
						{
							var td=(target.parentElement==undefined?target.parentNode:target.parentElement)
							if(td.nodeName.toUpperCase()=='TD')
							{
								var c=td.cellIndex
								var r=td.parentNode.rowIndex
								if(r>0) 
								{
									editCell(table.rows[r-1].cells[c])
									setCurrentCell(c,r-1)
								}
							}
							break;
						}
					case 39:
						{
							var td=(target.parentElement==undefined?target.parentNode:target.parentElement)
							if(td.nodeName.toUpperCase()=='TD')
							{
								var c=td.cellIndex
								var r=td.parentNode.rowIndex
								if(c<columnCount-1) 
								{
									editCell(table.rows[r].cells[c+1])
									setCurrentCell(c+1,r)
								}
							}
							break;
						}
					case 40:
						{
							var td=(target.parentElement==undefined?target.parentNode:target.parentElement)
							if(td.nodeName.toUpperCase()=='TD')
							{
								var c=td.cellIndex
								var r=td.parentNode.rowIndex
								if(r<rowCount-1) 
								{
									editCell(table.rows[r+1].cells[c])
									setCurrentCell(c,r+1)
								}
							}
							break;
						}
					}
				}
				return true
			}
			function uneditCell(td)
			{
				if(td.childNodes.length>0 && td.firstChild.nodeName.toUpperCase()=='INPUT')
				{
					var textBox=td.firstChild
					td.innerHTML=(textBox.value==''?'&nbsp;':textBox.value.replace(/</g,'&lt;'))
					delete textBox
				}
			}
			function setCurrentCell(c,r)
			{
				if(currentCell.cell!=null && (c!=currentCell.x || r!=currentCell.y))
				{
					currentCell.cell.style.backgroundColor=(inSelectedArea(currentCell.x,currentCell.y)?selectedColor:'')
				}
				currentCell.x=c
				currentCell.y=r
				currentCell.cell=table.rows[r].cells[c]
				currentCell.cell.style.backgroundColor=currentColor
			}
			function editCell(td)
			{
				if(td.nodeName.toUpperCase()=='TD')
				{
					var textBox=null
					var c=td.cellIndex
					var r=td.parentNode.rowIndex
					if(td.childNodes.length>0 && td.firstChild.nodeName.toUpperCase()=='INPUT')
					{
						textBox=td.firstChild
					}
					else
					{					
						var textBox=document.createElement('input')
						textBox.type='text'
						var text=(td.innerText==undefined?td.textContent:td.innerText)
						textBox.value=(td.innerHTML=='&nbsp;'?'':text)
						textBox.onchange=function()
						{
							dataSource.set(r,c,this.value)
						}
						if(td.childNodes.length>0)
							td.replaceChild(textBox,td.firstChild)
						else
							td.appendChild(textBox)
					}
					textBox.focus()
					if(currentEditCell.cell!=null && (c!=currentEditCell.x || r!=currentEditCell.y))
					{
						uneditCell(currentEditCell.cell)
					}
					currentEditCell.x=c
					currentEditCell.y=r
					currentEditCell.cell=td
				}
			}
			function clearCells(sx,sy,ex,ey)
			{
				var sc=Math.min(sx,ex)
				var ec=Math.max(sx,ex)
				var sr=Math.min(sy,ey)
				var er=Math.max(sy,ey)
				
				for(var i=sc;i<=ec;i++)
				{
					for(var j=sr;j<=er;j++)
					{
						var cell=table.rows[j].cells[i]
						cell.innerHTML="&nbsp;"
						dataSource.remove(j,i)
					}
				}
			}
			function hasSelected(grid)
			{
				return selectArea.startX!=-1 && selectArea.startY!=-1 && selectArea.endX!=-1 && selectArea.endY!=-1
			}
			function inSelectedArea(x,y)
			{
				if(hasSelected())
				{
					var sc=Math.min(selectArea.startX,selectArea.endX)
					var ec=Math.max(selectArea.startX,selectArea.endX)
					var sr=Math.min(selectArea.startY,selectArea.endY)
					var er=Math.max(selectArea.startY,selectArea.endY)
					return (x>=sc && x<=ec && y>=sr && y<=er)
				}
				else
					return false
			}
			function highLight(sx,sy,ex,ey,color)
			{
				var sc=Math.min(sx,ex)
				var ec=Math.max(sx,ex)
				var sr=Math.min(sy,ey)
				var er=Math.max(sy,ey)
				
				for(var i=sc;i<=ec;i++)
				{
					for(var j=sr;j<=er;j++)
					{
						if(j!=currentCell.y || i!=currentCell.x)
						{
							var cell=table.rows[j].cells[i]
							cell.style.backgroundColor=color
							
						}
					}
				}
			}
			function clearSelect()
			{
				if(hasSelected(grid)) 
				{
					highLight(selectArea.startX,selectArea.startY,selectArea.endX,selectArea.endY,'')
				}
				selectArea.startX=-1
				selectArea.startY=-1
				selectArea.endX=-1
				selectArea.endY=-1
			}
			
			if(document.attachEvent)
			{
				document.attachEvent(
					"onmouseup",
					body_onmouseup
				)
				document.attachEvent(
					"onmousedown",
					function(evt)
					{
						if(evt==null) evt=event
						return body_mousedown(evt)
					}
				)
			}
			else if(document.addEventListener)
			{
				document.addEventListener(
					"mouseup",
					body_onmouseup,
					false
				)
				document.addEventListener(
					"mousedown",
					function(evt)
					{
						if(evt==null) evt=event
						return body_mousedown(evt)
					},
					false
				)
			}
			
			function create()
			{
				var tableHTML=new Array();
				//var startTime=new Date()
				tableHTML.push('<div><table CellSpacing="0" id="{ID}">'.replace(/{ID}/g,tableId))
				tableHTML.push("<tbody>")
				for(var row=0;row<rowCount;row++)
				{
					var trHTML=new Array()
					trHTML.push("<tr")
					var cls=(css==null?'':css.getRowCss(row))
					if(cls!='')
					{
						trHTML.push(' class="')
						trHTML.push(cls)
						trHTML.push('"')
					}
					trHTML.push(">")
					for(var col=0;col<columnCount;col++)
					{
						var val=dataSource.get(row,col)
						trHTML.push('<td')
						var tdClass=(css==null?'':css.getColumnCss(col))
						if(tdClass!='')
						{
							trHTML.push(' class="')
							trHTML.push(tdClass)
							trHTML.push('"')
						}
						trHTML.push('>');
						if(val!=null) 
							trHTML.push(val.replace(/</g,'&lt;'))
						else
							trHTML.push("&nbsp;")
						trHTML.push('</td>')
					}
					trHTML.push("</tr>")
					tableHTML.push(trHTML.join(''))
				}
				trHTML.push("</tbody>")
				trHTML.push('</table></div>')
				var innerHTML=tableHTML.join('')
				//var endTime=new Date()
				//alert(endTime-startTime)
				container.innerHTML=innerHTML
				table=document.getElementById(tableId)
				
				var div=container.firstChild
				div.tabIndex = -1
				div.style.outline = "none"
				div.onselectstart=function(evt)
				{
					if(evt==null) evt=event
					return onselectstart(evt)
				};
				div.onmouseover=function(evt)
				{
					if(evt==null) evt=event
					return onmousemove(evt)
				}
				div.onmousedown=function(evt)
				{
					if(evt==null) evt=event
					return onmousedown(evt)
				}
				div.onmouseup=function(evt)
				{
					if(evt==null) evt=event
					return onmouseup(evt)
				}
				div.onclick=function(evt)
				{
					if(evt==null) evt=event
					return onclick(evt)
				}
				div.onkeydown=function(evt)
				{
					if(evt==null) evt=event
					return onkeydown(evt)
				}
				div.oncontextmenu=function(evt)
				{
					if(evt==null) evt=event
					return oncontextmenu(evt)
				} 

			}
			function mousePosition(ev)
			{
				if(ev.pageX || ev.pageY)
				{
					return {x:ev.pageX, y:ev.pageY}
				}
				else 
				{
					return {x:document.documentElement.scrollLeft+ev.clientX,y:document.documentElement.scrollTop+ev.clientY}
				}
			} 
			function oncontextmenu(evt)
			{
				var target=getTarget(evt)
				var mp=mousePosition(evt)
				showMenu(true,mp.x,mp.y)
				return false
			}
			function showMenu(display,x,y)
			{
				menu.style.display=(display?'block':'none')
				menu.style.left=x+'px'
				menu.style.top=y+'px'
			}
			function body_mousedown(evt)
			{
				var target=getTarget(evt)
				if(target.parentNode!=menu)
					showMenu(false,0,0)
			}
			function body_onmouseup(evt)
			{
				draging=false
				var target=getTarget(evt)
			}
			function body_onkeydown(evt)
			{
				if(evt==null) evt=event
				return onkeydown(evt)
			}
			function get(x,y)
			{
				var cell=table.rows[y].cells[x]
				if(cell.childNodes.length>0 && cell.firstChild.nodeName.toUpperCase()=='INPUT')
				{
					return cell.firstChild.value
				}
				else
				{
					return (cell.innerText==undefined?cell.textContent:cell.innerText)
				}
			}
			function set(value,sx,sy,ex,ey)
			{
				if(ex==null) ex=sx;
				if(ey==null) ey=sy;
				var sc=Math.min(sx,ex)
				var ec=Math.max(sx,ex)
				var sr=Math.min(sy,ey)
				var er=Math.max(sy,ey)
				for(var c=sc;c<=ec;c++)
				{
					for(var r=sr;r<=er;r++)
					{
						var td=table.rows[r].cells[c]
						var textBox
						if(td.childNodes.length>0 && td.firstChild.nodeName.toUpperCase()=='INPUT')
						{
							textBox=td.firstChild
							textBox.value=value
						}
						else
						{
							if(td.innerText==undefined)
								td.textContent=value
							else				
								td.innerText=value
						}
						dataSource.set(r,c,value)
					}
				}
			}
			
			function refresh(sx,sy,ex,ey)
			{
				if(sx==null) sx=0
				if(sy==null) sy=0
				if(ex==null) ex=columnCount-1
				if(ey==null) ey=rowCount-1
				var sc=Math.min(sx,ex)
				var ec=Math.max(sx,ex)
				var sr=Math.min(sy,ey)
				var er=Math.max(sy,ey)
				
				for(var c=sc;c<=ec;c++)
					for(var r=sr;r<=er;r++)
					{
						var td=table.rows[r].cells[c]
						var value=dataSource.get(r,c)
						if(value!=null)
						{
							if(td.childNodes.length>0 && td.firstChild.nodeName.toUpperCase()=='INPUT')
							{
								td.firstChild.value=value
							}
							else
							{					
								td.innerHTML=value.replace(/</g,'&lt;')
							}
						}
					}
			}
			
			function getSelectedArea()
			{
				var area={
					startX:selectArea.startX,
					startY:selectArea.startY,
					endX:selectArea.endX,
					endY:selectArea.endY
				}
				return hasSelected()?area:null
			}
			var completeCallback
			this.create=create
			this.refresh=refresh
			this.get=get
			this.set=set
			this.getSelectedArea=getSelectedArea
		}
	}
})()

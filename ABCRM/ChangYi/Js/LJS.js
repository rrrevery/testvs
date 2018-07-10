function LJS_Contructor()
{
	var m_root="";
	var creators={};
	var count=0;
	var baseDate=new Date()
	function create(objId)
	{
		if(creators[objId]!=null)
		{
			var args=[]
			for(var index=1;index<arguments.length;index++)
			{
				args.push(arguments[index])
			}
			var obj=creators[objId].apply(this,args);
			return obj;
		}
		else
			return null
	}
	
	function root(val)
	{
		if(val!=null) m_root=val;
		return m_root;
	}
	
	function generateUniqueId()
	{
		var dt=new Date()
		count++;
		return 'LJSID_'+(dt-baseDate)+"_"+count;
	}
	
	this.create=create;
	this.root=root;
	this.generateUniqueId=generateUniqueId;
	this.creators=creators;
}

var LJS=new LJS_Contructor()
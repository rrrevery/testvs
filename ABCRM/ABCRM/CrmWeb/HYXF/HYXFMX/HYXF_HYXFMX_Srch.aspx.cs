using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BF.CrmWeb.HYXF.HYXFMX
{
    public partial class HYXF_HYXFMX_Srch : BasePage
    {
        public string tp_condition = "(select HYID from HYK_HYXX where HYK_NO=";
        public string tp_conditionOne = "union  select HYID from HYK_CHILD_JL where HYK_NO=";
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}
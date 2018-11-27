using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace TestDatabind
{
    public class RDataTable : DataTable
    {
        public System.Windows.Forms.BindingSource BindSource = new System.Windows.Forms.BindingSource();
        public int Position
        {
            get { return BindSource.Position; }
            set { BindSource.Position = value; }
        }
        public RDataTable()
        {
            BindSource.DataSource = this;
        }
    }

}

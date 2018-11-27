using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDatabind
{
    public class DataObject// : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

       // public event PropertyChangedEventHandler PropertyChanged;
    }
}

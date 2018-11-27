using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMR.Controls
{
    class FilterPropertiesDesigner : System.Windows.Forms.Design.ControlDesigner
    {
        protected override void PreFilterProperties(System.Collections.IDictionary properties)
        {
            properties.Remove("AcceptsReturn");
            properties.Remove("AcceptsTab");
            properties.Remove("AccessibilityObject");
            properties.Remove("AccessibleDescription");
            properties.Remove("AccessibleRole");
            properties.Remove("AutoCompleteCustomSource");
            properties.Remove("AllowDrop");
            properties.Remove("AutoCompleteMode");
            properties.Remove("AutoScrollOffset");
            properties.Remove("AutoSize");
            properties.Remove("BackgroundImage");
            properties.Remove("MenuManager");
            properties.Remove("Properties");
        }
    }
}

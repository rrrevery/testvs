using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMR.Controls
{
    public class MyControlType
    {
        public static Type[] windowsFormsToolTypes = new Type[] {
            typeof(System.Windows.Forms.Label),
            typeof(System.Windows.Forms.TextBox),
            typeof(System.Windows.Forms.CheckBox),
            typeof(System.Windows.Forms.RadioButton),
            typeof(System.Windows.Forms.GroupBox),
            typeof(System.Windows.Forms.Panel),
            typeof(System.Windows.Forms.PictureBox),
            typeof(System.Windows.Forms.CheckedListBox),
            typeof(System.Windows.Forms.ComboBox)
        };
        public static Type[] componentsToolTypes = new Type[] {
            //typeof(EMR.Controls.JHUCDiagnosis),
            //typeof(EMR.Controls.UcBloodPressure),
            typeof(EMR.Controls.UcDataGridView),
            typeof(EMR.Controls.UcLabel),
            typeof(EMR.Controls.UcTextBox),
            typeof(EMR.Controls.UcRichTextBox),
            typeof(EMR.Controls.UcCheckBox),
            typeof(EMR.Controls.UcDateTime),
            typeof(EMR.Controls.UcComboBox),
            typeof(EMR.Controls.UcNumericUpDown),
            typeof(EMR.Controls.UcUserDefined),
            typeof(EMR.Controls.UcCheckBoxList )
        };
    }
}
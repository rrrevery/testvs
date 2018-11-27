using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EMR.Controls
{
    public partial class UcDataGridView : UserControl
    {
        #region 自定义的属性
        private enumEleName elemName;
        [CategoryAttribute("数据绑定"), ReadOnlyAttribute(false), DescriptionAttribute("字典名称")]
        public enumEleName ElemName
        {
            get { return elemName; }
            set { elemName = value; }
        }

        private int columnNo = 0;
        [CategoryAttribute("数据绑定"), ReadOnlyAttribute(false), DescriptionAttribute("列序位置号")]
        public int ColumnNo
        {
            get { return columnNo; }
            set { columnNo = value; }
        }
        #endregion

        /// <summary>
        /// 与dataGridView1绑定的数据表
        /// </summary>
        public DataTable dt = new DataTable();

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public UcDataGridView()
        {
            InitializeComponent();
        } 
        #endregion

        #region 窗体Load事件
        /// <summary>
        /// 窗体Load事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UcDataGridView_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            dt.Columns.Add("Group");
            dt.Columns.Add("Medicine");
            dt.Columns.Add("Dosage");
            dt.Columns.Add("Remove");
            dt.Columns.Add("Insert");
            dt.Columns.Add("Memo");

            DataRow dr = dt.NewRow();
            dr[3] = "删除";
            dr[4] = "插入";
            dt.Rows.Add(dr);

            dataGridView1.DataSource = dt;
            //dataGridView1.AutoResizeColumn(1, DataGridViewAutoSizeColumnMode.AllCells);
            dataGridView1.AutoResizeColumn(1);
        } 
        #endregion

        #region 点击 “删除”，“插入”列，实现在本行的插入与删除功能
        /// <summary>
        /// 点击 “删除”，“插入”列，实现在本行的插入与删除功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count > 1)
            {
                if (e.ColumnIndex == 3)
                {
                    DataRow[] drtsyp = dt.Select(string.Format(@"Memo = '特殊药物'")); //特殊药物
                    DataRow[] drFly = dt.Select(string.Format(@"Memo = '分类'")); //特殊药物

                    if (dt.Rows[e.RowIndex][5].ToString() == "特殊药物" && drtsyp.Length == 1)
                    {
                        dt.Rows[e.RowIndex][1] = "";
                        dt.Rows[e.RowIndex][2] = "";
                        dataGridView1.Refresh();
                    }
                    else if (dt.Rows[e.RowIndex][5].ToString() == "分类" && drFly.Length == 1)
                    {
                        dt.Rows[e.RowIndex][1] = "";
                        dt.Rows[e.RowIndex][2] = "";
                        dataGridView1.Refresh();
                    }
                    else
                    {
                        dataGridView1.Rows.RemoveAt(e.RowIndex);
                    }
                }

                if (e.ColumnIndex == 4)
                {
                    DataRow dr = dt.NewRow();
                    if (dt.Rows[e.RowIndex][5].ToString() == "特殊药物")
                    {
                        dr[0] = dataGridView1.Rows[e.RowIndex].Cells[0].Value;                        
                        dr[3] = "删除";
                        dr[4] = "插入";
                        dr[5] = "特殊药物";
                        dataGridView1.Focus();
                        dt.Rows.InsertAt(dr, e.RowIndex + 1);
                    }
                    else if (dt.Rows[e.RowIndex][5].ToString() == "分类")
                    {
                        dr[0] = dataGridView1.Rows[e.RowIndex].Cells[0].Value;
                        dr[3] = "删除";
                        dr[4] = "插入";
                        dr[5] = "分类";
                        dataGridView1.Focus();
                        dt.Rows.InsertAt(dr, e.RowIndex + 1);
                    }
                    else
                    {
                        dr[0] = dataGridView1.Rows[e.RowIndex].Cells[0].Value;
                        dr[3] = "删除";
                        dr[4] = "插入";
                        dataGridView1.Focus();
                        dt.Rows.InsertAt(dr, e.RowIndex + 1);
                    }
                }
            }
            else //如果仅剩一行，则仅清空内容
            {
                if (e.ColumnIndex == 3)
                {
                    dt.Clear();
                    DataRow dr = dt.NewRow();
                    dr[3] = "删除";
                    dr[4] = "插入";
                    dt.Rows.Add(dr);
                }

                if (e.ColumnIndex == 4)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = dataGridView1.Rows[e.RowIndex].Cells[0].Value;
                    dr[3] = "删除";
                    dr[4] = "插入";
                    dataGridView1.Focus();
                    dt.Rows.InsertAt(dr, e.RowIndex + 1);
                }
            }
        } 
        #endregion

        #region dataGridView1_RowsAdded事件（未使用）
        /// <summary>
        /// dataGridView1_RowsAdded事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (e.RowIndex == dataGridView1.Rows.Count - 1)
            {
                if (e.RowIndex == 1)
                {
                    dataGridView1.Rows[e.RowIndex - 1].SetValues("1", "", "", "删除", "");
                }
                else if (e.RowIndex >= 2)
                {
                    dataGridView1.Rows[e.RowIndex - 2].SetValues(dataGridView1.Rows[e.RowIndex - 2].Cells[0].Value, dataGridView1.Rows[e.RowIndex - 2].Cells[1].Value, dataGridView1.Rows[e.RowIndex - 2].Cells[2].Value, "删除", "插入");
                    dataGridView1.Rows[e.RowIndex - 1].SetValues(dataGridView1.Rows[e.RowIndex - 2].Cells[0].Value, "", "", "删除", "");
                }
            }
        } 
        #endregion

        #region dataGridView1_RowPostPaint（未使用）
        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y, dataGridView1.RowHeadersWidth - 4, e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), dataGridView1.RowHeadersDefaultCellStyle.Font, rectangle, dataGridView1.RowHeadersDefaultCellStyle.ForeColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Right);

        } 
        #endregion

        #region 右键单击增加一行（未使用）
        /// <summary>
        /// 右键单击增加一行（未使用）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                //Point pos = new Point(e.Node.Bounds.X + e.Node.Bounds.Width, e.Node.Bounds.Y + e.Node.Bounds.Height / 2);
                Point pos = new Point(MousePosition.X, MousePosition.Y);
                contextMenuStrip1.Show(pos);
            }
        }        

        /// <summary>
        /// 右键单击增加一行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem_Click(object sender, EventArgs e)
        {
            string s = sender.ToString();
            if (s == "增加一行")
            {
                DataRow dr = dt.NewRow();
                dr[3] = "删除";
                dr[4] = "插入";
                dt.Rows.Add(dr);
            }
            else if (s == "医嘱药品")
            {
                dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
            }
        }
        #endregion

        #region 输入即结束编辑状态（未使用，有bug）
        /// <summary>
        /// 输入即结束编辑状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (this.dataGridView1.IsCurrentCellDirty)
            {
                this.dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                //dataGridView1.EndEdit();
            }
        } 
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.IO;

namespace ICR100CardReader
{
    public partial class Form1 : Form
    {
        [DllImport("termb.dll", EntryPoint = "InitComm", CharSet = CharSet.Auto, SetLastError = false)]
        public static extern int InitComm(int Port);//声明外部的标准动态库, 跟Win32API是一样的
        [DllImport("termb.dll", EntryPoint = "Authenticate", CharSet = CharSet.Auto, SetLastError = false)]
        public static extern int Authenticate();
        [DllImport("termb.dll", EntryPoint = "Read_Content", CharSet = CharSet.Auto, SetLastError = false)]
        public static extern int Read_Content(int Active);
        [DllImport("termb.dll", EntryPoint = "CloseComm", CharSet = CharSet.Auto, SetLastError = false)]
        public static extern int CloseComm();
        [DllImport("termb.dll", EntryPoint = "GetSAMID", CharSet = CharSet.Ansi, SetLastError = false, CallingConvention = CallingConvention.StdCall)]
        public static extern int GetSAMID(ref byte strTmp);
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        void Log(string s)
        {
            textBox1.Text += s + "\r\n";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int iRetUSB = 0, iRetCOM = 0;
            var errortxt = string.Empty;
            int iPort;
            for (iPort = 1001; iPort <= 1016; iPort++)
            {
                iRetUSB = InitComm(iPort);
                if (iRetUSB == 1)
                {
                    break;
                }
            }
            if (iRetUSB != 1)
            {
                for (iPort = 1; iPort <= 4; iPort++)
                {
                    iRetCOM = InitComm(iPort);
                    if (iRetCOM == 1)
                    {
                        break;
                    }
                }
            }

            if ((iRetCOM == 1) || (iRetUSB == 1))
            {
                errortxt = "初始化成功！";
            }
            else
            {
                errortxt = "初始化失败！";
            }

            if ((iRetCOM == 1) || (iRetUSB == 1))
            {

                int authenticate = Authenticate();
                if (authenticate == 1)
                {
                    int readContent = Read_Content(2);
                    if (readContent == 1)
                    {
                        errortxt = "读卡操作成功！";
                        //var patient = FillData();
                        //return patient;
                        string txt = File.ReadAllText("wz.txt", Encoding.Unicode);
                        byte[] f = Encoding.Default.GetBytes(txt);
                        string name = Encoding.Default.GetString(f.Skip(0).Take(17).ToArray()).Trim();
                        string sex = Encoding.Default.GetString(f.Skip(18).Take(1).ToArray()).Trim();
                        string mz = Encoding.Default.GetString(f.Skip(19).Take(2).ToArray()).Trim();
                        string sr = Encoding.Default.GetString(f.Skip(21).Take(8).ToArray()).Trim();
                        string dz = Encoding.Default.GetString(f.Skip(29).Take(50).ToArray()).Trim();
                        string sfz = Encoding.Default.GetString(f.Skip(80).Take(18).ToArray()).Trim();
                        Log(f.ToString());
                    }
                    else
                    {
                        errortxt = "读卡操作失败！";
                    }
                }
                else
                {
                    errortxt = "未放卡或卡片放置不正确";
                }
                CloseComm();
            }
            else
            {
                errortxt = "初始化失败!";
            }
            Log(errortxt);
        }
    }
}

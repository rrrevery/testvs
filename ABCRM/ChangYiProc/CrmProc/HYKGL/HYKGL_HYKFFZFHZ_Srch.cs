using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BF.Pub;
using System.Data;
using System.Data.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using Microsoft.CSharp;
using System.Globalization;
using System.CodeDom.Compiler;
using System.Reflection;
namespace BF.CrmProc.HYKGL
{
    public class HYKGL_HYKFFZFHZ : DJLR_CLass
    {

        public override List<Object> SearchDataQuery(CyQuery query, DateTime serverTime)
        {
            List<ZFFS> list = new List<ZFFS>();
            List<object> lst = new List<object>();
            query.SQL.Text = "  select distinct SK.ZFFSID,Z.ZFFSMC,Z.ZFFSDM from HYK_CZKSKJL W,HYK_CZKSKJLSKMX SK,ZFFS Z where W.JLBH=SK.JLBH and W.FS=0 and SK.ZFFSID=Z.ZFFSID and ZXR is not null";
            query.Open();
            while (!query.Eof)
            {
                ZFFS item_zff = new ZFFS();
                item_zff.iZFFSID = query.FieldByName("ZFFSID").AsInteger;
                item_zff.sZFFSDM = query.FieldByName("ZFFSDM").AsString;
                item_zff.sZFFSMC = query.FieldByName("ZFFSMC").AsString;
                list.Add(item_zff);
                query.Next();
            }
            query.Close();
            Assembly assembly = NewAssembly(list);
            query.SQL.Text = "    SELECT L.DJR,L.DJRMC,I.HYKTYPE,D.HYKNAME,count(*) SL,";
            foreach (ZFFS item in lst)
            {
                query.SQL.Add(",nvl(SUM(decode(SK.ZFFSID,'" + item.iZFFSID + "',JE)),0) as '" + item.iZFFSID + "'");
            }
            query.SQL.Add("    FROM   HYK_CZKSKJL L,HYK_CZKSKJLSKMX SK,HYK_CZKSKJLITEM I,HYKDEF D");
            query.SQL.Add("    WHERE FS=0 AND L.Jlbh=SK.JLBH and SK.JLBH=I.JLBH and I.HYKTYPE=D.HYKTYPE");
            query.SQL.Add("   group by (L.DJR,L.DJRMC,I.HYKTYPE,D.HYKNAME)");
            query.Open();
            while (!query.Eof)
            {
                object item = assembly.CreateInstance("DynamicClass");
                ReflectionSetProperty(item, "iDJR", query.FieldByName("DJR").AsInteger);
                ReflectionSetProperty(item, "sDJRMC", query.FieldByName("DJRMC").AsString);
                ReflectionSetProperty(item, "iHYKTYPE", query.FieldByName("HYKTYPE").AsInteger);
                ReflectionSetProperty(item, "iSL", query.FieldByName("SL").AsInteger);
                ReflectionSetProperty(item, "sHYKNAME", query.FieldByName("HYKNAME").AsString);
                foreach (ZFFS item_zffs in list)
                {
                    ReflectionSetProperty(item, "f" + item_zffs.iZFFSID + "", query.FieldByName("" + item_zffs.iZFFSID + "").AsFloat);
                }
                lst.Add(item);
            }


            return lst;
        }

        public static Assembly NewAssembly(List<ZFFS> list)
        {
            Microsoft.CSharp.CSharpCodeProvider provider = new CSharpCodeProvider();
            System.CodeDom.Compiler.CompilerParameters param = new CompilerParameters();
            param.GenerateExecutable = false;
            param.GenerateInMemory = true;
            StringBuilder classSource = new StringBuilder();
            classSource.Append("public class DynamicClass \n");
            classSource.Append("{ \n");
            classSource.Append(propertyString("iDJR", "int"));
            classSource.Append(propertyString("sDJRMC", "string"));
            classSource.Append(propertyString("iHYKTYPE", "int"));
            classSource.Append(propertyString("sHYKNAME", "string"));
            classSource.Append(propertyString("iSL", "int"));
            foreach (ZFFS item in list)
            {
                classSource.Append(propertyString(item.iZFFSID.ToString(), "Double"));
            }
            classSource.Append("\n}");
            System.Diagnostics.Debug.WriteLine(classSource.ToString());
            System.CodeDom.Compiler.CompilerResults result = provider.CompileAssemblyFromSource(param, classSource.ToString());
            Assembly assembly = result.CompiledAssembly;
            return assembly;

        }

        public static string propertyString(string propertyName, string fieldType)
        {
            StringBuilder sbProperty = new StringBuilder();
            sbProperty.Append(" private   " + fieldType + "   _f" + propertyName + "   =   0;\n");
            sbProperty.Append(" public   " + fieldType + "   " + "f" + propertyName + "\n");
            sbProperty.Append(" {\n");
            sbProperty.Append(" get{   return   _f" + propertyName + ";}   \n");
            sbProperty.Append(" set{   _f" + propertyName + "   =   value;   }\n");
            sbProperty.Append(" }");
            return sbProperty.ToString();
        }

        public static void ReflectionGetProperty(object objClass, string propertyName)
        {
            PropertyInfo[] infos = objClass.GetType().GetProperties();
            foreach (PropertyInfo info in infos)
            {
                if (info.Name == propertyName && info.CanRead)
                {
                    System.Console.WriteLine(info.GetValue(objClass, null));
                }
            }
        }

        public static void ReflectionSetProperty(object objClass, string propertyName, int value)
        {
            PropertyInfo[] infos = objClass.GetType().GetProperties();
            foreach (PropertyInfo info in infos)
            {
                if (info.Name == propertyName && info.CanWrite)
                {
                    info.SetValue(objClass, value, null);
                }
            }
        }

        public static void ReflectionSetProperty(object objClass, string propertyName, string value)
        {
            PropertyInfo[] infos = objClass.GetType().GetProperties();
            foreach (PropertyInfo info in infos)
            {
                if (info.Name == propertyName && info.CanWrite)
                {
                    info.SetValue(objClass, value, null);
                }
            }
        }
        public static void ReflectionSetProperty(object objClass, string propertyName, double value)
        {
            PropertyInfo[] infos = objClass.GetType().GetProperties();
            foreach (PropertyInfo info in infos)
            {
                if (info.Name == propertyName && info.CanWrite)
                {
                    info.SetValue(objClass, value, null);
                }
            }
        }

    }

}

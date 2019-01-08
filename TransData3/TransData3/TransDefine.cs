using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace TransData3
{
    public class TransDefine
    {
        public string TblName = string.Empty;
        public string KeyFld = string.Empty;
        public string TMFld = string.Empty;
        public int TMType = 0;
        public string Stamp = string.Empty;
        public int Stamp_Int = 0;
        public string Stamp_Str = string.Empty;
    }
    public class TransTools
    {
        public static string PostFileToServer(string Server, string file)
        {
            FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read);
            byte[] buffur = new byte[fs.Length];
            fs.Read(buffur, 0, (int)fs.Length);
            fs.Close();
            return Upload(Server, "file", file, buffur, 0, buffur.Length);
        }

        public static string Upload(string uriStr, string name, string fileName, byte[] data, int offset, int count)
        {
            try
            {
                var request = WebRequest.Create(uriStr);
                request.Method = "POST";
                var boundary = $"******{DateTime.Now.Ticks}***";
                request.ContentType = $"multipart/form-data; boundary={boundary}";
                boundary = $"--{boundary}";
                using (var requestStream = request.GetRequestStream())
                {
                    var buffer = Encoding.ASCII.GetBytes(boundary + Environment.NewLine);
                    requestStream.Write(buffer, 0, buffer.Length);
                    buffer = Encoding.UTF8.GetBytes($"Content-Disposition: form-data; name=\"{name}\"; filename=\"{fileName}\"{Environment.NewLine}");
                    requestStream.Write(buffer, 0, buffer.Length);
                    buffer = Encoding.ASCII.GetBytes($"Content-Type: application/octet-stream{Environment.NewLine}{Environment.NewLine}");
                    requestStream.Write(buffer, 0, buffer.Length);
                    requestStream.Write(data, offset, count);
                    buffer = Encoding.ASCII.GetBytes($"{Environment.NewLine}{boundary}--");
                    requestStream.Write(buffer, 0, buffer.Length);
                }
                using (var response = request.GetResponse())
                using (var responseStream = response.GetResponseStream())
                using (var streamReader = new StreamReader(responseStream))
                {
                    return streamReader.ReadToEnd();
                }
            }
            catch
            {
                return null;
            }
        }
    }
}

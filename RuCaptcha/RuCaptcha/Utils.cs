using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RuCaptcha
{
    class Utils
    {
        public static string GetStringIntoStream(Stream s)
        {
            
            return Encoding.UTF8.GetString(GetByteArrayIntoStream(s));
        }
        public static byte[] GetByteArrayIntoStream(Stream s)
        {
            byte[] arr = new byte[1024 * 1024];
            s.Read(arr, 0, arr.Length);
            List<byte> lb = new List<byte>();
            foreach (byte b in arr)
            {
                if (b != default(byte)) lb.Add(b);
            }
            return lb.ToArray();
        }
        public static void WriteStringIntoStream(Stream s, string str)
        {
            byte[] arrStr = Encoding.UTF8.GetBytes(str);
            s.Write(arrStr, 0, arrStr.Length);
        }
        public static string GetRequest(string rUrl)
        {
            string html = string.Empty;
            string url = rUrl;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }
            return html;
        }
    }
}

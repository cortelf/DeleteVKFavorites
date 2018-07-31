using EasyHttp;
using EasyHttp.Http;
using System;
using System.Collections.Generic;
using RuCaptcha.HttpClasses;
using RuCaptcha.HttpClasses.Enums;
using RuCaptcha.JsonClasses;
using System.Net;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using System.Drawing;

namespace RuCaptcha
{

	public class RuCaptcha
	{
		protected string token;
		public RuCaptcha(string token) {
			this.token = token;
		}
		public long SendCaptcha(Uri path) {
            var client = new WebClient();
            client.DownloadFile(path.AbsoluteUri, "captcha.jpg");
            FileStream fileStream = new FileStream("captcha.jpg", FileMode.Open);
            fileStream.Close();
            string base64Img;
            using (Image image = Image.FromFile("captcha.jpg"))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();
                    base64Img = Convert.ToBase64String(imageBytes);
                }
            }
            File.Delete("captcha.jpg");
            
            Sender sender = new Sender();
            sender.body = base64Img;
            sender.key = token;
            sender.method = Methods.base64;
            Console.WriteLine(sender.body);
            var httpCaptcha = new HttpClient();
            var res = client.UploadValues("http://rucaptcha.com/in.php", sender.GetNameValue());
            string json = Encoding.UTF8.GetString(res);
            ResponseSend rs = JsonConvert.DeserializeObject<ResponseSend>(json);
            return rs.request;
        }
        public string GetCaptcha(long id)
        {
            Getter getter = new Getter();
            getter.id = id;
            getter.key = token;
            var resId = new HttpClient().Get("http://rucaptcha.com/res.php", getter);
            var captcha = resId.StaticBody<CaptchaGet>();
            return captcha.request;
        }
	}
}
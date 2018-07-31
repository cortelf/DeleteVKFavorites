using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RuCaptcha.HttpClasses.Enums;

namespace RuCaptcha.HttpClasses
{
    class Sender
    {
        public string key { get; set; }
        public Methods method { get; set; }
        public int json { get; set; }
        public int numeric { get; set; }
        public string body { get; set; }
        public Sender()
        {
            json = 1;
            numeric = 4;
        }
        public NameValueCollection GetNameValue()
        {
            NameValueCollection nameValue = new NameValueCollection();
            nameValue["key"] = this.key;
            nameValue["method"] = this.method.ToString();
            nameValue["json"] = this.json.ToString();
            nameValue["numeric"] = this.numeric.ToString();
            nameValue["body"] = this.body;
            return nameValue;
        }
    }
}

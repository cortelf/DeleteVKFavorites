﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuCaptcha.HttpClasses
{
    class Getter
    {
        public string key { get; set; }
        public string action { get; set; }
        public long id { get; set; }
        public int json { get; set; }

        public Getter()
        {
            json = 1;
            action = "get";
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iHuaban.App.Models
{
    public class Pin : IModel
    {
        public long pin_id { set; get; }
        public long user_id { set; get; }
        public long board_id { set; get; }
        public long file_id { set; get; }
        public int media_type { set; get; }
        public string source { set; get; }
        public string link { set; get; }
        public string raw_text { set; get; }
        public long via { set; get; }
        public long via_user_id { set; get; }
        public long original { set; get; }
        public long created_at { set; get; }
        public int like_count { set; get; }
        public int comment_count { set; get; }
        public int repin_count { set; get; }
        public long PinId => pin_id;

        public File file { set; get; }

        public string KeyId => pin_id.ToString();
    }
}

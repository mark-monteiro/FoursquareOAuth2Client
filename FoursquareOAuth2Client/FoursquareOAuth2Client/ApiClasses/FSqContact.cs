﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoursquareOAuth2Client.ApiClasses
{
    public class FSqContact
    {
        public string phone { get; set; }
        public string formattedPhone { get; set; }
        public string email { get; set; }
        public string twitter { get; set; }
        public string facebook { get; set; }
    }
}

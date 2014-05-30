using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoursquareOAuth2Client.ApiClasses
{
    public class FSqCheckins
    {
        public int count { get; set; }
        public List<UnparsedJSONField> items { get; set; }
    }
}

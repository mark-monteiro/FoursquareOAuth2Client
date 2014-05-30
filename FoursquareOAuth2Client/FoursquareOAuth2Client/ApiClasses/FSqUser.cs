using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoursquareOAuth2Client.ApiClasses
{
    public class FSqUser
    {
        public string id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string gender { get; set; }
        //public FSqPhoto photo { get; set; }
        //public Friends friends { get; set; }
        //public Tips tips { get; set; }
        public string homeCity { get; set; }
        public string bio { get; set; }
        public FSqContact contact { get; set; }
        public string checkinPings { get; set; }
        public bool pings { get; set; }
        public string type { get; set; }
        //public FSqVenue venue { get; set; }
        //public Badges badges { get; set; }
        //public Mayorships mayorships { get; set; }
        public FSqCheckins checkins { get; set; }
        //public Following following { get; set; }
        //public Requests requests { get; set; }
        //public Lists lists { get; set; }            
        //public Scores scores { get; set; }
        public int createdAt { get; set; }
        public string referralId { get; set; }

        //all ignored segments will go here (disable 'field not used' warning)
        [JsonExtensionData(ReadData = false, WriteData = false)]
#pragma warning disable 0169
        private IDictionary<string, JToken> _additionalData;
#pragma warning restore 0169

        /// <summary>
        /// Returns if the 'type' field has a value of venuePage
        /// </summary>
        public bool isVenue()
        {
            return this.type == "venuePage";
        }
    }
}

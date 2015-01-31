using HighSpeedRail.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HighSpeedRail.Models
{
    public class CanibetDetailModel
    {
        public long CanibetDetailID { get; set; }

        public long CanibetID { get; set; }

        public DetailTypeEnum Type { get; set; }

        public string MediaType { get; set; }

        public string Directory { get; set; }

        public string FileName { get; set; }

        public string Announcement { get; set; }

        public string Css { get; set; }
    }
}
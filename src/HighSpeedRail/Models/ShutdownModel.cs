using HighSpeedRail.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HighSpeedRail.Models
{
    public class ShutdownModel
    {
        public long ID { get; set; }

        public string IP { get; set; }

        public ClientActionEnum Action { get; set; }
    }
}
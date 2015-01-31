using HighSpeedRail.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HighSpeedRail.Enum
{
    public enum DetailTypeEnum
    {
        [LocalizedDescription("DetailType_Announcement", typeof(Resources))]
        Announcement = 0,
        [LocalizedDescription("DetailType_Picture", typeof(Resources))]
        Picture = 1,
        [LocalizedDescription("DetailType_Video", typeof(Resources))]
        Video = 2
    }
}
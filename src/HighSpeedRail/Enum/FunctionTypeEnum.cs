using HighSpeedRail.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HighSpeedRail.Enum
{
    public enum FunctionTypeEnum
    {
        NONE = 0,
        [LocalizedDescription("Today_PreSale", typeof(Resources))]
        Today_PreSale = 1,
        [LocalizedDescription("Elderly", typeof(Resources))]
        Elderly = 2,
        [LocalizedDescription("Bussiness", typeof(Resources))]
        Bussiness = 3,
        [LocalizedDescription("Regular", typeof(Resources))]
        Regular = 4,
        [LocalizedDescription("Enterprise", typeof(Resources))]
        Enterprise = 5
    }
}
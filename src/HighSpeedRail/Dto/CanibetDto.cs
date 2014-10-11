using HighSpeedRail.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HighSpeedRail.Dto
{
    public class CanibetDto
    {
        public int ID { get; set; }

        public FunctionTypeEnum FunctionType { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UPdateDate { get; set; }
    }
}
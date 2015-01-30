using HighSpeedRail.Entity;
using HighSpeedRail.Enum;
using System;
using System.Collections.Generic;

namespace HighSpeedRail.Models
{
    public class CanibetModel
    {
        public long ID { get; set; }

        public string FunctionType { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UPdateDate { get; set; }

        public bool IsUse { get; set; }

    }

    public class CanibetUpdateModel
    {
        public long ID { get; set; }

        public FunctionTypeEnum FunctionType { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UPdateDate { get; set; }

        public bool IsUse { get; set; }

        public DetailTypeEnum DetailType { get; set; }

        public string Announcement { get; set; }
    }

    public class CanibetCurrentModel
    {
        public string SelectedFunctionType { get; set; }

        public int SelectedCanibetID { get; set; }
    }

    public class CanibetIndexModel
    {
        public bool isUsing { get; set; }

        public string CurrentFunctionType  { get; set; }

        public int CurrentCanibetID { get; set; }
    }
}
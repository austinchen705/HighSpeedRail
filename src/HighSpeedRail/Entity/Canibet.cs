using HighSpeedRail.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HighSpeedRail.Entity
{
    [Table("Canibet")]
    public class Canibet
    {
        [Key]
        public long ID { get; set; }

        public FunctionTypeEnum FunctionType { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UPdateDate { get; set; }

        public bool IsUse { get; set; }

        public DetailTypeEnum DetailType { get; set; }

        public string IP { get; set; }

        public virtual ICollection<CanibetDetail> CanibetDetails { get; set; }
    }
}
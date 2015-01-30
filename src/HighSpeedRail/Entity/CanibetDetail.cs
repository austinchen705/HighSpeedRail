using HighSpeedRail.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HighSpeedRail.Entity
{
    [Table("CanibetDetail")]
    public class CanibetDetail
    {
        [Key]
        public long CanibetDetailID { get; set; }

        [ForeignKey("Canibet")]
        public long CanibetID { get; set; }

        public DetailTypeEnum Type { get; set; }

        public string MediaType { get; set; }

        public string Directory { get; set; }

        public string FileName { get; set; }

        public string Announcement { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public virtual Canibet Canibet { get; set; }
    }
}
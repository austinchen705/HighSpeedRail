using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HighSpeedRail.Entity
{
    [Table("User")]
    public class User
    {
        [Key]
        public long UserID { get; set; }

        public string UserCode { get; set; }

        public string Password { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }
    }
}
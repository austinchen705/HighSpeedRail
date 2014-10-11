using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HighSpeedRail.Dto
{
    public class UserDto
    {
        public int UserID { get; set; }

        public string UserCode { get; set; }

        public string Password { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }
    }
}
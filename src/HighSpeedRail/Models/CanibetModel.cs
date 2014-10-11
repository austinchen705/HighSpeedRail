using HighSpeedRail.Dto;
using HighSpeedRail.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HighSpeedRail.Models
{
    public class CanibetModel
    {

    }

    public class CanibetUpdateModel
    {
        public List<CanibetDto> CaniBets { get; set; }

        public FunctionTypeEnum SelectedFunctionType { get; set; }

        public int SelectedCanibetID { get; set; }
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
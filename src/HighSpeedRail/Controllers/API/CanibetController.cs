using HighSpeedRail.DAO;
using HighSpeedRail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HighSpeedRail.Enum;

namespace HighSpeedRail.Controllers.API
{
    public class CanibetController : ApiController
    {
        private CanibetDAO _canibetDao = new CanibetDAO();
        protected override void Dispose(bool disposing)
        {
            _canibetDao.Dispose();
            base.Dispose(disposing);
        }
        [HttpGet]
        public CanibetIndexModel GetUsingCanibet()
        {
            var dto = _canibetDao.GetUsingCanibet();
            CanibetIndexModel model = new CanibetIndexModel();
            if (dto != null)
            {
                model.isUsing = true;
                model.CurrentCanibetID = dto.ID;
                model.CurrentFunctionType = dto.FunctionType.GetDescription();
            }
            else
            {
                model.isUsing = false;
            }

            return model;
        }
    }
}

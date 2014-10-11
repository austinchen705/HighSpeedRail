using HighSpeedRail.DAO;
using HighSpeedRail.Dto;
using HighSpeedRail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HighSpeedRail.Util;
using HighSpeedRail.Enum;

namespace HighSpeedRail.Controllers
{
    public class CanibetController : Controller
    {
        private CanibetDAO _canibetDao = new CanibetDAO();

        protected override void Dispose(bool disposing)
        {
            _canibetDao.Dispose();

            base.Dispose(disposing);
        }
        // GET: Canibet
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Index()
        {
            var usingCanibet = _canibetDao.GetUsingCanibet();
            var model = new CanibetIndexModel();

            if (usingCanibet != null)
            {

                model.isUsing = true;
                model.CurrentCanibetID = usingCanibet.ID;
                model.CurrentFunctionType = usingCanibet.FunctionType.GetDescription();
            }
            else
                model.isUsing = false;

            return View(model);
        }

        [Authorize]
        public ActionResult Manage()
        {
            if (Session["CanibetID"] != null)
            {
                // int canibetID = Session["CanibetID"].ToString().ParseIntOrDefault();
                // var canibet = _canibetDao.GetCanibet(canibetID);
                var f = (FunctionTypeEnum)System.Enum.Parse(typeof(FunctionTypeEnum), Session["FunctionType"].ToString());
                var model = new CanibetCurrentModel()
                {
                    SelectedCanibetID = Session["CanibetID"].ToString().ParseIntOrDefault(),
                    SelectedFunctionType = f.GetDescription(),

                };
                return View(model);
            }
            else
                return RedirectToAction("UpdateCanibet");
        }

        [Authorize]
        public ActionResult UpdateCanibet()
        {
            var list = _canibetDao.GetCanibetList();
            var model = new CanibetUpdateModel()
            {
                CaniBets = list
            };


            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult UpdateCanibet(CanibetUpdateModel model)
        {
            Session["CanibetID"] = model.SelectedCanibetID;
            Session["FunctionType"] = model.SelectedFunctionType;
            _canibetDao.UpdateCanibet(model.SelectedCanibetID, (int)model.SelectedFunctionType);

            CanibetIndexModel indexModel = new CanibetIndexModel()
            {
                isUsing = true,
                CurrentFunctionType = model.SelectedFunctionType.GetDescription(),
                CurrentCanibetID = model.SelectedCanibetID
            };
            Microsoft.AspNet.SignalR.IHubContext context = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<CanibetHub>();
            context.Clients.All.broadcastMessage(indexModel);

            return RedirectToAction("Manage");
        }
    }
}
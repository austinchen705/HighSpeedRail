using HighSpeedRail.DAO;
using HighSpeedRail.DBContext;
using HighSpeedRail.Dto;
using HighSpeedRail.Entity;
using HighSpeedRail.Enum;
using HighSpeedRail.Models;
using HighSpeedRail.Util;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HighSpeedRail.Controllers
{
    public class CanibetController : Controller
    {
        private HSRContext _hsrDb = new HSRContext();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _hsrDb.Dispose();
            }

            base.Dispose(disposing);
        }
        // GET: Canibet
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> Index(int caniBetID, CancellationToken cancelllationToken)
        {
            var usingCanibet = await _hsrDb.Canibets.SingleOrDefaultAsync(c => c.ID == caniBetID, cancelllationToken);
            var model = new CanibetIndexModel();

            if (usingCanibet != null)
            {

                model.isUsing = true;
                model.CurrentCanibetID = 0;
                model.CurrentFunctionType = usingCanibet.FunctionType.GetDescription();
            }
            else
                model.isUsing = false;

            return View(model);
        }

        [Authorize]
        public ActionResult Manage()
        {
            //if (Session["CanibetID"] != null)
            //{
            //    // int canibetID = Session["CanibetID"].ToString().ParseIntOrDefault();
            //    // var canibet = _canibetDao.GetCanibet(canibetID);
            //    var f = (FunctionTypeEnum)System.Enum.Parse(typeof(FunctionTypeEnum), Session["FunctionType"].ToString());
            //    var model = new CanibetCurrentModel()
            //    {
            //        SelectedCanibetID = Session["CanibetID"].ToString().ParseIntOrDefault(),
            //        SelectedFunctionType = f.GetDescription(),

            //    };
            //    return View(model);
            //}
            //else
            //    return RedirectToAction("UpdateCanibet");

            return View();
        }

        [Authorize]
        public async Task<ActionResult> UpdateCanibet(int ID, CancellationToken cancelllationToken)
        {
            var canibet = await _hsrDb.Canibets.SingleAsync(c => c.ID == ID, cancelllationToken);
            //var model = new CanibetUpdateModel()
            //{
            //    CaniBets = canibet
            //};


            return View(canibet);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> UpdateCanibet(Canibet model, CancellationToken cancelllationToken)
        {
          //  _canibetDao.UpdateCanibet(model.SelectedCanibetID, (int)model.SelectedFunctionType);
            //var canibet = await _hsrDb.Canibets.SingleOrDefaultAsync(c => c.ID == model.SelectedCanibetID);

            //if (canibet != null)
            //{
            //    canibet.FunctionType = model.SelectedFunctionType;
            //    await _hsrDb.SaveChangesAsync(cancelllationToken);

            //    CanibetIndexModel indexModel = new CanibetIndexModel()
            //    {
            //        isUsing = true,
            //        CurrentFunctionType = model.SelectedFunctionType.GetDescription(),
            //        CurrentCanibetID = model.SelectedCanibetID
            //    };
            //    Microsoft.AspNet.SignalR.IHubContext context = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<CanibetHub>();
            //    context.Clients.All.broadcastMessage(indexModel);
            //}

            //return RedirectToAction("Manage");
            return null;
        }
    }
}
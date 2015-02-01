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
    [RoutePrefix("Canibet")]
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
        [Route("Index/{id:int}")]
        public async Task<ActionResult> Index(int id, CancellationToken cancelllationToken)
        {
            var canibet = await _hsrDb.Canibets.SingleOrDefaultAsync(c => c.ID == id, cancelllationToken);
            var model = new CanibetIndexModel();

            if (canibet != null)
            {
                model.isUsing = true;
                model.ID = canibet.ID;
                model.FunctionType = canibet.FunctionType.GetDescription();
                model.DetailType = canibet.DetailType.ToString();
            }
            else
                model.isUsing = false;

            return View(model);
        }

        [Authorize]
        public ActionResult Manage()
        {
            return View();
        }

        [Authorize]
        public async Task<ActionResult> UpdateCanibet(int ID, CancellationToken cancelllationToken)
        {
            var canibet = await _hsrDb.Canibets.SingleAsync(c => c.ID == ID, cancelllationToken);

            return View(canibet);
        }
    }
}
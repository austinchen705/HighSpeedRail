using HighSpeedRail.DAO;
using HighSpeedRail.DBContext;
using HighSpeedRail.Entity;
using HighSpeedRail.Enum;
using HighSpeedRail.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;
using System.Web.Http;

namespace HighSpeedRail.Controllers.API
{
    [RoutePrefix("api/Canibet")]
    public class CanibetController : ApiController
    {
        private HSRContext _hsrDb = new HSRContext();
        static readonly ILog _log = LogManager.GetLogger(typeof(CanibetController));
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _hsrDb.Dispose();
            }

            base.Dispose(disposing);
        }
        //[HttpGet]
        //public CanibetIndexModel GetUsingCanibet()
        //{
        //    var dto = _canibetDao.GetUsingCanibet();
        //    CanibetIndexModel model = new CanibetIndexModel();
        //    if (dto != null)
        //    {
        //        model.isUsing = true;
        //        model.CurrentCanibetID = dto.ID;
        //        model.CurrentFunctionType = dto.FunctionType.GetDescription();
        //    }
        //    else
        //    {
        //        model.isUsing = false;
        //    }

        //    return model;
        //}

        [HttpGet]
        [HttpPost]
        public async Task<dynamic> GetAllCanibet(CancellationToken cancellationToken)
        {
            _hsrDb.Configuration.LazyLoadingEnabled = false;
            List<CanibetModel> resobj = new List<CanibetModel>();
            var canibets = await _hsrDb.Canibets.ToListAsync(cancellationToken);

            foreach (var canibet in canibets)
            {
                resobj.Add(new CanibetModel()
                {
                    ID = canibet.ID,
                    FunctionType = canibet.FunctionType.GetDescription(),
                    DetailType = canibet.DetailType.GetDescription(),
                });
            }

            return resobj;

        }

        [HttpGet]
        public async Task<dynamic> GetCanibet(int id, CancellationToken cancellationToken)
        {
            _hsrDb.Configuration.LazyLoadingEnabled = false;
            var canibet = await _hsrDb.Canibets.SingleOrDefaultAsync(c => c.ID == id, cancellationToken);

            if (canibet == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return canibet;

        }

        [HttpGet]
        [Route("getCanibetFile/{id:int}/{detailType:int}")]
        public async Task<dynamic> GetCanibetFile(int id, DetailTypeEnum detailType, CancellationToken cancellationToken)
        {
            _hsrDb.Configuration.LazyLoadingEnabled = false;
            var files = await _hsrDb.CanibetDetails.Where(c => c.Type == detailType && c.CanibetID == id).ToListAsync(cancellationToken);
                

            return files;

        }

        [HttpDelete]
        [Route("delCanibetFile/{id:int}")]
        public async Task<dynamic> DeleteCanibetFile(int id, CancellationToken cancellationToken)
        {
            _hsrDb.Configuration.LazyLoadingEnabled = false;
            int result = 0;
            var file = await _hsrDb.CanibetDetails.SingleOrDefaultAsync(c => c.CanibetDetailID == id, cancellationToken);
            if (file != null)
            {
                _hsrDb.CanibetDetails.Remove(file);
                result = await _hsrDb.SaveChangesAsync(cancellationToken);
            }

            return result;

        }

        [HttpPost]
        [Route("uploadFile/{id:int}/{detailType:int}")]
        public async Task UploadFile(int id, DetailTypeEnum detailType, CancellationToken cancellationToken)
        {
            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                // Get the uploaded image from the Files collection
                var httpPostedFile = HttpContext.Current.Request.Files["UploadedFile"];

                if (httpPostedFile != null)
                {
                    // Validate the uploaded image(optional)
                    var guid = Guid.NewGuid();
                    // Get the complete file path
                    string directory = string.Empty;
                    switch (detailType)
                    {
                        case DetailTypeEnum.Picture:
                            directory = Path.Combine(HttpContext.Current.Server.MapPath(string.Format("~/UploadedFiles/{0}/Picture/{1}", id, guid.ToString())));
                            break;
                        case DetailTypeEnum.Video:
                            directory = Path.Combine(HttpContext.Current.Server.MapPath(string.Format("~/UploadedFiles/{0}/Video/{1}", id, guid.ToString())));
                            break;
                    }
                     
                    if (!Directory.Exists(directory))
                        Directory.CreateDirectory(directory);
                    var fileSavePath = string.Format("{0}/{1}", directory, httpPostedFile.FileName);

                    // Save the uploaded file to "UploadedFiles" folder
                    httpPostedFile.SaveAs(fileSavePath);
                    _hsrDb.CanibetDetails.Add(new CanibetDetail()
                    {
                        CanibetID = id,
                        CreateDate = DateTime.Now,
                        UpdateDate = DateTime.Now,
                        Directory = guid.ToString(),
                        FileName = httpPostedFile.FileName,
                        
                        Type = detailType,
                        MediaType = httpPostedFile.ContentType,
                    });
                    await _hsrDb.SaveChangesAsync(cancellationToken);
                }
            }
        }

        [HttpPost]
        public async Task<int> UpdateCanibet(CanibetUpdateModel model, CancellationToken cancelllationToken)
        {
            _hsrDb.Configuration.LazyLoadingEnabled = false;
            var canibet = await _hsrDb.Canibets.SingleOrDefaultAsync(c => c.ID == model.ID, cancelllationToken);
            var result = 0;
            if (canibet == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            canibet.DetailType = model.DetailType;
            canibet.FunctionType = model.FunctionType;
            canibet.UPdateDate = DateTime.Now;

            if (model.DetailType == DetailTypeEnum.Announcement) 
            {
                var canibetAnnouncement = await _hsrDb.CanibetDetails.SingleOrDefaultAsync(c => c.Type == DetailTypeEnum.Announcement && c.CanibetID == model.ID, cancelllationToken);
                if (canibetAnnouncement == null)
                    _hsrDb.CanibetDetails.Add(new CanibetDetail()
                    {
                        CanibetID = model.ID,
                        Announcement = model.Announcement,
                        CreateDate = DateTime.Now,
                        Directory = string.Empty,
                        Type = DetailTypeEnum.Announcement,
                        UpdateDate = DateTime.Now,
                        FileName = string.Empty,
                        MediaType = string.Empty
                    });
                else
                {
                    canibetAnnouncement.Announcement = model.Announcement;
                    canibetAnnouncement.UpdateDate = DateTime.Now;
                }
            }

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                result = await _hsrDb.SaveChangesAsync(cancelllationToken);
                scope.Complete();
            }

            return result;

        }

        [HttpGet]
        public async Task<HttpResponseMessage> Test()
        {
            var canibet = await _hsrDb.Canibets.SingleOrDefaultAsync(c => c.ID == 1);

            return new HttpResponseMessage()
            {
                 Content = new StringContent("test")
            };
        }
    }
}

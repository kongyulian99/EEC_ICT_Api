using EEC_ICT.Data.Common;
using EEC_ICT.Data.Core;
using EEC_ICT.Data.Models;
using EEC_ICT.Data.Services;
using System;
using System.Linq;
using System.Web.Http;

namespace EEC_ICT.Api.Controllers
{
    [RoutePrefix("api/logs")]
    public class LogsController : BaseController
    {
        [HttpGet]
        [Route("selectbyfilter")]
        public object SelectByFilter(string keyword, DateTime tungay, DateTime denngay, int pageindex, int pagesize)
        {
            Logger.Info("[SelectTopNews_News]");

            var retval = new ReturnInfo();
            retval.Status = new StatusReturn { Message = "Không thành công", Code = 0 };
            retval.Data = null;
            retval.Pagination = new PaginationInfo() { TotalRows = 0, PageIndex = pageindex, PageSize = pagesize };
            try
            {
                var data = LogsServices.SelectByFilter(keyword, tungay, denngay, pageindex, pagesize);
                var lstEntity = data;
                retval.Pagination.TotalRows = data.Count() > 0 ? data[0].TotalRows : 0;
                retval.Status.Message = "Thành công";
                retval.Status.Code = 1;
                retval.Data = lstEntity;
            }
            catch (Exception ex)
            {
                retval.Status.Message = ex.Message;
                retval.Status.Code = -1;
            }

            Logger.Info("[retval]" + retval.JSONSerializer());
            return retval;
        }

        [HttpGet]
        [Route("selectone/{id}")]
        public object SelectOne(int id)
        {
            Logger.Info("[SelectOne_News]");

            var retval = new ReturnInfo();
            retval.Status = new StatusReturn { Message = "Không thành công", Code = 0 };
            retval.Data = null;
            retval.Pagination = new PaginationInfo { PageIndex = 0, PageSize = 0, TotalRows = 0 };
            try
            {
                var data = LogsServices.SelectOne(id);
                retval.Status.Message = "Thành công";
                retval.Status.Code = 1;
                retval.Pagination.TotalRows = 1;
                retval.Data = data;
            }
            catch (Exception ex)
            {
                retval.Status.Message = ex.Message;
                retval.Status.Code = -1;
            }

            Logger.Info("[retval]" + retval.JSONSerializer());
            return retval;
        }

        [HttpPost]
        [Route("Insert")]
        public object Insert(Logs entity)
        {
            Logger.Info("[Insert_News]");
            var retval = new ReturnInfo();
            retval.Status = new StatusReturn { Message = "Không thành công", Code = 0 };
            retval.Data = null;
            retval.Pagination = new PaginationInfo { PageIndex = 0, PageSize = 0, TotalRows = 0 };
            try
            {
                var lstEntity = LogsServices.Insert(entity);
                retval.Status.Message = "Thành công";
                retval.Status.Code = 1;
                retval.Data = lstEntity;
            }
            catch (Exception ex)
            {
                retval.Status.Message = ex.Message;
                retval.Status.Code = -1;
            }

            Logger.Info("[retval]" + retval.JSONSerializer());
            return retval;
        }

        [HttpPost]
        [Route("Update")]
        public object Update(Logs entity)
        {
            Logger.Info("[Update_News]");
            var retval = new ReturnInfo();
            retval.Status = new StatusReturn { Message = "Không thành công", Code = 0 };
            retval.Data = null;
            retval.Pagination = new PaginationInfo { PageIndex = 0, PageSize = 0, TotalRows = 0 };
            try
            {
                var lstEntity = LogsServices.Update(entity);
                retval.Status.Message = "Thành công";
                retval.Status.Code = 1;
                retval.Data = lstEntity;
            }
            catch (Exception ex)
            {
                retval.Status.Message = ex.Message;
                retval.Status.Code = -1;
            }
            Logger.Info("[retval]" + retval.JSONSerializer());
            return retval;
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public object Delete(int id)
        {
            Logger.Info("[Delete_Logs]");
            var retval = new ReturnInfo();
            retval.Status = new StatusReturn { Message = "Không thành công", Code = 0 };
            retval.Data = null;
            retval.Pagination = new PaginationInfo { PageIndex = 0, PageSize = 0, TotalRows = 0 };
            try
            {
                LogsServices.Delete(id);
                retval.Data = id;
                retval.Status = new StatusReturn { Message = "Xóa thành công ", Code = 1 };
            }
            catch (Exception ex)
            {
                retval.Status = new StatusReturn { Message = ex.Message, Code = -1 };
            }

            Logger.Info("[retval]" + retval.JSONSerializer());
            return retval;
        }
    }
}
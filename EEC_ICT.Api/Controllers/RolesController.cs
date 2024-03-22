using EEC_ICT.Api.Constants;
using EEC_ICT.Api.Providers;
using EEC_ICT.Data.Common;
using EEC_ICT.Data.Core;
using EEC_ICT.Data.Models;
using EEC_ICT.Data.Services;
using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace EEC_ICT.Api.Controllers
{
    [RoutePrefix("api/roles")]
    public class RolesController : BaseController
    {
        [HttpGet]
        [Route("selectall")]
        //[Permission(Command = CommandConstants.VIEW, Function = FunctionConstants.ROLE)]
        [Permission]
        public ReturnInfo SelectAll(string filter, int pageIndex, int pageSize)
        {
            Logger.Info("[SelectAllRoles]");

            var retval = new ReturnInfo();
            int totalRow = 0;
            retval.Status = new StatusReturn { Message = "Không thành công", Code = 0 };
            retval.Data = null;
            retval.Pagination = new PaginationInfo { PageIndex = pageIndex, PageSize = pageSize, TotalRows = 0 };
            try
            {
                //var data = RoleServices.SelectAll().Where(o=>o.Name.ToLower()!="root").ToList();
                var data = RoleServices.SelectAll();
                var lstEntity = data;
                if (!string.IsNullOrEmpty(filter))
                    lstEntity = data.Where(x => x.Name.IndexOf(filter, StringComparison.CurrentCultureIgnoreCase)>=0).ToList();
                totalRow = lstEntity.Count();
                if (pageSize > 0)
                {
                    lstEntity = lstEntity.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                }
                retval.Status.Message = "Thành công";
                retval.Status.Code = 1;
                retval.Pagination.TotalRows = totalRow;
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
        [Route("selectallbypermission/{userid}")]
        [Permission]
        public object SelectAllByPermission(string userid,string filter, int pageIndex, int pageSize)
        {
            Logger.Info("[SelectAllByPermissionRoles]");

            var retval = new ReturnInfo();
            int totalRow = 0;
            retval.Status = new StatusReturn { Message = "Không thành công", Code = 0 };
            retval.Data = null;
            retval.Pagination = new PaginationInfo { PageIndex = pageIndex, PageSize = pageSize, TotalRows = 0 };
            try
            {
                var lstUsers = RoleServices.SelectAllByPermission(userid);
                var lstEntity = lstUsers;
                if (!string.IsNullOrEmpty(filter))
                    lstEntity = lstUsers.Where(o=>o.Name.IndexOf(filter, StringComparison.CurrentCultureIgnoreCase)>=0 || o.Description.IndexOf(filter, StringComparison.CurrentCultureIgnoreCase)>=0).ToList();
                totalRow = lstEntity.Count();
                if (pageSize > 0)
                {
                    lstEntity = lstEntity.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                }
                retval.Status.Message = "Thành công";
                retval.Status.Code = 1;
                retval.Pagination.TotalRows = totalRow;
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
        [Permission]
        public ReturnInfo SelectOne(int id)
        {
            Logger.Info("[SelectOneRoles]");

            var retval = new ReturnInfo();
            retval.Status = new StatusReturn { Message = "Không thành công", Code = 0 };
            retval.Data = null;
            retval.Pagination = new PaginationInfo { PageIndex = 0, PageSize = 0, TotalRows = 0 };
            try
            {
                var lstEntity = RoleServices.SelectOne(id);
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
        [Route("checkname")]
        [Permission]
        public object CheckName(Role request)
        {
            Logger.Info("[CheckNameRoles]");

            var retval = new ReturnInfo();
            retval.Status = new StatusReturn { Message = "Không thành công", Code = 0 };
            retval.Data = 0;
            retval.Pagination = new PaginationInfo { PageIndex = 0, PageSize = 0, TotalRows = 0 };

            try
            {
                var result = RoleServices.CheckName(request.Id, request.Name);
                retval.Status.Message = "Thành công";
                retval.Status.Code = 1;
                retval.Data = result;
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
        [Route("insert")]
        [Permission(Command = CommandConstants.CREATE, Function = FunctionConstants.ROLE)]
        public object Insert(HttpRequestMessage request, Role entity)
        {
            Logger.Info("[InsertRoles]");

            var retval = new ReturnInfo();
            retval.Status = new StatusReturn { Message = "Không thành công", Code = 0 };
            retval.Data = null;
            retval.Pagination = new PaginationInfo { PageIndex = 0, PageSize = 0, TotalRows = 0 };
            try
            {
                var idEntity = RoleServices.Insert(entity);
                retval.Status.Message = "Thêm mới thành công";
                retval.Status.Code = 1;
                retval.Data = idEntity;
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
        [Route("update")]
        [Permission(Command = CommandConstants.UPDATE, Function = FunctionConstants.ROLE)]
        public object Update(Role entity)
        {
            Logger.Info("[UpdateRoles]");

            var retval = new ReturnInfo();
            retval.Status = new StatusReturn { Message = "Không thành công", Code = 0 };
            retval.Data = null;
            retval.Pagination = new PaginationInfo { PageIndex = 0, PageSize = 0, TotalRows = 0 };
            try
            {
                var lstEntity = RoleServices.Update(entity);
                retval.Status.Message = "Cập nhật thành công";
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
        [Route("Delete")]
        [Permission(Command = CommandConstants.DELETE, Function = FunctionConstants.ROLE)]
        public object Delete([FromUri] int[] ids)
        {
            Logger.Info("[DeleteRoles]");

            var retval = new ReturnInfo();
            retval.Status = new StatusReturn { Message = "Không thành công", Code = 0 };
            retval.Data = null;
            retval.Pagination = new PaginationInfo { PageIndex = 0, PageSize = 0, TotalRows = 0 };
            try
            {
                foreach (var id in ids)
                {
                    RoleServices.Delete(id);
                };
                retval.Status.Message = "Xóa thành công " + ids.Length.ToString() + " bản ghi";
                retval.Status.Code = 1;
                retval.Data = "1";
            }
            catch (Exception ex)
            {
                retval.Status.Message = ex.Message;
                retval.Status.Code = -1;
            }
            Logger.Info("[retval]" + retval.JSONSerializer());
            return retval;
        }
    }
}
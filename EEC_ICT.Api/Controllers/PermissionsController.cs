using EEC_ICT.Api.Constants;
using EEC_ICT.Api.Providers;
using EEC_ICT.Data.Common;
using EEC_ICT.Data.Core;
using EEC_ICT.Data.Models;
using EEC_ICT.Data.Services;
using System;
using System.Web.Http;

namespace EEC_ICT.Api.Controllers
{
    [RoutePrefix("api/permissions")]
    public class PermissionsController : BaseController
    {
        [HttpGet]
        [Route("SelectByRoleId/{roleId}")]
        [Permission(Command = CommandConstants.VIEW, Function = FunctionConstants.PERMISSION)]
        public object SelectByRoleId(int roleId)
        {
            Logger.Info("[SelectPermissionsByRoleId]");

            var retval = new ReturnInfo();
            retval.Status = new StatusReturn { Message = "Không thành công", Code = 0 };
            retval.Data = null;
            retval.Pagination = new PaginationInfo { PageIndex = 0, PageSize = 0, TotalRows = 0 };
            try
            {
                var lstEntity = PermissionServices.SelectByRoleId(roleId);
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
        [Route("InsertWithRoleId/{roleId}")]
        [Permission(Command = CommandConstants.UPDATE, Function = FunctionConstants.PERMISSION)]
        public object InsertWithRoleId(int roleId, PermissionsInsertRequest permissions)
        {
            Logger.Info("[InsertWithRoleId]");

            var retval = new ReturnInfo();
            retval.Status = new StatusReturn { Message = "Không thành công", Code = 0 };
            retval.Data = null;
            retval.Pagination = new PaginationInfo { PageIndex = 0, PageSize = 0, TotalRows = 0 };
            try
            {
                PermissionServices.DeleteWithRoleId(roleId);
                foreach (var permission in permissions.Permissions)
                {
                    PermissionServices.Insert(permission);
                }

                retval.Status.Message = "Gán quyền thành công ";
                retval.Status.Code = 1;
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
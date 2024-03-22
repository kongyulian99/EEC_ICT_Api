using EEC_ICT.Api.Constants;
using EEC_ICT.Api.Providers;
using EEC_ICT.Data.Common;
using EEC_ICT.Data.Core;
using EEC_ICT.Data.Models;
using EEC_ICT.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace EEC_ICT.Api.Controllers
{
    [RoutePrefix("api/functions")]
    public class FunctionsController : BaseController
    {
        [HttpGet]
        [Route("selectall")]
        [Permission]
        public object SelectAll(string filter, int pageIndex, int pageSize)
        {
            Logger.Info("[SelectAllFunctions]");

            var retval = new ReturnInfo();
            int totalRow = 0;
            retval.Status = new StatusReturn { Message = "Không thành công", Code = 0 };
            retval.Data = null;
            retval.Pagination = new PaginationInfo { PageIndex = pageIndex, PageSize = pageSize, TotalRows = 0 };
            try
            {
                var data = FunctionServices.SelectAll();
                var lstEntity = data;
                if (!string.IsNullOrEmpty(filter))
                    lstEntity = data.Where(x => x.Name.IndexOf(filter, StringComparison.OrdinalIgnoreCase)>=0).ToList();
                totalRow = lstEntity.Count();
                if (pageSize > 0)
                {
                    lstEntity = lstEntity.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                }
                retval.Pagination.TotalRows = totalRow;
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
        [Route("selectallactivated")]
        [Permission]
        public object SelectAllActivated()
        {
            Logger.Info("[SelectAllActivated_Functions]");

            var retval = new ReturnInfo();
            int totalRow = 0;
            retval.Status = new StatusReturn { Message = "Không thành công", Code = 0 };
            retval.Data = null;
            retval.Pagination = new PaginationInfo { PageIndex = 0, PageSize = 0, TotalRows = 0 };
            try
            {
                var data = FunctionServices.SelectAllActivated();
                retval.Pagination.TotalRows = totalRow;
                retval.Status.Message = "Thành công";
                retval.Status.Code = 1;
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

        [HttpGet]
        [Route("selectone/{id}")]
        [Permission]
        public object SelectOne(string id)
        {
            Logger.Info("[SelectOneFunctions]");

            var retval = new ReturnInfo();
            retval.Status = new StatusReturn { Message = "Không thành công", Code = 0 };
            retval.Data = null;
            retval.Pagination = new PaginationInfo { PageIndex = 0, PageSize = 0, TotalRows = 0 };
            try
            {
                var lstEntity = FunctionServices.SelectOne(id);
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
        [Route("SelectByParentId")]
        [Permission]
        public object SelectByParentId(string parentId = null)
        {
            Logger.Info("[SelectByParentIdFunctions]");

            var retval = new ReturnInfo();
            retval.Status = new StatusReturn { Message = "Không thành công", Code = 0 };
            retval.Data = null;
            retval.Pagination = new PaginationInfo { PageIndex = 0, PageSize = 0, TotalRows = 0 };
            try
            {
                var lstEntity = FunctionServices.SelectAllWParentId(parentId);
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
        [Route("Insert")]
        [Permission(Command = CommandConstants.CREATE, Function = FunctionConstants.FUNCTION)]
        public object Insert(Function function)
        {
            Logger.Info("[InsertFunctions]");
            var retval = new ReturnInfo();
            retval.Status = new StatusReturn { Message = "Không thành công", Code = 0 };
            retval.Data = null;
            retval.Pagination = new PaginationInfo { PageIndex = 0, PageSize = 0, TotalRows = 0 };
            try
            {
                var lstEntity = FunctionServices.Insert(function);
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
        [Permission(Command = CommandConstants.UPDATE, Function = FunctionConstants.FUNCTION)]
        public object Update(Function function)
        {
            Logger.Info("[UpdateFunctions]");
            var retval = new ReturnInfo();
            retval.Status = new StatusReturn { Message = "Không thành công", Code = 0 };
            retval.Data = null;
            retval.Pagination = new PaginationInfo { PageIndex = 0, PageSize = 0, TotalRows = 0 };
            try
            {
                var lstEntity = FunctionServices.Update(function);
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
        [Route("checkfunctionid")]
        [Permission]
        public object CheckFunctionId(Function request)
        {
            Logger.Info("[CheckFunctionId_Functions]");

            var retval = new ReturnInfo();
            retval.Status = new StatusReturn { Message = "Không thành công", Code = 0 };
            retval.Data = 0;
            retval.Pagination = new PaginationInfo { PageIndex = 0, PageSize = 0, TotalRows = 0 };

            try
            {
                var result = FunctionServices.CheckFunctionId(request.Id);
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
        [Route("Delete")]
        [Permission(Command = CommandConstants.DELETE, Function = FunctionConstants.FUNCTION)]
        public object Delete([FromUri] string[] ids)
        {
            Logger.Info("[DeleteFunctions]");
            var retval = new ReturnInfo();
            retval.Status = new StatusReturn { Message = "Không thành công", Code = 0 };
            retval.Data = null;
            retval.Pagination = new PaginationInfo { PageIndex = 0, PageSize = 0, TotalRows = 0 };
            try
            {
                foreach (var functionId in ids)
                {
                    FunctionServices.Delete(functionId);
                }
                retval.Status = new StatusReturn { Message = "Xóa thành công " + ids.Count().ToString() + " bản ghi", Code = 1 };
            }
            catch (Exception ex)
            {
                retval.Status = new StatusReturn { Message = ex.Message, Code = -1 };
            }

            Logger.Info("[retval]" + retval.JSONSerializer());
            return retval;
        }

        [HttpGet]
        [Route("GetFunctionWithCommandsAndPermission")]
        //[Permission(Command = CommandConstants.VIEW, Function = FunctionConstants.FUNCTION)]
        [Permission]
        public object GetWithCommandsAndPermission(int roleId)
        {
            Logger.Info("[GetFunctionWithCommandsAndPermission]");
            var retval = new ReturnInfo();
            retval.Status = new StatusReturn { Message = "Không thành công", Code = 0 };
            retval.Data = null;
            retval.Pagination = new PaginationInfo { PageIndex = 0, PageSize = 0, TotalRows = 0 };
            try
            {
                var lstEntity = FunctionServices.GetFunctionWithCommand();
                var permissions = PermissionServices.SelectByRoleId(roleId);
                for (var i=0; i<lstEntity.Count; i++)
                {
                    var index = permissions != new List<Permission>() ? permissions.FindIndex(o => o.FunctionId == lstEntity[i].Id) : -1;
                    if (index!=-1) {
                        var findData = permissions.Where(o=>o.FunctionId == lstEntity[i].Id).ToList();
                        for (var j=0; j<findData.Count; j++)
                        {
                            switch (findData[j].CommandId)
                            {
                                case CommandConstants.CREATE:
                                    lstEntity[i].ValueCreate = true;
                                    break;
                                case CommandConstants.UPDATE:
                                    lstEntity[i].ValueUpdate = true;
                                    break;
                                case CommandConstants.DELETE:
                                    lstEntity[i].ValueDelete = true;
                                    break;
                                case CommandConstants.DOWNLOAD:
                                    lstEntity[i].ValueDownload = true;
                                    break;
                                case CommandConstants.UPLOAD:
                                    lstEntity[i].ValueUpload = true;
                                    break;
                                case CommandConstants.VIEW:
                                    lstEntity[i].ValueView = true;
                                    break;
                                case CommandConstants.APPROVE:
                                    lstEntity[i].ValueApprove = true;
                                    break;
                                //case CommandConstants.UNAPPROVE:
                                //    lstEntity[i].ValueUnApprove = true;
                                //    break;
                            }
                        }
                    }
                }
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

        //[HttpGet]
        //[Route("SelectByUserId/{userId}")]
        //[Permission]
        //public object SelectByUserId(string userId)
        //{
        //    Logger.Info("[SelectFunctionByUserId]");
        //    var retval = new ReturnInfo();
        //    retval.Status = new StatusReturn { Message = "Không thành công", Code = 0 };
        //    retval.Data = null;
        //    retval.Pagination = new PaginationInfo { PageIndex = 0, PageSize = 0, TotalRows = 0 };
        //    var checkAdmin = false;
        //    try
        //    {
        //        var roles = RoleServices.SelectByUserId(userId).ToList();
        //        foreach (var role in roles)
        //        {
        //            if (role.Name == "Admin") checkAdmin = true;
        //        }
        //        List<Function> lstEntity = new List<Function>();
        //        if (checkAdmin)
        //        {
        //            lstEntity = FunctionServices.SelectAll().ToList();
        //        }
        //        else
        //        {
        //            lstEntity = FunctionServices.SelectByUserId(userId).ToList();
        //        }
        //        retval.Status.Message = "Thành công";
        //        retval.Status.Code = 1;
        //        retval.Data = lstEntity;
        //    }
        //    catch (Exception ex)
        //    {
        //        retval.Status.Message = ex.Message;
        //        retval.Status.Code = -1;
        //        Logger.Info("[retval]" + retval.JSONSerializer());
        //        return retval;
        //    }

        //    Logger.Info("[retval]" + retval.JSONSerializer());
        //    return retval;
        //}
    }
}
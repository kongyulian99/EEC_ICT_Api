using EEC_ICT.Api.Constants;
using EEC_ICT.Api.Providers;
using EEC_ICT.Data.Common;
using EEC_ICT.Data.Core;
using EEC_ICT.Data.Models;
using EEC_ICT.Data.Services;
using System;
using System.Linq;
using System.Web.Http;

namespace EEC_ICT.Api.Controllers
{
    [RoutePrefix("api/commands")]
    public class CommandsController : BaseController
    {
        [HttpGet]
        [Route("selectall")]
        [Permission]
        public object SelectAll(string filter, int pageIndex, int pageSize)
        {
            Logger.Info("[SelectAllCommands]");

            var retval = new ReturnInfo();
            retval.Status = new StatusReturn { Message = "Không thành công", Code = 0 };
            retval.Data = null;
            retval.Pagination = new PaginationInfo { PageIndex = pageIndex, PageSize = pageSize, TotalRows = 0 };
            try
            {
                var data = CommandServices.SelectAll();
                var lstEntity = data;
                if (!string.IsNullOrEmpty(filter))
                    lstEntity = data.Where(x => x.Name.IndexOf(filter, StringComparison.CurrentCultureIgnoreCase)>=0).ToList();
                int totalRow = lstEntity.Count();
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
        [Route("selectbyfunctionid/{functionId}")]
        [Permission(Command = CommandConstants.VIEW, Function = FunctionConstants.FUNCTION)]
        public object SelectByFunctionId(string functionId, string filter, int pageIndex, int pageSize)
        {
            Logger.Info("[SelectByFunctionIdCommands]");

            var retval = new ReturnInfo();
            retval.Status = new StatusReturn { Message = "Không thành công", Code = 0 };
            retval.Data = null;
            retval.Pagination = new PaginationInfo { PageIndex = pageIndex, PageSize = pageSize, TotalRows = 0 };
            try
            {
                var data = CommandServices.SelectByFunctionId(functionId);
                var lstEntity = data;
                if (!string.IsNullOrEmpty(filter))
                    lstEntity = data.Where(x => x.Name.IndexOf(filter, StringComparison.OrdinalIgnoreCase)>=0).ToList();
                int totalRow = lstEntity.Count();
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

        [HttpPost]
        [Route("addcommandtofunction/{functionId}")]
        [Permission]
        public object AddCommandToFunction(string functionId, CommandAssign request)
        {
            Logger.Info("[AddCommandToFunctionCommands]");

            var retval = new ReturnInfo();
            retval.Status = new StatusReturn { Message = "Không thành công", Code = 0 };
            retval.Data = null;
            retval.Pagination = new PaginationInfo { PageIndex = 0, PageSize = 0, TotalRows = 0 };
            try
            {
                if (request.AddToAllFunctions)
                {
                    var functions = FunctionServices.SelectAll();
                    foreach (var function in functions)
                    {
                        foreach (var commandId in request.CommandIds)
                        {
                            var check = CommandInFunctionServices.SelectOne(commandId, function.Id);
                            if (check == null)
                            {
                                CommandInFunctionServices.Insert(new CommandInFunction()
                                {
                                    CommandId = commandId,
                                    FunctionId = function.Id
                                });
                            }
                        }
                    }
                }
                else
                {
                    foreach (var commandId in request.CommandIds)
                    {
                        if (CommandInFunctionServices.SelectOne(commandId, functionId) == null)
                        {
                            var entity = new CommandInFunction()
                            {
                                CommandId = commandId,
                                FunctionId = functionId
                            };
                            CommandInFunctionServices.Insert(entity);
                        }
                    }
                }
                retval.Data = "1";
                retval.Status = new StatusReturn { Message = "Thành công", Code = 1 };
            }
            catch (Exception ex)
            {
                retval.Status = new StatusReturn { Message = ex.Message, Code = -1 };
            }
            Logger.Info("[retval]" + retval.JSONSerializer());
            return retval;
        }

        [HttpPost]
        [Route("removecommandinfunction/{functionId}")]
        [Permission]
        public object RemoveCommandInFunction(string functionId, [FromUri] string[] ids)
        {
            Logger.Info("[RemoveCommandInFunctionCommands]");

            var retval = new ReturnInfo();
            retval.Status = new StatusReturn { Message = "Không thành công", Code = 0 };
            retval.Data = null;
            retval.Pagination = new PaginationInfo { PageIndex = 0, PageSize = 0, TotalRows = 0 };
            try
            {
                foreach (var commandId in ids)
                {
                    var entity = new CommandInFunction()
                    {
                        CommandId = commandId,
                        FunctionId = functionId
                    };
                    CommandInFunctionServices.Delete(entity);
                    retval.Status = new StatusReturn { Message = "Xóa thành công " + ids.Count().ToString() + " bản ghi", Code = 1 };
                }
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
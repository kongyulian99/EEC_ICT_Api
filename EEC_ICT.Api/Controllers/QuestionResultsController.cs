using DocumentFormat.OpenXml.Spreadsheet;
using EEC_ICT.Api.Providers;
using EEC_ICT.Api.Services;
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
    [RoutePrefix("api/QuestionResults")]
    public class QuestionResultsController : ApiController
    {
        [HttpGet]
        [Route("selectbyuser/{userId}")]
        public object SelectByUser(int questionId, string userId)
        {
            Logger.Info("[QuestionResults_SelecOne]");
            var retval = new ReturnInfo
            {
                Data = new QuestionResults(),
                Pagination = new PaginationInfo(),
                Status = new StatusReturn { Code = 0, Message = "Không thành công" }
            };
            try
            {
                retval.Data = QuestionResultsServices.SelectOne(questionId, userId);
                retval.Status = new StatusReturn { Code = 1, Message = "Thành công" };
            }
            catch (Exception ex)
            {
                retval.Status = new StatusReturn { Code = -1, Message = ex.Message };
            }
            Logger.Info("[retval]" + retval.JSONSerializer());
            return retval;
        }

        [HttpPost]
        [Route("insert")]
        public object Insert(QuestionResults request)
        {
            Logger.Info("[QuestionResults_Insert]");
            var retval = new ReturnInfo
            {
                Data = "",
                Pagination = new PaginationInfo(),
                Status = new StatusReturn { Code = 0, Message = "Không thành công" }
            };
            try
            {
                retval.Data = QuestionResultsServices.Insert(request);
                retval.Status = new StatusReturn { Code = 1, Message = "Thành công" };
            }
            catch (Exception ex)
            {
                retval.Status = new StatusReturn { Code = -1, Message = ex.Message };
            }
            Logger.Info("[retval]" + retval.JSONSerializer());
            return retval;
        }

        [HttpGet]
        [Route("getAverageScoreByTopic")]
        public object GetAverageScoreByUser(string userId, int TopicId)
        {
            Logger.Info("[QuestionResults_SelecOne]");
            var retval = new ReturnInfo
            {
                Data = new QuestionResultsByTopic(),
                Pagination = new PaginationInfo(),
                Status = new StatusReturn { Code = 0, Message = "Không thành công" }
            };
            try
            {
                retval.Data = QuestionResultsServices.SelectAverageScoreByUser(userId, TopicId);
                retval.Status = new StatusReturn { Code = 1, Message = "Thành công" };
            }
            catch (Exception ex)
            {
                retval.Status = new StatusReturn { Code = -1, Message = ex.Message };
            }
            Logger.Info("[retval]" + retval.JSONSerializer());
            return retval;
        }

    }
}
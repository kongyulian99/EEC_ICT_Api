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
    [RoutePrefix("api/TestResults")]
    public class TestResultsController : ApiController
    {
        //[HttpGet]
        //[Route("getAllUserAverageScore")]
        //public object GetAllUserAverageScore()
        //{
        //    Logger.Info("[TestResults_SelectAll]");
        //    var retval = new ReturnInfo
        //    {
        //        Data = new List<TestResults>(),
        //        Pagination = new PaginationInfo { PageIndex = 0, PageSize = 0, TotalRows = 0 },
        //        Status = new StatusReturn { Code = 0, Message = "Không thành công" }
        //    };
            
        //    try
        //    {
        //        var data = TestResultsServices.SelectAll();

        //        List<string> userIds = new List<string>();

        //        for(int i=0; i<data.Count(); i++)
        //        {
        //            if(userIds.IndexOf(data[i].UserId) < 0)
        //            {
        //                userIds.Add(data[i].UserId);
        //            }
        //        }

        //        List<double> scores = new List<double>();
        //        for (int i = 0; i < userIds.Count(); i++)
        //        {
        //            double score = data.Count(o => o.UserId == userIds[i] && o.Result == true) / data.Count(record => record.UserId == userIds[i]);
        //            scores.Add(score);
        //        }

        //        List<ResultStatistic> statistics = new List<ResultStatistic>();
        //        ResultStatistic st0 = new ResultStatistic();
        //        st0.Range = "0-20";
        //        st0.Count = scores.Count(o => o <= 0.2);
        //        statistics.Add(st0);
        //        ResultStatistic st1 = new ResultStatistic();
        //        st1.Range = "21-40";
        //        st1.Count = scores.Count(o => o <= 0.4 && o > 0.2);
        //        statistics.Add(st1);
        //        ResultStatistic st2 = new ResultStatistic();
        //        st2.Range = "41-60";
        //        st2.Count = scores.Count(o => o <= 0.6 && o > 0.4);
        //        statistics.Add(st1);
        //        ResultStatistic st3 = new ResultStatistic();
        //        st3.Range = "61-80";
        //        st3.Count = scores.Count(o => o <= 0.8 && o > 0.6);
        //        statistics.Add(st3);
        //        ResultStatistic st4 = new ResultStatistic();
        //        st4.Range = "81-100";
        //        st4.Count = scores.Count(o => o <= 1 && o > 0.8);
        //        statistics.Add(st4);


        //        retval.Data = statistics;
        //        retval.Status = new StatusReturn { Code = 1, Message = "Thành công" };
        //    }
        //    catch (Exception ex)
        //    {
        //        retval.Status = new StatusReturn { Code = -1, Message = ex.Message };
        //    }
        //    Logger.Info("[retval]" + retval.JSONSerializer());
        //    return retval;
        //}

        //[HttpGet]
        //[Route("getAllUserAverageScorebyTestdate")]
        //public object SelectStatistic(DateTime startTime, DateTime endTime)
        //{
        //    Logger.Info("[TestResults_SelecOne]");
        //    var retval = new ReturnInfo
        //    {
        //        Data = new TestResults(),
        //        Pagination = new PaginationInfo(),
        //        Status = new StatusReturn { Code = 0, Message = "Không thành công" }
        //    };

        //    try
        //    {
        //        var data = TestResultsServices.SelectAll().FindAll(o => o.TestDate >= startTime && o.TestDate < endTime);

        //        List<string> userIds = new List<string>();

        //        for (int i = 0; i < data.Count(); i++)
        //        {
        //            if (userIds.IndexOf(data[i].UserId) < 0)
        //            {
        //                userIds.Add(data[i].UserId);
        //            }
        //        }

        //        List<double> scores = new List<double>();
        //        for (int i = 0; i < userIds.Count(); i++)
        //        {
        //            double score = data.Count(o => o.UserId == userIds[i] && o.Result == true) / data.Count(record => record.UserId == userIds[i]);
        //            scores.Add(score);
        //        }

        //        List<ResultStatistic> statistics = new List<ResultStatistic>();
        //        ResultStatistic st0 = new ResultStatistic();
        //        st0.Range = "0-20";
        //        st0.Count = scores.Count(o => o <= 0.2);
        //        statistics.Add(st0);
        //        ResultStatistic st1 = new ResultStatistic();
        //        st1.Range = "21-40";
        //        st1.Count = scores.Count(o => o <= 0.4 && o > 0.2);
        //        statistics.Add(st1);
        //        ResultStatistic st2 = new ResultStatistic();
        //        st2.Range = "41-60";
        //        st2.Count = scores.Count(o => o <= 0.6 && o > 0.4);
        //        statistics.Add(st1);
        //        ResultStatistic st3 = new ResultStatistic();
        //        st3.Range = "61-80";
        //        st3.Count = scores.Count(o => o <= 0.8 && o > 0.6);
        //        statistics.Add(st3);
        //        ResultStatistic st4 = new ResultStatistic();
        //        st4.Range = "81-100";
        //        st4.Count = scores.Count(o => o <= 1 && o > 0.8);
        //        statistics.Add(st4);


        //        retval.Data = statistics;
        //        retval.Status = new StatusReturn { Code = 1, Message = "Thành công" };
        //    } catch(Exception ex)
        //    {
        //        retval.Status = new StatusReturn { Code = -1, Message = ex.Message };
        //    }
        //    Logger.Info("[retval]" + retval.JSONSerializer());
        //    return retval;
        //}

        [HttpGet]
        [Route("selectbyuser/{userId}")]
        public object SelectByUser(string userId)
        {
            Logger.Info("[TestResults_SelecOne]");
            var retval = new ReturnInfo
            {
                Data = new TestResults(),
                Pagination = new PaginationInfo(),
                Status = new StatusReturn { Code = 0, Message = "Không thành công" }
            };
            try
            {
                retval.Data = TestResultsServices.SelectByUser(userId);
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
        public object Insert(TestResults request)
        {
            Logger.Info("[TestResults_Insert]");
            var retval = new ReturnInfo
            {
                Data = "",
                Pagination = new PaginationInfo(),
                Status = new StatusReturn { Code = 0, Message = "Không thành công" }
            };
            try
            {
                retval.Data = TestResultsServices.Insert(request);
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
        [Route("getMaxScoreByUser")]
        public object GetMaxScoreByUser(string userId) {
            Logger.Info("[TestResults_SelecOne]");
            var retval = new ReturnInfo
            {
                Data = new MaxScoreAndIdDeThi(),
                Pagination = new PaginationInfo(),
                Status = new StatusReturn { Code = 0, Message = "Không thành công" }
            };
            try
            {
                retval.Data = TestResultsServices.SelectBestScoreByUser(userId);
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
        [Route("getMinScoreByUser")]
        public object GetMinScoreByUser(string userId)
        {
            Logger.Info("[TestResults_SelecOne]");
            var retval = new ReturnInfo
            {
                Data = new MinScoreAndIdDeThi(),
                Pagination = new PaginationInfo(),
                Status = new StatusReturn { Code = 0, Message = "Không thành công" }
            };
            try
            {
                retval.Data = TestResultsServices.SelectAverageScoreByUser(userId);
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
        [Route("getAverageScoreByUser")]
        public object GetAverageScoreByUser(string userId)
        {
            Logger.Info("[TestResults_SelecOne]");
            var retval = new ReturnInfo
            {
                Data = new AverageScoreAndIdDeThi(),
                Pagination = new PaginationInfo(),
                Status = new StatusReturn { Code = 0, Message = "Không thành công" }
            };
            try
            {
                retval.Data = TestResultsServices.SelectAverageScoreByUser(userId);
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
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
    [RoutePrefix("api/dm-dethi")]
    public class DM_DeThiController : ApiController
    {
        // READ
        [HttpGet]
        [Route("selectall")]
        public object SelectAll(string filter, int pageIndex, int pageSize, int topicId)
        {
            Logger.Info("[DM_DeThi_SelectAll]");
            var retval = new ReturnInfo
            {
                Data = new List<DM_DeThi>(),
                Pagination = new PaginationInfo { PageIndex = pageIndex, PageSize = pageSize, TotalRows = 0 },
                Status = new StatusReturn { Code = 0, Message = "Không thành công" }
            };
            
            try
            {
                var data = DM_DeThiServices.SelectAll();

                if (!string.IsNullOrEmpty(filter))
                {
                    data = data.Where(o => UtilityServices.convertToUnSign(o.TenDeThi).IndexOf(UtilityServices.convertToUnSign(filter), StringComparison.CurrentCultureIgnoreCase) >= 0).ToList();
                }
                retval.Pagination.TotalRows = data.Count;
                if (pageSize > 0)
                {
                    data = data.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();
                }
                retval.Data = data;
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
        [Route("selectone/{idDeThi}")]
        public object SelectOne(int idDeThi)
        {
            Logger.Info("[DM_DeThi_SelecOne]");
            var retval = new ReturnInfo
            {
                Data = new DM_DeThi(),
                Pagination = new PaginationInfo(),
                Status = new StatusReturn { Code = 0, Message = "Không thành công" }
            };
            try
            {
                retval.Data = DM_DeThiServices.SelectOne(idDeThi);

                // TODO
                // SELECT ALL QUESTION FROM DETHI


                retval.Status = new StatusReturn { Code = 1, Message = "Thành công" };
            }
            catch (Exception ex)
            {
                retval.Status = new StatusReturn { Code = -1, Message = ex.Message };
            }
            Logger.Info("[retval]" + retval.JSONSerializer());
            return retval;
        }

        //CREATE
        [HttpPost]
        [Route("insert")]
        public object Insert(DM_DeThi request)
        {
            Logger.Info("[DM_DeThi_Insert]");
            var retval = new ReturnInfo
            {
                Data = "",
                Pagination = new PaginationInfo(),
                Status = new StatusReturn { Code = 0, Message = "Không thành công" }
            };
            try
            {
                var questionId = DM_DeThiServices.Insert(request);
                //if (request.ChoiceList != null)
                //{
                //    for (int i = 0; i < request.ChoiceList.Count(); i++)
                //    {
                //        if (request.ChoiceList[i].AnswerId <= 0)
                //        {
                //            request.ChoiceList[i].QuestionId = int.Parse(questionId); // request.IdCanBoNhanVien;
                //            DM_DapAnServices.Insert(request.ChoiceList[i]);
                //        }
                //        else
                //        {
                //            DM_DapAnServices.Update(request.ChoiceList[i]);
                //        }
                //    }
                //}
                //if (request.ChoiceList_Delete != null)
                //{
                //    for (int i = 0; i < request.ChoiceList_Delete.Count(); i++)
                //    {
                //        DM_DapAnServices.Delete(request.ChoiceList_Delete[i].AnswerId);
                //    }
                //}

                retval.Data = questionId;
                retval.Status = new StatusReturn { Code = 1, Message = "Thành công" };
            }
            catch (Exception ex)
            {
                retval.Status = new StatusReturn { Code = -1, Message = ex.Message };
            }
            Logger.Info("[retval]" + retval.JSONSerializer());
            return retval;
        }
                                        
        //[HttpPost]
        //[Route("checkcorrect")]
        //public object CheckCorrect(List<DM_DapAnCheckCorrect> listRequest, string userId)
        //{
        //    Logger.Info("[DM_DeThi_CheckCorrect]");
        //    var retval = new ReturnInfo
        //    {
        //        Data = "",
        //        Pagination = new PaginationInfo(),
        //        Status = new StatusReturn { Code = 0, Message = "Không thành công" }
        //    };
        //    try
        //    {
        //        var listKetQua = new List<DM_DapAnCheckCorrect_Result>();
        //        for (int i = 0; i < listRequest.Count(); i++)
        //        {
        //            var ketQua = new DM_DapAnCheckCorrect_Result();
        //            ketQua.QuestionId = listRequest[i].QuestionId;
        //            var t = DM_DeThiServices.CheckCorrect(listRequest[i]);
        //            ketQua.IsCorrect = DM_DeThiServices.CheckCorrect(listRequest[i]) == "True" ? true: false;
        //            listKetQua.Add(ketQua);

        //            var testResult = new TestResults();
        //            testResult.UserId = userId;
        //            testResult.QuestionId = listRequest[i].QuestionId;
        //            testResult.Result = ketQua.IsCorrect;
        //            testResult.TestDate = DateTime.Now;
        //            TestResultsServices.Insert(testResult);
        //        }

        //        retval.Data = listKetQua;
        //        retval.Status = new StatusReturn { Code = 1, Message = "Thành công" };
        //    }
        //    catch (Exception ex)
        //    {
        //        retval.Status = new StatusReturn { Code = -1, Message = ex.Message };
        //    }
        //    Logger.Info("[retval]" + retval.JSONSerializer());
        //    return retval;
        //}

        // UPDATE
        [HttpPut]
        [Route("update")]
        public object Update(DM_DeThi request)
        {
            Logger.Info("[DM_DeThi_Update]");
            var retval = new ReturnInfo
            {
                Data = "",
                Pagination = new PaginationInfo(),
                Status = new StatusReturn { Code = 0, Message = "Không thành công" }
            };
            try
            {
                var questionId = int.Parse(DM_DeThiServices.Update(request));

                //if (request.ChoiceList != null)
                //{
                //    for (int i = 0; i < request.ChoiceList.Count(); i++)
                //    {
                //        if (request.ChoiceList[i].AnswerId <= 0)
                //        {
                //            request.ChoiceList[i].QuestionId = questionId; // request.IdCanBoNhanVien;
                //            DM_DapAnServices.Insert(request.ChoiceList[i]);
                //        }
                //        else
                //        {
                //            DM_DapAnServices.Update(request.ChoiceList[i]);
                //        }
                //    }
                //}
                //if (request.ChoiceList_Delete != null)
                //{
                //    for (int i = 0; i < request.ChoiceList_Delete.Count(); i++)
                //    {
                //        DM_DapAnServices.Delete(request.ChoiceList_Delete[i].AnswerId);
                //    }
                //}

                retval.Data = questionId;
                retval.Status = new StatusReturn { Code = 1, Message = "Thành công" };
            }
            catch (Exception ex)
            {
                retval.Status = new StatusReturn { Code = -1, Message = ex.Message };
            }
            Logger.Info("[retval]" + retval.JSONSerializer());
            return retval;
        }

        // DELETE
        [HttpDelete]
        [Route("delete/{questionId}")]
        public object Delete(int questionId)
        {
            Logger.Info("[DM_DeThi_Delete]");
            var retval = new ReturnInfo
            {
                Data = "",
                Pagination = new PaginationInfo(),
                Status = new StatusReturn { Code = 0, Message = "Không thành công" }
            };
            try
            {
                retval.Data = DM_DeThiServices.Delete(questionId);
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
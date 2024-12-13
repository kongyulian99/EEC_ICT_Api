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
        public object SelectAll(string filter, int pageIndex, int pageSize)
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
                var deThi = DM_DeThiServices.SelectOne(idDeThi);

                deThi.ListCauHoi = DM_CauHoiServices.SelectAll(idDeThi);

                //for (int i = 0; i < deThi.ListCauHoi.Count; i++) {
                //    deThi.ListCauHoi[i].ChoiceList = DM_DapAnServices.SelectAllWQuestionId(deThi.ListCauHoi[i].QuestionId);
                //}

                // TODO
                // SELECT ALL QUESTION FROM DETHI

                retval.Data = deThi;
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
        [Route("selectonefortest/{idDeThi}")]
        public object SelectOneForTest(int idDeThi)
        {
            Logger.Info("[DM_DeThi_SelecOneForTest]");
            var retval = new ReturnInfo
            {
                Data = new DM_DeThi(),
                Pagination = new PaginationInfo(),
                Status = new StatusReturn { Code = 0, Message = "Không thành công" }
            };
            try
            {
                var deThi = DM_DeThiServices.SelectOne(idDeThi);

                deThi.ListCauHoi = DM_CauHoiServices.SelectAll(idDeThi);

                //for (int i = 0; i < deThi.ListCauHoi.Count; i++)
                //{
                //    deThi.ListCauHoi[i].ChoiceList = DM_DapAnServices.SelectAllWQuestionId(deThi.ListCauHoi[i].QuestionId);
                //    for(int j=0; j < deThi.ListCauHoi[i].ChoiceList.Count; j++)
                //    {
                //        deThi.ListCauHoi[i].ChoiceList[j].IsCorrect = false;
                //    }
                //}

                // TODO
                // SELECT ALL QUESTION FROM DETHI

                retval.Data = deThi;
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
                //for (int i = 0; i < request.ListCauHoi?.Count; i++) {
                //    request.ListCauHoi[i].IdDeThi = request.IdDeThi;
                //    if (request.ListCauHoi[i].QuestionId > 0) DM_CauHoiServices.Update(request.ListCauHoi[i]);
                //    else request.ListCauHoi[i].QuestionId = int.Parse(DM_CauHoiServices.Insert(request.ListCauHoi[i]));

                //    for (int j = 0; j < request.ListCauHoi[i].ChoiceList?.Count; j++) {
                //        request.ListCauHoi[i].ChoiceList[j].QuestionId = request.ListCauHoi[i].QuestionId;
                //        if (request.ListCauHoi[i].ChoiceList[j].AnswerId > 0) DM_DapAnServices.Update(request.ListCauHoi[i].ChoiceList[j]);
                //        else DM_DapAnServices.Insert(request.ListCauHoi[i].ChoiceList[j]);
                //    }
                //}

                string id = DM_DeThiServices.Update(request);

                for (int i = 0; i < request.ListCauHoi?.Count; i++) {
                    request.ListCauHoi[i].IdDeThi = request.IdDeThi;

                    if(request.ListCauHoi[i].QuestionId <= 0)
                    {
                        var questionId = DM_CauHoiServices.Insert(request.ListCauHoi[i]);
                        request.ListCauHoi[i].QuestionId = int.Parse(questionId);
                    } else
                    {
                        DM_CauHoiServices.Update(request.ListCauHoi[i]);
                    }                    
                }

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

                retval.Data = id;
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
        [Route("submit/{userId}")]
        public object Submit(string userId, DM_DeThi request)
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
                var correctCount = 0;
                for (int i = 0; i < request.ListCauHoi?.Count; i++)
                {
                    var listDapAn = DM_DapAnServices.SelectAllWQuestionId(request.ListCauHoi[i].QuestionId);
                    var correctAnswerId = listDapAn.Find(o => o.IsCorrect == true).AnswerId;

                    //if (request.ListCauHoi[i].ChoiceList.Find(o => o.IsCorrect == true)?.AnswerId == correctAnswerId) { 
                    //    correctCount ++;
                    //}
                }

                // insert test result
                var testResult = new TestResults();
                testResult.UserId = userId;
                testResult.IdDeThi = request.IdDeThi;
                testResult.Score = float.Parse(correctCount.ToString()) / request.ListCauHoi.Count;
                testResult.StartTime = request.StartTime;
                testResult.EndTime = request.EndTime;
                TestResultsServices.Insert(testResult);

                retval.Data = correctCount;
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
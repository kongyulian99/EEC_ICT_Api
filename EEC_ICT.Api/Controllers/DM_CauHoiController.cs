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
    [RoutePrefix("api/dm-cauhoi")]
    public class DM_CauHoiController : ApiController
    {
        // READ
        [HttpGet]
        [Route("selectall")]
        public object SelectAll(string filter, int pageIndex, int pageSize, int topicId)
        {
            Logger.Info("[DM_CauHoi_SelectAll]");
            var retval = new ReturnInfo
            {
                Data = new List<DM_CauHoi>(),
                Pagination = new PaginationInfo { PageIndex = pageIndex, PageSize = pageSize, TotalRows = 0 },
                Status = new StatusReturn { Code = 0, Message = "Không thành công" }
            };
            
            try
            {
                var data = DM_CauHoiServices.SelectAll();

                data = data.FindAll(o => o.TopicId == topicId).ToList();

                // select thong tin dao tao
                for(var i=0; i<data.Count(); i++)
                {
                    data[i].ChoiceList = DM_DapAnServices.SelectAllWQuestionId(data[i].QuestionId);
                }
                
                //if (!string.IsNullOrEmpty(filter))
                //{
                //    data = data.Where(o => UtilityServices.convertToUnSign(o.TenCapBac).IndexOf(UtilityServices.convertToUnSign(filter), StringComparison.CurrentCultureIgnoreCase) >= 0).ToList();
                //}
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
        [Route("selectone/{questionId}")]
        public object SelectOne(int questionId)
        {
            Logger.Info("[DM_CauHoi_SelecOne]");
            var retval = new ReturnInfo
            {
                Data = new DM_CauHoi(),
                Pagination = new PaginationInfo(),
                Status = new StatusReturn { Code = 0, Message = "Không thành công" }
            };
            try
            {
                retval.Data = DM_CauHoiServices.SelectOne(questionId);
                retval.Status = new StatusReturn { Code = 1, Message = "Thành công" };
            }
            catch (Exception ex)
            {
                retval.Status = new StatusReturn { Code = -1, Message = ex.Message };
            }
            Logger.Info("[retval]" + retval.JSONSerializer());
            return retval;
        }

        //CREAT
        [HttpPost]
        [Route("insert")]
        public object Insert(DM_CauHoi request)
        {
            Logger.Info("[DM_CauHoi_Insert]");
            var retval = new ReturnInfo
            {
                Data = "",
                Pagination = new PaginationInfo(),
                Status = new StatusReturn { Code = 0, Message = "Không thành công" }
            };
            try
            {
                var questionId = DM_CauHoiServices.Insert(request);
                if (request.ChoiceList != null)
                {
                    for (int i = 0; i < request.ChoiceList.Count(); i++)
                    {
                        if (request.ChoiceList[i].AnswerId <= 0)
                        {
                            request.ChoiceList[i].QuestionId = int.Parse(questionId); // request.IdCanBoNhanVien;
                            DM_DapAnServices.Insert(request.ChoiceList[i]);
                        }
                        else
                        {
                            DM_DapAnServices.Update(request.ChoiceList[i]);
                        }
                    }
                }
                if (request.ChoiceList_Delete != null)
                {
                    for (int i = 0; i < request.ChoiceList_Delete.Count(); i++)
                    {
                        DM_DapAnServices.Delete(request.ChoiceList_Delete[i].AnswerId);
                    }
                }

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

        // UPDATE
        [HttpPut]
        [Route("update")]
        public object Update(DM_CauHoi request)
        {
            Logger.Info("[DM_CauHoi_Update]");
            var retval = new ReturnInfo
            {
                Data = "",
                Pagination = new PaginationInfo(),
                Status = new StatusReturn { Code = 0, Message = "Không thành công" }
            };
            try
            {
                var questionId = int.Parse(DM_CauHoiServices.Update(request));

                if (request.ChoiceList != null)
                {
                    for (int i = 0; i < request.ChoiceList.Count(); i++)
                    {
                        if (request.ChoiceList[i].AnswerId <= 0)
                        {
                            request.ChoiceList[i].QuestionId = questionId; // request.IdCanBoNhanVien;
                            DM_DapAnServices.Insert(request.ChoiceList[i]);
                        }
                        else
                        {
                            DM_DapAnServices.Update(request.ChoiceList[i]);
                        }
                    }
                }
                if (request.ChoiceList_Delete != null)
                {
                    for (int i = 0; i < request.ChoiceList_Delete.Count(); i++)
                    {
                        DM_DapAnServices.Delete(request.ChoiceList_Delete[i].AnswerId);
                    }
                }

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
            Logger.Info("[DM_CauHoi_Delete]");
            var retval = new ReturnInfo
            {
                Data = "",
                Pagination = new PaginationInfo(),
                Status = new StatusReturn { Code = 0, Message = "Không thành công" }
            };
            try
            {
                retval.Data = DM_CauHoiServices.Delete(questionId);
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
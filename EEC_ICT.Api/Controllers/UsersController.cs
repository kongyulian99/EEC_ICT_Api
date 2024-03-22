using EEC_ICT.Api.Constants;
using EEC_ICT.Api.Providers;
using EEC_ICT.Data.Common;
using EEC_ICT.Data.Core;
using EEC_ICT.Data.Models;
using EEC_ICT.Data.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;

namespace EEC_ICT.Api.Controllers
{
    [RoutePrefix("api/users")]
    public class UsersController : BaseController
    {
        /// <summary>
        /// Đăng nhập
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        public object Login(User request)
        {
            Logger.Info("[User_Login]" + request.JSONSerializer());
            var retval = new ReturnInfo();
            retval.Status = new StatusReturn { Message = "Không thành công", Code = 0 };
            retval.Data = null;
            if (string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Password))
            {
                retval.Status.Message = "Truyền thiếu dữ liệu";
                Logger.Info("[Login]" + retval.JSONSerializer());
                return retval;
            }
            var userInfo = UserServices.Login(request.UserName, request.Password);

            if (userInfo != null)
            {
                //var roles = RoleServices.SelectByUserId(userInfo.Id);
                //var functions = FunctionServices.SelectAllActivated();
                //List<Function> functionGen = functions.Where(o => String.IsNullOrEmpty(o.ParentId)).OrderBy(x=>x.SortOrder).ToList();
                //for (var i=0; i<functionGen.Count; i++)
                //{
                //    functionGen[i].Children.AddRange(functions.Where(o => o.ParentId==functionGen[i].Id).OrderBy(x=>x.SortOrder).ToList());
                //}
                //var rolesToString = "";
                //var permissionsToString = "";
                //foreach (var role in roles)
                //{
                //    rolesToString = rolesToString + role.Name + "_";
                //}
                //List<Permission> permissions = new List<Permission>();

                //if (roles.FindIndex(x => x.Name == "Admin") == -1)
                //{
                //    permissions = PermissionServices.SelectByUserId(userInfo.Id);
                //    foreach (var permission in permissions)
                //    {
                //        permissionsToString = permissionsToString + permission.FunctionId + "_" + permission.CommandId + ",";
                //    }
                //}

                //var token = jwtService.GenerateSecurityToken(userInfo.Id, rolesToString, permissionsToString);
                //var token = jwtService.GenerateSecurityTokenFixLength(userInfo.UserId);
                //var refreshToken = RefreshTokenServices.Insert(userInfo.UserId, Guid.NewGuid().ToString());
                var infoLogin = new
                {
                    userInfo.UserId,
                    userInfo.UserName,
                    userInfo.FullName,
                    userInfo.Avatar,
                    //Functions = functionGen,
                    //access_token = token,
                    //token_type = "Bearer",
                    //refresh_token = refreshToken,
                    //roles = rolesToString,
                    //permissions = permissionsToString,
                };
                //LogsServices.Insert(new Logs { NgayLogs = DateTime.Now, UserId = userInfo.Id });
                retval.Data = infoLogin;
                retval.Status.Message = "Đăng nhập thành công";
                retval.Status.Code = 1;
            }
            else
            {
                retval.Status.Message = "Sai thông tin đăng nhập";
                retval.Status.Code = -1;
            }
            Logger.Info("[Login]" + retval.JSONSerializer());
            return retval;
        }

        [HttpGet]
        [Route("selectall")]
        //[Permission(Command = CommandConstants.VIEW, Function = FunctionConstants.USER)]
        public object SelectAll(string filter, int pageIndex, int pageSize)
        {
            Logger.Info("[SelectAllUsers]");

            var retval = new ReturnInfo();
            int totalRow = 0;
            retval.Status = new StatusReturn { Message = "Không thành công", Code = 0 };
            retval.Data = null;
            retval.Pagination = new PaginationInfo { PageIndex = pageIndex, PageSize = pageSize, TotalRows = 0 };
            try
            {
                var lstUsers = UserServices.SelectAll();
                var lstEntity = lstUsers;
                if (!string.IsNullOrEmpty(filter))
                    lstEntity = lstUsers.Where(o=>o.UserName.IndexOf(filter, StringComparison.CurrentCultureIgnoreCase)>=0 || o.FullName.IndexOf(filter, StringComparison.CurrentCultureIgnoreCase)>=0).ToList();
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
        //[Permission]
        public object SelectOne(string id)
        {
            Logger.Info("[SelectOneUsers]");

            var retval = new ReturnInfo();
            retval.Status = new StatusReturn { Message = "Không thành công", Code = 0 };
            retval.Data = null;
            retval.Pagination = new PaginationInfo { PageIndex = 0, PageSize = 0, TotalRows = 0 };
            try
            {
                var user = UserServices.SelectOne(id);
                retval.Status.Message = "Thành công";
                retval.Status.Code = 1;
                retval.Data = user;
            }
            catch (Exception ex)
            {
                retval.Status.Message = ex.Message;
                retval.Status.Code = -1;
            }

            Logger.Info("[retval]" + retval.JSONSerializer());
            return retval;
        }

        [HttpPut]
        [Route("update-password/{userId}")]
        //[Permission]
        public object UpdatePassword(string userId, string newPassword, string oldPassword)
        {
            Logger.Info("[InsertUsers]");

            var retval = new ReturnInfo();
            retval.Status = new StatusReturn { Message = "Không thành công", Code = 0 };
            retval.Data = null;
            retval.Pagination = null;

            try
            {
                var isUser = UserServices.CheckPassword(userId, oldPassword);
                if (!isUser)
                {
                    throw new Exception("Chưa nhập đúng mật khẩu cũ!");
                }
                var result = UserServices.UpdatePassword(userId, newPassword);
                retval.Status.Message = "Thêm mới thành công";
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
        [HttpPut]
        [Route("reset-password")]
        //[Permission]
        public object ResetPassword(string userId)
        {
            Logger.Info("[InsertUsers]");

            var retval = new ReturnInfo();
            retval.Status = new StatusReturn { Message = "Không thành công", Code = 0 };
            retval.Data = null;
            retval.Pagination = null;

            try
            {
                var newPassword = "Admin@123";
                var result = UserServices.UpdatePassword(userId, newPassword);
                retval.Status.Message = "Reset password thành công";
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


        [HttpPut]
        [Route("update-commoninfo")]
        //[Permission]
        public object UpdateCommonInfo(User entity)
        {
            Logger.Info("[InsertUsers]");

            var retval = new ReturnInfo();
            retval.Status = new StatusReturn { Message = "Không thành công", Code = 0 };
            retval.Data = null;
            retval.Pagination = null;

            try
            {
                var result = UserServices.UpdateCommonInfo(entity);
                retval.Status.Message = "Thêm mới thành công";
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
        [Route("checkusername")]
        //[Permission]
        public object CheckUserName(User request)
        {
            Logger.Info("[CheckUserNameUsers]");

            var retval = new ReturnInfo();
            retval.Status = new StatusReturn { Message = "Không thành công", Code = 0 };
            retval.Data = 0;
            retval.Pagination = new PaginationInfo { PageIndex = 0, PageSize = 0, TotalRows = 0 };

            try
            {
                var result = UserServices.CheckUserName(request.UserId, request.UserName);
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
        //[Permission(Command = CommandConstants.CREATE, Function = FunctionConstants.USER)]
        public object Insert([FromBody] User request)
        {
            Logger.Info("[InsertUsers]");

            var retval = new ReturnInfo();
            retval.Status = new StatusReturn { Message = "Không thành công", Code = 0 };
            retval.Data = null;
            retval.Pagination = new PaginationInfo { PageIndex = 0, PageSize = 0, TotalRows = 0 };
            var user = new User()
            {
                UserId = Guid.NewGuid().ToString(),
                Email = request.Email,
                UserName = request.UserName,
                FullName = request.FullName,
                Avatar = request.Avatar,
                Status = request.Status,
                Gender = request.Gender,
                BirthDay = request.BirthDay,
                Password = request.Password,
                Address = request.Address,
                PhoneNumber = request.PhoneNumber
            };

            try
            {
                var result = UserServices.Insert(user);
                retval.Status.Message = "Thêm mới thành công";
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
        [Route("update")]
        //[Permission(Command = CommandConstants.UPDATE, Function = FunctionConstants.USER)]
        public object Update(User request)
        {
            Logger.Info("[UpdateUsers]");

            var retval = new ReturnInfo();
            retval.Status = new StatusReturn { Message = "Không thành công", Code = 0 };
            retval.Data = null;
            retval.Pagination = new PaginationInfo { PageIndex = 0, PageSize = 0, TotalRows = 0 };
            try
            {
                var result = UserServices.Update(request);
                retval.Status.Message = "Update thành công";
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

        [HttpDelete]
        [Route("delete/{id}")]
        //[Permission(Command = CommandConstants.DELETE, Function = FunctionConstants.USER)]
        public object Delete(string id)
        {
            Logger.Info("[DeleteUsers]");

            var retval = new ReturnInfo();
            retval.Status = new StatusReturn { Message = "Không thành công", Code = 0 };
            retval.Data = null;
            retval.Pagination = new PaginationInfo { PageIndex = 0, PageSize = 0, TotalRows = 0 };
            try
            {
                UserServices.Delete(id);
                retval.Status.Message = "Xóa thành công  1 bản ghi";
                retval.Status.Code = 1;
                retval.Data = id.ToString();
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
        [Route("getRoles/{userId}")]
        [Permission]
        public object GetRoles(string userId)
        {
            Logger.Info("[GetRolesUsers]");

            var retval = new ReturnInfo();
            retval.Status = new StatusReturn { Message = "Không thành công", Code = 0 };
            retval.Data = null;
            retval.Pagination = new PaginationInfo { PageIndex = 0, PageSize = 0, TotalRows = 0 };
            try
            {
                var roles = RoleServices.SelectByUserId(userId);
                retval.Status.Message = "Thành công";
                retval.Status.Code = 1;
                retval.Data = roles;
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
        [Route("insertrole/{userId}")]
        //[Permission(Command = CommandConstants.CREATE, Function = FunctionConstants.USER)]
        public object InsertRole(string userId, [FromUri] int[] roleIds)
        {
            Logger.Info("[InsertRoleUsers]");

            var retval = new ReturnInfo();
            retval.Status = new StatusReturn { Message = "Không thành công", Code = 0 };
            retval.Data = null;
            retval.Pagination = new PaginationInfo { PageIndex = 0, PageSize = 0, TotalRows = 0 };
            try
            {
                foreach (var roleId in roleIds)
                {
                    UserRoleServices.Insert(userId, roleId);
                }
                retval.Status.Message = "Thành công";
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

        [HttpPost]
        [Route("removerole/{userId}")]
        //[Permission(Command = CommandConstants.DELETE, Function = FunctionConstants.USER)]
        public object RemoveRole(string userId, [FromUri] int[] roleIds)
        {
            Logger.Info("[RemoveRoleInUsers]");

            var retval = new ReturnInfo();
            retval.Status = new StatusReturn { Message = "Không thành công", Code = 0 };
            retval.Data = null;
            retval.Pagination = new PaginationInfo { PageIndex = 0, PageSize = 0, TotalRows = 0 };
            try
            {
                foreach (var roleId in roleIds)
                {
                    UserRoleServices.Delete(userId, roleId);
                }
                retval.Status = new StatusReturn { Message = "Xóa thành công " + roleIds.Count().ToString() + " quyền", Code = 1 };
            }
            catch (Exception ex)
            {
                retval.Status = new StatusReturn { Message = ex.Message, Code = -1 };
            }

            Logger.Info("[retval]" + retval.JSONSerializer());
            return retval;
        }

        [HttpPost]
        [Route("logout/{refreshtoken}")]
        public object Logout(string refreshtoken)
        {
            Logger.Info("[LogoutUser]");

            var retval = new ReturnInfo();
            retval.Status = new StatusReturn { Message = "Không thành công", Code = 0 };
            retval.Data = null;
            retval.Pagination = new PaginationInfo { PageIndex = 0, PageSize = 0, TotalRows = 0 };
            var refreshTokenSecret = ConfigurationManager.AppSettings["secretKeyRefresh"];
            refreshtoken = refreshtoken.Remove(refreshtoken.Length - refreshTokenSecret.Length);
            try
            {
                RefreshTokenServices.Delete(refreshtoken);
            }
            catch { }
            retval.Status = new StatusReturn { Message = "Đăng xuất thành công ", Code = 1 };

            Logger.Info("[retval]" + retval.JSONSerializer());
            return retval;
        }
    }
}
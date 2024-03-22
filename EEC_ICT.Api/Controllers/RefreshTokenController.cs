using EEC_ICT.Data.Models;
using EEC_ICT.Data.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace EEC_ICT.Api.Controllers
{
    [RoutePrefix("api/refreshtoken")]
    public class RefreshTokenController: BaseController
    {
        
        
        [HttpPost]
        [Route("refresh")]
        public object Refresh(string refreshToken)
        {
            var retval = new ReturnInfo();
            retval.Status = new StatusReturn { Code = -1, Message = "Không thành công" };
            retval.Data = null;

            // decode data
            var refreshTokenSecret = ConfigurationManager.AppSettings["secretKeyRefresh"];
            refreshToken = refreshToken.Remove(refreshToken.Length - refreshTokenSecret.Length);

            //permissions = "" + permissions;
            //roles = "" + roles;

            // find refresh token in database
            var refreshTokenInfo = RefreshTokenServices.SelectByRefreshToken(refreshToken);
            
            if (refreshTokenInfo != null)
            {
                // create new access token
                // var token = jwtService.GenerateSecurityToken(refreshTokenInfo.UserId, roles, permissions);
                var token = jwtService.GenerateSecurityTokenFixLength(refreshTokenInfo.UserId);
                // create new refresh token
                string newRefreshToken = Guid.NewGuid().ToString();

                // save refresh token in database
                var rfToken = RefreshTokenServices.Insert(refreshTokenInfo.UserId, newRefreshToken);

                // info send to client
                var infoToken = new
                {
                    access_token = token,
                    token_type = "Bearer",
                    refresh_token = rfToken
                };

                retval.Status.Code = 1;
                retval.Status.Message = "Refresh token thành công";
                retval.Data = infoToken;
            }
            else
            {
                retval.Status.Code = -1;
                retval.Status.Message = "Sai thông tin Refresh Token";
            }

            return retval;
        }
    }
}
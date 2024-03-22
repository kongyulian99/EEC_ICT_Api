using EEC_ICT.Api.Services;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace EEC_ICT.Api.Providers
{
    public class PermissionAttribute : AuthorizeAttribute
    {
        public string Function;
        public string Command;
        public JwtServices jwtService;

        // GET api/<controller>
        public PermissionAttribute()
        {
            jwtService = new JwtServices();
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var token = actionContext.Request.Headers.Authorization.ToString();

            var permission = Function + "_" + Command;
            var infoToken = jwtService.GetInfoClaimToken(token);
            if (jwtService.CheckToken(token))
            {
                if (infoToken.Roles.ToLower().IndexOf("root") == -1)
                {
                    if (infoToken.Permissions.IndexOf(permission) == -1 && permission != "_")
                    {
                        actionContext.Response = new HttpResponseMessage() {
                            StatusCode = HttpStatusCode.Forbidden,
                            Content = new StringContent("Quyền truy cập bị chặn")
                        };
                    }
                }
            }
            else
            {
                actionContext.Response = new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                    Content = new StringContent("Lỗi xác thực")

                };
            }
            
            
            
        }

    }
}
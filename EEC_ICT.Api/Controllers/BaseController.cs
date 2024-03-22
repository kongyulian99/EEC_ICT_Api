using EEC_ICT.Api.Services;
using System.Net.Http;
using System.ServiceModel.Channels;
using System.Web;
using System.Web.Http;

namespace EEC_ICT.Api.Controllers
{
    public class BaseController : ApiController
    {
        public  JwtServices jwtService;

        // GET api/<controller>
        public BaseController()
        {
            jwtService = new JwtServices();
        }
    }
}
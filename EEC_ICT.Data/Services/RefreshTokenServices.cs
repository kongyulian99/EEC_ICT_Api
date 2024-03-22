using EEC_ICT.Data.Core;
using EEC_ICT.Data.Models;
using EEC_ICT.Data.Repository;

namespace EEC_ICT.Data.Services
{
    public class RefreshTokenServices
    {
        private static RefreshTokenRepository rep = new RefreshTokenRepository();

        public static RefreshTokenModel SelectByRefreshToken(string refreshToken)
        {
            var dr = rep.SelectByRefreshToken(refreshToken);
            return SqlHelper.GetInfo<RefreshTokenModel>(dr);
        }

        public static RefreshTokenModel SelectByUserId(string userId)
        {
            var dr = rep.SelectByUserId(userId);
            return SqlHelper.GetInfo<RefreshTokenModel>(dr);
        }

        public static string Insert(string userId, string refreshToken)
        {
            return rep.Insert(userId, refreshToken);
        }

        public static string Delete(string refreshtoken)
        {
            return rep.Delete(refreshtoken);
        }
    }
}
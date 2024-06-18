using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineClient.Common.DTO
{
    #region Request DTO
    public class UserLoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    #endregion

    #region Response DTO
    public class LoggedInUserResponse : BaseResponseDTO
    {
        public int UserId { get; set; }
        public int UserRoleId { get; set; }
        public string UserName { get; set; }
    }
    #endregion
}

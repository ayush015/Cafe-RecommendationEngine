using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineServer.Common.DTO
{
    public class UserLoginRequest
    { 
      public string UserName { get; set; }
      public string Password { get; set; }
    }

    public class LoggedInUserResponse : BaseDTO
    { 
      public int UserId { get; set; }
      public int UserRoleId { get; set; }
      public string UserName { get; set; }
    }


}

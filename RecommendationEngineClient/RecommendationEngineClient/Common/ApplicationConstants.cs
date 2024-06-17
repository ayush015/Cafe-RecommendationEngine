using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineClient.Common
{
    public class ApplicationConstants
    {
        public const string SocketAddress = "127.0.0.1";
        public const int Port = 3000;

        #region Generic Response Constants
        public const string StatusSuccess = "Success";
        public const string StatusFailed = "Failed";
        #endregion
    }
}

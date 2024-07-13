using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineServer.Common.Exceptions
{
    public class NotificationException : Exception
    {
        public NotificationException(string message) : base(message) { }
    }

    public class NoNewDailyMenuItemAddedException : NotificationException
    {
        public NoNewDailyMenuItemAddedException() : base(ApplicationConstants.NoNewDailyMenuItemAdded) { }
    }
}

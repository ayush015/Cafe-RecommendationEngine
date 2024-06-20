using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineClient._10_Common.Enum
{
    public enum ChefChoice
    {
        [Description("Menu List")]
        MenuList = 1,

        [Description("Add Daily MenuItem")]
        AddDailyMenuItem = 2,

        [Description("Send Notification")]
        SendNotification = 3,

    }
}

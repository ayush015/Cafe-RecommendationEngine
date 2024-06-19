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

        [Description("Recommended Menu List")]
        RecommendedMenuList = 2,

        [Description("Add Daily MenuItem")]
        AddDailyMenuItem = 3,

        [Description("Send Notification")]
        SendNotification = 4,

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineClient._10_Common.Enum
{
    public enum DiscardedMenuChoice
    {
        [Description("Discard Menu Item")]
        DiscardedMenuItem = 1,

        [Description("Improve Menu Item")]
        ImproveMenuItem = 2,

    }
}

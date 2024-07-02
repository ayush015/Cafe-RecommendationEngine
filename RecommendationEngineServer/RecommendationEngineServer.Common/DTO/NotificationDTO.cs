using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineServer.Common.DTO
{
    public class DiscardedMenuResponse : BaseResponseDTO
    {
        public List<RecommendedMenuModel> DiscardedMenus { get; set; }
    }

    public class MenuImprovementNotification
    {
        public DateTime CurrentDate { get; set; }
        public int MenuId { get; set; }
    }

    public class DailyMenuItemModel
    { 
      public int MenuId { get; set; }
      public int DailyMenuId { get; set; }
      public int PreferenceScore { get; set; }
    }

}

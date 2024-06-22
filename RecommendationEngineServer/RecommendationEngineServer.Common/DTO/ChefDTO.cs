using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineServer.Common.DTO
{
    #region Response DTO
    public class RecommendedMenuResponse : BaseResponseDTO
    { 
      public List<RecommendedMenuModel> RecommendedMenus { get; set; }
    }
    #endregion

    #region Models
    public class MenuRecommendationModel
    {
        public int MenuId { get; set; }
        public double AverageRating { get; set; }
        public List<string> Comments { get; set; }
        public int OrderFrequency { get; set; }
        public double RecommendationScore { get; set; }
    }

    public class RecommendedMenuModel : MenuListModel
    {
        public double RecommendationScore { get; set; }
    }


    public class SentimentModel
    { 
      public int PositiveSentimentScore { get; set; }
      public int NegativeSentimentScore { get; set; }
      public int NeutralSentimentScore { get; set; }
    
    }

    public class DailyMenuListModel : MenuListModel
    {
        public int DailyMenuId { get; set; }
    }

    public class UserOrderFrequencyModel
    {
        public int DailyMenuId { get; set; }
        public int MenuId { get; set; }
        public int OrderFrequency { get; set; }
    }

    #endregion
}

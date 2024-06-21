using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineServer.Common.DTO
{
    public class DailyMenuListViewModel : MenuListViewModel
    {
        public int DailyMenuId { get; set; }
    }

    public class UserOrderFrequency
    {
        public int DailyMenuId { get; set; }
        public int MenuId { get; set; }
        public int OrderFrequency { get; set; }
    }

    public class MenuRecommendation
    {
        public int MenuId { get; set; }
        public double AverageRating { get; set; }
        public List<string> Comments { get; set; }
        public int OrderFrequency { get; set; }
        public double RecommendationScore { get; set; }
    }

    public class RecommendedMenu : MenuListViewModel
    {
        public double RecommendationScore { get; set; }
    }

    public class RecommendedMenuResponse : BaseResponseDTO
    { 
      public List<RecommendedMenu> RecommendedMenus { get; set; }
    }

    public class Sentiment
    { 
      public int PositiveSentimentScore { get; set; }
      public int NegativeSentimentScore { get; set; }
      public int NeutralSentimentScore { get; set; }
    
    }


}

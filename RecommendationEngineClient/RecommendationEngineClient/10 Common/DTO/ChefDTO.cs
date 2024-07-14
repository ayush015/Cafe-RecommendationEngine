using RecommendationEngineClient.Common.DTO;

namespace RecommendationEngineClient._10_Common.DTO
{

    public class MenuItem
    {
        public DateTime CurrentDate { get; set; }
        public List<int> MenuItemsIds { get; set; }

    }


    public class RecommendedMenu : MenuListViewModel
    {
        public double RecommendationScore { get; set; }
    }

    public class RecommendedMenuResponse : BaseResponseDTO
    {
        public List<RecommendedMenu> RecommendedMenus { get; set; }
    }

    public class DiscardedMenuResponse : BaseResponseDTO
    {
        public List<RecommendedMenu> DiscardedMenus { get; set; }
    }
}

using RecommendationEngineClient.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineClient._10_Common.DTO
{
    public class RecommendedMenu : MenuListViewModel
    {
        public double RecommendationScore { get; set; }
    }

    public class RecommendedMenuResponse : BaseResponseDTO
    {
        public List<RecommendedMenu> RecommendedMenus { get; set; }
    }
}

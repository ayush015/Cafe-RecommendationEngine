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
}

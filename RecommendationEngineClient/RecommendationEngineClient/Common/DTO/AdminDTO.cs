using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineClient.Common.DTO
{
    public class MenuListResponse : BaseResponseDTO
    {
        public List<MenuListViewModel> MenuList { get; set; }
    }


    #region View Model
    public class MenuListViewModel
    {
        public int MenuId { get; set; }
        public int FoodItemId { get; set; }
        public string FoodItemName { get; set; }
        public int MealTypeId { get; set; }
        public string MealTypeName { get; set; }
    }
    #endregion
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineClient.Common.DTO
{

    #region Request DTO
    public class AddMenuItemRequest
    {
        public string FoodItemName { get; set; }
        public int MealTypeId { get; set; }
        public int FoodTypeId { get; set; }
        public int SpiceLevelId { get; set; }
        public int CuisineId { get; set; }
        public bool IsSweet { get; set; }
    }
    public class UpdateMenuItemRequest
    {
        public int MenuId { get; set; }
        public string FoodItemName { get; set; }
        public int MealTypeId { get; set; }
        public int FoodTypeId { get; set; }
        public int SpiceLevelId { get; set; }
        public int CuisineId { get; set; }
        public bool IsSweet { get; set; }
    }
    #endregion

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

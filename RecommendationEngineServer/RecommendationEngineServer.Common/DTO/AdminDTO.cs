using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineServer.Common.DTO
{

    #region Request DTO
    public class AddMenuItemRequest
    { 
      public string FoodItemName { get; set; }
      public int MealTypeId { get; set; }
    }
   
    public class UpdateMenuItemRequest
    {
        public int MenuId { get; set; }
        public string FoodItemName { get; set; }
        public int MealTypeId { get; set; }
    }
    #endregion

    #region Response DTO
    public class MenuListResponse : BaseResponseDTO
    { 
        public List<MenuListModel> MenuList { get; set; }
    }

    #endregion

    #region View Model
    public class MenuListModel
    {
        public int MenuId { get; set; }
        public int FoodItemId { get; set; }
        public string FoodItemName { get; set; }
        public int MealTypeId { get; set; }
        public string MealTypeName { get; set; }
    }
    #endregion
}

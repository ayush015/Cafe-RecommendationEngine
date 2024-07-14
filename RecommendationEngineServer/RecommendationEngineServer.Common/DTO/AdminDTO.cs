namespace RecommendationEngineServer.Common.DTO
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
    }
    #endregion

    #region Response DTO
    public class MenuListResponse : BaseResponseDTO
    {
        public List<MenuListModel> MenuList { get; set; }
    }

    #endregion

    #region Models
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

using System.ComponentModel.DataAnnotations;


namespace RecommendationEngineServer.DAL.Models
{
    public class Menu
    {
        [Key]
        public int Id { get; set; }
        public int FoodItemId { get; set; }
        public int MealTypeId { get; set; }
        public int FoodTypeId { get; set; }
        public int SpiceLevelId { get; set; }
        public int CuisineTypeId { get; set; }
        public bool IsSweet { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsDiscarded { get; set; }
        public virtual FoodItem FoodItem { get; set; }
        public virtual MealType MealType { get; set; }
    }
}

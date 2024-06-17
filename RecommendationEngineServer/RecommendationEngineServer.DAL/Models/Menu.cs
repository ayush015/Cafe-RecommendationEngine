using System.ComponentModel.DataAnnotations;


namespace RecommendationEngineServer.DAL.Models
{
    public class Menu
    {
        [Key]
        public int Id { get; set; }
        public int FoodItemId { get; set; }
        public int MealTypeId { get; set; }
        public bool IsDeleted { get; set; }
        public virtual FoodItem FoodItem { get; set; }
        public virtual MealType MealType { get; set; }
    }
}

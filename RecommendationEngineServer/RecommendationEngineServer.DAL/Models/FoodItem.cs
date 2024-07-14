using System.ComponentModel.DataAnnotations;

namespace RecommendationEngineServer.DAL.Models
{
    public class FoodItem
    {
        [Key]
        public int Id { get; set; }
        public string FoodName { get; set; }
        public bool IsDeleted { get; set; }
    }
}

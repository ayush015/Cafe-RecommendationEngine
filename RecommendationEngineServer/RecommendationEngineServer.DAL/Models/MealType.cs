using System.ComponentModel.DataAnnotations;


namespace RecommendationEngineServer.DAL.Models
{
    public class MealType
    {
        [Key]
        public int Id { get; set; }
        public string MealTypeName { get; set; }
        public bool IsDeleted { get; set; }
    }
}

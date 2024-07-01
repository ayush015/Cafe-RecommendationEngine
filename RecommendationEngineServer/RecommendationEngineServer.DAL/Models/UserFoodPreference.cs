using System.ComponentModel.DataAnnotations;

namespace RecommendationEngineServer.DAL.Models
{
    public class UserFoodPreference
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FoodTypeId { get; set; }
        public int SpiceLevelId { get; set; }
        public int PreferredCuisineId { get; set; }
        public bool HasSweetTooth { get; set; }
        public virtual User User { get; set; }
    }
}

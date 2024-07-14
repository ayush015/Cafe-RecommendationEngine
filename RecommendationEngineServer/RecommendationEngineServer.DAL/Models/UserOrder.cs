using System.ComponentModel.DataAnnotations;


namespace RecommendationEngineServer.DAL.Models
{
    public class UserOrder
    {
        [Key]
        public int Id { get; set; }
        public int DailyMenuId { get; set; }
        public int OrderId { get; set; }
        public virtual DailyMenu DailyMenu { get; set; }
        public virtual Order Order { get; set; }    
    }
}

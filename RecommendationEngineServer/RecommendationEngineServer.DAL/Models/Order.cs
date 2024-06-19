using System.ComponentModel.DataAnnotations;


namespace RecommendationEngineServer.DAL.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsFeedbackGiven { get; set; }
        public virtual User User { get; set; }
    }
}


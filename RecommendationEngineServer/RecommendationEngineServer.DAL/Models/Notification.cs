using System.ComponentModel.DataAnnotations;

namespace RecommendationEngineServer.DAL.Models
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }
        public int Message { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}

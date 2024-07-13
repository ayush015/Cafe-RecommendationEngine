using System.ComponentModel.DataAnnotations;

namespace RecommendationEngineServer.DAL.Models
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }
        public string Message { get; set; }
        public int NotificationTypeId { get; set; } = 1;
        public DateTime CreatedDate { get; set; }
        public virtual NotificationType NotificationType { get; set; }  
    }
}

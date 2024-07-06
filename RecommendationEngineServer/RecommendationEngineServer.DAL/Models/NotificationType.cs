using System.ComponentModel.DataAnnotations;

namespace RecommendationEngineServer.DAL.Models
{
    public class NotificationType
    {
        [Key]
        public int Id { get; set; }
        public string NotificationTypeName { get; set; }
        public bool IsDeleted { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecommendationEngineServer.DAL.Models
{
    public class UserNotification
    {
        [Key]
        public int Id { get; set; }
        //[ForeignKey("User")]
        public int UserId { get; set; }
        public int LastSeenNotificationId { get; set; }
        public virtual User User { get; set; }
    }
}

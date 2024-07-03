using System.ComponentModel.DataAnnotations;

namespace RecommendationEngineServer.DAL.Models
{
    public class UserMenuFeedbackAnswer
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MenuFeedbackQuestionId { get; set; }
        public string Answer { get; set; }
        public int MenuId { get; set; }
        public virtual User User { get; set; }
        public virtual MenuFeedbackQuestion MenuFeedbackQuestion { get; set;}
        public virtual Menu Menu { get; set; }
    }
}

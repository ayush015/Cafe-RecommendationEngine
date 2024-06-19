using System.ComponentModel.DataAnnotations;

namespace RecommendationEngineServer.DAL.Models
{
    public class Feedback
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MenuId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime Created { get; set; }
        public bool IsDeleted { get; set; }
        public virtual Menu Menu { get; set; }
        public virtual User User { get; set; }
    }
}

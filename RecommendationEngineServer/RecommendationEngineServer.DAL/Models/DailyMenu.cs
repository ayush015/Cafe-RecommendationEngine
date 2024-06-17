using System.ComponentModel.DataAnnotations;

namespace RecommendationEngineServer.DAL.Models
{
    public class DailyMenu
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public bool IsDeleted { get; set; }
        public int MenuId { get; set; }
        public virtual Menu Menu { get; set; }
    }
}

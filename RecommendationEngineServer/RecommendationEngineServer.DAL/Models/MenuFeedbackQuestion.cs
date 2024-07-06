using System.ComponentModel.DataAnnotations;

namespace RecommendationEngineServer.DAL.Models
{
    public class MenuFeedbackQuestion
    {
        [Key]
        public int Id { get; set; }
        public string Question{ get; set; }
    }
}

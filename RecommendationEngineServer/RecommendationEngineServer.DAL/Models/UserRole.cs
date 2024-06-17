using System.ComponentModel.DataAnnotations;

namespace RecommendationEngineServer.DAL.Models
{
    public class UserRole
    {
        [Key]
        public int Id { get; set; }
        public string UserRoleName { get; set; }
        public bool IsDeleted { get; set; }
    }
}

namespace RecommendationEngineClient._10_Common.DTO
{
    public class MenuImprovementNotification
    {
        public DateTime CurrentDate { get; set; }
        public int MenuId { get; set; }
    }

    public class DailyMenuNotification
    {
        public DateTime CurrentDate { get; set; }
        public int UserId { get; set; }
    }
}

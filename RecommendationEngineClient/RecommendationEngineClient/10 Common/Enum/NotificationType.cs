using System.ComponentModel;

namespace RecommendationEngineClient._10_Common.Enum
{
    public enum NotificationType
    {
        [Description("New Menu Item")]
        NewMenuItem = 1,

        [Description("New Daily Menu Item")]
        NewDailyMenuItem = 2,

        [Description("Discarded Menu Item")]
        DiscardedMenuItem = 3,

        [Description("Menu Improvement")]
        MenuImprovement = 4,
    }
}

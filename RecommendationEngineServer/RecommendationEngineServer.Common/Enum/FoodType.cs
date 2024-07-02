using System.ComponentModel;

namespace RecommendationEngineServer.Common.Enum
{
    public enum FoodType
    {
        [Description("Vegetarian")]
        Vegetarian = 1,

        [Description("Non-Vegetarian")]
        NonVegetarian = 2,

        [Description("Eggetarian")]
        Eggetarian = 3,
    }
}

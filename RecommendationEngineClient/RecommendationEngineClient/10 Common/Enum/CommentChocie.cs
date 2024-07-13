using System.ComponentModel;

namespace RecommendationEngineClient._10_Common.Enum
{
    public enum CommentChoice
    {
        [Description("Very Good")]
        VeryGood = 1,

        [Description("Good")]
        Good = 2,

        [Description("Average")]
        Average = 3,

        [Description("Spicy")]
        Spicy = 4,

        [Description("Tasty")]
        Tasty = 5,

        [Description("Bad")]
        Bad = 6,

        [Description("Cannot Recommend")]
        CannotRecommend = 7,
    }
}

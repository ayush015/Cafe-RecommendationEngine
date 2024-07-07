using System.ComponentModel;

namespace RecommendationEngineServer.Common.Enum
{
    public enum CuisineType
    {
        [Description("North Indian")]
        NorthIndian = 1,

        [Description("South Indian")]
        SouthIndian = 2,

        [Description("Others")]
        Others = 3,
    }
}

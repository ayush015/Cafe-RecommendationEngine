using System.ComponentModel;

namespace RecommendationEngineClient.Common.Enum
{
    public enum AdminChoice
    {
        [Description("Add Menu")]
        AddMenu = 1,

        [Description("Update Menu")]
        UpdateMenu = 2,

        [Description("Menu List")]
        MenuList = 3,

        [Description("Remove Item")]
        RemoveItem = 4,
    }
}

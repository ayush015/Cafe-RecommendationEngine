using System.ComponentModel;

namespace RecommendationEngineClient.Common.Enum
{
    public enum UserRole
    {
        [Description("Admin")]
        Admin = 1,

        [Description("Employee")]
        Employee = 2,

        [Description("Chef")]
        Chef = 3,
    }
}

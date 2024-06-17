using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineClient.Common.Enum
{
    public enum UserRole
    {
        [Description("Admin")]
        Admin = 1,

        [Description("Chef")]
        Chef = 2,

        [Description("Employee")]
        Employee = 3,
    }
}

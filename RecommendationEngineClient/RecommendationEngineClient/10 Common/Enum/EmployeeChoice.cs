using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineClient._10_Common.Enum
{
    public enum EmployeeChoice
    {
      
        [Description("Select Items")]
        SelectItems = 1,

        [Description("Update Profile")]
        UpdateProfile = 2,
    }
}

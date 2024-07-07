using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineClient._10_Common
{
    public class ApiEndpoints
    {
        #region Controllers
        public const string LoginController = "Login";
        public const string AdminController = "Admin";
        public const string EmployeeController = "Employee";
        public const string ChefController = "Chef";
        public const string NotificationController = "Notification";
        #endregion


        #region EmployeeController Action Method
        public const string GetNotification = "GetNotification";
        public const string GiveFeedBack = "GiveFeedBack";
        public const string SelectFoodItemsFromDailyMenu = "SelectFoodItemsFromDailyMenu";
        public const string GetMenuItemByOrderId = "GetMenuItemByOrderId";
        public const string GetMenuFeedBackQuestions = "GetMenuFeedBackQuestions";
        public const string AddMenuImprovementFeedbacks = "AddMenuImprovementFeedbacks";
        public const string AddUserPreference = "AddUserPreference";
        public const string GetDailyRolledOutMenu = "GetDailyRolledOutMenu";
        #endregion
    }
}

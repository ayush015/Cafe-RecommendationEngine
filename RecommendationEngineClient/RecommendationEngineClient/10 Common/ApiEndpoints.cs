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

        #region ChefController Action Methods
        public const string GetMenuListItems = "GetMenuListItems";
        public const string AddDailyMenuItem = "AddDailyMenuItem";
        public const string SendDailyMenuNotification = "SendDailyMenuNotification";
        public const string DiscardMenu = "DiscardMenu";
        #endregion

        #region NotificationController Action Methods
        public const string GetMonthlyNotification = "GetMonthlyNotification";
        public const string AddNewNotificationForDiscardedMenuFeedback = "AddNewNotificationForDiscardedMenuFeedback";
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

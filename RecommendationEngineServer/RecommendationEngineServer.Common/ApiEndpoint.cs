using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineServer.Common
{
    public static class ControllerNames
    {
        public const string Login = "Login";
        public const string Admin = "Admin";
        public const string Chef = "Chef";
        public const string Employee = "Employee";
        public const string Notification = "Notification";
    }

    public static class ControllerActions
    {
        #region AuthController actions
        public const string AuthLogin = "Login";
        #endregion

        #region AdminController actions
        public const string AddMenuItem = "AddMenuItem";
        public const string GetMenuList = "GetMenuList";
        public const string RemoveMenuItem = "RemoveMenuItem";
        public const string UpdateMenuItem = "UpdateMenuItem";
        #endregion

        #region ChefController actions
        public const string AddDailyMenuItem = "AddDailyMenuItem";
        public const string SendDailyMenuNotification = "SendDailyMenuNotification";
        public const string GetMenuListItems = "GetMenuListItems";
        public const string DiscardMenu = "DiscardMenu";
        #endregion

        #region EmployeeController actions
        public const string GetNotification = "GetNotification";
        public const string SelectFoodItemsFromDailyMenu = "SelectFoodItemsFromDailyMenu";
        public const string GiveFeedback = "GiveFeedBack";
        public const string GetMenuItemByOrderId = "GetMenuItemByOrderId";
        public const string AddMenuImprovementFeedbacks = "AddMenuImprovementFeedbacks";
        public const string GetMenuFeedbackQuestions = "GetMenuFeedBackQuestions";
        public const string AddUserPreference = "AddUserPreference";
        public const string GetDailyRolledOutMenu = "GetDailyRolledOutMenu";
        #endregion

        #region NotificationController actions
        public const string GetMonthlyNotification = "GetMonthlyNotification";
        public const string AddNewNotificationForDiscardedMenuFeedback = "AddNewNotificationForDiscardedMenuFeedback";
        #endregion
    }

}

namespace RecommendationEngineServer.Common
{
    public class ApplicationConstants
    {
        public const int ServerPort = 3000;

        #region Generic Response Constants
        public const string StatusSuccess = "Success";
        public const string StatusFailed = "Failed";
        #endregion

        #region Auth Constants
        public const string UserNamePasswordIsNull = "UserName or password was empty. Try again !";
        public const string UserNameAndPasswordDidNotMatch = "The entered UserName and Password did not match";
        public const string UserLoginSuccessfull = "Logged in Successfully";
        #endregion

        #region Admin Constants
        public const string InvalidMenuItem = "Menu Item trying to add is Invalid";
        public const string MenuItemNotFound = "Menu Item not found";
        public const string MenuItemRemovedSuccessfully = "Menu item has been removed successfully";
        public const string MenuItemALreadyPresent = "Menu Item you are trying to add already exists";
        public const string MenuItemAddedSuccessfully = "Menu item has been added successfully";
        public const string MenuListIsEmpty = "There are no items in Menu";
        public const string MenuItemUpdatedSuccessfully = "Menu has be updated successfully";
        #endregion

        #region Chef Constants
        public const string DailyMenuListCannotbeEmpty = "Daily Menu List Item cannot be empty";
        public const string DailyMenuAddedSuccessfully = "Daily Menu has been added successfully";
        #endregion

        #region Notification Constants
        public const string SentNotificationSuccessfully = "The notification has been sent successfully";
        public const string NoNewDailyMenuItemAdded = "No New Daily Menu Item has been added to send notification";
        public const string NotificationReceivedSuccessfully = "Notification has been received successfully";
        #endregion

        #region
        public const string FoodItemSelectSuccessfully = "Food Items has been select";
        public const string FeedbackSuccess = "Thanks for provinding feedback";
        #endregion
    }
}

namespace RecommendationEngineServer.Common.Exceptions
{
    public class NotificationException : Exception
    {
        public NotificationException(string message) : base(message) { }
    }

    public class NoNewDailyMenuItemAddedException : NotificationException
    {
        public NoNewDailyMenuItemAddedException() : base(ApplicationConstants.NoNewDailyMenuItemAdded) { }
    }
}

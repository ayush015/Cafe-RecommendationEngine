namespace RecommendationEngineServer.Common.Exceptions
{
    public class MenuException : Exception
    {
        public MenuException(string message) : base(message) { }
    }

    public class  MenuItemNotFoundException : MenuException
    {
        public MenuItemNotFoundException() : base(ApplicationConstants.MenuItemNotFound) { }
    }

    public class MenuItemAlreadyPresentException : MenuException
    {
        public MenuItemAlreadyPresentException() : base(ApplicationConstants.MenuItemNotFound) { }
    }
}

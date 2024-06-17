namespace RecommendationEngineServer.Common
{
    public class ApplicationConstants
    {
        public const int ServerPort = 3000;

        //Generic Response
        public const string StatusSuccess = "Success";
        public const string StatusFailed = "Failed";


        //Auth
        public const string UserNamePasswordIsNull = "UserName or password was empty. Try again !";
        public const string UserNameAndPasswordDidNotMatch = "The entered UserName and Password did not match";
        public const string UserLoginSuccessfull = "Logged in Successfully";
    }
}

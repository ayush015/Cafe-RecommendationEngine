using RecommendationEngineServer.DAL.Repository.DailyMenu;
using RecommendationEngineServer.DAL.Repository.Feedback;
using RecommendationEngineServer.DAL.Repository.FoodItem;
using RecommendationEngineServer.DAL.Repository.ImprovementQuestion;
using RecommendationEngineServer.DAL.Repository.ImprovementRecord;
using RecommendationEngineServer.DAL.Repository.MealType;
using RecommendationEngineServer.DAL.Repository.Menu;
using RecommendationEngineServer.DAL.Repository.Notification;
using RecommendationEngineServer.DAL.Repository.Order;
using RecommendationEngineServer.DAL.Repository.User;
using RecommendationEngineServer.DAL.Repository.UserFoodPreference;
using RecommendationEngineServer.DAL.Repository.UserNotification;
using RecommendationEngineServer.DAL.Repository.UserOrder;
using RecommendationEngineServer.DAL.Repository.UserRole;

namespace RecommendationEngineServer.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RecommendationEngineDBContext _dbContext;

        public IUser User { get; }
        public IUserRole UserRole { get; }
        public IUserOrder UserOrder { get; }
        public IOrder Order { get; }
        public IMenu Menu { get; }
        public IMealType MealType { get; }
        public IFoodItem FoodItem { get; }
        public IFeedback Feedback { get; }
        public IDailyMenu DailyMenu { get; }
        public INotification Notification { get; }
        public IUserNotification UserNotification { get; }
        public IMenuFeedbackQuestion MenuFeedbackQuestion { get; }
        public IUserMenuFeedbackAnswer UserMenuFeedbackAnswer { get; }
        public IUserFoodPreference UserFoodPreference { get; }
        public UnitOfWork(RecommendationEngineDBContext dbContext)
        {
            _dbContext = dbContext;
            User = new User(_dbContext);
            UserRole = new UserRole(_dbContext);
            UserOrder = new UserOrder(_dbContext);
            Order = new Order(_dbContext);
            Menu = new Menu(_dbContext);
            MealType = new MealType(_dbContext);
            FoodItem = new FoodItem(_dbContext);
            Feedback = new Feedback(_dbContext);
            DailyMenu = new DailyMenu(_dbContext);
            Notification = new Notification(_dbContext);
            UserNotification = new UserNotification(_dbContext);
            MenuFeedbackQuestion = new MenuFeedbackQuestion(_dbContext);
            UserMenuFeedbackAnswer = new UserMenuFeedbackAnswer(_dbContext);
            UserFoodPreference = new UserFoodPreference(_dbContext);
        }

        public async Task<int> Complete()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}


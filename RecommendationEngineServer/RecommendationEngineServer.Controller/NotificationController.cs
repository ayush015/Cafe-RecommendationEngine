using RecommendationEngineServer.Common;
using RecommendationEngineServer.Common.DTO;
using RecommendationEngineServer.Service.Notifications;
using RecommendationEngineServer.Service.Chef;
using RecommendationEngineServer.Service.Employee;


namespace RecommendationEngineServer.Controller
{
    public class NotificationController
    {
        private INotificationService _notificationService;
        private IChefService _chefService;
        public NotificationController(INotificationService notificationService,IChefService chefService)
        {
            _notificationService = notificationService;
            _chefService = chefService;
        }

        public async Task<DiscardedMenuResponse> GetMonthlyDiscardedMenuNotification(DateTime currentDate)
        {

            var recommendedMenus = await _chefService.GetMenuListItems();
            try
            {
                var discardedMenus = await _notificationService.GetMonthlyDiscardedMenuNotification(currentDate, recommendedMenus);

                if(discardedMenus.Count < 0)
                {
                    return new DiscardedMenuResponse
                    {
                        Status = ApplicationConstants.StatusSuccess,
                    };
                }
                return new DiscardedMenuResponse
                {
                    DiscardedMenus = discardedMenus,
                    Status = ApplicationConstants.StatusSuccess,
                };

            }
            catch (Exception ex)
            {
                return new DiscardedMenuResponse
                {
                    Message = ex.Message,
                    Status = ApplicationConstants.StatusSuccess,
                };

            }
        }

        public async Task<BaseResponseDTO> AddNewNotificationForDiscardedMenuFeedback(MenuImprovementNotificationRequest menuImprovement)
        {
            try
            {
                await _notificationService.AddNewNotificationForDiscardedMenuFeedback(menuImprovement);
                return new BaseResponseDTO
                { Status = ApplicationConstants.StatusSuccess };
            }
            catch(Exception ex) 
            {
                return new BaseResponseDTO
                { Status = ApplicationConstants.StatusFailed, Message=ex.Message };
            }
        }
    }
}


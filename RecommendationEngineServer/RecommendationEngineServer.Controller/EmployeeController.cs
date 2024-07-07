using RecommendationEngineServer.Common;
using RecommendationEngineServer.Common.DTO;
using RecommendationEngineServer.Service.Employee;

namespace RecommendationEngineServer.Controller
{
    public class EmployeeController
    {
        private IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeLogic)
        {
            _employeeService = employeeLogic;
        }

        public async Task<NotificationResponse> GetNotifcation(NotificationRequest notificationRequest)
        {
            try
            {
                var notification = await _employeeService.GetNotification(notificationRequest);
                if(string.IsNullOrEmpty(notification.NotificationMessgae))
                {
                    
                    notification.Status = ApplicationConstants.StatusSuccess;
                    notification.IsNewNotification = false;
                    return notification;
                }


                notification.Status = ApplicationConstants.StatusSuccess;
                notification.IsNewNotification = true;
                notification.Message = ApplicationConstants.NotificationReceivedSuccessfully; 
                return notification;
            }
            catch (Exception ex)
            {
                return new NotificationResponse
                {
                    Status = ApplicationConstants.StatusFailed,
                    Message = ex.Message
                };
            }
        }

        public async Task<OrderResponse> SelectFoodItemsFromDailyMenu(OrderRequest orderRequest)
        {
            try
            {
                var orderId = await _employeeService.SelectFoodItemsFromDailyMenu(orderRequest);
                return new OrderResponse
                {
                    OrderId = orderId,
                    Status = ApplicationConstants.StatusSuccess,
                    Message = ApplicationConstants.FoodItemSelectSuccessfully
                };
            }
            catch (Exception ex)
            {
                return new OrderResponse
                {
                    Status = ApplicationConstants.StatusFailed,
                    Message = ex.Message
                };
            }
        }

        public async Task<BaseResponseDTO> GiveFeedBack(List<GiveFeedBackRequest> giveFeedBackRequest)
        {
            try
            {
                await _employeeService.GiveFeedBack(giveFeedBackRequest);
                return new BaseResponseDTO
                {
                    Status = ApplicationConstants.StatusSuccess,
                    Message = ApplicationConstants.FeedbackSuccess
                };
            }
            catch (Exception ex)
            {
                return new BaseResponseDTO
                {
                    Status = ApplicationConstants.StatusFailed,
                    Message = ex.Message
                };
            }
        }

        public async Task<UserOrderMenuListResponse> GetMenuItemByOrderId(int orderId)
        {
            try
            {
               var orderedMenuList =  await _employeeService.GetMenuItemsByOrderId(orderId);
                if (orderedMenuList == null || orderedMenuList.Count < 1)
                {
                    throw new Exception(ApplicationConstants.MenuListIsEmpty);
                }
                List<UserOrderMenuModel> menuList = new List<UserOrderMenuModel>();
                menuList.AddRange(orderedMenuList);
                return new UserOrderMenuListResponse
                {
                    Status = ApplicationConstants.StatusSuccess,
                    UserOrderMenus = menuList
                };
            }
            catch(Exception ex)
            {
                return new UserOrderMenuListResponse
                {
                    Status = ApplicationConstants.StatusFailed,
                    Message = ex.Message,
                    UserOrderMenus = null
                };
            }

        }

        public async Task<BaseResponseDTO> AddMenuImprovementFeedbacks(MenuImprovementFeedbackRequest menuImprovementFeedbackRequest)
        {
            try
            {
                await _employeeService.AddUserMenuImprovementFeedback(menuImprovementFeedbackRequest);
                return new BaseResponseDTO { Status = ApplicationConstants.StatusSuccess, Message = "Added Successfully" };
            }
            catch (Exception ex) 
            { 
                return new BaseResponseDTO { Status = ApplicationConstants.StatusFailed, Message = ex.Message };
            }
        }

        public async Task<FeedbackQuestionResponse> GetMenuFeedBackQuestions()
        {
            try
            {
                var result = await _employeeService.GetMenuFeedBackQuestions();
                return new FeedbackQuestionResponse
                {
                    FeedbackQuestions = result,
                    Status = ApplicationConstants.StatusSuccess,

                };
            }
            catch(Exception ex)
            {
                return new FeedbackQuestionResponse
                {
                    Status = ApplicationConstants.StatusFailed,
                    Message = ex.Message
                };
            }
        }

        public async Task<BaseResponseDTO> AddUserPreference(UserPreferenceRequest userPreferenceRequest)
        {
            try
            {
                await _employeeService.AddUserPreference(userPreferenceRequest);
                return new BaseResponseDTO
                { 
                     Status = ApplicationConstants.StatusSuccess,
                     Message = "Added Successfully"
                };
            }
            catch (Exception ex) 
            {
                return new BaseResponseDTO
                {
                    Status = ApplicationConstants.StatusFailed,
                    Message = ex.Message    
                };
            }
        }

        public async Task<DailyRolledOutMenuResponse> GetDailyRolledOutMenu(DailyRolledOutMenuRequest dialyRolledOutMenuRequest)
        {
            try
            {
                var result = await _employeeService.GetRolledOutMenus(dialyRolledOutMenuRequest);
                return new DailyRolledOutMenuResponse
                {
                    Status = ApplicationConstants.StatusSuccess,
                    RolledOutMenu = result
                };
            }
            catch(Exception ex)
            {
                return new DailyRolledOutMenuResponse
                { 
                   Status = ApplicationConstants.StatusFailed,  
                   Message = ex.Message 
                };

            }
        }
    }
}


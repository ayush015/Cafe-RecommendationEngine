using RecommendationEngineServer.Common;
using RecommendationEngineServer.Common.DTO;
using RecommendationEngineServer.Common.Exceptions;
using RecommendationEngineServer.Service.Chef;

namespace RecommendationEngineServer.Controller
{
    public class ChefController
    {
        private IChefService _chefService;
        public ChefController(IChefService chefService)
        {
            _chefService = chefService;
        }

        public async Task<BaseResponseDTO> AddDailyMenuItem(MenuItem menuItem)
        {
            try
            {
                var result = await _chefService.AddDailyMenuItem(menuItem);
                if(result == 0)
                {
                    return new BaseResponseDTO
                    {
                        Status = ApplicationConstants.StatusFailed,
                        Message = ApplicationConstants.DailyMenuDateAlreadyExistError
                    };
                }
                return new BaseResponseDTO
                {
                    Status = ApplicationConstants.StatusSuccess,
                    Message = ApplicationConstants.DailyMenuAddedSuccessfully
                };
            }
            catch (MenuException ex)
            {
                return new BaseResponseDTO
                {
                    Status = ApplicationConstants.StatusFailed,
                    Message = ex.Message,
                };

            }
            catch (Exception ex)
            {
                return new BaseResponseDTO
                {
                    Status = ApplicationConstants.StatusFailed,
                    Message = ex.Message,
                };
            }
        }

        public async Task<BaseResponseDTO> SendDailyMenuNotification(DateTime currentDate)
        {
            try
            {
                await _chefService.SendDailyMenuNotification(currentDate);
                return new BaseResponseDTO
                {
                    Status = ApplicationConstants.StatusSuccess,
                    Message = ApplicationConstants.SentNotificationSuccessfully
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

        public async Task<RecommendedMenuResponse> GetMenuListItems()
        {
            try
            {
                var recommendedMenuList = await _chefService.GetMenuListItems();
                return new RecommendedMenuResponse
                { 
                    RecommendedMenus = recommendedMenuList,
                    Status = ApplicationConstants.StatusSuccess, 
                };

            }
            catch (Exception ex) 
            {
                return new RecommendedMenuResponse
                {
                    Message = ex.Message,
                    Status = ApplicationConstants.StatusSuccess,
                };

            }
        }

        public async Task<BaseResponseDTO> DiscardMenuItem(int menuId)
        {
            try
            {
                await _chefService.DiscardMenuItem(menuId);
                return new BaseResponseDTO { Status = ApplicationConstants.StatusSuccess };
            }
            catch (Exception ex) 
            { 
              return new BaseResponseDTO() { Status = ApplicationConstants.StatusFailed, Message = ex.Message };
            }
        }
    }
}

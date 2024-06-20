using RecommendationEngineServer.Common;
using RecommendationEngineServer.Common.DTO;
using RecommendationEngineServer.Common.Exceptions;
using RecommendationEngineServer.Logic.Chef;

namespace RecommendationEngineServer.Controller
{
    public class ChefController
    {
        private IChefLogic _chefLogic;
        public ChefController(IChefLogic chefLogic)
        {
            _chefLogic = chefLogic;
        }

        public async Task<BaseResponseDTO> AddDailyMenuItem(List<int> menuIds)
        {
            try
            {
                await _chefLogic.AddDailyMenuItem(menuIds);
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

        public async Task<BaseResponseDTO> SendNotification()
        {
            try
            {
                await _chefLogic.SendNotification();
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
                var recommendedMenuList = await _chefLogic.GetMenuListItems();
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
    }
}

using RecommendationEngineServer.Common;
using RecommendationEngineServer.Common.DTO;
using RecommendationEngineServer.Common.Exceptions;
using RecommendationEngineServer.Service.Admin;

namespace RecommendationEngineServer.Controller
{
    public class AdminController
    {

        private IAdminService _adminLogic;

        public AdminController(IAdminService adminLogic)
        {
            _adminLogic = adminLogic;
        }

        public async Task<BaseResponseDTO> AddMenuItem(AddMenuItemRequest addMenuItemRequest)
        {
            try
            {
                var result = await _adminLogic.AddMenuItem(addMenuItemRequest);
                return new BaseResponseDTO
                {
                    Status = ApplicationConstants.StatusSuccess,
                    Message = ApplicationConstants.MenuItemAddedSuccessfully
                };
            }
            catch (MenuItemAlreadyPresentException ex)
            {
                return new BaseResponseDTO
                {
                    Status = ApplicationConstants.StatusFailed,
                    Message = ex.Message
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

        public async Task<MenuListResponse> GetMenuList()
        {
            try
            {
                var result = await _adminLogic.GetMenuList();
                if (result == null || result.Count < 1)
                {
                    throw new Exception(ApplicationConstants.MenuListIsEmpty);
                }
                List<MenuListModel> menuList = new List<MenuListModel>();
                menuList.AddRange(result);
                return new MenuListResponse
                {
                    Status = ApplicationConstants.StatusSuccess,
                    MenuList = menuList
                };
            }
            catch (Exception ex)
            {
                return new MenuListResponse
                {
                    Status = ApplicationConstants.StatusFailed,
                    Message = ex.Message,
                    MenuList = null
                };
            }
        }

        public async Task<BaseResponseDTO> RemoveMenuItem(int menuId)
        {
            try
            {
                await _adminLogic.RemoveMenuItem(menuId);

                return new BaseResponseDTO
                {
                    Status = ApplicationConstants.StatusSuccess,
                    Message = ApplicationConstants.MenuItemRemovedSuccessfully
                };
            }
            catch (MenuItemNotFoundException ex)
            {
                return new BaseResponseDTO
                {
                    Status = ApplicationConstants.StatusFailed,
                    Message = ex.Message
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

        public async Task<BaseResponseDTO> UpdateMenuItem(UpdateMenuItemRequest updateMenuItemRequest)
        {
            try
            {
                await _adminLogic.UpdateMenuItem(updateMenuItemRequest);

                return new BaseResponseDTO
                {
                    Status = ApplicationConstants.StatusSuccess,
                    Message = ApplicationConstants.MenuItemUpdatedSuccessfully
                };
            }
            catch (MenuItemNotFoundException ex)
            {
                return new BaseResponseDTO
                {
                    Status = ApplicationConstants.StatusFailed,
                    Message = ex.Message
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
    }
}

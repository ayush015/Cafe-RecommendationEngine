using RecommendationEngineServer.Common;
using RecommendationEngineServer.Common.Exceptions;
using RecommendationEngineServer.DAL.Models;
using RecommendationEngineServer.DAL.UnitOfWork;
using System.Text;

namespace RecommendationEngineServer.Logic.Chef
{
    public class ChefLogic : IChefLogic
    {
        private IUnitOfWork _unitOfWork;
        public ChefLogic(IUnitOfWork unitOfWork) 
        { 
          _unitOfWork = unitOfWork;
        }

        public async Task<int> AddDailyMenuItem(List<int> menuIds)
        {
            if (menuIds.Count <= 0)
                throw new MenuException(ApplicationConstants.MenuListIsEmpty);

            var allDailyMenu =  await _unitOfWork.DailyMenu.GetAll();

            List<DailyMenu> menuList = new List<DailyMenu>();


            foreach (var item in menuIds)
            {
               if(allDailyMenu == null)
               {
                    DailyMenu menu = new DailyMenu()
                    {
                        Date = DateTime.Now,
                        IsDeleted = false,
                        MenuId = item,
                        IsNotificationSent = false,
                    };
                    menuList.Add(menu);
               }
               else
               {
                    var previousDate = allDailyMenu.LastOrDefault().Date;
                    var newDate = previousDate.AddDays(1);
                    DailyMenu menu = new DailyMenu()
                    {
                        Date = newDate,
                        IsDeleted = false,
                        MenuId = item,
                        IsNotificationSent = false,
                    };
                    menuList.Add(menu);
               }
            }

            await _unitOfWork.DailyMenu.AddDailyMenuList(menuList);

            return await _unitOfWork.Complete();
        }

        public async Task SendNotification()
        {
            
            var allDailyMenu = (await _unitOfWork.DailyMenu.GetAll())
                              .Where(m => m.IsNotificationSent == false)
                              .OrderBy(m => m.Menu.MealTypeId)
                              .ToList();

            DateTime date = allDailyMenu.FirstOrDefault().Date;
            StringBuilder notificationMessage = new StringBuilder();
            foreach (var item in allDailyMenu) 
            { 
                var menuItem = await _unitOfWork.Menu.GetMenuItemById(item.MenuId);
                string message = $"FoodItem {menuItem.FoodItemName}\t{menuItem.MealTypeName}\n";
                notificationMessage.Append(message);
            }

            Notification addNotification = new Notification()
            {
                Message = notificationMessage.ToString(),
                CreatedDate = date,
            };

            await _unitOfWork.Notification.Create(addNotification);
        }

    }
}

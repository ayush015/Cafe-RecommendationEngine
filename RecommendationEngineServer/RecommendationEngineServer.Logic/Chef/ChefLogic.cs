using RecommendationEngineServer.Common;
using RecommendationEngineServer.Common.DTO;
using RecommendationEngineServer.Common.Exceptions;
using RecommendationEngineServer.DAL.Models;
using RecommendationEngineServer.DAL.Repository.Menu;
using RecommendationEngineServer.DAL.UnitOfWork;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace RecommendationEngineServer.Logic.Chef
{
    public class ChefLogic : IChefLogic
    {
        private IUnitOfWork _unitOfWork;
        public ChefLogic(IUnitOfWork unitOfWork) 
        { 
          _unitOfWork = unitOfWork;
        }

        #region Public Methods
        public async Task<int> AddDailyMenuItem(List<int> menuIds)
        {
            List<DailyMenu> menuList = new List<DailyMenu>();

            if (menuIds.Count <= 0)
                throw new MenuException(ApplicationConstants.MenuListIsEmpty);

            var allDailyMenu =  (await _unitOfWork.DailyMenu.GetAll()).ToList();

            foreach (var item in menuIds)
            {
               if(allDailyMenu == null || allDailyMenu.Count == 0)
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
            if(allDailyMenu.Count == 0)
            {
                throw new NoNewDailyMenuItemAddedException();   
            }
            DateTime date = allDailyMenu.FirstOrDefault().Date;
            StringBuilder notificationMessage = new StringBuilder();
            foreach (var item in allDailyMenu) 
            { 
                var menuItem = await _unitOfWork.Menu.GetMenuItemById(item.MenuId);
                item.IsNotificationSent = true;
                string message = $"{menuItem.DailyMenuId} {menuItem.FoodItemName} {menuItem.MealTypeName},";
                notificationMessage.AppendLine(message);
            }

            Notification addNotification = new Notification()
            {
                Message = notificationMessage.ToString(),
                CreatedDate = date,
            };

            await _unitOfWork.Notification.Create(addNotification);
            await _unitOfWork.Complete();
        }

        public async Task<List<RecommendedMenu>> GetMenuListItems()
        {
            var recommendedMenuList = await GetRecommendedMenuList();
            List<RecommendedMenu> recommendedMenus = new List<RecommendedMenu>();
            foreach (var recommendedMenu in recommendedMenuList)
            {
                var menuDetails = await _unitOfWork.Menu.GetMenuDetailByMenuId(recommendedMenu.MenuId);
                menuDetails.RecommendationScore = recommendedMenu.RecommendationScore;
                recommendedMenus.Add(menuDetails);
            }
            return recommendedMenus;
        }
        #endregion


        public async Task<List<MenuRecommendation>> GetRecommendedMenuList()
        {
            var feedbacks = (await _unitOfWork.Feedback.GetAll()).ToList();
            var allMenuItems = (await _unitOfWork.Menu.GetAll()).Where(m => !m.IsDeleted).ToList();
            var allOrders = (await _unitOfWork.UserOrder.GetAll()).OrderBy(o => o.DailyMenuId).ToList();

            var averageRating = feedbacks
                                .Where(f => !f.IsDeleted)
                                .GroupBy(f => f.MenuId)
                                .Select(g => new
                                {
                                    MenuId = g.Key,
                                    AverageRating = g.Average(f => f.Rating),
                                    Comments = g.Select(f => f.Comment).ToList()
                                });
            var menuOrderFrequency = await GetOrderFrequency(allOrders);

            var recommendations = (from menu in allMenuItems
                                   join rating in averageRating on menu.Id equals rating.MenuId into mr
                                   from rating in mr.DefaultIfEmpty()
                                   join orderFrequency in menuOrderFrequency on menu.Id equals orderFrequency.MenuId into of
                                   from orderFrequency in of.DefaultIfEmpty()
                                   select new MenuRecommendation
                                   {
                                       MenuId = menu.Id,
                                       AverageRating = rating?.AverageRating ?? 0,
                                       Comments = rating?.Comments ?? new List<string>(),
                                       OrderFrequency = orderFrequency?.OrderFrequency ?? 0,
                                       RecommendationScore = CalculateRecommendationScore(rating?.AverageRating ?? 0, rating?.Comments ?? new List<string>(), orderFrequency?.OrderFrequency ?? 0)
                                   }).OrderByDescending(r => r.RecommendationScore).ToList();
            return recommendations;
        }




        //private async Task<List<UserOrderFrequency>> GetOrderFrequency(List<UserOrder> userOrders)
        //{
        //    int frequency = 0;
        //    var allMenu = (await _unitOfWork.Menu.GetAll()).ToList();
        //    List<UserOrderFrequency> orderFrequencies= new List<UserOrderFrequency>();
        //    int dailyMenuId = userOrders[0].DailyMenuId;
        //    foreach (var item in userOrders)
        //    {
        //        if(dailyMenuId == item.DailyMenuId)
        //        {
        //            ++frequency;
        //        }
        //        else
        //        {
        //            var dailyMenu = await _unitOfWork.DailyMenu.GetById(dailyMenuId);
        //            UserOrderFrequency userOrder = new UserOrderFrequency()
        //            {
        //                DailyMenuId = dailyMenuId,
        //                OrderFrequency = frequency,
        //                MenuId = dailyMenu.MenuId
        //            };
        //            orderFrequencies.Add(userOrder);
        //            frequency = 0;
        //            dailyMenuId = item.DailyMenuId;
        //            ++frequency;
        //        }

        //    }

        //    foreach (var menu in allMenu)
        //    {
        //        if (!orderFrequencies.Any(x => x.MenuId == menu.Id))
        //        {
        //            UserOrderFrequency userOrder = new UserOrderFrequency()
        //            {
        //                MenuId = menu.Id,
        //                OrderFrequency = 0,
        //                DailyMenuId = 0
        //            };
        //            orderFrequencies.Add(userOrder);
        //        }
        //    }
        //    return orderFrequencies;

        //}

        private async Task<List<UserOrderFrequency>> GetOrderFrequency(List<UserOrder> userOrders)
        {
            int frequency = 0;
            int? currentDailyMenuId = null;
            List<UserOrderFrequency> orderFrequencies = new List<UserOrderFrequency>();
            userOrders = userOrders.OrderBy(uo => uo.DailyMenuId).ToList();

            foreach (var item in userOrders)
            {
                if (currentDailyMenuId == null || currentDailyMenuId == item.DailyMenuId)
                {
                    ++frequency;
                }
                else
                {
                    var dailyMenu = await _unitOfWork.DailyMenu.GetById(currentDailyMenuId.Value);
                    UserOrderFrequency userOrder = new UserOrderFrequency()
                    {
                        DailyMenuId = currentDailyMenuId.Value,
                        OrderFrequency = frequency,
                        MenuId = dailyMenu.MenuId
                    };
                    orderFrequencies.Add(userOrder);
                    frequency = 1;
                }

                currentDailyMenuId = item.DailyMenuId;
            }
            if (currentDailyMenuId != null)
            {
                var dailyMenu = await _unitOfWork.DailyMenu.GetById(currentDailyMenuId.Value);
                UserOrderFrequency userOrder = new UserOrderFrequency()
                {
                    DailyMenuId = currentDailyMenuId.Value,
                    OrderFrequency = frequency,
                    MenuId = dailyMenu.MenuId
                };
                orderFrequencies.Add(userOrder);
            }
            var allMenuIds = (await _unitOfWork.Menu.GetAll()).Where(m => !m.IsDeleted).Select(menu => menu.Id).ToList();
            var existingMenuIds = orderFrequencies.Select(of => of.MenuId).ToList();
            var missingMenuIds = allMenuIds.Except(existingMenuIds);

            foreach (var menuId in missingMenuIds)
            {
                orderFrequencies.Add(new UserOrderFrequency()
                {
                    MenuId = menuId,
                    OrderFrequency = 0,
                    DailyMenuId = 0
                });
            }

            return orderFrequencies;
        }


        private double CalculateRecommendationScore(double averageRating, List<string> comments, int orderFrequency)
        {
            double maxCommentScore = 10.0;
            double maxOrderFrequency = 50.0;

            double normalizedRating = (averageRating / 5.0) * 5.0;
            double normalizedCommentScore = (Math.Min(comments.Count, maxCommentScore) / maxCommentScore) * 5.0;
            double normalizedOrderFrequency = (Math.Min(orderFrequency, maxOrderFrequency) / maxOrderFrequency) * 5.0;


            double ratingWeight = 0.5;
            double commentWeight = 0.2;
            double orderFrequencyWeight = 0.3;

            double finalScore = (normalizedRating * ratingWeight) +
                        (normalizedCommentScore * commentWeight) +
                        (normalizedOrderFrequency * orderFrequencyWeight);
            return Math.Round(finalScore, 1);
        }

    }
}

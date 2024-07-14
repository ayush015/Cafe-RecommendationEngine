using RecommendationEngineServer.Common;
using RecommendationEngineServer.Common.DTO;
using RecommendationEngineServer.Common.Exceptions;
using RecommendationEngineServer.DAL.Models;
using RecommendationEngineServer.DAL.UnitOfWork;

namespace RecommendationEngineServer.Service.Chef
{
    public class ChefService : IChefService
    {
        private IUnitOfWork _unitOfWork;
        public ChefService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Public Methods
        public async Task<int> AddDailyMenuItem(MenuItem menuItem)
        {
            List<DailyMenu> menuList = new List<DailyMenu>();

            if (menuItem.MenuItemsIds.Count <= 0)
                throw new MenuException(ApplicationConstants.MenuListIsEmpty);

            var allDailyMenu = (await _unitOfWork.DailyMenu.GetAll()).ToList().LastOrDefault();

            if (allDailyMenu.Date == menuItem.CurrentDate)
            {
                return ApplicationConstants.DailyMenuDateAlreadyExist;
            }

            foreach (var item in menuItem.MenuItemsIds)
            {
                DailyMenu menu = new DailyMenu()
                {
                    Date = menuItem.CurrentDate,
                    IsDeleted = false,
                    MenuId = item,
                    IsNotificationSent = false,
                };
                menuList.Add(menu);
            }

            await _unitOfWork.DailyMenu.AddDailyMenuList(menuList);

            return await _unitOfWork.Complete();
        }

        public async Task SendDailyMenuNotification(DateTime currentDate)
        {

            Notification addNotification = new Notification()
            {
                Message = $"New Daily Menu has been Added for {currentDate}",
                CreatedDate = currentDate,
                NotificationTypeId = 2
            };

            await _unitOfWork.Notification.Create(addNotification);
            await _unitOfWork.Complete();
        }

        public async Task<List<RecommendedMenuModel>> GetMenuListItems()
        {
            var recommendedMenuList = await GetRecommendedMenuList();
            List<RecommendedMenuModel> recommendedMenus = new List<RecommendedMenuModel>();
            foreach (var recommendedMenu in recommendedMenuList)
            {
                var menuDetails = await _unitOfWork.Menu.GetMenuDetailByMenuId(recommendedMenu.MenuId);
                menuDetails.RecommendationScore = recommendedMenu.RecommendationScore;
                recommendedMenus.Add(menuDetails);
            }
            return recommendedMenus;
        }

        public async Task<List<MenuRecommendationModel>> GetRecommendedMenuList()
        {
            var feedbacks = (await _unitOfWork.Feedback.GetAll()).ToList();
            var allMenuItems = (await _unitOfWork.Menu.GetAll()).Where(m => !m.IsDeleted && !m.IsDiscarded).ToList();
            var allOrders = (await _unitOfWork.UserOrder.GetAll()).OrderBy(o => o.DailyMenuId).ToList();

            var averageRating = CalculateAverageRatings(feedbacks);
            var menuOrderFrequency = await GetOrderFrequency(allOrders);

            var recommendations = CreateMenuRecommendations(allMenuItems, averageRating, menuOrderFrequency);
            return recommendations;
        }

        public async Task DiscardMenuItem(int menuId)
        {
            var menuItem = await _unitOfWork.Menu.GetById(menuId);

            if (menuItem == null) throw new MenuItemNotFoundException();

            menuItem.IsDiscarded = true;

            await _unitOfWork.Complete();

        }
        #endregion

        #region Private Methods
        private List<AverageRatingModel> CalculateAverageRatings(List<Feedback> feedbacks)
        {
            return feedbacks
                .Where(f => !f.IsDeleted)
                .GroupBy(f => f.MenuId)
                .Select(g => new AverageRatingModel
                {
                    MenuId = g.Key,
                    AverageRating = g.Average(f => f.Rating),
                    Comments = g.Select(f => f.Comment).ToList()
                }).ToList();
        }

        private async Task<List<UserOrderFrequencyModel>> GetOrderFrequency(List<UserOrder> userOrders)
        {
            var orderFrequencies = CalculateOrderFrequencies(userOrders);
            var allMenuIds = await GetAllMenuIds();
            return MergeOrderFrequenciesWithAllMenus(orderFrequencies, allMenuIds);
        }

        private List<UserOrderFrequencyModel> CalculateOrderFrequencies(List<UserOrder> userOrders)
        {
            int frequency = 0;
            int? currentMenuId = null;
            List<UserOrderFrequencyModel> orderFrequencies = new List<UserOrderFrequencyModel>();
            userOrders = userOrders.OrderBy(uo => uo.DailyMenu.MenuId).ToList();

            foreach (var item in userOrders)
            {
                if (currentMenuId == null || currentMenuId == item.DailyMenu.MenuId)
                {
                    ++frequency;
                }
                else
                {
                    UserOrderFrequencyModel userOrder = new UserOrderFrequencyModel()
                    {
                        OrderFrequency = frequency,
                        MenuId = (int)currentMenuId
                    };
                    orderFrequencies.Add(userOrder);
                    frequency = 1;
                }
                currentMenuId = item.DailyMenu.MenuId;
            }

            if (currentMenuId != null)
            {
                orderFrequencies.Add(new UserOrderFrequencyModel()
                {
                    OrderFrequency = frequency,
                    MenuId = (int)currentMenuId
                });
            }

            return orderFrequencies;
        }

        private async Task<List<int>> GetAllMenuIds()
        {
            return (await _unitOfWork.Menu.GetAll())
                .Where(m => !m.IsDeleted && !m.IsDiscarded)
                .Select(menu => menu.Id)
                .ToList();
        }

        private List<UserOrderFrequencyModel> MergeOrderFrequenciesWithAllMenus(List<UserOrderFrequencyModel> orderFrequencies, List<int> allMenuIds)
        {
            var existingMenuIds = orderFrequencies.Select(of => of.MenuId).ToList();
            var missingMenuIds = allMenuIds.Except(existingMenuIds);

            foreach (var menuId in missingMenuIds)
            {
                orderFrequencies.Add(new UserOrderFrequencyModel()
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

            double maxOrderFrequency = 50.0;

            double normalizedRating = (averageRating / 5.0) * 5.0;
            double normalizedCommentScore = CalculateCommentSentimentScore(comments);
            double normalizedOrderFrequency = (Math.Min(orderFrequency, maxOrderFrequency) / maxOrderFrequency) * 5.0;

            double ratingWeight = 0.5;
            double commentWeight = 0.2;
            double orderFrequencyWeight = 0.3;

            double finalScore = (normalizedRating * ratingWeight) +
                        (normalizedCommentScore * commentWeight) +
                        (normalizedOrderFrequency * orderFrequencyWeight);
            return Math.Round(finalScore, 1);
        }

        private double CalculateCommentSentimentScore(List<string> comments)
        {
            double maxSentimentScore = 10;
            double totalSentimentScore = 0;

            if (comments.Count == 0) return 0;

            foreach (var comment in comments)
            {
                double sentimentScore = AnalyzeCommentSentiment(comment);
                totalSentimentScore += sentimentScore;
            }

            double averageSentimentScore = totalSentimentScore / comments.Count;
            double normalizedCommentScore = (Math.Min(averageSentimentScore, maxSentimentScore) / maxSentimentScore) * 5.0;

            return normalizedCommentScore;
        }

        private double AnalyzeCommentSentiment(string comment)
        {
            var positiveWords = GetPositiveWords();
            var negativeWords = GetNegativeWords();

            comment = comment.ToLower();
            var words = comment.Split(new[] { ' ', '.', ',' }, StringSplitOptions.RemoveEmptyEntries);

            int positiveScore = 0;
            int negativeScore = 0;
            bool negate = false;

            foreach (var word in words)
            {
                if (word == "not" || word == "no" || word == "never")
                {
                    negate = !negate;
                    continue;
                }

                if (positiveWords.Contains(word))
                {
                    positiveScore += negate ? -1 : 1;
                }
                else if (negativeWords.Contains(word))
                {
                    negativeScore += negate ? -1 : 1;
                }
                negate = false;
            }

            if (positiveScore > negativeScore)
            {
                return 1.0;
            }
            else if (negativeScore > positiveScore)
            {
                return -1.0;
            }
            else
            {
                return 0.5;
            }
        }

        private List<string> GetPositiveWords()
        {
            var positiveWords = new List<string>
            {
            "delicious", "tasty", "yummy", "flavorful", "satisfying", "exquisite",
            "mouth-watering", "savory", "delectable", "succulent", "fresh", "crispy",
            "juicy", "perfect", "amazing", "wonderful", "excellent", "great",
            "fantastic", "superb", "awesome", "outstanding", "appetizing", "heavenly",
            "good", "very good","enjoyable", "yum"
            };

            return positiveWords;
        }

        private List<string> GetNegativeWords()
        {
            var negativeWords = new List<string>
            {
            "bland", "tasteless", "flavorless", "bad", "very bad","terrible", "awful", "disgusting",
            "stale", "cold", "overcooked", "undercooked", "burnt", "soggy", "greasy",
            "unappetizing", "horrible", "nasty", "inedible", "gross", "displeasing",
            "unpalatable", "poor", "unsatisfactory", "unpleasant", "mediocre"
            };

            return negativeWords;
        }

        private List<MenuRecommendationModel> CreateMenuRecommendations(List<DAL.Models.Menu> allMenuItems, List<AverageRatingModel> averageRatings, List<UserOrderFrequencyModel> menuOrderFrequency)
        {
            return (from menu in allMenuItems
                    join rating in averageRatings on menu.Id equals rating.MenuId into mr
                    from rating in mr.DefaultIfEmpty()
                    join orderFrequency in menuOrderFrequency on menu.Id equals orderFrequency.MenuId into of
                    from orderFrequency in of.DefaultIfEmpty()
                    select new MenuRecommendationModel
                    {
                        MenuId = menu.Id,
                        AverageRating = rating?.AverageRating ?? 0,
                        Comments = rating?.Comments ?? new List<string>(),
                        OrderFrequency = orderFrequency?.OrderFrequency ?? 0,
                        RecommendationScore = CalculateRecommendationScore(rating?.AverageRating ?? 0, rating?.Comments ?? new List<string>(), orderFrequency?.OrderFrequency ?? 0)
                    }).OrderByDescending(r => r.RecommendationScore).ToList();
        }
        #endregion
    }
}

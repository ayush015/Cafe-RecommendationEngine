using RecommendationEngineClient.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineClient._10_Common.DTO
{
    public class NotificationResponse : BaseResponseDTO
    {
        public string NotificationMessgae { get; set; }
        public bool IsNewNotification { get; set; }
    }

    public class OrderRequest
    {
        public List<int> DailyMenuIds { get; set; }
        public int UserId { get; set; }
    }

    public class OrderResponse : BaseResponseDTO
    {
        public int OrderId { get; set; }
    }

    public class UserOrderMenu : MenuListViewModel
    {
        public int DailyMenuId { get; set; }
    }

    public class UserOrderMenuListResponse
    {
        public List<UserOrderMenu> UserOrderMenus { get; set; }
    }

    public class GiveFeedBackRequest
    {
        public int MenuIds { get; set; }
        public int DailyMenuId { get; set; }
        public int UserId { get; set; }
        public int Rating { get; set; }
        public string Comments { get; set; }
    }
}

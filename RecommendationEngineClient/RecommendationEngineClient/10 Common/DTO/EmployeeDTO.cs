using RecommendationEngineClient.Common.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineClient._10_Common.DTO
{
    public class NotificationRequest
    { 
      public int UserId { get; set; }
      public DateTime CurrentDate { get; set; }
    
    }


    public class NotificationResponse : BaseResponseDTO
    {
        public string NotificationMessgae { get; set; }
        public bool IsNewNotification { get; set; }
        public int NotificationTypeId { get; set; }
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

    public class UserOrderMenuListResponse : BaseResponseDTO
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

   
    public class FeedbackQuestionResponse : BaseResponseDTO
    {
        public List<FeedbackQuestion> FeedbackQuestions { get; set; }
    }

    public class FeedbackQuestion
    {
        public int QuestionId { get; set; }
        public string Question { get; set; }
    }

    public class MenuImprovementFeedbackRequest
    {
        public string FoodItemName { get; set; }
        public int UserId { get; set; }
        public List<ImprovementFeedback> ImprovementFeedbacks { get; set; }
    }

    public class ImprovementFeedback
    {
        public int QuestionId { get; set; }
        public string Answer { get; set; }

    }
}

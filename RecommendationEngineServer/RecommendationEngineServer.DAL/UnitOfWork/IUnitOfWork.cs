﻿using RecommendationEngineServer.DAL.Repository.DailyMenu;
using RecommendationEngineServer.DAL.Repository.Feedback;
using RecommendationEngineServer.DAL.Repository.FoodItem;
using RecommendationEngineServer.DAL.Repository.ImprovementQuestion;
using RecommendationEngineServer.DAL.Repository.ImprovementRecord;
using RecommendationEngineServer.DAL.Repository.MealType;
using RecommendationEngineServer.DAL.Repository.Menu;
using RecommendationEngineServer.DAL.Repository.Notification;
using RecommendationEngineServer.DAL.Repository.Order;
using RecommendationEngineServer.DAL.Repository.User;
using RecommendationEngineServer.DAL.Repository.UserFoodPreference;
using RecommendationEngineServer.DAL.Repository.UserNotification;
using RecommendationEngineServer.DAL.Repository.UserOrder;
using RecommendationEngineServer.DAL.Repository.UserRole;

namespace RecommendationEngineServer.DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        IUser User { get; }
        IUserRole UserRole { get; }
        IUserOrder UserOrder { get; }
        IOrder Order { get; }
        IMenu Menu { get; }
        IMealType MealType { get; }
        IFoodItem FoodItem { get; }
        IFeedback Feedback { get; }
        IDailyMenu DailyMenu { get; }
        INotification Notification { get; }
        IUserNotification UserNotification { get; }
        IMenuFeedbackQuestion MenuFeedbackQuestion { get; }
        IUserMenuFeedbackAnswer UserMenuFeedbackAnswer { get; }
        IUserFoodPreference UserFoodPreference { get; }
        Task<int> Complete();
    }
}

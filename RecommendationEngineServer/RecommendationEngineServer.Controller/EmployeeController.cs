﻿using RecommendationEngineServer.Common;
using RecommendationEngineServer.Common.DTO;
using RecommendationEngineServer.Service.Chef;
using RecommendationEngineServer.Service.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineServer.Controller
{
    public class EmployeeController
    {
        private IEmployeeService _employeeLogic;
        public EmployeeController(IEmployeeService employeeLogic)
        {
            _employeeLogic = employeeLogic;
        }

        public async Task<NotificationResponse> GetNotification(NotificationRequest notificationRequest)
        {
            try
            {
                var notification = await _employeeLogic.GetNotification(notificationRequest);
                if(string.IsNullOrEmpty(notification))
                {
                    return new NotificationResponse
                    {
                        Status = ApplicationConstants.StatusSuccess,
                        IsNewNotification = false,
                    };
                }

                return new NotificationResponse
                {
                    Status = ApplicationConstants.StatusSuccess,
                    NotificationMessgae = notification,
                    Message = ApplicationConstants.SentNotificationSuccessfully,
                    IsNewNotification = true,
                };
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
                var orderId = await _employeeLogic.SelectFoodItemsFromDailyMenu(orderRequest);
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
                await _employeeLogic.GiveFeedBack(giveFeedBackRequest);
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
               var orderedMenuList =  await _employeeLogic.GetMenuItemsByOrderId(orderId);
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

    }
}


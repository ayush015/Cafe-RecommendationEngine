using Newtonsoft.Json;
using RecommendationEngineClient.Common;
using RecommendationEngineClient.Common.DTO;
using RecommendationEngineClient.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecommendationEngineClient.Admin
{
    public class AdminHandler
    {
        private RequestServices _requestServices;
        public AdminHandler(RequestServices requestServices) 
        { 
          _requestServices = requestServices;
        }

        public async Task AdminConsole()
        {
            while(true)
            {
                Console.WriteLine("Enter Choice");
                Console.WriteLine("1. Add Menu\n2. Update Menu\n3. Menu List\n4. Remove Item\n5. Logout");
                int choice = Convert.ToInt32(Console.ReadLine());
                AdminChoice adminChoice = (AdminChoice)choice;
                switch (adminChoice) 
                {
                    case AdminChoice.AddMenu :
                        {
                            break;
                        }
                    case AdminChoice.UpdateMenu:
                        {
                            break;
                        }
                    case AdminChoice.MenuList:
                        {
                            await GetMenuList();
                            break;
                        }
                    case AdminChoice.RemoveItem:
                        {

                            break;
                        }
                }
            }
        }

        public async Task GetMenuList()
        {
            DataObject requestData = new DataObject()
            {
                Controller = "Admin",
                Action = "GetMenuList",
                Data = null
            };

            var jsonRequest = JsonConvert.SerializeObject(requestData);
            var response = await _requestServices.SendRequestAsync(jsonRequest);
            var menuList =  JsonConvert.DeserializeObject<MenuListResponse>(response);

            if (menuList.Status.Equals(ApplicationConstants.StatusFailed))
            {
                Console.WriteLine($"{menuList.Message}");
                return;
            }

            if(menuList.MenuList != null && menuList.MenuList.Count == 0)
            {
                Console.WriteLine($"Menu is Empty.");
            }

            foreach (var item in menuList.MenuList)
            {
                Console.WriteLine($"{item.MenuId}\t {item.FoodItemName}\t {item.MealTypeName}");
            }

            Console.ReadKey();
        }
    }
}

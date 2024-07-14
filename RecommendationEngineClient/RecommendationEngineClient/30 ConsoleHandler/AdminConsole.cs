using RecommendationEngineClient._20_ClientService.Admin;
using RecommendationEngineClient.Common;
using RecommendationEngineClient.Common.Enum;

namespace RecommendationEngineClient.Admin
{
    public class AdminConsole
    {
        private IAdminService _adminService;

        public AdminConsole(RequestService requestServices)
        {
            _adminService = new AdminService(requestServices);
        }

        public async Task AdminConsoleHandler()
        {
            while (true)
            {
                Console.WriteLine("Enter Choice");
                Console.WriteLine("1. Add Menu\n2. Update Menu\n3. Menu List\n4. Remove Item\n5. Logout\n");
                Console.Write("Enter : ");
                string userInput = Console.ReadLine();
                Console.WriteLine();
                if (!int.TryParse(userInput, out int choice) || choice < 0)
                {
                    Console.WriteLine("\nInvalit Input\n");
                }
                else
                {
                    if (choice == ApplicationConstants.Logout)
                    {
                        Console.WriteLine($"{ApplicationConstants.LogoutSuccessfull}\n");
                        break;
                    }

                    AdminChoice adminChoice = (AdminChoice)choice;
                    switch (adminChoice)
                    {
                        case AdminChoice.AddMenu:
                            {
                                await _adminService.AddMenuItem();
                                break;
                            }
                        case AdminChoice.UpdateMenu:
                            {
                                await _adminService.UpdateMenuItem();
                                break;
                            }
                        case AdminChoice.MenuList:
                            {
                                await _adminService.GetMenuList();
                                break;
                            }
                        case AdminChoice.RemoveItem:
                            {
                                await _adminService.RemoveMenuItem();
                                break;
                            }
                        default:
                            {
                                Console.WriteLine(ApplicationConstants.InvalidChoice);
                                break;
                            }
                    }
                }
            }
        }
    }
}

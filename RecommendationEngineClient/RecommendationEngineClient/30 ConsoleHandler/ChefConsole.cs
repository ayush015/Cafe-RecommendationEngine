using RecommendationEngineClient._20_ClientOperations;
using RecommendationEngineClient._20_ClientOperations.Chef;
using RecommendationEngineClient.Common.Enum;
using RecommendationEngineClient.Common;
using RecommendationEngineClient._10_Common.Enum;

namespace RecommendationEngineClient._30_ConsoleHandler
{
    public class ChefConsole
    {
        IChefClientOperations _chefService;
        public ChefConsole(RequestServices requestServices)
        {
            _chefService = new ChefClientOperations(requestServices);
        }

        public async Task ChefConsoleHandler()
        {
            while (true)
            {
                Console.WriteLine("Enter Choice");
                Console.WriteLine("1. Get All Menu List\n2. Roll out menu\n3. Send Notification\n5. Logout\n");
                Console.Write("Enter : ");
                string userInput = Console.ReadLine();
                Console.WriteLine();
                if (!int.TryParse(userInput, out int choice) || choice < 0)
                {
                    Console.WriteLine("Invalit Input\n");
                }
                else
                {
                    if (choice == ApplicationConstants.Logout)
                    {
                        Console.WriteLine($"{ApplicationConstants.LogoutSuccessfull}\n");
                        break;
                    }

                    ChefChoice chefChoice = (ChefChoice)choice;
                    switch (chefChoice)
                    {
                        case ChefChoice.MenuList:
                            {
                                await _chefService.GetMenuList();
                                break;
                            }
                        case ChefChoice.AddDailyMenuItem:
                            {
                                await _chefService.AddDailyMenuItem();
                                break;
                            }
                        case ChefChoice.SendNotification:
                            {
                                await _chefService.SendNotification();
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

using RecommendationEngineClient._10_Common.DTO;
using RecommendationEngineClient._10_Common.Enum;
using RecommendationEngineClient._20_ClientService.Chef;
using RecommendationEngineClient.Common;

namespace RecommendationEngineClient._30_ConsoleHandler
{
    public class ChefConsole
    {
        IChefService _chefService;
        public ChefConsole(RequestService requestServices)
        {
            _chefService = new ChefService(requestServices);
        }

        public async Task ChefConsoleHandler()
        {
            await SetDateFromChef();
            await _chefService.GetMonthlyNotification();

            while (true)
            {
                Console.WriteLine("Enter Choice");
                Console.WriteLine("1. Get All Menu List\n2. Roll out menu\n5. Logout\n");
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
                                await Task.Delay(3000);
                                await _chefService.SendDailyMenuNotification();
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

        private static async Task SetDateFromChef()
        {
            Console.Write("Please enter the date (yyyy-MM-dd): ");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
            {
                DateDTO newdate = new DateDTO { CurrentDate = date };
                await DateStore.SaveDataAsync(newdate);
                Console.WriteLine("Date saved successfully");
            }
            else
            {
                Console.WriteLine("Invalid date format.");
            }

            var currentDate = await DateStore.LoadDataAsync();

            Console.WriteLine(currentDate.CurrentDate);

        }
    }
}

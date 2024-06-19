using RecommendationEngineClient._30_Services.Admin;
using RecommendationEngineClient.Common.Enum;
using RecommendationEngineClient.Common;
using RecommendationEngineClient._10_Common.Enum;
using RecommendationEngineClient._30_Services.Employee;

namespace RecommendationEngineClient._20_ConsoleHandler
{
    public class EmployeeConsole
    {
        private IEmployeeService _employeeService;

        public EmployeeConsole(RequestServices requestServices)
        {
            _employeeService = new EmployeeService(requestServices);
        }

        public async Task EmployeeConsoleHandler(int userId)
        {
            while (true)
            {
                await _employeeService.GetNotification(userId);
                Console.WriteLine("Enter Choice");
                Console.WriteLine("1. Select item from Daily Menu\n2. Give FeedBack\n5. Logout\n");
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

                    EmployeeChoice employeeChoice = (EmployeeChoice)choice;
                    switch (employeeChoice)
                    {
                        case EmployeeChoice.SelectItems:
                            {
                                await _employeeService.SelectFoodItemsFromDailyMenu(userId);
                                break;
                            }
                        case EmployeeChoice.GiveFeedBack:
                            {
                                await _employeeService.GiveFeedBack(userId);
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

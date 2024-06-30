using RecommendationEngineClient._20_ClientOperations.Admin;
using RecommendationEngineClient.Common.Enum;
using RecommendationEngineClient.Common;
using RecommendationEngineClient._10_Common.Enum;
using RecommendationEngineClient._20_ClientOperations.Employee;

namespace RecommendationEngineClient._30_ConsoleHandler
{
    public class EmployeeConsole
    {
        private IEmployeeClientOperations _employeeService;

        public EmployeeConsole(RequestServices requestServices)
        {
            _employeeService = new EmployeeClientOperations(requestServices);
        }

        public async Task EmployeeConsoleHandler(int userId)
        {
            while (true)
            {
               var notification =  await _employeeService.GetNotification(userId);
               
                Console.WriteLine("Enter Choice");
                Console.WriteLine("1. Select item from Daily Menu\n5. Logout\n");
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
                                if (notification != 1)
                                {
                                    await Console.Out.WriteLineAsync("Option not available at the moment\nWait for new notification\n");
                                    break;
                                }
                                await _employeeService.SelectFoodItemsFromDailyMenu(userId);
                                await Task.Delay(5000);
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

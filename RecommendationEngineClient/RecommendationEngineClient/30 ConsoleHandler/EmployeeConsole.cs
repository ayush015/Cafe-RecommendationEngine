using RecommendationEngineClient._10_Common.Enum;
using RecommendationEngineClient._20_ClientOperations.Employee;
using RecommendationEngineClient.Common;

namespace RecommendationEngineClient._30_ConsoleHandler
{
    public class EmployeeConsole
    {
        private IEmployeeClientOperations _employeeClientOperation;

        public EmployeeConsole(RequestServices requestServices)
        {
            _employeeClientOperation = new EmployeeClientOperations(requestServices);
        }

        public async Task EmployeeConsoleHandler(int userId)
        {
            while (true)
            {
                var notification = await _employeeClientOperation.GetNotification(userId);

                Console.WriteLine("Enter Choice");
                Console.WriteLine("1. Select item from Daily Menu\n2. Update Your Profile\n5. Logout\n");
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
                                await _employeeClientOperation.SelectFoodItemsFromDailyMenu(userId);
                                await Task.Delay(5000);
                                await _employeeClientOperation.GiveFeedBack(userId);
                                break;
                            }
                        case EmployeeChoice.UpdateProfile:
                            {
                                await _employeeClientOperation.AddUserFoodPreference(userId);
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

using System;
using System.Net.Http;
using System.Threading.Tasks;
using CarSimulator.RandomUser;
using Newtonsoft.Json;

namespace CarSimulator
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            User user = await GetRandomUserAsync();
            string driverName = $"{user.Name.Title} {user.Name.First} {user.Name.Last}";
            RunCarSimulator(driverName);
        }

        public static async Task<User> GetRandomUserAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetStringAsync("https://randomuser.me/api/");
                var userData = JsonConvert.DeserializeObject<RandomUserResults>(response);
                return userData.Results[0];
            }
        }

        static void RunCarSimulator(string driverName)
        {
            Car car = new Car();
            bool running = true;

            while (running)
            {
                Console.Clear();
                PrintHeader("Car Simulator");
                PrintHeader($"Driver: {driverName}");
                car.PrintStatus();
                PrintHeader("Available commands:");
                Console.WriteLine("\nAvailable commands:");
                Console.WriteLine("1. Turn left");
                Console.WriteLine("2. Turn right");
                Console.WriteLine("3. Move forward");
                Console.WriteLine("4. Move backward");
                Console.WriteLine("5. Rest");
                Console.WriteLine("6. Refuel the car");
                Console.WriteLine("7. Exit");
                Console.Write("\nWhat do you want to do next? ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        car.TurnLeft();
                        break;
                    case "2":
                        car.TurnRight();
                        break;
                    case "3":
                        car.MoveForward();
                        break;
                    case "4":
                        car.MoveBackward();
                        break;
                    case "5":
                        car.Rest();
                        break;
                    case "6":
                        car.Refuel();
                        break;
                    case "7":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Oops! That's not a valid command, try again.");
                        Thread.Sleep(2000);
                        break;
                }
                Thread.Sleep(2000);
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        static void PrintHeader(string header)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n{header}");
            Console.WriteLine(new string('-', header.Length));
            Console.ResetColor();
        }
    }
}
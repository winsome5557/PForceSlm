using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;


namespace ParcelForce.Test.client
{
    class Program
    {
        private static readonly HttpClient Client = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:44200/api/")
        };


        public static void InitializeSignalRClient()
        {
            var hubConnection = new HubConnection("http://localhost:44200/api/");
            IHubProxy currencyExchangeHubProxy = hubConnection.CreateHubProxy("SlmmHub");

            // This line is necessary to subscribe for broadcasting messages
            currencyExchangeHubProxy.On<string>("StateChanged", HandleNotify);

            // Start the connection
            hubConnection.Start().Wait();
        }

        private static void HandleNotify(string state)
        {
            Console.WriteLine(state);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to SLMM Application Client");

            // REgister with the notification hub
            //InitializeSignalRClient();
            GetLogAndPrint();


            Console.WriteLine("Please provide  width of the lawn ");
            string sWidth = Console.ReadLine();
            int Width = Int32.Parse(sWidth);


            Console.WriteLine("Please provide  height of the lawn ");
            string sHeight = Console.ReadLine();
            int Height = Int32.Parse(sWidth);


            LawnSize size = new LawnSize() {SizeX = Width, SizeY = Height,StartX= 0, StartY = 0};
            InitializeLawnAsync(size);

            // Initialize the client   client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var exit = false;
            ShowHelp();
            while (!exit)
            {
                string input = Console.ReadLine()?.ToLower();
                switch (input)
                {
                    case "tl":
                        SaveExecution(() => TurnMachine(-1));
                        break;
                    case "tr":
                        SaveExecution(() => TurnMachine(1));
                        break;
                    case "mf":
                        SaveExecution(async () =>
                        {
                            Console.WriteLine("Enter steps to move:");
                            int steps;
                            if (!int.TryParse(Console.ReadLine(), out steps))
                                Console.WriteLine("Invalid input. Number is expected.");
                            else
                                await Client.PutAsJsonAsync("location", steps).ContinueWith(PrintResult);
                        });
                        break;
                    case "ml":
                        SaveExecution(() => Client.PutAsJsonAsync("mower", true).ContinueWith(PrintResult));
                        break;
                    case "exit":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Not supported command.");
                        break;
                }
            }
        }

        static CancellationTokenSource wtoken = new CancellationTokenSource();
        static Task task;
        static string LastMsg= string.Empty;
        private static void GetLogAndPrint()
        {
            task = Task.Run(async () =>  // <- marked async
            {
                while (true)
                {
                    GetCommandLogAndrPint();
                    await Task.Delay(1000, wtoken.Token); // <- await with cancellation
                }
            }, wtoken.Token);
        }

        private  static void GetCommandLogAndrPint()
        {
            try
            {
                HttpResponseMessage response =  Client.GetAsync("Lawn\\1").Result;
                var content =  response.Content.ReadAsStringAsync();
                if (!LastMsg.ToLower().Equals(content.Result.ToLower()))
                {
                    if (!content.Result.Equals(string.Empty))
                    {
                        Console.WriteLine(content.Result);
                    }
                    LastMsg = content.Result;
                }
                // Return the URI of the created resource.
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occur while initializing lawn : {ex.Message}");
            }
            return ;
        }

        static async Task<Uri> InitializeLawnAsync(LawnSize lawnsize)
        {
            try
            {
                HttpResponseMessage response = await Client.PostAsJsonAsync("Lawn", lawnsize);

                response.EnsureSuccessStatusCode();
                // Return the URI of the created resource.
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occur while initializing lawn : {ex.Message}");
            }
            return null;

        }

        private static async void SaveExecution(Func<Task> func)
        {
            try
            {
                await func();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occur while command execution: {ex.Message}");
            }
        }

        private static async Task TurnMachine(int direction)
        {
            HttpResponseMessage response = await Client.GetAsync("Rotation");
            if (response.IsSuccessStatusCode)
            {
                var rotation = await response.Content.ReadAsAsync<Rotation>();
                var newRotation = (rotation + direction < 0) ? (Rotation)(4 + (int)(rotation + direction) % 4) : (Rotation)((int)(rotation + direction) % 4);
                await Client.PutAsJsonAsync("Rotation", newRotation).ContinueWith(PrintResult);
            }
            else
            {
                Console.WriteLine("Error occur during command execution");
            }
        }

        private static void PrintResult(Task<HttpResponseMessage> obj)
        {
            var result = obj.Result;
            Console.WriteLine(result.IsSuccessStatusCode
                ? "Command Sent Successfully to SLMM machine."
                : $"Unable To Send Command to SLLM Machine. StatusCode: {result.StatusCode}");
        }

        private static void ShowHelp()
        {
            Console.WriteLine("Please Enter your command to control SLMM Machine. The comands are sent asyncrhonously and results are printed on console when received from SLLM.\r\n Following commands expected:\r\n" +
                              "TL -> Turn Left\r\n" +
                              "TR -> Turn Right\r\n" +
                              "MF -> Move Forward\r\n" +
                              "ML -> Mow Lawn\r\n" +
                              "Exit -> end application\r\n");
        }
    }
}

using System;
using System.Net.Http;


namespace ConsoleApp.Test
{
    class Program
    {
        static  void Main(string[] args)
        {


            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44325/");
            var response = client.GetAsync("api/ToDoList/1").GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                string model = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                Console.WriteLine(model);
            }

            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}

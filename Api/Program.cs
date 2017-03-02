using System;
using Microsoft.Owin.Hosting;

namespace ParcelForce.Test.WebApi
{
    class Program
    {
        static void Main(string[] args)
        {
            using (WebApp.Start<Startup>("http://localhost:44200"))
            {
                Console.ReadLine();
            }
        }
    }
}

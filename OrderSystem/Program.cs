using Microsoft.EntityFrameworkCore;
using OrderSystem.Data;
using System;

namespace OrderSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new ApplicationContext();

            db.Database.Migrate();
            Console.WriteLine("Hello World!");
        }
    }
}

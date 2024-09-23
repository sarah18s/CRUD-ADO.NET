using Microsoft.Extensions.Configuration;
using System;


class Program
{
    public static void Main()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();


        Console.WriteLine(configuration.GetSection("ConnectionString").Value);


    }
}

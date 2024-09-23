using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;

class Program
{
    public static void Main()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        SqlConnection conn = new(configuration.GetSection("ConnectionString").Value);

        var sql = "SELECT * FROM WALLETS";

        SqlCommand command = new(sql, conn)
        {
            CommandType = CommandType.Text
        };

        conn.Open();

        SqlDataReader reader = command.ExecuteReader();

        Wallet wallet;

        while (reader.Read())
        {
            wallet = new()
            {
                Id = reader.GetInt32("Id"),
                Holder = reader.GetString("Holder"),
                Balance = reader.GetDecimal("Balance"),
            };

            Console.WriteLine(wallet);
        }

        conn.Close();

    }
}
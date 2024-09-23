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
       
        Wallet walletToInsert = new()
        {
            Holder = "Sarah",
            Balance = 4500
        };

        SqlConnection conn = new(configuration.GetSection("ConnectionString").Value);

        var sql = @"INSERT INTO WALLETS (Holder, Balance) VALUES 
              (@Holder, @Balance);
             SELECT CAST(scope_identity() AS int)";

        SqlParameter holderParameter = new()
        {
            ParameterName = "@Holder",
            SqlDbType = SqlDbType.VarChar,
            Direction = ParameterDirection.Input,
            Value = walletToInsert.Holder,
        };

        SqlParameter balanceParameter = new()
        {
            ParameterName = "@Balance",
            SqlDbType = SqlDbType.Decimal,
            Direction = ParameterDirection.Input,
            Value = walletToInsert.Balance,
        };


        SqlCommand command = new(sql, conn);

        command.Parameters.Add(holderParameter);
        command.Parameters.Add(balanceParameter);

        command.CommandType = CommandType.Text;

        conn.Open();

        walletToInsert.Id = (int)command.ExecuteScalar();

        Console.WriteLine($"wallet {walletToInsert} added successully");

        conn.Close();
    }
}
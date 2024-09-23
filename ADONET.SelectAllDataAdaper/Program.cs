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

        conn.Open();

        SqlDataAdapter adapter = new(sql, conn);

        DataTable dataTable = new();

        adapter.Fill(dataTable);

        conn.Close();

        foreach (DataRow dr in dataTable.Rows)
        {

            var wallet = new Wallet
            {
                Id = Convert.ToInt32(dr["Id"]),
                Holder = Convert.ToString(dr["Holder"]),
                Balance = Convert.ToDecimal(dr["Balance"]),
            };

            Console.WriteLine(wallet);
        }



    }
}
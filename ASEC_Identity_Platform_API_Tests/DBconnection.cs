using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

public class DBConnection
{
    public static string? EmailAddress { get; set; }
    private readonly string _connectionDB;
    private SqlConnection? _con;

    public DBConnection()
    {
        var config = new ConfigurationBuilder().SetBasePath(Directory
            .GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        _connectionDB = config["ConnectionStrings:PDOConnectionDB"]
        ?? throw new InvalidOperationException("PDOConnectionDB is not configured.");

    }

    public void OpenConnection()
    {
        if (string.IsNullOrEmpty(_connectionDB))
            throw new Exception("Connection string is null or empty!");

        _con = new SqlConnection(_connectionDB);
        _con.Open();
    }

    public void CloseConnection()
    {
        _con?.Close();
    }

    public void DBUpdateTokenUserAllowRenewalDate()
    {
        if (string.IsNullOrEmpty(EmailAddress))
            throw new InvalidOperationException("EmailAddress must be set before updating the database.");

        using var connection = new SqlConnection(_connectionDB);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = @"
            UPDATE dbo.Person 
            SET AllowRenewalDate = DATEADD(month, 1, GETDATE()),
                IsEnrolmentUpdated = 0 
            WHERE EmailAddress = @emailAddress";
        command.Parameters.AddWithValue("@emailAddress", EmailAddress);
        command.ExecuteNonQuery();
    }

    // Async version (recommended for .NET 8)
    public async Task DBUpdateTokenUserAllowRenewalDateAsync()
    {
        if (string.IsNullOrEmpty(EmailAddress))
            throw new InvalidOperationException("EmailAddress must be set before updating the database.");

        await using var connection = new SqlConnection(_connectionDB);
        await connection.OpenAsync();
        await using var command = connection.CreateCommand();
        command.CommandText = @"
            UPDATE dbo.Person 
            SET AllowRenewalDate = DATEADD(month, 1, GETDATE()),
                IsEnrolmentUpdated = 0 
            WHERE EmailAddress = @emailAddress";
        command.Parameters.AddWithValue("@emailAddress", EmailAddress);
        await command.ExecuteNonQueryAsync();
    }

    public void DB_DeleteClientId(string name)
    {
        using var connection = new SqlConnection(_connectionDB);
        connection.Open();
        using var transaction = connection.BeginTransaction();

        try
        {
            //Delete from [BusinessUnits] child table 
            using var BunisessUnitCommand = connection.CreateCommand();
            BunisessUnitCommand.Transaction = transaction;
            BunisessUnitCommand.CommandText = @"
            DELETE bu FROM [EnrolmentDatabaseQA].[dbo].[BusinessUnits] bu
            INNER JOIN [EnrolmentDatabaseQA].[dbo].[Client] c
                ON bu.ClientId = c.ClientId
            WHERE c.name = @name";
            BunisessUnitCommand.Parameters.AddWithValue("@name", name);
            BunisessUnitCommand.ExecuteNonQuery();

            //Delete from the parent table [Client]
            using var ClientNameCommand = connection.CreateCommand();
            ClientNameCommand.Transaction = transaction;
            ClientNameCommand.CommandText = @"
            DELETE FROM [EnrolmentDatabaseQA].[dbo].[Client]
            WHERE name = @name";
            ClientNameCommand.Parameters.AddWithValue("@name", name);
            ClientNameCommand.ExecuteNonQuery();

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public void DB_DeleteClientProductSetting_ClientProductId_ClientId(string clientName)
    {
        using var connection = new SqlConnection(_connectionDB);
        connection.Open();
        using var transaction = connection.BeginTransaction();
        try
        {
            //Delete from [ClientProductSetting] table
            //ps stands for Product settings,
            //cp stands for ClientProduct
            //c stands for Client
            using var ClientProductSettingCommand = connection.CreateCommand();
            ClientProductSettingCommand.Transaction = transaction;
            ClientProductSettingCommand.CommandText = @"
            DELETE ps 
            FROM [EnrolmentDatabaseQA].[dbo].[ClientProductSetting] ps
            INNER JOIN [EnrolmentDatabaseQA].[dbo].[ClientProduct] cp
                ON ps.FK_ClientProduct_ClientProductId = cp.ClientProductId
            INNER JOIN [EnrolmentDatabaseQA].[dbo].[Client] c
                ON cp.FK_Client_ClientId = c.ClientId
            WHERE c.Name = @clientName";
            ClientProductSettingCommand.Parameters.AddWithValue("@clientName", clientName);
            ClientProductSettingCommand.ExecuteNonQuery();

            //Delete from [ClientProduct] table
            using var ClientProductCommand = connection.CreateCommand();
            ClientProductCommand.Transaction = transaction;
            ClientProductCommand.CommandText = @"
            DELETE cp 
            FROM [EnrolmentDatabaseQA].[dbo].[ClientProduct] cp
            INNER JOIN [EnrolmentDatabaseQA].[dbo].[Client] c
                ON cp.FK_Client_ClientId = c.ClientId
            WHERE c.Name = @clientName";
            ClientProductCommand.Parameters.AddWithValue("@clientName", clientName);
            ClientProductCommand.ExecuteNonQuery();

            //Delete from the parent table [Client]
            using var ClientNameCommand = connection.CreateCommand();
            ClientNameCommand.Transaction = transaction;
            ClientNameCommand.CommandText = @"
            DELETE FROM [EnrolmentDatabaseQA].[dbo].[Client]
            WHERE Name = @clientName";
            ClientNameCommand.Parameters.AddWithValue("@clientName", clientName);
            ClientNameCommand.ExecuteNonQuery();

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public void DB_DeleteOfficer(string userName)
    {
        using var connection = new SqlConnection(_connectionDB);
        connection.Open();
        using var transaction = connection.BeginTransaction();

        try
        {
            //Delete from Officer table
            using var UserNameCommand = connection.CreateCommand();
            UserNameCommand.Transaction = transaction;
            UserNameCommand.CommandText = @"
            DELETE FROM [EnrolmentDatabaseQA].[dbo].[Officer]
            WHERE userName = @userName";
            UserNameCommand.Parameters.AddWithValue("@userName", userName);
            UserNameCommand.ExecuteNonQuery();

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }
}


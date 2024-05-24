using Dapper;
using DbUp.Engine;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;
using System.Text.Json.Serialization;

namespace Migrations.Scripts;

public class S202306131106_SeedBookCategoryTable : IScript
{
    static Random _rand = new Random((int)DateTime.Now.Ticks);

    public string ProvideScript(Func<IDbCommand> commandFactory)
    {

        using var command = (commandFactory() as SqlCommand)!;

        string[] bookCategories = new string[]
        {
            "Fiction",
            "Non-Fiction",
            "Mystery",
            "Science Fiction",
            "Fantasy",
            "Romance",
            "Thriller",
            "Historical Fiction",
            "Biography",
            "Self-Help",
            "Young Adult",
            "Children's",
            "Horror",
            "Poetry",
            "Graphic Novels"
        };

        for (int i = 0; i < bookCategories.Length; i++)
        {
            InsertEntry(command, bookCategories[i]);
        }

        return "";
    }

    public static void InsertEntry(SqlCommand command, string categoryName)
    {
        const string query = @"
        INSERT INTO [dbo].[BookCategory]([Name],[LastModified])
        VALUES (@Name,@LastModified)
        ";

        var parameters = new
        {
            Name = categoryName,
            LastModified = DateTime.UtcNow.AddHours(GetRandomInt(-100000, -1))
        };

        command.Connection.Execute(query, parameters, command.Transaction);
    }
     
    static int GetRandomInt(int minInclusive, int maxInclusive)
    {
        return _rand.Next(minInclusive, maxInclusive + 1);
    }
}

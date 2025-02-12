﻿using System.Data;
using System.Data.SqlClient;

namespace MMSDotNetCore.ConsoleApp.AdoDotNetExamples;

public class AdoDotNetExample
{
    //public readonly SqlConnectionStringBuilder _stringBuilder = new SqlConnectionStringBuilder()
    //{
    //    DataSource = ".",
    //    InitialCatalog = "DotNetTrainingBatch4",
    //    UserID = "sa",
    //    Password = "sa@123",
    //    TrustServerCertificate = true
    //};

    private readonly SqlConnectionStringBuilder _stringBuilder;

    public AdoDotNetExample(SqlConnectionStringBuilder stringBuilder)
    {
        _stringBuilder = stringBuilder;
    }

    public void Read()
    {
        SqlConnection connection = new SqlConnection(_stringBuilder.ConnectionString);
        connection.Open();

        string query = "select * from Tbl_Blog";
        SqlCommand cmd = new SqlCommand(query, connection);
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        sqlDataAdapter.Fill(dt);

        connection.Close();

        //dataset => datatable
        //datatable => datarow
        //datarow => datacolumn

        foreach (DataRow dr in dt.Rows)
        {
            Console.WriteLine("BlogId => " + dr["BlogId"]);
            Console.WriteLine("BlogTitle => " + dr["BlogTitle"]);
            Console.WriteLine("BlogAuthor => " + dr["BlogAuthor"]);
            Console.WriteLine("BlogContent => " + dr["BlogContent"]);
            Console.WriteLine("---------------------------------------");
        }
    }

    public void Edit(int id)
    {
        SqlConnection connection = new SqlConnection(_stringBuilder.ConnectionString);
        connection.Open();

        string query = "select * from Tbl_Blog where BlogId=@BlogId";
        SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@BlogId", id);
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        sqlDataAdapter.Fill(dt);

        connection.Close();

        if (dt.Rows.Count == 0)
        {
            Console.WriteLine("No Data Found");
            return;
        }

        DataRow dr = dt.Rows[0];
        Console.WriteLine("BlogId => " + dr["BlogId"]);
        Console.WriteLine("BlogTitle => " + dr["BlogTitle"]);
        Console.WriteLine("BlogAuthor => " + dr["BlogAuthor"]);
        Console.WriteLine("BlogContent => " + dr["BlogContent"]);
        Console.WriteLine("---------------------------------------");
    }

    public void Create(string title, string author, string content)
    {
        SqlConnection connection = new SqlConnection(_stringBuilder.ConnectionString);
        connection.Open();

        string query = @"INSERT INTO [dbo].[Tbl_Blog]
                            ([BlogTitle],
                            [BlogAuthor],
                            [BlogContent])
                            VALUES
                (@BlogTitle,
                @BlogAuthor,
                @BlogContent)";
        SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@BlogTitle", title);
        cmd.Parameters.AddWithValue("@BlogAuthor", author);
        cmd.Parameters.AddWithValue("@BlogContent", content);
        int result = cmd.ExecuteNonQuery();
        connection.Close();

        string message = result > 0 ? "New Blog Creation Successful" : "New Blog Creation Fail";
        Console.WriteLine(message);
    }

    public void Update(int id, string title, string author, string content)
    {
        SqlConnection connection = new SqlConnection(_stringBuilder.ConnectionString);
        connection.Open();

        string query = @"UPDATE [dbo].[Tbl_Blog]
                            SET [BlogTitle] = @BlogTitle
                            ,[BlogAuthor] = @BlogAuthor
                            ,[BlogContent] = @BlogContent
                            WHERE BlogId=@BlogId";
        SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@BlogId", id);
        cmd.Parameters.AddWithValue("@BlogTitle", title);
        cmd.Parameters.AddWithValue("@BlogAuthor", author);
        cmd.Parameters.AddWithValue("@BlogContent", content);
        int result = cmd.ExecuteNonQuery();
        connection.Close();

        string message = result > 0 ? "Blog update successful" : "Blog update fail";
        Console.WriteLine(message);
    }

    public void Delete(int id)
    {
        SqlConnection connection = new SqlConnection(_stringBuilder.ConnectionString);
        connection.Open();

        string query = @"DELETE FROM [dbo].[Tbl_Blog]
      WHERE BlogId=@BlogId";

        SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@BlogId", id);
        int result = cmd.ExecuteNonQuery();
        connection.Close();

        string message = result > 0 ? "Blog delete successful" : "Blog delete fail";
        Console.WriteLine(message);
    }
}

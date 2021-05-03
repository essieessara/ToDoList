using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Database;

namespace ToDoList.Repositories.ToDoItemRepos
{
    public class ToDoItemDapperRepo : IToDoItemRepo
    {
        private readonly IConfiguration _configuration;

        public ToDoItemDapperRepo(IConfiguration configuration)
        => _configuration = configuration;

        public async Task<ToDoItemtEntity> CreateToDoItemAsync(ToDoItemtEntity toDodb)
        {
            string sql = "Insert into Lists (ItemID,ItemName,IsFinished,CreatedDate,UserID,EndedDate) VALUES (@ItemID,@ItemName,@IsFinished,@CreatedDate,@UserID,@EndedDate)";
            var sql1 = "SELECT * FROM Lists WHERE ItemID = @Id";
            var sql2 = "SELECT * FROM Lists WHERE UserID = @Uid";
            //string sql = @"
            //Insert into Lists (ItemID,ItemName,IsFinished,CreatedDate,UserID,EndedDate) VALUES (@ItemID,@ItemName,@IsFinished,@CreatedDate,@UserID,@EndedDate);
            //SELECT /*CAST( SCOPE_IDENTITY() as int*/ ItemID from Lists/*)*/";

            using IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            await connection.ExecuteAsync(sql, toDodb);
            var result = await connection.QueryFirstOrDefaultAsync<int>(sql1, new { Id = toDodb.ItemID/*, Uid = toDodb.UserID*/ });
            var result1 = await connection.QueryFirstOrDefaultAsync<int>(sql2, new { Uid = toDodb.UserID /*, Id = toDodb.ItemID*/ });
            var output = await GetToDoByIdAsync(result,result1);
            return output;

        }

        public async Task DeleteToDoByIdAsync(int id)
        {
            var sql = "DELETE FROM Lists WHERE ItemID = @Id";
            using IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            await connection.ExecuteAsync(sql, new { Id = id });

        }

        public async Task<ToDoItemtEntity> EditToDoByIdAsync(ToDoItemtEntity toDodb)
        {
            string sql = @"
            UPDATE Lists SET ItemName = @ItemName, IsFinished = @IsFinished, CreatedDate = @CreatedDate, UserID = @UserID, EndedDate = @EndedDate where ItemID = @ItemID;
            SELECT CAST(SCOPE_IDENTITY() as int)";
            //var sql = "UPDATE Lists SET ItemName = @ItemName, IsFinished = @IsFinished, CreatedDate = @CreatedDate, UserID = @UserID, EndedDate = @EndedDate where ItemID = @ItemID";
            using IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            await connection.ExecuteAsync(sql, toDodb);
            var output = await GetToDoByIdAsync(toDodb.ItemID , toDodb.UserID);
            return output;
        }

        public async Task<List<ToDoItemtEntity>> GetAllToDoListASync()
        {
            var sql = "SELECT * FROM Lists";
            using IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            var result = await connection.QueryAsync<ToDoItemtEntity>(sql);
            return result.ToList();

        }

        public async Task<ToDoItemtEntity> GetToDoByIdAsync(int id , int uid)
        {
            var sql = "SELECT * FROM Lists WHERE ItemID = @Id and UserID = @Uid";
            using IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            var result = await connection.QuerySingleOrDefaultAsync<ToDoItemtEntity>(sql, new { Id = id ,Uid = uid });
            return result;

        }

        public async Task<ToDoItemtEntity> GetToDoByNameAsync(string name)
        {
            var sql = "SELECT * FROM Lists WHERE ItemName = @name";
            using IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            var result = await connection.QuerySingleOrDefaultAsync<ToDoItemtEntity>(sql, new { name });
            return result;

        }

        public async Task<List<ToDoItemtEntity>> GetToDoUserByIdAsync(int id)
        {
            var sql = "SELECT * FROM Lists WHERE UserID = @Id";
            using IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            var result = await connection.QueryAsync<ToDoItemtEntity>(sql, new { Id = id });
            return result.ToList();

        }
    }
}

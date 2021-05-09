using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Database;

namespace ToDoList.Repositories.UserRepos
{
    public class UserDapperRepo : IUserRepo
    {
        private readonly IConfiguration _configuration;
        public UserDapperRepo(IConfiguration configuration)
        => _configuration = configuration;

        public async Task<UserEntity> CreateToDoUserAsync(UserEntity toDoUser)
        {
            //   var sql = "Insert into Users (FirstName,LastName,Username,Password) VALUES (@FirstName,@LastName,@Username,@Password)";
            string sql = @"
            Insert into Users (FirstName,LastName,Username,Password) VALUES (@FirstName,@LastName,@Username,@Password);
            SELECT CAST(SCOPE_IDENTITY() as int)";

            using IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            var result = await connection.QueryFirstAsync<int>(sql, toDoUser);
            var output = await GetUserByIdAsync(result);
            return output;
        }

        public async Task DeleteToDoUserByIdAsync(int id)
        {
            var sql = "DELETE FROM Users WHERE UserID = @Id";
            using IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            await connection.ExecuteAsync(sql, new { Id = id });
        }

        public async Task<UserEntity> EditToDoUserByIdAsync(UserEntity toDoUser)
        {
            string sql = @"UPDATE Users SET FirstName = @FirstName, LastName = @LastName, Username = @Username, Password = @Password where UserID = @UserID ;
            SELECT CAST(SCOPE_IDENTITY() as int)";

            //var sql = "UPDATE Users SET FirstName = @FirstName, LastName = @LastName, Username = @Username, Password = @Password where UserID = @UserID";
            using IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            await connection.ExecuteAsync(sql, toDoUser);
            var output = await GetUserByIdAsync(toDoUser.UserID);
            return output;
        }

        public async Task<List<UserEntity>> GetAllToDoUsersListASync()
        {
            var sql = "SELECT * FROM Users";
            using IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            var result = await connection.QueryAsync<UserEntity>(sql);
            return result.ToList();
        }

        public async Task<UserEntity> GetToDoUserByUsernameAsync(string name)
        {
            var sql = "SELECT * FROM Users WHERE Username = @name";
            using IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            var result = await connection.QuerySingleOrDefaultAsync<UserEntity>(sql, new { name });
            return result;
        }

        public async Task<UserEntity> GetUserByIdAsync(int id)
        {
            var sql = "SELECT * FROM Users WHERE UserID = @Id";
            using IDbConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            var result = await connection.QuerySingleOrDefaultAsync<UserEntity>(sql, new { Id = id });
            return result;
        }
    }
}

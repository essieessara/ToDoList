using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Database;
using ToDoList.Models.ResponseModels;
using ToDoList.Models.UserModels;

namespace ToDoList.Mapper
{
    public class UserMapper
    {
        public UserDataResponseModel Map(UserEntity model)
        {
            return new UserDataResponseModel
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Username = model.Username,
                UserID = model.UserID
            };
        }

        public UserEntity Map(RegisterUserModel model)
        {
            return new UserEntity
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Username = model.Username,
                Password = model.Password
            };
        }

        public UserEntity Map(LoginUserModel model)
        {
            return new UserEntity
            {
                Username = model.Username,
                Password = model.Password
            };
        }
    }
}

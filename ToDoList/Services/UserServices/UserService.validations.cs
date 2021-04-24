using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Database;
using ToDoList.Exceptions.UserExceptions;
using ToDoList.Models.ResponseModels;
using ToDoList.Models.UserModels;

namespace ToDoList.Services.UserServices
{
    public partial class UserService
    {
        private void ValidateGetUserById(UserEntity model)
        {
            if (model is null) { throw new UserNotFoundException(); }
        }

        private void ValidateDeleteUserById(UserEntity model)
        {
            if (model is null) { throw new UserNotFoundException(); }
        }
        private void ValidateRegister(RegisterUser model, UserEntity Entity)
        {
            if (model is null) { throw new UserValueIsNullException(); }
            if (Entity != null) { throw new UserAlreadyExistsException(); }
            if (model.Password != model.ConfirmPassword) { throw new PasswordDoesNotMatchException(); }
        }

         private void ValidateUpdate(UserEntity model)
         {
            if(model is null) { throw new CanNotUpdateUserException(); }
         }
        private void ValidateUpdatePass(UserEntity Entity , UpdateUserModel model)
        {
            if (Entity.Password is null && Entity.Password != model.Password && model.NewPassword != model.ConfirmNewPassword)
            { throw new CanNotUpdateUserException(); }
        }
    }
}

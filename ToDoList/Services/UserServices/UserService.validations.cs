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
        private void ValidateRegister(RegisterUserModel model, UserEntity Entity)
        {
            if (model is null) { throw new UserValueIsNullException(); }
            if (Entity != null) { throw new UserAlreadyExistsException(); }
            if (model.Password != model.ConfirmPassword) { throw new PasswordDoesNotMatchException(); }
        }
        private void ValidateLogin(LoginUserModel model, UserEntity Entity)
        {
            if (model is null) { throw new UserValueIsNullException(); }
            if(model.Password is null) { throw new PasswordIsNullException(); }
            if (model.Password != Entity.Password) { throw new PasswordIsNullException(); }
        }

        private void ValidateUpdate(UserEntity model)
         {
            if (model is null) { throw new CanNotUpdateUserException(); }
         }
        private void ValidateUpdateUsername(UserEntity model)
        {
            if (model != null) { throw new UserAlreadyExistsException(); }
        }

        private void ValidateUpdatePass(UserEntity Entity , UpdateUserModel model)
        {
            if (Entity.Password is null ) { throw new CanNotUpdateUserException(); }
            if (Entity.Password != model.Password) { throw new CanNotUpdateUserException(); }
            if (model.NewPassword != model.ConfirmNewPassword) { throw new CanNotUpdateUserException(); }
        }
        private void ValidateUpdatePass(UserEntity Entity, ResetPasswordModel model)
        {
            if (Entity is null) { throw new CanNotUpdateUserException(); }
            if (Entity.Password != model.Password) { throw new CanNotUpdateUserException(); }
            if (model.NewPassword != model.ConfirmNewPassword) { throw new CanNotUpdateUserException(); }
        }
    }
}

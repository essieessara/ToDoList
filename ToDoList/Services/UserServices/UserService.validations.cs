using System.Security.Claims;
using Todolist.Shared.Models.UserModels;
using ToDoList.Database;
using ToDoList.Exceptions.UserExceptions;


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
            if (model is null) { throw new UserValueIsIncorrectOrNullException(); }
            if (Entity != null) { throw new UserAlreadyExistsException(); }
            if (model.Password != model.ConfirmPassword) { throw new PasswordDoesNotMatchException(); }
        }
        private void ValidateLogin(LoginUserModel model, UserEntity Entity)
        {
            if (Entity is null) { throw new UserValueIsIncorrectOrNullException(); }
            if (model is null) { throw new UserValueIsIncorrectOrNullException(); }
            if (model.Password is null) { throw new PasswordIsIncorrectOrNullException(); }
            if (model.Password != Entity.Password) { throw new PasswordIsIncorrectOrNullException(); }
        }

        private void ValidateUpdate(UserEntity model)
        {
            if (model is null) { throw new CanNotUpdateUserException(); }
        }
        private void ValidateUpdateUsername(UserEntity model)
        {
            if (model != null) { throw new UserAlreadyExistsException(); }
        }

        private void ValidateUpdatePass(UserEntity Entity, UpdateUserModel model)
        {
            if (Entity.Password is null) { throw new CanNotUpdateUserException(); }
            if (Entity.Password != model.Password) { throw new CanNotUpdateUserException(); }
            if (model.NewPassword != model.ConfirmNewPassword) { throw new CanNotUpdateUserException(); }
        }
        private void ValidateUpdatePass(UserEntity Entity, ResetPasswordModel model)
        {
            if (Entity is null) { throw new CanNotUpdateUserException(); }
            if (Entity.Password != model.Password) { throw new CanNotUpdateUserException(); }
            if (model.NewPassword != model.ConfirmNewPassword) { throw new CanNotUpdateUserException(); }
        }
        private void Validateauthentication()
        {
            if (!_loggedUser.Identity.IsAuthenticated) { throw new UserNotLoggedInException(); }
        }
        private void ValidateLogin(Claim claim)
        {
            if (claim is null) { throw new UserNotLoggedInException(); }
        }
    }
}

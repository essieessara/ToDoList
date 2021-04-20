using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Database;
using ToDoList.Exceptions.UserExceptions;
using ToDoList.Models.ResponseModels;
using ToDoList.Models.UserModels;
using ToDoList.Repositories.UserRepos;
using ToDoList.Services.ToDoServices;

namespace ToDoList.Services.UserServices
{
    public partial class UserService : IUserService
    {
        private readonly IUserRepo _repo;
        private readonly IToDoItemService _itemService;

        public UserService(IUserRepo User, IToDoItemService itemService)
        {
            _repo = User;
            _itemService = itemService;
        }


        public Task<List<UserEntity>> GetUserListAsync()
             => TryCatch(async () =>
             {
                 var todoUserList = await _repo.GetAllToDoUsersListASync();
                 return todoUserList;
             });
        public Task<UserDataResponseModel> GetUserByIdAsync(int id)
            => TryCatch(async () =>
            {
                var todoListUser = await _repo.GetToDoUserByIdAsync(id);
                if (todoListUser != null)
                {
                    var output = new UserDataResponseModel()
                    { FirstName = todoListUser.FirstName, LastName = todoListUser.LastName };
                    todoListUser.Lists = (ICollection<ToDoItemtEntity>)await _itemService.GetUserByIdAsync(id);

                    output.ToDoLists = todoListUser.Lists.Select(x => new ToDoItemResponseModel
                    {
                        CreatedDate = x.CreatedDate,
                        EndedDate = x.EndedDate,
                        IsFinished = x.IsFinished,
                        ItemID = x.ItemID,
                        ItemName = x.ItemName
                    }).ToList();
                    return output;
                }
                else
                {
                    throw new UserNotFoundException();
                }
            });
        public Task DeleteUserAccountAsync(int id)
            => TryCatch(async () =>
            {
                var objectTovalidate = await _repo.GetToDoUserByIdAsync(id);
                if (objectTovalidate != null)
                {
                    await _repo.DeleteToDoUserByIdAsync(id);
                }
                else
                {
                    throw new UserNotFoundException();
                }
            });

        public Task<UserEntity> RegisterUserAsync(RegisterUser toDoUser)
             => TryCatch(async () =>
             {
                 var dbExistingModel = await GetByUsernameAsync(toDoUser.Username);
                 UserEntity dbCreateUser = new UserEntity()
                 {
                     FirstName = toDoUser.FirstName,
                     LastName = toDoUser.LastName,
                     Username = toDoUser.Username,
                     Password = toDoUser.Password

                 };
                 if (toDoUser != null)
                 {
                     if (dbExistingModel == null)
                     {
                         var todoNewUser = await _repo.CreateToDoUserAsync(dbCreateUser);
                         return todoNewUser;
                     }
                     throw new UserAlreadyExistsException();

                 }
                 throw new UserValueIsNullException();

             });

        public Task UpdateToDoUserAsync(UpdateUserModel User)
             => TryCatch(async () =>
             {
                 UserEntity dbUpdateModel = await _repo.GetToDoUserByIdAsync(User.UserID);


                 if (dbUpdateModel != null)
                 {
                     dbUpdateModel.UserID = User.UserID;
                     dbUpdateModel.FirstName = User.FirstName;
                     dbUpdateModel.LastName = User.LastName;
                     dbUpdateModel.Username = User.Username;
                     dbUpdateModel.Password = User.Password;
                     await _repo.EditToDoUserByIdAsync(dbUpdateModel);
                 }
                 else
                     throw new UserNotFoundException();

             });

        private async Task<UserEntity> GetByUsernameAsync(string name)
           => await TryCatch(async () =>
           {
               var todoListItem = await _repo.GetToDoUserByUsernameAsync(name);
               return todoListItem;
           });

    }
}

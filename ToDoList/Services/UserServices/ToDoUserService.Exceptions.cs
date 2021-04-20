﻿using System;
using System.Threading.Tasks;

namespace ToDoList.Services.UserServices
{
    public partial class UserService
    {
        private delegate Task Function();
        private delegate Task<T> Function<T>();

        private async Task TryCatch(Function model)
        {
            try
            {
                await model();
            }
            catch (Exception)
            {
                throw;
            }
        }
        private async Task<T> TryCatch<T>(Function<T> model)
        {
            try
            {
                return await model();
            }
            catch (Exception)
            {
                throw;

            }
        }
    }
}

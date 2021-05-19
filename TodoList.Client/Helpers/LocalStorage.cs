using Blazored.LocalStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.Client.Helpers
{
    public class LocalStorage : ILocalStorage
    {
        private ILocalStorageService _localStorageService;

        public LocalStorage(ILocalStorageService localStorageService)
            => _localStorageService = localStorageService;

        public ValueTask AddLocalStorageAsync<T>(string key, T Data)
        => _localStorageService.SetItemAsync(key, Data);

        public ValueTask<T> CallLocalStorage<T>(string Key)
            => _localStorageService.GetItemAsync<T>(Key);
    }
}

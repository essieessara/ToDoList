using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Todolist.Shared.Models.UserModels;
using ToDoList.Exceptions;

namespace ToDoList.Helpers
{
    public class ToDoControllerBase : ControllerBase
    {
        protected delegate Task<ActionResult> Func();
        protected delegate Task<ActionResult<ResponseModel>> Func<T>();



        protected async Task<ActionResult> TryCatch(Func model)
        {
            try
            {
                return await model();
            }
            catch (ToDoExceptions e)
            {
                return NotFound(new ResponseError(e.Errors));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        protected async Task<ActionResult<ResponseModel>> TryCatch<T>(Func<ResponseModel> model)
        {
            try
            {
                return await model();
            }
            catch (ToDoExceptions e)
            {
                return NotFound(new ErrorResponseModel(e.Errors));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

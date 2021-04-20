using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ToDoList.Exceptions;

namespace ToDoList.Controllers
{
    public class ToDoControllerBase : ControllerBase
    {
        protected delegate Task<ActionResult> Func();
        protected delegate Task<ActionResult<T>> Func<T>();



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
        protected async Task<ActionResult<T>> TryCatch<T>(Func<T> model)
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
    }
}

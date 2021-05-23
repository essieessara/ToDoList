using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todolist.Shared.Models.UserModels
{


    public class ResponseModel
    {
        public bool IsSuceess { get; init; }
        public string Message { get; set; }
    }
    public class SucessResponseModel<T> : ResponseModel
    {
        public SucessResponseModel()
        {
            IsSuceess = true;
        }
        public T Data { get; set; }
    }
    public class ErrorResponseModel : ResponseModel
    {
        private List<string> errors;

        public ErrorResponseModel()
        {
            IsSuceess = false;
        }

        public ErrorResponseModel(string message): this()
        {
            Message = message;
        }
        public ErrorResponseModel(List<string> errors) :this()
        {
            this.errors = errors;
        }

        public string[] Errors { get; set; }

    }
    public class SucessResponseModel : ResponseModel
    {
        public SucessResponseModel()
        {
            IsSuceess = false;
        }
       
    }


}

using ET_Common.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ET_Controller.Filters;

public class ApiResponseFilter : IResultFilter
{
    public void OnResultExecuting(ResultExecutingContext context)
    {
        if(context.Result is OkObjectResult ok)
        {
            context.Result = new OkObjectResult(
                new ApiResponse<Object>
                {
                    Success = true,
                    Data = ok.Value
                }
            );
        }
    }
    public void OnResultExecuted(ResultExecutedContext context){}
}
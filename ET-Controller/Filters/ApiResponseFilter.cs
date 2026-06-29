using ET_Common.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ET_Controller.Filters;

public class ApiResponseFilter : IResultFilter
{
    public void OnResultExecuting(ResultExecutingContext context)
{
    if (context.Result is not ObjectResult objectResult)
        return;

    // Only wrap successful responses
    if (objectResult.StatusCode is < 200 or >= 300)
        return;

    // Prevent double wrapping
    if (objectResult.Value is ApiResponse<object>)
        return;

    context.Result = new ObjectResult(new ApiResponse<object>
    {
        Success = true,
        Data = objectResult.Value
    })
    {
        StatusCode = objectResult.StatusCode
    };
}
    public void OnResultExecuted(ResultExecutedContext context){}
}
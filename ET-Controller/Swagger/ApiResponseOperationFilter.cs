using ET_Common.Responses;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ET_Controller.Swagger;

// Todo: Implement the Apply method to add custom responses to the Swagger documentation
public class ApiResponseOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        // AddResponse(operation, context, "400", "Bad Request");

        // AddResponse(operation, context, "401", "Unauthorized");

        // AddResponse(operation, context, "403", "Forbidden");

        AddResponse(operation, context, "404", "Not Found");

        // AddResponse(operation, context, "409", "Conflict");

        // AddResponse(operation, context, "500", "Internal Server Error");
    }

    private static void AddResponse( OpenApiOperation operation,
        OperationFilterContext context,
        string statusCode,
        string description)
    {
        operation.Responses.TryAdd(statusCode, new OpenApiResponse
        {
            Description = description,
            Content =
            {
                ["application/json"]= new OpenApiMediaType
                {
                    Schema = context.SchemaGenerator.GenerateSchema(
                        typeof(ApiResponse<object>),
                        context.SchemaRepository
                    )
                }
            }
        });
    }
}
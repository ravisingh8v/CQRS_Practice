using ET_Commands.Expense;
using ET_Infrastructure;
using ET_Infrastructure.Repositories;
using ET_Queries.Expense;
using ET_AppServices;
using Microsoft.EntityFrameworkCore;
using ET_Common.Middleware;
using ET_Controller.Filters;
using Microsoft.AspNetCore.Mvc;
using ET_Common.Responses;
using ET_Controller.Swagger;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Controllers
builder.Services.AddControllers(opt=>
{
    opt.Filters.Add<ApiResponseFilter>();
});

// Add MediatR - Register query and command handlers
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(GetAllExpensesQuery).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(CreateExpenseCommand).Assembly);
});

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

// Add Repositories
builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();

// Add service layer
builder.Services.AddScoped<IExpenseService, ExpenseService>();

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt=> 
opt.OperationFilter<ApiResponseOperationFilter>()
);

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors  = context.ModelState.Values.SelectMany(v=> v.Errors).Select(e=> e.ErrorMessage).ToList();
        var message = errors.FirstOrDefault() ?? "Validation failed."; 

        if(message.Contains("JSON", StringComparison.OrdinalIgnoreCase) || message.Contains("could not be converted", StringComparison.OrdinalIgnoreCase))
        {
            message = "Invalid JSON format.";
        }
        else
        {
            message = "Validation failed.";
        }

        return new BadRequestObjectResult(new ApiResponse<object>
        {
            Success = false,
            Data = null,
            Error = new ApiError
            {
                Message = message,
                StatusCode = StatusCodes.Status400BadRequest,
                Details = errors
            }
        });
    };
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();


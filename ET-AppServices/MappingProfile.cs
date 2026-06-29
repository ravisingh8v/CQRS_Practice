using AutoMapper;
using ET_DTO;
using ET_Infrastructure.Models;

namespace ET_AppServices;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ExpenseModel, ExpenseDto>().ReverseMap();
        CreateMap<CreateExpenseDto, ExpenseModel>();
        CreateMap<UpdateExpenseDto, ExpenseModel>();
    }
}

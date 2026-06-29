namespace ET_Common.Responses;
public class BadRequestException(string message) : Exception(message)
{
}
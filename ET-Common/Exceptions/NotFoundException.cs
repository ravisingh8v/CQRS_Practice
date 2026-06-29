namespace ET_Common.Responses;
public class NotFoundException(string message) : Exception(message)
{
}
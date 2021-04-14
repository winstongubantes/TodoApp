namespace TodoApp.Services
{
    public interface IAuthService
    {
        (bool IsAuthenticated, string Message) IsAuthenticated(string userName, string password);
    }
}
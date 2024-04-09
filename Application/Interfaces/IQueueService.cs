namespace Application.Interfaces
{
    public interface IQueueService
    {
        Task ReadMessages(Func<string, Task> action);
        Task SendMessage(string message);
    }
}

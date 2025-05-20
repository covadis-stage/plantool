namespace plantool.Presentation.SignalR;

public interface IRealtimeClient
{
    Task ReceiveActivityUpdate(string activityKey, string key, string? value);
}
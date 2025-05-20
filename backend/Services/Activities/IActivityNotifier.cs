namespace plantool.Services.Activities;

public interface IActivityNotifier
{
    Task NotifyActivityUpdate(string activityKey, string key, string? value);
}

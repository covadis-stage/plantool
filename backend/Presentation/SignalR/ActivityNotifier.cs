using Microsoft.AspNetCore.SignalR;
using plantool.Services.Activities;

namespace plantool.Presentation.SignalR;

public class ActivityNotifier : IActivityNotifier
{
    private readonly IHubContext<RealtimeHub, IRealtimeClient> _hubContext;

    public ActivityNotifier(IHubContext<RealtimeHub, IRealtimeClient> hubContext) =>
        _hubContext = hubContext;

    public async Task NotifyActivityUpdate(string activityKey, string key, string? value) =>
        await _hubContext.Clients.All.ReceiveActivityUpdate(activityKey, key, value);
}
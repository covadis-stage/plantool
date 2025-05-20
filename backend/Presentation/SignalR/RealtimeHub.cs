using Microsoft.AspNetCore.SignalR;

namespace plantool.Presentation.SignalR;

public class RealtimeHub : Hub<IRealtimeClient>
{
}

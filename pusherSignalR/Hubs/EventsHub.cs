namespace pusherSignalR.Hubs;

using Microsoft.AspNetCore.SignalR;

//todo handle auth
public class EventsHub : Hub
{
    public async Task SubscribeToChannel(string channelName)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, channelName);
    }

    public async Task UnsubscribeFromChannel(string channelName)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, channelName);
    }

    public async Task Trigger(string channelName, string eventName, object eventObject)
    {
        await Clients.Group(channelName).SendAsync(eventName, eventObject);
    }
}
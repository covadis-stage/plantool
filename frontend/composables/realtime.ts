import * as signalR from '@microsoft/signalr';

export const useRealtime = () => {
    const hubUrl = useRuntimeConfig().public.apiUrl + '/realtime';

    const connection = new signalR.HubConnectionBuilder()
        .withUrl(hubUrl, { withCredentials: false })
        .withAutomaticReconnect()
        .configureLogging(signalR.LogLevel.Information)
        .build();

    const startConnection = async () => {
        try {
            await connection.start();
            console.log('SignalR connection started');
        } catch (err) {
            console.error('Error while starting SignalR connection: ', err);
            setTimeout(() => startConnection(), 5000); // Retry connection after 5 seconds
        }
    }

    const onActivityUpdate = (callback: (activityKey: string, key: string, value: string) => void) => {
        connection.on("ReceiveActivityUpdate", (activityKey, key, value) => {
            callback(activityKey, key, value);
        });
    };

    return {
        startConnection,
        onActivityUpdate,
    }
}
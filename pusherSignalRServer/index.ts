import signalR from "@microsoft/signalr";
import {HOST_NAME} from "./config";


// todo maintain one connection per key
class PusherServer {
    connection: signalR.HubConnection;
    key: string;
    appId: string;
    secret: string;

    constructor({key, appId, secret}: { key: string, appId: string, secret: string }) {
        this.key = key;
        this.appId = appId;
        this.secret = secret;

        this.connection = new signalR.HubConnectionBuilder()
            .withUrl(HOST_NAME)
            .configureLogging(signalR.LogLevel.Debug)
            .build();
    }

    async start() {
        try {
            await this.connection.start();
            console.log("SignalR Connected.");
        } catch (err) {
            console.log(err);
            setTimeout(this.start, 5000);
        }
    };

    async trigger(channel: string, event: string, data: any) {

        await this.connection.invoke("Trigger", this.key, channel, event, data);


    }


}

export default PusherServer;
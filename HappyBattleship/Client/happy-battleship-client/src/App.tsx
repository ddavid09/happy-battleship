import { useEffect, useState } from "react";
import "./App.css";
import Board from "./components/board";
import { HubConnection, HubConnectionBuilder, LogLevel } from "@microsoft/signalr";

function App() {
  const [hub, setHub] = useState<HubConnection | null>(null);

  const InitHubConnection = () => {
    let hub = new HubConnectionBuilder()
      .withUrl("http://localhost:5000/battleshipHub")
      .withAutomaticReconnect()
      .configureLogging(LogLevel.Information)
      .build();

    hub.start().catch((error) => console.log("Error starting signalr hub", error));

    return hub;
  };

  const StopHubConnection = () => {
    hub?.stop().catch((error) => console.log("Error stopping signalr connecion", error));
    setHub(null);
  };

  useEffect(() => {
    setHub(InitHubConnection);
  }, []);

  return (
    <div className="boards">
      <Board hub={hub} side="Left" />
      <Board hub={hub} side="Right"/>
    </div>
  );
}

export default App;

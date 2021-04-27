import { useEffect, useState } from "react";
import { Guid } from "guid-typescript";
import "./App.css";
import Board from "./components/board";
import { HubConnection, HubConnectionBuilder, LogLevel } from "@microsoft/signalr";

const CLIENT_GUID = Guid.create().toString();

function App() {
  console.log("CLIENT GUID: " + CLIENT_GUID);

  const InitHubConnection = () => {
    let hub = new HubConnectionBuilder()
      .withUrl("http://localhost:5000/battleship")
      .withAutomaticReconnect()
      .configureLogging(LogLevel.Information)
      .build();

    return hub;
  };

  const [hub, setHub] = useState<HubConnection>(InitHubConnection);

  const StopHubConnection = () => {
    hub?.stop().catch((error) => console.log("Error stopping signalr connecion", error));
  };

  useEffect(() => {
    async function startHubConnection() {
      await hub
        .start()
        .then(() => hub.send("InitSimulation", CLIENT_GUID))
        .catch((error) => console.log("Error starting signalr hub", error));
    }

    startHubConnection();
  });

  return (
    <div className="boards">
      <Board hub={hub} side="left" />
      <Board hub={hub} side="right" />
      <button
        className="start-btn"
        onClick={() => {
          hub.send("StartSimulation", CLIENT_GUID);
        }}
      >
        Start
      </button>
    </div>
  );
}

export default App;

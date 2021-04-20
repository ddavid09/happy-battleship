import React, { useEffect, useState } from "react";
import "./App.css";
import { Position } from "./models/position";
import Board from "./components/board";
import { HubConnection, HubConnectionBuilder, LogLevel } from "@microsoft/signalr";

function App() {
  const [leftBoardPositions, setLeftBoardPositions] = useState<Position[][]>([]);
  const [rightBoardPositions, setRightBoardPositions] = useState<Position[][]>([]);

  const [hub, setHub] = useState<HubConnection | null>(null);

  const InitHubConnection = () => {
    let hub = new HubConnectionBuilder()
      .withUrl("http://localhost:5000/battleshipHub")
      .withAutomaticReconnect()
      .configureLogging(LogLevel.Information)
      .build();

    hub.start().catch((error) => console.log("Error starting signalr hub", error));

    hub.on("LoadBoards", (positions: Position[][]) => {
      const half = Math.ceil(positions.length / 2);

      const firstBoard = positions.splice(0, half);
      const secondBoard = positions.splice(-half);

      setLeftBoardPositions(firstBoard);
      setRightBoardPositions(secondBoard);
    });

    return hub;
  };

  const StopHubConnection = () => {
    hub?.stop().catch((error) => console.log("Error stopping signalr connecion", error));
    setHub(null);
  };

  useEffect(() => {
    setHub(InitHubConnection);
  }, []);

  useEffect(() => {
    var leftBoard2DArray: Position[][] = [];
    var rightBoard2DArray: Position[][] = [];

    for (var row: number = 0; row < 10; row++) {
      leftBoard2DArray[row] = [];
      rightBoard2DArray[row] = [];
      for (var col: number = 0; col < 10; col++) {
        var emptyPosition: Position = {
          x: row,
          y: col,
          state: 0,
        };

        leftBoard2DArray[row][col] = emptyPosition;
        rightBoard2DArray[row][col] = emptyPosition;
      }
    }

    setLeftBoardPositions(leftBoard2DArray);
    setRightBoardPositions(rightBoard2DArray);
  }, []);

  return (
    <div className="boards">
      <Board positions={leftBoardPositions} />
      <Board positions={rightBoardPositions} />
    </div>
  );
}

export default App;

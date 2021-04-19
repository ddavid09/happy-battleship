import React, { useEffect, useState } from "react";
import "./App.css";
import { Position } from "./models/position";
import Board from "./components/board";

function App() {
  const [leftBoardPositions, setLeftBoardPositions] = useState<Position[][]>([]);
  const [rightBoardPositions, setRightBoardPositions] = useState<Position[][]>([]);

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

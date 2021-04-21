import { HubConnection } from "@microsoft/signalr";
import { useEffect, useState } from "react";
import { Position } from "../models/position";
import BoardHeader from "./boardHeader";
import RowHeader from "./rowHeader";
import Square from "./square";

interface BoardProps {
  hub: HubConnection | null;
  side: string;
}

const BOARD_ROWS = 10;
const BOARD_COLS = 11;

const generateKey = (rowIndex: number, colIndex: number) => {
  return "" + rowIndex + colIndex;
};

const Board = ({ hub, side }: BoardProps) => {
  const initState = Array(100).fill({ x: 0, y: 0, state: 0 });

  const [boardPositionsState, setBoardPositionsState] = useState<Position[]>(initState);

  useEffect(() => {
    hub?.on(
      "updateBoardsState",
      (leftBoardJson: string, rightBoardJson: string, bothBoardJson: string) => {
        let newBoardPositions: Position[] = initState;
        if (side === "Left") {
          newBoardPositions = JSON.parse(leftBoardJson);
        } else if (side === "Right") {
          newBoardPositions = JSON.parse(rightBoardJson);
        }

        setBoardPositionsState(newBoardPositions);
      }
    );
    console.log("new " + side + "board listner created");
  }, []);

  return (
    <div className="board">
      <BoardHeader title={side + " Player"} />
      {[...Array(BOARD_ROWS)].map((_, rowIndex) =>
        [...Array(BOARD_COLS)].map((_, colIndex) => {
          if (colIndex === 0) {
            return <RowHeader key={colIndex} rowIndex={rowIndex} />;
          } else {
            return (
              <Square
                key={generateKey(rowIndex, colIndex - 1)}
                x={rowIndex}
                y={colIndex - 1}
                state={boardPositionsState[parseInt(generateKey(rowIndex, colIndex - 1))].state}
              />
            );
          }
        })
      )}
    </div>
  );
};

export default Board;

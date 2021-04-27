import { HubConnection } from "@microsoft/signalr";
import { useEffect, useState } from "react";
import { Position } from "../models/position";
import BoardHeader from "./boardHeader";
import RowHeader from "./rowHeader";
import Square from "./square";

interface BoardProps {
  hub: HubConnection;
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
    hub.on(
      "SimulationInitialised",
      (leftBoardJson: string, rightBoardJson: string, beginingSide: string) => {
        let newBoardPositions: Position[] = initState;
        if (side === "left") {
          newBoardPositions = JSON.parse(leftBoardJson);
        } else if (side === "right") {
          newBoardPositions = JSON.parse(rightBoardJson);
        }

        setBoardPositionsState(newBoardPositions);
      }
    );
    console.log("new " + side + "board listner created");

    hub.on("HandleNewTurn", (positionToUpdateJson: string, sideToUpdate: string) => {
      let positionToUpdate: Position = JSON.parse(positionToUpdateJson);
      console.log("to update: " + positionToUpdateJson + " on " + sideToUpdate + " board");
      if (side === "left" && sideToUpdate === "left") {
        setBoardPositionsState((prev) =>
          prev.map((position) =>
            position.x === positionToUpdate.x && position.y === positionToUpdate.y
              ? positionToUpdate
              : position
          )
        );
      } else if (side === "right" && sideToUpdate === "right") {
        setBoardPositionsState((prev) =>
          prev.map((position) =>
            position.x === positionToUpdate.x && position.y === positionToUpdate.y
              ? positionToUpdate
              : position
          )
        );
      }
    });
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

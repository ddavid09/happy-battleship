import { HubConnection } from "@microsoft/signalr";
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
                state={0}
              />
            );
          }
        })
      )}
    </div>
  );
};

export default Board;

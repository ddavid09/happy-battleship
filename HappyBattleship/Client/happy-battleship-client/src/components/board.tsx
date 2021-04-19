import { Position } from "../models/position";
import Square from "./square";

interface BoardProps {
  positions: Position[][];
}

const Board = ({ positions }: BoardProps) => {
  return (
    <div>
      {positions.map((row, rowIndex) =>
        row.map((_, colIndex) => <Square position={positions[rowIndex][colIndex]} />)
      )}
    </div>
  );
};

export default Board;

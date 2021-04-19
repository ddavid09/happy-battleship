import { Position } from "../models/position";
import BoardHeader from "./boardHeader";
import Square from "./square";

interface BoardProps {
  positions: Position[][];
}

const Board = ({ positions }: BoardProps) => {
  let rows: JSX.Element[] = [];
  positions.forEach((row, rowIndex) => {
    rows.push(<div className="board-square board-header">{rowIndex + 1}</div>);
    row.forEach((_, colIndex) => rows.push(<Square position={positions[rowIndex][colIndex]} />));
  });

  return (
    <div className="board">
      <BoardHeader />
      {rows.map((row) => row)}
    </div>
  );
};

export default Board;

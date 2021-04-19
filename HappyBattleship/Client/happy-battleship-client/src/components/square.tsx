import { Position } from "../models/position";

interface SquareProps {
  position: Position;
}

const Square = ({ position }: SquareProps) => {
  return (
    <div className="board-square">
      <p>X: {position.x}</p>
      <p>Y: {position.y}</p>
      <p>s: {position.state}</p>
    </div>
  );
};

export default Square;

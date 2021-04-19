import { Position } from "../models/position";

interface SquareProps {
  position: Position;
}

const Square = ({ position }: SquareProps) => {
  return (
    <div>
      <h4>positionX: {position.x}</h4>
      <h4>positionY: {position.y}</h4>
      <h4>state: {position.state}</h4>
    </div>
  );
};

export default Square;

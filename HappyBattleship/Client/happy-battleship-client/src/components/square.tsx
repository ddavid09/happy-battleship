interface SquareProps {
  x: number;
  y: number;
  state: number;
}

const Square = ({ x, y, state }: SquareProps) => {
  return (
    <div className="board-square">
      <p>X: {x}</p>
      <p>Y: {y}</p>
      <p>s: {state}</p>
    </div>
  );
};

export default Square;

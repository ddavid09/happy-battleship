import { useEffect, useState } from "react";

interface SquareProps {
  x: number;
  y: number;
  state: number;
}

const Square = ({ x, y, state }: SquareProps) => {
  const [squareState, setSquareState] = useState(0);

  useEffect(() => {
    setSquareState(state);
  }, [state]);

  return (
    <div className={squareState === 1 ? "board-square covered" : "board-square"}>
      <p>X: {x}</p>
      <p>Y: {y}</p>
      <p>s: {squareState}</p>
    </div>
  );
};

export default Square;

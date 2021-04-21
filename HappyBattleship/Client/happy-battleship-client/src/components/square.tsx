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
    <div
      className={
        squareState === 1
          ? "board-square covered"
          : squareState === 2
          ? "board-square missed"
          : squareState === 3
          ? "board-square hit"
          : "board-square"
      }
    ></div>
  );
};

export default Square;

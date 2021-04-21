interface RowHeaderProps {
  rowIndex: number;
}

const RowHeader = ({ rowIndex }: RowHeaderProps) => {
  return <div className="board-square board-header">{rowIndex}</div>;
};

export default RowHeader;

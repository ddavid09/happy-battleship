interface BoardHeaderProps {
  title: string;
}

const BoardHeader = ({ title }: BoardHeaderProps) => {
  const letters: string[] = [" ", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J"];

  return (
    <>
      <div className="board-header-title">{title}</div>
      {letters.map((l, i) => (
        <div key={i} className="board-square board-header">
          {l}
        </div>
      ))}
    </>
  );
};

export default BoardHeader;

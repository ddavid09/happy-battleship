const BoardHeader = () => {
  const letters: string[] = [" ", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J"];

  return (
    <>
      {letters.map((l) => (
        <div className="board-square board-header">{l}</div>
      ))}
    </>
  );
};

export default BoardHeader;

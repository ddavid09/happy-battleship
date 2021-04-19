namespace HappyBattleship.web
{
    public class Position
    {
        public char X { get; set; }
        public int Y { get; set; }
        public PositionState State { get; set; }

        public Position(char x, int y)
        {
            X = x;
            Y = y;
            State = PositionState.Initial;
        }

    }
}
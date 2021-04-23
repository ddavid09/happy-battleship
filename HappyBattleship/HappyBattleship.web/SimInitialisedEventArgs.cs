namespace HappyBattleship.web
{
    public class SimInitialisedEventArgs
    {
        public string PlayerBegining { get; set; }
        public Position[] LeftBoardToDraw { get; set; }
        public Position[] RightBoardToDraw { get; set; }
    }
}
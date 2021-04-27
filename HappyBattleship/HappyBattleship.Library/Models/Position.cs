using Newtonsoft.Json;

namespace HappyBattleship.Library
{
    public class Position
    {
        [JsonProperty("x")]
        public int X { get; set; }

        [JsonProperty("y")]
        public int Y { get; set; }

        [JsonProperty("state")]
        public PositionState State { get; set; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
            State = PositionState.Initial;
        }

    }
}
namespace Hubsson.Hackathon.Arcade.Client.Dotnet.Domain
{
    public class Action
    {
        public Direction direction { get; init; }
        public Direction Direction { get; internal set; }
        public int iteration { get; init; }
        public object ActionType { get; internal set; }
    }
}
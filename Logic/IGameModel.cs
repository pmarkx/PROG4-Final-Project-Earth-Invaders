using static Logic.GameLogic;

namespace Logic
{
    public interface IGameModel
    {
        SpaceItem[,] GameMatrix { get; set; }
    }
}

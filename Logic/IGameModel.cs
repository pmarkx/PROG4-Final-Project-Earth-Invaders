using Logic.Models;
using static Logic.GameLogic;

namespace Logic
{
    public interface IGameModel
    {
        GameObject[,] GameMatrix { get; set; }
    }
}
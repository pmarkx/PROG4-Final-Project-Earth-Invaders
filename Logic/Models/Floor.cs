namespace Logic.Models
{
    public class Floor : GameObject
    {
        public override int Priority => 0;

        public Floor(int xPosition, int yPosition) : base(xPosition, yPosition, "F", 99, false, 0, false)
        {
        }
    }
}
